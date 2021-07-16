using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.ConfigurationManagers;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.Modeling.InputParameters;
using KadOzenka.Dal.Modeling.Dictionaries;
using KadOzenka.Dal.Modeling.Factors.Entities;
using KadOzenka.Dal.Modeling.Modeling.Requests;
using KadOzenka.Dal.Modeling.Modeling.Responses;
using KadOzenka.Dal.Modeling.Objects.Entities;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Modeling;
using Serilog;

namespace KadOzenka.Dal.Modeling.Modeling
{
    public class Training : AModelingTemplate
    {
	    public ModelDictionaryService ModelDictionaryService { get; set; }
        private TrainingRequest RequestForService { get; set; }
        protected GeneralModelingInputParameters InputParameters { get; set; }
        private OMModel GeneralModel { get; }
        private List<OMModelToMarketObjects> MarketObjectsForTraining { get; set; }
        private List<ModelAttributeRelationPure> ModelAttributes { get; set; }
        protected override string SubjectForMessageInNotification => $"Процесс обучения модели '{GeneralModel.Name}'";
        private string AdditionalMessage { get; set; }

        public Training(string inputParametersXml, OMQueue processQueue)
            : base(processQueue, Log.ForContext<Training>())
        {
            InputParameters = inputParametersXml.DeserializeFromXml<GeneralModelingInputParameters>();
            GeneralModel = ModelService.GetModelEntityById(InputParameters.ModelId);
            MarketObjectsForTraining = new List<OMModelToMarketObjects>();
            ModelAttributes = new List<ModelAttributeRelationPure>();
            ModelDictionaryService = new ModelDictionaryService();
        }


        protected override string GetUrl()
        {
            var baseUrl = ConfigurationManager.KoConfig.ModelingProcessConfig.TrainingBaseUrl;
            switch (InputParameters.ModelType)
            {
	            case KoAlgoritmType.None:
		            return $"{baseUrl}/{ConfigurationManager.KoConfig.ModelingProcessConfig.TrainingAllTypesUrl}/{GeneralModel.InternalName}";
                case KoAlgoritmType.Line:
                    return $"{baseUrl}/{ConfigurationManager.KoConfig.ModelingProcessConfig.TrainingLinearTypeUrl}/{GeneralModel.InternalName}";
                case KoAlgoritmType.Exp:
                    return $"{baseUrl}/{ConfigurationManager.KoConfig.ModelingProcessConfig.TrainingExponentialTypeUrl}/{GeneralModel.InternalName}";
                case KoAlgoritmType.Multi:
                    return $"{baseUrl}/{ConfigurationManager.KoConfig.ModelingProcessConfig.TrainingMultiplicativeTypeUrl}/{GeneralModel.InternalName}";
                default:
                    throw new Exception($"Не известный тип модели: {InputParameters.ModelType.GetEnumDescription()}");
            }
        }

        protected override void PrepareData()
        {
            AddLog($"Начата работа с моделью '{GeneralModel.Name}', тип модели: '{InputParameters.ModelType.GetEnumDescription()}'.");

            ModelAttributes = ModelFactorsService.GetGeneralModelAttributes(GeneralModel.Id).Where(x => x.IsActive).ToList();
            var unActiveAttributes = ModelFactorsService.GetAttributesWhichMustBeUnActive();
            var activeForbiddenAttributes = ModelAttributes.Select(x => x.Id).Intersect(unActiveAttributes).ToList();
            if (activeForbiddenAttributes.Count > 0)
            {
	            var attributeNames = RegisterCache.RegisterAttributes
		            .Where(x => activeForbiddenAttributes.Contains(x.Key)).Select(x => x.Value.Name);
	            var attributeNamesStr = string.Join(',', attributeNames);
	            throw new Exception($"Атрибуты, которые относятся к аналогам должны быть отмечены как неактивные: '{attributeNamesStr}'");
            }
            AddLog($"Найдено {ModelAttributes?.Count} активных атрибутов для модели.");
            Logger.ForContext("Attributes", ModelAttributes, destructureObjects: true).Debug("Атрибуты для модели");

            MarketObjectsForTraining = ModelObjectsRepository.GetIncludedModelObjects(GeneralModel.Id, IncludedObjectsMode.Training);
            AddLog($"Найдено {MarketObjectsForTraining.Count} объекта для обучения.");
        }

