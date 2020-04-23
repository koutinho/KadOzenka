using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.Correction;
using KadOzenka.Dal.Correction.Requests;
using ObjectModel.Directory;
using ObjectModel.Market;
using Platform.Reports;
using Core.Shared.Extensions;
using DevExpress.DataProcessing;
using ObjectModel.Directory.MarketObjects;

namespace KadOzenka.Dal.FastReports
{
    public class CorrectionByBargainReport : FastReportBase
    {
        private readonly CorrectionSettingsService _correctionSettingsService = new CorrectionSettingsService();

        protected override string TemplateName(NameValueCollection query)
        {
            return nameof(CorrectionByBargainReport);
        }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
        {
            if (initialisation)
            {
                var cadastralQuarterFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "CadastralQuarter");
                if (cadastralQuarterFilterValue != null)
                {
                    cadastralQuarterFilterValue.ReportParameters = new List<ReportParameter>();
                    var omQuarters = OMQuartalDictionary.Where(x => true)
                        .Select(x => x.CadastralQuartal)
                        .Execute().Select(x => x.CadastralQuartal);

                    cadastralQuarterFilterValue.ReportParameters.Add(new ReportParameter { Value = string.Empty, Key = string.Empty });
                    cadastralQuarterFilterValue.ReportParameters.AddRange(omQuarters.Select(x => new ReportParameter { Value = $"{x}", Key = $"key:{x}" }));
                }
            }
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var dataTitleTable = new DataTable("Common");
            dataTitleTable.Columns.Add("Title");
            dataTitleTable.Rows.Add(GetReportTitle(query));

            var dataTable = new DataTable("Data");
            dataTable.Columns.Add("CadastralNumber");
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("MarketSegment");
            dataTable.Columns.Add("Price");
            dataTable.Columns.Add("BargainCoefficient");
            dataTable.Columns.Add("PriceAfterCorrectionByBargain");

            var request = GetCorrectionByBargainExportRequest(query);
            var settings = _correctionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByBargain);
            var correctionByBargainExport = new CorrectionByBargainExport(settings);
            var data = correctionByBargainExport.GetBargainCorrectionData(request);

            foreach (var bargainDto in data.Where(x => x.PriceAfterCorrectionByBargain.HasValue))
            {
                dataTable.Rows.Add(bargainDto.CadastralNumber,
                    bargainDto.Date?.ToShortDateString(),
                    bargainDto.Address,
                    bargainDto.MarketSegment.GetEnumDescription(),
                    bargainDto.Price,
                    bargainDto.BargainCoefficient,
                    bargainDto.PriceAfterCorrectionByBargain);
            }

            var dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            dataSet.Tables.Add(dataTitleTable);

            return dataSet;
        }

        private CorrectionByBargainExportRequest GetCorrectionByBargainExportRequest(NameValueCollection query)
        {
            var request = new CorrectionByBargainExportRequest();

            var useAllSegments = GetQueryParam<bool>("UseAllSegments", query);
            if (useAllSegments)
            {
                var segments = System.Enum.GetValues(typeof(MarketSegment)).Cast<MarketSegment>();
                request.MarketSegments = new HashSet<MarketSegment>(segments);
            }
            else
            {
                var segmentsStrings = (GetQueryParam<string>("Segments", query))?.Split(";");
                var segments = new List<MarketSegment>();
                if (segmentsStrings != null)
                    foreach (var segmentString in segmentsStrings.Where(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        segments.Add((MarketSegment) EnumExtensions.GetEnumByDescription<MarketSegment>(segmentString));
                    }

                request.MarketSegments = new HashSet<MarketSegment>(segments);
            }

            var coverageAreaTypeParam = GetQueryParam<string>("CoverageAreaType", query);
            request.CoverageAreaType = GetCoverageAreaTypeByName(coverageAreaTypeParam);

            var districtParam = GetQueryParam<string>("Districts", query);
            if (!string.IsNullOrEmpty(districtParam))
            {
                request.Distinct = (Hunteds) EnumExtensions.GetEnumByDescription<Hunteds>(districtParam);
            }

            var regionParam = GetQueryParam<string>("Regions", query);
            if (!string.IsNullOrEmpty(regionParam))
            {
                request.Region = (Districts) EnumExtensions.GetEnumByDescription<Districts>(regionParam);
            }

            var zoneParam = GetQueryParam<string>("Zones", query);
            if (!string.IsNullOrEmpty(zoneParam))
            {
                request.Zone = GetZoneByName(zoneParam);
            }

            request.CadastralQuarter = GetQueryParam<string>("CadastralQuarter", query);
            request.DateFrom = GetQueryParam<DateTime?>("DateFrom", query);
            request.DateTo = GetQueryParam<DateTime?>("DateTo", query);

            return request;
        }

        private string GetReportTitle(NameValueCollection query)
        {
            string title = "Корректировка на торг ";

            title += (GetQueryParam<DateTime?>("DateFrom", query)).HasValue
                ? $"c {GetQueryParam<DateTime?>("DateFrom", query).Value.ToShortDateString()} "
                : "";
            title += (GetQueryParam<DateTime?>("DateTo", query)).HasValue
                ? $"по {GetQueryParam<DateTime?>("DateTo", query).Value.ToShortDateString()} "
                : "";

            var useAllSegments = GetQueryParam<bool>("UseAllSegments", query);
            if (useAllSegments)
            {
                title += "по всем сегментам ";
            }
            else
            {
                var segmentsStrings = (GetQueryParam<string>("Segments", query))?.Split(";");
                if (segmentsStrings != null)
                    title +=
                        $"по сегментам {string.Join(", ", segmentsStrings.Where(x => !string.IsNullOrWhiteSpace(x)))} ";
            }

            title += $"(Географический охват: {GetQueryParam<string>("CoverageAreaType", query)}";

            var districtParam = GetQueryParam<string>("Districts", query);
            if (!string.IsNullOrEmpty(districtParam))
            {
                title += $", Административный Округ: {districtParam}";
            }

            var regionParam = GetQueryParam<string>("Regions", query);
            if (!string.IsNullOrEmpty(regionParam))
            {
                title += $", Район: {regionParam}";
            }

            var zoneParam = GetQueryParam<string>("Zones", query);
            if (!string.IsNullOrEmpty(zoneParam))
            {
                title += $", Зона: {zoneParam}";
            }

            var cadastralQuarter = GetQueryParam<string>("CadastralQuarter", query);
            if (!string.IsNullOrEmpty(cadastralQuarter))
            {
                title += $", Кадастровый квартал: {cadastralQuarter}";
            }

            title += ")";

            return title;
        }

        private long GetZoneByName(string zoneParam)
        {
            switch (zoneParam)
            {
                case "Зона 1":
                    return 1;
                case "Зона 2":
                    return 2;
                case "Зона 3":
                    return 3;
                case "Зона 4":
                    return 4;
                case "Зона 5":
                    return 5;
                default:
                    throw new Exception($"Указана неизвестная зона: {zoneParam}");
            }
        }

        private CoefficientCoverageAreaType GetCoverageAreaTypeByName(string coverageAreaTypeParam)
        {
            switch (coverageAreaTypeParam)
            {
                case "Город":
                    return CoefficientCoverageAreaType.City;
                case "Административный Округ":
                    return CoefficientCoverageAreaType.District;
                case "Район":
                    return CoefficientCoverageAreaType.Region;
                case "Зона":
                    return CoefficientCoverageAreaType.Zone;
                case "Квартал":
                    return CoefficientCoverageAreaType.CadastralQuarter;
                default:
                    throw new Exception("Не указан Географический охват");
            }
        }

    }
}
