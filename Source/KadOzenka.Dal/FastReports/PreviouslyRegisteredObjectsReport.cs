using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register;
using Platform.Reports;
using Core.Shared.Extensions;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport;

namespace KadOzenka.Dal.FastReports
{
    public class PreviouslyRegisteredObjectsReport : FastReportBase
    {
	    private readonly GbuObjectService _gbuObjectService;
	    private readonly ReportingFormFormationService _reportingFormFormationService;
	    private readonly CancellationManager _cancellationManager;

		public PreviouslyRegisteredObjectsReport()
	    {
		    _gbuObjectService = new GbuObjectService();
		    _cancellationManager = new CancellationManager();
		    _reportingFormFormationService = new ReportingFormFormationService(_gbuObjectService, _cancellationManager);
	    }

		protected override string TemplateName(NameValueCollection query)
		{
			var reportType = GetQueryParam<string>("ReportType", query);
			switch (reportType)
			{
				case "В разрезе видов объекта недвижимости":
					return "RegisteredObjectsByPropertyTypeReport";
				case "В разрезе видов использования":
					return "RegisteredObjectsByTypeOfUseReport";
				default:
					return "RegisteredObjectsDefaultReport";
			}
		}

		public override string GetTitle(long? objectId)
		{
			return ReportType.Title;
		}

		public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
		{
			if (initialisation)
			{
				var typeOfUseAttributeFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "TypeOfUseAttribute");
				if (typeOfUseAttributeFilterValue != null)
				{
					typeOfUseAttributeFilterValue.ReportParameters = new List<ReportParameter>();
					var attributes = _gbuObjectService.GetGbuAttributes();
					typeOfUseAttributeFilterValue.ReportParameters.Add(new ReportParameter { Value = string.Empty, Key = string.Empty });
					typeOfUseAttributeFilterValue.ReportParameters.AddRange(attributes.Select(x => new ReportParameter
						{ Value = $"{x.Name} ({x.ParentRegister?.RegisterDescription})", Key = $"key:{x.Id}" })
					);
				}
			}
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			_cancellationManager.SetBaseToken(CancellationToken);
			DataSet dataSet;
			var reportType = GetQueryParam<string>("ReportType", query);
			switch (reportType)
			{
				case "В разрезе видов объекта недвижимости":
					dataSet = GetDataForPreviouslyRegisteredObjectsByPropertyTypeReport(query);
					break;
				case "В разрезе видов использования":
					dataSet = GetDataForPreviouslyRegisteredObjectsByTypeOfUseReport(query);
					break;
				default:
					dataSet = GetDataForPreviouslyRegisteredObjectsDefaultReport(query);
					break;
			}

			return dataSet;
		}

		private DataSet GetDataForPreviouslyRegisteredObjectsDefaultReport(NameValueCollection query)
		{
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add(MakeTitle("Количество ранее учтённых объектов недвижимости", query));

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("CadastralNumber");
			dataTable.Columns.Add("CreationDate");
			dataTable.Columns.Add("StatusRepeatCalc");
			dataTable.Columns.Add("Square");
			dataTable.Columns.Add("PropertyType");
			dataTable.Columns.Add("Status");

			var data = _reportingFormFormationService.GetPreviouslyRegisteredObjects(
				GetQueryParam<DateTime?>("DateFrom", query), GetQueryParam<DateTime?>("DateTo", query));

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.CadastralNumber,
					unitDto.CreationDate?.ToShortDateString(),
					unitDto.StatusRepeatCalc.GetEnumDescription(),
					unitDto.Square,
					unitDto.PropertyType.GetEnumDescription(),
					unitDto.Status.GetEnumDescription());
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}

		private DataSet GetDataForPreviouslyRegisteredObjectsByPropertyTypeReport(NameValueCollection query)
		{
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add(MakeTitle("Количество ранее учтённых объектов недвижимости в разрезе видов объекта недвижимости", query));

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("PropertyType");
			dataTable.Columns.Add("ObjectsCount");

			var data = _reportingFormFormationService.GetPreviouslyRegisteredObjectsByPropertyType(
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

		private DataSet GetDataForPreviouslyRegisteredObjectsByTypeOfUseReport(NameValueCollection query)
		{
			var typeOfUseAttributeId = GetQueryParam<long?>("TypeOfUseAttribute", query);
			if (!typeOfUseAttributeId.HasValue)
			{
				throw new Exception("Не указан Атрибут вида использования");
			}
			var attributeData = RegisterCache.GetAttributeData(typeOfUseAttributeId.Value);
			var registerData = RegisterCache.GetRegisterData(attributeData.RegisterId);
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			var title = MakeTitle("Количество ранее учтённых объектов недвижимости в разрезе видов использования",
				query);
			title += $" (атрибут вида использования: {attributeData.Name} ({registerData.Description}))";
			dataTitleTable.Rows.Add(title);

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("TypeOfUse");
			dataTable.Columns.Add("ObjectsCount");

			var data = _reportingFormFormationService.GetPreviouslyRegisteredObjectsByTypeOfUse(
				GetQueryParam<DateTime?>("DateFrom", query), GetQueryParam<DateTime?>("DateTo", query), typeOfUseAttributeId.Value);

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.TypeOfUse, unitDto.ObjectsCount);
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
