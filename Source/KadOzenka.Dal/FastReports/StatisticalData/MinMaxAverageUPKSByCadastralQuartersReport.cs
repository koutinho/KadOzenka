using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
	public class MinMaxAverageUPKSByCadastralQuartersReport : StatisticalDataReport
	{
		private readonly MinMaxAverageUPKSByCadastralQuartersService _service;

		public MinMaxAverageUPKSByCadastralQuartersReport()
		{
			_service = new MinMaxAverageUPKSByCadastralQuartersService(new StatisticalDataService());
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(MinMaxAverageUPKSByCadastralQuartersReport);
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			var title =
				"Минимальные, максимальные, средние удельные показатели кадастровой стоимости объектов недвижимости по кадастровым кварталам города Москвы";
			dataTitleTable.Rows.Add(title);

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("CadastralRegionNumber", typeof(string));
			dataTable.Columns.Add("CadastralQuater", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));
			dataTable.Columns.Add("UpksCalcType", typeof(string));
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("UpksCalcValue", typeof(decimal));

			var data = _service.GetMinMaxAverageUPKS(taskIdList);
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.CadastralRegionNumber, unitDto.CadastralQuater, unitDto.ObjectsCount, unitDto.UpksCalcType.GetEnumDescription(), unitDto.PropertyType, unitDto.UpksCalcValue);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return HadleData(dataSet);
		}
	}
}
