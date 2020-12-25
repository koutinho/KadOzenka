using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using Platform.Reports;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport;

namespace KadOzenka.Dal.FastReports
{
    public class ChangedRegisteredObjectsReport : FastReportBase
    {
	    private readonly ReportingFormFormationService _reportingFormFormationService;
	    private readonly CancellationManager _cancellationManager;

		public ChangedRegisteredObjectsReport()
		{
			_cancellationManager = new CancellationManager();
			_reportingFormFormationService = new ReportingFormFormationService(new GbuObjectService(), _cancellationManager);
		}

		protected override string TemplateName(NameValueCollection query)
        {
	        var reportType = GetQueryParam<string>("ReportType", query);
	        return reportType == "В разрезе видов объекта недвижимости"
		        ? "RegisteredObjectsByPropertyTypeReport"
		        : "RegisteredChangedObjectsReport";
        }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
	        _cancellationManager.BaseCancellationToken = CancellationToken.GetValueOrDefault();
			var reportType = GetQueryParam<string>("ReportType", query);
	        DataSet dataSet = reportType == "В разрезе видов объекта недвижимости"
	            ? GetDataForNewlyRegisteredObjectsByPropertyTypeReport(query)
				: GetDataForNewlyRegisteredObjectsDefaultReport(query);

			return dataSet;
        }

        private DataSet GetDataForNewlyRegisteredObjectsDefaultReport(NameValueCollection query)
        {
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add(MakeTitle("Количество изменённых объектов недвижимости", query));

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("CadastralNumber");
			dataTable.Columns.Add("CreationDate");
			dataTable.Columns.Add("StatusRepeatCalc");
			dataTable.Columns.Add("Square");
			dataTable.Columns.Add("PropertyType");
			dataTable.Columns.Add("Status");
			dataTable.Columns.Add("ChangedFactors");

			var data = _reportingFormFormationService.GetChangedObjects(
				GetQueryParam<DateTime?>("DateFrom", query), GetQueryParam<DateTime?>("DateTo", query));

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.CadastralNumber,
					unitDto.CreationDate?.ToShortDateString(),
					unitDto.StatusRepeatCalc.GetEnumDescription(),
					unitDto.Square,
					unitDto.PropertyType.GetEnumDescription(),
					unitDto.Status.GetEnumDescription(),
					string.Join("; ", unitDto.ChangedFactors));
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}

        private DataSet GetDataForNewlyRegisteredObjectsByPropertyTypeReport(NameValueCollection query)
        {
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add(MakeTitle("Количество изменённых объектов недвижимости в разрезе видов объекта недвижимости", query));

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("PropertyType");
			dataTable.Columns.Add("ObjectsCount");

			var data = _reportingFormFormationService.GetChangedObjectsByPropertyType(
				GetQueryParam<DateTime?>("DateFrom", query), GetQueryParam<DateTime?>("DateTo", query));

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.PropertyType.GetEnumDescription(), unitDto.ObjectsCount);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}

        private string MakeTitle(string firstPart, NameValueCollection query)
        {
	        var result = firstPart;
	        result += (GetQueryParam<DateTime?>("DateFrom", query)).HasValue
		        ? $" c {GetQueryParam<DateTime?>("DateFrom", query).Value.ToShortDateString()} "
		        : "";
	        result += (GetQueryParam<DateTime?>("DateTo", query)).HasValue
		        ? $"по {GetQueryParam<DateTime?>("DateTo", query).Value.ToShortDateString()} "
		        : "";

	        return result;
        }
	}
}
