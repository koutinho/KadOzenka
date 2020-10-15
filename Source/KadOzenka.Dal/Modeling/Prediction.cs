using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;
using ObjectModel.Modeling;
using Serilog;

namespace KadOzenka.Dal.Modeling
{
    public class Prediction : AModelingTemplate
    {
        private PredictionRequest RequestForService { get; set; }
        protected GeneralModelingInputParameters InputParameters { get; set; }
        protected OMModel Model { get; }
        protected override string SubjectForMessageInNotification => $"Процесс прогнозирования модели '{Model.Name}'";

        public Prediction(string inputParametersXml, OMQueue processQueue, ILogger logger)
            : base(processQueue, logger)
        {
            InputParameters = inputParametersXml.DeserializeFromXml<GeneralModelingInputParameters>();
            Model = ModelingService.GetModelEntityById(InputParameters.ModelId);
        }


        protected override string GetUrl()
        {
            var baseUrl = ModelingProcessConfig.Current.PredictionBaseUrl;
            switch (InputParameters.ModelType)
            {
                case ModelType.Linear:
                    return $"{baseUrl}/{ModelingProcessConfig.Current.PredictionLinearTypeUrl}/{Model.InternalName}";
                case ModelType.Exponential:
                    return $"{baseUrl}/{ModelingProcessConfig.Current.PredictionExponentialTypeUrl}/{Model.InternalName}";
                case ModelType.Multiplicative:
                    return $"{baseUrl}/{ModelingProcessConfig.Current.PredictionMultiplicativeTypeUrl}/{Model.InternalName}";
                default:
                    throw new Exception($"Не известный тип модели: {InputParameters.ModelType.GetEnumDescription()}");
            }
        }

        protected override void PrepareData()
        {
            AddLog($"Начата работа с моделью '{Model.Name}', тип модели: '{InputParameters.ModelType.GetEnumDescription()}'.");

            if (InputParameters.ModelType == ModelType.Linear && string.IsNullOrWhiteSpace(Model.LinearTrainingResult))
                throw new Exception(GetErrorMessage(ModelType.Linear));

            if (InputParameters.ModelType == ModelType.Exponential && string.IsNullOrWhiteSpace(Model.ExponentialTrainingResult))
                throw new Exception(GetErrorMessage(ModelType.Exponential));

            if (InputParameters.ModelType == ModelType.Multiplicative && string.IsNullOrWhiteSpace(Model.MultiplicativeTrainingResult))
                throw new Exception(GetErrorMessage(ModelType.Multiplicative));
        }

        protected override object GetRequestForService()
        {
            RequestForService = new PredictionRequest();

            var allAttributes = ModelingService.GetModelFactors(InputParameters.ModelId);

            var modelObjects = ModelingService.GetIncludedModelObjects(InputParameters.ModelId, false);
            modelObjects.ForEach(modelObject =>
            {
                var modelObjectAttributes = modelObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
                if (modelObjectAttributes == null || modelObjectAttributes.Count == 0)
                    return;

                var coefficients = new List<decimal?>();
                allAttributes.ForEach(modelAttribute =>
                {
	                var currentAttribute = modelObjectAttributes.FirstOrDefault(x =>
		                x.AttributeId == modelAttribute.AttributeId && !string.IsNullOrWhiteSpace(x.Value));
                    coefficients.Add(currentAttribute?.Coefficient);
                });

                //TODO эта проверка будет в сервисе
                if (coefficients.All(x => x != null))
                {
                    RequestForService.Coefficients.Add(coefficients);
                    RequestForService.CadastralNumbers.Add(modelObject.CadastralNumber);
                    RequestForService.OmModelToMarketObjectsIds.Add(modelObject.Id);
                }
            });

            if (RequestForService.Coefficients.Count == 0)
                throw new Exception("Не было найдено объектов, подходящих для моделирования (у которых значения всех атрибутов не пустые)");

            return RequestForService;
        }

        protected override void ProcessServiceResponse(GeneralResponse generalResponse)
        {
            var predictionResult = JsonConvert.DeserializeObject<PredictionResponse>(generalResponse.Data.ToString());

            if (predictionResult.Prices == null || predictionResult.Prices.Count != RequestForService.CadastralNumbers.Count)
                throw new Exception("Сервис для моделирования вернул цены не для всех объектов");

            SavePredictedPrice(predictionResult.Prices);
            AddLog("Сохранены спрогнозированные цены");
        }

        #region Support Methods

        private void SavePredictedPrice(List<decimal> prices)
        {
            var modelObjects = OMModelToMarketObjects.Where(x => RequestForService.OmModelToMarketObjectsIds.Contains(x.Id))
                .SelectAll().Execute();

            for (var i = 0; i < RequestForService.OmModelToMarketObjectsIds.Count; i++)
            {
                var modelObject = modelObjects.FirstOrDefault(x => x.Id == RequestForService.OmModelToMarketObjectsIds[i]);
                if (modelObject == null)
                    continue;

                modelObject.PriceFromModel = prices[i];
                modelObject.Save();
            }
        }

        private string GetErrorMessage(ModelType type)
        {
            return $"{type.GetEnumDescription()} модель '{Model.Name}' не была обучена. Расчет невозможен.";
        }

        #endregion
    }
}
