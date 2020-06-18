using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
    public class TrainingStrategy : AModelingStrategy
    {
        private TrainingRequest RequestForService { get; set; }
        protected GeneralModelingInputParameters InputParameters { get; set; }
        protected OMModelingModel Model { get; }

        public TrainingStrategy(string inputParametersXml)
        {
            InputParameters = inputParametersXml.DeserializeFromXml<GeneralModelingInputParameters>();
            Model = GetModel(InputParameters.ModelId);
        }


        //TODO вынести в конфиг
        public override string GetUrl()
        {
            //ConfigurationManager.AppSettings["trainModelLink"];
            switch (InputParameters.ModelType)
            {
                case ModelType.Linear:
                    return $"http://82.148.28.237:5000/api/teach/lin/{Model.InternalName}";
                case ModelType.Exponential:
                    return $"http://82.148.28.237:5000/api/teach/exp/{Model.InternalName}";
                case ModelType.Multiplicative:
                    return $"http://82.148.28.237:5000/api/teach/mult/{Model.InternalName}";
                default:
                    throw new Exception($"Не известный тип модели: {InputParameters.ModelType.GetEnumDescription()}");
            }
        }

        public override void PrepareData(OMQueue processQueue)
        {
            ModelingService.CreateObjectsForModel(InputParameters.ModelId, processQueue);
        }

        public override object GetRequestForService()
        {
            RequestForService = new TrainingRequest();

            var allAttributes = ModelingService.GetModelAttributes(InputParameters.ModelId);
            RequestForService.AttributeNames.AddRange(allAttributes.Select(x => PreProcessAttributeName(x.AttributeName)));
            RequestForService.AttributeIds.AddRange(allAttributes.Select(x => x.AttributeId));

            var modelObjects = ModelingService.GetIncludedModelObjects(InputParameters.ModelId, true);
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
                    RequestForService.Prices.Add(new List<decimal>{modelObject.Price});
                    RequestForService.CadastralNumbers.Add(modelObject.CadastralNumber);
                }
            });

            if (RequestForService.Coefficients.Count == 0)
                throw new Exception("Не было найдено объектов, подходящих для моделирования (у которых значения всех аттрибутов не пустые)");

            return RequestForService;
        }

        public override void ProcessServiceAnswer(string responseContentStr)
        {
            var trainingResult = JsonConvert.DeserializeObject<TrainingResult>(responseContentStr);
            PreprocessTrainingResult(trainingResult);

            ResetPredictedPrice();
            ResetCoefficientsForPredictedPrice();

            UpdateModel(trainingResult);
        }

        public override void RollBackResult()
        {
            Model.WasTrained = false;
            Model.LinearTrainingResult = null;
            Model.ExponentialTrainingResult = null;
            Model.MultiplicativeTrainingResult = null;
            Model.Save();
        }

        public override void SendSuccessNotification(OMQueue processQueue)
        {
            var subject = $"Процесс обучения модели '{Model.Name}'";
            var message = "Операция успешно завершена";
            NotificationSender.SendNotification(processQueue, subject, message);
        }

        public override void SendFailNotification(OMQueue processQueue)
        {
            var subject = $"Процесс обучения модели '{Model.Name}'";
            var message = "Операция завершена с ошибкой. Подробнее в списке процессов";
            NotificationSender.SendNotification(processQueue, subject, message);
        }


        #region Support Methods

        /// <summary>
        /// Заменяем имена аттрибутов на их Id
        /// </summary>
        /// <param name="result"></param>
        private void PreprocessTrainingResult(TrainingResult result)
        {
            var newCoefficients = new Dictionary<string, decimal>();
            var oldCoefficients = result.CoefficientsForAttributes;

            for (var i = 0; i < oldCoefficients.Count; i++)
            {
                var entry = oldCoefficients.ElementAtOrDefault(i);
                var attributeId = RequestForService.AttributeIds.ElementAtOrDefault(i);

                newCoefficients[attributeId.ToString()] = entry.Value;
            }

            result.CoefficientsForAttributes = newCoefficients;
        }

        private void ResetPredictedPrice()
        {
            var modelObjects = OMModelToMarketObjects.Where(x => x.ModelId == Model.Id && x.PriceFromModel != null)
                .SelectAll().Execute();

            modelObjects.ForEach(x =>
            {
                x.PriceFromModel = null;
                x.Save();
            });
        }

        private void ResetCoefficientsForPredictedPrice()
        {
            var modelAttributeRelations = OMModelAttributesRelation.Where(x => x.ModelId == Model.Id && x.Coefficient != null)
                .SelectAll().Execute();

            modelAttributeRelations.ForEach(x =>
            {
                x.Coefficient = null;
                x.Save();
            });
        }

        private void UpdateModel(TrainingResult trainingResult)
        {
            var jsonTrainingResult = JsonConvert.SerializeObject(trainingResult);
            switch (InputParameters.ModelType)
            {
                case ModelType.Linear:
                    Model.LinearTrainingResult = jsonTrainingResult;
                    break;
                case ModelType.Exponential:
                    Model.ExponentialTrainingResult = jsonTrainingResult;
                    break;
                case ModelType.Multiplicative:
                    Model.MultiplicativeTrainingResult = jsonTrainingResult;
                    break;
                default:
                    throw new Exception($"Не известный тип модели: {InputParameters.ModelType.GetEnumDescription()}");
            }

            Model.WasTrained = true;
            Model.Save();
        }

        #endregion
    }
}
