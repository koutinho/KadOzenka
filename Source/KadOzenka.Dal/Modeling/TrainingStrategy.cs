using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Market;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
    public class TrainingStrategy : AModelingStrategy
    {
        private TrainingRequest RequestForService { get; set; }
        protected GeneralModelingInputParameters InputParameters { get; set; }
        protected OMModelingModel Model { get; }

        public TrainingStrategy(string inputParametersXml, OMQueue processQueue)
            : base(processQueue)
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

        public override void PrepareData()
        {
            AddLog("Начат сбор данных\n");

            var groupToMarketSegmentRelation = ObjectModel.Ko.OMGroupToMarketSegmentRelation
                .Where(x => x.GroupId == Model.GroupId)
                .Select(x => x.MarketSegment_Code)
                .Select(x => x.TerritoryType_Code)
                .ExecuteFirstOrDefault();
            AddLog($"Найден тип: {groupToMarketSegmentRelation?.MarketSegment_Code.GetEnumDescription()}\n");

            //var territoryCondition = ModelingService.GetConditionForTerritoryType(groupToMarketSegmentRelation.TerritoryType_Code);

            var groupedObjects = OMCoreObject.Where(x =>
                    x.PropertyMarketSegment_Code == groupToMarketSegmentRelation.MarketSegment_Code &&
                    x.CadastralNumber != null &&
                    x.ProcessType_Code != ProcessStep.Excluded)
                //TODO PART
                //.And(territoryCondition)
                .Select(x => x.CadastralNumber)
                .Select(x => x.Price)
                .GroupBy(x => new
                {
                    x.CadastralNumber,
                    x.Price
                })
                .ExecuteSelect(x => new
                {
                    x.CadastralNumber,
                    x.Price
                });
            AddLog($"Выполнен sql запрос. Найдено {groupedObjects.Count} объекта.\n");

            ModelingService.DestroyModelMarketObjects(Model.Id);
            AddLog($"Удалены предыдущие данные.\n");

            var modelAttributes = ModelingService.GetModelAttributes(Model.Id);
            AddLog($"Получено {modelAttributes?.Count} атрибутов для модели.\n");


            var maxDegreeOfParallelism = 20;
            AddLog($"Максимальное число потоков {maxDegreeOfParallelism}: ");
            var cancelTokenSource = new CancellationTokenSource();
            var options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };

            AddLog($"Обработано объектов: ");
            var i = 0;
            Parallel.ForEach(groupedObjects, options, groupedObj =>
            {
                var isForTraining = i < groupedObjects.Count / 2;
                i++;
                var modelObject = new OMModelToMarketObjects
                {
                    ModelId = Model.Id,
                    CadastralNumber = groupedObj.CadastralNumber,
                    Price = groupedObj.Price ?? 0,
                    IsForTraining = isForTraining
                };

                var objectCoefficients = ModelingService.GetCoefficientsForObject(Model.TourId,
                    modelObject.CadastralNumber, modelAttributes);

                modelObject.Coefficients = objectCoefficients.SerializeToXml();
                modelObject.Save();

                if (i % 100 == 0)
                    AddLog($"{i}, ");
            });

            AddLog($"\nСбор данных закончен");
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

        public override void ProcessServiceResponse(GeneralResponse generalResponse)
        {
            var trainingResult = JsonConvert.DeserializeObject<TrainingResponse>(generalResponse.Data.ToString());
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

        public override void SendFailNotification(OMQueue processQueue, Exception exception)
        {
            var subject = $"Процесс обучения модели '{Model.Name}'";
            var message = $"Операция завершена с ошибкой: {exception.Message}. \nПодробнее в списке процессов.";
            NotificationSender.SendNotification(processQueue, subject, message);
        }


        #region Support Methods

        private void AddLog(string message)
        {
            var previousLog = string.IsNullOrWhiteSpace(ProcessQueue.Log) ? string.Empty : ProcessQueue.Log;
            var newLog = previousLog + message;
            ProcessQueue.Log = newLog;
            ProcessQueue.Save();
        }

        /// <summary>
        /// Заменяем имена аттрибутов на их Id
        /// </summary>
        /// <param name="result"></param>
        private void PreprocessTrainingResult(TrainingResponse result)
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

        private void UpdateModel(TrainingResponse trainingResult)
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
