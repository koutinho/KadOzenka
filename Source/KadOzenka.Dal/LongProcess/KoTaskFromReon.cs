using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ObjectModel.Core.LongProcess;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using IO.Swagger.Model;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Tasks;
using KadOzenka.WebClients;
using KadOzenka.WebClients.ReonClient.Api;
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
            WorkerCommon.SetProgress(processQueue, 0);
            TaskService = new TaskService();
            ReonWebClientService = new RosreestrDataApi();

            var request = GetRequest();
            var response = ReonWebClientService.RosreestrDataGetRRData(request.DateFrom, request.DateTo);
            var errorIds = ProcessResponse(response);
            var message = errorIds.Count > 0 
                ? $"Не удалось обработать все данные, подробно в журнале №{string.Join(", ", errorIds)}" 
                : "Операция выполнена успешно. Задания созданы. Загрузка добавлена в очередь, по результатам загрузки будет отправлено сообщение.";

            NotificationSender.SendNotification(processQueue, "Получение заданий на оценку из ИС РЕОН", message);
            WorkerCommon.SetProgress(processQueue, 100);
        }


        #region Support Methods

        private TaskFromReonRequest GetRequest()
        {
            var dateFrom = DateTime.Today.AddDays(-1);
            var dateTo = DateTime.Today;

            return new TaskFromReonRequest(dateFrom, dateTo);
        }

        public List<long> ProcessResponse(List<RRDataLoadModel> tasksFromResponse)
        {
            var errorIds = new List<long>();
            tasksFromResponse.ForEach(task =>
            {
                try
                {
                    var documentId = TaskService.CreateDocument(task.DocNumber, task.DocName, task.DocDate);

                    var omTask = CreateTask(task, documentId);

                    foreach (var fileInfo in task.XmlDocUrls)
                    {
                        ProcessFile(fileInfo, omTask.Id);
                    }
                }
                catch (Exception ex)
                {
                    long errorId = ErrorManager.LogError(ex);
                    errorIds.Add(errorId);
                }
            });

            return errorIds;
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

        private void ProcessFile(DocUrl fileInfo, long taskId)
        {
            var url = ReonServiceConfig.Current.BaseUrl + "/RosreestrData/" + fileInfo.Url;
            var data = GetFileData(url);
            var stream = new MemoryStream(data);

            DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", fileInfo.FileName,
                stream, OMTask.GetRegisterId(), taskId);
        }

        private static byte[] GetFileData(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadData(url);
            }
        }

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
