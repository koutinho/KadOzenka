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
using KadOzenka.WebClients.ReonClient.Api;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess
{
    //TODO пока нет апи, поэтому процесс не доработан
    public class KoFactorsFromReon : LongProcess
    {
        private RosreestrDataApi ReonWebClientService { get; set; }

        public static void AddProcessToQueue(long taskId)
        {
            LongProcessManager.AddTaskToQueue(nameof(KoFactorsFromReon), objectId: taskId, registerId: OMTask.GetRegisterId());
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

            ReonWebClientService = new RosreestrDataApi();

            var task = GetTask(processQueue.ObjectId.Value);
            var units = GetUnits(task.Id);

            units.ForEach(unit =>
            {
                var request = GetRequest(task, unit);
                var response = ReonWebClientService.RosreestrDataGetGraphFactorsByCadNum(request.CadastralNumber, request.EstimationDate);
                //ProcessServiceResponse(response);
            });

            NotificationSender.SendNotification(processQueue, "Получение графических факторов из ИС РЕОН", "Операция выполнена успешно.");
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

        public void ProcessServiceResponse(FactorsFromReonResponse input)
        {
            //var factors = JsonConvert.DeserializeObject<List<FactorsFromReonResponse>>(responseContentStr);
            input = new FactorsFromReonResponse
            {
                CadNum = "CadastralNumber",
                DateAppraisal = DateTime.Today,
                GraphicFactors = new List<GraphFactors>
                {
                    new GraphFactors
                    {

                    }
                }
            };

            input.GraphicFactors?.ForEach(task =>
            {
                
            });
        }

        #endregion

        #region Entities

        public class FactorsFromReonRequest
        {
            public string CadastralNumber { get; set; }

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
            public string CadNum { get; set; }

            [JsonProperty("CalculationDate")]
            public DateTime DateAppraisal { get; set; }

            [JsonProperty("GraphicFactors")]
            public List<GraphFactors> GraphicFactors { get; set; }
        }

        public class GraphFactors
        {
            [JsonProperty("CalculationDate")]
            public DateTime DateCalc { get; set; }

            [JsonProperty("SourceCadastralNumber")]
            public string CadBlock { get; set; }

            [JsonProperty("SourceLayerName")]
            public string LayerSourceName { get; set; }

            [JsonProperty("CalculationFactorLayerName")]
            public string LayerTargetName { get; set; }

            [JsonProperty("CalculationType")]
            public string CalcType { get; set; }

            [JsonProperty("FactorName")]
            public string FactorName { get; set; }

            [JsonProperty("FactorValue")]
            public double FactorValue { get; set; }

            [JsonProperty("ObjectName")]
            public string ObjectName { get; set; }

            [JsonProperty("FactorValueByQuartal")]
            public long FactorValueByCadBlock { get; set; }

            [JsonProperty("ObjectNameByQuartal")]
            public string ObjectNameByCadBlock { get; set; }
        }

        #endregion
    }
}
