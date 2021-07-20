using System.Collections.Generic;
using CommonSdks;
using KadOzenka.Dal.DataImport.Dto;
using ObjectModel.Common;
using ObjectModel.Core.SRD;

namespace KadOzenka.Dal.DataImport
{
    public class DataImporterService
    {
        public List<ImportDataLogDto> GetCommonDataLog(long registerId, long objectId)
        {
            var importDataLogs = OMImportDataLog.Where(x => x.RegisterId == registerId && x.ObjectId == objectId)
                .SelectAll()
                .Select(x => x.ParentUser.FullName)
                .Execute();

            var result = new List<ImportDataLogDto>();
            importDataLogs.ForEach(x =>
            {
                result.Add(new ImportDataLogDto
                {
                    Id = x.Id,
                    CreationDate = x.DateCreated,
                    Author = x.ParentUser?.FullName,
                    FileName = DataImporterCommon.GetDownloadDataFileName(x),
                    NumberOfImportedObjects = x.NumberOfImportedObjects,
                    TotalNumberOfObjects = x.TotalNumberOfObjects
                });
            });

            return result;
        }
    }
}
