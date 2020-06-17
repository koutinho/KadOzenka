using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.Register;
using ObjectModel.Market;

namespace KadOzenka.Dal.Modeling
{
    public class CorrelationStrategy : AModelingStrategy
    {
        private string ColumnNameFroPrice => "PriceForService";
        protected CorrelationInputParameters InputParameters { get; set; }
        private List<long> ObjectIds { get; set; }
        private List<OMAttribute> Attributes { get; set; }
        private string ResultMessage { get; set; }

        public CorrelationStrategy(string inputParametersXml)
        {
            InputParameters = inputParametersXml.DeserializeFromXml<CorrelationInputParameters>();
        }


        //TODO вынести в конфиг
        public override string GetUrl()
        {
            return "http://82.148.28.237:5000/api/teach/testCorrelation";
        }

        public override void PrepareData()
        {
            ObjectIds = GetObjectIds(InputParameters.QsQueryStr);
            Attributes = GetAttributes(InputParameters.AttributeIds);
        }

        public override object GetRequestForService()
        {
            var query = new QSQuery
            {
                MainRegisterID = OMCoreObject.GetRegisterId(),
                Condition = new QSConditionSimple
                {
                    ConditionType = QSConditionType.In,
                    LeftOperand = OMCoreObject.GetColumn(x => x.Id),
                    RightOperand = new QSColumnConstant(ObjectIds)
                }
            };
            query.AddColumn(OMCoreObject.GetColumn(x => x.Price, ColumnNameFroPrice));
            Attributes.ForEach(attribute =>
            {
                query.AddColumn(attribute.Id, attribute.InternalName);
            });

            var request = new CorrelationRequest();
            request.AttributeNames.AddRange(Attributes.Select(x => x.Name));
            var table = query.ExecuteQuery();
            //i=1 чтобы пропустить строку с Id
            for (var i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var priceForService = row[ColumnNameFroPrice].ParseToDecimalNullable();
                var currentCoefficients = new List<decimal?>();
                Attributes.ForEach(attribute =>
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

        public override void ProcessServiceAnswer(string responseContentStr)
        {
            var correlationResult = JsonConvert.DeserializeObject<CorrelationResult>(responseContentStr);

            var sb = new StringBuilder();
            foreach (var coefficientsForAttribute in correlationResult.CoefficientsForAttributes)
            {
                sb.Append(coefficientsForAttribute.Key).Append(": ")
                    .AppendLine(coefficientsForAttribute.Value.ToString());
            }

            ResultMessage =  sb.ToString();
        }

        public override void RollBackResult()
        {
        }

        public override void SendSuccessNotification(OMQueue processQueue)
        {
            var subject = "Процесс корреляции";
            var message = new StringBuilder()
                .AppendLine("Операция успешно завершена")
                .AppendLine()
                .AppendLine("Коэффициенты:")
                .AppendLine(ResultMessage);

            NotificationSender.SendNotification(processQueue, subject, message.ToString());
        }

        public override void SendFailNotification(OMQueue processQueue)
        {
            var subject = "Процесс корреляции";
            var message = "Операция завершена с ошибкой. Подробнее в списке процессов";
            NotificationSender.SendNotification(processQueue, subject, message);
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

        #endregion
    }
}
