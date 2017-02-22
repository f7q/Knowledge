# dotnet aspnet-codegenerator コマンド

- Microsoft.VisualStudio.Web.CodeGeneration.Tools
- Microsoft.VisualStudio.Web.CodeGenerators.Mvc

Visual Studio 2015 のGUI無しでRazor画面をscaffolldする方法。  
出力ログからキャッチアップ。

## Windows
### Razor CRUD
--useDefaultLayout => ./Views/_ViewStart.chtml  
```cmd
dotnet.exe aspnet-codegenerator ^
  --project "./" controller ^
  --force --controllerName ValuesController ^
  --model WebApplication.Models.Value ^
  --relativeFolderPath Controllers ^
  --controllerNamespace WebApplication.Controllers ^
  --referenceScriptLibraries ^
  --useDefaultLayout ^
  --dataContext WebApplication.Models.SQLiteDbContext
```
### WEB API CRUD
--restWithNoViews => REST API設定  
```cmd
dotnet.exe aspnet-codegenerator ^
  --project "./" controller ^
  --force --controllerName ValuesController ^
  --model WebApplication.Models.Value ^
  --dataContext WebApplication.Models.SQLiteDbContext ^
  --relativeFolderPath Controllers ^
  --controllerNamespace WebApplication.Controllers ^
  --restWithNoViews
```

## Linux
```sh
dotnet aspnet-codegenerator \
  --project "./" controller \
  --force --controllerName ValuesController \
  --model WebApplication.Models.Value \
  --relativeFolderPath Controllers \
  --controllerNamespace WebApplication.Controllers \
  --referenceScriptLibraries \
  --useDefaultLayout \
  --dataContext WebApplication.Models.SQLiteDbContext
```
