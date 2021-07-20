using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CommonSdks;
using CommonSdks.ConfigurationManagers;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Interfaces;
using ModelingBusiness.Modeling.InputParameters;
using ModelingBusiness.Modeling.Requests;
using ModelingBusiness.Modeling.Responses;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.Register;
using Serilog;

namespace ModelingBusiness.Modeling
{
    public class Correlation : AModelingTemplate
    {
        private string ColumnNameFroPrice => "PriceForService";
        protected CorrelationInputParameters InputParameters { get; set; }
        private List<long> ObjectIds { get; set; }
        private List<OMAttribute> Attributes { get; set; }
        private string ResultMessage { get; set; }
        protected override string SubjectForMessageInNotification => "Процесс корреляции";
        protected IMarketObjectsForModelingService MarketObjectsForModelingService { get;}

        public Correlation(string inputParametersXml, OMQueue processQueue)
            : base(processQueue, Log.ForContext<Correlation>())
        {
            InputParameters = inputParametersXml.DeserializeFromXml<CorrelationInputParameters>();
            MarketObjectsForModelingService = new MarketObjectsForModelingService();
        }


        protected override string GetUrl()
        {
	        return ConfigurationManager.KoConfig.ModelingProcessConfig.CorrelationUrl;
        }

        protected override void PrepareData()
        {
            ObjectIds = GetObjectIds(InputParameters.QsQueryStr);
            Attributes = GetAttributes(InputParameters.AttributeIds);

            AddLog($"Найдено: {ObjectIds.Count} объектов и {Attributes.Count} атрибутов.");
        }

        protected override object GetRequestForService()
        {
	        var correlationDto = MarketObjectsForModelingService.GetObjectsForCorrelation(ObjectIds, Attributes);
	        if (correlationDto.Coefficients.Count == 0)
		        throw new Exception("Не было найдено объектов, подходящих для моделирования (у которых значения всех атрибутов не пустые)");

            var request = new CorrelationRequest
	        {
                AttributeNames = correlationDto.AttributeNames,
                Coefficients = correlationDto.Coefficients
	        };

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

            new NotificationSender().SendNotification(processQueue, SubjectForMessageInNotification, message.ToString());
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
