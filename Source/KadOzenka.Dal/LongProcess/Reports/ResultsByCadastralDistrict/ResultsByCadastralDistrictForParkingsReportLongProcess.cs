using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.LongProcess.Reports.ResultsByCadastralDistrict.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.ResultsByCadastralDistrict
{
	public class ResultsByCadastralDistrictForParkingsReportLongProcess : ALinearReportsLongProcessTemplate<ResultsByCadastralDistrictForParkingsReportLongProcess.ReportItem, InputParametersForParkings>
	{
		protected override string ReportName => "Результаты в разрезе КР (Машино-места)";
		protected override string ProcessName => nameof(ResultsByCadastralDistrictForParkingsReportLongProcess);
		protected RosreestrRegisterService RosreestrRegisterService { get; set; }
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }
		protected override long ReportCode => (long)StatisticalDataType.ResultsByKRForParking;



		public ResultsByCadastralDistrictForParkingsReportLongProcess() : base(Log.ForContext<ResultsByCadastralDistrictForParkingsReportLongProcess>())
		{
			RosreestrRegisterService = new RosreestrRegisterService();
		}


		protected override bool AreInputParametersValid(InputParametersForParkings inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0 &&
			       inputParameters.SegmentAttributeId != 0 &&
			       inputParameters.UsageTypeNameAttributeId != 0 &&
			       inputParameters.UsageTypeCodeAttributeId != 0 &&
			       inputParameters.UsageTypeCodeSourceAttributeId != 0 &&
			       inputParameters.SubGroupUsageTypeCodeAttributeId != 0 &&
			       inputParameters.FunctionalSubGroupNameAttributeId != 0;
		}

		protected override void PrepareVariables(InputParametersForParkings inputParameters)
		{
			BaseSql = GetBaseSql(inputParameters);
			TaskIdsStr = string.Join(',', inputParameters.TaskIds);

			BaseUnitsCondition = $@" where unit.TASK_ID IN ({TaskIdsStr}) AND 
										(unit.PROPERTY_TYPE_CODE = 11 and unit.OBJECT_ID is not null) ";
		}

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 200000;
			var defaultThreadsCount = 3;

			return GetProcessConfigFromSettings("ResultsByCadastralDistrictForParkings", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(InputParametersForParkings inputParameters)
		{
			return GetMaxUnitsCount(BaseUnitsCondition);
		}

		protected override string GetSql(int packageIndex, int packageSize)
		{
			var unitsCondition = $@"{BaseUnitsCondition}
										order by unit.id 
										limit {packageSize} offset {packageIndex * packageSize}";

			return string.Format(BaseSql, unitsCondition);
		}

		protected override Func<IEnumerable<ReportItem>, IEnumerable<ReportItem>> FuncForDownloadedItems()
		{
			return x => x.OrderBy(y => y.CadastralNumber);
		}

		protected override string GenerateReportTitle()
		{
			return "Состав данных о результатах кадастровой оценки по характеристикам объектов недвижимости и с присвоенными группами и кодами видов расчета";
		}

		protected override List<Column> GenerateReportHeaders()
		{
			var columns = new List<Column>
			{
				new Column {Header = "№ п/п", Width = 2},
				new Column {Header = "Кадастровый номер", Width = ColumnWidthForCadastralNumber},
				new Column {Header = "Год ввода в эксплуатацию", Width = ColumnWidthForDates},
				new Column {Header = "Год постройки", Width = ColumnWidthForDates},
				new Column {Header = "Количество подземных этажей"},
				new Column {Header = "Количество этажей"},
				new Column {Header = "Материал стен"},
				new Column {Header = "Местоположение", Width = ColumnWidthForAddresses},
				new Column {Header = "Адрес", Width = ColumnWidthForAddresses},
				new Column {Header = "Назначение здания или сооружения, в котором расположено помещение"},
				new Column {Header = "Кадастровый номер здания или сооружения, в котором расположено помещение"},
				new Column {Header = "Номер подгруппы здания или сооружения, в котором расположено помещение"},
				new Column {Header = "Назначение помещения"},
				new Column {Header = "Наименование объекта"},
				new Column {Header = "Площадь"},
				new Column {Header = "Тип объекта"},
				new Column {Header = "Этаж"},
				new Column {Header = "Кадастровый квартал ", Width = ColumnWidthForCadastralNumber},
				new Column {Header = "Сегмент"},
				new Column {Header = "Наименование вида использования"},
				new Column {Header = "Код вида использования"},
				new Column {Header = "Источник информации кода вида использования"},
				new Column {Header = "Код подгруппы вида использования"}, 
				new Column {Header = "Наименование функциональной подгруппы"},
				new Column {Header = "Номер подгруппы"},
				new Column {Header = "УПКС объекта недвижимости, руб./кв.м."},
				new Column {Header = "Кадастровая стоимость объекта недвижимости, руб."}
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
				item.UndergroundFloorsNumber,
				item.FloorsNumber,
				item.WallMaterial,
				item.Location,
				item.Address,
				item.ParentPurpose,
				item.ParentCadastralNumber,
				item.ParentGroup,
				item.PlacementPurpose,
				item.ObjectName,
				item.Square,
				"Не задан тип территории",
				item.Floor,
				item.CadastralQuartal,
				item.Segment,
				item.UsageTypeName,
				item.UsageTypeCode,
				item.UsageTypeCodeSource,
				item.SubGroupUsageTypeCode,
				item.FunctionalSubGroupName,
				item.SubGroupNumber,
				item.Upks,
				item.CadastralCost
			};
		}



		#region Support Methods

		private string GetBaseSql(InputParametersForParkings inputParameters)
		{
			var tourId = GetTourFromTasks(inputParameters.TaskIds);

			var baseFolderWithSql = "ResultsByCadastralDistrict";
			var sql = StatisticalDataService.GetSqlFileContent(baseFolderWithSql, "ParkingsForLongProcess");

			var commissioningYear = RosreestrRegisterService.GetCommissioningYearAttribute();
			var buildYear = RosreestrRegisterService.GetBuildYearAttribute();
			var undergroundFloorsNumber = RosreestrRegisterService.GetUndergroundFloorsNumberAttribute();
			var floorsNumber = RosreestrRegisterService.GetFloorsNumberAttribute();
			var wallMaterial = RosreestrRegisterService.GetWallMaterialAttribute();
			var location = RosreestrRegisterService.GetLocationAttribute();
			var address = RosreestrRegisterService.GetAddressAttribute();
			var parentCadastralNumber = RosreestrRegisterService.GetParentCadastralNumberAttribute();
			var placementPurpose = RosreestrRegisterService.GetPlacementPurposeAttribute();
			var objectName = RosreestrRegisterService.GetObjectNameAttribute();
			var floor = RosreestrRegisterService.GetFloorAttribute();

			var segment = RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId);
			var usageTypeName = RegisterCache.GetAttributeData(inputParameters.UsageTypeNameAttributeId);
			var usageTypeCode = RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeAttributeId);
			var usageTypeCodeSource = RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeSourceAttributeId);
			var subGroupUsageTypeCode = RegisterCache.GetAttributeData(inputParameters.SubGroupUsageTypeCodeAttributeId);
			var functionalSubGroupName = RegisterCache.GetAttributeData(inputParameters.FunctionalSubGroupNameAttributeId);

			var cadastralQuartal = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
			var subGroupNumber = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);

			var buildingPurpose = RosreestrRegisterService.GetBuildingPurposeAttribute();
			var constructionPurpose = RosreestrRegisterService.GetConstructionPurposeAttribute();

			var sqlWithParameters = string.Format(sql, "{0}", commissioningYear.Id, buildYear.Id,
				undergroundFloorsNumber.Id, floorsNumber.Id, wallMaterial.Id, location.Id,
				address.Id, parentCadastralNumber.Id, placementPurpose.Id, objectName.Id,
				floor.Id, segment.Id, usageTypeName.Id, usageTypeCode.Id, usageTypeCodeSource.Id,
				subGroupUsageTypeCode.Id, functionalSubGroupName.Id, cadastralQuartal.Id,
				subGroupNumber.Id, buildingPurpose.Id, constructionPurpose.Id, subGroupNumber.Id);

			return sqlWithParameters;
		}

		#endregion


		#region Entities

		public class ReportItem : InfoFromTourSettings
		{
			//From Units
			public string CadastralNumber { get; set; }
			public decimal? Square { get; set; }
			public decimal? Upks { get; set; }
			public decimal? CadastralCost { get; set; }

			//From Rosreestr
			public string CommissioningYear { get; set; }
			public string BuildYear { get; set; }
			public string UndergroundFloorsNumber { get; set; }
			public string FloorsNumber { get; set; }
			public string WallMaterial { get; set; }
			public string Location { get; set; }
			public string Address { get; set; }
			public string ParentCadastralNumber { get; set; }
			public string PlacementPurpose { get; set; }
			public string ObjectName { get; set; }
			public string Floor { get; set; }

			//From Tour Settings


			//From UI
			public string Segment { get; set; }
			public string UsageTypeName { get; set; }
			public string UsageTypeCode { get; set; }
			public string UsageTypeCodeSource { get; set; }
			public string SubGroupUsageTypeCode { get; set; }
			public string FunctionalSubGroupName { get; set; }

			//From Parent
			public string ParentPurpose { get; set; }
			public string ParentGroup { get; set; }
		}

		#endregion
	}
}
