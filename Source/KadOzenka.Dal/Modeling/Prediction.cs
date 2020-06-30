using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
    public class Prediction : AModelingTemplate
    {
        private PredictionRequest RequestForService { get; set; }
        protected GeneralModelingInputParameters InputParameters { get; set; }
        protected OMModelingModel Model { get; }
        protected override string SubjectForMessageInNotification => $"Процесс прогнозирования модели '{Model.Name}'";

        public Prediction(string inputParametersXml, OMQueue processQueue)
            : base(processQueue)
        {
            InputParameters = inputParametersXml.DeserializeFromXml<GeneralModelingInputParameters>();
            Model = GetModel(InputParameters.ModelId);
        }


        protected override string GetUrl()
        {
            return $"{ModelingProcessConfig.Current.PredictionUrl}/{Model.InternalName}";
        }

        protected override void PrepareData()
        {
            if(InputParameters.ModelType == ModelType.Linear && string.IsNullOrWhiteSpace(Model.LinearTrainingResult))
                throw new Exception(GetErrorMessage(ModelType.Linear));

            if (InputParameters.ModelType == ModelType.Exponential && string.IsNullOrWhiteSpace(Model.ExponentialTrainingResult))
                throw new Exception(GetErrorMessage(ModelType.Exponential));

            if (InputParameters.ModelType == ModelType.Multiplicative && string.IsNullOrWhiteSpace(Model.MultiplicativeTrainingResult))
                throw new Exception(GetErrorMessage(ModelType.Multiplicative));
        }

        protected override object GetRequestForService()
        {
            RequestForService = new PredictionRequest();

            var allAttributes = ModelingService.GetModelAttributes(InputParameters.ModelId);

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

        protected override void ProcessServiceResponse(GeneralResponse generalResponse)
        {
            var predictionResult = JsonConvert.DeserializeObject<PredictionResponse>(generalResponse.Data.ToString());

            if (predictionResult.Prices == null || predictionResult.Prices.Count != RequestForService.CadastralNumbers.Count)
                throw new Exception("Сервис для моделирования вернул цены не для всех объектов");

            var trainingResultStr = GetTrainingResultStr();
            var trainingResult = JsonConvert.DeserializeObject<TrainingResponse>(trainingResultStr);

            SavePredictedPrice(predictionResult.Prices);
            AddLog("Сохранены спрогнозированные цены");

            SaveCoefficientsForPredictedPrice(trainingResult.CoefficientsForAttributes);
            AddLog("Сохранены коэффициенты");

            SaveResultToCalculationSubSystem(trainingResult.CoefficientsForAttributes);
            AddLog("Сохранены данные в КО-часть");
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

        private void SaveCoefficientsForPredictedPrice(Dictionary<string, decimal> coefficients)
        {
            var modelAttributeRelations = OMModelAttributesRelation.Where(x => x.ModelId == Model.Id).SelectAll().Execute();

            foreach (var coefficient in coefficients)
            {
                var modelAttributeRelation = modelAttributeRelations.FirstOrDefault(x => x.AttributeId == coefficient.Key.ParseToLong());
                if (modelAttributeRelation == null)
                    continue;

                modelAttributeRelation.Coefficient = coefficient.Value;
                modelAttributeRelation.Save();
            }
        }

        private string GetTrainingResultStr()
        {
            var trainingResult = string.Empty;
            switch (InputParameters.ModelType)
            {
                case ModelType.Linear:
                    trainingResult = Model.LinearTrainingResult;
                    break;
                case ModelType.Exponential:
                    trainingResult = Model.ExponentialTrainingResult;
                    break;
                case ModelType.Multiplicative:
                    trainingResult = Model.MultiplicativeTrainingResult;
                    break;
            }
            if (string.IsNullOrWhiteSpace(trainingResult))
                throw new Exception($"Не найдено результатов обучения модели типа: '{InputParameters.ModelType.GetEnumDescription()}'");

            return trainingResult;
        }

        private string GetErrorMessage(ModelType type)
        {
            return $"{type.GetEnumDescription()} модель '{Model.Name}' не была обучена. Расчет невозможен.";
        }


        #region Integration With KO SubSystem

        private void SaveResultToCalculationSubSystem(Dictionary<string, decimal> coefficientsForAttributes)
        {
            var koModelAlgorithmType = GetModelAlgorithmType();

            var koModel = CreateModel(koModelAlgorithmType);

            CreateFactors(koModel.Id, coefficientsForAttributes);

            koModel.Formula = koModel.GetFormulaFull(true);
            koModel.Save();
        }

        private KoAlgoritmType GetModelAlgorithmType()
        {
            switch (InputParameters.ModelType)
            {
                case ModelType.Linear:
                    return KoAlgoritmType.Line;
                case ModelType.Exponential:
                    return KoAlgoritmType.Exp;
                case ModelType.Multiplicative:
                    return KoAlgoritmType.Multi;
            }

            throw new Exception($"Неизвестный тип модели {InputParameters.ModelType}");
        }

        private OMModel CreateModel(KoAlgoritmType algorithmType)
        {
            var exitedModel = OMModel.Where(x => x.GroupId == Model.GroupId).SelectAll().ExecuteFirstOrDefault();
            if (exitedModel == null)
            {
                exitedModel = new OMModel
                {
                    AlgoritmType_Code = algorithmType,
                    Formula = string.Empty,
                    Description = Model.Name,
                    Name = Model.Name,
                    GroupId = Model.GroupId
                };
            }
            else
            {
                exitedModel.AlgoritmType_Code = algorithmType;
                exitedModel.Formula = string.Empty;
                exitedModel.Name = Model.Name;
            }

            return exitedModel;
        }

        public void CreateFactors(long koModelId, Dictionary<string, decimal> coefficientsForAttributes)
        {
            var modelObjects = ModelingService.GetIncludedModelObjects(Model.Id, false);

            var existedFactors = OMModelFactor.Where(x => x.ModelId == koModelId).SelectAll().Execute();
            existedFactors.ForEach(x => x.Destroy());

            foreach (var entry in coefficientsForAttributes)
            {
                var factorId = entry.Key.ParseToLong();

                new OMModelFactor
                {
                    ModelId = koModelId,
                    FactorId = entry.Key.ParseToLong(),
                    MarkerId = -1,
                    Weight = entry.Value,
                    B0 = 0 //TODO only for multiplicative
                }.Save();

                CreateMarkCatalog(factorId, modelObjects);
            }
        }

        public void CreateMarkCatalog(long factorId, List<OMModelToMarketObjects> modelObjects)
        {
            var existedMarks = OMMarkCatalog.Where(x => x.GroupId == Model.GroupId && x.FactorId == factorId).SelectAll().Execute();
            existedMarks.ForEach(x => x.Destroy());

            modelObjects.ForEach(modelObject =>
            {
                var objectCoefficients = modelObject.Coefficients.DeserializeFromXml<List<CoefficientForObject>>();
                var objectCoefficient = objectCoefficients.FirstOrDefault(x => x.AttributeId == factorId);
                if (objectCoefficient == null || !string.IsNullOrWhiteSpace(objectCoefficient.Message) ||
                    objectCoefficient.Value == null)
                    return;

                var value = objectCoefficient.Value;
                var metka = objectCoefficient.Coefficient;

                new OMMarkCatalog
                {
                    GroupId = Model.GroupId,
                    FactorId = factorId,
                    ValueFactor = value,
                    MetkaFactor = metka
                }.Save();
            });
        }

        #endregion

        #endregion
    }
}
