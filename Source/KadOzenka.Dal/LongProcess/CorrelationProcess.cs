using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using Newtonsoft.Json;
using ObjectModel.Core.Register;
using ObjectModel.Market;

namespace KadOzenka.Dal.LongProcess
{
    public class CorrelationProcess : LongProcess
    {
        public const string LongProcessName = nameof(CorrelationProcess);
        private static HttpClient _httpClient = new HttpClient();
        //TODO ConfigurationManager.AppSettings["correlationModelLink"];
        private string Url => "http://82.148.28.237:5000/api/teach/testCorrelation";
        private string ColumnNameFroPrice => "PriceForService";


        public static void AddProcessToQueue(CorrelationInputParameters inputParameters)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, registerId: OMCoreObject.GetRegisterId(),
                parameters: inputParameters.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);

            CorrelationInputParameters inputParameters = null;
            if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
            {
                inputParameters = processQueue.Parameters.DeserializeFromXml<CorrelationInputParameters>();
            }
            if (inputParameters?.AttributeIds == null || inputParameters.AttributeIds.Count == 0 ||
                string.IsNullOrWhiteSpace(inputParameters.QsQueryStr))
            {
                WorkerCommon.SetMessage(processQueue, "Не переданы входные параметры для процесса: список аттрибутов и запрос для формирования объектов");
                WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);

                NotificationSender.SendNotification(processQueue, "Корреляция",
                    "Операция завершена с ошибкой. Подробнее в списке процессов");

                return;
            }


            try
            {
                var objectIds = GetObjectIds(inputParameters.QsQueryStr);
                var attributes = GetAttributes(inputParameters.AttributeIds);

                var requestForService = GetRequestForService(objectIds, attributes);
                var response = SendDataToService(_httpClient, Url, requestForService).GetAwaiter().GetResult();
                response = PreProcessServiceResponse(response);
                var result = SaveResult(response);

                WorkerCommon.SetProgress(processQueue, 100);

                SendNotification("Операция успешно завершена " + result, processQueue);
            }
            catch (Exception)
            {
                SendNotification("Операция завершена с ошибкой. Подробнее в списке процессов", processQueue);

                throw;
            }
        }


        #region Support Methods

        private List<long> GetObjectIds(string query)
        {
            var qsQuery = query.DeserializeFromXml<QSQuery>();
            var table = qsQuery.ExecuteQuery();

            var objectIds = new List<long>();
            foreach (DataRow row in table.Rows)
            {
                var id = row["ID"].ParseToLong();
                objectIds.Add(id);
            }

            return objectIds;
        }

        private List<OMAttribute> GetAttributes(List<long> attributeIds)
        {
            return OMAttribute.Where(x => attributeIds.Contains(x.Id))
                .Select(x => x.Id)
                .Select(x => x.Name)
                .Select(x => x.RegisterId)
                .Select(x => x.InternalName)
                .Execute();

        }

        public object GetRequestForService(List<long> objectIds, List<OMAttribute> attributes)
        {
            var query = new QSQuery
            {
                MainRegisterID = OMCoreObject.GetRegisterId(),
                Condition = new QSConditionSimple
                {
                    ConditionType = QSConditionType.In,
                    LeftOperand = OMCoreObject.GetColumn(x => x.Id),
                    RightOperand = new QSColumnConstant(objectIds)
                }
            };
            query.AddColumn(OMCoreObject.GetColumn(x => x.Price, ColumnNameFroPrice));
            attributes.ForEach(attribute =>
            {
                query.AddColumn(attribute.Id, attribute.InternalName);
            });

            var request = new CorrelationRequest();
            request.AttributeNames.AddRange(attributes.Select(x => x.Name));
            var table = query.ExecuteQuery();
            //i=1 чтобы пропустить строку с Id
            for (var i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var priceForService = row[ColumnNameFroPrice].ParseToDecimalNullable();
                var currentCoefficients = new List<decimal?>();
                attributes.ForEach(attribute =>
                {
                    var val = row[attribute.InternalName].ParseToDecimalNullable();
                    currentCoefficients.Add(val);
                });

                if (currentCoefficients.All(x => x != null))
                {
                    request.Prices.Add(new List<decimal> { priceForService.GetValueOrDefault() });
                    request.Coefficients.Add(currentCoefficients);
                }
            }

            return request;
        }

        private string SaveResult(string responseContentStr)
        {
            var correlationResult = JsonConvert.DeserializeObject<CorrelationResult>(responseContentStr);

            var sb = new StringBuilder();
            foreach (var coefficientsForAttribute in correlationResult.CoefficientsForAttributes)
            {
                sb.Append(coefficientsForAttribute.Key).Append(": ")
                    .AppendLine(coefficientsForAttribute.Value.ToString());
            }

            return sb.ToString();
        }

        private void SendNotification(string message, OMQueue processQueue)
        {
            var subject = "Корреляция";

            NotificationSender.SendNotification(processQueue, subject, message);
        }

        #endregion


        public class CorrelationRequest
        {
            [JsonProperty("y")]
            public List<List<decimal>> Prices { get; set; }
            [JsonProperty("columns")]
            public List<string> AttributeNames { get; set; }
            [JsonProperty("x")]
            public List<List<decimal?>> Coefficients { get; set; }

            public CorrelationRequest()
            {
                Prices = new List<List<decimal>>();
                AttributeNames = new List<string>();
                Coefficients = new List<List<decimal?>>();
            }
        }

        public class CorrelationResult
        {
            [JsonProperty("coef")]
            public Dictionary<string, decimal> CoefficientsForAttributes { get; set; }
        }
    }
}
