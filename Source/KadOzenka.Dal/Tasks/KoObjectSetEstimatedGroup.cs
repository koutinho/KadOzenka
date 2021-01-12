using System;
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
using KadOzenka.Dal.GbuObject.Dto;
using Newtonsoft.Json;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
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

		public static object locked;
		public static int SuccessCount;


		public static long Run(EstimatedGroupModel param)
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

			var unitsGetter = new InheritanceUnitsGetter(Logger, param) as AItemsGetter<SetEstimatedGroupUnitPure>;
			unitsGetter = new GbuObjectStatusFilterDecorator<SetEstimatedGroupUnitPure>(unitsGetter, Logger,
				param.ObjectChangeStatus, DateTime.Now.GetEndOfTheDay());

			var units = unitsGetter.GetItems();
			Logger.Debug($"Найдено {units.Count} ЕО");

			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 1
			};

			var estimatedSubGroupAttribute = RegisterCache.GetAttributeData((int) param.IdEstimatedSubGroup);
			var codeGroupAttribute = RegisterCache.GetAttributeData((int)param.IdCodeGroup);

			var gbuObjects = new List<OMMainObject>();
			var gbuObjectIds = units.Select(x => x.ObjectId).ToList();
			if(gbuObjectIds.IsNotEmpty())
				gbuObjects.AddRange(OMMainObject.Where(x => gbuObjectIds.Contains(x.Id)).SelectAll().Execute());

			Parallel.ForEach(units, options, item =>
			{
				var gbuObject = gbuObjects.FirstOrDefault(x => x.Id == item.ObjectId);

				// берем код группы (значение из справочника цод)
				var codeGroup = GetValueFactor(gbuObject, codeGroupAttribute.RegisterId, codeGroupAttribute.Id);
				if (string.IsNullOrEmpty(codeGroup.Value))
				{
					AddErrorRow(item.CadastralNumber,$"Не найдено значение справочника ЦОД у объекта {gbuObject.CadastralNumber}", reportService);
					return;
				}

				var complianceGuides = GetComplianceGuides(OMComplianceGuide.Where(x => x.Code == codeGroup.Value && x.TypeProperty == item.PropertyType).SelectAll().Execute());

				if (complianceGuides.Count == 1)
				{
                    var value = complianceGuides[0].Group;
                    AddValueFactor(gbuObject, estimatedSubGroupAttribute.Id, codeGroup.IdDocument, DateTime.Now, value);
					AddRowToReport(item.CadastralNumber, estimatedSubGroupAttribute.Id, codeGroupAttribute.Id, value, reportService);
					return;
				}

				if (complianceGuides.Count <= 1)
				{
					AddErrorRow(item.CadastralNumber, $"Не найдено значение в таблице сопоставления {gbuObject.CadastralNumber}", reportService);
					return;
				}
				{
					var attributeQuarter = RegisterCache.GetAttributeData((int)param.IdCodeQuarter);
					// берем кадастровый квартал
					ValueItem codeQuarter = GetValueFactor(gbuObject, attributeQuarter.RegisterId, attributeQuarter.Id);

					if (string.IsNullOrEmpty(codeQuarter.Value))
					{
						AddErrorRow(item.CadastralNumber, $"Не найден кадастровый квартал для объекта {gbuObject.CadastralNumber}.", reportService);
						return;
					}

					var gbuQuarterObject = OMMainObject.Where(x => x.CadastralNumber == codeQuarter.Value).SelectAll().ExecuteFirstOrDefault();

					if (gbuQuarterObject == null)
					{
						AddErrorRow(item.CadastralNumber, $"Не найден объект кадастровый квартал {codeQuarter.Value} .", reportService);
						return;
					}
					var attributeTerritoryType = RegisterCache.GetAttributeData((int)param.IdTerritoryType);

					ValueItem territoryType = GetValueFactor(gbuQuarterObject, attributeTerritoryType.RegisterId, attributeTerritoryType.Id);

					if (string.IsNullOrEmpty(territoryType.Value))
					{
						AddErrorRow(item.CadastralNumber, $"Не найден тип территории для объекта {gbuObject.CadastralNumber}", reportService);
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
					AddValueFactor(gbuObject, estimatedSubGroupAttribute.Id, codeGroup.IdDocument, DateTime.Now, value);
					AddRowToReport(item.CadastralNumber, estimatedSubGroupAttribute.Id, codeGroupAttribute.Id, value, reportService);
				}
			});

			long reportId = reportService.SaveReport();

			Logger.Debug("Закончена операция присвоения оценочной группы");
			return reportId;
		}

		#region Help Methods

		private static void AddValueFactor(OMMainObject mObject, long? idFactor, long? idDoc, DateTime date, string value)
		{
			var attributeValue = new GbuObjectAttribute
			{
				Id = -1,
				AttributeId = idFactor.Value,
				ObjectId = mObject.Id,
				ChangeDocId = (idDoc == null) ? -1 : idDoc.Value,
				S = date.Date,
				ChangeUserId = SRDSession.Current.UserID,
				ChangeDate = DateTime.Now,
				Ot = date.Date.Date,
				StringValue = value,
			};
			var id = attributeValue.Save();
			if (id != 0)
			{
				lock(locked)
				{
					SuccessCount++;
				}
            }
        }

		private static ValueItem GetValueFactor(OMMainObject obj, long idRegister, long idFactor)
		{
			ValueItem res = new ValueItem
			{
				Value = string.Empty,
				IdDocument = null,
			};

			List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(obj.Id, new List<long> { idRegister }, new List<long> { idFactor }, DateTime.Now.Date);
			if (attribs.Count > 0)
			{
				if (attribs[0].GetValueInString() != string.Empty && attribs[0].GetValueInString() != null)
				{
					res.Value = attribs[0].GetValueInString();
					res.IdDocument = attribs[0].ChangeDocId;
				}
			}

			return res;
		}

		private static List<ComplianceGuid> GetComplianceGuides(List<OMComplianceGuide> complianceGuides)
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

		public static void AddErrorRow(string kn, string value, GbuReportService reportService)
		{
			lock (locked)
			{
				var rowReport = reportService.GetCurrentRow();
				reportService.AddValue(kn, (int)ReportColumns.KnColumn, rowReport);
				reportService.AddValue(value, (int)ReportColumns.ErrorColumn, rowReport);
			}
		}

		public static void AddRowToReport(string kn, long inputAttributeId, long sourceAttributeId, string value, GbuReportService reportService)
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

	public class InheritanceUnitsGetter : AItemsGetter<SetEstimatedGroupUnitPure>
	{
		public EstimatedGroupModel Settings { get; set; }

		public InheritanceUnitsGetter(ILogger logger, EstimatedGroupModel setting) : base(logger)
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