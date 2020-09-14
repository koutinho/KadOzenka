
$df =  (Get-Date).ToString("yyyy-MM-dd HH-mm")

$site = $IIS_siteName = $IIS_WebAppPoolName = "CIPJS_KO_TEST"
$site2 = $IIS_siteName2 = $IIS_WebAppPoolName2 = "CIPJS_KO_TEST_AD"
$service = $IIS_serviceName = $IIS_servicePoolName = "CIPJS_KO_TEST_LongProcessService"

$backup = $true
$backup_path = "D:\Backup_Site\$site\"
$Server = "192.168.3.67"


$site_dir = "C:\inetpub\wwwroot\$site"
$service_dir = "C:\inetpub\wwwroot\$service"

$sql_scripts_path = "C:\! Выполнение скриптов БД\Кадастровая оценка - TEST\! Скрипты"

$config_array = @("KadOzenka.Web.dll.config", "web.config", "app.config", "appsettings.json")
$service_array = @(
    "KadOzenka.LongProcessService.deps",
    "KadOzenka.LongProcessService.runtimeconfig"
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
Write-Host 1. Бэкап текущей версии
if ($backup) {
    Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Делаем бэкап
        foreach ($item in $config_array){
            cpi -Path $site_dir\$item $backup_path\config\ -Recurse -Force -Verbose
            cpi -Path $site_dir\$item $config_path -Recurse -Force -Verbose
        }

  
        cpi -Path $site_dir "$backup_path\backup\" -Recurse -Force
        Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Делаем бэкап
        compress-archive "$backup_path\backup\" $backup_path"backup_$site_$df.zip" 
    }

Write-Host 2. Публикация релиза 
    
    Import-Module WebAdministration

    Stop-WebSite  -Name $IIS_siteName
    Stop-WebAppPool -Name $IIS_WebAppPoolName
    
    Stop-WebSite  -Name $IIS_siteName2
    Stop-WebAppPool -Name $IIS_WebAppPoolName2
    
    Stop-WebSite  -Name $IIS_serviceName  
    Stop-WebAppPool -Name $IIS_servicePoolName

    Write-Host $IIS_siteName
    $currentRetry = 0
    $success = $false
    do{
        $status = Get-WebAppPoolState -name $IIS_WebAppPoolName2
        Write-Host $status.Value
        if ($status.Value -eq "Stopped"){
     
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Делаем бэкап
                foreach ($item in $config_array){
                    cpi -Path $site_dir\$item $backup_path\config -Recurse -Force -Verbose
                    cpi -Path $site_dir\$item $config_path -Recurse -Force -Verbose
                }
             
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Публикуем релиз сайта
                cpi -Path $site_path\* $site_dir\ -Recurse -Force
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Заменяем конфиги
                cpi -Path $config_path\* $site_dir -Force -Verbose 
                      
                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Обновляем файлы службы фоновых процессов
                $files = Get-ChildItem -Path $service_path -Recurse -Include *.dll #| Where-Object -FilterScript {($_.LastWriteTime -gt '2020-01-01')}
                cpi -Path $files  -Destination $service_dir -Force -Verbose 
                $files = Get-ChildItem -Path $service_path -Recurse -Include *.pdb 
                cpi -Path $files  -Destination $service_dir -Force -Verbose                
                foreach ($item in $service_array){
                     cpi -Path $service_path\$item $service_dir -Force -Verbose
                }

                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Обновляем sql-скрипты
                cpi -Path $scripts_path\* $sql_scripts_path -Force -Verbose

                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Запускаем сайты и приложения
                Start-WebAppPool -Name $IIS_servicePoolName
                Start-WebAppPool -Name $IIS_WebAppPoolName
                Start-WebAppPool -Name $IIS_WebAppPoolName2
                Start-WebSite    -Name $IIS_serviceName
                Start-WebSite    -Name $IIS_siteName 
                Start-WebSite    -Name $IIS_siteName2
                
                Invoke-WebRequest -Uri "http://localhost:8079/"
                Invoke-WebRequest -Uri "http://localhost:8080/"

                Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Релиз опубликован. Нужно выполнить запуск C:\! Выполнение скриптов БД\Кадастровая оценка - TEST\ExecuteSqlFiles2.exe с правами администратора

                $success = $true;
            }
            Start-Sleep -s 5
            $currentRetry = $currentRetry + 1;
        }
    while (!$success -and $currentRetry -le 10)

	if(!$success){
        Write-Host (Get-Date).ToString("yyyy-MM-dd HH:mm:ss") Ошибка публикации релиза
    }
   