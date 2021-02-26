using System;
using System.Collections.Generic;
using Core.Register;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.LongProcess.Reports.ResultsByCadastralDistrict.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.ResultsByCadastralDistrict
{
	public class ResultsByCadastralDistrictForZuReportLongProcess : ALinearReportsLongProcessTemplate<ResultsByCadastralDistrictForZuReportLongProcess.ReportItem, InputParametersForZu>
	{
		protected override string ReportName => "Результаты в разрезе КР (ЗУ)";
		protected override string ProcessName => nameof(ResultsByCadastralDistrictForZuReportLongProcess);
		protected RosreestrRegisterService RosreestrRegisterService { get; set; }
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }


		public ResultsByCadastralDistrictForZuReportLongProcess() : base(Log.ForContext<ResultsByCadastralDistrictForZuReportLongProcess>())
		{
			RosreestrRegisterService = new RosreestrRegisterService();
		}


		protected override bool AreInputParametersValid(InputParametersForZu inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0 &&
			       inputParameters.InfoAboutExistenceOfOtherObjectsAttributeId != 0 &&
			       inputParameters.InfoSourceAttributeId != 0 &&
			       inputParameters.SegmentAttributeId != 0 &&
			       inputParameters.UsageTypeCodeAttributeId != 0 &&
			       inputParameters.UsageTypeNameAttributeId != 0 &&
			       inputParameters.UsageTypeCodeSourceAttributeId != 0;
		}

		protected override void PrepareVariables(InputParametersForZu inputParameters)
		{
			BaseSql = GetBaseSql(inputParameters);
			TaskIdsStr = string.Join(',', inputParameters.TaskIds);

			BaseUnitsCondition = $@" where unit.TASK_ID IN ({TaskIdsStr}) AND 
										(unit.PROPERTY_TYPE_CODE = 4 and unit.OBJECT_ID is not null) ";
		}

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 200000;
			var defaultThreadsCount = 3;

			return GetProcessConfigFromSettings("ResultsByCadastralDistrictForZu", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(InputParametersForZu inputParameters)
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

		protected override Func<ReportItem, string> GetSortingCondition()
		{
			return x => x.CadastralNumber;
		}

		protected override string GenerateReportTitle()
		{
			return "Состав данных о результатах кадастровой оценки по характеристикам объектов недвижимости и с присвоенными группами и кодами видов расчета";
		}

		protected override List<Column> GenerateReportHeaders()
		{
			var columns = new List<Column>
			{
				new Column {Header = "№ п/п", Width = 3},
				new Column {Header = "Кадастровый номер", Width = ColumnWidthForCadastralNumber},
				new Column {Header = "Кадастровый квартал", Width = 6}, 
				new Column {Header = "Кадастровый район", Width = 3},
				new Column {Header = "Тип объекта", Width = 4},
				new Column {Header = "Площадь", Width = 4},
				new Column {Header = "Наименование земельного участка", Width = 4},
				new Column {Header = "Местоположение", Width = 6},
				new Column {Header = "Адрес", Width = 6},
				new Column {Header = "Дата образования", Width = ColumnWidthForDates},
				new Column {Header = "Категория земель", Width = 4},
				new Column {Header = "Вид использования по документам", Width = 4},
				new Column {Header = "Вид использования по классификатору", Width = 4},
				new Column {Header = "Сведения о нахождении на земельном участке других связанных с ним объектов недвижимости", Width = 4},
				new Column {Header = "Источник информации", Width = 4},
				new Column {Header = "Сегмент", Width = 4},
				new Column {Header = "Код вида использования", Width = 4},
				new Column {Header = "Наименование вида использования", Width = 4},
				new Column {Header = "Источник информации кода вида использования", Width = 4},
				new Column {Header = "Номер подгруппы", Width = 4},
				new Column {Header = "УПКС объекта недвижимости, руб./кв.м.", Width = 4},
				new Column {Header = "Кадастровая стоимость объекта недвижимости, руб.", Width = 4}
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
				item.CadastralQuartal,
				item.CadastralDistrict,
				item.ObjectType,
				item.Square,
				item.ParcelName,
				item.Location,
				item.Address,
				ProcessDate(item.FormationDate),
				item.ParcelCategory,
				item.TypeOfUseByDocuments,
				item.TypeOfUseByClassifier,
				item.InfoAboutExistenceOfOtherObjects,
				item.InfoSource,
				item.Segment,
				item.UsageTypeCode,
				item.UsageTypeName,
				item.UsageTypeCodeSource,
				item.SubGroupNumber,
				item.Upks,
				item.CadastralCost
			};
		}



		#region Support Methods

		private string GetBaseSql(InputParametersForZu inputParameters)
		{
			var tourId = GetTourFromTasks(inputParameters.TaskIds);

			var baseFolderWithSql = "ResultsByCadastralDistrict";
			var sql = StatisticalDataService.GetSqlFileContent(baseFolderWithSql, "ParcelsForLongProcess");

			var parcelName = RosreestrRegisterService.GetParcelNameAttribute();
			var location = RosreestrRegisterService.GetLocationAttribute();
			var address = RosreestrRegisterService.GetAddressAttribute();
			var formationDate = RosreestrRegisterService.GetFormationDateAttribute();
			var parcelCategory = RosreestrRegisterService.GetParcelCategoryAttribute();
			var typeOfUseByDocuments = RosreestrRegisterService.GetTypeOfUseByDocumentsAttribute();
			var typeOfUseByClassifier = RosreestrRegisterService.GetTypeOfUseByClassifierAttribute();

			var infoAboutExistenceOfOtherObjects = RegisterCache.GetAttributeData(inputParameters.InfoAboutExistenceOfOtherObjectsAttributeId);
			var infoSource = RegisterCache.GetAttributeData(inputParameters.InfoSourceAttributeId);
			var segment = RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId);
			var usageTypeCode = RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeAttributeId);
			var usageTypeName = RegisterCache.GetAttributeData(inputParameters.UsageTypeNameAttributeId);
			var usageTypeCodeSource = RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeSourceAttributeId);

			var objectType = StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId);
			var cadastralQuartal = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
			var subGroupNumber = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);

			var sqlWithParameters = string.Format(sql, "{0}", parcelName.Id, location.Id,
				address.Id, formationDate.Id, parcelCategory.Id, typeOfUseByDocuments.Id, typeOfUseByClassifier.Id,
				infoAboutExistenceOfOtherObjects.Id, infoSource.Id, segment.Id, usageTypeCode.Id, usageTypeName.Id,
				usageTypeCodeSource.Id, objectType.Id, cadastralQuartal.Id, subGroupNumber.Id);

			return sqlWithParameters;
		}

		#endregion


		#region Entities

		public class ReportItem : InfoFromTourSettings
		{
			//From Unit
			public string CadastralNumber { get; set; }
			public string CadastralDistrict { get; set; }
			public decimal? Square { get; set; }
			public decimal? Upks { get; set; }
			public decimal? CadastralCost { get; set; }

			//From Rosreestr
			public string ParcelName { get; set; }
			public string Location { get; set; }
			public string Address { get; set; }
			public string FormationDate { get; set; }
			public string ParcelCategory { get; set; }
			public string TypeOfUseByDocuments { get; set; }

			//From Tour Settings


			//From UI
			public string TypeOfUseByClassifier { get; set; }
			public string InfoAboutExistenceOfOtherObjects { get; set; }
			public string InfoSource { get; set; }
			public string Segment { get; set; }
			public string UsageTypeCode { get; set; }
			public string UsageTypeName { get; set; }
			public string UsageTypeCodeSource { get; set; }
		}

		#endregion
	}
}
