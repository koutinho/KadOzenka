using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.Modeling.InputParameters;
using MarketPlaceBusiness;
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Factors;
using ModelingBusiness.Model;
using ModelingBusiness.Objects.Entities;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Modeling
{
	public class FactorAdditionToModelObjectsLongProcess : BaseObjectFormationForModelingProcess
	{
		public static long ProcessId => 62;
		private OMModel Model { get; set; }
		private OMQueue Queue { get; set; }
		private ModelAttributePure Attribute { get; set; }

		public FactorAdditionToModelObjectsLongProcess() : base(Log.ForContext<FactorAdditionToModelObjectsLongProcess>())
		{
			ModelService = new ModelService();
			ModelFactorsService = new ModelFactorsService();
			ModelDictionaryService = new ModelDictionaryService();
		}


		public static void AddProcessToQueue(FactorAdditionToModelObjectsInputParameters inputParameters)
		{
			LongProcessManager.AddTaskToQueue(nameof(FactorAdditionToModelObjectsLongProcess),
				objectId: inputParameters.ModelId, parameters: inputParameters.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Logger.ForContext("InputParameters", processQueue.Parameters).Information("Старт фонового процесса при добавлении нового фактора к Модели");
			WorkerCommon.SetProgress(processQueue, 0);
			Queue = processQueue;

			var inputParameters = processQueue.Parameters?.DeserializeFromXml<FactorAdditionToModelObjectsInputParameters>();
			if (inputParameters == null || inputParameters.ModelId == 0 || inputParameters.AttributeId == 0)
			{
				WorkerCommon.SetMessage(processQueue, Common.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Common.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				NotificationSender.SendNotification(processQueue, GetMessageSubject(), $"Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов. {processQueue.Parameters}");
				return;
			}

			try
			{
				Model = ModelService.GetModelEntityById(inputParameters.ModelId);
				var allModelAttributes = GetGeneralModelAttributes(Model.Id);

				Attribute = allModelAttributes.First(x => x.AttributeId == inputParameters.AttributeId);
				var attributes = new List<ModelAttributePure> { Attribute };
				AddLog(Queue, $"Найден атрибут '{Attribute.AttributeName}'.", logger: Logger);

				var dictionaries = Attribute.DictionaryId == null
					? new List<OMModelingDictionary>()
					: ModelDictionaryService.GetDictionaries(new List<long> { Attribute.DictionaryId.Value });
				AddLog(Queue, $"Найдено {dictionaries?.Count} словарей для атрибута.", logger: Logger);

				var objects = ModelObjectsService.GetModelObjects(inputParameters.ModelId);
				AddLog(Queue, $"Найдено {objects.Count} объектов Модели.", logger: Logger);

				AddLog(Queue, "Начат сбор коэффициентов.", logger: Logger);
				var coefficients = Attribute.RegisterId == Consts.RegisterId
					? GetCoefficientsFromMarketObject(objects.Select(x => x.MarketObjectId.GetValueOrDefault()).ToList(), dictionaries, attributes)
					: GetCoefficientsFromTourFactors(objects.Where(x => x.UnitId != null).Select(x => x.UnitId.Value).Distinct().ToList(), dictionaries, attributes);
				AddLog(Queue, "Закончен сбор коэффициентов.", logger: Logger);

				AddLog(Queue, "Начато сохранение коэффициентов.", logger: Logger);
				SaveCoefficient(objects, coefficients);
				AddLog(Queue, "Закончено сохранение коэффициентов.", logger: Logger);

				CreateMarkCatalog(objects, attributes, Queue);

				SaveStatistic(objects, allModelAttributes, Model, Queue);

				NotificationSender.SendNotification(Queue, GetMessageSubject(), "Операция успешно завершена");
			}
			catch (Exception exception)
			{
				var errorId = ErrorManager.LogError(exception);
				NotificationSender.SendNotification(Queue, GetMessageSubject(), $"Операция завершена с ошибкой: {exception.Message}. Подробнее в журнале ({errorId})");
				Logger.Error(exception, "Ошибка в ходе сбора данных для моделирования при добавлении нового фактора");
			}

			WorkerCommon.SetProgress(Queue, 100);
			Logger.Information("Закончен фоновый процесс для добавления нового фактора к Модели");
		}


		#region Support Methods

		private void SaveCoefficient(List<OMModelToMarketObjects> objects, Dictionary<long, List<CoefficientForObject>> coefficientsToObject)
		{
			AddLog(Queue, "Обработано объектов: ", logger: Logger);

			for (var i = 0; i < objects.Count; i++)
			{
				var objectId = Attribute.RegisterId == Consts.RegisterId ? objects[i].MarketObjectId : objects[i].UnitId;
				coefficientsToObject.TryGetValue(objectId.GetValueOrDefault(), out var coefficients);
				var coefficient = coefficients?.FirstOrDefault();

				var existedCoefficients = objects[i].DeserializedCoefficients;

				var mustUpdateCoefficient = false;
				var existedAttributeInCoefficients = existedCoefficients.FirstOrDefault(x => x.AttributeId == Attribute.AttributeId);
				if (existedAttributeInCoefficients == null)
				{
					if (coefficient != null)
					{
						mustUpdateCoefficient = true;
					}
				}
				else
				{
					if (existedAttributeInCoefficients.Value != coefficient?.Value ||
					    existedAttributeInCoefficients.Coefficient != coefficient?.Coefficient)
					{
						mustUpdateCoefficient = true;
						existedCoefficients.Remove(existedAttributeInCoefficients);
					}
				}

				if (mustUpdateCoefficient)
				{
					existedCoefficients.Add(coefficient);
					objects[i].Coefficients = existedCoefficients.SerializeCoefficient();
					objects[i].Save();
				}

				if (i % 100 == 0)
					AddLog(Queue, $"{i}, ", false, logger: Logger);
			}
		}

		private string GetMessageSubject()
		{
			return $"Результат сбора данных для добавленного фактора '{Attribute?.AttributeName}' Модели '{Model?.Name}'";
		}

		#endregion
	}
}
