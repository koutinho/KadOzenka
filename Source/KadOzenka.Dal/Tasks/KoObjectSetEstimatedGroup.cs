﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Register;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Entities;
using Newtonsoft.Json;
using ObjectModel.Gbu;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.KoObject
{
	#region TypeStructure

	public enum ReportColumns: int
	{
		KnColumn = 0,
		InputFieldColumn = 1,
		ValueColumn = 2,
		OutputFieldColumn = 3,
		ErrorColumn = 4
	}
	public struct ValueItem
	{
		public string Value { get; set; }
		public long? IdDocument { get; set; }
	}

	public struct ComplianceGuid
	{
		public string Group { get; set; }
		public string Code { get; set; }
		public string TypeRoom { get; set; }
		public long SubGroup { get; set; }
	}

	public class EstimatedGroupModel
	{
		public long IdTask { get; set; }
		public long IdCodeGroup { get; set; }
		public long IdCodeQuarter { get; set; }
		public long IdTerritoryType { get; set; }
		public List<ObjectChangeStatus> ObjectChangeStatus { get; set; }

		/// <summary>
		/// Result parameter.
		/// </summary>
		public long IdEstimatedSubGroup { get; set; }
	}

	#endregion


	public class KoObjectSetEstimatedGroup
	{
		private static readonly ILogger Logger = Log.ForContext<KoObjectSetEstimatedGroup>();
		private GbuObjectService GbuObjectService { get; }
		private object locked;

		public int CountAllUnits { get; private set; }
		public int CurrentCount { get; private set; }

		public KoObjectSetEstimatedGroup()
		{
			GbuObjectService = new GbuObjectService();
		}


		public string Run(EstimatedGroupModel param)
		{ 
			Logger.ForContext("InputParameters", JsonConvert.SerializeObject(param)).Debug("Входные данные для Присвоения оценочной группы");

			using var reportService = new GbuReportService("Отчет проставления оценочной группы");
			reportService.AddHeaders(new List<string>{"КН", "Поле в которое производилась запись", "Внесенное значение", "Источник внесенного значения", "Ошибка" });
			reportService.SetIndividualWidth((int)ReportColumns.KnColumn, 4);
			reportService.SetIndividualWidth((int)ReportColumns.InputFieldColumn, 6);
			reportService.SetIndividualWidth((int)ReportColumns.ValueColumn, 3);
			reportService.SetIndividualWidth((int)ReportColumns.OutputFieldColumn, 6);
			reportService.SetIndividualWidth((int)ReportColumns.ErrorColumn, 5);
			locked = new object();

			var unitsGetter = new EstimatedGroupAffixingUnitsGetter(Logger, param) as AItemsGetter<SetEstimatedGroupUnitPure>;
			unitsGetter = new GbuObjectStatusFilterDecorator<SetEstimatedGroupUnitPure>(unitsGetter, Logger,
				param.ObjectChangeStatus, DateTime.Now.GetEndOfTheDay());

			////TODO для тестирования
			//var cadasterNumbersForTesting = new List<string> { "77:02:0023003:88", "50:21:0110114:855", "50:26:0150506:743" };
			//var units = unitsGetter.GetItems().Where(x => cadasterNumbersForTesting.Contains(x.CadastralNumber)).ToList();
			var units = unitsGetter.GetItems();
			Logger.Debug($"Найдено {units.Count} ЕО");
			CountAllUnits = units.Count;

			var estimatedSubGroupAttribute = RegisterCache.GetAttributeData((int) param.IdEstimatedSubGroup);
			var codeGroupAttribute = RegisterCache.GetAttributeData((int)param.IdCodeGroup);
			var attributeQuarter = RegisterCache.GetAttributeData((int)param.IdCodeQuarter);
			var attributeTerritoryType = RegisterCache.GetAttributeData((int)param.IdTerritoryType);
			var tourId = OMTask.Where(x => x.Id == param.IdTask).Select(x => x.TourId).ExecuteFirstOrDefault().TourId;
			var allComplianceGuidesInTour = GetAllComplianceGuidesInTour(tourId);

			// обрабатываем юниты порциями по 1000 для уменьшения числа запросов к бд
			var partitionSize = 100000;
			var partitionCount = units.Count / partitionSize + 1;
			var generalCancelTokenSource = new CancellationTokenSource();
			var generalOptions = new ParallelOptions
			{
				CancellationToken = generalCancelTokenSource.Token,
				MaxDegreeOfParallelism = 5
			};
			Parallel.For(0, partitionCount, generalOptions, (i, s) => 
			{
				var currentUnitsPartition = units.Skip(i * partitionSize).Take(partitionSize).ToList();
				var gbuObjectIds = currentUnitsPartition.Select(x => x.ObjectId).ToList();
				Logger.ForContext("CurrentHandledCount", CurrentCount)
					.ForContext("UnitPartitionCount", currentUnitsPartition.Count)
					.ForContext("CountAllUnits", CountAllUnits)
					.Debug("Обаботка пакета юнитов");

				var codeGroups = GetValueFactors(gbuObjectIds, codeGroupAttribute.RegisterId, codeGroupAttribute.Id);
				Logger.Verbose("Получение значений атрибута кода группы ГБУ объектов");

				var currentComplianceGuides = new List<OMComplianceGuide>();
				if (currentUnitsPartition.IsNotEmpty() && codeGroups.Values.Any(x => !string.IsNullOrEmpty(x.Value)))
				{
					var propertyTypeValues = currentUnitsPartition.Select(x => x.PropertyType).Distinct().ToList();
					var codeGroupValues = codeGroups.Values.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value).Distinct().ToList();
					currentComplianceGuides = allComplianceGuidesInTour.Where(x => codeGroupValues.Contains(x.Code) && propertyTypeValues.Contains(x.TypeProperty)).ToList();
				}
				Logger.Verbose("Получение значений из таблицы соответствий кода и группы");

				var codeQuarters = GetValueFactors(gbuObjectIds, attributeQuarter.RegisterId, attributeQuarter.Id);
				Logger.Verbose("Получение значений атрибута кадастровый квартал ГБУ объектов");

				var gbuQuarterObjects = new List<OMMainObject>();
				if (codeQuarters.Values.Any(x => !string.IsNullOrEmpty(x.Value)))
				{
					var codeQuartersValues = codeQuarters.Values.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value).Distinct().ToList();
					gbuQuarterObjects = OMMainObject.Where(x => codeQuartersValues.Contains(x.CadastralNumber)).Select(x => x.CadastralNumber).Execute();
				}
				Logger.ForContext("GbuQuarterObjectsCount", gbuQuarterObjects.Count)
					.Verbose("Получение ГБУ объектов кадастровых кварталов");

				var gbuQuarterObjectIds = gbuQuarterObjects.Select(x => x.Id).ToList();
				var territoryTypes = GetValueFactors(gbuQuarterObjectIds, attributeTerritoryType.RegisterId, attributeTerritoryType.Id);
				Logger.Verbose("Получение значений атрибута тип территории ГБУ объектов кадастровых кварталов");

				CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
				ParallelOptions options = new ParallelOptions
				{
					CancellationToken = cancelTokenSource.Token,
					MaxDegreeOfParallelism = 10
				};
				Logger.Debug("Начата обработка каждого юнита");
				Parallel.ForEach(currentUnitsPartition, options, item =>
				{
					lock (locked)
					{
						CurrentCount++;
					}

					codeGroups.TryGetValue(item.ObjectId, out var codeGroup);
					if (string.IsNullOrEmpty(codeGroup.Value))
					{
						AddErrorRow(item.CadastralNumber, $"Не найдено значение справочника ЦОД у объекта {item.CadastralNumber}", reportService);
						return;
					}

					var complianceGuides = GetComplianceGuides(currentComplianceGuides.Where(x => x.Code == codeGroup.Value && x.TypeProperty == item.PropertyType).ToList());

					if (complianceGuides.IsEmpty())
					{
						AddErrorRow(item.CadastralNumber, $"Не найдено значение в таблице сопоставления {item.CadastralNumber}", reportService);
					} 
					else if (complianceGuides.Count == 1)
					{
						var value = complianceGuides[0].Group;
						AddValueFactor(item.ObjectId, estimatedSubGroupAttribute.Id, codeGroup.IdDocument, DateTime.Now, value);
						AddRowToReport(item.CadastralNumber, estimatedSubGroupAttribute.Id, codeGroupAttribute.Id, value, reportService);
					} 
					else
					{
						codeQuarters.TryGetValue(item.ObjectId, out var codeQuarter);
						if (string.IsNullOrEmpty(codeQuarter.Value))
						{
							AddErrorRow(item.CadastralNumber, $"Не найден кадастровый квартал для объекта {item.CadastralNumber}.", reportService);
							return;
						}

						var gbuQuarterObject = gbuQuarterObjects.FirstOrDefault(x => x.CadastralNumber == codeQuarter.Value);
						if (gbuQuarterObject == null)
						{
							AddErrorRow(item.CadastralNumber, $"Не найден объект кадастровый квартал {codeQuarter.Value} .", reportService);
							return;
						}

						territoryTypes.TryGetValue(gbuQuarterObject.Id, out var territoryType);
						if (string.IsNullOrEmpty(territoryType.Value))
						{
							AddErrorRow(item.CadastralNumber, $"Не найден тип территории для объекта {gbuQuarterObject.CadastralNumber}", reportService);
							return;
						}

						var complianceGuid = complianceGuides
							.FirstOrDefault(x => x.SubGroup.ToString() == territoryType.Value);

						if (string.IsNullOrEmpty(complianceGuid.Code))
						{
							AddErrorRow(item.CadastralNumber, $"Не найдено значение в таблице сопоставления, для кода {codeGroup.Value} и подгруппы {territoryType.Value}", reportService);
							return;
						}
						var value = complianceGuid.Group;
						AddValueFactor(item.ObjectId, estimatedSubGroupAttribute.Id, codeGroup.IdDocument, DateTime.Now, value);
						AddRowToReport(item.CadastralNumber, estimatedSubGroupAttribute.Id, codeGroupAttribute.Id, value, reportService);
					}
				});
			});

			var reportId = reportService.SaveReport();

			Logger.Debug("Закончена операция присвоения оценочной группы");

			return reportService.GetUrlToDownloadFile(reportId);
		}


		#region Help Methods

		private List<OMComplianceGuide> GetAllComplianceGuidesInTour(long? tourId)
		{
			var allComplianceGuidesInTour = OMComplianceGuide.Where(x => x.TourId == tourId).Select(x => new
			{
				x.SubGroup,
				x.TypeProperty,
				x.Code
			}).Execute();

			Logger.Debug("Найдено {ComplianceCount} строк из Таблицы соответствия кода и группы");

			return allComplianceGuidesInTour;
		}

		private void AddValueFactor(long objectId, long? idFactor, long? idDoc, DateTime date, string value)
		{
			var attributeValue = new GbuObjectAttribute
			{
				Id = -1,
				AttributeId = idFactor.Value,
				ObjectId = objectId,
				ChangeDocId = (idDoc == null) ? -1 : idDoc.Value,
				S = date.Date,
				ChangeUserId = SRDSession.Current.UserID,
				ChangeDate = DateTime.Now,
				Ot = date.Date.Date,
				StringValue = value,
			};
			attributeValue.Save();
		}

		private Dictionary<long, ValueItem> GetValueFactors(List<long> objectIds, long idRegister, long idFactor)
		{
			var result = new Dictionary<long, ValueItem>();

			var attributes = GbuObjectService.GetAllAttributes(objectIds,
				new List<long> {idRegister}, new List<long> {idFactor}, DateTime.Now.Date,
				attributesToDownload: new List<GbuColumnsToDownload> {GbuColumnsToDownload.Value, GbuColumnsToDownload.DocumentId});

			foreach (var id in objectIds)
			{
				ValueItem res = new ValueItem
				{
					Value = string.Empty,
					IdDocument = null,
				};

				var objAttr = attributes.FirstOrDefault(x => x.ObjectId == id);
				if (objAttr != null)
				{
					var valueInString = objAttr.GetValueInString();
					if (!string.IsNullOrEmpty(valueInString))
					{
						res.Value = valueInString;
						res.IdDocument = objAttr.ChangeDocId;
					}
				}

				result.Add(id, res);
			}

			return result;
		}

		private List<ComplianceGuid> GetComplianceGuides(List<OMComplianceGuide> complianceGuides)
		{
			var res = new List<ComplianceGuid>();

			foreach (var complianceGuide in complianceGuides)
			{
				long.TryParse(complianceGuide.SubGroup?.Split('.')[1], out var sGroup);
				if(complianceGuide.SubGroup != null)
					res.Add(new ComplianceGuid { Group = complianceGuide.SubGroup, Code = complianceGuide.Code, SubGroup = sGroup });
			}

			return res;
		}

		private void AddErrorRow(string kn, string value, GbuReportService reportService)
		{
			lock (locked)
			{
				var rowReport = reportService.GetCurrentRow();
				reportService.AddValue(kn, (int)ReportColumns.KnColumn, rowReport);
				reportService.AddValue(value, (int)ReportColumns.ErrorColumn, rowReport);
			}
		}

		private void AddRowToReport(string kn, long inputAttributeId, long sourceAttributeId, string value, GbuReportService reportService)
		{
			lock (locked)
			{
				var rowReport = reportService.GetCurrentRow();
				var inputAttributeName = GbuObjectService.GetAttributeNameById(inputAttributeId);
				var sourceAttributeName = GbuObjectService.GetAttributeNameById(sourceAttributeId);
				reportService.AddValue(kn, (int)ReportColumns.KnColumn, rowReport);
				reportService.AddValue(inputAttributeName, (int)ReportColumns.InputFieldColumn, rowReport);
				reportService.AddValue(value, (int)ReportColumns.ValueColumn, rowReport);
				reportService.AddValue(sourceAttributeName, (int)ReportColumns.OutputFieldColumn, rowReport);
				reportService.AddValue(string.Empty, (int)ReportColumns.ErrorColumn, rowReport);
			}
		}

		#endregion
    }


	#region Entities

	public class SetEstimatedGroupUnitPure : ItemBase
	{
		public string CadastralNumber { get; set; }
		public string PropertyType { get; set; }
	}

	public class EstimatedGroupAffixingUnitsGetter : AItemsGetter<SetEstimatedGroupUnitPure>
	{
		public EstimatedGroupModel Settings { get; set; }

		public EstimatedGroupAffixingUnitsGetter(ILogger logger, EstimatedGroupModel setting) : base(logger)
		{
			Settings = setting;
		}


		public override List<SetEstimatedGroupUnitPure> GetItems()
		{
			if (Settings.IdTask == 0)
				return new List<SetEstimatedGroupUnitPure>();

			return OMUnit.Where(x => x.TaskId == Settings.IdTask && x.ObjectId != null)
				.Select(x => new
				{
					x.ObjectId,
					x.CadastralNumber,
					x.PropertyType
				})
				.Execute()
				.Select(x => new SetEstimatedGroupUnitPure
				{
					Id = x.Id,
					ObjectId = x.ObjectId.GetValueOrDefault(),
					CadastralNumber = x.CadastralNumber,
					PropertyType = x.PropertyType
				}).ToList();
		}
	}

	#endregion
}