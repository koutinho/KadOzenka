using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
	public class MinMaxAverageUPKSByCadastralQuartersReport : StatisticalDataReport
	{
		private readonly MinMaxAverageUPKSByCadastralQuartersService _service;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		public MinMaxAverageUPKSByCadastralQuartersReport()
		{
			_service = new MinMaxAverageUPKSByCadastralQuartersService(new StatisticalDataService(), new GbuCodRegisterService());
			_logger = Log.ForContext<MinMaxAverageUPKSByCadastralQuartersReport>();
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(MinMaxAverageUPKSByCadastralQuartersReport);
		}

		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			_service.QueryManager.SetBaseToken(CancellationToken);
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
			Logger.Debug("Найдено {Count} объектов", data?.Count);

			Logger.Debug("Начато формирование таблиц");
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(unitDto.CadastralRegionNumber, unitDto.CadastralQuater, unitDto.ObjectsCount,
					unitDto.UpksCalcTypeEnum.GetEnumDescription(), unitDto.PropertyType,
					(unitDto.UpksCalcValue.HasValue
						? Math.Round(unitDto.UpksCalcValue.Value, PrecisionForDecimalValues)
						: (decimal?) null));
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);
			Logger.Debug("Закончено формирование таблиц");

			return dataSet;
		}
	}
}
