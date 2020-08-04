using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using Core.Main.FileStorages;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.SessionManagment;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.XmlParser;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess
{
    public class KoImportGroupFromExcelLongProcess : LongProcess
    {
        public const string LongProcessName = nameof(KoImportGroupFromExcelLongProcess);

        public static void AddProcessToQueue(ExcelFile excelFile, string registerViewId,
            int mainRegisterId, long tourId, List<long> taskFilter)
        {
            var parameters = new ImportParamsWithTaskFilter
            {
                excelFile = excelFile,
                registerViewId = registerViewId,
                mainRegisterId = mainRegisterId,
                tourId = tourId,
                taskFilter = taskFilter
            };
            LongProcessManager.AddTaskToQueue(LongProcessName, parameters: parameters.SerializeToXml());
        }

        public static void AddProcessToQueue(ExcelFile excelFile, string registerViewId, int mainRegisterId,
            long tourId, ObjectModel.Directory.KoUnitStatus unitStatus)
        {
            var parameters = new ImportParamsWithUnitStatus
            {
                excelFile = excelFile,
                registerViewId = registerViewId,
                mainRegisterId = mainRegisterId,
                tourId = tourId,
                unitStatus = unitStatus
            };
            LongProcessManager.AddTaskToQueue(LongProcessName, parameters: parameters.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            //TODO: добавлен процесс ImportDataGroupNumberFromExcelLongProcess
            //WorkerCommon.SetProgress(processQueue, 0);

            //ImportParamsWithTaskFilter paramsWithTaskFilter;
            //Exception e;
            //var deserializeTaskSuccess = processQueue.Parameters.TryDeserializeFromXml(out paramsWithTaskFilter, out e);

            //MemoryStream resultStream;
            //string fileName;
            //WorkerCommon.SetProgress(processQueue, 10);
            //if (deserializeTaskSuccess)
            //{
            //    fileName = paramsWithTaskFilter.excelFile.DocumentProperties.Custom["Filename"].ToString();
            //    resultStream = (MemoryStream)DataImporterKO.ImportDataGroupNumberFromExcel(
            //        paramsWithTaskFilter.excelFile,
            //        paramsWithTaskFilter.registerViewId,
            //        paramsWithTaskFilter.mainRegisterId,
            //        paramsWithTaskFilter.tourId,
            //        paramsWithTaskFilter.taskFilter);
            //}
            //else
            //{
            //    ImportParamsWithUnitStatus paramsWithUnitStatus = processQueue.Parameters.DeserializeFromXml<ImportParamsWithUnitStatus>();

            //    fileName = paramsWithUnitStatus.excelFile.DocumentProperties.Custom["Filename"].ToString();
            //    resultStream = (MemoryStream)DataImporterKO.ImportDataGroupNumberFromExcel(
            //        paramsWithUnitStatus.excelFile,
            //        paramsWithUnitStatus.registerViewId,
            //        paramsWithUnitStatus.mainRegisterId,
            //        paramsWithUnitStatus.tourId,
            //        paramsWithUnitStatus.unitStatus);

            //}
            //WorkerCommon.SetProgress(processQueue, 80);

            //var fsName = "DataImporterFromTemplate";
            //var dt = DateTime.Today;
            //var ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            //FileStorageManager.Save(resultStream, fsName, dt, $"{ts}_{fileName}");

            //var userId = SRDSession.Current.UserID;
            //WorkerCommon.SetProgress(processQueue, 90);
            //new MessageService().SendMessages(new MessageDto
            //{
            //    Addressers = new MessageAddressersDto {UserIds = new long[] {userId}},
            //    ///Subject = $"Импорт групп из Excel ({fileName})",
            //    //Message =
            //    //    $@"Процесс импорта завершен. <a href=""/Tour/DownloadResult?dt={dt}&fileName={ts}_{fileName}"">Скачать результаты</a>",
            //    IsUrgent = true,
            //    IsEmail = false
            //});
            //WorkerCommon.SetProgress(processQueue, 100);
        }

        private class ImportParams
        {
            public ExcelFile excelFile;
            public string registerViewId;
            public int mainRegisterId;
            public long tourId;
        }

        private class ImportParamsWithTaskFilter : ImportParams
        {
            public List<long> taskFilter;
        }

        private class ImportParamsWithUnitStatus : ImportParams
        {
            public KoUnitStatus unitStatus;
        }
    }
}