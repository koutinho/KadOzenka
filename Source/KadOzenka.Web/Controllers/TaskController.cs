﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using Core.Main.FileStorages;
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
using KadOzenka.Web.Models.Unit;
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
		public DataImporterService DataImporterService { get; set; }
		public GbuObjectService GbuObjectService { get; set; }
		public TourFactorService TourFactorService { get; set; }
        public GroupService GroupService { get; set; }
		public GroupCalculationSettingsService GroupCalculationSettingsService { get; set; }
		public RegisterAttributeService RegisterAttributeService { get; set; }
		public SystemAttributeSettingsService SystemAttributeSettingsService { get; set; }
		public TemplateService TemplateService { get; set; }
		public FactorSettingsService FactorSettingsService { get; set; }

		public TaskController(TemplateService templateService)
		{
			TaskService = new TaskService();
			DataImporterService = new DataImporterService();
			GbuObjectService = new GbuObjectService();
		    TourFactorService = new TourFactorService();
            GroupService = new GroupService();
            GroupCalculationSettingsService = new GroupCalculationSettingsService();
			RegisterAttributeService = new RegisterAttributeService();
            SystemAttributeSettingsService = new SystemAttributeSettingsService();
            TemplateService = templateService;
            FactorSettingsService = new FactorSettingsService();
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
		public ActionResult TransferAttributes()
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

			model.CreateAttributes = false;

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
        public ActionResult GetRowExports(int startRowNumber, int rowCount, List<PartialExportAttributesRowModel> rowValues, long tourId,
	         ObjectTypeExtended objectType)
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

	        ViewData["StartRowNumber"] = startRowNumber;
	        ViewData["RowCount"] = rowCount;

	        return PartialView("/Views/Task/PartialTransferAttributeRows.cshtml", rowValues);
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
		public JsonResult GetUnitFactorsShowTypes()
		{
			var types = Helpers.EnumExtensions.GetSelectList(typeof(UnitFactorsShowType));
			return Json(types);
		}

		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public JsonResult GetUnitFactors(long id, UnitFactorsShowType unitFactorsShowType = UnitFactorsShowType.ModelFactors, bool isShowOnlyFilledFactors = false)
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
				List<UnitFactor> factorsValues = new List<UnitFactor>();
				switch (unitFactorsShowType)
				{
					case UnitFactorsShowType.ModelFactors:
						var model = new ModelingRepository().GetActiveModelEntityByGroupId(unit.GroupId);
						if (model != null)
						{
							var modelFactorIds = OMModelFactor.Where(x => x.ModelId == model.Id && x.FactorId != null)
								.Select(x => x.FactorId)
								.Execute()
								.Select(x => x.FactorId.GetValueOrDefault()).ToList();
							if(!modelFactorIds.IsEmpty())
								factorsValues = TourFactorService.GetUnitFactorValues(unit, modelFactorIds);
						}
						break;
					case UnitFactorsShowType.GroupFactors:
						var groupFactorIds = OMGroupFactor.Where(x => x.GroupId == unit.GroupId && x.FactorId != null)
							.Select(x => x.FactorId)
							.Execute()
							.Select(x => x.FactorId.GetValueOrDefault()).ToList();
						if(!groupFactorIds.IsEmpty())
							factorsValues = TourFactorService.GetUnitFactorValues(unit, groupFactorIds);
						break;
					default:
						factorsValues = TourFactorService.GetUnitFactorValues(unit);
						break;
				}

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

			////TODO код для отладки
			//new CalculateCadastralPriceLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	Parameters = settings.SerializeToXml()
			//}, new CancellationToken());
			
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
			ViewData["TreeAttributes"] = GetGbuAttributesTree();

			var model = new UpdateTaskCadastralDataAttributeSettingsModel();
			model.CadastralQuarterGbuAttributeId =
				SystemAttributeSettingsService.GetCadastralDataCadastralQuarterAttributeId();
			model.BuildingCadastralNumberGbuAttributeId = SystemAttributeSettingsService
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

			SystemAttributeSettingsService.UpdateCadastralDataAttributeSettings(model.CadastralQuarterGbuAttributeId,
				model.BuildingCadastralNumberGbuAttributeId);

			return Json(new { Success = "Сохранено успешно" });
		}


		#endregion Актуализация кадастровых данных


		#region Перенос Оценочной группы

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult UpdateEvaluativeGroupSettings()
		{
			ViewData["TreeAttributes"] = GetGbuAttributesTree();

			var model = new UpdateEvaluativeGroupSettingsModel
			{
				EvaluativeGroupGbuAttributeId = SystemAttributeSettingsService.GetEvaluativeGroupAttributeId()
			};

			return View(model);
		}

		[HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult UpdateEvaluativeGroupSettings(UpdateEvaluativeGroupSettingsModel model)
		{
			if (!ModelState.IsValid)
				return GenerateMessageNonValidModel();

			SystemAttributeSettingsService.UpdateEvaluativeGroupAttributeSettings(model.EvaluativeGroupGbuAttributeId);

			return Ok();
		}

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult UpdateEvaluativeGroup(long taskId)
		{
			var taskName = TaskService.GetTemplateForTaskName(taskId);

			////TODO код для отладки
			//new UpdateEvaluativeGroupLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	ObjectId = taskId
			//}, new CancellationToken());
			UpdateEvaluativeGroupLongProcess.AddProcessToQueue(taskId);

			return Content($"Процесс Переноса оценочной группы для задания на оценку '{taskName}' успешно добавлен в очередь");
		}

		#endregion Перенос Оценочной группы


		#region Изменения в атрибутах

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
		public ActionResult TaskAttributeChangesModal(long taskId, long objectId)
		{
			OMTask task = OMTask.Where(x => x.Id == taskId)
				.ExecuteFirstOrDefault();

			if (task == null)
			{
				throw new Exception("Не найдено задание на оценку с ИД=" + taskId);
			}

			var model = new TaskAttributeChangesModalModel
			{
				TaskId = taskId,
				ObjectId = objectId
			};
			return View(model);
		}

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult TaskAttributeChangesToExcelModal(long taskId)
		{
			var isInitial = TaskService.CheckIfInitial(taskId);
			var model = new TaskAttributeChangesToExcelModalModel();
			model.TaskId = taskId;
			model.ButtonEnabled = !isInitial;
			model.Message = isInitial
				? "Выгрузка изменений недоступна для исходного перечня"
				: $"Выгрузка изменений для задачи с идентификатором {taskId}";
			return View(model);
		}

		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public IActionResult TaskAttributeChangesToExcel(long taskId)
		{
			try
			{
				TaskAttributeChangesToExcelProcess.AddProcessToQueue(
					new TaskAttributeChangesToExcelProcess.TaskAttributeChangesParams
					{
						KOTaskId = taskId,
						UserId = SRDSession.GetCurrentUserId()
					});
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(500, "Возникла ошибка при постановке задачи в очередь");
			}
		}

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public FileResult DownloadTaskAttributeChanges(long taskId, string dt)
		{
			var FileStorage = "DataExporterByTemplate";
			var dateTime = DateTime.Parse(dt);
			var st = FileStorageManager.GetFileStream(FileStorage, dateTime, $"{taskId}_TaskAttributeChanges.xlsx");
			return File(st, Consts.ExcelContentType,
				$"{taskId}_{dt:ddMMyyyy}_TaskAttributeChanges.xlsx");
		}

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult TaskForCod(long taskId)
		{
			////TODO код для отладки
			//new TaskForCodLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	ObjectId = taskId,
			//	UserId = SRDSession.GetCurrentUserId()
			//}, new CancellationToken());

			TaskForCodLongProcess.AddProcessToQueue(taskId);

			return View();
		}

		#endregion

		#region Выгрузка факторов

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public IActionResult GetRegistersForFactorDownload(long taskId, bool isOks)
        {
            DropDownTreeItemModel BuildTreeItemModel(List<OMAttribute> attrList,string upperNodeValue, string upperNodeText)
            {
                return new DropDownTreeItemModel
                {
                    Value = upperNodeValue,
                    Text = upperNodeText,
                    Items = attrList.Select(x => new DropDownTreeItemModel
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList()
                };
            }

            var task = OMTask.Where(x => x.Id == taskId).SelectAll().ExecuteFirstOrDefault();
            if (task == null) return StatusCode(500,$"Задача с идентификатором {taskId} не найдена");

            var taskAttr = RegisterAttributeService.GetActiveRegisterAttributes(201);
            var taskAttrTree = BuildTreeItemModel(taskAttr, "201", "Еденица оценки");

            var tourAttributes = TourFactorService.GetTourAttributes(task.TourId ?? 0, isOks ? ObjectTypeExtended.Oks : ObjectTypeExtended.Zu);
            if (tourAttributes.Count == 0)
                return StatusCode(500,$"Для {(isOks?"ОКС":"ЗУ")} тура оценки {task.TourId} не найдены факторы");

            var tourRegister = TourFactorService.GetTourRegister(task.TourId ?? 0, isOks ? ObjectType.Oks : ObjectType.ZU);
            var paramsRegisterId = tourRegister.RegisterId;
            var paramsTree = BuildTreeItemModel(tourAttributes, paramsRegisterId.ToString(), "Факторы");

            var groupAttr = RegisterAttributeService.GetActiveRegisterAttributes(205);
            var groupNumber = groupAttr.FirstOrDefault(x => x.Id == 20500500);
            if (groupNumber!=null) groupNumber.Name = "Оценочная группа";
            var groupTree = BuildTreeItemModel(groupAttr, "205", "Группы/подгруппы");

            var treeModel = new List<DropDownTreeItemModel>
            {
                taskAttrTree,
                groupTree,
                paramsTree
            };
            return new JsonResult(treeModel);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public IActionResult FactorDownloadForm(long taskId)
        {
            return View("~/Views/Task/FactorDownloadForm.cshtml", taskId);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public IActionResult QueueFactorDownload(long taskId, long[] attributes, bool isOks)
        {
            try
            {
                FactorsExportLongProcess.AddProcessToQueue(
                    new FactorsExportLongProcess.FactorsDownloadParams
                    {
                        Attributes = attributes,
                        TaskId = taskId,
                        IsOks = isOks,
                        UserId = SRDSession.GetCurrentUserId()
                    });
                return Ok();
            }
            catch
            {
                return StatusCode(500, "Возникла ошибка при постановке задачи в очередь");
            }
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public FileResult DownloadFactorExportResult(long taskId, string dt)
        {
            var FileStorage = "DataExporterByTemplate";
            var dateTime = DateTime.Parse(dt);
            var st = FileStorageManager.GetFileStream(FileStorage, dateTime, $"{taskId}_FactorsExport.xlsx");
            return File(st, Consts.ExcelContentType, $"{taskId}_{dateTime:ddMMyyyy}_FactorsExport.xlsx");
        }

        #endregion

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

		#region Просмотр настроек факторов

		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public ActionResult FactorSettings()
		{
			return View();
		}

		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public JsonResult GetFactorSettings()
		{
			var factorSettings = FactorSettingsService.GetFactorSettings()
				.Select(FactorSettingsModel.FromDto).ToList();

			return Json(factorSettings);
		}

		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public JsonResult GetFactorInheritanceTypes()
		{
			var types = Helpers.EnumExtensions.GetSelectList(typeof(FactorInheritance));
			return Json(types);
		}

		#endregion Просмотр настроек факторов
	}
}