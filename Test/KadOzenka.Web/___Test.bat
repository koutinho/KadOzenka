chcp 1251
DEL *.config
rmdir /s /q %cd%\wwwroot\imgLayer\
7z a -tzip "Backup %date% %time::=%.zip" C:\Users\silanov\Documents\Дженикс\Publish