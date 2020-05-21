using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using ObjectModel.Core.LongProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using IO.Swagger.Model;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Tasks;
using KadOzenka.WebClients.ReonClient.Api;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess
{
    public class KoTaskFromReon : LongProcess
    {
        private TaskService TaskService { get; set; }
        private RosreestrDataApi ReonWebClientService { get; set; }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            //WorkerCommon.SetProgress(processQueue, 0);
            TaskService = new TaskService();
            ReonWebClientService = new RosreestrDataApi();

            var request = GetRequest();
            var response = ReonWebClientService.RosreestrDataGetRRData(request.DateFrom, request.DateTo);
            ProcessResponse(response);

            //WorkerCommon.SetProgress(processQueue, 100);
            NotificationSender.SendNotification(processQueue, "Получения заданий на оценку из ИС РЕОН",
                "Операция выполнена успешно. Задания созданы. Загрузка добавлена в очередь, по результатам загрузки будет отправлено сообщение.");
        }


        #region Support Methods

        private TaskFromReonRequest GetRequest()
        {
            var dateFrom = DateTime.Today.AddDays(-1);
            var dateTo = DateTime.Today;

            return new TaskFromReonRequest(dateFrom, dateTo);
        }

        public void ProcessResponse(List<RRDataLoadModel> tasksFromResponse)
        {
            tasksFromResponse.ForEach(task =>
            {
                var documentId = TaskService.CreateDocument(task.DocNumber, task.DocName, task.DocDate);

                var omTask = CreateTask(task, documentId);

                foreach (var fileInfo in task.XmlDocUrls)
                {
                    ProcessFile(fileInfo.FileName, task.Id, omTask.Id);
                }
            });
        }

        private OMTask CreateTask(RRDataLoadModel task, long documentId)
        {
            var omTask = new OMTask
            {
                TourId = 2018, //TODO TourId,
                DocumentId = documentId,
                CreationDate = DateTime.Now,
                EstimationDate = task.DateAppraisal,
                NoteType_Code = GetTaskNoteType(task.LoadType),
                Status_Code = KoTaskStatus.InWork
            };
            omTask.Save();

            return omTask;
        }


        private KoNoteType GetTaskNoteType(string loadType)
        {
            switch (loadType)
            {
                case "Плановый":
                    return KoNoteType.Initial;
                case "Вновь учтенные объекты":
                    return KoNoteType.Day;
                case "Внеплановый (полный)":
                    return KoNoteType.Year;
                default:
                    return KoNoteType.None;
            }
        }

        private void ProcessFile(string fileName, long? loadId, long taskId)
        {
            var file = ReonWebClientService.RosreestrDataGetFileByIdCA(loadId, fileName);
            if (file == null)
                return;

            //var stream = SerializeToStream(file);
            var byteArray = Encoding.UTF8.GetBytes(file);
            var stream = new MemoryStream(byteArray);
            DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", fileName,
                stream, OMTask.GetRegisterId(), taskId);
        }

        public static MemoryStream SerializeToStream(object obj)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream;
        }

        //TODO оставила код, пока в апи не реализована загрузка файла
        //private void ProcessFile(XmlDocUrls fileInfo, long taskId)
        //{
        //    var data = GetFileData(urlForFile);
        //    var stream = new MemoryStream(data);
        //    var fileName = fileInfo.FileName;

        //    DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", fileName,
        //        stream, OMTask.GetRegisterId(), taskId);
        //}

        //private static byte[] GetFileData(string url)
        //{
        //    using (var client = new WebClient())
        //    {
        //        return client.DownloadData(url);
        //    }
        //}

        #endregion


        #region Entities

        public class TaskFromReonRequest
        {
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }

            public TaskFromReonRequest(DateTime dateFrom, DateTime dateTo)
            {
                DateFrom = dateFrom;
                DateTo = dateTo;
            }
        }

        #endregion
    }
}
