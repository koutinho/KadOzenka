//using System;
//using System.Collections.Generic;
//using System.Linq;
//using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
//using Serilog;
//using KadOzenka.Dal.LongProcess.Reports.Entities;
//using KadOzenka.Dal.Registers.GbuRegistersServices;
//using Microsoft.Practices.EnterpriseLibrary.Data;

//TODO эксперементальный код через материализованное представление
//namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Reports
//{
//	public class OksReportLongProcess : ALinearReportsLongProcessTemplate<OksReportLongProcess.ReportItem, ReportLongProcessOnlyTasksInputParameters>
//	{
//		protected override string ReportName => "Состав данных по перечню объектов недвижимости (ОКС)";
//		protected override string ProcessName => nameof(OksReportLongProcess);
//		protected RosreestrRegisterService RosreestrRegisterService { get; set; }
//		private string TaskIdsStr { get; set; }
//		private string BaseUnitsCondition { get; set; }
//		private string BaseSql { get; set; }
//		private string MaterializedViewName => "oks_report_mat_view";


//		public OksReportLongProcess() : base(Log.ForContext<OksReportLongProcess>())
//		{
//			Logger.Debug("Process via Materialized view");
//			RosreestrRegisterService = new RosreestrRegisterService();
//		}


//		protected override bool AreInputParametersValid(ReportLongProcessOnlyTasksInputParameters inputParameters)
//		{
//			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0;
//		}

//		protected override void PrepareVariables(ReportLongProcessOnlyTasksInputParameters inputParameters)
//		{
//			BaseSql = GetBaseSql(inputParameters);
//			TaskIdsStr = string.Join(',', inputParameters.TaskIds);

//			BaseUnitsCondition = $@" where unit.TASK_ID IN ({TaskIdsStr}) AND 
//										unit.PROPERTY_TYPE_CODE <> 4 AND unit.PROPERTY_TYPE_CODE<>2190 AND 
//										unit.OBJECT_ID IS NOT NULL ";

//			var sql = $@"
//DROP MATERIALIZED VIEW IF EXISTS {MaterializedViewName};
//CREATE MATERIALIZED VIEW {MaterializedViewName} (
//    cadastralnumber,
//    square,
//    costvalue,
//    datevaluation,
//    dateentering,
//    dateapproval,
//    docnumber,
//    docdate,
//    docname,
//    applicationdate,
//    revisalstatementdate,
//    commissioningyear,
//    buildyear,
//    formationdate,
//    undergroundfloorsnumber,
//    floorsnumber,
//    wallmaterial,
//    location,
//    address,
//    purpose,
//    objectname,
//    objecttype,
//    cadastralquartal,
//    subgroupnumber)
//AS

//with object_ids as (
// select unit.object_id from ko_unit unit  where unit.TASK_ID IN ({TaskIdsStr}) AND 
//										unit.PROPERTY_TYPE_CODE <> 4 AND unit.PROPERTY_TYPE_CODE<>2190 AND 
//										unit.OBJECT_ID IS NOT NULL 
//										order by unit.id 
//),
//--Rosreestr
//commissioningYearAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 16)
//),
//buildYearAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 15)
//),
//formationDateAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 13)
//),
//undergroundFloorsNumberAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 18)
//),
//floorsNumberAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 17)
//),
//wallMaterialAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 21)
//),
//locationAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 8)
//),
//addressAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 600)
//),
//buildingPurposeAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 14)
//),
//placementPurposeAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 23)
//),
//constructionPurposeAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 22)
//),
//objectNameAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 19)
//),


//--Tour
//objectTypeAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 603)
//),
//cadastralQuartalAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 601)
//),
//subGroupNumberAttrValues as (
//	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 44404322)
//)


