using System;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Models.GbuObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.ErrorManagment;
using Core.Register;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.GbuLongProcesses;
using KadOzenka.Dal.LongProcess.TaskLongProcesses;
using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.GbuObject.ObjectAttributes;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Common;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.Gbu;
using ObjectModel.KO;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
    public class GbuObjectController : KoBaseController
	{
		#region Initialization

		private readonly GbuObjectService _service;
		private readonly TaskService _taskService;
	    private TourFactorService _tourFactorService;
	    private TemplateService _templateService;

        public GbuObjectController(GbuObjectService service, TaskService taskService, TourFactorService tourFactorService, TemplateService templateService)
		{
			_service = service;
			_taskService = taskService;
		    _tourFactorService = tourFactorService;
            _templateService = templateService;
        }

		#endregion

		#region Object Card

        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_ALL_DATA)]
		public ActionResult AllDataTree(long objectId)
		{
			return View(objectId);
		}

        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public ActionResult TreeList(long objectId, string parentNodeId, long nodeLevel)
		{
			List<AllDataTreeDto> treeList = _service.GetAllDataTree(objectId, parentNodeId, nodeLevel);

			return Content(JsonConvert.SerializeObject(treeList), "application/json");
		}

        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public ActionResult AllDetails(long objectId, long? registerId = null, long? attributeId = null)
		{
			List<long> sources = null;

			if (registerId != null)
			{
				sources = new List<long> { registerId.Value };
			}

			List<long> attributes = null;

			if (attributeId != null)
			{
				attributes = new List<long> { attributeId.Value };
			}

			var sttributesValues = _service.GetAllAttributes(objectId, sources, attributes);

			return View(sttributesValues);
		}

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public ActionResult GbuObjectCard(long objectId)
		{
			var obj = OMMainObject.Where(x => x.Id == objectId).SelectAll().ExecuteFirstOrDefault();
		    var registerDtoList = GetRegisterDtoList(obj, DateTime.Now);

            return View("~/Views/GbuObject/GbuObjectCardNew.cshtml", GbuObjectViewModel.FromEntity(obj, DateTime.Now, registerDtoList));
		}

        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public ActionResult GetGbuObjectCardPanelBar(long objectId, DateTime? actualDate)
		{
		    var obj = OMMainObject.Where(x => x.Id == objectId).SelectAll().ExecuteFirstOrDefault();
		    var registerDtoList = GetRegisterDtoList(obj, actualDate);

            return PartialView("~/Views/GbuObject/GbuObjectCardPartials/_gbuObjectCardPanelBar.cshtml", GbuObjectViewModel.FromEntity(obj, actualDate?.Date ?? DateTime.Now.Date, registerDtoList));
		}

	    private List<RegisterDto> GetRegisterDtoList(OMMainObject obj, DateTime? actualDate)
	    {
	        var registerDtoList = new List<RegisterDto>();
	        if (obj != null)
	        {
	            var ts = new TimeSpan(23, 59, 59);
	            var date = actualDate.HasValue ? actualDate.Value.Date + ts : DateTime.Now.Date + ts;
	            var mainRegister = RegisterCache.GetRegisterData(ObjectModel.Gbu.OMMainObject.GetRegisterId());
	            var getSources = RegisterCache.Registers.Values.Where(x =>
	                x.QuantTable == mainRegister.QuantTable && x.Id != mainRegister.Id && x.Id != 1).ToList();
	            foreach (var source in getSources)
	            {
	                var objAttributes = _service
	                    .GetAllAttributes(obj.Id, new List<long> { source.Id }, null, date)
	                    .ToList();
	                if (objAttributes.Count > 0)
	                {
	                    registerDtoList.Add(new RegisterDto(source.Id, obj.Id, source.Description, objAttributes));
	                }
	            }
	        }

	        return registerDtoList;
	    }

        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public ActionResult GetAttributeHistory(long objectId, long registerId, long attrId)
	    {
	        var attributeValues = _service
	            .GetAllAttributes(objectId, new List<long> { registerId }, new List<long> { attrId })
	            .OrderByDescending(x => x.Id)
	            .ToList();
	        var model = new List<AttributeHistoryRecordDto>();
	        foreach (var attributeValue in attributeValues)
	        {
	            model.Add(new AttributeHistoryRecordDto(attributeValue));
	        }

            return View("~/Views/GbuObject/AttributeHistory.cshtml", model);
        }

        #endregion

        #region Нормализация

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_GROUPING_OBJECT)]
		public ActionResult GroupingObject()
		{
			ViewData["CodJob"] = OMCodJob.Where(x => x).SelectAll().Execute().Select(x => new
			{
				Value = x.Id,
				Text = x.NameJob
			}).AsEnumerable();

			ViewBag.TreeAttributes = GetGbuAttributesTree();
            ViewBag.StringTreeAttributes = GetGbuAttributesTree(new List<RegisterAttributeType> { RegisterAttributeType.STRING });

			return View(new GroupingObject());
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_GROUPING_OBJECT)]
		public JsonResult GroupingObject(GroupingObject model)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			if (model.IsNewAttribute)
			{
				int idAttr = _service.AddNewVirtualAttribute(model.NameNewAttribute, model.RegistryId.GetValueOrDefault(), model.TypeNewAttribute ?? RegisterAttributeType.INTEGER);
				if (idAttr == 0)
				{
					return SendErrorMessage("Не корректные данные для создания нового атрибута");
				}

				model.IdAttributeResult = idAttr;
			}

            long queueId;
            try
            {
                var settings = model.CovertToGroupingSettings();
				////TODO код для отладки
				//new SetPriorityGroupProcess().StartProcess(new OMProcessType(), new OMQueue
				//{
				//	Status_Code = Status.Added,
				//	UserId = SRDSession.GetCurrentUserId(),
				//	Parameters = settings.SerializeToXml()
				//}, new CancellationToken());
				//queueId = 0;
				queueId = SetPriorityGroupProcess.AddProcessToQueue(settings);
			}
			catch (Exception e)
			{
				return SendErrorMessage(e.Message);
			}

		    return Json(new
		    {
		        success = true,
		        idResultAttribute = model.IsNewAttribute ? model.IdAttributeResult : null,
                QueueId = queueId
            });
		}
		#endregion

		#region Harmonization

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_HARMONIZATION)]
		public ActionResult Harmonization()
		{
			//ViewData["TreeAttributes"] = GetGbuAttributesTree();
            ViewBag.TreeAttributes = GetGbuAttributesTree();

			var viewModel = new HarmonizationViewModel();

			return View(viewModel);
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_HARMONIZATION)]
		public ActionResult Harmonization(HarmonizationViewModel viewModel)
		{
			if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            if (viewModel.IsNewAttribute)
			{
				int idAttr = _service.AddNewVirtualAttribute(viewModel.NameNewAttribute, viewModel.RegistryId.GetValueOrDefault(), viewModel.TypeNewAttribute ?? RegisterAttributeType.INTEGER);
				if (idAttr == 0)
				{
					return SendErrorMessage("Не корректные данные для создания нового атрибута");
				}

				viewModel.IdAttributeResult = idAttr;
			}

            long queueId;
			try
			{
                //TODO для тестирования (без процесса)
                //new Harmonization(viewModel.ToHarmonizationSettings()).Run();
                ////TODO для тестирования (с процессом)
                //new HarmonizationProcess().StartProcess(new OMProcessType(), new OMQueue
                //{
                //    Status_Code = Status.Added,
                //    UserId = SRDSession.GetCurrentUserId(),
                //    Parameters = viewModel.ToHarmonizationSettings().SerializeToXml()
                //}, new CancellationToken());
                //queueId = 0;
                queueId = HarmonizationProcess.AddProcessToQueue(viewModel.ToHarmonizationSettings());
            }
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

            return Json(new
            {
                Success = "Процедура Гармонизации успешно добавлена в очередь, по результатам операции будет отправлено сообщение",
                idResultAttribute = viewModel.IsNewAttribute ? viewModel.IdAttributeResult : null,
                QueueId = queueId
            });
        }

		[SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public ActionResult GetRowWithNewLevelForHarmonization([FromForm] int rowNumber)
        {
            ViewData["TreeAttributes"] = GetGbuAttributesTree();

            var model = new PartialNewHarmonizationLevel
            {
                RowNumber = rowNumber,
                LevelNumber = HarmonizationViewModel.NumberOfConstantLevelsInHarmonization + rowNumber
            };

            return PartialView("/Views/GbuObject/Partials/PartialNewHarmonizationRow.cshtml", model);
        }

        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public ActionResult GetRowsWithNewLevelForHarmonization(int startRowNumber, int rowCount, int?[] rowValues)
        {
	        ViewData["TreeAttributes"] = GetGbuAttributesTree();
	        ViewData["StartRowNumber"] = startRowNumber;
	        ViewData["RowCount"] = rowCount;

	        var models = new List<PartialNewHarmonizationLevel>();
	        for (int rowNumber = startRowNumber, i = 0; rowNumber < startRowNumber + rowCount; rowNumber++, i++)
	        {
		        models.Add(new PartialNewHarmonizationLevel
		        {
			        RowNumber = rowNumber,
			        LevelNumber = HarmonizationViewModel.NumberOfConstantLevelsInHarmonization + rowNumber,
					AttributeId = rowValues[i]
		        });
	        }

	        return PartialView("/Views/GbuObject/Partials/PartialNewHarmonizationRows.cshtml", models);
        }

		#endregion

		#region HarmonizationCOD

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_HARMONIZATION_COD)]
		public ActionResult HarmonizationCOD()
		{
			ViewData["TreeAttributes"] = GetGbuAttributesTree();

			ViewData["CodJobs"] = OMCodJob.Where(x => x).SelectAll().Execute().Select(x => new
			{
				Text = x.NameJob,
				Value = x.Id,
			}).ToList();

			ViewData["Documents"] = GetDocumentsForPartialView();

			return View(new HarmonizationCODViewModel());
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_HARMONIZATION_COD)]
		public ActionResult HarmonizationCOD(HarmonizationCODViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			if (viewModel.IsNewAttribute)
			{
				int idAttr = _service.AddNewVirtualAttribute(viewModel.NameNewAttribute, viewModel.RegistryId.GetValueOrDefault(), viewModel.TypeNewAttribute ?? RegisterAttributeType.INTEGER);
				if (idAttr == 0)
				{
					return SendErrorMessage("Не корректные данные для создания нового атрибута");
				}

				viewModel.IdAttributeResult = idAttr;
			}

            viewModel.Document.ProcessDocument();

            long queueId;
            try
			{
                ////TODO для тестирования (без процесса)
                //new HarmonizationCOD(viewModel.ToHarmonizationCODSettings()).Run();
                ////TODO для тестирования (с процессом)
                //new HarmonizationCodProcess().StartProcess(new OMProcessType(), new OMQueue
                //{
                //    Status_Code = Status.Added,
                //    UserId = SRDSession.GetCurrentUserId(),
                //    Parameters = viewModel.ToHarmonizationCODSettings().SerializeToXml()
                //}, new CancellationToken());
                //queueId = 0;
                queueId = HarmonizationCodProcess.AddProcessToQueue(viewModel.ToHarmonizationCODSettings());
            }
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

		    return Json(new
		    {
		        Success =
		            "Процедура Гармонизации по классификатору ЦОД успешно добавлена в очередь, по результатам операции будет отправлено сообщение",
		        idResultAttribute = viewModel.IsNewAttribute ? viewModel.IdAttributeResult : null,
		        idDocument = viewModel.Document.IsNewDocument ? viewModel.Document.IdDocument : null,
                QueueId = queueId
            });
		}

        #endregion

        #region Unloading From Dict

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_UNLOADING_FROM_DICT)]
		public ActionResult UnloadingFromDict()
		{
			ViewData["CodJob"] = OMCodJob.Where(x => x).SelectAll().Execute().Select(x => new
			{
				Value = x.Id,
				Text = x.NameJob
			}).AsEnumerable();

			ViewData["TreeAttributes"] = GetGbuAttributesTree();

			ViewData["Documents"] = GetDocumentsForPartialView();

			return View(new UnloadingFromDicViewModel());
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_UNLOADING_FROM_DICT)]
		public JsonResult UnloadingFromDict(UnloadingFromDicViewModel viewModel)
		{
            if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			if (viewModel.IsNewAttribute)
			{
				int idAttr = _service.AddNewVirtualAttribute(viewModel.NameNewAttribute, viewModel.RegistryId.GetValueOrDefault(), viewModel.TypeNewAttribute ?? RegisterAttributeType.INTEGER);
				if (idAttr == 0)
				{
					return SendErrorMessage("Не корректные данные для создания нового атрибута");
				}

				viewModel.IdAttributeResult = idAttr;
			}

            viewModel.Document.ProcessDocument();

			////TODO код для отладки
			//new SelectionCodLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	Parameters = viewModel.ToCodSelectionSettings().SerializeToXml()
			//}, new CancellationToken());

			SelectionCodLongProcess.AddProcessToQueue(viewModel.ToCodSelectionSettings());

			return Json(new
		    {
		        success = "Операция успешно поставлена в очерердь",
		        idResultAttribute = viewModel.IsNewAttribute ? viewModel.IdAttributeResult : null,
		        idDocument = viewModel.Document.IsNewDocument ? viewModel.Document.IdDocument : null
		    });
		}

		#endregion

		#region Inheritance

		#region load data

        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
        public JsonResult GetRatingTours()
		{
			var tours = OMTour.Where(x => true).SelectAll().Execute()
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Year.ToString()
				});

			return Json(tours);
		}

        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public ActionResult GetRow([FromForm] int rowNumber)
		{
			ViewData["TreeAttributes"] = GetGbuAttributesTree();

			ViewData["RowNumber"] = rowNumber.ToString();
			return PartialView("/Views/GbuObject/Partials/PartialNewRow.cshtml", new PartialAttribute());
		}

		[SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public ActionResult GetRows(int startRowNumber, int rowCount, int[] rowValues)
		{
			ViewData["TreeAttributes"] = GetGbuAttributesTree();
			ViewData["StartRowNumber"] = startRowNumber;
			ViewData["RowCount"] = rowCount;

			var models = new List<PartialAttribute>();
			for (int rowNumber = startRowNumber, i = 0; rowNumber < startRowNumber + rowCount; rowNumber++, i++)
			{
				models.Add(new PartialAttribute {Attributes = rowValues[i]});
			}

			return PartialView("/Views/GbuObject/Partials/PartialNewRows.cshtml", models);
		}

		#endregion
		
        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_INHERITANCE)]
		public ActionResult Inheritance()
		{
			ViewData["TreeAttributes"] = GetGbuAttributesTree();

			long[] arr = new long[5];
			return View(new InheritanceViewModel{Attributes = new List<long>(arr.ToList())});
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_INHERITANCE)]
		public JsonResult Inheritance(InheritanceViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			try
			{
                ////TODO код для отладки
                //new InheritanceLongProcess().StartProcess(new OMProcessType(), new OMQueue
                //{
                //    Status_Code = Status.Added,
                //    UserId = SRDSession.GetCurrentUserId(),
                //    Parameters = viewModel.ToAttributeSettings().SerializeToXml()
                //}, new CancellationToken());

                InheritanceLongProcess.AddProcessToQueue(viewModel.ToAttributeSettings());
			}
			catch (Exception e)
			{
				return SendErrorMessage(e.Message);
			}

            return Json(new { Success = "Операция успешно добавлена в очередь долгих процессов."});
		}

        #endregion

	    #region Присвоение оценочной группы

	    [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_SET_ESTIMATED_GROUP)]
		public ActionResult SetEstimatedGroup()
	    {
	        ViewData["TreeAttributes"] = GetGbuAttributesTree();

	        return View();
	    }

	    [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS_SET_ESTIMATED_GROUP)]
		public JsonResult SetEstimatedGroup(EstimatedGroupViewModel viewModel)
	    {
	        if (!ModelState.IsValid)
	        {
	            return GenerateMessageNonValidModel();
	        }

            long queueId;
            try
	        {
	            var estimatedGroupModelParamsDto =
	                _tourFactorService.GetEstimatedGroupModelParamsForTask(viewModel.IdTask.Value);

	            var parameters = viewModel.ToGroupModel(estimatedGroupModelParamsDto);
				////TODO код для отладки
				//new TaskSetEstimatedGroup().StartProcess(new OMProcessType(), new OMQueue
				//{
				//	ObjectRegisterId = OMTask.GetRegisterId(),
				//	ObjectId = viewModel.IdTask.Value,
				//	Status_Code = Status.Added,
				//	UserId = SRDSession.GetCurrentUserId(),
				//	Parameters = parameters.SerializeToXml()
				//}, new CancellationToken());
				//queueId = 0;

				queueId = TaskSetEstimatedGroup.AddProcessToQueue(OMTask.GetRegisterId(), viewModel.IdTask.Value, parameters);
	        }
	        catch (Exception ex)
	        {
	            return SendErrorMessage(ex.Message);
	        }

	        return Json(new {Success = true, QueueId = queueId });
	    }

		#endregion

		[SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public List<SelectListItem> GetTasksData()
		{
			var documentInfoList = _taskService.GetTaskDocumentInfoList().OrderByDescending(x => x.DocumentCreateDate);
			return documentInfoList
				.Select(x => new SelectListItem(_taskService.GetTemplateForTaskName(x), x.TaskId.ToString()))
				.ToList();
		}

		#region Support Methods

        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public IEnumerable<SelectListItem> GetAllGbuRegisters()
		{
			return RegisterCache.Registers.Values.Where(x => _service.GetGbuRegistersIds().Contains(x.Id)).Select(x => new SelectListItem(x.Description, x.Id.ToString()));
		}

		#endregion
    }
}
