using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
	public class CadastralCostDeterminationResultsMainReport : StatisticalDataReport
	{
		private readonly QueryManager _queryManager;
		private const int PackageSize = 125000;
		public static string IndividuallyResultsGroupNamePhrase => "индивидуального расчета";
		private Dictionary<Type, ICadastralCostDeterminationResultsReport> _reportsDictionary;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;
		private object _locker;


		public CadastralCostDeterminationResultsMainReport()
		{
			_queryManager = new QueryManager();
			_reportsDictionary = new Dictionary<Type, ICadastralCostDeterminationResultsReport>();
			_logger = Log.ForContext<CadastralCostDeterminationResultsMainReport>();
			_locker = new object();
		}

		protected override string TemplateName(NameValueCollection query)
		{
			var report = GetReport(query);
			return report.GetTemplateName(query);
		}


		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			_queryManager.SetBaseToken(CancellationToken);
			var taskIds = GetTaskIdList(query).ToList();
			var taskIdStr = string.Join(',', taskIds);

			using (Logger.TimeOperation("Общее время выполнения"))
			{
				var report = GetReport(query);
				Logger.Debug("Тип отчета {ReportType}", report.GetType().ToString());

				var groupIds = report.GetAvailableGroupIds();
				var groupIdsStr = string.Join(',', groupIds);
				Logger.Debug("Найдено {GroupsCount} Групп", groupIds.Count);

				var unitsCount = OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && groupIds.Contains((long)x.GroupId) &&
												   x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
				Logger.Debug("Всего в БД {UnitsCount} ЕО.", unitsCount);


				var options = new ParallelOptions
				{
					CancellationToken = new CancellationTokenSource().Token,
					MaxDegreeOfParallelism = 20
				};
				var cadastralQuarterAttributeId = GbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id;
				var numberOfPackages = unitsCount / PackageSize + 1;
				var operations = new List<ReportItem>();
				Parallel.For(0, numberOfPackages, options, (i, s) =>
				{
					var unitsCondition = $@"where unit.task_id IN ({taskIdStr}) AND 
									unit.GROUP_ID IN ({groupIdsStr}) AND
									(unit.PROPERTY_TYPE_CODE <> 2190 or unit.PROPERTY_TYPE_CODE is null)
										order by unit.id 
										limit {PackageSize} offset {i * PackageSize}";

					var sql = $@"with object_ids as (
									select object_id from ko_unit unit {unitsCondition}
								),
								cadastralDistrictAttrValues as (
									select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {cadastralQuarterAttributeId})
								)
								SELECT
									SUBSTRING(COALESCE(cadastralDistrictAttr.attributeValue, unit.CADASTRAL_BLOCK), 0, 6) as CadastralDistrict,
									unit.CADASTRAL_NUMBER AS CadastralNumber,
									unit.PROPERTY_TYPE AS Type,
									unit.SQUARE AS SQUARE,
									unit.UPKS AS UPKS,
									unit.CADASTRAL_COST AS Cost
										FROM KO_UNIT unit
										LEFT JOIN cadastralDistrictAttrValues cadastralDistrictAttr ON unit.object_id=cadastralDistrictAttr.objectId
										{unitsCondition}";

					List<ReportItem> currentOperations;
					Logger.Debug(new Exception(sql), "Начата работа с пакетом №{PackageNumber} из {MaxPackagesCount}", i, numberOfPackages);
					using (Logger.TimeOperation("Сбор данных для пакета №{i}", i))
					{
						currentOperations = _queryManager.ExecuteSql<ReportItem>(sql);
					}

					lock (_locker)
					{
						operations.AddRange(currentOperations);
						Logger.Debug("Выкачено {CurrentOperationsCount} ЕО из {MaxPackagesCount}", operations.Count, unitsCount);
					}
				});

				using (Logger.TimeOperation("Сортировка по Кадастровому кварталу"))
				{
					operations = operations.OrderBy(x => x.CadastralDistrict).ToList();
				}

				Logger.Debug("Начато формирование таблиц");
				var dataSet = new DataSet();
				var itemTable = GetItemDataTable(operations);
				dataSet.Tables.Add(itemTable);
				Logger.Debug("Закончено формирование таблиц");

				return dataSet;
			}
		}


		#region Support Methods

		private ICadastralCostDeterminationResultsReport GetReport(NameValueCollection query)
		{
			Type type;
			var reportType = GetQueryParam<string>("ReportType", query);
			switch (reportType)
			{
				case "Результаты определения кадастровой стоимости":
					type = typeof(StateResultsReport);
					break;
				case "Сведения о результатах определения КС ОН, КС которых определен индивидуально":
					type = typeof(IndividuallyResultsReport);
					break;
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}

			if (!_reportsDictionary.TryGetValue(type, out var concreteReport))
			{
				concreteReport = (ICadastralCostDeterminationResultsReport)Activator.CreateInstance(type);
				_reportsDictionary[type] = concreteReport;
			}

			return concreteReport;
		}

		private DataTable GetItemDataTable(List<ReportItem> operations)
		{
			var dataTable = new DataTable("ITEM");

			dataTable.Columns.Add("Number");
			dataTable.Columns.Add("CadastralDistrict");
			dataTable.Columns.Add("CadastralNumber");
			dataTable.Columns.Add("Type");
			dataTable.Columns.Add("Square");
			dataTable.Columns.Add("Upks");
			dataTable.Columns.Add("Cost");

			for (var i = 0; i < operations.Count; i++)
			{
				dataTable.Rows.Add(i + 1,
					operations[i].CadastralDistrict,
					operations[i].CadastralNumber,
					operations[i].Type,
					operations[i].Square,
					operations[i].Upks,
					operations[i].Cost);
			}

			return dataTable;
		}

		#endregion


		#region Entities

		private class ReportItem
		{
			public string CadastralDistrict { get; set; }
			public string CadastralNumber { get; set; }
			public string Type { get; set; }
			public decimal? Square { get; set; }
			public decimal? Upks { get; set; }
			public decimal? Cost { get; set; }
		}

		#endregion
	}
}
