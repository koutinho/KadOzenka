using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
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
		//public string TypeRoom { get; set; }
		public long SubGroup { get; set; }

		public override string ToString()
		{
			return $"Код - {Code}, Группа - '{Group}', Подгруппа - {SubGroup}.";
		}
	}

	public class EstimatedGroupModel
	{
		public long IdTask { get; set; }
		public long IdCodeGroup { get; set; }
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

			CountAllUnits = unitsGetter.GetItemsCount();
			Logger.Debug("Всего в БД {MaxUnitsCount} ЕО", CountAllUnits);

			var estimatedSubGroupAttribute = RegisterCache.GetAttributeData((int) param.IdEstimatedSubGroup);
			var codeGroupAttribute = RegisterCache.GetAttributeData((int)param.IdCodeGroup);
			var tourId = OMTask.Where(x => x.Id == param.IdTask).Select(x => x.TourId).ExecuteFirstOrDefault().TourId;
			var allComplianceGuidesInTour = GetAllComplianceGuidesInTour(tourId);

			var packageSize = 100000;
			var numberOfPackages = CountAllUnits / packageSize + 1;
			var generalCancelTokenSource = new CancellationTokenSource();
			var generalOptions = new ParallelOptions
			{
				CancellationToken = generalCancelTokenSource.Token,
				MaxDegreeOfParallelism = 3
			};
			Parallel.For(0, numberOfPackages, generalOptions, (i, s) => 
			{
				////TODO для тестирования
				//var cadasterNumbersForTesting = new List<string> { "77:02:0023003:88", "50:21:0110114:855", "50:26:0150506:743" };
				//var currentUnitsPartition = unitsGetter.GetItems(i, packageSize).Where(x => cadasterNumbersForTesting.Contains(x.CadastralNumber)).ToList();
				var currentUnitsPartition = unitsGetter.GetItems(i, packageSize);
				var gbuObjectIds = currentUnitsPartition.Select(x => x.ObjectId).ToList();
				Logger.ForContext("CurrentHandledCount", CurrentCount)
					.ForContext("UnitPartitionCount", currentUnitsPartition.Count)
					.ForContext("CountAllUnits", CountAllUnits)
					.Debug("Начата обработка пакета юнитов №{PackageIndex} из {MaxPackageIndex}", i, numberOfPackages);

				var codeGroups = GetValueFactors(gbuObjectIds, codeGroupAttribute.RegisterId, codeGroupAttribute.Id);
				Logger.Debug("Найдено {CodeGroupsCount} атрибутов с кодом группы для пакета №{PackageIndex}", codeGroups.Count, i);

				var currentComplianceGuides = new List<OMComplianceGuide>();
				if (currentUnitsPartition.IsNotEmpty() && codeGroups.Values.Any(x => !string.IsNullOrEmpty(x.Value)))
				{
					var propertyTypeValues = currentUnitsPartition.Select(x => x.PropertyType).Distinct().ToList();
					var codeGroupValues = codeGroups.Values.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value).Distinct().ToList();
					currentComplianceGuides = allComplianceGuidesInTour.Where(x => codeGroupValues.Contains(x.Code) && propertyTypeValues.Contains(x.TypeProperty)).ToList();
				}
				Logger.Debug("Найдено {CurrentComplianceGuidesCount} значений из таблицы соответствий кода и группы для пакета №{PackageIndex}", currentComplianceGuides.Count, i);

				var cancelTokenSource = new CancellationTokenSource();
				var options = new ParallelOptions
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
						AddErrorRow(item.CadastralNumber, $"Не заполнен код в атрибуте '{codeGroupAttribute.Name}'", reportService);
						return;
					}

					var codeGroupValue = codeGroup.Value;
					var complianceGuides = GetComplianceGuides(currentComplianceGuides.Where(x => x.Code == codeGroupValue && x.TypeProperty == item.PropertyType).ToList());
					if (complianceGuides.IsEmpty())
					{
						AddErrorRow(item.CadastralNumber, $"В таблице сопоставления не найдено значение с кодом '{codeGroupValue}' и типом '{item.PropertyType}'", reportService);
					} 
					else if (complianceGuides.Count == 1)
					{
						var value = complianceGuides[0].Group;
						AddValueFactor(item.ObjectId, estimatedSubGroupAttribute.Id, codeGroup.IdDocument, DateTime.Now, value);
						AddRowToReport(item.CadastralNumber, estimatedSubGroupAttribute.Id, codeGroupAttribute.Id, value, reportService);
					} 
					else
					{
						var complianceGuidesStr = new StringBuilder();
						complianceGuides.ForEach(x => complianceGuidesStr.AppendLine(x.ToString()));
						AddErrorRow(item.CadastralNumber, $"Найдено несколько соответсвий: {complianceGuidesStr}", reportService);
					}
				});

				Logger.Debug("Закончена обработка пакета юнитов №{PackageIndex} из {MaxPackageIndex}", i, numberOfPackages);
				//попытка принудительно освободить память
				currentUnitsPartition = null;
				GC.Collect();
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

			Logger.Debug("Найдено {ComplianceCount} строк из Таблицы соответствия кода и группы", allComplianceGuidesInTour.Count);

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
		private string BaseUnitsCondition { get; set; }

		public EstimatedGroupAffixingUnitsGetter(ILogger logger, EstimatedGroupModel setting) : base(logger)
		{
			Settings = setting;

			BaseUnitsCondition = $@" where unit.TASK_ID = {Settings.IdTask}";
		}


		public override List<SetEstimatedGroupUnitPure> GetItems(int packageIndex, int packageSize)
		{
			if (Settings.IdTask == 0)
				return new List<SetEstimatedGroupUnitPure>();

			var sql = $@"select OBJECT_ID as {nameof(SetEstimatedGroupUnitPure.ObjectId)}, 
								CADASTRAL_NUMBER as {nameof(SetEstimatedGroupUnitPure.CadastralNumber)}, 
								PROPERTY_TYPE as {nameof(SetEstimatedGroupUnitPure.PropertyType)}
										from ko_unit unit
										{BaseUnitsCondition}
										order by unit.id 
										limit {packageSize} offset {packageIndex * packageSize}";

			return QSQuery.ExecuteSql<SetEstimatedGroupUnitPure>(sql);
		}

		public override int GetItemsCount()
		{
			var columnName = "count";
			var countSql = $@"select count(*) as {columnName} from ko_unit unit {BaseUnitsCondition}";
			var command = DBMngr.Main.GetSqlStringCommand(countSql);
			var dataSet = DBMngr.Main.ExecuteDataSet(command);

			var unitCount = 0;
			var row = dataSet.Tables[0]?.Rows[0];
			if (row != null)
			{
				unitCount = row[columnName].ParseToInt();
			}

			return unitCount;
		}
	}

	#endregion
}