//SELECT 
//	unit.CADASTRAL_NUMBER AS CadastralNumber,
//	unit.SQUARE AS Square,
//	costRosreesrt.COSTVALUE AS CostValue,
//	costRosreesrt.DATEVALUATION AS DateValuation,
//	costRosreesrt.DATEENTERING AS DateEntering,
//	costRosreesrt.DATEAPPROVAL AS DateApproval,
//	costRosreesrt.DOCNUMBER AS DocNumber,
//	costRosreesrt.DOCDATE AS DocDate,
//	costRosreesrt.DOCNAME AS DocName,
//	costRosreesrt.APPLICATIONDATE AS ApplicationDate,
//	costRosreesrt.REVISALSTATEMENTDATE AS RevisalStatementDate,
//    --Rosreestr
//    commissioningYearAttr.attributeValue as CommissioningYear,
//    buildYearAttr.attributeValue as BuildYear,
//    formationDateAttr.attributeValue as FormationDate,
//    undergroundFloorsNumberAttr.attributeValue as UndergroundFloorsNumber,
//    floorsNumberAttr.attributeValue as FloorsNumber,
//    wallMaterialAttr.attributeValue as WallMaterial,
//    locationAttr.attributeValue as Location,
//    addressAttr.attributeValue as Address,
//    (case when unit.PROPERTY_TYPE_CODE = 5 then buildingPurposeAttr.attributeValue
//			when unit.PROPERTY_TYPE_CODE = 6 then placementPurposeAttr.attributeValue
//            when unit.PROPERTY_TYPE_CODE = 7 then constructionPurposeAttr.attributeValue
//			else null end) as Purpose,	 
//    objectNameAttr.attributeValue as ObjectName,
//    --Tour
//    objectTypeAttr.attributeValue as ObjectType,
//    cadastralQuartalAttr.attributeValue as CadastralQuartal,
//    subGroupNumberAttr.attributeValue as SubGroupNumber
//FROM KO_UNIT unit
// 	LEFT JOIN KO_COST_ROSREESTR costRosreesrt ON (unit.ID = costRosreesrt.ID_OBJECT)
//     --Rosreestr
//    LEFT JOIN commissioningYearAttrValues commissioningYearAttr ON unit.object_id=commissioningYearAttr.objectId
//    LEFT JOIN buildYearAttrValues buildYearAttr ON unit.object_id=buildYearAttr.objectId
//    LEFT JOIN formationDateAttrValues formationDateAttr ON unit.object_id=formationDateAttr.objectId
//    LEFT JOIN undergroundFloorsNumberAttrValues undergroundFloorsNumberAttr ON unit.object_id=undergroundFloorsNumberAttr.objectId
//    LEFT JOIN floorsNumberAttrValues floorsNumberAttr ON unit.object_id=floorsNumberAttr.objectId
//    LEFT JOIN wallMaterialAttrValues wallMaterialAttr ON unit.object_id=wallMaterialAttr.objectId
//    LEFT JOIN locationAttrValues locationAttr ON unit.object_id=locationAttr.objectId
//    LEFT JOIN addressAttrValues addressAttr ON unit.object_id=addressAttr.objectId
//    LEFT JOIN buildingPurposeAttrValues buildingPurposeAttr ON unit.object_id=buildingPurposeAttr.objectId
//    LEFT JOIN placementPurposeAttrValues placementPurposeAttr ON unit.object_id=placementPurposeAttr.objectId
//    LEFT JOIN constructionPurposeAttrValues constructionPurposeAttr ON unit.object_id=constructionPurposeAttr.objectId
//    LEFT JOIN objectNameAttrValues objectNameAttr ON unit.object_id=objectNameAttr.objectId
//    --Tour
//    LEFT JOIN objectTypeAttrValues objectTypeAttr ON unit.object_id=objectTypeAttr.objectId
//    LEFT JOIN cadastralQuartalAttrValues cadastralQuartalAttr ON unit.object_id=cadastralQuartalAttr.objectId
//    LEFT JOIN subGroupNumberAttrValues subGroupNumberAttr ON unit.object_id=subGroupNumberAttr.objectId
// where unit.TASK_ID IN ({TaskIdsStr}) AND 
//										unit.PROPERTY_TYPE_CODE <> 4 AND unit.PROPERTY_TYPE_CODE<>2190 AND 
//										unit.OBJECT_ID IS NOT NULL 
//										order by unit.id;";

//			Logger.Debug("Начато создание материального представления", new Exception(sql));
			
//			var command = DBMngr.Main.GetSqlStringCommand(sql);
//			DBMngr.Main.ExecuteNonQuery(command);

