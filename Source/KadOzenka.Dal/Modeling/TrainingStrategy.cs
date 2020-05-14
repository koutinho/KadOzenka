using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
    public class TrainingStrategy : AModelingStrategy
    {
        //TODO ConfigurationManager.AppSettings["trainModelLink"];
        public override string Url => $"http://82.148.28.237:5000/api/teach/{Model.InternalName}";
        private TrainingRequest RequestForService { get; set; }

        public TrainingStrategy(ModelingInputParameters request, OMModelingModel model)
            : base(request, model)
        {
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

        public override void SaveResult(string responseContentStr)
        {
            //it will be list
            var trainingResult = JsonConvert.DeserializeObject<TrainingResult>(responseContentStr);

            ResetPredictedPrice();
            ResetCoefficientsForPredictedPrice();

            Model.LinearTrainingResult = responseContentStr;
            //Model.ExponentialTrainingResult = responseContentStr;
            //Model.MultiplicativeTrainingResult = responseContentStr;
            Model.WasTrained = true;
            Model.Save();
        }


        #region Support Methods

        private string PreProcessAttributeName(string name)
        {
            var pattern = new Regex("[() ]");
            return pattern.Replace(name, string.Empty);
        }

        private void ResetPredictedPrice()
        {
            var modelObjects = OMModelToMarketObjects.Where(x => x.ModelId == Model.Id).SelectAll().Execute();

            modelObjects.ForEach(x =>
            {
                x.PriceFromModel = null;
                x.Save();
            });
        }

        private void ResetCoefficientsForPredictedPrice()
        {
            var modelAttributeRelations = OMModelAttributesRelation.Where(x => x.ModelId == Model.Id).SelectAll().Execute();

            modelAttributeRelations.ForEach(x =>
            {
                x.Coefficient = null;
                x.Save();
            });
        }

        #endregion
    }
}
