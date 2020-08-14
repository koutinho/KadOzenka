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
        New-Item -ItemType Directory -Force -Path $config_path 
        New-Item -ItemType Directory -Force -Path $scripts_path 
        New-Item -ItemType Directory -Force -Path $archive_path
    }

    Set-Location $PSScriptRoot
    cd..    
    cd.. 
    $project_path = Get-Location
    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") ������� ������ �������: $project_path

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") ��������� �������� ��� ���� ������
    dotnet build C:\TFSProjects\CIPJS\KadOzenka\Source\Utils\GenerateDbScripts\ -c Release
    dotnet C:\TFSProjects\CIPJS\KadOzenka\Source\Utils\GenerateDbScripts\bin\Release\netcoreapp2.1\GenerateDbScripts.dll
    
    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") �������� ������ ��� ���������� KadOzenka.Web
    Set-Location $project_path\KadOzenka.Web
    #dotnet publish -c Release -o $site_path
    
    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") �������� ������ ��� ���������� KadOzenka.LongProcessService
    Set-Location $project_path\KadOzenka.LongProcessService
    #dotnet publish -c Release -o $service_path

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") �������� config-�����
    foreach ($item in $config_array){
        Move-Item -Path $site_path\$item -Destination $config_path -Force -Verbose
    }

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") �������� ������� ��� ���� ������
    #Start-Process -FilePath 'dotnet' -WorkingDirectory 'C:\TFSProjects\CIPJS\KadOzenka\Source\Utils\GenerateDbScripts\' -ArgumentList 'run --release'
    Set-Location "C:\TFSProjects\CIPJS\KadOzenka\Source\Utils\GenerateDbScripts\bin\Release\netcoreapp2.1\"
    foreach ($item in $GeneratedScripts){
        cpi -Path $project_path\Database\GeneratedScripts\$item $scripts_path -Force -Verbose
    }

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") �������� ������� ����������
    #cpi $project_path\Utils\PublishScripts\* $release_path\ -Force -Verbose

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") ���������� ���������
    #compress-archive $release_path\* $archive_path\$site_$df.zip 

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") ����������� �����

    Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") ����������� ������ ������ �� ��������� ������ $Server
    #cpi -Path $archive_path\$site_$df.zip "\\$Server\d$\Distr\$site" -Force
}
Write-Host (Get-Date).ToString("yyyy.MM.dd HH:mm:ss") "���������� ������� ��������� � �����: ExitCode = $LASTEXITCODE"

