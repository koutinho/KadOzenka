using System;
using System.Collections.Specialized;
using System.Data;
using FastReport;
using FastReport.Matrix;
using Newtonsoft.Json;
using Platform.Reports;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
	public abstract class StatisticalDataReport : FastReportBase
	{
		public override string GetTitle(long? objectId)
		{
			return ReportType.Title;
		}

		protected long[] GetTaskIdList(NameValueCollection query)
		{
			var taskIdList = GetQueryParam<string>("TaskIdList", query);
			if (string.IsNullOrEmpty(taskIdList))
			{
				throw new Exception("Истекло время ожидания сессии. Обновите страницу.");
			}
			var taskIdListValue = JsonConvert.DeserializeObject<long[]>(taskIdList);

			return taskIdListValue;
		}

		protected DataSet HadleData(DataSet dataSet)
		{
			foreach (DataTable table in dataSet.Tables)
			{
				string tableName = table.TableName;
				Dictionary.Report.RegisterData(dataSet, tableName);
				GetDataSource(tableName).Enabled = true;

				// в файле *.frx банд должен называться Band<название_таблицы_в_DataSet>, например BandItem
				DataBand dataBand = FindObject("Band" + tableName) as DataBand;
				if (dataBand != null) dataBand.DataSource = GetDataSource(tableName);

				MatrixObject matrix = FindObject("Matrix" + tableName) as MatrixObject;
				if (matrix != null) matrix.DataSource = GetDataSource(tableName);
			}

			return new DataSet();
		}
	}
}
