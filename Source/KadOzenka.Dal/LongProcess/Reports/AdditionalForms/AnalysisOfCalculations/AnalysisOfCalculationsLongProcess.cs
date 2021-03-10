using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.AdditionalForms.AnalysisOfCalculations.Entities;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.AdditionalForms.AnalysisOfCalculations
{
	public class AnalysisOfCalculationsLongProcess: ALinearReportsLongProcessTemplate<ReportItem, ReportLongProcessOnlyTasksInputParameters>
	{
		private readonly string _reportSqlFileName = "AdditionalForms_CalculationAnalysis";
		private readonly GbuCodRegisterService _gbuCodRegisterService;
		private readonly RosreestrRegisterService _rosreestrRegisterService;
		private List<long> _taskIdList;
		private string UnitsConditionToCount { get; set; }
		public static readonly int PrecisionForDecimalValues = 2;
		public static readonly string DateFormat = "dd.MM.yyyy";


		public AnalysisOfCalculationsLongProcess() : base(Log.ForContext<AnalysisOfCalculationsLongProcess>())
		{
			_gbuCodRegisterService = new GbuCodRegisterService();
			_rosreestrRegisterService = new RosreestrRegisterService();
		}

		protected override bool AreInputParametersValid(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			return inputParameters?.TaskIds?.Count > 0;
		}

		protected override string ReportName  => "Анализ расчетов";
		protected override string ProcessName => nameof(AnalysisOfCalculationsLongProcess);

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 200000;
			var defaultThreadsCount = 4;

			return GetProcessConfigFromSettings("AnalysisOfCalculationsReport", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			return GetMaxUnitsCount(UnitsConditionToCount);
		}

		protected override List<Column> GenerateReportHeaders()
		{
			var columns = new List<Column>
			{
				new Column{Header = "№ п/п"},
				new Column{Header = "Кадастровый номер"},
				new Column{Header = "Тип"},
				new Column{Header = "Площадь"},
				new Column{Header = "Наименование (для ОКС)/Вид использование (для ЗУ)", Width = 6},
				new Column{Header = "Назначение"},
				new Column{Header = "Адрес"},
				new Column{Header = "Местоположение"},
				new Column{Header = "Оценочная подгруппа 2018", Width = 6},
				new Column{Header = "УПКС 2018"},
				new Column{Header = "КС 2018"},
				new Column{Header = "Кадастровый квартал 2018"},
				new Column{Header = "Тип(годовая, ежедневная, обращение)"},
				new Column{Header = "Оценочная подгруппа", Width = 6},
				new Column{Header = "УПКС"},
				new Column{Header = "КС"},
				new Column{Header = "Кадастровый квартал"},
				new Column{Header = "Дата изменения в ЕГРН"},
				new Column{Header = "Статус"},
				new Column{Header = "Изменения"},
				new Column{Header = "Минимальное УПКС по КК"},
				new Column{Header = "Среднее УПКС по КК"},
				new Column{Header = "Максимальное УПКС по КК"},
				new Column{Header = "Минимальное УПКС по Зоне"},
				new Column{Header = "Среднее УПКС по Зоне"},
				new Column{Header = "Максимальное УПКС по Зоне"},
				new Column{Header = "Количество раз участие в оценке"},
				new Column{Header = "Количество в годовом"},
				new Column{Header = "Количество в ежедневках"},
			};

			var counter = 0;
			columns.ForEach(x => x.Index = counter++);

			return columns;
		}

		protected override string GenerateReportTitle()
		{
			return "Анализ расчетов";
		}

		protected override List<MergedColumns> GenerateReportMergedHeaders()
		{
			return new List<MergedColumns>
			{
				new MergedColumns
				{
					OrderNumber = 1,
					StartColumnIndex = 0,
					EndColumnIndex = 6,
					Text = "Характеристики ОН"
				},
				new MergedColumns
				{
					OrderNumber = 1,
					StartColumnIndex = 7,
					EndColumnIndex = 10,
					Text = "Отчет ГКО 2018"
				},
				new MergedColumns
				{
					OrderNumber = 1,
					StartColumnIndex = 11,
					EndColumnIndex = 18,
					Text = "ВУОН"
				},
				new MergedColumns
				{
					OrderNumber = 1,
					StartColumnIndex = 19,
					EndColumnIndex = 25,
					Text = "Справочная информация"
				},
				new MergedColumns
				{
					OrderNumber = 1,
					StartColumnIndex = 26,
					EndColumnIndex = 28,
					Text = "Участие в оценке"
				}

			};
		}

		protected override List<object> GenerateReportReportRow(int index, ReportItem item)
		{
			return new List<object>
			{
				(index + 1).ToString(),
				item.CadastralNumber,
				item.TypeEnum.GetEnumDescription(),
				item.RosreestrSquareValue,
				item.ObjectNameTypeOfUse,
				item.Purpose,
				item.Address,
				item.Location,
				item.EvaluationSubgroup2018,
				(item.Upks2018.HasValue
					? Math.Round(item.Upks2018.Value, PrecisionForDecimalValues)
					: (decimal?)null),
				item.CadastralCost2018,
				item.CadastralQuartal2018,
				item.TaskTypeEnum.GetEnumDescription(),
				item.EvaluationSubgroup,
				(item.Upks.HasValue
					? Math.Round(item.Upks.Value, PrecisionForDecimalValues)
					: (decimal?)null),
				item.CadastralCost,
				item.CadastralQuartal,
				item.EGRNChangeDate.HasValue ? item.EGRNChangeDate.Value.ToString(DateFormat) : null,
				item.StatusEnum.GetEnumDescription(),
				item.Changes,
				(item.MinUpksByCadastralQuartal.HasValue
					? Math.Round(item.MinUpksByCadastralQuartal.Value, PrecisionForDecimalValues)
					: (decimal?)null),
				(item.AverageUpksByCadastralQuartal.HasValue
					? Math.Round(item.AverageUpksByCadastralQuartal.Value, PrecisionForDecimalValues)
					: (decimal?)null),
				(item.MaxUpksByCadastralQuartal.HasValue
					? Math.Round(item.MaxUpksByCadastralQuartal.Value, PrecisionForDecimalValues)
					: (decimal?)null),
				(item.MinUpksByZone.HasValue
					? Math.Round(item.MinUpksByZone.Value, PrecisionForDecimalValues)
					: (decimal?)null),
				(item.AverageUpksByZone.HasValue
					? Math.Round(item.AverageUpksByZone.Value, PrecisionForDecimalValues)
					: (decimal?)null),
				(item.MaxUpksByZone.HasValue
					? Math.Round(item.MaxUpksByZone.Value, PrecisionForDecimalValues)
					: (decimal?)null),
				item.ParticipatingCount,
				item.CountInYear,
				item.CountInDays
			};
		}

		protected override void PrepareVariables(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			_taskIdList = inputParameters.TaskIds;

			UnitsConditionToCount = $"where TASK_ID in ({string.Join(", ", _taskIdList)}) and property_type_code<>2190";
		}

		protected override string GetSql(int packageIndex, int packageSize)
		{
			string contents;
			using (var sr = new StreamReader(StatisticalDataService.GetSqlQueryFileStream(_reportSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", _taskIdList),
				_gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id,
				_rosreestrRegisterService.GetSquareAttribute().Id,
				_rosreestrRegisterService.GetObjectNameAttribute().Id,
				_rosreestrRegisterService.GetTypeOfUseByDocumentsAttribute().Id,
				_rosreestrRegisterService.GetBuildingPurposeAttribute().Id,
				_rosreestrRegisterService.GetPlacementPurposeAttribute().Id,
				_rosreestrRegisterService.GetConstructionPurposeAttribute().Id,
				_rosreestrRegisterService.GetAddressAttribute().Id,
				_rosreestrRegisterService.GetLocationAttribute().Id,
				packageSize,
				packageIndex
			);

			return sql;
		}
	}
}