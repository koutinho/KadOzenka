
$df =  (Get-Date).ToString("yyyy-MM-dd HH-mm")

$site = $IIS_siteName = $IIS_WebAppPoolName = "CIPJS_KO_PPR"
$service = $IIS_serviceName = $IIS_servicePoolName = "CIPJS_KO_PPR_LongProcessService"

$backup = $false
$backup_path = "D:\Backup\$site"
$Server = "192.168.3.67"

$site_dir = "C:\inetpub\wwwroot\$site"
$service_dir = "C:\inetpub\wwwroot\$service"
$sql_scripts_path = "C:\!Выполнение скриптов\ppr\! Скрипты"

$config_array = @("KadOzenka.Web.dll.config", "web.config", "app.config", "appsettings.json")
$service_array = @(
    "KadOzenka.LongProcessService",
    "KadOzenka.Dal",
    "KadOzenka.ObjectModel",
    "KadOzenka.WebClients",
    "Platform",
    "Platform.Web",
    "Platform.Reports"
)

$GeneratedScripts = @("ExportData.sql","ExportTables.sql")

$release_path = "$PSScriptRoot"
$service_path = "$release_path\service"
$config_path =  "$release_path\config"
$site_path =    "$release_path\site"
$scripts_path = "$release_path\scripts"


if (Test-Path -Path $service_dir -IsValid){ 
    New-Item -ItemType Directory -Force -Path $service_dir 
}

if (Test-Path -Path $backup_path -IsValid){ 
    New-Item -ItemType Directory -Force -Path $backup_path\config 
}


Write-Host 2. Публикация релиза 
    
    Import-Module WebAdministration
    Stop-WebSite  -Name $IIS_siteName
    Stop-WebAppPool -Name $IIS_WebAppPoolName
    
    Stop-WebSite  -Name $IIS_serviceName
    Stop-WebAppPool -Name $IIS_servicePoolName
    
    Write-Host $IIS_siteName
    $currentRetry = 0
    $success = $false
    do{
        $status = Get-WebAppPoolState -name $IIS_WebAppPoolName
        Write-Host $status.Value
        if ($status.Value -eq "Stopped"){
     
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Делаем бэкап
                foreach ($item in $config_array){
                    cpi -Path $site_dir\$item $backup_path\config -Recurse -Force -Verbose
                    cpi -Path $site_dir\$item $config_path -Recurse -Force -Verbose
                }
                if ($backup) {
                    compress-archive $site_dir $backup_path"$df.zip" 
                }
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Публикуем релиз сайта
                cpi -Path $site_path\* $site_dir\ -Recurse -Force
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Заменяем конфиги
                cpi -Path $config_path\* $site_dir -Force -Verbose 
                Start-WebSite -Name $IIS_siteName 
                Start-WebAppPool -Name $IIS_WebAppPoolName
               

                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Обновляем файлы службы фоновых процессов
                foreach ($item in $service_array){
                        cpi -Path $service_path\$item".dll" $service_dir -Force -Verbose
                        cpi -Path $service_path\$item".pdb" $service_dir -Force -Verbose
                }
                Start-WebSite -Name $IIS_serviceName
                Start-WebAppPool -Name $IIS_servicePoolName

                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Обновляем файлы sql-скриптов
                cpi -Path $scripts_path\* $sql_scripts_path -Force -Verbose
                #Invoke-Expression -Command  "C:\'!Выполнение скриптов'\ppr\ExecuteSqlFiles2.exe" 

                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Релиз опубликован
                $success = $true;
            }
            Start-Sleep -s 5
            $currentRetry = $currentRetry + 1;
        }
    while (!$success -and $currentRetry -le 10)


    if(!$success){
        Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Ошибка публикации релиза
    }
	