using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using ObjectModel.Core.LongProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Tasks;
using Newtonsoft.Json;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess
{
    public class KoTaskFromReon : LongProcess
    {
        private const string MAIN_URL = "http://localhost/cadAppraisal/cadappraisaldataapi/RosreestrData/xml_by_date";
        private const string BASE_URL_FOR_FILE = "http://localhost/cadAppraisal/cadappraisaldataapi/RosreestrData";
        private static HttpClient _httpClient;
        private TaskService TaskService { get; set; }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            //WorkerCommon.SetProgress(processQueue, 0);
            if(_httpClient == null)
                _httpClient = new HttpClient();
            TaskService = new TaskService();

            var request = GetRequest();
            var response = SendDataToService(request).GetAwaiter().GetResult();
            ProcessResponse(response);

            //WorkerCommon.SetProgress(processQueue, 100);
            NotificationSender.SendNotification(processQueue, "Получения заданий на оценку из ИС РЕОН",
                "Операция выполнена успешно. Задания созданы. Загрузка добавлена в очередь, по результатам загрузки будет отправлено сообщение.");
        }


        #region Support Methods

        private TaskFromReonRequest GetRequest()
        {
            var dateFrom = DateTime.Today.AddDays(-1);
            var dateTo = DateTime.Today.AddTicks(-1);

            return new TaskFromReonRequest(dateFrom, dateTo);
        }

        private async Task<string> SendDataToService(TaskFromReonRequest request)
        {
            var builder = new UriBuilder(MAIN_URL);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["dateFrom"] = request.DateFrom.ToShortDateString();
            query["dateTo"] = request.DateTo.ToShortDateString();
            builder.Query = query.ToString();
            var url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public void ProcessResponse(string responseContentStr)
        {
            var tasksFromResponse = JsonConvert.DeserializeObject<List<TaskFromReonResponse>>(responseContentStr);

            tasksFromResponse.ForEach(task =>
            {
                var documentId = TaskService.CreateDocument(task.DocNumber, task.DocName, task.DocDate);

                var omTask = CreateTask(task, documentId);

                foreach (var fileInfo in task.XmlDocUrls)
                {
                    ProcessFile(fileInfo, omTask.Id);
                }
            });
        }

        private OMTask CreateTask(TaskFromReonResponse task, long documentId)
        {
            var omTask = new OMTask
            {
                TourId = 2018, //TODO TourId,
                DocumentId = documentId,
                CreationDate = DateTime.Now,
                EstimationDate = task.DateAppraisal,
                NoteType_Code = ObjectModel.Directory.KoNoteType.Day, //TODO Converter
                Status_Code = ObjectModel.Directory.KoTaskStatus.InWork
            };
            omTask.Save();

            return omTask;
        }

        private void ProcessFile(XmlDocUrls fileInfo, long taskId)
        {
            var urlForFile = BASE_URL_FOR_FILE + fileInfo.Url;
            var data = GetFileData(urlForFile);
            var stream = new MemoryStream(data);
            var fileName = fileInfo.FileName;

            DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", fileName,
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

        public class TaskFromReonResponse
        {
            public DateTime LoadDate { get; set; }

            public string DocNumber { get; set; }

            public DateTime DocDate { get; set; }

            public string DocName { get; set; }

            public string OrgName { get; set; }

            public DateTime DateAppraisal { get; set; }

            public string LoadType { get; set; }

            public XmlDocUrls DocBaseUrl { get; set; }

            public List<XmlDocUrls> XmlDocUrls { get; set; }
        }

        public class XmlDocUrls
        {
            public string FileName { get; set; }

            public string Url { get; set; }
        }

        #endregion
    }
}
