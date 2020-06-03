using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.QualityPricingFactorsEncodingResults
{
	public class QualityPricingFactorsEncodingResultsOksReport : StatisticalDataReport
	{
		private readonly QualityPricingFactorsEncodingResultsService _service;

		public QualityPricingFactorsEncodingResultsOksReport()
		{
			_service = new QualityPricingFactorsEncodingResultsService(GbuObjectService, StatisticalDataService);
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(QualityPricingFactorsEncodingResultsOksReport);
		}

		public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
		{
			if (initialisation)
			{
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "ParentKnAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "TypeOfUsingNameAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "TypeOfUsingCodeAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "TypeOfUsingCodeSourceAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "TypeOfUsingGroupCodeAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "FunctionalGroupNameAttribute"));
				InitialiseGbuAttributesFilterValue(
					filterValues.FirstOrDefault(f => f.ParamName == "SegmentAttribute"));
			}
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);

			var parentKnAttributeId = GetQueryParam<long?>("ParentKnAttribute", query);
			if (!parentKnAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'КН родителя (для помещения)'");
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

			var typeOfUsingGroupCodeAttributeId = GetQueryParam<long?>("TypeOfUsingGroupCodeAttribute", query);
			if (!typeOfUsingGroupCodeAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'Код подгруппы вида использования'");
			}

			var functionalGroupNameAttributeId = GetQueryParam<long?>("FunctionalGroupNameAttribute", query);
			if (!functionalGroupNameAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'Наименование функциональной подгруппы'");
			}

			var segmentAttributeId = GetQueryParam<long?>("SegmentAttribute", query);
			if (!segmentAttributeId.HasValue)
			{
				throw new Exception("Не указан атрибут 'Сегмент'");
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
			dataTable.Columns.Add("Purpose");
			dataTable.Columns.Add("Address");
			dataTable.Columns.Add("Location");
			dataTable.Columns.Add("CadastralQuarter");
			dataTable.Columns.Add("ParentKn");
			dataTable.Columns.Add("ZuCadastralNumber");
			dataTable.Columns.Add("BuildingYear");
			dataTable.Columns.Add("CommissioningYear");
			dataTable.Columns.Add("FloorCount");
			dataTable.Columns.Add("UndergroundFloorCount");
			dataTable.Columns.Add("FloorNumber");
			dataTable.Columns.Add("WallMaterial");
			dataTable.Columns.Add("TypeOfUsingName");
			dataTable.Columns.Add("TypeOfUsingCode");
			dataTable.Columns.Add("TypeOfUsingCodeSource");
			dataTable.Columns.Add("TypeOfUsingGroupCode");
			dataTable.Columns.Add("FunctionalGroupName");
			dataTable.Columns.Add("Segment");


			var data = _service.GetDataForOksObjects(taskIdList, parentKnAttributeId.Value,
				typeOfUsingNameAttributeId.Value, typeOfUsingCodeAttributeId.Value, typeOfUsingCodeSourceAttributeId.Value, typeOfUsingGroupCodeAttributeId.Value,
				functionalGroupNameAttributeId.Value, segmentAttributeId.Value);

			var i = 1;
			foreach (var dto in data)
			{
				dataTable.Rows.Add(i,
					dto.PropertyType,
					dto.CadastralNumber,
					dto.Square,
					dto.Name,
					dto.Purpose,
					dto.Address,
					dto.Location,
					dto.CadastralQuarter,
					dto.ParentKn,
					dto.ZuCadastralNumber,
					dto.BuildingYear,
					dto.CommissioningYear,
					dto.FloorCount,
					dto.UndergroundFloorCount,
					dto.FloorNumber,
					dto.WallMaterial,
					dto.TypeOfUsingName,
					dto.TypeOfUsingCode,
					dto.TypeOfUsingCodeSource,
					dto.TypeOfUsingGroupCode,
					dto.FunctionalGroupName,
					dto.Segment
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
