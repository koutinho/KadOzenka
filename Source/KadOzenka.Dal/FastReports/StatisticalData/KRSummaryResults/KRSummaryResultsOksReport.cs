using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.KRSummaryResults
{
	public class KRSummaryResultsOksReport : StatisticalDataReport
	{
		private readonly KRSummaryResultsService _summaryResultsService;

		public KRSummaryResultsOksReport()
		{
			_summaryResultsService = new KRSummaryResultsService(GbuObjectService, StatisticalDataService);
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(KRSummaryResultsOksReport);
		}

		public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
		{
			if (initialisation)
            {
                var klardFilter = filterValues.FirstOrDefault(f => f.ParamName == "KlardAttribute");
                var parentKnFilter = filterValues.FirstOrDefault(f => f.ParamName == "ParentKnAttribute");

                InitialiseGbuAttributesFilterValue(klardFilter, parentKnFilter);
            }
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);

			var klardAttributeId = GetQueryParam<long?>("KlardAttribute", query);
			if (!klardAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'КЛАДР'");
			}

			var parentKnAttributeId = GetQueryParam<long?>("ParentKnAttribute", query);
			if (!parentKnAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'КН родителя'");
			}

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Сводные результаты государственной кадастровой оценки объектов недвижимости по кадастровому району");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("DataNum");
			dataTable.Columns.Add("Kn");
			dataTable.Columns.Add("PropertyType");
			dataTable.Columns.Add("Square");
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("Purpose");
			dataTable.Columns.Add("Address");
			dataTable.Columns.Add("Kladr");
			dataTable.Columns.Add("ParentKn");
			dataTable.Columns.Add("Location");
			dataTable.Columns.Add("CadastralQuarter");
			dataTable.Columns.Add("ZuCadastralNumber");
			dataTable.Columns.Add("BuildingYear");
			dataTable.Columns.Add("CommissioningYear");
			dataTable.Columns.Add("FloorCount");
			dataTable.Columns.Add("UndergroundFloorCount");
			dataTable.Columns.Add("FloorNumber");
			dataTable.Columns.Add("WallMaterial");
			dataTable.Columns.Add("AvailabilityPercentage");
			dataTable.Columns.Add("Upks");
			dataTable.Columns.Add("CadastralCost");

			var data = _summaryResultsService.GetKRSummaryResultsOksData(taskIdList, klardAttributeId.Value, parentKnAttributeId.Value);

			var i = 1;
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(i, 
					unitDto.CadastralNumber,
					unitDto.PropertyType,
					unitDto.Square,
					unitDto.Name,
					unitDto.Purpose,
					unitDto.Address,
					unitDto.Kladr,
					unitDto.ParentKn,
					unitDto.Location,
					unitDto.CadastralQuarter,
					unitDto.ZuCadastralNumber,
					unitDto.BuildingYear,
					unitDto.CommissioningYear,
					unitDto.FloorCount,
					unitDto.UndergroundFloorCount,
					unitDto.FloorNumber,
					unitDto.WallMaterial,
					unitDto.AvailabilityPercentage,
					unitDto.Upks,
					unitDto.CadastralCost
					);
				i++;
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}
	}
}
