# dotnet aspnet-codegenerator コマンド

- Microsoft.VisualStudio.Web.CodeGeneration.Tools
- Microsoft.VisualStudio.Web.CodeGenerators.Mvc

## Windows
dotnet.exe aspnet-codegenerator ^
  --project "./" controller ^
  --force --controllerName ValuesController ^
  --model WebApplication.Models.Value ^
  --relativeFolderPath Controllers ^
  --controllerNamespace WebApplication.Controllers ^
  --referenceScriptLibraries ^
  --useDefaultLayout ^
  --dataContext WebApplication.Models.SQLiteDbContext

## Linux
dotnet aspnet-codegenerator \
  --project "./" controller \
  --force --controllerName ValuesController \
  --model WebApplication.Models.Value \
  --relativeFolderPath Controllers \
  --controllerNamespace WebApplication.Controllers \
  --referenceScriptLibraries \
  --useDefaultLayout \
  --dataContext WebApplication.Models.SQLiteDbContext