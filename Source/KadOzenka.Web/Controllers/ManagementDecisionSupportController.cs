using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.MapModeling;
using KadOzenka.Web.Attributes;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Models.ManagementDecisionSupport;
using Kendo.Mvc;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.KO;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
	public class ManagementDecisionSupportController : KoBaseController
	{
		private readonly MapBuildingService _mapBuildingService;
		private readonly DashboardWidgetService _dashboardWidgetService;
		private readonly StatisticsReportsService _statisticsReportsService;
		private readonly StatisticsReportsExportService _statisticsReportsExportService;
		private readonly TourService _tourService;

        public ManagementDecisionSupportController(MapBuildingService mapBuildingService,
            DashboardWidgetService dashboardWidgetService, StatisticsReportsService statisticsReportsService,
            StatisticsReportsExportService statisticsReportsExportService, TourService tourService)
        {
            _mapBuildingService = mapBuildingService;
            _dashboardWidgetService = dashboardWidgetService;
            _statisticsReportsService = statisticsReportsService;
            _statisticsReportsExportService = statisticsReportsExportService;
            _tourService = tourService;
        }

        #region MapBuilding

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT_THEME_MAPS)]
		public ActionResult Map()
		{
			var exceptions = new List<long> { (long)PropertyTypes.None };
			var segments = Helpers.EnumExtensions.GetSelectList(typeof(PropertyTypes), exceptions: exceptions);
			ViewBag.Segments = segments;
			ViewBag.MinZoom = _mapBuildingService.GetMapMinZoom();
			ViewBag.MaxZoom = _mapBuildingService.GetMapMaxZoom();

			return View();
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT_THEME_MAPS)]
		public JsonResult HeatMapData(long tourId, PropertyTypes objectType, MapDivisionType divisionType, string colors)
		{
			var result = _mapBuildingService.GetHeatMapData(tourId, objectType, divisionType, colors.Split(","));
			return Json(result);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT_THEME_MAPS)]
		public ActionResult CadastralHeatMapTiles(int x, int y, int z)
		{
			var file = _mapBuildingService.GetHeatMapTile(x, y, z);
			if (file == null)
				return EmptyResponse();

			return File(file, "image/png");
		}

		#endregion MapBuilding

		#region ObjectsByGroupsWidget

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public JsonResult GetGroupsChartData()
		{
			var actualDate = DateTime.Now.Date;
			//var actualDate = new DateTime(2019, 2, 6);
			var chartGroupDtoList = _dashboardWidgetService.GetChartParentGroupsData(actualDate);

			return Json(new{ actualDate = actualDate, data = chartGroupDtoList});
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
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

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public JsonResult GetUnitPropertyTypes()
		{
			var exceptions = new List<long> { (long)PropertyTypes.None };
			var types = Helpers.EnumExtensions.GetSelectList(typeof(PropertyTypes), exceptions: exceptions);

			return Json(types);
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public JsonResult GetZoneTypes()
		{
			var types = _statisticsReportsService.GetZoneData();
			return Json(types);
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetImportedObjectsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			GridDataDto<UnitObjectDto> data = _statisticsReportsService.GetImportedObjectsData(request, dateStart, dateEnd);
			return Json(data);
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public JsonResult GetExportedObjectsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			GridDataDto<ExportedObjectDto> data = _statisticsReportsService.GetExportedObjectsData(request, dateStart, dateEnd);
			return Json(data);
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public JsonResult GetZoneStatisticsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			GridDataDto<ZoneStatisticDto> data = _statisticsReportsService.GetZoneStatisticsData(request, dateStart, dateEnd);
			return Json(data);
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public JsonResult GetFactorStatisticsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			GridDataDto<FactorStatisticDto> data = _statisticsReportsService.GetFactorStatisticsData(request, dateStart, dateEnd);
			return Json(data);
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public JsonResult GetGroupStatisticsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			GridDataDto<GroupStatisticDto> data = _statisticsReportsService.GetGroupStatisticsData(request, dateStart, dateEnd);
			return Json(data);
		}

		#endregion StatisticsReportsWidget

		#region StatisticsReportsWidgetExport

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
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

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
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

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
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

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
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

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
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

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT_STATISTICS)]
		public ActionResult StatisticalData()
		{
			return View(new StatisticalDataModel());
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT_STATISTICS)]
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
			currentHttpContext.Session.SetString($"Report{fmReportTypeValue}TourId", JsonConvert.SerializeObject(model.TourId));
			currentHttpContext.Session.SetString($"Report{fmReportTypeValue}SecondTourId", JsonConvert.SerializeObject(model.SecondTourId));

			return Json(new { reportUrl = $"/Report/Viewer?reportTypeId={fmReportTypeValue}" });
		}


        #region Reports with additional configuration

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public ActionResult PreviousToursReportConfiguration(long? lastTourId)
        {
            var lastTour = _tourService.GetTourById(lastTourId);
            var previousTours = OMTour.Where(x => x.Year <= lastTour.Year).OrderBy(x => x.Year).SelectAll().Execute();

            var model = new PreviousToursConfigurationModel
            {
                AvailableTours = previousTours.Select(x => new SelectListItem
                {
                    Text = x.Year?.ToString(),
                    Value = x.Id.ToString()
                }).ToList()
            };

            return PartialView("~/Views/ManagementDecisionSupport/Partials/PreviousToursReportConfiguration.cshtml", model);
        }

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult GetPreviousToursReportReport(PreviousToursConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            if (model.IsInBackground)
            {
                var inputParameters = new PreviousToursReportInputParameters
                {
                    GroupId = model.GroupId.Value,
                    TaskIds = model.SelectedTasks.ToList()
                };

                ////TODO для тестирования
                //new PreviousToursReportProcess().StartProcess(new OMProcessType(), new OMQueue
                //{
                //    Status_Code = Status.Added,
                //    Parameters = inputParameters.SerializeToXml()
                //}, new CancellationToken());

                PreviousToursReportProcess.AddProcessToQueue(inputParameters);

                return Content(
                    JsonConvert.SerializeObject(new
                        {Message = "Процесс добавлен в очередь. Результат будет отправлен на почту."}),
                    "application/json");
            }

            return GetStatisticalDataReportUrl(model.Map());
        }

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT_STATISTICS)]
        public IActionResult AddUniformReportLongProcessToQueue(StatisticalDataModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var parameters = new UniformReportLongProcessInputParameters
	        {
		        TaskIds = model.TaskFilter.ToList()
	        };

			////TODO код для отладки
			//new UniformReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	Parameters = parameters.SerializeToXml()
			//}, new CancellationToken());

			UniformReportLongProcess.AddProcessToQueue(parameters);

			return Ok();
        }

		#endregion

		#endregion StatisticalData
	}
}
