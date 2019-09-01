using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

using Core.Register.LongProcessManagment;
using Core.Register.CustomConfiguration;
using ObjectModel.Core.LongProcess;
using System.Diagnostics;
using Npgsql;
using System.IO;
using Core.Shared.Extensions;
using Core.Messages;
using ObjectModel.Core.SRD;

namespace CIPJS.DAL.PostgresBackup
{
    public class PostgresBackupProcess : ILongProcess
    {
        private StringBuilder logger = new StringBuilder();

        OMQueue ProcessQueue { get; set; }

        private static StringBuilder pgOutput = new StringBuilder();
        private static StringBuilder pgError = new StringBuilder();

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            this.ProcessQueue = processQueue;

            DirectoryInfo backupFolder = null;

            this.logger.Append("\r\n");
            this.Log("Запуск PostgresBackupProcess");

            /* Извлекаем параметры конфигурации */
            this.Log("Извлекаем параметры конфигурации");
            PostgresBackupConfiguration sysConfig = PostgresBackupConfiguration.GetConfig();
            PostgresBackupConfiguration userConfig = processType.Parameters.DeserializeFromXml<PostgresBackupConfiguration>();
            if (userConfig != null)
            {
                sysConfig.PostgresDirectory = string.IsNullOrEmpty(userConfig.PostgresDirectory) ? sysConfig.PostgresDirectory : userConfig.PostgresDirectory;
                sysConfig.BaseBackupDirectory = string.IsNullOrEmpty(userConfig.BaseBackupDirectory) ? sysConfig.BaseBackupDirectory : userConfig.BaseBackupDirectory;
                sysConfig.PostgresWAL = string.IsNullOrEmpty(userConfig.PostgresWAL) ? sysConfig.PostgresWAL : userConfig.PostgresWAL;
                sysConfig.MaxBackupsCount = userConfig.MaxBackupsCount == 0 ? sysConfig.MaxBackupsCount : userConfig.MaxBackupsCount;
            }

            try
            {
                /* Извлечь пароль для супер-пользователя PostgreSQL */
                this.Log($"Извлекаем пароль для супер-пользователя PostgreSQL из '{sysConfig.PgPasswordPath}'");
                string pgPassword = GetPgPassword(sysConfig.PgPasswordPath);
                //if (string.IsNullOrEmpty(pgPassword))
                //    ThrowException($"Не удалось извлечь пароль для супер-пользователя PostgreSQL из '{Configuration.PgPasswordPath}'");

                string folderBaseBackup = DateTime.Now.ToString("yyyyMMdd_HHmm");
                string pgConnectionString = $"Host={sysConfig.PgHost};Port={sysConfig.PgPort};" +
                                            $"Username={sysConfig.PgUsername};Password={pgPassword};" +
                                            $"Database={sysConfig.PgDatabase};CommandTimeout=0;enlist=true";

                /* Создать папку для базового backup'а */
                backupFolder = new DirectoryInfo($"{sysConfig.BaseBackupDirectory}\\{folderBaseBackup}");
                this.Log($"Создание папки для бэкапа '{backupFolder.FullName}'");
                if (!backupFolder.Exists)
                    backupFolder.Create();
                else if (backupFolder.GetFileSystemInfos().Length > 0)
                    ThrowException($"Папка '{backupFolder.FullName}' не пуста");

                /* Создать точку восстановления */
                string restorePointName = $"{sysConfig.PgHost}:{sysConfig.PgPort}@{folderBaseBackup}";
                this.Log($"Создание точки восстановления '{restorePointName}'");
                string pgLSN = PgExecuteScalar(pgConnectionString, $"SELECT pg_create_restore_point('{restorePointName}')::varchar;");
                if (string.IsNullOrEmpty(pgLSN))
                    ThrowException($"Не удалось создать точку восстановления '{restorePointName}'");

                /* Запустить pg_basebackup (создать базовый backup) */
                Process pgBaseBackup = new Process();
                pgBaseBackup.StartInfo.RedirectStandardOutput = true;
                pgBaseBackup.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e) { pgOutput.Append(Environment.NewLine + "  " + e.Data); };
                pgBaseBackup.StartInfo.RedirectStandardError = true;
                pgBaseBackup.ErrorDataReceived += delegate (object sender, DataReceivedEventArgs e) { pgError.Append(Environment.NewLine + "  " + e.Data); };
                //pgBaseBackup.StartInfo.WorkingDirectory = $"\"{Configuration.PostgresDirectory}\\bin\"";
                pgBaseBackup.StartInfo.FileName = $"\"{sysConfig.PostgresDirectory}\\bin\\pg_basebackup.exe\"";
                pgBaseBackup.StartInfo.Arguments = $"-D {sysConfig.BaseBackupDirectory}\\{folderBaseBackup}" +
                    $" -d postgresql://{sysConfig.PgUsername}:{pgPassword}@{sysConfig.PgHost}:{sysConfig.PgPort}/{sysConfig.PgDatabase}";

                this.Log($"Запуск pg_basebackup '{pgBaseBackup.StartInfo.FileName}'" + $"-D {sysConfig.BaseBackupDirectory}\\{folderBaseBackup}" +
                    $" -d postgresql://{sysConfig.PgUsername}:********@{sysConfig.PgHost}:{sysConfig.PgPort}/{sysConfig.PgDatabase}");
                if (!pgBaseBackup.Start())
                    ThrowException($"Процесс '{pgBaseBackup.StartInfo.FileName}' не был запущен");

