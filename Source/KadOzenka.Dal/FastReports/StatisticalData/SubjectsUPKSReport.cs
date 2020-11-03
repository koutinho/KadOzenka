using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
	public class SubjectsUPKSReport : StatisticalDataReport
	{
		private readonly SubjectsUPKSService _subjectsUPKSService;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		public SubjectsUPKSReport()
		{
			_subjectsUPKSService = new SubjectsUPKSService(StatisticalDataService);
			_logger = Log.ForContext<SubjectsUPKSReport>();
		}

		protected override string TemplateName(NameValueCollection query)
		{
			var reportType = GetQueryParam<string>("ReportType", query);
			switch (reportType)
			{
				case "Тип объекта недвижимости":
					return "SubjectsUPKSByTypeReport";
				case "Тип и назначение объекта недвижимости":
					return "SubjectsUPKSByTypeAndPurposeReport";
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}
		}

		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);
			var reportType = GetQueryParam<string>("ReportType", query);

			DataSet data;
			switch (reportType)
			{
				case "Тип объекта недвижимости":
					data =  GetSubjectsUPKSByTypeReportData(taskIdList);
					break;
				case "Тип и назначение объекта недвижимости":
					data = GetSubjectsUPKSByTypeAndPurposeReportData(taskIdList);
					break;
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}

			return data;
		}

		private DataSet GetSubjectsUPKSByTypeReportData(long[] taskIdList)
		{
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Минимальные, максимальные, средние удельные показатели кадастровой стоимости объектов недвижимости в городе Москве");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("ObjectsCount", typeof(long));
			dataTable.Columns.Add("UpksCalcType", typeof(string));
			dataTable.Columns.Add("UpksCalcValue", typeof(decimal));

			var data = _subjectsUPKSService.GetSubjectsUPKSByTypeData(taskIdList);

			Logger.Debug("Начато формирование таблиц");
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.PropertyType, unitDto.ObjectsCount,
					unitDto.UpksCalcTypeEnum.GetEnumDescription(),
					(unitDto.UpksCalcValue.HasValue
						? Math.Round(unitDto.UpksCalcValue.Value, PrecisionForDecimalValues)
						: (decimal?)null));
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);
			Logger.Debug("Закончено формирование таблиц");

			return dataSet;
		}

		private DataSet GetSubjectsUPKSByTypeAndPurposeReportData(long[] taskIdList)
		{
			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Минимальные, максимальные, средние удельные показатели кадастровой стоимости объектов недвижимости в городе Москве");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("PropertyType", typeof(string));
			dataTable.Columns.Add("Purpose", typeof(string));
			dataTable.Columns.Add("HasPurpose", typeof(bool));
			dataTable.Columns.Add("ObjectsCount", typeof(decimal));
			dataTable.Columns.Add("UpksCalcType", typeof(string));
			dataTable.Columns.Add("UpksCalcValue", typeof(string));
			

			var data = _subjectsUPKSService.GetSubjectsUPKSByTypeAndPurposeData(taskIdList);
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.PropertyType, unitDto.Purpose, unitDto.HasPurpose, unitDto.ObjectsCount,
					unitDto.UpksCalcTypeEnum.GetEnumDescription(),
					(unitDto.UpksCalcValue.HasValue
						? Math.Round(unitDto.UpksCalcValue.Value, PrecisionForDecimalValues).ParseToString()
						: null));
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}
	}
}
