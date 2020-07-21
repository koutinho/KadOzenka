using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using Microsoft.AspNetCore.Http;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.NumberOfObjectsByZoneAndSubgroups
{
	public class NumberOfObjectsByZoneAndSubgroupsZuReport : StatisticalDataReport
	{
		private readonly NumberOfObjectsByZoneAndSubgroupsService _service;

		public NumberOfObjectsByZoneAndSubgroupsZuReport()
		{
			_service = new NumberOfObjectsByZoneAndSubgroupsService(StatisticalDataService, GbuObjectService);
		}

		protected override string TemplateName(NameValueCollection query)
		{
			return "NumberOfObjectsByZoneAndSubgroupsZuOksReport";
		}

		public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
		{
			if (!initialisation)
				return;

			var reportType =
				int.Parse((StatisticalDataType.OnTheNumberOfObjectsByZoneAndSubgroupsZu).GetAttributeValue<StatisticalDataFmReportCodeAttribute>(
					nameof(StatisticalDataFmReportCodeAttribute.Code)));
			var tourKey = $"Report{reportType}TourId";
			var secondTourKey = $"Report{reportType}SecondTourId";
			if (!HttpContextHelper.HttpContext.Session.Keys.Contains(tourKey))
			{
				throw new Exception("Не передан идентификатор Тура 1");
			}
			if (!HttpContextHelper.HttpContext.Session.Keys.Contains(secondTourKey))
			{
				throw new Exception("Не передан идентификатор Тура 2");
			}

			var tourId = long.Parse(HttpContextHelper.HttpContext.Session.GetString(tourKey));
			var secondTourId = long.Parse(HttpContextHelper.HttpContext.Session.GetString(secondTourKey));

			var groupsFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "TourGroup");
			if (groupsFilterValue != null)
			{
				var groups = GetSubgroupsForTour(tourId);

				groupsFilterValue.ReportParameters = new List<ReportParameter>();
				groupsFilterValue.ReportParameters.AddRange(groups.Select(x => new ReportParameter { Value = $"{x.GroupName}", Key = $"key:{x.Id}" }));
			}
			var secondTourGroupsFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "SecondTourGroup");
			if (secondTourGroupsFilterValue != null)
			{
				var groups = GetSubgroupsForTour(secondTourId);

				secondTourGroupsFilterValue.ReportParameters = new List<ReportParameter>();
				secondTourGroupsFilterValue.ReportParameters.AddRange(groups.Select(x => new ReportParameter { Value = $"{x.GroupName}", Key = $"key:{x.Id}" }));
			}
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var firstTourId = GetQueryParam<long?>("TourId", query);
			if (!firstTourId.HasValue)
			{
				throw new Exception("Истекло время ожидания сессии. Обновите страницу.");
			}
			var secondTourId = GetQueryParam<long?>("SecondTourId", query);
			if (!secondTourId.HasValue)
			{
				throw new Exception("Истекло время ожидания сессии. Обновите страницу.");
			}
			var firstGroupId = GetQueryParam<long?>("TourGroup", query);
			if (!firstGroupId.HasValue)
			{
				throw new Exception("Не выбрана подгруппа Тура 1");
			}
			var secondGroupId = GetQueryParam<long?>("SecondTourGroup", query);
			if (!secondGroupId.HasValue)
			{
				throw new Exception("Истекло время ожидания сессии. Обновите страницу.");
			}


			var dataTitleTable = new DataTable("Common");
			dataTitleTable.Columns.Add("Title");
			dataTitleTable.Rows.Add("Статистика по УПКС ЗУ, расположенных в зонах и районах, отнесенных к подгруппе");

			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("Zone");
			dataTable.Columns.Add("ZoneNameByCircles");
			dataTable.Columns.Add("DistrictName");
			dataTable.Columns.Add("RegionName");
			dataTable.Columns.Add("ZoneDistrict");
			dataTable.Columns.Add("ZoneDistrictRegion");
			dataTable.Columns.Add("PropertyType");
			dataTable.Columns.Add("FirstTourObjectCount");
			dataTable.Columns.Add("SecondTourObjectCount");
			dataTable.Columns.Add("FirstTourObjectCountWithoutGroupChanging");
			dataTable.Columns.Add("SecondTourObjectCountWithoutGroupChanging");
			dataTable.Columns.Add("FirstTourObjectCountWithGroupChanging");
			dataTable.Columns.Add("SecondTourObjectCountWithGroupChanging");
			dataTable.Columns.Add("FirstTourMinUpks");
			dataTable.Columns.Add("FirstTourMaxUpks");
			dataTable.Columns.Add("FirstTourAverageUpks");
			dataTable.Columns.Add("SecondTourMinUpks");
			dataTable.Columns.Add("SecondTourMaxUpks");
			dataTable.Columns.Add("SecondTourAverageUpks");
			dataTable.Columns.Add("MinUpksVarianceBetweenTours");
			dataTable.Columns.Add("MaxUpksVarianceBetweenTours");
			dataTable.Columns.Add("AverageUpksVarianceBetweenTours");
			dataTable.Columns.Add("MinUpksVarianceBetweenToursWithoutGroupChanging");
			dataTable.Columns.Add("MaxUpksVarianceBetweenToursWithoutGroupChanging");
			dataTable.Columns.Add("AverageUpksVarianceBetweenToursWithoutGroupChanging");


			var data = _service.GetNumberOfObjectsByZoneAndSubgroupsData(firstTourId.Value, firstGroupId.Value,
				secondTourId.Value, secondGroupId.Value, false);


			foreach (var dto in data)
			{
				dataTable.Rows.Add(dto.Zone,
					dto.ZoneNameByCircles,
					dto.DistrictName,
					dto.RegionName,
					dto.ZoneDistrict,
					dto.ZoneDistrictRegion,
					dto.PropertyType,
					dto.FirstTourObjectCount,
					dto.SecondTourObjectCount,
					dto.FirstTourObjectCountWithoutGroupChanging,
					dto.SecondTourObjectCountWithoutGroupChanging,
					dto.FirstTourObjectCountWithGroupChanging,
					dto.SecondTourObjectCountWithGroupChanging,
					dto.FirstTourMinUpks.HasValue
						? Math.Round(dto.FirstTourMinUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.FirstTourMaxUpks.HasValue
						? Math.Round(dto.FirstTourMaxUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.FirstTourAverageUpks.HasValue
						? Math.Round(dto.FirstTourAverageUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.SecondTourMinUpks.HasValue
						? Math.Round(dto.SecondTourMinUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.SecondTourMaxUpks.HasValue
						? Math.Round(dto.SecondTourMaxUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.SecondTourAverageUpks.HasValue
						? Math.Round(dto.SecondTourAverageUpks.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.MinUpksVarianceBetweenTours.HasValue
						? Math.Round(dto.MinUpksVarianceBetweenTours.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.MaxUpksVarianceBetweenTours.HasValue
						? Math.Round(dto.MaxUpksVarianceBetweenTours.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.AverageUpksVarianceBetweenTours.HasValue
						? Math.Round(dto.AverageUpksVarianceBetweenTours.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.MinUpksVarianceBetweenToursWithoutGroupChanging.HasValue
						? Math.Round(dto.MinUpksVarianceBetweenToursWithoutGroupChanging.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.MaxUpksVarianceBetweenToursWithoutGroupChanging.HasValue
						? Math.Round(dto.MaxUpksVarianceBetweenToursWithoutGroupChanging.Value, PrecisionForDecimalValues)
						: (decimal?)null,
					dto.AverageUpksVarianceBetweenToursWithoutGroupChanging.HasValue
						? Math.Round(dto.AverageUpksVarianceBetweenToursWithoutGroupChanging.Value, PrecisionForDecimalValues)
						: (decimal?)null
					);
			}

			var dataSet = new DataSet();
			dataSet.Tables.Add(dataTable);
			dataSet.Tables.Add(dataTitleTable);

			return dataSet;
		}

		private IEnumerable<GroupTreeDto> GetSubgroupsForTour(long tourId)
		{
			var allGroups = GroupService.GetGroupsTreeForTour(tourId);

			var mainGroupId = (long)KoGroupAlgoritm.MainParcel;
			var mainGroups = allGroups.Where(x => x.Id == mainGroupId).SelectMany(x => x.Items);
			var subgroups = mainGroups.SelectMany(x => x.Items.Select(y => new GroupTreeDto { Id = y.Id, GroupName = $"{y.GroupName} ({x.GroupName})" }));

			return subgroups;
		}
	}

}
