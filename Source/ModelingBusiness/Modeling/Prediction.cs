using System;
using System.Collections.Generic;
using System.Linq;
using CommonSdks.ConfigurationManagers;
using Core.Shared.Extensions;
using ModelingBusiness.Modeling.InputParameters;
using ModelingBusiness.Modeling.Requests;
using ModelingBusiness.Modeling.Responses;
using ModelingBusiness.Objects.Entities;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Modeling;
using Serilog;

namespace ModelingBusiness.Modeling
{
    public class Prediction : AModelingTemplate
    {
        private PredictionRequest RequestForService { get; set; }
        protected GeneralModelingInputParameters InputParameters { get; set; }
        protected OMModel GeneralModel { get; }
        protected override string SubjectForMessageInNotification => $"Процесс прогнозирования модели '{GeneralModel.Name}'";

        public Prediction(string inputParametersXml, OMQueue processQueue)
            : base(processQueue, Log.ForContext<Prediction>())
        {
            InputParameters = DeserializeInputParameters(inputParametersXml);
            GeneralModel = ModelService.GetModelEntityById(InputParameters.ModelId);
        }


        protected override string GetUrl()
        {
            var baseUrl = ConfigurationManager.KoConfig.ModelingProcessConfig.PredictionBaseUrl;
            switch (InputParameters.ModelType)
            {
                case KoAlgoritmType.Line:
                    return $"{baseUrl}/{ConfigurationManager.KoConfig.ModelingProcessConfig.PredictionLinearTypeUrl}/{GeneralModel.InternalName}";
                case KoAlgoritmType.Exp:
                    return $"{baseUrl}/{ConfigurationManager.KoConfig.ModelingProcessConfig.PredictionExponentialTypeUrl}/{GeneralModel.InternalName}";
                case KoAlgoritmType.Multi:
                    return $"{baseUrl}/{ConfigurationManager.KoConfig.ModelingProcessConfig.PredictionMultiplicativeTypeUrl}/{GeneralModel.InternalName}";
                default:
                    throw new Exception($"Не известный тип модели: {InputParameters.ModelType.GetEnumDescription()}");
            }
        }

        protected override void PrepareData()
        {
            AddLog($"Начата работа с моделью '{GeneralModel.Name}', тип модели: '{InputParameters.ModelType.GetEnumDescription()}'.");

            if (InputParameters.ModelType == KoAlgoritmType.Line && !GeneralModel.HasLinearTrainingResult)
	            throw new Exception(GetErrorMessage(KoAlgoritmType.Line));

            if (InputParameters.ModelType == KoAlgoritmType.Exp && !GeneralModel.HasExponentialTrainingResult)
	            throw new Exception(GetErrorMessage(KoAlgoritmType.Exp));

            if (InputParameters.ModelType == KoAlgoritmType.Multi && !GeneralModel.HasMultiplicativeTrainingResult)
	            throw new Exception(GetErrorMessage(KoAlgoritmType.Multi));
        }

        protected override object GetRequestForService()
        {
            RequestForService = new PredictionRequest();

            var allAttributes = ModelFactorsService.GetFactors(InputParameters.ModelId).Where(x => x.IsActive).ToList();
            var modelObjects = ModelObjectsRepository.GetIncludedModelObjects(InputParameters.ModelId, IncludedObjectsMode.Prediction);
            modelObjects.ForEach(modelObject =>
            {
                var modelObjectCoefficients = modelObject.DeserializedCoefficients;
                if (modelObjectCoefficients == null || modelObjectCoefficients.Count == 0)
                    return;

                var coefficients = new List<decimal?>();
                allAttributes.ForEach(modelAttribute =>
                {
	                var currentAttribute = modelObjectCoefficients.FirstOrDefault(x =>
		                x.AttributeId == modelAttribute.AttributeId && !string.IsNullOrWhiteSpace(x.Value));
                    coefficients.Add(currentAttribute?.Coefficient);
                });

                //TODO эта проверка будет в сервисе
                if (coefficients.All(x => x != null))
                {
                    RequestForService.Coefficients.Add(coefficients);
                    RequestForService.CadastralNumbers.Add(modelObject.MarketObjectInfo);
                    RequestForService.OmModelToMarketObjectsIds.Add(modelObject.Id);
                }
            });

            if (RequestForService.Coefficients.Count == 0)
                throw new Exception("Не было найдено объектов, подходящих для моделирования. Для расчета цены подходят объекты, которые не исключены из расчета и не находятся ни в обучающей, ни в контрольной выборках.");

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

        private string GetErrorMessage(KoAlgoritmType type)
        {
            return $"{type.GetEnumDescription()} модель '{GeneralModel.Name}' не была обучена. Расчет невозможен.";
        }

        #endregion
    }
}
