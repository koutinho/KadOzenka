using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using Core.Main.FileStorages;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess
{
    public class GknXmlToExcelLongProcess : LongProcess
    {
        public const string LongProcessName = nameof(GknXmlToExcelLongProcess);

        public static void AddProcessToQueue(GknXmlConvertParams convertParams)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, parameters: convertParams.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);
            var convertParams = processQueue.Parameters.DeserializeFromXml<GknXmlConvertParams>();

            var id = convertParams.KOTaskId;
            var config = convertParams.config;
            var userId = convertParams.UserId;

            var attachments = OMImportDataLog.Where(x => x.RegisterId == OMTask.GetRegisterId() && x.ObjectId == id)
                .SelectAll().Execute();
            var omImportDataLogs = attachments.Where(a => a.DataFileName.EndsWith(".xml")).ToList();
            xmlImportGkn.FillDictionary(FileStorageManager.GetPathForStorage("SchemaPath"));

            var allObjects = new xmlObjectList();
            foreach (var log in omImportDataLogs)
            {
                xmlObjectList doc;
                try
                {
                    var fileName = DataImporterCommon.GetTemplateName(log.Id);
                    Console.WriteLine($"Log name: {fileName}");
                    var fs = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, log.DateCreated,
                        fileName);
                    doc = xmlImportGkn.GetXmlObject(fs);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                allObjects.Buildings.AddRange(doc.Buildings);
                allObjects.Flats.AddRange(doc.Flats);
                allObjects.CarPlaces.AddRange(doc.CarPlaces);
                allObjects.Constructions.AddRange(doc.Constructions);
                allObjects.Uncompliteds.AddRange(doc.Uncompliteds);
                allObjects.Parcels.AddRange(doc.Parcels);
            }

            WorkerCommon.SetProgress(processQueue, 50);

            var exporter = new XmlToExcelExport(allObjects, config);

            var result = exporter.GetResult();

            var memoryStream = new MemoryStream();

            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var (name, file) in result)
                {
                    var archiveFile = archive.CreateEntry(name + ".xlsx");

                    using (var entryStream = archiveFile.Open())
                    {
                        file.Save(entryStream, SaveOptions.XlsxDefault);
                    }
                }
            }

            memoryStream.Seek(0, SeekOrigin.Begin);


            var fsName = "DataExporterByTemplate";
            var dt = DateTime.Today;
            FileStorageManager.Save(memoryStream, fsName, dt, $"{id}_ExcelConversion.zip");

            if (userId != null)
                new MessageService().SendMessages(new MessageDto
                {
                    Addressers = new MessageAddressersDto {UserIds = new long[] {userId.Value}},
                    Subject = $"Конвертация xml в xlsx по задаче с идентификатором {id}",
                    Message =
                        $@"Процесс конвертации завершен. <a href=""/GknXmlToExcel/DownloadResult?dt={dt}&id={id}"">Скачать результаты</a>",
                    IsUrgent = true,
                    IsEmail = false
                });
            WorkerCommon.SetProgress(processQueue, 100);
        }

        public class GknXmlConvertParams
        {
            public int[] config;
            public long KOTaskId;
            public int? UserId;
        }
    }
}