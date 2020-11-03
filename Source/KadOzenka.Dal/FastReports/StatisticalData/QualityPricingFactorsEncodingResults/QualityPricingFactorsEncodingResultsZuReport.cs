using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.QualityPricingFactorsEncodingResults
{
	public class QualityPricingFactorsEncodingResultsZuReport : StatisticalDataReport
	{
		private readonly QualityPricingFactorsEncodingResultsService _service;
		private readonly ILogger _logger;
		protected override ILogger Logger => _logger;

		public QualityPricingFactorsEncodingResultsZuReport()
		{
			_service = new QualityPricingFactorsEncodingResultsService(StatisticalDataService, GbuCodRegisterService);
			_logger = Log.ForContext<QualityPricingFactorsEncodingResultsZuReport>();
		}


		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(QualityPricingFactorsEncodingResultsZuReport);
		}

		public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
		{
			if (initialisation)
			{
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "LinkedObjectsInfoAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "LinkedObjectsInfoSourceAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "TypeOfUsingNameAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "TypeOfUsingCodeAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "TypeOfUsingCodeSourceAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "SegmentAttribute"));
			}
		}

		protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);

			var linkedObjectsInfoAttributeId = GetQueryParam<long?>("LinkedObjectsInfoAttribute", query);
			if (!linkedObjectsInfoAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'Сведения о нахождении на земельном участке других связанных с ним объектов недвижимости'");
			}

			var linkedObjectsInfoSourceAttributeId = GetQueryParam<long?>("LinkedObjectsInfoSourceAttribute", query);
			if (!linkedObjectsInfoSourceAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'Источник информации о нахождении на земельном участке других связанных с ним объектов недвижимости'");
			}

			var segmentAttributeId = GetQueryParam<long?>("SegmentAttribute", query);
			if (!segmentAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'Сегмент'");
			}

			var typeOfUsingNameAttributeId = GetQueryParam<long?>("TypeOfUsingNameAttribute", query);
			if (!typeOfUsingNameAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'Наименование вида использования'");
			}

			var typeOfUsingCodeAttributeId = GetQueryParam<long?>("TypeOfUsingCodeAttribute", query);
			if (!typeOfUsingCodeAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'Код вида использования'");
			}

			var typeOfUsingCodeSourceAttributeId = GetQueryParam<long?>("TypeOfUsingCodeSourceAttribute", query);
			if (!typeOfUsingCodeSourceAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'Источник информации кода вида использования'");
			}

			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Состав данных объектов недвижимости с присвоенными видами использования");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("DataNum");
			dataTable.Columns.Add("PropertyType");
			dataTable.Columns.Add("Kn");
			dataTable.Columns.Add("Square");
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("PermittedUsing");
			dataTable.Columns.Add("Address");
			dataTable.Columns.Add("Location");
			dataTable.Columns.Add("CadastralQuarter");
			dataTable.Columns.Add("LinkedObjectsInfo");
			dataTable.Columns.Add("LinkedObjectsInfoSource");
			dataTable.Columns.Add("Segment");
			dataTable.Columns.Add("TypeOfUsingName");
			dataTable.Columns.Add("TypeOfUsingCode");
			dataTable.Columns.Add("TypeOfUsingCodeSource");

			var data = _service.GetDataForZuObjects(taskIdList, linkedObjectsInfoAttributeId.Value, linkedObjectsInfoSourceAttributeId.Value,
				segmentAttributeId.Value, typeOfUsingNameAttributeId.Value, typeOfUsingCodeAttributeId.Value, typeOfUsingCodeSourceAttributeId.Value);
			Logger.Debug("Найдено {Count} объектов", data?.Count);

			Logger.Debug("Начато формирование таблиц");
			var i = 1;
			foreach (var unitDto in data)
			{
				dataTable.Rows.Add(i,
					unitDto.PropertyType,
					unitDto.CadastralNumber,
					unitDto.Square,
					unitDto.Name,
					unitDto.PermittedUsing,
					unitDto.Address,
					unitDto.Location,
					unitDto.CadastralQuarter,
					unitDto.LinkedObjectsInfo,
					unitDto.LinkedObjectsInfoSource,
					unitDto.Segment,
					unitDto.TypeOfUsingName,
					unitDto.TypeOfUsingCode,
					unitDto.TypeOfUsingCodeSource
				);
				i++;
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);
			Logger.Debug("Закончено формирование таблиц");

			return dataSet;
		}
	}
}
