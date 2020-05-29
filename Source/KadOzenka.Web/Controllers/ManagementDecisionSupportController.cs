using System;
using System.Collections.Generic;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Web.Models.ManagementDecisionSupport;
using Kendo.Mvc;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Directory;

namespace KadOzenka.Web.Controllers
{
	public class ManagementDecisionSupportController : KoBaseController
	{
		private readonly MapBuildingService _mapBuildingService;
		private readonly DashboardWidgetService _dashboardWidgetService;
		private readonly StatisticsReportsService _statisticsReportsService;
		private readonly StatisticsReportsExportService _statisticsReportsExportService;

		public ManagementDecisionSupportController(MapBuildingService mapBuildingService, DashboardWidgetService dashboardWidgetService, StatisticsReportsService statisticsReportsService, StatisticsReportsExportService statisticsReportsExportService)
		{
			_mapBuildingService = mapBuildingService;
			_dashboardWidgetService = dashboardWidgetService;
			_statisticsReportsService = statisticsReportsService;
			_statisticsReportsExportService = statisticsReportsExportService;
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
			var types = _statisticsReportsService.GetZoneData();
			return Json(types);
		}

		public ActionResult GetImportedObjectsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			GridDataDto<UnitObjectDto> data = _statisticsReportsService.GetImportedObjectsData(request, dateStart, dateEnd);
			return Json(data);
		}

		public JsonResult GetExportedObjectsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			GridDataDto<ExportedObjectDto> data = _statisticsReportsService.GetExportedObjectsData(request, dateStart, dateEnd);
			return Json(data);
		}

		public JsonResult GetZoneStatisticsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			GridDataDto<ZoneStatisticDto> data = _statisticsReportsService.GetZoneStatisticsData(request, dateStart, dateEnd);
			return Json(data);
		}

		public JsonResult GetFactorStatisticsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			GridDataDto<FactorStatisticDto> data = _statisticsReportsService.GetFactorStatisticsData(request, dateStart, dateEnd);
			return Json(data);
		}

		public JsonResult GetGroupStatisticsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			GridDataDto<GroupStatisticDto> data = _statisticsReportsService.GetGroupStatisticsData(request, dateStart, dateEnd);
			return Json(data);
		}

		#endregion StatisticsReportsWidget

		#region StatisticsReportsWidgetExport

		public FileResult ExportImportedObjects(string filters, string sorts, int pageSize, int page, DateTime? dateStart, DateTime? dateEnd)
		{
			var request = new DataSourceRequest();
			request.Filters = FilterDescriptorFactory.Create(filters);
			request.Sorts = DataSourceDescriptorSerializer.Deserialize<SortDescriptor>(sorts);
			request.PageSize = pageSize;
			request.Page = page;

			var file = _statisticsReportsExportService.ExportImportedObjects(request, dateStart, dateEnd);
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Выгрузка объектов.xlsx");
		}

		public FileResult ExportExportedObjects(string filters, string sorts, int pageSize, int page, DateTime? dateStart, DateTime? dateEnd)
		{
			var request = new DataSourceRequest();
			request.Filters = FilterDescriptorFactory.Create(filters);
			request.Sorts = DataSourceDescriptorSerializer.Deserialize<SortDescriptor>(sorts);
			request.PageSize = pageSize;
			request.Page = page;

			var file = _statisticsReportsExportService.ExportExportedObjects(request, dateStart, dateEnd);
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Выгрузка объектов.xlsx");
		}

		public FileResult ExportZoneStatistics(string filters, string sorts, int pageSize, int page, DateTime? dateStart, DateTime? dateEnd)
		{
			var request = new DataSourceRequest();
			request.Filters = FilterDescriptorFactory.Create(filters);
			request.Sorts = DataSourceDescriptorSerializer.Deserialize<SortDescriptor>(sorts);
			request.PageSize = pageSize;
			request.Page = page;

			var file = _statisticsReportsExportService.ExportZoneStatistics(request, dateStart, dateEnd);
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Выгрузка объектов.xlsx");
		}

		public FileResult ExportFactorStatistics(string filters, string sorts, int pageSize, int page, DateTime? dateStart, DateTime? dateEnd)
		{
			var request = new DataSourceRequest();
			request.Filters = FilterDescriptorFactory.Create(filters);
			request.Sorts = DataSourceDescriptorSerializer.Deserialize<SortDescriptor>(sorts);
			request.PageSize = pageSize;
			request.Page = page;

			var file = _statisticsReportsExportService.ExportFactorStatistics(request, dateStart, dateEnd);
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Выгрузка объектов.xlsx");
		}

		public FileResult ExportGroupStatistics(string filters, string sorts, int pageSize, int page, DateTime? dateStart, DateTime? dateEnd)
		{
			var request = new DataSourceRequest();
			request.Filters = FilterDescriptorFactory.Create(filters);
			request.Sorts = DataSourceDescriptorSerializer.Deserialize<SortDescriptor>(sorts);
			request.PageSize = pageSize;
			request.Page = page;

			var file = _statisticsReportsExportService.ExportGroupStatistics(request, dateStart, dateEnd);
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Выгрузка объектов.xlsx");
		}

		#endregion StatisticsReportsWidgetExport

		#region StatisticalData

		public ActionResult StatisticalData()
		{
			return View(new StatisticalDataModel());
		}

		public IActionResult GetStatisticalDataReportUrl(StatisticalDataModel model)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			var fmReportType =
				((StatisticalDataType)model.ReportType.Value).GetAttributeValue<StatisticalDataFmReportCodeAttribute>(
					nameof(StatisticalDataFmReportCodeAttribute.Code));
			if(!int.TryParse(fmReportType, out var fmReportTypeValue))
			{
				throw new Exception(
					$"Не удалось получить код отчета для типа '{((StatisticalDataType) model.ReportType.Value).GetEnumDescription()}'");
			}

			HttpContext currentHttpContext = HttpContextHelper.HttpContext;
			currentHttpContext.Session.SetString($"Report{fmReportTypeValue}TaskFilter", JsonConvert.SerializeObject(model.TaskFilter));

			return Json(new { reportUrl = $"/Report/Viewer?reportTypeId={fmReportTypeValue}" });
		}

		#endregion StatisticalData
	}
}
