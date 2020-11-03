using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.QualityPricingFactorsEncodingResults
{
	public class QualityPricingFactorsEncodingResultsGroupingReport : StatisticalDataReport
	{
		private readonly QualityPricingFactorsEncodingResultsService _service;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		public QualityPricingFactorsEncodingResultsGroupingReport()
		{
			_service = new QualityPricingFactorsEncodingResultsService(StatisticalDataService, GbuCodRegisterService);
			_logger = Log.ForContext<QualityPricingFactorsEncodingResultsGroupingReport>();
		}


		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(QualityPricingFactorsEncodingResultsGroupingReport);
		}

		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);
			var tourId = GetTourId(query);

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Группировка объектов недвижимости");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("DataNum");
			
			dataTable.Columns.Add("PropertyType");
			dataTable.Columns.Add("Kn");
			dataTable.Columns.Add("GroupNumber");
			dataTable.Columns.Add("ModelCalculationMethod");


			var data = _service.GetGroupingData(taskIdList, tourId);

			var i = 1;
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(i,
					unitDto.PropertyType,
					unitDto.CadastralNumber,
					unitDto.GroupNumber,
					unitDto.ModelCalculationMethod
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
