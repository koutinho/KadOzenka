using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
    public class PredictionStrategy : AModelingStrategy
    {
        //TODO ConfigurationManager.AppSettings["calculateModelLink"];
        public override string Url => $"http://82.148.28.237:5000/api/predict/{Model.InternalName}";
        private PredictionRequest RequestForService { get; set; }

        public PredictionStrategy(ModelingInputParameters request, OMModelingModel model)
            : base(request, model)
        {
        }

        public override void PrepareData()
        {
            if (!Model.WasTrained.GetValueOrDefault())
                throw new Exception($"Модель '{Model.Name}' не была обучена. Расчет невозможен.");
        }

        public override object GetRequestForService()
        {
            RequestForService = new PredictionRequest();

            var allAttributes = ModelingService.GetModelAttributes(InputParameters.ModelId);
            RequestForService.OmModelAttributeRelationIds.AddRange(allAttributes.Select(x => x.Id));

            var modelObjects = ModelingService.GetIncludedModelObjects(InputParameters.ModelId, false);
            modelObjects.ForEach(modelObject =>
            {
                var modelObjectAttributes = modelObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
                if (modelObjectAttributes == null || modelObjectAttributes.Count == 0)
                    return;

                var coefficients = new List<decimal?>();
                allAttributes.ForEach(modelAttribute =>
                {
                    coefficients.Add(modelObjectAttributes.FirstOrDefault(x => x.AttributeId == modelAttribute.AttributeId)?.Coefficient);
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
                throw new Exception("Не было найдено объектов, подходящих для моделирования (у которых значения всех аттрибутов не пустые)");

            return RequestForService;
        }

        public override void SaveResult(string responseFromService)
        {
            var predictionResult = JsonConvert.DeserializeObject<PredictionResult>(responseFromService);

            if (predictionResult.Prices == null || predictionResult.Prices.Count != RequestForService.CadastralNumbers.Count)
                throw new Exception("Сервис для моделирования вернул цены не для всех объектов");

            SavePredictedPrice(predictionResult.Prices);

            SaveCoefficientsForPredictedPrice();
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

        private void SaveCoefficientsForPredictedPrice()
        {
            var trainingResultStr = GetTrainingResultStr();
            var trainingResult = JsonConvert.DeserializeObject<TrainingResult>(trainingResultStr);

            var coefficients = trainingResult.CoefficientsForAttributes.Values.ToList();

            var modelAttributeRelations = OMModelAttributesRelation
                .Where(x => RequestForService.OmModelAttributeRelationIds.Contains(x.Id)).SelectAll().Execute();

            for (var i = 0; i < RequestForService.OmModelAttributeRelationIds.Count; i++)
            {
                var modelAttributeRelation = modelAttributeRelations.FirstOrDefault(x => x.Id == RequestForService.OmModelAttributeRelationIds[i]);
                if (modelAttributeRelation == null)
                    continue;

                modelAttributeRelation.Coefficient = coefficients.ElementAtOrDefault(i);
                modelAttributeRelation.Save();
            }
        }

        private string GetTrainingResultStr()
        {
            var trainingResult = string.Empty;
            switch (InputParameters.PredictionType)
            {
                case PredictionType.Linear:
                    trainingResult = Model.LinearTrainingResult;
                    break;
                case PredictionType.Exponential:
                    trainingResult = Model.ExponentialTrainingResult;
                    break;
                case PredictionType.Multiplicative:
                    trainingResult = Model.MultiplicativeTrainingResult;
                    break;
            }
            if (string.IsNullOrWhiteSpace(trainingResult))
                throw new Exception($"Не найдено результатов обучения модели типа: '{InputParameters.PredictionType.GetEnumDescription()}'");

            return trainingResult;
        }

        #endregion
    }
}
