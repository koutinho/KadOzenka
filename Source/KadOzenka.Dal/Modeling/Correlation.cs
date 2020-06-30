using System;
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
    public class Correlation : AModelingTemplate
    {
        private string ColumnNameFroPrice => "PriceForService";
        protected CorrelationInputParameters InputParameters { get; set; }
        private List<long> ObjectIds { get; set; }
        private List<OMAttribute> Attributes { get; set; }
        private string ResultMessage { get; set; }
        protected override string SubjectForMessageInNotification => "Процесс корреляции";

        public Correlation(string inputParametersXml, OMQueue processQueue)
            : base(processQueue)
        {
            InputParameters = inputParametersXml.DeserializeFromXml<CorrelationInputParameters>();
        }


        protected override string GetUrl()
        {
            return ModelingProcessConfig.Current.CorrelationUrl;
        }

        protected override void PrepareData()
        {
            ObjectIds = GetObjectIds(InputParameters.QsQueryStr);
            Attributes = GetAttributes(InputParameters.AttributeIds);

            AddLog($"Найдено: {ObjectIds.Count} объектов и {Attributes.Count} атрибутов.");
        }

        protected override object GetRequestForService()
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
                query.AddColumn(attribute.Id, attribute.Id.ToString());
            });

            var request = new CorrelationRequest();
            request.AttributeNames.AddRange(Attributes.Select(x => x.Name));
            request.AttributeNames.Add("Цена");
            var table = query.ExecuteQuery();
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var priceForService = row[ColumnNameFroPrice].ParseToDecimalNullable();
                var coefficients = new List<decimal?>();
                Attributes.ForEach(attribute =>
                {
                    var val = row[attribute.Id.ToString()].ParseToDecimalNullable();
                    coefficients.Add(val);
                });

                if (coefficients.All(x => x != null))
                {
                    coefficients.Add(priceForService.GetValueOrDefault());
                    request.Coefficients.Add(coefficients);
                }
            }

            if (request.Coefficients.Count == 0)
                throw new Exception("Не было найдено объектов, подходящих для моделирования (у которых значения всех аттрибутов не пустые)");

            return request;
        }

        protected override void ProcessServiceResponse(GeneralResponse generalResponse)
        {
            var correlationResult = JsonConvert.DeserializeObject<CorrelationResponse>(generalResponse.Data.ToString());

            if (correlationResult.CoefficientsForAttributes == null ||
                correlationResult.CoefficientsForAttributes.Count == 0)
            {
                ResultMessage = "Сервис для моделирования не вернул данные. Обратитесь к администратору.";
                return;
            }

            var sb = new StringBuilder();
            foreach (var coefficientsForAttribute in correlationResult.CoefficientsForAttributes)
            {
                sb.Append(coefficientsForAttribute.FirstColumn)
                    .Append(" - ")
                    .Append(coefficientsForAttribute.SecondColumn)
                    .Append(": ")
                    .AppendLine(coefficientsForAttribute.Coefficient.ToString());
            }

            ResultMessage = sb.ToString();
        }

        protected override void SendSuccessNotification(OMQueue processQueue)
        {
            var message = new StringBuilder()
                .AppendLine("Операция успешно завершена")
                .AppendLine()
                .AppendLine("Коэффициенты:")
                .AppendLine(ResultMessage);

            NotificationSender.SendNotification(processQueue, SubjectForMessageInNotification, message.ToString());
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
                .Execute();
        }

        #endregion
    }
}
