using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities;
using KadOzenka.Dal.FastReports.StatisticalData.Common;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms
{
	public class CalculationsAnalysisReport : StatisticalDataReport
	{
		private readonly string _reportSqlFileName = "AdditionalForms_CalculationAnalysis";

		protected override string TemplateName(NameValueCollection query)
		{
			return "AdditionalFormsCalculationsAnalysisReport";
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);
			var reportItems = GetReportData(taskIdList);
			var dataSet = new DataSet();
			var dataTable = GetReportDataTable(reportItems);
			dataSet.Tables.Add(dataTable);

			return dataSet;
		}

		#region Support Methods

		public List<CalculationsAnalysisReportItem> GetReportData(long[] taskIdList)
		{
			string contents;
			using (var sr = new StreamReader(StatisticalDataService.GetSqlQueryFileStream(_reportSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", taskIdList),
                RosreestrRegisterService.GetSquareAttribute().Id,
                RosreestrRegisterService.GetObjectNameAttribute().Id,
                RosreestrRegisterService.GetTypeOfUseByDocumentsAttribute().Id,
                RosreestrRegisterService.GetBuildingPurposeAttribute().Id,
                RosreestrRegisterService.GetPlacementPurposeAttribute().Id,
                RosreestrRegisterService.GetConstructionPurposeAttribute().Id,
                RosreestrRegisterService.GetAddressAttribute().Id,
                RosreestrRegisterService.GetLocationAttribute().Id
			);
			var result = QSQuery.ExecuteSql<CalculationsAnalysisReportItem>(sql);

			return result;
		}

		public DataTable GetReportDataTable(List<CalculationsAnalysisReportItem> reportItems)
		{
			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("CadastralNumber", typeof(string));
			dataTable.Columns.Add("Type", typeof(string));
			dataTable.Columns.Add("Square", typeof(string));
			dataTable.Columns.Add("ObjectNameTypeOfUse", typeof(string));
			dataTable.Columns.Add("Purpose", typeof(string));
			dataTable.Columns.Add("Address", typeof(string));
			dataTable.Columns.Add("Location", typeof(string));
			dataTable.Columns.Add("EvaluationSubgroup2018", typeof(string));
			dataTable.Columns.Add("Upks2018", typeof(string));
			dataTable.Columns.Add("CadastralCost2018", typeof(decimal));
			dataTable.Columns.Add("CadastralQuartal2018", typeof(string));
			dataTable.Columns.Add("TaskType", typeof(string));
			dataTable.Columns.Add("EvaluationSubgroup", typeof(string));
			dataTable.Columns.Add("Upks", typeof(decimal));
			dataTable.Columns.Add("CadastralCost", typeof(decimal));
			dataTable.Columns.Add("CadastralQuartal", typeof(string));
			dataTable.Columns.Add("EGRNChangeDate", typeof(string));
			dataTable.Columns.Add("Status", typeof(string));
			dataTable.Columns.Add("Changes", typeof(string));
			dataTable.Columns.Add("MinUpksByCadastralQuartal", typeof(decimal));
			dataTable.Columns.Add("AverageUpksByCadastralQuartal", typeof(decimal));
			dataTable.Columns.Add("MaxUpksByCadastralQuartal", typeof(decimal));
			dataTable.Columns.Add("MinUpksByZone", typeof(decimal));
			dataTable.Columns.Add("AverageUpksByZone", typeof(decimal));
			dataTable.Columns.Add("MaxUpksByZone", typeof(decimal));
			dataTable.Columns.Add("ParticipatingCount", typeof(decimal));
			dataTable.Columns.Add("CountInYear", typeof(decimal));
			dataTable.Columns.Add("CountInDays", typeof(decimal));

			foreach (var item in reportItems)
			{
				dataTable.Rows.Add(item.CadastralNumber,
					item.TypeEnum.GetEnumDescription(),
					item.RosreestrSquareValue,
					item.ObjectNameTypeOfUse,
					item.Purpose,
					item.Address,
					item.Location,
					item.EvaluationSubgroup2018,
					(item.Upks2018.HasValue
						? Math.Round(item.Upks2018.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					item.CadastralCost2018,
					item.CadastralQuartal2018,
					item.TaskTypeEnum.GetEnumDescription(),
					item.EvaluationSubgroup,
					(item.Upks.HasValue
						? Math.Round(item.Upks.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					item.CadastralCost,
					item.CadastralQuartal,
					item.EGRNChangeDate.HasValue ? item.EGRNChangeDate.Value.ToString(DateFormat) : null,
					item.StatusEnum.GetEnumDescription(),
					item.Changes,
					(item.MinUpksByCadastralQuartal.HasValue
						? Math.Round(item.MinUpksByCadastralQuartal.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					(item.AverageUpksByCadastralQuartal.HasValue
						? Math.Round(item.AverageUpksByCadastralQuartal.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					(item.MaxUpksByCadastralQuartal.HasValue
						? Math.Round(item.MaxUpksByCadastralQuartal.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					(item.MinUpksByZone.HasValue
						? Math.Round(item.MinUpksByZone.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					(item.AverageUpksByZone.HasValue
						? Math.Round(item.AverageUpksByZone.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					(item.MaxUpksByZone.HasValue
						? Math.Round(item.MaxUpksByZone.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					item.ParticipatingCount,
					item.CountInYear,
					item.CountInDays
					);
			}

			return dataTable;
		}

		#endregion Support Methods
	}
}
