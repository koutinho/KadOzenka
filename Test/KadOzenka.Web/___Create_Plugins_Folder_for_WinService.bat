if not exist "PlugIns" mkdir "PlugIns"

xcopy "Platform.dll" ".\PlugIns" /Y
xcopy "Platform.Reports.dll" ".\PlugIns" /Y
xcopy "Platform.Web.dll" ".\PlugIns" /Y

xcopy "KadOzenka.ObjectModel.dll" ".\PlugIns" /Y
xcopy "KadOzenka.Dal.dll" ".\PlugIns" /Y

xcopy /S /I /E "Config" ".\PlugIns\Config" /Y