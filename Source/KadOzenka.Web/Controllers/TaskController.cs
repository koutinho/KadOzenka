using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using Core.Register;
using KadOzenka.Web.Models.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Directory;
using ObjectModel.KO;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.LongProcess.TaskLongProcesses;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ObjectModel.Core.Register;
using KadOzenka.Dal.Models.Task;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Tasks.Dto;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Helpers;
using KadOzenka.Web.Models.DataImport;
using KadOzenka.Web.Models.Modeling;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Common;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.Directory.KO;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;
using Serilog;

namespace KadOzenka.Web.Controllers
{
	public class TaskController : KoBaseController
	{
		private readonly ILogger _log = Log.ForContext<TaskController>();
		public TaskService TaskService { get; set; }
		public ModelingService ModelService { get; set; }
		public DataImporterService DataImporterService { get; set; }
		public GbuObjectService GbuObjectService { get; set; }
		public TourFactorService TourFactorService { get; set; }
        public GroupService GroupService { get; set; }
		public GroupCalculationSettingsService GroupCalculationSettingsService { get; set; }
		public RegisterAttributeService RegisterAttributeService { get; set; }
		public UpdateCadastralDataService UpdateCadastralDataService { get; set; }
		public TemplateService TemplateService { get; set; }
		public ModelFactorsService ModelFactorsService { get; set; }
		public ModelingService ModelingService { get; set; }

		public TaskController(TemplateService templateService, ModelFactorsService modelFactorsService, ModelingService modelingService)
		{
			TaskService = new TaskService();
			ModelService = new ModelingService(new DictionaryService());
			DataImporterService = new DataImporterService();
			GbuObjectService = new GbuObjectService();
		    TourFactorService = new TourFactorService();
            GroupService = new GroupService();
            GroupCalculationSettingsService = new GroupCalculationSettingsService();
			RegisterAttributeService = new RegisterAttributeService();
            UpdateCadastralDataService = new UpdateCadastralDataService();
            TemplateService = templateService;
            ModelFactorsService = modelFactorsService;
            ModelingService = modelingService;
		}


		#region Карточка задачи

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult TaskCard(long taskId)
		{
			var taskDto = TaskService.GetTaskById(taskId);
			if (taskDto == null)
				return NotFound();

			var taskModel = TaskEditModel.ToEditModel(taskDto);

			return View(taskModel);
		}