                /* Подождать завершения процесса pg_basebackup */
                this.Log("Ждем завершения pg_basebackup");
                pgBaseBackup.WaitForExit();
                this.Log($"{Environment.NewLine}Вывод: {pgOutput.ToString()}{Environment.NewLine}Ошибки:{pgError.ToString()}");

                /* Получить имя WAL записи по LSN точки восстановления */
                string walFileName = PgExecuteScalar(pgConnectionString, $"SELECT pg_walfile_name('{pgLSN}');");
                this.Log($"Получено имя WAL файла ('{walFileName}') для контрольной точки '{restorePointName}'");
                /* Запомнить имя WAL записи в папку с базовым backup'ом */
                using (StreamWriter writer = new StreamWriter($"{sysConfig.BaseBackupDirectory}\\{folderBaseBackup}\\filename.wal", false))
                {
                    this.Log($"Запись имени WAL файла в '{sysConfig.BaseBackupDirectory}\\{folderBaseBackup}\\filename.wal'");
                    writer.Write(walFileName);
                }

                /* Получить информацию о backup'ах */
                List<PostgresBackupInfo> postgresBackups = GetPostgresBackups(sysConfig.BaseBackupDirectory);
                this.Log($"Найдено бэкапов {postgresBackups.Count}");

                /* Удалить устаревшие backup'ы и почистить WAL архив */
                if (postgresBackups.Count > sysConfig.MaxBackupsCount)
                {
                    this.Log($"Удаляем неактуальные бэкапы из '{sysConfig.BaseBackupDirectory}'");
                    for (int i = 0; i < postgresBackups.Count - sysConfig.MaxBackupsCount; i++)
                        DeletePostgresBackup(sysConfig.BaseBackupDirectory, postgresBackups[i]);

                    string firstActualWAL = postgresBackups[postgresBackups.Count - sysConfig.MaxBackupsCount].WAL;
                    this.Log($"Чистим WAL архив '{sysConfig.PostgresWAL}', актуальный WAL {firstActualWAL}");
                    if (!string.IsNullOrEmpty(firstActualWAL))
                        TruncatePostgresWAL(sysConfig.PostgresWAL, firstActualWAL);
                }

                /* Завершение процесса */
                this.Log("PostgresBackupProcess успешно выполнен");

            }
            catch (Exception exception)
            {
                if (backupFolder != null && backupFolder.Exists)
                    backupFolder.Delete(true);

                this.LogError(null, exception);

				throw;
            }
        }

        private void SendMessage(string message)
        {
            List<OMRole> omRoles = OMRole.Where(w => w.IsAdmin == true).Execute();
            long[] roleIds = omRoles.Select(s => s.Id).ToArray();

            List<OMUserRole> omUsersRoles = OMUserRole.Where(w => roleIds.Contains(w.RoleId)).Execute();
            long[] userds = omUsersRoles.Select(s => s.UserId).ToArray();

            MessageService messageSvc = new MessageService();
            MessageDto messages = new MessageDto
            {
                IsEmail = false,
                UserIds = userds,
                Subject = "PostgresBackupService",
                Message = message
            };
            messageSvc.SendMessages(messages);
        }

        private void ThrowException(string message)
        {
            SendMessage(message);
            throw new ApplicationException(message);
        }


        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
            this.Log(string.Format("errorId:{0}, objectId:{1}, Exception: {2}", errorId ?? 0, objectId ?? 0, ex.Message));
        }

        private void Log(string message)
        {
            this.logger.Append("\r\n");
            this.logger.Append(string.Format("{0}: {1}", DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss.fff"), message));

            if (this.ProcessQueue != null)
            {
                //this.ProcessQueue.Message = "CIPJS.DAL.PostgresBackup.PostgresBackupProcess";
                this.ProcessQueue.Log = logger.ToString();
                this.ProcessQueue.Save();
            }
        }

        public bool Test()
        {
            return true;
        }

        #region Вспомогательные методы (статические)
        private string GetPgPassword(string pgPasswordPath)
        {
            return File.ReadAllText(pgPasswordPath);
        }

        private static string PgExecuteScalar(string pgConnectionString, string commandText)
        {
            object result;

            using (NpgsqlConnection connection = new NpgsqlConnection(pgConnectionString))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
                result = command.ExecuteScalar();
            }

            return (string)result;
        }

        private static List<PostgresBackupInfo> GetPostgresBackups(string baseBackupDirectory)
        {
            List<PostgresBackupInfo> postgresBackups = new List<PostgresBackupInfo>();
            /* Отсортировать папки по алфавиту */
            DirectoryInfo directoryInfo = new DirectoryInfo(baseBackupDirectory);
            foreach(DirectoryInfo dir in directoryInfo.EnumerateDirectories().OrderBy(o => o.Name))
            {
                PostgresBackupInfo backupInfo = new PostgresBackupInfo();

                backupInfo.Name = dir.Name;
                backupInfo.Created = dir.CreationTime;

                FileInfo walFilename = new FileInfo($"{dir.FullName}\\filename.wal");
                if(walFilename.Exists)
                    backupInfo.WAL = File.ReadAllText(walFilename.FullName);

                postgresBackups.Add(backupInfo);
            }

            return postgresBackups;
        }

        private static void DeletePostgresBackup(string baseBackupDirectory, PostgresBackupInfo backupInfo)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo($"{baseBackupDirectory}\\{backupInfo.Name}");
            directoryInfo.Delete(true);
        }

        private static void TruncatePostgresWAL(string postgresWAL, string firstActualWAL)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(postgresWAL);
            foreach(FileInfo walFile in directoryInfo.EnumerateFiles().Where(w => w.Name.CompareTo(firstActualWAL) == -1))
            {
                walFile.Delete();
            }
        }
        #endregion
    }
}