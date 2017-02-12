#tool "GitVersion.CommandLine"
#addin "Cake.DocFx"
#tool "docfx.msbuild"
#tool "OpenCover"
#tool "nuget:?package=ReportGenerator"

#load "helpers.cake"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");
var framework = Argument<string>("framework", "net461,netstandard1.6");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var solutionPath = File("./WpfApplication.sln");
var solution = ParseSolution(solutionPath);
var projects = solution.Projects.Where(p => p.Type != "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}");
var projectPaths = projects.Select(p => p.Path.GetDirectory());
var frameworks = GetFrameworks(framework);
var artifacts = "./dist/";
GitVersion versionInfo = null;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");
	versionInfo = GitVersion();
	Information("Building for version {0}", versionInfo.FullSemVer);
	Information("Building against '{0}'", framework);
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
	.Does(() =>
{
	// Clean solution directories.
	foreach(var path in projectPaths)
	{
		Information("Cleaning {0}", path);
		CleanDirectories(path + "/**/bin/" + configuration);
		CleanDirectories(path + "/**/obj/" + configuration);
	}
	Information("Cleaning common files...");
	CleanDirectory(artifacts);
	DeleteFiles(GetFiles("./*.temp.nuspec"));
});

Task("Restore")
	.Does(() =>
{
	// Restore all NuGet packages.
	Information("Restoring solution...");
	//NuGetRestore(solutionPath);
	foreach(var project in projects) {
		DotNetCoreRestore(project.Path.GetDirectory() + "/project.json");
	}
});

Task("Build")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>
{
	Information("Building solution...");
	CreateDirectory(artifacts + "build/");
	foreach(var project in projects) {
		foreach(var f in frameworks) {
			//CreateDirectory(artifacts + "build/" + f);
			DotNetCoreBuild(project.Path.GetDirectory().FullPath, new DotNetCoreBuildSettings {
				//Framework = f,
				Configuration = configuration,
				//OutputDirectory = artifacts + "build/" + configuration + "/" + f
			});
		}
	}
});

Task("Post-Build")
	.IsDependentOn("Build")
	.Does(() => {
		foreach (var project in projects) {
			CopyDirectory(project.Path.GetDirectory() + "/bin/" + configuration, artifacts + "build/" + project.Name);
		}
	});

Task("Generate-Docs").Does(() => {
	DocFx("./docfx/docfx.json");
	Zip("./docfx/_site/", artifacts + "/docfx.zip");
});

Task("NuGet")
	.IsDependentOn("Post-Build")
//	.IsDependentOn("Copy-Core-Dependencies")
	.Does(() => {
		CreateDirectory(artifacts + "package/");
		Information("Building NuGet package");
		var nuspecFiles = GetFiles("./*.nuspec");
		var versionNotes = ParseAllReleaseNotes("./ReleaseNotes.md").FirstOrDefault(v => v.Version.ToString() == versionInfo.MajorMinorPatch);
		var content = GetContent(frameworks, artifacts + "build/WpfApplication/", "/WpfApplication");
		NuGetPack(nuspecFiles, new NuGetPackSettings() {
			Version = versionInfo.NuGetVersionV2,
			ReleaseNotes = versionNotes != null ? versionNotes.Notes.ToList() : new List<string>(),
			OutputDirectory = artifacts + "/package",
			Files = content,
			//KeepTemporaryNuSpecFile = true
			});
	});

///////////////////////////////////////////////////////////////////////////////
// TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
	.IsDependentOn("NuGet")
	.IsDependentOn("Generate-Docs");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);