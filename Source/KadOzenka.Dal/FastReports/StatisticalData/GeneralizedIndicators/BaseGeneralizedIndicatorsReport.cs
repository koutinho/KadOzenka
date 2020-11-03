using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.GeneralizedIndicators
{
	public abstract class BaseGeneralizedIndicatorsReport : StatisticalDataReport
	{
		private readonly GeneralizedIndicatorsService _service;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		public BaseGeneralizedIndicatorsReport()
		{
			_service = new GeneralizedIndicatorsService(StatisticalDataService, new GbuCodRegisterService());
			_logger = Log.ForContext<BaseGeneralizedIndicatorsReport>();
		}


		protected abstract string GetReportTitle(NameValueCollection query);
		protected abstract string GetDataNameColumnText(NameValueCollection query);
		protected abstract StatisticDataAreaDivisionType GetStatisticDataAreaDivisionTypeReport(NameValueCollection query);

		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Columns.Add("DataNameColumnText");
			dataTitleTable.Rows.Add(GetReportTitle(query),
				GetDataNameColumnText(query));

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("AdditionalName");
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("ObjectsCount", typeof(int));
			dataTable.Columns.Add("GroupName");
			dataTable.Columns.Add("UpksCalcType", typeof(string));
			dataTable.Columns.Add("UpksCalcValue", typeof(decimal));

			var data = _service.GetData(taskIdList, GetStatisticDataAreaDivisionTypeReport(query), GetPropertyObjectTypeReport(query));

			Logger.Debug("Начато формирование таблиц");
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(
					unitDto.AdditionalName, unitDto.Name,unitDto.ObjectsCount,
					unitDto.GroupName, unitDto.UpksCalcTypeEnum.GetEnumDescription(),
					(unitDto.UpksCalcValue.HasValue ? Math.Round(unitDto.UpksCalcValue.Value, PrecisionForDecimalValues) : (decimal?)null)
				);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);
			Logger.Debug("Закончено формирование таблиц");

			return dataSet;
		}

		private PropertyObjectType GetPropertyObjectTypeReport(NameValueCollection query)
		{
			var propertyObjectType = GetQueryParam<string>("ZuOksObjectType", query);
			switch (propertyObjectType)
			{
				case "ОКС":
					return PropertyObjectType.Oks;
				case "ЗУ":
					return PropertyObjectType.Zu;
				case "ОКС и ЗУ":
					return PropertyObjectType.OksAndZu;
				default:
					throw new InvalidDataException($"Неизвестный тип объекта недвижимости: {propertyObjectType}");
			}
		}
	}
}
