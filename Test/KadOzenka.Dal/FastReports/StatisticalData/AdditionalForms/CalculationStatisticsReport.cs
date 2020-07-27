﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms
{
	public class CalculationStatisticsReport : StatisticalDataReport
	{
		private readonly AdditionalFormsService _service;

		public CalculationStatisticsReport()
		{
			_service = new AdditionalFormsService(StatisticalDataService, GbuObjectService);
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return "AdditionalFormsCalculationStatisticsReport";
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("SubgroupId", typeof(string));
			dataTable.Columns.Add("SubgroupName", typeof(string));
			dataTable.Columns.Add("CalculationMethod", typeof(string));
			dataTable.Columns.Add("Formula", typeof(string));
			dataTable.Columns.Add("FactorsSubgroups", typeof(string));
			dataTable.Columns.Add("Coef", typeof(string));
			dataTable.Columns.Add("SighMarket", typeof(string));

			var data = _service.GetCalculationStatisticsData(taskIdList);

			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.SubgroupId,
					unitDto.SubgroupName,
					unitDto.CalculationMethod,
					unitDto.Formula,
					unitDto.FactorsSubgroups,
					unitDto.Coef,
					unitDto.SighMarket);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);

			return dataSet;
		}
	}
}
