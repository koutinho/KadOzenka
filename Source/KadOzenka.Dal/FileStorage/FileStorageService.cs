using Core.Main.FileStorages;
using Core.Shared.Extensions;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace CIPJS.DAL.FileStorage
{
    public class FileStorageService
    {
        public static string FileStorageFolder
        {
            get
            {
				return FileStorageManager.GetPathForStorage("FileStorageFolder");
            }
        }

        /// <summary>
        /// Создает виртуальную директорию для файлов
        /// </summary>
        /// <param name="periodRegDate">Период учета</param>
        /// <returns>Идентификатор виртуальной директории</returns>
        public long CreateVirtualDirectory(DateTime? periodRegDate = null)
        {
            OMFileStorage fileStorage = new OMFileStorage();
            fileStorage.IsVirtualDirectory = true;
            fileStorage.PeriodRegDate = periodRegDate;
            return fileStorage.Save();
        }

        /// <summary>
        /// Сохраняет файл в папку указанную в конфигурации FileStorageFolder
        /// </summary>
        /// <param name="stream">Файл</param>
        /// <param name="fileName">Название файла</param>
        /// <param name="virtualDirectoryId">Связь с виртуальной директорией</param>
        /// <param name="periodRegDate">Период учета</param>
        /// <returns>Идентификатор файла</returns>
        public long Save(Stream stream, string fileName, long? virtualDirectoryId = null, DateTime? periodRegDate = null, bool checkExistsFile = false)
        {
            string hash;

            using (MD5 md5 = MD5.Create())
            {
                hash = Convert.ToBase64String(md5.ComputeHash(stream));

                stream.Seek(0, SeekOrigin.Begin);
            }

            if (checkExistsFile)
            {
                OMFileStorage omFileStorage = OMFileStorage.Where(x => x.Hash == hash).Execute().FirstOrDefault();

                if (omFileStorage != null)
                {
                    throw new Exception($"Файл \"{fileName}\" (поиск по хэшу \"{hash}\") был загружен ранее");
                }
            }

            OMFileStorage fileStorage = OMFileStorage.CreateEmpty();

            if (!Directory.Exists(FileStorageFolder))
            {
                Directory.CreateDirectory(FileStorageFolder);
            }

            string fullFileName = Path.Combine(FileStorageFolder, fileStorage.Id.ToString());
            fileStorage.Filename = Path.GetFileName(fileName);
            fileStorage.VirtualDirectoryId = virtualDirectoryId;
            fileStorage.PeriodRegDate = periodRegDate;
            fileStorage.Hash = hash;
            using (FileStream fs = File.Create(fullFileName))
            {
                fs.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fs);
            }

            return fileStorage.Save();
        }

        /// <summary>
        /// Получает запись из реестра файлов по идентификатору
        /// </summary>
        /// <param name="fileId">Идентификатор файла</param>
        /// <returns></returns>
        public OMFileStorage Get(long fileId)
        {
            OMFileStorage fileStorage = OMFileStorage
                .Where(x => x.Id == fileId)
                .SelectAll()
                .Execute()
                .FirstOrDefault();

            if (fileStorage == null)
            {
                throw new Exception($"Не удалось определить файл по идентификатору {fileId}");
            }

            return fileStorage;
        }

        /// <summary>
        /// Получает записи из реестра файлов, относящиеся к виртуальной директории
        /// </summary>
        /// <param name="virtualDirectoryId">Идентификатор виртуальной директории</param>
        /// <returns></returns>
        public List<OMFileStorage> GetVirtualDirectoryFiles(long virtualDirectoryId)
        {
            return OMFileStorage
                .Where(x => x.VirtualDirectoryId == virtualDirectoryId)
                .SelectAll()
                .Execute();
        }

        /// <summary>
        /// Загружает в память файл из хранилища
        /// </summary>
        /// <param name="fileStorage">Идентификатор файла</param>
        /// <returns></returns>
        public FileStream GetFileStream(OMFileStorage fileStorage)
        {
            if (fileStorage == null
                || (fileStorage.IsVirtualDirectory.HasValue && fileStorage.IsVirtualDirectory.Value))
            {
                return null;
            }

            string fullFileName = Path.Combine(FileStorageFolder, fileStorage.Id.ToString());

            if (!File.Exists(fullFileName))
            {
                throw new Exception($"Файл отстуствует в каталоге загрузки (FileStorageFolder)");
            }

            return File.OpenRead(fullFileName);
        }
    }
}
