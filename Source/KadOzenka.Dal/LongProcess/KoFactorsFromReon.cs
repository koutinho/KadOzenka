using System;
using System.Collections.Generic;
using ObjectModel.Core.LongProcess;
using System.Threading;
using System.Transactions;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.SRD;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Registers;
using KadOzenka.WebClients.ReonClient.Api;
using Newtonsoft.Json;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.KO;
using Platform.Configurator;

namespace KadOzenka.Dal.LongProcess
{
    //TODO пока нет апи, поэтому процесс не доработан
    public class KoFactorsFromReon : LongProcess
    {
        private const long REOIN_SOURCE_REGISTER_ID = 44355304;
        private RosreestrDataApi ReonWebClientService { get; set; }
        private RegisterAttributeService RegisterAttributeService { get; set; }

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
            RegisterAttributeService = new RegisterAttributeService();

            var task = GetTask(processQueue.ObjectId.Value);
            var units = GetUnits(task.Id);

            units.ForEach(unit =>
            {
                if (!unit.ObjectId.HasValue)
                    return;

                var request = GetRequest(task, unit);
                //var response = ReonWebClientService.RosreestrDataGetGraphFactorsByCadNum(request.CadastralNumber, request.EstimationDate);
                var response = new FactorsFromReonResponse
                {
                    CadNum = "CadastralNumber",
                    DateAppraisal = DateTime.Today,
                    GraphicFactors = new List<GraphFactors>
                    {
                        new GraphFactors
                        {
                            FactorName = "Test string",
                            FactorValue = "value"
                        },
                        new GraphFactors
                        {
                            FactorName = "Test decimal",
                            FactorValue = 1.5m
                        },
                        new GraphFactors
                        {
                            FactorName = "Test date",
                            FactorValue = DateTime.Today
                        }
                    }
                };
                ProcessServiceResponse(unit.ObjectId.Value, response);
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
                .Select(x => x.ObjectId)
                .Execute();
        }

        private FactorsFromReonRequest GetRequest(OMTask task, OMUnit unit)
        {
            return new FactorsFromReonRequest(unit.CadastralNumber, task.EstimationDate);
        }

        public void ProcessServiceResponse(long objectId, FactorsFromReonResponse response)
        {
            response.GraphicFactors?.ForEach(factor =>
            {
                var attributeType = GetFactorType(factor.FactorValue);

                var attribute = GetAttribute(factor.FactorName);
                if (attribute == null)
                    attribute = CreateAttribute(factor.FactorName, attributeType);

                SaveFactor(objectId, attribute.Id, attributeType, factor);
            });
        }

        private RegisterAttributeType GetFactorType(object factorValue)
        {
            if (factorValue == null)
                return RegisterAttributeType.STRING;

            if (decimal.TryParse(factorValue.ToString(), out var number))
                return RegisterAttributeType.DECIMAL;

            if (DateTime.TryParse(factorValue.ToString(), out var date))
                return RegisterAttributeType.DATE;

            return RegisterAttributeType.STRING;
        }

        private OMAttribute GetAttribute(string attributeName)
        {
            return OMAttribute.Where(x => x.RegisterId == REOIN_SOURCE_REGISTER_ID && x.Name == attributeName)
                .Select(x => x.Id)
                .ExecuteFirstOrDefault();
        }

        private OMAttribute CreateAttribute(string attributeName, RegisterAttributeType type)
        {
            OMAttribute omAttribute;
            using (var ts = new TransactionScope())
            {
                omAttribute = RegisterAttributeService.CreateRegisterAttribute(attributeName,
                    REOIN_SOURCE_REGISTER_ID, type, false);

                var dbConfigurator = RegisterConfigurator.GetDbConfigurator();
                RegisterConfigurator.CreateDbColumnForRegister(omAttribute, dbConfigurator);

                ts.Complete();
            }

            RegisterCache.UpdateCache(0, null);

            return omAttribute;
        }

        private void SaveFactor(long objectId, long attributeId, RegisterAttributeType attributeType,
            GraphFactors factor)
        {
            var gbuObjectAttribute = new GbuObjectAttribute
            {
                ObjectId = objectId,
                AttributeId = attributeId,
                S = DateTime.Today,
                Ot = DateTime.Today,
                ChangeDocId = -1,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now
            };

            if (factor.FactorValue != null)
            {
                switch (attributeType)
                {
                    case RegisterAttributeType.STRING:
                        gbuObjectAttribute.StringValue = factor.FactorValue.ToString();
                        break;
                    case RegisterAttributeType.DECIMAL:
                        gbuObjectAttribute.NumValue = decimal.Parse(factor.FactorValue.ToString());
                        break;
                    case RegisterAttributeType.DATE:
                        gbuObjectAttribute.DtValue = DateTime.Parse(factor.FactorValue.ToString());
                        break;
                }
            }

            gbuObjectAttribute.Save();
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
            public object FactorValue { get; set; }

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