        protected override object GetRequestForService()
        {
            RequestForService = new TrainingRequest();

            RequestForService.AttributeNames.AddRange(ModelAttributes.Select(x => PreProcessAttributeName(x.AttributeName)));
            RequestForService.AttributeIds.AddRange(ModelAttributes.Select(x => x.AttributeId));

            var counter = 0;
            MarketObjectsForTraining.ForEach(modelObject =>
            {
                if(counter++ % 100 == 0)
					Logger.Debug("Идет обработка объекта моделирования №{i}", counter);

                var modelObjectAttributes = modelObject.DeserializeCoefficient();
                if (modelObjectAttributes == null || modelObjectAttributes.Count == 0)
                    return;

                var coefficients = new List<decimal?>();
                ModelAttributes.ForEach(modelAttribute =>
                {
	                var currentAttribute = modelObjectAttributes.FirstOrDefault(x => x.AttributeId == modelAttribute.AttributeId);
                    coefficients.Add(currentAttribute?.Coefficient);
                });

                if (coefficients.All(x => x != null))
                {
	                if (modelObject.IsForTraining.GetValueOrDefault())
	                {
		                RequestForService.CoefficientsForTraining.Add(coefficients);
		                RequestForService.PricesForTraining.Add(new List<decimal> { modelObject.Price });
                    }
	                if (modelObject.IsForControl.GetValueOrDefault())
	                {
		                RequestForService.CoefficientsForControl.Add(coefficients);
		                RequestForService.PricesForControl.Add(new List<decimal> { modelObject.Price });
                    }

                    RequestForService.CadastralNumbers.Add(modelObject.CadastralNumber);
                }
            });

            if (RequestForService.CoefficientsForControl.Count < 2)
                throw new Exception("Недостаточно данных для построения модели (у которых значения всех атрибутов не пустые). Для объектов в контрольной выборке.");
            if (RequestForService.CoefficientsForTraining.Count < 2)
	            throw new Exception("Недостаточно данных для построения модели (у которых значения всех атрибутов не пустые). Для объектов в обучающей выборке.");

            return RequestForService;
        }

        protected override void ProcessServiceResponse(GeneralResponse generalResponse)
        {
	        var trainingResults = new List<TrainingResponse>();

	        var data = generalResponse.Data.ToString();

            Logger.ForContext("TrainingResultFromService", data).Debug("Результаты обучения от сервиса");
            if (InputParameters.ModelType == KoAlgoritmType.None)
	        {
		        trainingResults = JsonConvert.DeserializeObject<List<TrainingResponse>>(data);
            }
	        else
	        {
		        var trainingResult = JsonConvert.DeserializeObject<TrainingResponse>(data);
		        trainingResults.Add(trainingResult);
            }

            ResetPredictedPrice();
            AddLog("Закончен сброс спрогнозированной цены.");

            var returnedResultType = new List<KoAlgoritmType>();
            trainingResults.ForEach(trainingResult =>
            {
	            if (trainingResult == null)
	            {
		            var returnedTypesStr = string.Join(", ", returnedResultType.Select(x => x.GetEnumDescription()).ToArray());

                    AdditionalMessage = $"Сервис моделирования вернул результаты обучения для алгоритмов: {returnedTypesStr}";
		            Logger.Error(AdditionalMessage);
                    return;
                }

	            PreprocessTrainingResult(trainingResult);

	            var trainingType = GetTrainingType(trainingResult.Type);
	            returnedResultType.Add(trainingType);

                SaveCoefficients(trainingResult.Coefficients?.CoefficientsForAttributes, trainingType);
	            SaveTrainingResult(trainingType, trainingResult, trainingResult.Coefficients?.A0);

	            AddLog($"Закончено сохранение коэффициентов для типизированной модели '{trainingType.GetEnumDescription()}'.");
            });

            try
            {
	            if (InputParameters.ModelType == KoAlgoritmType.None)
	            {
		            var array = (KoAlgoritmType[])System.Enum.GetValues(typeof(KoAlgoritmType));
		            var list = new List<KoAlgoritmType>(array);
		            var notReturnedTypes = list.Where(x => x != KoAlgoritmType.None).Except(returnedResultType).ToList(); 
		            notReturnedTypes.ForEach(x =>
		            {
			            ModelingService.ResetTrainingResults(GeneralModel, x);
                    });
                }
            }
            catch (Exception)
            {
	            Logger.Error("Ошибка при сбросе результатов обучения");
            }
        }

        protected override void RollBackResult()
        {
	        ModelingService.ResetTrainingResults(GeneralModel, InputParameters.ModelType);
        }

        protected override void SendSuccessNotification(OMQueue processQueue)
        {
	        var message = string.IsNullOrWhiteSpace(AdditionalMessage) 
		        ? "Операция успешно завершена."
		        : $"Операция частично завершена.<br>{AdditionalMessage}";

	        new NotificationSender().SendNotification(processQueue, SubjectForMessageInNotification, message);
        }


        #region Support Methods

