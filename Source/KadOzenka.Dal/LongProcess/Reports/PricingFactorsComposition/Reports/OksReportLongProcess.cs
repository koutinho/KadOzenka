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
	public class OksReportLongProcess : ALinearReportsLongProcessTemplate<OksReportLongProcess.ReportItem, ReportLongProcessOnlyTasksInputParameters>
	{
		protected override string ReportName => "Состав данных по перечню объектов недвижимости (ОКС)";
		protected override string ProcessName => nameof(OksReportLongProcess);
		protected RosreestrRegisterService RosreestrRegisterService { get; set; }
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }


		public OksReportLongProcess() : base(Log.ForContext<OksReportLongProcess>())
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
										unit.PROPERTY_TYPE_CODE <> 4 AND unit.PROPERTY_TYPE_CODE<>2190 AND 
										unit.OBJECT_ID IS NOT NULL ";
		}

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 200000;
			var defaultThreadsCount = 3;

			return GetProcessConfigFromSettings("PricingFactorsCompositionForOks", defaultPackageSize, defaultThreadsCount);
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

		protected override List<GbuReportService.Column> GenerateReportHeaders()
		{
			var columns = new List<GbuReportService.Column>
			{
				new GbuReportService.Column {Header = "№ п/п", Width = 4},
				new GbuReportService.Column {Header = "КН", Width = ColumnWidthForCadastralNumber},
				new GbuReportService.Column {Header = "Год ввода в эксплуатацию", Width = 5},
				new GbuReportService.Column {Header = "Год постройки", Width = 4}, 
				new GbuReportService.Column {Header = "Дата образования", Width = 4},
				new GbuReportService.Column {Header = "Количество подземных этажей", Width = 4},
				new GbuReportService.Column {Header = "Количество этажей", Width = 4},
				new GbuReportService.Column {Header = "Материал стен", Width = 4},
				new GbuReportService.Column {Header = "Местоположение", Width = ColumnWidthForAddresses},
				new GbuReportService.Column {Header = "Адрес", Width = ColumnWidthForAddresses},
				new GbuReportService.Column {Header = "Назначение здания", Width = 4},
				new GbuReportService.Column {Header = "Наименование объекта", Width = 4},
				new GbuReportService.Column {Header = "Площадь", Width = 4},
				new GbuReportService.Column {Header = "Тип объекта", Width = 4},
				new GbuReportService.Column {Header = "Кадастровый квартал", Width = 4},
				new GbuReportService.Column {Header = "Значение кадастровой стоимости", Width = 4},
				new GbuReportService.Column {Header = "Дата определения кадастровой стоимости", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Дата внесения сведений о кадастровой стоимости в ЕГРН", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Дата утверждения кадастровой стоимости", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Номер акта об утверждении кадастровой стоимости", Width = 4},
				new GbuReportService.Column {Header = "Дата акта об утверждении кадастровой стоимости", Width = ColumnWidthForDates},
				new GbuReportService.Column {Header = "Наименование документа об утверждении кадастровой стоимости", Width = 4},
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
				item.CommissioningYear,
				item.BuildYear,
				ProcessDate(item.FormationDate),
				item.UndergroundFloorsNumber,
				item.FloorsNumber,
				item.WallMaterial,
				item.Location,
				item.Address,
				item.Purpose,
				item.ObjectName,
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
			var sql = StatisticalDataService.GetSqlFileContent(baseFolderWithSql, "OksForLongProcess");

			var commissioningYear = RosreestrRegisterService.GetCommissioningYearAttribute();
			var buildYear = RosreestrRegisterService.GetBuildYearAttribute();
			var formationDate = RosreestrRegisterService.GetFormationDateAttribute();
			var undergroundFloorsNumber = RosreestrRegisterService.GetUndergroundFloorsNumberAttribute();
			var floorsNumber = RosreestrRegisterService.GetFloorsNumberAttribute();
			var wallMaterial = RosreestrRegisterService.GetWallMaterialAttribute();
			var location = RosreestrRegisterService.GetLocationAttribute();
			var address = RosreestrRegisterService.GetAddressAttribute();
			var buildingPurpose = RosreestrRegisterService.GetBuildingPurposeAttribute();
			var placementPurpose = RosreestrRegisterService.GetPlacementPurposeAttribute();
			var constructionPurpose = RosreestrRegisterService.GetConstructionPurposeAttribute();
			var objectName = RosreestrRegisterService.GetObjectNameAttribute();

			var objectType = StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId);
			var cadastralQuartal = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
			var subGroupNumber = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);

			var sqlWithParameters = string.Format(sql, "{0}", commissioningYear.Id,
				buildYear.Id, formationDate.Id, undergroundFloorsNumber.Id, floorsNumber.Id, wallMaterial.Id, location.Id,
				address.Id, buildingPurpose.Id, placementPurpose.Id, constructionPurpose.Id, objectName.Id,
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

			//From Rosreestr
			public string CommissioningYear { get; set; }
			public string BuildYear { get; set; }
			public string FormationDate { get; set; }
			public string UndergroundFloorsNumber { get; set; }
			public string FloorsNumber { get; set; }
			public string WallMaterial { get; set; }
			public string Location { get; set; }
			public string Address { get; set; }
			public string Purpose { get; set; }
			public string ObjectName { get; set; }

			//From Tour Settings


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
		}

		#endregion
	}
}
