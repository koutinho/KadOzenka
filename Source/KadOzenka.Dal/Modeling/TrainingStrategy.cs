using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        //TODO ConfigurationManager.AppSettings["trainModelLink"];
        public override string Url => $"http://82.148.28.237:5000/api/teach/{Model.InternalName}";
        private TrainingRequest RequestForService { get; set; }
        protected TrainingInputParameters InputParameters { get; set; }
        protected OMModelingModel Model { get; }

        public TrainingStrategy(string inputParametersXml)
        {
            InputParameters = inputParametersXml.DeserializeFromXml<TrainingInputParameters>();
            Model = GetModel(InputParameters.ModelId);
        }

        public override void PrepareData()
        {
            ModelingService.CreateObjectsForModel(InputParameters.ModelId);
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
            //it will be list
            var trainingResult = JsonConvert.DeserializeObject<TrainingResult>(responseContentStr);
            PreprocessTrainingResult(trainingResult);

            ResetPredictedPrice();
            ResetCoefficientsForPredictedPrice();

            Model.LinearTrainingResult = JsonConvert.SerializeObject(trainingResult);
            //Model.ExponentialTrainingResult = responseContentStr;
            //Model.MultiplicativeTrainingResult = responseContentStr;
            Model.WasTrained = true;
            Model.Save();
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
                var entry = oldCoefficients.ElementAt(i);
                var attributeId = RequestForService.AttributeIds.ElementAt(i);

                newCoefficients.Add(attributeId.ToString(), entry.Value);
            }

            result.CoefficientsForAttributes = newCoefficients;
        }

        private string PreProcessAttributeName(string name)
        {
            var pattern = new Regex("[() ]");
            return pattern.Replace(name, string.Empty);
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

        #endregion
    }
}
