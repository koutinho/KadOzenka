using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using ModelingBusiness.Model;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Reports
{
    //запрос для подбора данных для тестирования лежит в файле previous_tours_test_data.sql
    public class PreviousToursReportProcess : ALinearReportsLongProcessTemplate<PreviousTourReportItem, PreviousToursReportInputParameters>
    {
        private readonly ExcelFile _excelFile;
        private readonly ExcelWorksheet _mainWorkSheet;
        private int _currentRowIndex;
        private PreviousToursService PreviousToursService { get; }
        private FactorsService FactorsService { get; }
        private ModelService ModelService { get; }
        protected override string ReportName => "Состав данных о результатах кадастровой оценки предыдущих туров";
        protected override string ProcessName => nameof(PreviousToursReportProcess);
        private List<FactorsService.PricingFactors> GroupedFactors { get; set; }
        private List<FactorsService.Attribute> AllAttributes { get; set; }
        private List<long> TaskIds { get; set; }
        private string TaskIdsStr { get; set; }
        private string BaseUnitsCondition { get; set; }
        private string BaseSql { get; set; }
        private long GroupId { get; set; }
        private long? ModelId { get; set; }
        protected override long ReportCode => (long)StatisticalDataType.PricingFactorsCompositionForPreviousTours;



        public PreviousToursReportProcess() : base(Log.ForContext<PreviousToursReportProcess>())
        {
	        PreviousToursService = new PreviousToursService();
            FactorsService = new FactorsService();
            ModelService = new ModelService();

            _excelFile = new ExcelFile();
            _mainWorkSheet = _excelFile.Worksheets.Add("Лист 1");
        }

        protected override bool AreInputParametersValid(PreviousToursReportInputParameters inputParameters)
        {
	        return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0 && inputParameters.GroupId != 0;
        }

        protected override void PrepareVariables(PreviousToursReportInputParameters inputParameters)
        {
	        TaskIds = inputParameters.TaskIds;
            TaskIdsStr = string.Join(',', inputParameters.TaskIds);

	        GroupId = inputParameters.GroupId;
	        ModelId = ModelService.GetActiveModelEntityByGroupId(GroupId)?.Id;

            GroupedFactors = ModelId == null
		        ? new List<FactorsService.PricingFactors>()
		        : FactorsService.GetGroupedModelFactors(ModelId.Value, QueryManager);
            AllAttributes = GroupedFactors.SelectMany(x => x.Attributes).OrderBy(x => x.Name).ToList();

	        BaseUnitsCondition = $@" where unit.task_id IN ({TaskIdsStr}) and unit.group_id = {GroupId} and unit.PROPERTY_TYPE_CODE <> 2190 ";

            BaseSql = GetBaseSql();
        }

        protected override ReportsConfig GetProcessConfig()
        {
	        var defaultPackageSize = 200000;
	        var defaultThreadsCount = 2;

	        return GetProcessConfigFromSettings("PreviousTours", defaultPackageSize, defaultThreadsCount);
        }

        protected override int GetMaxItemsCount(PreviousToursReportInputParameters inputParameters)
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

        protected override Func<IEnumerable<PreviousTourReportItem>, IEnumerable<PreviousTourReportItem>> FuncForDownloadedItems()
        {
	        return x => x.OrderBy(y => y.CadastralNumber).ThenBy(x => x.TourYear);
        }

        protected override List<PreviousTourReportItem> GetReportItems(string sql)
        {
	        var dataTable = QueryManager.ExecuteSqlStringToDataSet(sql).Tables[0];

            var items = new List<PreviousTourReportItem>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new PreviousTourReportItem
                {
                    CadastralNumber = row[nameof(PreviousTourReportItem.CadastralNumber)].ParseToStringNullable(),
                    Square = row[nameof(PreviousTourReportItem.Square)].ParseToDecimalNullable(),
                    CadastralCost = row[nameof(PreviousTourReportItem.CadastralCost)].ParseToDecimalNullable(),
                    TourYear = row[nameof(PreviousTourReportItem.TourYear)].ParseToLongNullable(),
                    ResultName = row[nameof(PreviousTourReportItem.ResultName)].ParseToStringNullable(),
                    ResultPurpose = row[nameof(PreviousTourReportItem.ResultPurpose)].ParseToStringNullable(),
                    PermittedUse = row[nameof(PreviousTourReportItem.PermittedUse)].ParseToStringNullable(),
                    Address = row[nameof(PreviousTourReportItem.Address)].ParseToStringNullable(),
                    Location = row[nameof(PreviousTourReportItem.Location)].ParseToStringNullable(),
                    ParentCadastralNumberForOks = row[nameof(PreviousTourReportItem.ParentCadastralNumberForOks)].ParseToStringNullable(),
                    BuildYear = row[nameof(PreviousTourReportItem.BuildYear)].ParseToStringNullable(),
                    CommissioningYear = row[nameof(PreviousTourReportItem.CommissioningYear)].ParseToStringNullable(),
                    FloorsNumber = row[nameof(PreviousTourReportItem.FloorsNumber)].ParseToStringNullable(),
                    UndergroundFloorsNumber = row[nameof(PreviousTourReportItem.UndergroundFloorsNumber)].ParseToStringNullable(),
                    WallMaterial = row[nameof(PreviousTourReportItem.WallMaterial)].ParseToStringNullable(),
                    ObjectType = row[nameof(PreviousTourReportItem.ObjectType)].ParseToStringNullable(),
                    CadastralQuartal = row[nameof(PreviousTourReportItem.CadastralQuartal)].ParseToStringNullable(),
                    SubGroupNumber = row[nameof(PreviousTourReportItem.SubGroupNumber)].ParseToStringNullable(),
                    Factors = FactorsService.ProcessModelFactors(row, AllAttributes)
                };

                items.Add(item);
            }

            return items;
        }

        protected override string GenerateReportTitle()
        {
	        throw new NotImplementedException();
        }

        protected override List<Column> GenerateReportHeaders()
        {
	        throw new NotImplementedException();
        }

        protected override List<object> GenerateReportReportRow(int index, PreviousTourReportItem item)
        {
	       throw new NotImplementedException();
        }


        protected override MemoryStream GetReportStream(List<PreviousTourReportItem> reportItems)
        {
	        var tourYears = GetToursByTasks(TaskIds).OrderBy(x => x.Year).Select(x => x.Year.ToString()).ToList();
	        var pricingFactorNames = AllAttributes.Select(x => x.Name).ToList();
	        
	        GenerateReportHeader(tourYears, pricingFactorNames);
	        
	        GenerateReportBody(reportItems, tourYears, pricingFactorNames);

	        var lastUsedColumnIndex = _mainWorkSheet.CalculateMaxUsedColumns();
	        for (var i = 0; i < lastUsedColumnIndex; i++)
	        {
		        _mainWorkSheet.Columns[i].SetWidth(4, LengthUnit.Centimeter);
            }

	        //попытка принудительно освободить память
            reportItems = null;
	        GC.Collect();

	        var stream = new MemoryStream();
	        _excelFile.Save(stream, SaveOptions.XlsxDefault);
	        stream.Seek(0, SeekOrigin.Begin);

	        return stream;
        }


        #region Support Methods

        private string GetBaseSql()
        {
            var sqlFileContent = StatisticalDataService.GetSqlFileContent("PricingFactorsComposition", "PreviousToursForLongProcess");

            var rosreestrRegisterService = new RosreestrRegisterService();

            var oksName = rosreestrRegisterService.GetObjectNameAttribute();
            var zuName = rosreestrRegisterService.GetParcelNameAttribute();
            var buildingPurpose = rosreestrRegisterService.GetBuildingPurposeAttribute();
            var placementPurpose = rosreestrRegisterService.GetPlacementPurposeAttribute();
            var constructionPurpose = rosreestrRegisterService.GetConstructionPurposeAttribute();
            var permittedUse = rosreestrRegisterService.GetTypeOfUseByDocumentsAttribute();
            var address = rosreestrRegisterService.GetAddressAttribute();
            var location = rosreestrRegisterService.GetLocationAttribute();
            var parentCadastralNumberForOks = rosreestrRegisterService.GetParcelAttribute();
            var buildYear = rosreestrRegisterService.GetBuildYearAttribute();
            var commissioningYear = rosreestrRegisterService.GetCommissioningYearAttribute();
            var floorsNumber = rosreestrRegisterService.GetFloorsNumberAttribute();
            var undergroundFloorsNumber = rosreestrRegisterService.GetUndergroundFloorsNumberAttribute();
            var wallMaterial = rosreestrRegisterService.GetWallMaterialAttribute();

            var sqlForModelFactors = FactorsService.GetSqlForModelFactors(ModelId, GroupedFactors);
            if (!string.IsNullOrWhiteSpace(sqlForModelFactors.Columns))
                sqlForModelFactors.Columns = $", {sqlForModelFactors.Columns}";

            var sqlWithParameters = string.Format(sqlFileContent, "{0}", GroupId, oksName.Id,
                zuName.Id, buildingPurpose.Id, placementPurpose.Id, constructionPurpose.Id, permittedUse.Id, address.Id,
                location.Id, parentCadastralNumberForOks.Id, buildYear.Id, commissioningYear.Id, floorsNumber.Id,
                undergroundFloorsNumber.Id, wallMaterial.Id, sqlForModelFactors.Columns, sqlForModelFactors.Tables);

            return sqlWithParameters;
        }

        private List<OMTour> GetToursByTasks(List<long> taskIds)
        {
            return OMTask.Where(x => taskIds.Contains(x.Id))
                .Select(x => new
                {
                    x.ParentTour.Id,
                    x.ParentTour.Year
                })
                .OrderBy(x => x.ParentTour.Year)
                .Execute()
                .Select(x => x.ParentTour)
                .DistinctBy(x => x.Id)
                .ToList();
        }

        private void GenerateReportHeader(List<string> tourYears, List<string> pricingFactorNames)
        {
			var columnsWithoutTourSeparation = new List<string>
			{
				"№ п/п",
				"Кадастровый номер"
			};

			var allColumnTitles = new List<string>();
			allColumnTitles.AddRange(columnsWithoutTourSeparation);
			allColumnTitles.AddRange(PreviousToursService.ColumnTitles);
			allColumnTitles.AddRange(pricingFactorNames);

			//для колонки с № п/п и КН  не нужны подзаголовки с турами
			var tourColumns = new List<string>();
			tourColumns.AddRange(columnsWithoutTourSeparation);
			tourColumns.AddRange(Enumerable
				.Repeat(tourYears, allColumnTitles.Count - columnsWithoutTourSeparation.Count)
				.SelectMany(x => x.ToList()).ToList());

			AddTitle("Таблица. Состав данных о результатах кадастровой оценки предыдущих туров", allColumnTitles.Count * tourYears.Count);
			AddHeaders(allColumnTitles, tourYears.Count, columnsWithoutTourSeparation.Count);
			AddRow(tourColumns, true);
		}

        private void AddTitle(string title, int allColumnsCount)
        {
	        var startColumnIndex = 0;
            var endColumnIndex = startColumnIndex + allColumnsCount - 1;

            _mainWorkSheet.Rows[_currentRowIndex].Cells[startColumnIndex].SetValue(title);

            var cells = _mainWorkSheet.Cells.GetSubrangeAbsolute(_currentRowIndex, startColumnIndex, _currentRowIndex, endColumnIndex);
            cells.Merged = true;
            cells.Style = GetCellStyle(true);

            _currentRowIndex++;
        }

        private void AddHeaders(List<string> titles, int mergedColumnsCount, int numberOfColumnsWithoutTourSeparation)
        {
            mergedColumnsCount--;
            var endColumnIndex = -1;
            foreach (var title in titles)
            {
                endColumnIndex++;
                var startColumnIndex = endColumnIndex;
                var startRowIndex = _currentRowIndex;
                var endRowIndex = startRowIndex;

                _mainWorkSheet.Rows[_currentRowIndex].Cells[startColumnIndex].SetValue(title);

                //оставляем первые N ячеек необъединенными по горизонтали, но объединенными по вертикали
                if (startColumnIndex < numberOfColumnsWithoutTourSeparation)
                {
                    endColumnIndex = startColumnIndex;
                    endRowIndex++;
                }
                else
                {
                    endColumnIndex = startColumnIndex + mergedColumnsCount;
                }

                var cells = _mainWorkSheet.Cells.GetSubrangeAbsolute(startRowIndex, startColumnIndex, endRowIndex, endColumnIndex);
                cells.Merged = true;
                cells.Style = GetCellStyle(true);
            }

            _currentRowIndex++;
        }

        private void AddRow(List<string> values, bool isHeaderCellStyle = false)
        {
            for (var i = 0; i < values.Count; i++)
            {
                var cells = _mainWorkSheet.Rows[_currentRowIndex].Cells[i];
                cells.SetValue(values[i]);
                cells.Style = GetCellStyle(isHeaderCellStyle);
            }

            _currentRowIndex++;
        }

        private CellStyle GetCellStyle(bool isHeaderCellStyle = false)
        {
            var style = new CellStyle
            {
                HorizontalAlignment = HorizontalAlignmentStyle.Center,
                VerticalAlignment = VerticalAlignmentStyle.Center,
                WrapText = true
            };
            style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

            if (isHeaderCellStyle)
	            style.Font.Weight = ExcelFont.BoldWeight;

            return style;
        }

        /// <summary>
        /// Построчная генерация отчета
        /// </summary>
        private void GenerateReportBody(List<PreviousTourReportItem> items, List<string> tourYears, List<string> pricingFactorNames)
        {
            var index = 1;
            var groupedReportItems = items.GroupBy(x => x.CadastralNumber).ToList();
            groupedReportItems.ForEach(groupedItem =>
            {
                var rowValues = new List<string> { index.ToString(), groupedItem.Key };

                PreviousToursService.ColumnTitles.ForEach(title =>
                {
                    tourYears.ForEach(tourYear =>
                    {
                        var itemInTour = groupedItem.FirstOrDefault(x => x.TourYear?.ToString() == tourYear);
                        rowValues.Add(PreviousToursService.GetValueForReportItem(title, itemInTour)?.ToString());
                    });
                });

                pricingFactorNames.ForEach(factorName =>
                {
                    tourYears.ForEach(tourYear =>
                    {
                        var itemInTour = groupedItem.FirstOrDefault(x => x.TourYear?.ToString() == tourYear);
                        var itemFactor = itemInTour?.Factors?.FirstOrDefault(x => x.Name == factorName);
                        rowValues.Add(itemFactor?.Value);
                    });
                });

                index++;
                AddRow(rowValues);
            });
        }

        #endregion
    }
}
