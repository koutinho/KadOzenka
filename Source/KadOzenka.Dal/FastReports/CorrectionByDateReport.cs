using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using Core.UI.Registers.Reports.Model;
using Platform.Reports;
using EP.Morph;
using KadOzenka.Dal.Correction;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.FastReports
{
	public class CorrectionByDateReport : FastReportBase
    {
        private CorrectionByDateInput _input;
        public CorrectionByDateService CorrectionByDateService { get; set; }

        public CorrectionByDateReport()
        {
            CorrectionByDateService = new CorrectionByDateService();
        }

        static CorrectionByDateReport()
		{
            Morphology.Initialize(MorphLang.RU);
		}
        
        protected override string TemplateName(NameValueCollection query)
        {
            return nameof(CorrectionByDateReport);
        }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
		{
            if (!initialisation)
                return;

            var quartalFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "Quartal");
            if (quartalFilterValue == null)
                return;

            quartalFilterValue.ReportParameters = new List<ReportParameter>();
            var quartals = OMQuartalDictionary.Where(x => true).SelectAll().Execute();
            quartalFilterValue.ReportParameters.Add(new ReportParameter { Value = string.Empty, Key = string.Empty});
            quartalFilterValue.ReportParameters.AddRange(quartals.Select(x => new ReportParameter { Value = x.CadastralQuartal, Key = x.CadastralQuartal }));
        }

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            SetFilterValues(query);

            var operations = GetOperations();

            var dataSet = new DataSet();
            dataSet.Tables.Add(GetCommonDataTable());
            dataSet.Tables.Add(GetItemDataTable(operations));

            return dataSet;
        }


        #region Support Methods

        private void SetFilterValues(NameValueCollection query)
        {
            var city = GetQueryParam<string>("City", query);
            var district = GetQueryParam<string>("District", query) ?? string.Empty;
            var districtCode = EnumExtensions.GetEnumByDescription<Hunteds>(district);
            var neighborhood = GetQueryParam<string>("Neighborhood", query) ?? string.Empty;
            var neighborhoodCode = EnumExtensions.GetEnumByDescription<Districts>(neighborhood);
            var quartal = GetQueryParam<string>("Quartal", query);
            var segments = GetQueryParam<string>("Segment", query) ?? string.Empty;
            var segmentCodes = segments.Split(';').Select(EnumExtensions.GetEnumByDescription<MarketSegment>).Where(x => x != 0).ToList();

            var date = GetQueryParam<DateTime?>("ReportDate", query);
            if (date?.Month >= DateTime.Today.AddMonths(1).Month)
                throw new Exception($"Максимальное значение даты: {DateTime.Today.ToString(Correction.Consts.DateFormatForDateCorrection)}");

            _input = new CorrectionByDateInput(city, districtCode, neighborhoodCode, quartal, date, segmentCodes);
        }

        private DataTable GetCommonDataTable()
        {
            var dataTable = new DataTable("Common");

            dataTable.Columns.Add("Date");

            dataTable.Rows.Add(_input.Date?.ToString(Correction.Consts.DateFormatForDateCorrection));

            return dataTable;
        }

        private DataTable GetItemDataTable(List<CorrectionByDateItem> operations)
        {
            var dataTable = new DataTable("Item");

            dataTable.Columns.Add("CadastralNumber");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("DealType");
            dataTable.Columns.Add("PropertyMarketSegment");
            dataTable.Columns.Add("Price");
            dataTable.Columns.Add("PriceAfterCorrectionByDate");
            dataTable.Columns.Add("ParserDate");
            dataTable.Columns.Add("LastUpdateDate");
            dataTable.Columns.Add("ProcessType");
            dataTable.Columns.Add("District");
            dataTable.Columns.Add("Neighborhood");
            dataTable.Columns.Add("CadastralQuartal");

            foreach (var operation in operations)
            {
                dataTable.Rows.Add(operation.CadastralNumber,
                    operation.Address,
                    operation.DealType,
                    operation.PropertyMarketSegment,
                    operation.Price,
                    operation.PriceAfterCorrectionByDate,
                    operation.ParserDate?.ToString(Correction.Consts.DateFormatForDateCorrection),
                    operation.LastUpdateDate?.ToString(Correction.Consts.DateFormatForDateCorrection),
                    operation.ProcessType,
                    operation.District,
                    operation.Neighborhood,
                    operation.CadastralQuartal);
            }

            return dataTable;
        }

        private List<CorrectionByDateItem> GetOperations()
        {
            var marketObjects = GetObjects();

            var inflationRate = GetInflationRate(_input.Date);

            var result = new List<CorrectionByDateItem>();
            marketObjects.ForEach(x =>
            {
                result.Add(new CorrectionByDateItem
                {
                    CadastralNumber = x.CadastralNumber,
                    Address = x.Address,
                    DealType = x.DealType,
                    PropertyMarketSegment = x.PropertyMarketSegment,
                    Price = x.Price,
                    PriceAfterCorrectionByDate = CalculatePriceAfterCorrectionByDate(x, inflationRate),
                    ParserDate = x.ParserTime,
                    LastUpdateDate = x.LastDateUpdate,
                    ProcessType = x.ProcessType,
                    District = x.District,
                    Neighborhood = x.Neighborhood,
                    CadastralQuartal = x.CadastralQuartal
                });
            });
            
            return result;
        }

        private List<OMCoreObject> GetObjects()
        {
           var objects = OMCoreObject.Where(x => (x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal)
                                           && (x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed)
                                           && (string.IsNullOrWhiteSpace(_input.City) || x.City.ToLower().Contains(_input.City))
                                           && (_input.DistrictCode == 0 || x.District_Code == (Hunteds)_input.DistrictCode)
                                           && (_input.NeighborhoodCode == 0 || x.Neighborhood_Code == (Districts)_input.NeighborhoodCode))
               //todo remove 
               .SetPackageSize(100).SetPackageIndex(0)
               .SelectAll().Execute();

           return objects.Where(x => (string.IsNullOrWhiteSpace(_input.Quartal) || x.CadastralQuartal == _input.Quartal)
                                     && (_input.SegmentCodes.Count == 0 || _input.SegmentCodes.Where(s => s != 0)
                                             .Select(s => (MarketSegment) s).Contains(x.PropertyMarketSegment_Code))).ToList();
        }

        private decimal? GetInflationRate(DateTime? date)
        {
            if(date == null)
                return null;

            var indexes = OMIndexesForDateCorrection.Where(x => x.Date < date).SelectAll()
                .OrderByDescending(x => x.Date).Execute();
            indexes.Insert(0, CorrectionByDateService.GetDefaultNewConsumerIndex(date.Value));

           CorrectionByDateService.RecalculateConsumerPriceIndexes(indexes);

           return indexes.Sum(x => x.ConsumerPriceIndex.GetValueOrDefault());
        }

        private decimal CalculatePriceAfterCorrectionByDate(OMCoreObject obj, decimal? inflationRate)
        {
            return 0;
        }

        #endregion

        #region Entities

        private class CorrectionByDateItem
        {
            public string CadastralNumber { get; set; }
            public string Address { get; set; }
            public string PropertyMarketSegment { get; set; }
            public decimal? Price { get; set; }
            public decimal? PriceAfterCorrectionByDate { get; set; }
            public string DealType { get; set; }
            public DateTime? LastUpdateDate { get; set; }
            public DateTime? ParserDate { get; set; }
            public string ProcessType { get; set; }
            public string District { get; set; }
            public string Neighborhood { get; set; }
            public string CadastralQuartal { get; set; }
        }

        private class CorrectionByDateInput
        {
            public string City { get; }
            public long DistrictCode { get; }
            public long NeighborhoodCode { get; }
            public string Quartal { get; }
            public DateTime? Date { get; }
            public List<long> SegmentCodes { get; }

            public CorrectionByDateInput(string city, long districtCode, long neighborhoodCode, string quartal,
                DateTime? date, List<long> segmentCodes)
            {
                City = city;
                DistrictCode = districtCode;
                NeighborhoodCode = neighborhoodCode;
                Quartal = quartal;
                Date = date;
                SegmentCodes = segmentCodes ?? new List<long>();
            }
        }


        #endregion
    }
}
