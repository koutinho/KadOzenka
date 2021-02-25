using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.SessionManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.Common;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.LongProcess.ManagementDecisionSupport;
using KadOzenka.Dal.LongProcess.ManagementDecisionSupport.Settings;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Support;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest.Filter;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Web.Attributes;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Models.ManagementDecisionSupport;
using KadOzenka.Web.Models.ManagementDecisionSupport.QualityPricingFactorsEncodingResults;
using KadOzenka.Web.Models.ManagementDecisionSupport.ResultsByCadastralDistrictReport;
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
using ReportLongProcessOnlyTasksInputParameters = KadOzenka.Dal.LongProcess.Reports.Entities.ReportLongProcessOnlyTasksInputParameters;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
	public class ManagementDecisionSupportController : KoBaseController
	{
		private readonly MapBuildingService _mapBuildingService;
		private readonly DashboardWidgetService _dashboardWidgetService;
		private readonly StatisticsReportsWidgetService _statisticsReportsWidgetService;
		private readonly StatisticsReportsWidgetExportService _statisticsReportsWidgetExportService;
		private readonly StatisticalDataService _statisticalDataService;
		private readonly TourService _tourService;
		private GroupService GroupService { get; }
		private IGbuObjectService GbuObjectService { get; }
		private ILongProcessService LongProcessService { get; }
		private readonly int dataPageSize = 30;
		private readonly int dataCacheSize = 3000;

		public ManagementDecisionSupportController(MapBuildingService mapBuildingService,
            DashboardWidgetService dashboardWidgetService, StatisticsReportsWidgetService statisticsReportsWidgetService,
            StatisticsReportsWidgetExportService statisticsReportsWidgetExportService, TourService tourService,
            StatisticalDataService statisticalDataService, ILongProcessService longProcessService, IGbuObjectService gbuObjectService,
            GroupService groupService)
        {
            _mapBuildingService = mapBuildingService;
            _dashboardWidgetService = dashboardWidgetService;
            _statisticsReportsWidgetService = statisticsReportsWidgetService;
            _statisticsReportsWidgetExportService = statisticsReportsWidgetExportService;
            _tourService = tourService;
            _statisticalDataService = statisticalDataService;
            LongProcessService = longProcessService;
            GbuObjectService = gbuObjectService;
            GroupService = groupService;
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
			var types = _statisticsReportsWidgetService.GetZoneData();
			return Json(types);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetSessionKey()
		{
			SetNewUniqueSessionKey();
			if (CurrentUniqueSessionKey.IsNullOrEmpty())
			{
				throw new Exception("Не передан уникальный ключ сессии");
			}

			return Json(CurrentUniqueSessionKey);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetImportedObjectsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			request.PageSize = dataCacheSize;
			request.Page = 1;
			var data = _statisticsReportsWidgetService.GetImportedObjectsData(GetDataSourceRequest(request), dateStart, dateEnd);
			SessionManager.Set(SessionVariablesStatisticsReports.ImportedObjectsDataReader, data.Skip(dataPageSize).ToList());

			return Json(data.Take(dataPageSize).ToList());
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetImportedObjectsDataCount(string filters, string sorts, DateTime? dateStart, DateTime? dateEnd)
		{
			var request = GetDataSourceRequest(filters, sorts);
			var count = _statisticsReportsWidgetService.GetImportedObjectsDataCount(request, dateStart, dateEnd);
			return Json(count);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetAddImportedObjectsData(string filters, string sorts, int currentCount, int totalCount, DateTime? dateStart, DateTime? dateEnd)
		{
			var data = GetDataFromCache(filters, sorts, currentCount, totalCount, dateStart, dateEnd,
				SessionVariablesStatisticsReports.ImportedObjectsDataReader,
				_statisticsReportsWidgetService.GetImportedObjectsData);

			return Json(data);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public JsonResult GetExportedObjectsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			request.PageSize = dataCacheSize;
			request.Page = 1;
			var data = _statisticsReportsWidgetService.GetExportedObjectsData(GetDataSourceRequest(request), dateStart, dateEnd);
			SessionManager.Set(SessionVariablesStatisticsReports.ExportedObjectsDataReader, data.Skip(dataPageSize).ToList());

			return Json(data.Take(dataPageSize).ToList());
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetExportedObjectsDataCount(string filters, string sorts, DateTime? dateStart, DateTime? dateEnd)
		{
			var request = GetDataSourceRequest(filters, sorts);
			var count = _statisticsReportsWidgetService.GetExportedObjectsDataCount(request, dateStart, dateEnd);
			return Json(count);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetAddExportedObjectsData(string filters, string sorts, int currentCount, int totalCount, DateTime? dateStart, DateTime? dateEnd)
		{
			var data = GetDataFromCache(filters, sorts, currentCount, totalCount, dateStart, dateEnd,
				SessionVariablesStatisticsReports.ExportedObjectsDataReader,
				_statisticsReportsWidgetService.GetExportedObjectsData);

			return Json(data);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public JsonResult GetZoneStatisticsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			request.PageSize = dataCacheSize;
			request.Page = 1;
			var data = _statisticsReportsWidgetService.GetZoneStatisticsData(GetDataSourceRequest(request), dateStart, dateEnd);
			SessionManager.Set(SessionVariablesStatisticsReports.ZoneStatisticsDataReader, data.Skip(dataPageSize).ToList());

			return Json(data.Take(dataPageSize).ToList());
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetZoneStatisticsDataCount(string filters, string sorts, DateTime? dateStart, DateTime? dateEnd)
		{
			var request = GetDataSourceRequest(filters, sorts);
			var count = _statisticsReportsWidgetService.GetZoneStatisticsDataCount(request, dateStart, dateEnd);
			return Json(count);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetAddZoneStatisticsData(string filters, string sorts, int currentCount, int totalCount, DateTime? dateStart, DateTime? dateEnd)
		{
			var data = GetDataFromCache(filters, sorts, currentCount, totalCount, dateStart, dateEnd,
				SessionVariablesStatisticsReports.ZoneStatisticsDataReader,
				_statisticsReportsWidgetService.GetZoneStatisticsData);

			return Json(data);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public JsonResult GetFactorStatisticsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			request.PageSize = dataCacheSize;
			request.Page = 1;
			var data = _statisticsReportsWidgetService.GetFactorStatisticsData(GetDataSourceRequest(request), dateStart, dateEnd);
			SessionManager.Set(SessionVariablesStatisticsReports.FactorStatisticsDataReader, data.Skip(dataPageSize).ToList());

			return Json(data.Take(dataPageSize).ToList());
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetFactorStatisticsDataCount(string filters, string sorts, DateTime? dateStart, DateTime? dateEnd)
		{
			var request = GetDataSourceRequest(filters, sorts);
			var count = _statisticsReportsWidgetService.GetFactorStatisticsDataCount(request, dateStart, dateEnd);
			return Json(count);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetAddFactorStatisticsData(string filters, string sorts, int currentCount, int totalCount, DateTime? dateStart, DateTime? dateEnd)
		{
			var data = GetDataFromCache(filters, sorts, currentCount, totalCount, dateStart, dateEnd,
				SessionVariablesStatisticsReports.FactorStatisticsDataReader,
				_statisticsReportsWidgetService.GetFactorStatisticsData);

			return Json(data);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public JsonResult GetGroupStatisticsData([DataSourceRequest]DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
		{
			request.PageSize = dataCacheSize;
			request.Page = 1;
			var data = _statisticsReportsWidgetService.GetGroupStatisticsData(GetDataSourceRequest(request), dateStart, dateEnd);
			SessionManager.Set(SessionVariablesStatisticsReports.GroupStatisticsDataReader, data.Skip(dataPageSize).ToList());

			return Json(data.Take(dataPageSize).ToList());
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetGroupStatisticsDataCount(string filters, string sorts, DateTime? dateStart, DateTime? dateEnd)
		{
			var request = GetDataSourceRequest(filters, sorts);
			var count = _statisticsReportsWidgetService.GetGroupStatisticsDataCount(request, dateStart, dateEnd);
			return Json(count);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public ActionResult GetAddGroupStatisticsData(string filters, string sorts, int currentCount, int totalCount, DateTime? dateStart, DateTime? dateEnd)
		{
			var data = GetDataFromCache(filters, sorts, currentCount, totalCount, dateStart, dateEnd,
				SessionVariablesStatisticsReports.GroupStatisticsDataReader,
				_statisticsReportsWidgetService.GetGroupStatisticsData);

			return Json(data);
		}

		#endregion StatisticsReportsWidget

		#region StatisticsReportsWidgetExport

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public IActionResult ExportImportedObjects(string filters, string sorts, DateTime? dateStart, DateTime? dateEnd, bool backgroundExport = false)
		{
			var request = GetDataSourceRequest(filters, sorts);

			if (backgroundExport)
			{
				//var processType = OMProcessType.Where(x => x.ProcessName == StatisticsReportWidgetExportLongProcess.LongProcessName)
				//	.SelectAll().ExecuteFirstOrDefault();
				//var queue = new OMQueue
				//{
				//	Status_Code = Status.Added,
				//	UserId = SRDSession.GetCurrentUserId(),
				//	Parameters = (new StatisticsReportWidgetExportLongProcessSettings
				//	{
				//		DataSourceRequest = request, DateStart = dateStart, DateEnd = dateEnd,
				//		StatisticsReportExportType = StatisticsReportExportType.ImportedObjects
				//	}).SerializeToXml()
				//};
				//new StatisticsReportWidgetExportLongProcess().StartProcess(processType, queue
				//	, new CancellationTokenSource().Token);
				//queue.Status_Code = Status.Completed;
				//queue.EndDate = DateTime.Now;
				//queue.Save();
				StatisticsReportWidgetExportLongProcess.AddProcessToQueue(new StatisticsReportWidgetExportLongProcessSettings
				{ DataSourceRequest = request, DateStart = dateStart, DateEnd = dateEnd, StatisticsReportExportType = StatisticsReportExportType.ImportedObjects });
				return Ok();
			}
			else
			{
				var exportResult =
					_statisticsReportsWidgetExportService.ExportImportedObjects(request, dateStart, dateEnd);
				return File(exportResult.ReportFile.FileStream, GetContentTypeByExtension(System.IO.Path.GetExtension(exportResult.ReportFile.FileName)),
					System.IO.Path.GetFileName(exportResult.ReportFile.FileName));
			}
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public IActionResult ExportExportedObjects(string filters, string sorts, DateTime? dateStart, DateTime? dateEnd, bool backgroundExport = false)
		{
			var request = GetDataSourceRequest(filters, sorts);
			if (backgroundExport)
			{
				StatisticsReportWidgetExportLongProcess.AddProcessToQueue(new StatisticsReportWidgetExportLongProcessSettings
					{ DataSourceRequest = request, DateStart = dateStart, DateEnd = dateEnd, StatisticsReportExportType = StatisticsReportExportType.ExportedObjects });
				return Ok();
			}
			else
			{
				var exportResult = _statisticsReportsWidgetExportService.ExportExportedObjects(request, dateStart, dateEnd);
				return File(exportResult.ReportFile.FileStream, GetContentTypeByExtension(System.IO.Path.GetExtension(exportResult.ReportFile.FileName)),
					System.IO.Path.GetFileName(exportResult.ReportFile.FileName));
			}
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public IActionResult ExportZoneStatistics(string filters, string sorts, DateTime? dateStart, DateTime? dateEnd, bool backgroundExport = false)
		{
			var request = GetDataSourceRequest(filters, sorts);
			if (backgroundExport)
			{
				StatisticsReportWidgetExportLongProcess.AddProcessToQueue(new StatisticsReportWidgetExportLongProcessSettings
					{ DataSourceRequest = request, DateStart = dateStart, DateEnd = dateEnd, StatisticsReportExportType = StatisticsReportExportType.ZoneStatistics });
				return Ok();
			}
			else
			{
				var exportResult = _statisticsReportsWidgetExportService.ExportZoneStatistics(request, dateStart, dateEnd);
				return File(exportResult.ReportFile.FileStream, GetContentTypeByExtension(System.IO.Path.GetExtension(exportResult.ReportFile.FileName)),
					System.IO.Path.GetFileName(exportResult.ReportFile.FileName));
			}
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public IActionResult ExportFactorStatistics(string filters, string sorts, DateTime? dateStart, DateTime? dateEnd, bool backgroundExport = false)
		{
			var request = GetDataSourceRequest(filters, sorts);
			if (backgroundExport)
			{
				StatisticsReportWidgetExportLongProcess.AddProcessToQueue(new StatisticsReportWidgetExportLongProcessSettings
					{ DataSourceRequest = request, DateStart = dateStart, DateEnd = dateEnd, StatisticsReportExportType = StatisticsReportExportType.FactorStatistics });
				return Ok();
			}
			else
			{
				var exportResult = _statisticsReportsWidgetExportService.ExportFactorStatistics(request, dateStart, dateEnd);
				return File(exportResult.ReportFile.FileStream, GetContentTypeByExtension(System.IO.Path.GetExtension(exportResult.ReportFile.FileName)),
					System.IO.Path.GetFileName(exportResult.ReportFile.FileName));
			}
		}

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
		public IActionResult ExportGroupStatistics(string filters, string sorts, DateTime? dateStart, DateTime? dateEnd, bool backgroundExport = false)
		{
			var request = GetDataSourceRequest(filters, sorts);
			if (backgroundExport)
			{
				//var processType = OMProcessType.Where(x => x.ProcessName == StatisticsReportWidgetExportLongProcess.LongProcessName)
				//	.SelectAll().ExecuteFirstOrDefault();
				//var queue = new OMQueue
				//{
				//	Status_Code = Status.Added,
				//	UserId = SRDSession.GetCurrentUserId(),
				//	Parameters = (new StatisticsReportWidgetExportLongProcessSettings
				//	{
				//		DataSourceRequest = request,
				//		DateStart = dateStart,
				//		DateEnd = dateEnd,
				//		StatisticsReportExportType = StatisticsReportExportType.GroupStatistics
				//	}).SerializeToXml()
				//};
				//new StatisticsReportWidgetExportLongProcess().StartProcess(processType, queue
				//	, new CancellationTokenSource().Token);
				//queue.Status_Code = Status.Completed;
				//queue.EndDate = DateTime.Now;
				//queue.Save();

				StatisticsReportWidgetExportLongProcess.AddProcessToQueue(new StatisticsReportWidgetExportLongProcessSettings
				{ DataSourceRequest = request, DateStart = dateStart, DateEnd = dateEnd, StatisticsReportExportType = StatisticsReportExportType.GroupStatistics });
				return Ok();
			}
			else
			{
				var exportResult = _statisticsReportsWidgetExportService.ExportGroupStatistics(request, dateStart, dateEnd);
				return File(exportResult.ReportFile.FileStream, GetContentTypeByExtension(System.IO.Path.GetExtension(exportResult.ReportFile.FileName)),
					System.IO.Path.GetFileName(exportResult.ReportFile.FileName));
			}
		}

		#endregion StatisticsReportsWidgetExport

		#region StatisticalData

        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT_STATISTICS)]
		public ActionResult StatisticalData()
		{
			var dataCompositionByCharacteristicReportsActualizationDate =
				LongProcessService.GetLastSuccessfulCompletedQueue(ReportTableUpdater.ProcessId)?.EndDate ??
				LongProcessService.GetLastSuccessfulCompletedQueue(InitialReportTableFiller.ProcessId)?.EndDate;

			var model = new StatisticalDataModel
			{
				PricingFactorsCompositionFinalUniformReportActualizationDate = dataCompositionByCharacteristicReportsActualizationDate,
				PricingFactorsCompositionFinalNonuniformActualizationDate = dataCompositionByCharacteristicReportsActualizationDate
			};

			return View(model);
		}

		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT_STATISTICS)]
		public IActionResult ProcessReport(StatisticalDataModel model)
		{
			if (!ModelState.IsValid)  
				return GenerateMessageNonValidModel();

			//есть три типа отчетов
			//1. отчеты, которые идут через платформу - GetStatisticalDataReportUrl(model)
			//2. отчеты, которые идут через свои собственные процессы и не требуют дополнительных входных параметров - model.IsForBackground
			//3. отчеты, которые идут через свои собственные процессы и требуют дополнительных входных параметров - model.IsWithAdditionalConfiguration
			if (model.IsForBackground)
			{
				var parameters = new ReportLongProcessOnlyTasksInputParameters
				{
					TaskIds = model.TaskFilter.ToList()
				};
				_statisticalDataService.AddProcessToQueue(model.ReportType, parameters);
				
				return Ok();
			}
			if (model.IsWithAdditionalConfiguration)
			{
				if (!model.ReportsWithAdditionalConfiguration.TryGetValue(model.ReportType.GetValueOrDefault(), out var actionName))
					throw new Exception("Формирование отчета с дополнительной конфигурацией недопустимо. Обратитесь к администратору.");

				return RedirectToAction(actionName, model);
			}

			return GetStatisticalDataReportUrl(model);
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
        public ActionResult PreviousToursReportConfiguration(StatisticalDataModel model)
        {
            var lastTour = _tourService.GetTourById(model.TourId);
            var previousTours = OMTour.Where(x => x.Year <= lastTour.Year).OrderBy(x => x.Year).SelectAll().Execute();

            var reportConfigurationModel = new PreviousToursConfigurationModel
            {
                AvailableTours = previousTours.Select(x => new SelectListItem
                {
                    Text = x.Year?.ToString(),
                    Value = x.Id.ToString()
                }).ToList()
            };

            return PartialView("~/Views/ManagementDecisionSupport/Partials/PreviousToursReportConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult ProcessPreviousToursReportReport(PreviousToursConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var inputParameters = model.MapToInputParameters();

			////TODO для тестирования
			//new PreviousToursReportProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

	        PreviousToursReportProcess.AddProcessToQueue(inputParameters);

			return Ok();
		}

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public ActionResult CadastralCostDeterminationResultsConfiguration(StatisticalDataModel model)
        {
	        var reportConfigurationModel = new CadastralCostDeterminationResultsModel
			{
		       TaskIds = model.TaskFilter
	        };

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/CadastralCostDeterminationResultsConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult ProcessCadastralCostDeterminationResultsReport(CadastralCostDeterminationResultsModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

			var inputParameters = new Dal.LongProcess.Reports.CadastralCostDeterminationResults.Entities.ReportLongProcessInputParameters
			{
				TaskIds = model.TaskIds?.ToList(),
				Type = model.ReportType
			};

			////TODO для тестирования
			//new Dal.LongProcess.Reports.CadastralCostDeterminationResults.CadastralCostDeterminationResultsBaseReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

			new Dal.LongProcess.Reports.CadastralCostDeterminationResults.CadastralCostDeterminationResultsBaseReportLongProcess().AddToQueue(inputParameters);

			return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public ActionResult ResultsByCadastralDistrictForZuConfiguration(StatisticalDataModel model)
        {
	        var reportConfigurationModel = new ZuConfigurationModel
			{
		        TaskIds = model.TaskFilter,
		        GbuAttributes = GetGbuAttributesTree()
			};

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/ResultsByCadastralDistrict/ZuConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult ProcessResultsByCadastralDistrictForZuReport(ZuConfigurationModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var inputParameters = model.MapToInputParameters();

			////TODO для тестирования
			//new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForZuReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

			new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForZuReportLongProcess().AddToQueue(inputParameters);

			return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public ActionResult ResultsByCadastralDistrictForBuildingsConfiguration(StatisticalDataModel model)
        {
	        var reportConfigurationModel = new BuildingsConfigurationModel
			{
		        TaskIds = model.TaskFilter,
		        GbuAttributes = GetGbuAttributesTree()
			};

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/ResultsByCadastralDistrict/BuildingsConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult ProcessResultsByCadastralDistrictForBuildingsReport(BuildingsConfigurationModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var inputParameters = model.MapToInputParameters();

			////TODO для тестирования
			//new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForBuildingsReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

			new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForBuildingsReportLongProcess().AddToQueue(inputParameters);

			return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public ActionResult ResultsByCadastralDistrictForConstructionsConfiguration(StatisticalDataModel model)
        {
	        var reportConfigurationModel = new ConstructionsConfigurationModel
			{
		        TaskIds = model.TaskFilter,
		        GbuAttributes = GetGbuAttributesTree()
	        };

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/ResultsByCadastralDistrict/ConstructionsConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult ProcessResultsByCadastralDistrictForConstructionsReport(ConstructionsConfigurationModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var inputParameters = model.MapToInputParameters();

			////TODO для тестирования
			//new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForConstructionsReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

			new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForConstructionsReportLongProcess().AddToQueue(inputParameters);

			return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public ActionResult ResultsByCadastralDistrictForUncompletedBuildingsConfiguration(StatisticalDataModel model)
        {
	        var reportConfigurationModel = new UncompletedBuildingsConfigurationModel
			{
		        TaskIds = model.TaskFilter,
		        GbuAttributes = GetGbuAttributesTree()
	        };

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/ResultsByCadastralDistrict/UncompletedBuildingsConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult ProcessResultsByCadastralDistrictForUncompletedBuildingsReport(UncompletedBuildingsConfigurationModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var inputParameters = model.MapToInputParameters();

			////TODO для тестирования
			//new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForUncompletedBuildingsReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

			new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForUncompletedBuildingsReportLongProcess().AddToQueue(inputParameters);

			return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public ActionResult ResultsByCadastralDistrictForPlacementsConfiguration(StatisticalDataModel model)
        {
	        var reportConfigurationModel = new PlacementsConfigurationModel
			{
		        TaskIds = model.TaskFilter,
		        GbuAttributes = GetGbuAttributesTree()
	        };

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/ResultsByCadastralDistrict/PlacementsConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult ProcessResultsByCadastralDistrictForPlacementsReport(PlacementsConfigurationModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var inputParameters = model.MapToInputParameters();

			////TODO для тестирования
			//new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForPlacementsReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

			new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForPlacementsReportLongProcess().AddToQueue(inputParameters);

			return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public ActionResult ResultsByCadastralDistrictForParkingsConfiguration(StatisticalDataModel model)
        {
	        var reportConfigurationModel = new ParkingsConfigurationModel
			{
		        TaskIds = model.TaskFilter,
		        GbuAttributes = GetGbuAttributesTree()
	        };

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/ResultsByCadastralDistrict/ParkingsConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult ProcessResultsByCadastralDistrictForParkingsReport(ParkingsConfigurationModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var inputParameters = model.MapToInputParameters();

			////TODO для тестирования
			//new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForParkingsReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

			new Dal.LongProcess.Reports.ResultsByCadastralDistrict.ResultsByCadastralDistrictForParkingsReportLongProcess().AddToQueue(inputParameters);

			return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public ActionResult ModelingsResultsConfiguration(StatisticalDataModel model)
        {
			var possibleGroups = GroupService.GetSortedGroupsWithNumbersByTasks(model.TaskFilter.ToList())
				.Select(x => new DropDownTreeItemModel { Value = x.Id.ToString(), Text = x.CombinedName }).ToList();

			var reportConfigurationModel = new ModelingResultsModel
			{
		        TaskIds = model.TaskFilter,
		        PossibleGroups = possibleGroups
			};

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/ModelingResultsConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult ProcessModelingsResultsReport(ModelingResultsModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var inputParameters = model.MapToInputParameters();

			////TODO для тестирования
			//new Dal.LongProcess.Reports.CalculationParams.ModelingResultsLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

			new Dal.LongProcess.Reports.CalculationParams.ModelingResultsLongProcess().AddToQueue(inputParameters);

			return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public ActionResult DataCompositionWithCrviForOksConfiguration(StatisticalDataModel model)
        {
	        var reportConfigurationModel = new DataCompositionWithCrviForOksConfigurationModel
			{
		        TaskIds = model.TaskFilter,
		        GbuAttributes = GetGbuAttributesTree()
	        };

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/QualityPricingFactorsEncodingResults/DataCompositionWithCrviForOksConfiguration.cshtml", reportConfigurationModel);
		}

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult ProcessDataCompositionWithCrviForOksReport(DataCompositionWithCrviForOksConfigurationModel model)
        {
			if (!ModelState.IsValid)
				return GenerateMessageNonValidModel();

			var inputParameters = model.MapToInputParameters();

			////TODO для тестирования
			//new Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults.DataCompositionWithCrviForOksReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

			new Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults.DataCompositionWithCrviForOksReportLongProcess().AddToQueue(inputParameters);

			return Ok();
		}

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult KRSummaryResultsOksConfiguration(StatisticalDataModel model)
        {
	        var reportConfigurationModel = new KRSummaryResultsOksModel
	        {
		        TaskIds = model.TaskFilter,
		        GbuAttributes = GetGbuAttributesTree()
	        };

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/KRSummaryResultsOksConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult KRSummaryResultsOksConfiguration(KRSummaryResultsOksModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var inputParameters = new Dal.LongProcess.Reports.KRSummaryResults.Entities.OksReportLongProcessInputParameters()
	        {
		        TaskIds = model.TaskIds?.ToList(),
		        KladrAttributeId = model.KladrAttributeId,
		        ParentKnAttributeId = model.ParentKnAttributeId
	        };

	        ////TODO для тестирования
//	        new Dal.LongProcess.Reports.KRSummaryResults.OksReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
//	        {
//	        	Status_Code = Status.Added,
//	        	Parameters = inputParameters.SerializeToXml()
//	        }, new CancellationToken());

	        new Dal.LongProcess.Reports.KRSummaryResults.OksReportLongProcess().AddToQueue(inputParameters);

	        return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult KRSummaryResultsZuConfiguration(StatisticalDataModel model)
        {
	        var reportConfigurationModel = new KRSummaryResultsZuModel
	        {
		        TaskIds = model.TaskFilter,
		        GbuAttributes = GetGbuAttributesTree()
	        };

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/KRSummaryResultsZuConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult KRSummaryResultsZuConfiguration(KRSummaryResultsZuModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var inputParameters = new Dal.LongProcess.Reports.KRSummaryResults.Entities.ZuReportLongProcessInputParameters()
	        {
		        TaskIds = model.TaskIds?.ToList(),
		        KladrAttributeId = model.KladrAttributeId,
	        };

	        ////TODO для тестирования
	        new Dal.LongProcess.Reports.KRSummaryResults.ZuReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
	        {
	        	Status_Code = Status.Added,
	        	Parameters = inputParameters.SerializeToXml()
	        }, new CancellationToken());

	        //new Dal.LongProcess.Reports.KRSummaryResults.ZuReportLongProcess().AddToQueue(inputParameters);

	        return Ok();
        }


        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public ActionResult DataCompositionWithCrviForZuConfiguration(StatisticalDataModel model)
        {
	        var reportConfigurationModel = new DataCompositionWithCrviForZuConfigurationModel
			{
		        TaskIds = model.TaskFilter,
		        GbuAttributes = GetGbuAttributesTree()
	        };

	        return PartialView("~/Views/ManagementDecisionSupport/Partials/QualityPricingFactorsEncodingResults/DataCompositionWithCrviForZuConfiguration.cshtml", reportConfigurationModel);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DECISION_SUPPORT)]
        public IActionResult ProcessDataCompositionWithCrviForZuReport(DataCompositionWithCrviForZuConfigurationModel model)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var inputParameters = model.MapToInputParameters();

			////TODO для тестирования
			//new Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults.DataCompositionWithCrviForZuReportLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

			new Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults.DataCompositionWithCrviForZuReportLongProcess().AddToQueue(inputParameters);

			return Ok();
        }

		#endregion

		#endregion StatisticalData

		#region Helpers

		private List<T> GetDataFromCache<T>(string filters, string sorts, int currentCount, int totalCount, DateTime? dateStart,
			DateTime? dateEnd, SessionVariable<List<T>> sessionVariable, Func<DataSourceRequestDto, DateTime?, DateTime?, List<T>> dataFunc) where T : UnitObjectDto
		{
			List<T> data;
			var reader = SessionManager.Get(sessionVariable);
			if (reader.IsEmpty() && currentCount < totalCount)
			{
				var request = GetDataSourceRequest(filters, sorts);
				request.PageSize = dataCacheSize;
				request.Page = currentCount / dataCacheSize + 1;
				data = dataFunc(request, dateStart, dateEnd);
				SessionManager.Set(sessionVariable,
					data.Skip(currentCount % dataCacheSize + dataPageSize).ToList());
				data = data.Skip(currentCount % dataCacheSize).Take(dataPageSize).ToList();
			}
			else
			{
				data = reader.Take(dataPageSize).ToList();
				if (reader.Count >= dataPageSize)
				{
					reader.RemoveRange(0, dataPageSize);
				}
				else
				{
					reader.Clear();
				}

				SessionManager.Set(sessionVariable, reader);
			}

			return data;
		}

		private DataSourceRequestDto GetDataSourceRequest(string filters, string sorts, int pageSize = 0, int page = 0)
        {
	        var kendoRequest = new DataSourceRequest
	        {
		        Filters = FilterDescriptorFactory.Create(filters),
		        Sorts = DataSourceDescriptorSerializer.Deserialize<SortDescriptor>(sorts),
		        PageSize = pageSize,
		        Page = page
	        };

	        return GetDataSourceRequest(kendoRequest);
        }

        private DataSourceRequestDto GetDataSourceRequest(DataSourceRequest kendoRequest)
        {
	        var request = new DataSourceRequestDto();
	        request.PageSize = kendoRequest.PageSize;
	        request.Page = kendoRequest.Page;

	        request.Sorts = new List<SortDto>();
	        if (kendoRequest.Sorts.Any())
	        {
		        foreach (var kendoSort in kendoRequest.Sorts)
		        {
			        var sort = new SortDto();
			        sort.Member = kendoSort.Member;
			        sort.SortDirection = kendoSort.SortDirection == ListSortDirection.Ascending
				        ? SortDirectionType.Ascending
				        : SortDirectionType.Descending;
			        request.Sorts.Add(sort);
		        }
	        }

	        request.Filters = new List<FilterDto>();
	        AddDataSourceRequestFilters(request.Filters, kendoRequest.Filters);

	        return request;
		}

		private void AddDataSourceRequestFilters(List<FilterDto> filters, IList<IFilterDescriptor> kendoFilters)
        {
	        if (kendoFilters.Any())
	        {
		        foreach (var kendoFilter in kendoFilters)
		        {
			        if (kendoFilter is FilterDescriptor descriptor)
			        {
				        var filter = new FilterSimpleDto
				        {
					        Member = descriptor.Member,
					        Value = descriptor.Value,
					        Operator = GetFilterOperatorType(descriptor.Operator)
				        };
				        filters.Add(filter);
					}
			        else if (kendoFilter is CompositeFilterDescriptor compositeFilterDescriptor)
			        {
				        var filter = new FilterCompositeDto {Filters = new List<FilterDto>()};
				        AddDataSourceRequestFilters(filter.Filters, compositeFilterDescriptor.FilterDescriptors);
						filters.Add(filter);
			        }
		        }
	        }
		}

        private FilterOperatorType GetFilterOperatorType(FilterOperator kendoOperator)
        {
	        switch (kendoOperator)
	        {
		        case FilterOperator.Contains:
			        return FilterOperatorType.Contains;
		        case FilterOperator.IsEqualTo:
			        return FilterOperatorType.Equal;
		        default:
			        return FilterOperatorType.Equal;
	        }
		}

		#endregion Helpers
	}
}
