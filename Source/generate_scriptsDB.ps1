$path = "C:\TFSProjects\CIPJS\KadOzenka\Source"
Set-Location $path\Utils\GenerateDbScripts\
dotnet build $path\Utils\GenerateDbScripts\ -c Release
Set-Location "$path\Utils\GenerateDbScripts\bin\Release\netcoreapp2.1\"
dotnet GenerateDbScripts.dll
