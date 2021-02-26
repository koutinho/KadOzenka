using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using ObjectModel.Directory;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports
{
	public class InfoAboutCadastralCostDeterminingMethodReportLongProcess : ALinearReportsLongProcessTemplate<InfoAboutCadastralCostDeterminingMethodReportLongProcess.ReportItem, ReportLongProcessOnlyTasksInputParameters>
	{
		protected override string ReportName => "Сведения о способе определения кадастровой стоимости";
		protected override string ProcessName => nameof(InfoAboutCadastralCostDeterminingMethodReportLongProcess);
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }


		public InfoAboutCadastralCostDeterminingMethodReportLongProcess() : base(Log.ForContext<InfoAboutCadastralCostDeterminingMethodReportLongProcess>())
		{
		}


		protected override bool AreInputParametersValid(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0;
		}

		protected override void PrepareVariables(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			BaseSql = GetBaseSql();
			TaskIdsStr = string.Join(',', inputParameters.TaskIds);

			BaseUnitsCondition = $@" where unit.TASK_ID IN ({TaskIdsStr}) AND 
										unit.PROPERTY_TYPE_CODE <> 2190 ";
		}

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 400000;
			var defaultThreadsCount = 4;

			return GetProcessConfigFromSettings("InfoAboutCadastralCostDeterminingMethod", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			return GetMaxUnitsCount(BaseUnitsCondition);
		}

		protected override string GetSql(int packageIndex, int packageSize)
		{
			return $@"{BaseSql} 
						{BaseUnitsCondition}
						order by unit.id 
						limit {packageSize} offset {packageIndex * packageSize}";
		}

		protected override Func<IEnumerable<ReportItem>, IEnumerable<ReportItem>> FuncForDownloadedItems()
		{
			return x => x.OrderBy(y => y.CadastralNumber);
		}

		protected override List<Column> GenerateReportHeaders()
		{
			var columns = new List<Column>
			{
				new Column {Header = "№ п/п", Width = 2},
				new Column {Header = "Тип"},
				new Column {Header = "Кадастровый номер", Width = ColumnWidthForCadastralNumber},
				new Column {Header = "Подгруппа", Width = 6},
				new Column {Header = "Способ", Width = 6},
				new Column {Header = "Подход", Width = 6},
				new Column {Header = "Метод оценки", Width = 6},
				new Column {Header = "Модель", Width = 6}
			};

			var counter = 0;
			columns.ForEach(x => x.Index = counter++);

			return columns;
		}

		protected override List<object> GenerateReportReportRow(int index, ReportItem item)
		{
			return new List<object>
			{
				(index + 1).ToString(),
				item.ObjectPropertyType == PropertyTypes.None
					? null
					: item.ObjectPropertyType.GetEnumDescription(),
				item.CadastralNumber,
				item.FullGroupName,
				item.ModelingWay.GetEnumDescription(),
				item.ModelCalculationType == KoCalculationType.None
					? null
					: item.ModelCalculationType.GetEnumDescription(),
				item.ModelCalculationMethod == KoCalculationMethod.None
					? null
					: item.ModelCalculationMethod.GetEnumDescription(),
				item.ModelName
			};
		}



		#region Support Methods

		private string GetBaseSql()
		{
			return $@"SELECT 
						unit.PROPERTY_TYPE_CODE AS {nameof(ReportItem.ObjectPropertyType)},
						unit.CADASTRAL_NUMBER AS {nameof(ReportItem.CadastralNumber)},
						groups.NUMBER || '. ' || groups.GROUP_NAME AS {nameof(ReportItem.FullGroupName)},
						model.CALCULATION_TYPE AS {nameof(ReportItem.ModelCalculationType)},
						model.CALCULATION_METHOD AS {nameof(ReportItem.ModelCalculationMethod)},
						model.NAME AS {nameof(ReportItem.ModelName)}
					FROM KO_UNIT unit
						LEFT JOIN KO_GROUP groups ON unit.GROUP_ID = groups.ID
						LEFT JOIN KO_MODEL model ON unit.GROUP_ID = model.GROUP_ID and model.IS_ACTIVE = 1 ";
		}

		#endregion


		#region Entities

		public class ReportItem
		{
			public PropertyTypes ObjectPropertyType { get; set; }
			public string CadastralNumber { get; set; }
			public string FullGroupName { get; set; }

			public KoModelingWay ModelingWay =>
				ModelCalculationMethod == KoCalculationMethod.IndividualCalculation
					? KoModelingWay.IndividualCalculation
					: KoModelingWay.MassCalculation;

			public KoCalculationType ModelCalculationType { get; set; }
			public KoCalculationMethod ModelCalculationMethod { get; set; }
			public string ModelName { get; set; }
		}

		#endregion
	}
}
