using System;
using System.Collections.Generic;
using System.IO;
using Core.Main.FileStorages;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataExport
{
    public class UnloadResultStorageManager
    {
        public const string FileStorageName = "UnloadResultStorage";

        public static string GetUnloadResultFileName(OMUnloadResultFiles export) => $"{export.FileName}.{export.FileExtension}";
        public static string GetUnloadResultFileName(long resultFileId){
            var file = GetUnloadResultFile(resultFileId);
            return $"{file.FileName}.{file.FileExtension}";
        }

        public static string GetUnloadResultFileExtension(long resultFileId){
            var file = GetUnloadResultFile(resultFileId);
            return file.FileExtension;
        }

        public static List<OMUnloadResultFiles> GetAllFilesFromUnloadQueue(OMUnloadResultQueue queue)
        {
            return OMUnloadResultFiles
                .Where(x => x.UnloadId == queue.Id)
                .SelectAll()
                .Execute();
        }

        public static OMUnloadResultFiles GetUnloadResultFile(long resultFileId)
        {
            return OMUnloadResultFiles.Where(x => x.Id == resultFileId).SelectAll().ExecuteFirstOrDefault();
        }
        public static Stream GetUnloadResultFileById(long resultFileId)
        {
            var file = GetUnloadResultFile(resultFileId);
            if (file == null) return Stream.Null;

            return FileStorageManager.GetFileStream(FileStorageName, file.DateCreated, file.Id+"."+file.FileExtension);
        }

        public static bool CheckIfFileExists(long resultFileId)
        {
            var file = GetUnloadResultFile(resultFileId);
            if (file == null) return false;
            try
            {
                FileStorageManager.GetFileStream(FileStorageName, file.DateCreated, file.Id + "." + file.FileExtension);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public static string GetUnloadResultContentType(long resultFileId)
        {
            var file = GetUnloadResultFile(resultFileId);
            var res = file.FileExtension.ToLower() switch
            {
                "xls" => "application/vnd.ms-excel",
                "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "xml" => "text/xml",
                "zip" => "application/zip",
                _ => "application/octet-stream"
            };
            return res;
        }

        public static long SaveUnloadFile(Stream stream, string fileName, string extension, long unloadId, KoUnloadResultType unloadTypeCode = KoUnloadResultType.None)
        {
            var unloadFile = new OMUnloadResultFiles();
            unloadFile.DateCreated = DateTime.Now;
            unloadFile.FileName = fileName;
            unloadFile.FileExtension = extension;
            unloadFile.UnloadType_Code = unloadTypeCode;
            unloadFile.UnloadId = unloadId;
            var id = unloadFile.Save();
            FileStorageManager.Save(stream, FileStorageName, unloadFile.DateCreated, unloadFile.Id+"."+unloadFile.FileExtension);
            return id;
        }
    }
}