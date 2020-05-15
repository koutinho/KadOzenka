using System;
using System.Collections.Generic;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Directory;

namespace KadOzenka.Web.Controllers
{
	public class ManagementDecisionSupportController : KoBaseController
	{
		private readonly MapBuildingService _mapBuildingService;
		private readonly DashboardWidgetService _dashboardWidgetService;

		public ManagementDecisionSupportController(MapBuildingService mapBuildingService, DashboardWidgetService dashboardWidgetService)
		{
			_mapBuildingService = mapBuildingService;
			_dashboardWidgetService = dashboardWidgetService;
		}

		#region MapBuilding

		public ActionResult Map()
		{
			var exceptions = new List<long> { (long)PropertyTypes.None };
			var segments = Helpers.EnumExtensions.GetSelectList(typeof(PropertyTypes), exceptions: exceptions);
			ViewBag.Segments = segments;

			return View();
		}

		public JsonResult HeatMapData(long tourId, PropertyTypes objectType, MapDivisionType divisionType, string colors)
		{
			var result = _mapBuildingService.GetHeatMapData(tourId, objectType, divisionType, colors.Split(","));
			return Json(result);
		}

		#endregion MapBuilding

		#region ObjectsByGroupsWidget

		[HttpGet]
		public JsonResult GetGroupsChartData()
		{
			var actualDate = DateTime.Now.Date;
			//var actualDate = new DateTime(2019, 2, 6);
			var chartGroupDtoList = _dashboardWidgetService.GetChartParentGroupsData(actualDate);

			return Json(new{ actualDate = actualDate, data = chartGroupDtoList});
		}

		public FileResult ExportGroupsChartDataToExcel()
		{
			var actualDate = DateTime.Now.Date;
			//var actualDate = new DateTime(2019, 2, 6);
			var file = _dashboardWidgetService.ExportChartDataToExcel(actualDate);
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
				$"Выгрузка загруженных объектов по группам (Дата актуальности: {actualDate.GetString()})" + ".xlsx");
		}

		#endregion ObjectsByGroupsWidget

		#region StatisticsReportsWidget

		public JsonResult GetUnitPropertyTypes()
		{
			var types = Helpers.EnumExtensions.GetSelectList(typeof(PropertyTypes));
			return Json(types);
		}

		public JsonResult GetZoneTypes()
		{
			var types = _dashboardWidgetService.GetZoneData();
			return Json(types);
		}

		public JsonResult GetImportedObjectsData(DateTime? dateStart, DateTime? dateEnd)
		{
			List<UnitObjectDto> data = _dashboardWidgetService.GetImportedObjectsData(dateStart, dateEnd);
			return Json(data);
		}

		public JsonResult GetExportedObjectsData(DateTime? dateStart, DateTime? dateEnd)
		{
			List<ExportedObjectDto> data = _dashboardWidgetService.GetExportedObjectsData(dateStart, dateEnd);
			return Json(data);

		}

		public JsonResult GetZoneStatisticsData(DateTime? dateStart, DateTime? dateEnd)
		{
			List<ZoneStatisticDto> data = _dashboardWidgetService.GetZoneStatisticsData(dateStart, dateEnd);
			return Json(data);
		}

		public JsonResult GetFactorStatisticsData(DateTime? dateStart, DateTime? dateEnd)
		{
			List<FactorStatisticDto> data = _dashboardWidgetService.GetFactorStatisticsData(dateStart, dateEnd);
			return Json(data);
		}

		public JsonResult GetGroupStatisticsData(DateTime? dateStart, DateTime? dateEnd)
		{
			List<GroupStatisticDto> data = _dashboardWidgetService.GetGroupStatisticsData(dateStart, dateEnd);
			return Json(data);
		}

		#endregion StatisticsReportsWidget
	}
}