	    [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult TaskCard(TaskEditModel model)
	    {
	        if (!ModelState.IsValid)
	        {
	            return GenerateMessageNonValidModel();
	        }

            try
            {
                TaskService.UpdateTaskData(model.ToDto());
            }
            catch (Exception e)
            {
                return SendErrorMessage(e.Message);
            }

            return Json(new { Success = "Сохранено успешно" });
	    }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult GetXmlDocuments([DataSourceRequest]DataSourceRequest request, long taskId)
		{
			var importDataLogsDto = DataImporterService.GetCommonDataLog(OMTask.GetRegisterId(), taskId);

			var result = new List<DataImporterLayoutDto>();
			importDataLogsDto.ForEach(x =>
			{
				result.Add(new DataImporterLayoutDto
				{
					Id = x.Id,
					DateCreated = x.CreationDate,
					UserName = x.Author,
					DataFileName = x.FileName,
				    NumberOfImportedObjects = x.NumberOfImportedObjects,
				    TotalNumberOfObjects = x.TotalNumberOfObjects,
                });
			});

			return Json(result.ToDataSourceResult(request));
		}

        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
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

        #endregion

        #region Перенос атрибутов

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
		public ActionResult TransferAttributes(bool create = false)
		{
			var model = new ExportAttributesModel();

			ViewData["TreeAttributes"] = GbuObjectService.GetGbuAttributesTree()
				.Select(x => new DropDownTreeItemModel
				{
					Value = Guid.NewGuid().ToString(),
					Text = x.Text,
					Items = x.Items.Select(y => new DropDownTreeItemModel
					{
						Value = y.Value,
						Text = y.Text
					}).ToList()
				}).AsEnumerable();

			ViewData["KoAttributes"] = new List<string>();

			model.CreateAttributes = create;

			return View(model);
		}

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
        public JsonResult GetRegisterAttributes()
        {
            var res = GbuObjectService.GetGbuAttributesTree()
                .Select(x => new DropDownTreeItemModel
                {
                    Value = Guid.NewGuid().ToString(),
                    Text = x.Text,
                    Items = x.Items.Select(y => new DropDownTreeItemModel
                    {
                        Value = y.Value,
                        Text = y.Text
                    }).ToList()
                }).AsEnumerable();
            return Json(res);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
        public ActionResult CreateAndTransferAttributes()
        {
            var model = new ExportAttributesModel();

            ViewData["TreeAttributes"] = GbuObjectService.GetGbuAttributesTree()
                .Select(x => new DropDownTreeItemModel
                {
                    Value = Guid.NewGuid().ToString(),
                    Text = x.Text,
                    Items = x.Items.Select(y => new DropDownTreeItemModel
                    {
                        Value = y.Value,
                        Text = y.Text
                    }).ToList()
                }).AsEnumerable();

            ViewData["KoAttributes"] = new List<string>();

            model.CreateAttributes = true;

            return View(model);
        }

		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
		public JsonResult GetTasksByTour(long tourId)
		{
			var tasks = TaskService.GetTasksByTour(tourId);
			_log.Debug("Задания на оценку по туру {TourId} {Tasks}", tourId, JsonConvert.SerializeObject(tasks));
            var models = MapToSelectListItems(tasks);

            return Json(models);
		}

        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
		public JsonResult GetTasksByTours(List<long?> tourIds)
        {
            var tasks = TaskService.GetTasksByTour(tourIds);

            var models = MapToSelectListItems(tasks);

            return Json(models);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
		public JsonResult GetKoAttributes(long tourId, ObjectTypeExtended objectType, List<long> exceptedAttributes)
        {
            var koAttributes = GetOmAttributesForKo(tourId, objectType, exceptedAttributes);

            var models = koAttributes.Select(x => new
            {
                Value = x.Id,
                Text = x.Name
            }).AsEnumerable();

            return Json(models);
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
		public JsonResult TransferAttributes(ExportAttributesModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

			GbuExportAttributeSettings settings;
			if (model.CreateAttributes)
			{
				settings = model.ToGbuExportAndCreateAttributeSettings();
            }
			else
			{
				settings = model.ToGbuExportAttributeSettings();
				ValidateExportAttributeItems(settings.Attributes);
			}

			if (settings.Attributes == null || settings.Attributes.Count == 0)
			{
				_log.Warning("Перенос атрибутов невозможен. Не выбраны атрибуты для переноса.");
				throw new Exception("Не выбраны атрибуты для переноса.");
			}

            ////TODO код для отладки
            //new ExportAttributeToKoProcess().StartProcess(new OMProcessType(), new OMQueue
            //{
            //    Status_Code = Status.Added,
            //    UserId = SRDSession.GetCurrentUserId(),
            //    Parameters = settings.SerializeToXml()
            //}, new CancellationToken());


			ExportAttributeToKoProcess.AddProcessToQueue(settings);
			_log.ForContext("TaskFilter", settings.TaskFilter)
				.Debug("Добавлена задача фонового процесса: перенос атрибутов {TransferAttributes}", JsonConvert.SerializeObject(settings.Attributes));

			return new JsonResult(Ok());
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
        public ActionResult GetRowExport([FromForm] int rowNumber, [FromForm] long tourId,
            [FromForm] ObjectTypeExtended objectType, [FromForm] bool create)
        {
            ViewData["TreeAttributes"] = GbuObjectService.GetGbuAttributesTree()
                .Select(x => new DropDownTreeItemModel
                {
                    Value = Guid.NewGuid().ToString(),
                    Text = x.Text,
                    Items = x.Items.Select(y => new DropDownTreeItemModel
                    {
                        Value = y.Value,
                        Text = y.Text
                    }).ToList()
                }).AsEnumerable();

            var koAttributes = GetOmAttributesForKo(tourId, objectType, null) ?? new List<OMAttribute>();

            ViewData["KoAttributes"] = koAttributes.Select(x => new
            {
                Value = x.Id,
                Text = x.Name
            }).AsEnumerable();

            ViewData["RowNumber"] = rowNumber.ToString();
            ViewData["CreateAttributes"] = create;

            return PartialView("/Views/Task/PartialTransferAttributeRow.cshtml", new PartialExportAttributesRowModel());
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
        public List<SelectListItem> GetTemplatesForTransferAttributes(bool isCreateMode)
        {
            var formType = isCreateMode
                ? DataFormStorege.TransferAttributesWithCreate
                : DataFormStorege.TransferAttributesWithoutCreate;

            return TemplateService.GetTemplates(formType)
                .Select(x => new SelectListItem(x.TemplateName ?? string.Empty, x.Id.ToString())).ToList();
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
        public JsonResult SaveTemplateForTransferAttributesWithCreate(string nameTemplate, bool isCommon, [FromForm]ExportAttributesModel viewModel)
        {
            return SaveTemplate(nameTemplate, isCommon, DataFormStorege.TransferAttributesWithCreate, viewModel.SerializeToXml());
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
        public JsonResult SaveTemplateForTransferAttributesWithoutCreate(string nameTemplate, bool isCommon, [FromForm]ExportAttributesModel viewModel)
        {
            return SaveTemplate(nameTemplate, isCommon, DataFormStorege.TransferAttributesWithoutCreate, viewModel.SerializeToXml());
        }

        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
        public JsonResult GetTemplateForTransferAttributes(int id)
        {
            if (id == 0)
                return new JsonResult(Ok());

            var storage = OMDataFormStorage.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			if (storage == null)
			{
				_log.Warning("Не найдено хранилище шаблонов с id={TemplateId}", id);
				throw new Exception($"Не найдено хранилище шаблонов с Id='{id}'");
			}
			else
			{
				_log.ForContext("FormType", storage.FormType)
					.ForContext("FormType_Code", storage.FormType_Code)
					.Debug(new Exception(storage.Data),"Получение шаблона переноса атрибутов");
			}

            var viewModel = storage.Data.DeserializeFromXml<ExportAttributesModel>();

            return Json(new { data = JsonConvert.SerializeObject(viewModel) });
        }


        #region Support Methods

        private IEnumerable<SelectListItem> MapToSelectListItems(List<TaskDocumentInfoDto> tasks)
        {
            var models = tasks.Select(x => new SelectListItem
            {
                Value = x.TaskId.ToString(),
                Text = TaskService.GetTemplateForTaskName(x)
            });

            return models;
        }

        private List<OMAttribute> GetOmAttributesForKo(long tourId, ObjectTypeExtended objectType, List<long> exceptedAttributes)
        {
            var koAttributes = TourFactorService.GetTourAttributes(tourId, objectType);

            if (exceptedAttributes != null && exceptedAttributes.Count > 0)
            {
                koAttributes = koAttributes.Where(x => !exceptedAttributes.Contains(x.Id)).ToList();
            }

            return koAttributes;
        }

        private void ValidateExportAttributeItems(List<ExportAttributeItem> item)
		{
			var message = new StringBuilder("Один из параметров не выбран, строки №:");
			var withErrors = false;

			for (var i = 0; i < item.Count; i++)
			{
				var current = item[i];

				if (!((current.IdAttributeGBU == 0 && current.IdAttributeKO == 0) ||
					  (current.IdAttributeGBU != 0 && current.IdAttributeKO != 0)))
				{
					message.Append($"{i + 1}, ");
					withErrors = true;
				}
			}

			if (withErrors)
				throw new ArgumentException(message.ToString());
		}

		#endregion

		#endregion


		#region Единица оценки

		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public JsonResult GetExplication(long id)
		{
			List<UnitExplicationDto> result = new List<UnitExplicationDto>();

			List<OMExplication> explications = OMExplication.Where(x => x.ObjectId == id)
				.SelectAll()
				.Execute();

			if (explications.Any())
			{
				List<long> groupIds = explications.Select(x => x.GroupId).ToList();

				List<OMGroup> groups = OMGroup.Where(x => groupIds.Contains(x.Id))
					.Select(x => x.GroupName)
					.Select(x => x.Number)
					.Execute();

				if (groups.Any())
				{
					result = explications.Select(x =>
					{
						var group = groups.Find(y => y.Id == x.GroupId);
						return new UnitExplicationDto
						{
							Id = x.Id,
							GroupId = x.GroupId,
							Group = group != null 
								? $"{group.Number}. {group.GroupName}" 
								: null,
							Square = x.Square,
							Upks = x.Upks,
							Kc = x.Kc,
							Analog = x.NameAnalog?.ToString()
						};
					}).ToList();
				}
			}
			return Json(result);
		}

        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public JsonResult GetUnitFactors(long id, bool showOnlyModelFactors = true, bool isShowOnlyFilledFactors = false)
		{
			var unit = OMUnit.Where(x => x.Id == id)
				.Select(x => new
				{
					x.GroupId,
					x.TourId, 
					x.PropertyType_Code
				})
				.ExecuteFirstOrDefault();

			if (unit != null)
			{
				var modelFactorIds = new List<long>();
				if (showOnlyModelFactors)
				{
					var model = OMModel.Where(x => x.GroupId == unit.GroupId).ExecuteFirstOrDefault();
					if (model != null)
					{
						modelFactorIds = OMModelFactor.Where(x => x.ModelId == model.Id && x.FactorId != null)
							.Select(x => x.FactorId)
							.Execute()
							.Select(x => x.FactorId.GetValueOrDefault()).ToList();
					}
				}

				var factorsValues = TourFactorService.GetUnitFactorValues(unit, modelFactorIds);
				var result = MapFactors(factorsValues, isShowOnlyFilledFactors);
				return Json(result);
			}

			return Json(new List<UnitFactorsDto>());
		}


		#region Support Methods

		private List<UnitFactorsDto> MapFactors(List<UnitFactor> factors, bool isShowOnlyFilledFactors)
		{
			var result = new List<UnitFactorsDto>();
			foreach (var factorValue in factors)
			{
				var value = factorValue.GetValueInString();
				if (!isShowOnlyFilledFactors || !string.IsNullOrWhiteSpace(value))
				{
					result.Add(new UnitFactorsDto
					{
						FactorId = factorValue.AttributeId,
						FactorName = factorValue.GetFactorName(),
						FactorValue = value
					});
				}
			}

			return result;
		}

		#endregion

		#endregion

		#region Расчет кадастровой стоимости

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_CALCULATE_CADASTRAL_PRICE)]
		public ActionResult CalculateCadastralPrice()
        {
            return View();
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_CALCULATE_CADASTRAL_PRICE)]
		public JsonResult CalculateCadastralPrice(CadastralPriceCalculationModel model)
        {
            if (model.TaskFilter == null || model.TaskFilter.Count == 0)
                throw new ArgumentException("Не выбраны задания на оценку");

            if (!model.IsAllGroups && (model.SubGroups == null || model.SubGroups.Count == 0))
                throw new ArgumentException("Не выбраны группы");

            var settings = CadastralPriceCalculationModel.UnMap(model);
            CalculateCadastralPriceLongProcess.AddProcessToQueue(settings);

            return Json(new {Message = "Операция Расчета кадастровой стоимости добавлена в очередь" });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_CALCULATE_CADASTRAL_PRICE)]
		public ActionResult CalculationOrderSettings(long tourId, bool isParcel)
        {
            ViewBag.TourId = tourId;
            ViewBag.IsParcel = isParcel;
            return PartialView();
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_CALCULATE_CADASTRAL_PRICE)]
		public JsonResult GetCalculationOrderSettings(long tourId, bool isParcel)
        {
            var settings = GroupCalculationSettingsService.GetCalculationSettings(tourId, isParcel);
            var models = settings.Select(CadastralPriceCalculationSettingsModel.ToModel).ToList();

            return Json(models);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_CALCULATE_CADASTRAL_PRICE)]
		public JsonResult SaveCalculationSettings(string models)
        {
            var settingsJson = JObject.Parse(models).SelectToken("models").ToString();
            var settingsModels = JsonConvert.DeserializeObject<List<CadastralPriceCalculationSettingsModel>>(settingsJson);

            if (settingsModels.Count == 0)
                return Json(new { Message = "Нет данных для изменения" });

            var dtos = settingsModels.Select(CadastralPriceCalculationSettingsModel.FromModel).ToList();
            for (var i = 0; i < dtos.Count; i++)
            {
                dtos[i].Priority = i;
            }

            GroupCalculationSettingsService.SaveCalculationSettings(dtos);

            return Json(new { Message = "Изменения сохранены" });
        }

        #endregion


        #region Загрузка графических факторов из РЕОН

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_DOWNLOAD_GRAPHIC_FACTORS_FROM_REON)]
        public ActionResult DownloadGraphicFactorsFromReon(long taskId)
        {
            var model = new GraphicFactorsFromReonModel
            {
                TaskId = taskId
            };

			_log.Debug("Загрузка графических факторов из РЕОН {TaskId}", taskId);

			return View(model);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_DOWNLOAD_GRAPHIC_FACTORS_FROM_REON)]
        public JsonResult GetReonRegisterAttributes()
        {
            var availableAttributeTypes = new[]
            {
                Consts.IntegerAttributeType, Consts.DecimalAttributeType
            };
            var numericRegisterAttributes = RegisterAttributeService
                .GetActiveRegisterAttributes(KoFactorsFromReon.ReonSourceRegisterId)
                .Where(x => availableAttributeTypes.Contains(x.Type) && !string.IsNullOrWhiteSpace(x.Name))
                .Select(x =>
                {
                    var lastSeparatorIndex = x.Name.LastIndexOf(KoFactorsFromReon.AttributeNameSeparator);
                    if (lastSeparatorIndex == -1)
                    {
                        return new
                        {
                            Id = x.Id,
                            Category = x.Name,
                            Name = string.Empty
                        };
                    }
                    var category = x.Name.Substring(0, lastSeparatorIndex).Trim();
                    var nameStartPosition = lastSeparatorIndex + KoFactorsFromReon.AttributeNameSeparator.Length;
                    var name = x.Name.Substring(nameStartPosition, x.Name.Length - nameStartPosition).Trim();
                    return new
                    {
                        Id = x.Id,
                        Category = category,
                        Name = name
                    };
                })
                .GroupBy(x => x.Category)
                .Select(x =>
                {
                    var factorsInGroup = x.ToList();
                    return new DropDownTreeItemModel
                    {
                        Value = Guid.NewGuid().ToString(),
                        Text = x.Key,
                        HasChildren = factorsInGroup.Count > 0,
                        Items = factorsInGroup.Select(y => new DropDownTreeItemModel
                        {
                            Value = y.Id.ToString(),
                            Text = y.Name
                        }).ToList()
                    };
                }).ToList();

			if (numericRegisterAttributes[0] != null)
			{
				_log.ForContext("RegisterAttributes0", JsonConvert.SerializeObject(numericRegisterAttributes[0]))
					.Debug("Получение списка атрибутов ({AttributesCount}) из РЕОН", numericRegisterAttributes.Count);
			}

			return new JsonResult(numericRegisterAttributes);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_DOWNLOAD_GRAPHIC_FACTORS_FROM_REON)]
        public JsonResult DownloadGraphicFactorsFromReon(GraphicFactorsFromReonModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var inputParameters = new KoFactorsFromReonInputParameters
            {
                TaskId = model.TaskId,
                AttributeIds = model.AttributeIds
            };
            ////TODO код для отладки
            //new KoFactorsFromReon().StartProcess(new OMProcessType(), new OMQueue
            //{
            //    Status_Code = Status.Added,
            //    UserId = SRDSession.GetCurrentUserId(),
            //    Parameters = inputParameters.SerializeToXml()
            //}, new CancellationToken());

            KoFactorsFromReon.AddProcessToQueue(inputParameters);
			_log.ForContext("TaskId", inputParameters.TaskId)
				.ForContext("AttributeIds", inputParameters.AttributeIds)
				.Information("Процесс {SRDCoreFunctions} поставлен в очередь", "KO_TASKS_DOWNLOAD_GRAPHIC_FACTORS_FROM_REON");

			return new JsonResult(new { Message = "Процесс поставлен в очередь. Результат будет отправлен на почту." });
        }

		#endregion

		#region Актуализация кадастровых данных

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult UpdateTaskCadastralData(long taskId)
		{
			var task = OMTask.Where(x => x.Id == taskId).ExecuteFirstOrDefault();
			if (task == null)
			{
				throw new Exception($"Не найдено задание на оценку с ИД {taskId}");
			}

			var taskName = TaskService.GetTemplateForTaskName(taskId);
			//////TODO код для отладки
			//new UpdateTaskCadastralDataLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	ObjectId = taskId
			//}, new CancellationToken());
			UpdateTaskCadastralDataLongProcess.AddProcessToQueue(taskId);

			return Content($"Процесс Актуализация кадастровых данных для задания на оценку '{taskName}' успешно добавлен в очередь");
		}

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult UpdateCadastralDataAttributeSettings()
		{
			ViewData["TreeAttributes"] = GbuObjectService.GetGbuAttributesTree()
				.Select(x => new DropDownTreeItemModel
				{
					Value = Guid.NewGuid().ToString(),
					Text = x.Text,
					Items = x.Items.Select(y => new DropDownTreeItemModel
					{
						Value = y.Value,
						Text = y.Text
					}).ToList()
				}).AsEnumerable();

			var model = new UpdateTaskCadastralDataAttributeSettingsModel();
			model.CadastralQuarterGbuAttributeId =
				UpdateCadastralDataService.GetCadastralDataCadastralQuarterAttributeId();
			model.BuildingCadastralNumberGbuAttributeId = UpdateCadastralDataService
				.GetCadastralDataBuildingCadastralNumberAttributeId();

			return View(model);
		}

		[HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public JsonResult UpdateCadastralDataAttributeSettings(UpdateTaskCadastralDataAttributeSettingsModel model)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			UpdateCadastralDataService.UpdateCadastralDataAttributeSettings(model.CadastralQuarterGbuAttributeId,
				model.BuildingCadastralNumberGbuAttributeId);

			return Json(new { Success = "Сохранено успешно" });
		}


		#endregion Актуализация кадастровых данных


		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult DataMapping(long taskId)
		{
			OMTask task = OMTask.Where(x => x.Id == taskId)				
				.ExecuteFirstOrDefault();

			if (task == null)
			{
				throw new Exception("Не найдено задание на оценку с ИД=" + taskId);
			}

			return View(taskId);
		}

		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult DataMappingModal(long taskId, long objectId)
		{
			OMTask task = OMTask.Where(x => x.Id == taskId)
				.ExecuteFirstOrDefault();

			if (task == null)
			{
				throw new Exception("Не найдено задание на оценку с ИД=" + taskId);
			}

			return View((taskId,objectId));
		}

        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public JsonResult GetTaskObjects(long taskId)
		{
			List<OMUnit> unitList = OMUnit.Where(x => x.TaskId == taskId)
				.Select(x => x.ObjectId)
				.Select(x => x.CadastralNumber)
				.Execute();

			var objectList = unitList.Select(x => new { x.ObjectId, x.CadastralNumber }).ToList();
			return Json(objectList);
		}

        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public JsonResult GetDataMapping(long taskId, long objectId)
		{			
			OMTask task = OMTask.Where(x => x.Id == taskId)
				.Select(x => x.CreationDate)
				.Select(x => x.EstimationDate)
				.Select(x => x.DocumentId)
				.ExecuteFirstOrDefault();
					
			List<DataMappingDto> list = TaskService.FetchGbuData(objectId, task);

			list = list.Where(x => !Object.Equals(x.Value, x.OldValue)).ToList();

			return Json(list);
		}
	}	
}