using System.Collections.Generic;
using KadOzenka.Dal.DataImport.Dto;
using ObjectModel.Common;
using ObjectModel.Core.SRD;

namespace KadOzenka.Dal.DataImport
{
    public class DataImporterService
    {
        public List<ImportDataLogDto> GetCommonDataLog(long registerId, long objectId)
        {
            var importDataLogs = OMImportDataLog.Where(x => x.RegisterId == registerId && x.ObjectId == objectId).SelectAll()
                .Execute();

            var result = new List<ImportDataLogDto>();
            importDataLogs.ForEach(x =>
            {
                var user = OMUser.Where(u => u.Id == x.UserId).SelectAll().ExecuteFirstOrDefault();

                result.Add(new ImportDataLogDto
                {
                    Id = x.Id,
                    CreationDate = x.DateCreated,
                    Author = user?.FullName,
                    FileName = x.DataFileName,
                    NumberOfImportedObjects = x.NumberOfImportedObjects,
                    TotalNumberOfObjects = x.TotalNumberOfObjects
                });
            });

            return result;
        }
    }
}