//			Logger.Debug("Закончено создание материального представления");
//		}

//		protected override ReportsConfig GetProcessConfig()
//		{
//			var defaultPackageSize = 4500;
//			var defaultThreadsCount = 3;

//			return GetProcessConfigFromSettings("PricingFactorsCompositionForOks", defaultPackageSize, defaultThreadsCount);
//		}

//		protected override int GetMaxItemsCount(ReportLongProcessOnlyTasksInputParameters inputParameters)
//		{
//			return GetMaxUnitsCount(BaseUnitsCondition);
//		}

//		protected override string GetSql(int packageIndex, int packageSize)
//		{
//			return $"select * from {MaterializedViewName} limit {packageSize} offset {packageIndex * packageSize}";

//			var unitsCondition = $@"{BaseUnitsCondition}
//										--order by unit.id 
//										limit {packageSize} offset {packageIndex * packageSize}";

//			return string.Format(BaseSql, unitsCondition);
//		}

//		protected override Func<IEnumerable<ReportItem>, IEnumerable<ReportItem>> FuncForDownloadedItems()
//		{
//			return x => x.OrderBy(y => y.CadastralNumber);
//		}

//		protected override List<Column> GenerateReportHeaders()
//		{
//			var columns = new List<Column>
//			{
//				new Column {Header = "№ п/п", Width = 2},
//				new Column {Header = "КН", Width = ColumnWidthForCadastralNumber},
//				new Column {Header = "Год ввода в эксплуатацию", Width = ColumnWidthForDates},
//				new Column {Header = "Год постройки", Width = ColumnWidthForDates},
//				new Column {Header = "Дата образования", Width = ColumnWidthForDates},
//				new Column {Header = "Количество подземных этажей"},
//				new Column {Header = "Количество этажей"},
//				new Column {Header = "Материал стен"},
//				new Column {Header = "Местоположение", Width = ColumnWidthForAddresses},
//				new Column {Header = "Адрес", Width = ColumnWidthForAddresses},
//				new Column {Header = "Назначение здания"},
//				new Column {Header = "Наименование объекта"},
//				new Column {Header = "Площадь"},
//				new Column {Header = "Тип объекта"},
//				new Column {Header = "Кадастровый квартал"},
//				new Column {Header = "Значение кадастровой стоимости"},
//				new Column {Header = "Дата определения кадастровой стоимости", Width = ColumnWidthForDates},
//				new Column {Header = "Дата внесения сведений о кадастровой стоимости в ЕГРН", Width = ColumnWidthForDates},
//				new Column {Header = "Дата утверждения кадастровой стоимости", Width = ColumnWidthForDates},
//				new Column {Header = "Номер акта об утверждении кадастровой стоимости"},
//				new Column {Header = "Дата акта об утверждении кадастровой стоимости", Width = ColumnWidthForDates},
//				new Column {Header = "Наименование документа об утверждении кадастровой стоимости"},
//				new Column {Header = "Дата начала применения кадастровой стоимости", Width = ColumnWidthForDates},
//				new Column {Header = "Дата подачи заявления о пересмотре кадастровой стоимости", Width = ColumnWidthForDates}
//			};

//			var counter = 0;
//			columns.ForEach(x => x.Index = counter++);

//			return columns;
//		}

//		protected override List<object> GenerateReportReportRow(int index, ReportItem item)
//		{
//			return new List<object>
//			{
//				(index + 1).ToString(),
//				item.CadastralNumber,
//				item.CommissioningYear,
//				item.BuildYear,
//				ProcessDate(item.FormationDate),
//				item.UndergroundFloorsNumber,
//				item.FloorsNumber,
//				item.WallMaterial,
//				item.Location,
//				item.Address,
//				item.Purpose,
//				item.ObjectName,
//				item.Square,
//				item.ObjectType,
//				item.CadastralQuartal,
//				item.CostValue,
//				item.DateValuation,
//				item.DateEntering,
//				item.DateApproval,
//				item.DocNumber,
//				item.DocDate,
//				item.DocName,
//				item.ApplicationDate,
//				item.RevisalStatementDate
//			};
//		}



//		#region Support Methods