        /// <summary>
        /// Заменяем имена атрибутов на их Id
        /// </summary>
        /// <param name="result"></param>
        private void PreprocessTrainingResult(TrainingResponse result)
        {
	        if (result.Coefficients?.CoefficientsForAttributes == null)
		        return;

            var newCoefficients = new Dictionary<string, decimal>();
            var oldCoefficients = result.Coefficients.CoefficientsForAttributes;

            for (var i = 0; i < oldCoefficients.Count; i++)
            {
                var entry = oldCoefficients.ElementAtOrDefault(i);
                var attributeId = RequestForService.AttributeIds.ElementAtOrDefault(i);

                newCoefficients[attributeId.ToString()] = entry.Value;
            }

            result.Coefficients.CoefficientsForAttributes = newCoefficients;
        }

        private void ResetPredictedPrice()
        {
            var modelObjects = OMModelToMarketObjects.Where(x => x.ModelId == GeneralModel.Id && x.PriceFromModel != null)
                .SelectAll().Execute();

            modelObjects.ForEach(x =>
            {
                x.PriceFromModel = null;
                x.Save();
            });
        }

        private void SaveCoefficients(Dictionary<string, decimal> coefficients, KoAlgoritmType type)
        {
	        var factors = ModelFactorsService.GetFactors(GeneralModel.Id, type);

	        foreach (var coefficient in coefficients)
            {
	            var attributeId = coefficient.Key.ParseToLong();
	            var factor = factors.FirstOrDefault(x => x.FactorId == attributeId);
	            if (factor == null)
		            throw new Exception($"Не найден фактор с ИД {attributeId}");

	            if (factor.Weight != coefficient.Value)
	            {
		            factor.Weight = coefficient.Value;
		            factor.Save();
	            }

                AddLog($"Сохранение коэффициента '{coefficient.Value}' для фактора '{attributeId}' модели '{type.GetEnumDescription()}'");
	        }
        }

        private KoAlgoritmType GetTrainingType(string type)
        {
	        switch (type)
	        {
		        case "line":
			        return KoAlgoritmType.Line;
		        case "exponential":
			        return KoAlgoritmType.Exp;
		        case "multiplicative":
			        return KoAlgoritmType.Multi;
		        default:
			        throw new Exception("Невозможно конвертировать тип модели, присланной из сервиса");
	        }
        }

        private void SaveTrainingResult(KoAlgoritmType type, TrainingResponse trainingResult, decimal? a0)
        {
	        var student = trainingResult.AccuracyScore?.Student;
	        var mse = trainingResult.AccuracyScore?.MeanSquaredError;
	        var r2 = trainingResult.AccuracyScore?.R2;
	        var fisher = trainingResult.AccuracyScore?.FisherCriterion;
            trainingResult.QualityControlInfo = new QualityControlInfo
            {
	            Student = new QualityControlSpecial
	            {
		            Estimated = student?.Estimated,
		            Tabular = student?.Tabular
	            },
	            MeanSquaredError = new QualityControlSpecial
	            {
		            Estimated = mse?.Test,
		            Tabular = mse?.Test
                },
	            R2 = new QualityControlSpecial
	            {
		            Estimated = r2?.Test,
		            Tabular = r2?.Test
	            },
                Fisher = new QualityControlSpecial
                {
	                Estimated = fisher?.Estimated,
                    Tabular = fisher?.Tabular
                }
            };
            var trainingResultStr = JsonConvert.SerializeObject(trainingResult);

            switch (type)
			{
				case KoAlgoritmType.Exp:
					GeneralModel.ExponentialTrainingResult = trainingResultStr;
					GeneralModel.A0ForExponential = a0;
					break;
				case KoAlgoritmType.Line:
					GeneralModel.LinearTrainingResult = trainingResultStr;
					GeneralModel.A0 = a0;
                    break;
				case KoAlgoritmType.Multi:
					GeneralModel.MultiplicativeTrainingResult = trainingResultStr;
					GeneralModel.A0ForMultiplicative = a0;
                    break;
                case KoAlgoritmType.None:
	                throw new Exception("Невозможно обновить результаты обучения модели, т.к. не указан её тип");
			}

			GeneralModel.Save();

			SaveImagesToDb(type, trainingResult);
        }

        private void SaveImagesToDb(KoAlgoritmType type, TrainingResponse trainingResult)
        {
	        var existedImages = ModelingService.GetModelImages(GeneralModel.Id, type);
	        if (existedImages == null)
	        {
		        existedImages = new OMModelTrainingResultImages
		        {
			        ModelId = GeneralModel.Id,
			        AlgorithmType_Code = type
		        };
	        }

	        var scatterImage = DownloadImage(trainingResult.Images?.ScatterLink);
	        var correlationImage = DownloadImage(trainingResult.Images?.CorrelationLink);
	        existedImages.Scatter = scatterImage;
	        existedImages.Correlation = correlationImage;
	        existedImages.Save();
        }

        private byte[] DownloadImage(string url)
        {
	        if (string.IsNullOrWhiteSpace(url))
		        return null;

	        using (var webClient = new WebClient())
	        {
		        return webClient.DownloadData(url);
	        }
        }

        #endregion
    }
}
