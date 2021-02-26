using System;
using System.Collections.Generic;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using Serilog;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Reports
{
	public class ZuReportLongProcess : ALinearReportsLongProcessTemplate<ZuReportLongProcess.ReportItem, ReportLongProcessOnlyTasksInputParameters>
	{
		protected override string ReportName => "Состав данных по перечню объектов недвижимости (ЗУ)";
		protected override string ProcessName => nameof(ZuReportLongProcess);
		protected RosreestrRegisterService RosreestrRegisterService { get; set; }
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }


		public ZuReportLongProcess() : base(Log.ForContext<ZuReportLongProcess>())
		{
			RosreestrRegisterService = new RosreestrRegisterService();
		}


		protected override bool AreInputParametersValid(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0;
		}

		protected override void PrepareVariables(ReportLongProcessOnlyTasksInputParameters inputParameters)
		{
			BaseSql = GetBaseSql(inputParameters);
			TaskIdsStr = string.Join(',', inputParameters.TaskIds);

			BaseUnitsCondition = $@" where unit.TASK_ID IN ({TaskIdsStr}) AND 
										unit.PROPERTY_TYPE_CODE = 4 AND 
										unit.OBJECT_ID IS NOT NULL ";
		}

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 200000;
			var defaultThreadsCount = 4;

			return GetProcessConfigFromSettings("PricingFactorsCompositionForZu", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(ReportLongProcessOnlyTasksInputParameters inputParameters,
			QueryManager queryManager)
		{
			return GetMaxUnitsCount(BaseUnitsCondition, queryManager);
		}

		protected override string GetSql(int packageIndex, int packageSize)
		{
			var unitsCondition = $@"{BaseUnitsCondition}
										order by unit.id 
										limit {packageSize} offset {packageIndex * packageSize}";

			return string.Format(BaseSql, unitsCondition);
		}

		protected override Func<ReportItem, string> GetSortingCondition()
		{
			return x => x.CadastralNumber;
		}

		protected override string GenerateReportTitle()
		{
			return "Состав данных по перечню объектов недвижимости, подлежащих государственной кадастровой оценке (земельные участки)";
		}

		protected override List<GbuReportService.Column> GenerateReportHeaders()
		{
			var columns = new List<GbuReportService.Column>
			{
				new GbuReportService.Column {Header = "№ п/п", Width = 4},
				new GbuReportService.Column {Header = "КН", Width = 6},
				new GbuReportService.Column {Header = "Вид использования по документам", Width = 6},
				new GbuReportService.Column {Header = "Вид использования по классификатору", Width = 6},
				new GbuReportService.Column {Header = "Дата образования", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Категория земель", Width = 6},
				new GbuReportService.Column {Header = "Местоположение", Width = 6},
				new GbuReportService.Column {Header = "Адрес", Width = 8},
				new GbuReportService.Column {Header = "Наименование земельного участка", Width = 6},
				new GbuReportService.Column {Header = "Площадь", Width = 4},
				new GbuReportService.Column {Header = "Тип объекта", Width = 6},
				new GbuReportService.Column {Header = "Кадастровый квартал", Width = 6},
				new GbuReportService.Column {Header = "Значение кадастровой стоимости", Width = 6},
				new GbuReportService.Column {Header = "Дата определения кадастровой стоимости", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Дата внесения сведений о кадастровой стоимости в ЕГРН", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Дата утверждения кадастровой стоимости", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Номер акта об утверждении кадастровой стоимости", Width = 6},
				new GbuReportService.Column {Header = "Дата акта об утверждении кадастровой стоимости", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Наименование документа об утверждении кадастровой стоимости", Width = 6},
				new GbuReportService.Column {Header = "Дата начала применения кадастровой стоимости", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Дата подачи заявления о пересмотре кадастровой стоимости", Width = ColumnWidthForDates}
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
				item.CadastralNumber,
				item.TypeOfUseByDocuments,
				item.TypeOfUseByClassifier,
				item.FormationDate,
				item.ParcelCategory,
				item.Location,
				item.Address,
				item.ParcelName,
				item.Square,
				item.ObjectType,
				item.CadastralQuartal,
				item.CostValue,
				item.DateValuation,
				item.DateEntering,
				item.DateApproval,
				item.DocNumber,
				item.DocDate,
				item.DocName,
				item.ApplicationDate,
				item.RevisalStatementDate
			};
		}



		#region Support Methods
		
		private string GetBaseSql(ReportLongProcessOnlyTasksInputParameters parameters)
		{
			var tourId = GetTourFromTasks(parameters.TaskIds);

			var baseFolderWithSql = "PricingFactorsComposition";
			var sql = StatisticalDataService.GetSqlFileContent(baseFolderWithSql, "ZuForLongProcess");

			var typeOfUseByDocuments = RosreestrRegisterService.GetTypeOfUseByDocumentsAttribute();
			var typeOfUseByClassifier = RosreestrRegisterService.GetTypeOfUseByClassifierAttribute();
			var formationDate = RosreestrRegisterService.GetFormationDateAttribute();
			var parcelCategory = RosreestrRegisterService.GetParcelCategoryAttribute();
			var location = RosreestrRegisterService.GetLocationAttribute();
			var address = RosreestrRegisterService.GetAddressAttribute();
			var parcelName = RosreestrRegisterService.GetParcelNameAttribute();

			var objectType = StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId);
			var cadastralQuartal = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
			var subGroupNumber = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);

			var sqlWithParameters = string.Format(sql, "{0}", typeOfUseByDocuments.Id,
				typeOfUseByClassifier.Id, formationDate.Id, parcelCategory.Id, location.Id, address.Id, parcelName.Id,
				objectType.Id, cadastralQuartal.Id, subGroupNumber.Id);

			return sqlWithParameters;
		}

		#endregion


		#region Entities

		public class ReportItem : InfoFromTourSettings
		{
			//From Unit
			public string CadastralNumber { get; set; }
			public decimal? Square { get; set; }

			//From KO.CostRosreestr (KO_COST_ROSREESTR)
			/// <summary>
			/// Значение кадастровой стоимости
			/// </summary>
			public decimal? CostValue { get; set; }
			/// <summary>
			/// Дата определения кадастровой стоимости
			/// </summary>
			public DateTime? DateValuation { get; set; }
			/// <summary>
			/// Дата внесения сведений о кадастровой стоимости в ЕГРН
			/// </summary>
			public DateTime? DateEntering { get; set; }
			/// <summary>
			/// Дата утверждения кадастровой стоимости
			/// </summary>
			public DateTime? DateApproval { get; set; }
			/// <summary>
			/// Номер акта об утверждении кадастровой стоимости
			/// </summary>
			public string DocNumber { get; set; }
			/// <summary>
			/// Дата акта об утверждении кадастровой стоимости
			/// </summary>
			public DateTime? DocDate { get; set; }
			/// <summary>
			/// Наименование документа об утверждении кадастровой стоимости
			/// </summary>
			public string DocName { get; set; }
			/// <summary>
			/// Дата начала применения кадастровой стоимости
			/// </summary>
			public DateTime? ApplicationDate { get; set; }
			/// <summary>
			/// Дата подачи заявления о пересмотре кадастровой стоимости
			/// </summary>
			public DateTime? RevisalStatementDate { get; set; }

			//From Rosreestr
			public string TypeOfUseByDocuments { get; set; }
			public string TypeOfUseByClassifier { get; set; }
			public string FormationDate { get; set; }
			public string ParcelCategory { get; set; }
			public string Location { get; set; }
			public string Address { get; set; }
			public string ParcelName { get; set; }

			//From Tour Settings
		}

		#endregion
	}
}