//		private string GetBaseSql(ReportLongProcessOnlyTasksInputParameters parameters)
//		{
//			var tourId = GetTourFromTasks(parameters.TaskIds);

//			var baseFolderWithSql = "PricingFactorsComposition";
//			var sql = StatisticalDataService.GetSqlFileContent(baseFolderWithSql, "OksForLongProcess");

//			var commissioningYear = RosreestrRegisterService.GetCommissioningYearAttribute();
//			var buildYear = RosreestrRegisterService.GetBuildYearAttribute();
//			var formationDate = RosreestrRegisterService.GetFormationDateAttribute();
//			var undergroundFloorsNumber = RosreestrRegisterService.GetUndergroundFloorsNumberAttribute();
//			var floorsNumber = RosreestrRegisterService.GetFloorsNumberAttribute();
//			var wallMaterial = RosreestrRegisterService.GetWallMaterialAttribute();
//			var location = RosreestrRegisterService.GetLocationAttribute();
//			var address = RosreestrRegisterService.GetAddressAttribute();
//			var buildingPurpose = RosreestrRegisterService.GetBuildingPurposeAttribute();
//			var placementPurpose = RosreestrRegisterService.GetPlacementPurposeAttribute();
//			var constructionPurpose = RosreestrRegisterService.GetConstructionPurposeAttribute();
//			var objectName = RosreestrRegisterService.GetObjectNameAttribute();

//			var objectType = StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId);
//			var cadastralQuartal = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
//			var subGroupNumber = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);

//			var sqlWithParameters = string.Format(sql, "{0}", commissioningYear.Id,
//				buildYear.Id, formationDate.Id, undergroundFloorsNumber.Id, floorsNumber.Id, wallMaterial.Id, location.Id,
//				address.Id, buildingPurpose.Id, placementPurpose.Id, constructionPurpose.Id, objectName.Id,
//				objectType.Id, cadastralQuartal.Id, subGroupNumber.Id);

//			return sqlWithParameters;
//		}

//		#endregion


//		#region Entities

//		public class ReportItem : InfoFromTourSettings
//		{
//			//From Unit
//			public string CadastralNumber { get; set; }
//			public decimal? Square { get; set; }

//			//From Rosreestr
//			public string CommissioningYear { get; set; }
//			public string BuildYear { get; set; }
//			public string FormationDate { get; set; }
//			public string UndergroundFloorsNumber { get; set; }
//			public string FloorsNumber { get; set; }
//			public string WallMaterial { get; set; }
//			public string Location { get; set; }
//			public string Address { get; set; }
//			public string Purpose { get; set; }
//			public string ObjectName { get; set; }

//			//From Tour Settings


//			//From KO.CostRosreestr (KO_COST_ROSREESTR)
//			/// <summary>
//			/// Значение кадастровой стоимости
//			/// </summary>
//			public decimal? CostValue { get; set; }
//			/// <summary>
//			/// Дата определения кадастровой стоимости
//			/// </summary>
//			public DateTime? DateValuation { get; set; }
//			/// <summary>
//			/// Дата внесения сведений о кадастровой стоимости в ЕГРН
//			/// </summary>
//			public DateTime? DateEntering { get; set; }
//			/// <summary>
//			/// Дата утверждения кадастровой стоимости
//			/// </summary>
//			public DateTime? DateApproval { get; set; }
//			/// <summary>
//			/// Номер акта об утверждении кадастровой стоимости
//			/// </summary>
//			public string DocNumber { get; set; }
//			/// <summary>
//			/// Дата акта об утверждении кадастровой стоимости
//			/// </summary>
//			public DateTime? DocDate { get; set; }
//			/// <summary>
//			/// Наименование документа об утверждении кадастровой стоимости
//			/// </summary>
//			public string DocName { get; set; }
//			/// <summary>
//			/// Дата начала применения кадастровой стоимости
//			/// </summary>
//			public DateTime? ApplicationDate { get; set; }
//			/// <summary>
//			/// Дата подачи заявления о пересмотре кадастровой стоимости
//			/// </summary>
//			public DateTime? RevisalStatementDate { get; set; }
//		}

//		#endregion
//	}
//}
