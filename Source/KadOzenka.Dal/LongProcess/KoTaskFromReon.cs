using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using ObjectModel.Core.LongProcess;
using System.Threading;
using System.Threading.Tasks;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Tasks;
using Newtonsoft.Json;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess
{
    //TODO пока нет апи, поэтому процесс не доработан
    public class KoTaskFromReon : LongProcess
    {
        public const string LongProcessName = nameof(KoTaskFromReon);
        private string Url => "";
        private static HttpClient _httpClient;
        private TaskService TaskService { get; set; }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            //WorkerCommon.SetProgress(processQueue, 0);
            if(_httpClient == null)
                _httpClient = new HttpClient();
            TaskService = new TaskService();

            var requestForService = GetRequestForService();
            //var response = SendDataToService(_httpClient, Url, requestForService).GetAwaiter().GetResult();
            var response = string.Empty;
            ProcessServiceResponse(response);

            NotificationSender.SendNotification(processQueue, "Получения заданий на оценку из ИС РЕОН", "Операция выполнена успешно. Задания созданы. Загрузка добавлена в очередь, по результатам загрузки будет отправлено сообщение.");
            //WorkerCommon.SetProgress(processQueue, 100);
        }


        #region Support Methods

        private TaskFromReonRequest GetRequestForService()
        {
            var dateFrom = DateTime.Today.AddDays(-1);
            var dateTo = DateTime.Today.AddTicks(-1);

            return new TaskFromReonRequest(dateFrom, dateTo);
        }

        private async Task<string> SendDataToService(HttpClient httpClient, string url, object data)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new Exception("Не найден URL для сервиса");

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public void ProcessServiceResponse(string responseContentStr)
        {
            //var tasksFromResponse = JsonConvert.DeserializeObject<List<TaskFromReonResponse>>(responseContentStr);
            var tasksFromResponse = new List<TaskFromReonResponse>
            {
                new TaskFromReonResponse
                {
                    DownloadDate = DateTime.Today,
                    DocumentNumber = "DocumentNumber",
                    DocumentDate = DateTime.Today,
                    DocumentName = "DocumentName",
                    Organization = "Organization",
                    EstimationDate = DateTime.Today,
                    DownloadType = 1,
                    LinkToFileBase = "LinkToFileBase",
                    LinksToXmlFiles = new List<string> { "https://localhost:50252/DataImporterLayout/Download?importId=17618271&downloadResult=False&fileNameWithExtension=list_1_1A1B68C2-47F4-42DD-9AF5-8A921CBD9D96_deleted888.xml" }
                }
            };

            tasksFromResponse.ForEach(task =>
            {
                var documentId = TaskService.CreateDocument(task.DocumentNumber, task.DocumentName, task.DocumentDate);

                var omTask = CreateTask(task, documentId);

                foreach (var link in task.LinksToXmlFiles)
                {
                    ProcessFile(link, omTask.Id);
                }
            });
        }

        private OMTask CreateTask(TaskFromReonResponse task, long documentId)
        {
            var omTask = new OMTask
            {
                TourId = 2018, //TODO dto.TourYear,
                DocumentId = documentId,
                CreationDate = DateTime.Now,
                EstimationDate = task.EstimationDate,
                NoteType_Code = (ObjectModel.Directory.KoNoteType)task.DownloadType,
                Status_Code = ObjectModel.Directory.KoTaskStatus.InWork
            };
            omTask.Save();

            return omTask;
        }

        private void ProcessFile(string link, long taskId)
        {
            var data = GetFileData(link);
            var stream = new MemoryStream(data);
            var fileName = $"file_for_task_{taskId}.xml";

            DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", fileName,
                stream, OMTask.GetRegisterId(), taskId);
        }

        private static byte[] GetFileData(string address)
        {
            using (var client = new WebClient())
            {
                return client.DownloadData(address);
            }
        }

        #endregion

        #region Entities

        public class TaskFromReonRequest
        {
            [JsonProperty("dateFrom")]
            public DateTime DateFrom { get; set; }
            [JsonProperty("dateTo")]
            public DateTime DateTo { get; set; }

            public TaskFromReonRequest(DateTime dateFrom, DateTime dateTo)
            {
                DateFrom = dateFrom;
                DateTo = dateTo;
            }
        }

        public class TaskFromReonResponse
        {
            [JsonProperty("dateFrom")]
            public DateTime DownloadDate { get; set; }

            [JsonProperty("dateTo")]
            public string DocumentNumber { get; set; }

            [JsonProperty("dateTo")]
            public DateTime DocumentDate { get; set; }

            [JsonProperty("dateTo")]
            public string DocumentName { get; set; }

            [JsonProperty("dateTo")]
            public string Organization { get; set; }

            [JsonProperty("dateTo")]
            public DateTime EstimationDate { get; set; }

            [JsonProperty("dateTo")]
            public long DownloadType { get; set; }

            [JsonProperty("dateTo")]
            public string LinkToFileBase { get; set; }

            [JsonProperty("dateTo")]
            public List<string> LinksToXmlFiles { get; set; }
        }

        #endregion
    }
}
