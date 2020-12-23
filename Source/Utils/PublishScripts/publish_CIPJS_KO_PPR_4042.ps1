
$df =  (Get-Date).ToString("yyyy-MM-dd HH-mm")

$site = $IIS_siteName = $IIS_WebAppPoolName = "CIPJS_KO_PPR"
$service = $IIS_serviceName = $IIS_servicePoolName = "CIPJS_KO_PPR_LongProcessService"

$backup = $false
$backup_path = "D:\Backup\$site"
$Server = "192.168.3.67"

$GeneratedScripts = @("ExportData.sql","ExportTables.sql")

$release_path = "$PSScriptRoot"
$service_path = "$release_path\service"
$config_path =  "$release_path\config"
$site_path =    "$release_path\site"
$scripts_path = "$release_path\scripts"


if (-not (Test-Path $service_dir )){ New-Item  $service_dir -Force -ItemType Directory }
if (-not (Test-Path $sql_scripts_path )){ New-Item $sql_scripts_path -Force -ItemType Directory }
if (-not (Test-Path $backup_path\config\site )) { New-Item $backup_path\config\site -Force -ItemType Directory }
if (-not (Test-Path $backup_path\config\long_process )) { New-Item $backup_path\config\long_process -Force -ItemType Directory }


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
                
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Обновляем файлы sql-скриптов
                cpi -Path $scripts_path\* $sql_scripts_path -Force -Verbose
                #Invoke-Expression -Command  "C:\'!Выполнение скриптов'\ppr\ExecuteSqlFiles2.exe" 
                
               Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Делаем бэкап config-файлов
                cpi -Path $site_dir\*.config, $site_dir\appsettings.json $backup_path\config\site -Force -Verbose
                cpi -Path $service_dir\*.config, $service_dir\appsettings.json $backup_path\config\long_process -Force -Verbose
              
                if ($backup) {
                    compress-archive $site_dir $backup_path"$df.zip" 
                }
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Публикуем релиз сайта
                cpi -Path $site_path\* $site_dir\ -Recurse -Force
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Заменяем config-файлы сайта
                cpi -Path $backup_path\config\site\* $site_dir -Force -Verbose 
             
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Обновляем файлы службы фоновых процессов
                cpi -Path $service_path\* $service_dir\ -Recurse -Force  
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Заменяем config-файлы фоновых процессов
                cpi -Path $backup_path\config\long_process\* $service_dir -Force -Verbose

                Start-WebSite -Name $IIS_serviceName
                Start-WebAppPool -Name $IIS_servicePoolName
                
                Start-WebSite -Name $IIS_siteName 
                Start-WebAppPool -Name $IIS_WebAppPoolName

                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Релиз опубликован
                Invoke-WebRequest -Uri "http://localhost:40421/"
                Invoke-WebRequest -Uri "http://localhost:4042/"
                $success = $true;
            }
            Start-Sleep -s 5
            $currentRetry = $currentRetry + 1;
        }
    while (!$success -and $currentRetry -le 10)


    if(!$success){
        Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Ошибка публикации релиза
    }

	