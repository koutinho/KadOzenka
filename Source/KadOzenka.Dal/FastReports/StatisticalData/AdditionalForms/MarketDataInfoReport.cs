using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms
{
	public class MarketDataInfoReport : StatisticalDataReport
	{
		private readonly AdditionalFormsService _service;

		public MarketDataInfoReport()
		{
			_service = new AdditionalFormsService(StatisticalDataService);
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return "AdditionalFormsMarketDataInfoReport";
		}

		public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
		{
			if (initialisation)
			{
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "TypeOfUseCodeAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "OksGroupAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "TypeOfUseAttribute"));
			}
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var dateFrom = GetQueryParam<DateTime?>("DateFrom", query);
			var dateTo = GetQueryParam<DateTime?>("DateTo", query);
			var typeOfUseCodeAttributeId = GetFilterParameterValue(query, "TypeOfUseCodeAttribute", "Код вида использования");
			var oksGroupAttributeId = GetFilterParameterValue(query, "OksGroupAttribute", "Группа ОКС");
			var typeOfUseAttributeId = GetFilterParameterValue(query, "TypeOfUseAttribute", "Вид использования (функциональное назначение)");

			if (dateFrom.HasValue && dateTo.HasValue && dateTo < dateFrom)
			{
				throw new ArgumentException($"Некорректный временной интервал: с '{dateFrom.Value.ToString(DateFormat)}' по '{dateTo.Value.ToString(DateFormat)}'");
			}

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add(GetReportTitle(dateFrom, dateTo));

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("DataNum");
			dataTable.Columns.Add("UniqueNumber");
			dataTable.Columns.Add("Kn");
			dataTable.Columns.Add("SegmentGroup");
			dataTable.Columns.Add("TypeOfUseCode");
			dataTable.Columns.Add("OksGroup");
			dataTable.Columns.Add("SubjectCode");
			dataTable.Columns.Add("OKTMO");
			dataTable.Columns.Add("AddressReferencePoint");
			dataTable.Columns.Add("Metro");
			dataTable.Columns.Add("Market");
			dataTable.Columns.Add("Link");
			dataTable.Columns.Add("Phone");
			dataTable.Columns.Add("Date");
			dataTable.Columns.Add("AdText");
			dataTable.Columns.Add("TypeOfProperty");
			dataTable.Columns.Add("TypeOfUse");
			dataTable.Columns.Add("TypeOfRight");
			dataTable.Columns.Add("RoomCount");
			dataTable.Columns.Add("DealSuggestion");
			dataTable.Columns.Add("Square");
			dataTable.Columns.Add("Price");
			dataTable.Columns.Add("Upks");
			dataTable.Columns.Add("AnnualRateOfRent");

			var data = _service.GetMarketData(dateFrom, dateTo, typeOfUseCodeAttributeId, oksGroupAttributeId, typeOfUseAttributeId);
			var i = 1;
			foreach (var dto in data)
			{
				dataTable.Rows.Add(i,
					dto.UniqueNumber,
					dto.Kn,
					dto.SegmentGroup,
					dto.TypeOfUseCode,
					dto.OksGroup,
					dto.SubjectCode,
					dto.OKTMO,
					dto.AddressReferencePoint,
					dto.Metro,
					dto.Market,
					dto.Link,
					dto.Phone,
					dto.Date?.ToString(DateFormat),
					dto.AdText,
					dto.TypeOfProperty,
					dto.TypeOfUseCode,
					dto.TypeOfRight,
					dto.RoomCount,
					dto.DealSuggestion,
					dto.Square,
					dto.Price,
					(dto.Upks.HasValue
						? Math.Round(dto.Upks.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					(dto.AnnualRateOfRent.HasValue
						? Math.Round(dto.AnnualRateOfRent.Value, PrecisionForDecimalValues)
						: (decimal?)null));
				i++;
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}

		private string GetReportTitle(DateTime? dateFrom, DateTime? dateTo)
		{
			var title = "Состав данных о рыночной информации";
			if (dateFrom.HasValue || dateTo.HasValue)
			{
				title = $"{title} ({(dateFrom.HasValue ? $"c {dateFrom.Value.ToString(DateFormat)}" : string.Empty)} {(dateTo.HasValue ? $"по {dateTo.Value.ToString(DateFormat)}" : string.Empty)})";
			}

			return title;
		}
	}
}
