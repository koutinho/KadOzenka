using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using ObjectModel.Core.LongProcess;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.LongProcessManagment;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Tasks;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess
{
    //TODO пока нет апи, поэтому процесс не доработан
    public class KoFactorsFromReon : LongProcess
    {
        public const string LongProcessName = nameof(KoFactorsFromReon);
        private string Url => "";
        private static HttpClient _httpClient;
        private TaskService TaskService { get; set; }

        public static void AddProcessToQueue(long taskId)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, objectId: taskId, registerId: OMTask.GetRegisterId());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            //WorkerCommon.SetProgress(processQueue, 0);
            if (!processQueue.ObjectId.HasValue)
            {
                WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
                WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
                return;
            }

            if (_httpClient == null)
                _httpClient = new HttpClient();

            var task = GetTask(processQueue.ObjectId.Value);
            var units = GetUnits(task.Id);

            units.ForEach(unit =>
            {
                var request = GetRequest(task, unit);
                //var response = SendDataToService(_httpClient, Url, request).GetAwaiter().GetResult();
                var response = string.Empty;
                ProcessServiceResponse(response);
            });

            NotificationSender.SendNotification(processQueue, "Получения заданий на оценку из ИС РЕОН", "Операция выполнена успешно. Задания созданы. Загрузка добавлена в очередь, по результатам загрузки будет отправлено сообщение.");
            //WorkerCommon.SetProgress(processQueue, 100);
        }


        #region Support Methods

        private OMTask GetTask(long taskId)
        {
            return OMTask.Where(x => x.Id == taskId)
                .Select(x => x.EstimationDate)
                .ExecuteFirstOrDefault();
        }

        private List<OMUnit> GetUnits(long taskId)
        {
            return OMUnit.Where(x =>
                    x.TaskId == taskId && (x.PropertyType_Code == PropertyTypes.Building ||
                                           x.PropertyType_Code == PropertyTypes.Stead))
                .Select(x => x.CadastralNumber)
                .Execute();
        }

        private FactorsFromReonRequest GetRequest(OMTask task, OMUnit unit)
        {
            return new FactorsFromReonRequest(unit.CadastralNumber, task.EstimationDate);
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
            //var factors = JsonConvert.DeserializeObject<List<FactorsFromReonResponse>>(responseContentStr);
            var factors = new List<FactorsFromReonResponse>
            {
               new FactorsFromReonResponse
               {
                   CadastralNumber = "CadastralNumber",
                   GraphicFactors = new List<string>{ "GraphicFactors" },
                   SourceCadastralNumber = "SourceCadastralNumber",
                   SourceLayerName = "SourceLayerName",
                   CalculationFactorLayerName = "CalculationFactorLayerName",
                   CalculationType = "CalculationType",
                   FactorName = "FactorName",
                   FactorValue = 0,
                   ObjectName = "ObjectName",
                   FactorValueByQuartal = 0,
                   ObjectNameByQuartal = "ObjectNameByQuartal",
                   CalculationDate = DateTime.Today
               }
            };

            factors.ForEach(task =>
            {
                
            });
        }

        #endregion

        #region Entities

        public class FactorsFromReonRequest
        {
            [JsonProperty("CadastralNumber")]
            public string CadastralNumber { get; set; }

            [JsonProperty("EstimationdDate")]
            public DateTime? EstimationDate { get; set; }

            public FactorsFromReonRequest(string cadastralNumber, DateTime? estimationDate)
            {
                CadastralNumber = cadastralNumber;
                EstimationDate = estimationDate;
            }
        }

        public class FactorsFromReonResponse
        {
            [JsonProperty("CadastralNumber")]
            public string CadastralNumber { get; set; }

            [JsonProperty("GraphicFactors")]
            public List<string> GraphicFactors { get; set; }

            [JsonProperty("SourceCadastralNumber")]
            public string SourceCadastralNumber { get; set; }

            [JsonProperty("SourceLayerName")]
            public string SourceLayerName { get; set; }

            [JsonProperty("CalculationFactorLayerName")]
            public string CalculationFactorLayerName { get; set; }

            [JsonProperty("CalculationType")]
            public string CalculationType { get; set; }

            [JsonProperty("FactorName")]
            public string FactorName { get; set; }

            [JsonProperty("FactorValue")]
            public double FactorValue { get; set; }

            [JsonProperty("ObjectName")]
            public string ObjectName { get; set; }

            [JsonProperty("FactorValueByQuartal")]
            public long FactorValueByQuartal { get; set; }

            [JsonProperty("ObjectNameByQuartal")]
            public string ObjectNameByQuartal { get; set; }

            [JsonProperty("CalculationDate")]
            public DateTime CalculationDate { get; set; }
        }

        #endregion
    }
}
