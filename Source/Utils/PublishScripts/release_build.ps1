$ErrorActionPreference = "Stop"
#$VerbosePreference = "continue"

$consoleAllocated = [Environment]::UserInteractive
function AllocConsole()
{
    if($Global:consoleAllocated)
    {
        return
    }

    &cmd /c ver | Out-Null
    $a = @' 
[DllImport("kernel32", SetLastError = true)] 
public static extern bool AllocConsole(); 
'@

    $params = New-Object CodeDom.Compiler.CompilerParameters 
    $params.MainClass = "methods" 
    $params.GenerateInMemory = $true 
    $params.CompilerOptions = "/unsafe" 
 
    $r = Add-Type -MemberDefinition $a -Name methods -Namespace kernel32 -PassThru -CompilerParameters $params

    Write-Verbose "Allocating console"
    [kernel32.methods]::AllocConsole() | Out-Null
    Write-Verbose "Console allocated"
    $Global:consoleAllocated = $true
}

function RunConsole($scriptBlock)
{
    AllocConsole

    $encoding = [Console]::OutputEncoding 
   [Console]::OutputEncoding = [System.Text.Encoding]::GetEncoding("cp866")
    $prevErrAction = $ErrorActionPreference
    $ErrorActionPreference = "Continue"
    try
    {
        & $scriptBlock
    }
    finally
    {
        $ErrorActionPreference = $prevErrAction
        [Console]::OutputEncoding = $encoding
    }
}

RunConsole {
    #& $PSScriptRoot\ConsoleApp2.exe
    #Set-ExecutionPolicy -Scope CurrentUser
    #Set-ExecutionPolicy RemoteSigned

    $df =  (Get-Date).ToString("yyyy-MM-dd HH-mm")
    $root = "C:\release"
    $project = "CIPJS_KO"
    $Server = "192.168.3.67"

    $release_path = "$root\$project\$df"
    $service_path = "$release_path\service"
    $config_path =  "$release_path\config"
    $site_path =    "$release_path\site"
    $scripts_path = "$release_path\scripts"
    $archive_path = "$root\$project\archive"

    $config_array = @("KadOzenka.Web.dll.config", "web.config", "appsettings.json")
    $GeneratedScripts = @("ExportData.sql","ExportTables.sql")
    
    if (Test-Path -Path $release_path -IsValid){ 
        New-Item -ItemType Directory -Force -Path $release_path 
        New-Item -ItemType Directory -Force -Path $site_path 
        New-Item -ItemType Directory -Force -Path $config_path 
        New-Item -ItemType Directory -Force -Path $scripts_path 
        New-Item -ItemType Directory -Force -Path $archive_path
    }

    Set-Location $PSScriptRoot
    cd..
    cd..
    $project_path = Get-Location
    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") Каталог файлов проекта: $project_path
   
    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") Создание файлов для публикации KadOzenka.Web
    Set-Location $project_path\KadOzenka.Web
    dotnet publish -c Release -o $site_path\
    
    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") Создание файлов для публикации KadOzenka.LongProcessService
    Set-Location $project_path\KadOzenka.LongProcessService
    dotnet publish -c Release -o $service_path

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") Отделяем config-файлы
    foreach ($item in $config_array){
        Move-Item -Path $site_path\$item -Destination $config_path -Force -Verbose
    }
    
    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") Генерация скриптов для базы данных
    dotnet build $project_path\Utils\GenerateDbScripts\ -c Release
    #dotnet $project_path\GenerateDbScripts\bin\Release\netcoreapp2.1\GenerateDbScripts.dll
    #Set-Location $project_path\Utils\GenerateDbScripts\bin\Release\netcoreapp2.1\
    #Start-Process -FilePath 'dotnet' -WorkingDirectory '$project_path\Utils\GenerateDbScripts\Release\netcoreapp2.1\GenerateDbScripts.dll' -ArgumentList 'run --release'
    
    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") Копируем скрипты для базы данных
    foreach ($item in $GeneratedScripts){
        cpi -Path $project_path\Database\GeneratedScripts\$item $scripts_path -Force -Verbose
    }

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") Копируем скрипты публикации
    cpi $project_path\Utils\PublishScripts\* $release_path\ -Force -Verbose

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") Архивируем артефакты
    compress-archive $release_path\* $archive_path\$project_$df.zip 

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") Дистрибутив готов

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") Копирование архива релиза на удаленный сервер $Server
    cpi -Path $archive_path\$project_$df.zip "\\$Server\d$\Distr\$project" -Force
}
Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") "Выполнение скрипта завершено с кодом: ExitCode = $LASTEXITCODE"

