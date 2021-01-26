using System;
using System.Linq;
using KadOzenka.Dal.Modeling;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using Core.ErrorManagment;
using Core.ObjectModel.CustomAttribute;
using Core.Register;
using Core.Register.Enums;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.CoreUI.Registers;
using GemBox.Spreadsheet;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Entities;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectModel.Core.Register;
using ObjectModel.Market;
using KadOzenka.Dal.Registers;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using ObjectModel.KO;
using System.IO;
using System.Threading;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.LongProcess.Common;
using KadOzenka.Dal.LongProcess.Modeling;
using KadOzenka.Dal.LongProcess.Modeling.Entities;
using Microsoft.Practices.ObjectBuilder2;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.Ko;
using ObjectModel.Modeling;
using Consts = KadOzenka.Web.Helpers.Consts;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
	public class ModelingController : KoBaseController
	{
		public ModelingService ModelingService { get; set; }
        public TourFactorService TourFactorService { get; set; }
        public RegisterAttributeService RegisterAttributeService { get; set; }
        public DictionaryService DictionaryService { get; set; }
        public ModelFactorsService ModelFactorsService { get; set; }
        public GroupService GroupService { get; set; }


        public ModelingController(ModelingService modelingService, TourFactorService tourFactorService,
            RegisterAttributeService registerAttributeService, DictionaryService dictionaryService,
            ModelFactorsService modelFactorsService, GroupService groupService)
        {
            ModelingService = modelingService;
            TourFactorService = tourFactorService;
            RegisterAttributeService = registerAttributeService;
            DictionaryService = dictionaryService;
            ModelFactorsService = modelFactorsService;
            GroupService = groupService;
        }


        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult ModelCard(long modelId, bool isPartial = false, bool isReadOnly = false)
        {
	        var isGroupExists = ModelingService.IsModelGroupExist(modelId);
	        if (!isGroupExists)
	        {
                return RedirectToAction(nameof(ModelWithDeletedGroupCard), new { modelId, isPartial });
            }

            var model = OMModel.Where(x => x.Id == modelId).Select(x => x.Type_Code).ExecuteFirstOrDefault();
	        if (model?.Type_Code == KoModelType.Automatic)
	        {
		        return RedirectToAction(nameof(AutomaticModelCard), new {modelId, isPartial, isReadOnly});
	        }

	        return RedirectToAction(nameof(ManualModelCard), new {modelId, isPartial, isReadOnly});
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_ADD_MODEL)]
        public ActionResult AddModel()
        {
	        var model = new GeneralModelingModel
	        {
		        IsCreationMode = true
            };

	        return View(model);
        }

        [HttpPost]
        [JsonExceptionHandler(Message = "Модель с заданным именем уже существует")]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_ADD_MODEL)]
        public JsonResult AddModel(GeneralModelingModel modelingModel)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var modelDto = modelingModel.ToDto();
	        if (modelDto.Type == KoModelType.Automatic)
	        {
		        ModelingService.AddAutomaticModel(modelDto);
	        }
	        else
	        {
		        ModelingService.AddManualModel(modelDto);
	        }

	        return Json(new { Message = "Сохранение выполнено" });
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult MakeModelActive(long modelId)
        {
	        ModelingService.MakeModelActive(modelId);

	        return Ok();
        }


        #region Карточка автоматической модели

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult ModelWithDeletedGroupCard(long modelId, bool isPartial)
        {
	        var modelName = OMModel.Where(x => x.Id == modelId).Select(x => x.Name).ExecuteFirstOrDefault()?.Name;
	        if (isPartial)
	        {
		        return PartialView(nameof(ModelWithDeletedGroupCard), modelName);
	        }

	        return View(nameof(ModelWithDeletedGroupCard), modelName);
        }

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
		public ActionResult AutomaticModelCard(long modelId, bool isPartial, bool isReadOnly = false)
		{
			var modelDto = ModelingService.GetModelById(modelId);

			var hasFormedObjectArray = ModelingService.GetIncludedModelObjectsQuery(modelId, true).ExecuteExists();
			var model = AutomaticModelingModel.ToModel(modelDto, hasFormedObjectArray);
			model.IsReadOnly = isReadOnly;

			if (isPartial)
			{
				return PartialView(model);
			}

            return View(model);
		}

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
		public JsonResult GetModelAttributes(long modelId, KoAlgoritmType type)
		{
			var attributes = ModelFactorsService.GetModelAttributes(modelId, type);

			return Json(attributes);
		}

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
		public JsonResult GetA0(long modelId, KoAlgoritmType type)
		{
			var model = OMModel.Where(x => x.Id == modelId).Select(x => new
			{
				x.A0,
				x.A0ForExponential,
				x.A0ForMultiplicative,
                x.A0ForLinearTypeInPreviousTour,
                x.A0ForExponentialTypeInPreviousTour,
                x.A0ForMultiplicativeTypeInPreviousTour
			}).ExecuteFirstOrDefault();

			decimal? a0 = null;
			decimal? a0Previous = null;
			switch (type)
			{
				case KoAlgoritmType.None:
				case KoAlgoritmType.Line:
					a0 = model?.A0;
					a0Previous = model?.A0ForLinearTypeInPreviousTour;
                    break;
				case KoAlgoritmType.Exp:
					a0 = model?.A0ForExponential;
					a0Previous = model?.A0ForExponentialTypeInPreviousTour;
                    break;
				case KoAlgoritmType.Multi:
					a0 = model?.A0ForMultiplicative;
					a0Previous = model?.A0ForMultiplicativeTypeInPreviousTour;
                    break;
			}

			return Json(new {a0, a0Previous});
		}

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult GetGroups(long tourId, ObjectTypeExtended objectType)
        {
	        if (tourId == 0)
		        return Json(string.Empty);

            var groupTree = GroupService.GetTourGroupsInfo(tourId, objectType);

	        var resultGroups = objectType == ObjectTypeExtended.Oks ? groupTree.OksGroups : groupTree.ZuGroups;
	        var resultGroupsIds = resultGroups?.Select(x => x.Id);

            var resultSubgroups = objectType == ObjectTypeExtended.Oks ? groupTree.OksSubGroups : groupTree.ZuSubGroups;
            var resultSubgroupIds = resultSubgroups?.Select(x => x.Id);

            var allGroupIds = resultGroupsIds?.Concat(resultSubgroupIds).ToList();

            if(allGroupIds?.Count == 0)
                return Json(string.Empty);

            var groups = OMGroupToMarketSegmentRelation
	            .Where(x => allGroupIds.Contains(x.GroupId))
	            .Select(x => new
	            {
		            x.GroupId,
		            x.ParentGroup.GroupName,
		            x.ParentGroup.Number
	            })
	            .Execute()
	            .Select(x => new
	            {
		            GroupId = x.GroupId.ToString(),
		            FullGroupName = $"{x.ParentGroup?.Number}.{x.ParentGroup?.GroupName}",
		            ParentGroupNumber = GroupService.GetParentGroupNumber(x.ParentGroup?.Number),
		            SubGroupNumber = GroupService.GetSubGroupNumber(x.ParentGroup?.Number)
	            })
	            .OrderBy(x => x.ParentGroupNumber).ThenBy(x => x.SubGroupNumber)
	            .Select(x => new SelectListItem
	            {
		            Value = x.GroupId,
		            Text = x.FullGroupName
	            }).ToList();

	        return Json(groups);
        }

        [HttpPost]
        [JsonExceptionHandler(Message = "Модель с заданным именем уже существует")]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
		public ActionResult AutomaticModelCard(AutomaticModelingModel modelingModel)
		{
			if (!ModelState.IsValid)
				return GenerateMessageNonValidModel();

			var modelDto = modelingModel.ToDto();
			ModelingService.UpdateAutomaticModel(modelDto);

            return Ok();
		}

		[HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
		public ActionResult FormObjectArray(long modelId)
		{
			if (modelId == 0)
				throw new Exception("Не передан ИД модели");

            var isProcessExists = LongProcessService.CheckProcessActiveInQueue(ObjectFormationForModelingProcess.ProcessId, modelId);
			if (isProcessExists)
				throw new Exception("Процесс сбора данных для модели уже находится в очереди");

			////TODO код для отладки
			//new ObjectFormationForModelingProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	ObjectId = modelId
			//}, new CancellationToken());

			ObjectFormationForModelingProcess.AddProcessToQueue(modelId);

			return Ok();
		}

        [HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult TrainModel(long modelId, ModelType modelType)
        {
	        var areAttributesExist = OMModelFactor.Where(x => x.ModelId == modelId).ExecuteExists();
            if (!areAttributesExist)
                throw new Exception("Для модели не найдено сохраненных атрибутов");

            var inputParameters = new GeneralModelingInputParameters
            {
	            ModelId = modelId,
	            ModelType = ConvertModelType(modelType)
            };
			//////TODO код для отладки
			//new ModelingProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	Parameters = new ModelingInputParameters
			//	{
			//		Mode = ModelingMode.Training,
			//		InputParametersXml = inputParameters.SerializeToXml()
			//	}.SerializeToXml()
			//}, new CancellationToken());
			ModelingProcess.AddProcessToQueue(new ModelingInputParameters
			{
				Mode = ModelingMode.Training,
				InputParametersXml = inputParameters.SerializeToXml()
			});

			return Json(new { Message = "Процесс обучения модели поставлен в очередь" });
        }

        [HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult Predict(long modelId, ModelType modelType)
        {
            var inputParameters = new GeneralModelingInputParameters
            {
                ModelId = modelId,
                ModelType = ConvertModelType(modelType)
            };
			////TODO код для отладки
			//new ModelingProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	Parameters = new ModelingInputParameters
			//	{
			//		Mode = ModelingMode.Prediction,
			//		InputParametersXml = inputParameters.SerializeToXml()
			//	}.SerializeToXml()
			//}, new CancellationToken());
			ModelingProcess.AddProcessToQueue(new ModelingInputParameters
			{
				Mode = ModelingMode.Prediction,
				InputParametersXml = inputParameters.SerializeToXml()
			});

			return Json(new { Message = "Процесс рассчета цены на основе модели поставлен в очередь" });
        }

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult Statistic(long modelId)
        {
	        if (modelId == 0)
		        throw new Exception("Не передан ИД модели");

	        var statisticStr = OMModel.Where(x => x.Id == modelId)
		        .Select(x => x.ObjectsStatistic)
		        .ExecuteFirstOrDefault()?.ObjectsStatistic;

	        var statistic = statisticStr?.DeserializeFromXml<ModelingObjectsStatistic>();
	        if (statistic == null)
		        return Json(string.Empty);

	        return Json(new {statistic.TotalCount, Attributes = statistic.ObjectsByAttributeStatistics});
        }


        #region Факторы

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult AddAutomaticModelFactor(long generalModelId)
        {
	        var factorDto = new AutomaticFactorModel
	        {
		        Id = -1,
		        ModelId = generalModelId,
                IsActive = true
	        };

	        return View("EditAutomaticModelFactor", factorDto);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult EditAutomaticModelFactor(long? id)
        {
	       var factor = ModelFactorsService.GetFactorById(id);

	       var model = AutomaticFactorModel.ToModel(factor);

	        return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult EditAutomaticModelFactor(AutomaticFactorModel factorModel)
        {
	        var dto = factorModel.ToDto();

	        var isProcessForFactorAdditionCreated = false;
	        if (factorModel.Id == -1)
	        {
		        var hasFormedObjectArray = OMModelToMarketObjects.Where(x => x.ModelId == factorModel.ModelId).ExecuteExists();
                var queue = LongProcessService.GetProcessActiveQueue(FactorAdditionToModelObjectsLongProcess.ProcessId, factorModel.ModelId);
		        if (hasFormedObjectArray && queue != null)
		        {
			        var existedInputParameters = queue.Parameters?.DeserializeFromXml<FactorAdditionToModelObjectsInputParameters>();
			        var attributeName = existedInputParameters?.AttributeId == null 
				        ? string.Empty 
				        : RegisterCache.GetAttributeData(existedInputParameters.AttributeId).Name;
			        throw new Exception($"В очередь уже поставлен процесс сбора данных для фактора '{attributeName}'. Дождитесь его окончания");
		        }

                ModelFactorsService.AddAutomaticFactor(dto);
		        ModelingService.ResetTrainingResults(factorModel.ModelId, KoAlgoritmType.None);

		        if (hasFormedObjectArray)
		        {
			        isProcessForFactorAdditionCreated = true;
					var inputParameters = new FactorAdditionToModelObjectsInputParameters
					{
						ModelId = factorModel.ModelId.GetValueOrDefault(),
						AttributeId = factorModel.FactorId.GetValueOrDefault()
					};
					////TODO код для отладки
					//new FactorAdditionToModelObjectsLongProcess().StartProcess(new OMProcessType(), new OMQueue
					//{
					//	Status_Code = Status.Added,
					//	UserId = SRDSession.GetCurrentUserId(),
					//	Parameters = inputParameters.SerializeToXml()
					//}, new CancellationToken());

					FactorAdditionToModelObjectsLongProcess.AddProcessToQueue(inputParameters);
				}
            }
            else
	        {
		        var mustResetTrainingResult = ModelFactorsService.UpdateAutomaticFactor(dto);
		        if (mustResetTrainingResult)
		        {
			        ModelingService.ResetTrainingResults(factorModel.ModelId, KoAlgoritmType.None);
                }
	        }

	        return Json(isProcessForFactorAdditionCreated);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult DeleteAutomaticModelFactor(long? id)
        {
	        var factor = ModelFactorsService.GetFactorById(id);

            ModelFactorsService.DeleteAutomaticModelFactor(factor);

            ModelingService.ResetTrainingResults(factor.ModelId, KoAlgoritmType.None);

            var model = OMModel.Where(x => x.Id == factor.ModelId)
	            .Select(x => x.ObjectsStatistic)
	            .ExecuteFirstOrDefault();

            var statistic = model?.ObjectsStatistic?.DeserializeFromXml<ModelingObjectsStatistic>();
            var deletedFactorStatistic = statistic?.ObjectsByAttributeStatistics.FirstOrDefault(x => x.AttributeId == factor.FactorId);
            if (deletedFactorStatistic != null)
            {
	            statistic.ObjectsByAttributeStatistics.Remove(deletedFactorStatistic);
	            model.ObjectsStatistic = statistic.SerializeToXml();
	            model.Save();
            }

            return Ok();
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult GetAllAttributes(long modelId)
        {
	        var model = ModelingService.GetModelEntityById(modelId);

	        var tour = ModelingService.GetModelTour(model.GroupId);
	        var type = model.IsOksObjectType.GetValueOrDefault() ? ObjectTypeExtended.Oks : ObjectTypeExtended.Zu;

	        var availableAttributeTypes = new[]
	        {
		        Consts.IntegerAttributeType, Consts.DecimalAttributeType,
		        Consts.StringAttributeType, Consts.DateAttributeType
	        };

            var tourAttributes = TourFactorService.GetTourAttributes(tour.Id, type)
	            .Where(x => availableAttributeTypes.Contains(x.Type)).ToList();

            var marketObjectAttributes = RegisterAttributeService
		        .GetActiveRegisterAttributes(OMCoreObject.GetRegisterId())
		        .Where(x => availableAttributeTypes.Contains(x.Type)).ToList();

	        var tourAttributesTree = MapAttributes(tourAttributes.FirstOrDefault()?.RegisterId, tourAttributes);
	        var marketObjectsAttributesTree = MapAttributes(OMCoreObject.GetRegisterId(), marketObjectAttributes);

	        var fullTree = new List<DropDownTreeItemModel>
	        {
		        tourAttributesTree,
		        marketObjectsAttributesTree
	        };

	        return Json(fullTree);
        }

        #endregion


        #region Support Methods

        private KoAlgoritmType ConvertModelType(ModelType inputType)
        {
	        switch (inputType)
	        {
		        case ModelType.Linear:
			        return KoAlgoritmType.Line;
		        case ModelType.Exponential:
			        return KoAlgoritmType.Exp;
		        case ModelType.Multiplicative:
			        return KoAlgoritmType.Multi;
		        case ModelType.All:
			        return KoAlgoritmType.None;
		        default:
			        throw new ArgumentOutOfRangeException($"Неизвестный тип модели {inputType.GetEnumDescription()}");
	        }
        }

        private DropDownTreeItemModel MapAttributes(long? registerId, List<OMAttribute> attributes)
        {
	        var tourFactorsRegister = RegisterCache.Registers.Values.FirstOrDefault(x => x.Id == registerId);
	        return new DropDownTreeItemModel
	        {
		        Value = Guid.NewGuid().ToString(),
		        Text = tourFactorsRegister?.Description,
		        HasChildren = attributes.Count > 0,
		        Items = attributes.Select(x => new DropDownTreeItemModel
		        {
			        Value = x.Id.ToString(),
			        Text = x.Name
		        }).ToList()
	        };
        }

        #endregion

        #endregion


        #region Карточка ручной модели

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult ManualModelCard(long modelId, bool isPartial, bool isReadOnly = false)
        {
            var modelDto = ModelingService.GetModelById(modelId);
            var model = ManualModelingModel.ToModel(modelDto);
            model.IsReadOnly = isReadOnly;

            if (isPartial)
            {
                model.IsPartial = true;
                return PartialView(model);
            }

            return View(model);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult GetFactorsForManualModel(long modelId)
        {
	        var model = ModelingService.GetModelEntityById(modelId);

	        var tour = ModelingService.GetModelTour(model.GroupId);

	        var objectType = model.IsOksObjectType.GetValueOrDefault() ? ObjectTypeExtended.Oks : ObjectTypeExtended.Zu;
	        var tourAttributes = TourFactorService.GetTourAttributes(tour.Id, objectType);

	        var result = tourAttributes.Select(x => new
	        {
		        Text = x.Name,
		        Value = (int)x.Id
	        }).ToList();

	        return Json(result);
        }

        [HttpPost]
        [JsonExceptionHandler(Message = "Модель с заданным именем уже существует")]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult ManualModelCard(ManualModelingModel model)
        {
	        var dto = model.ToDto();

            ModelingService.UpdateManualModel(dto);

            return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult GetFormula(long modelId, long algType)
        {
            var model = ModelingService.GetModelEntityById(modelId);

            model.AlgoritmType_Code = (KoAlgoritmType)algType;
            var formula = model.GetFormulaFull(true);
            var a0 = model.GetA0();

            return Json(new { formula, a0 });
        }

        #region Факторы

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult EditManualModelFactor(long? id, long generalModelId)
        {
            ManualFactorModel manualFactorDto;

            if (id.HasValue)
            {
                var factor = ModelFactorsService.GetFactorById(id);

                manualFactorDto = new ManualFactorModel
                {
                    Id = factor.Id,
                    GeneralModelId = generalModelId,
                    FactorId = factor.FactorId,
                    Factor = RegisterCache.GetAttributeData(factor.FactorId.GetValueOrDefault()).Name,
                    MarkerId = factor.MarkerId,
                    Weight = factor.Weight,
                    B0 = factor.B0,
                    SignDiv = factor.SignDiv,
                    SignAdd = factor.SignAdd,
                    SignMarket = factor.SignMarket
                };
            }
            else
            {
                manualFactorDto = new ManualFactorModel
                {
                    Id = -1,
                    GeneralModelId = generalModelId,
                    FactorId = -1,
                    MarkerId = -1
                };
            }

            return View(manualFactorDto);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult EditManualModelFactor(ManualFactorModel manualFactorModel)
        {
            var dto = manualFactorModel.ToDto();

            var model = ModelingService.GetModelEntityById(manualFactorModel.GeneralModelId);
            dto.Type = model.AlgoritmType_Code;

            if (manualFactorModel.Id == -1)
            {
                ModelFactorsService.AddManualFactor(dto);
            }
            else
            {
                ModelFactorsService.UpdateManualFactor(dto);
            }

            return Ok();
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult DeleteModelFactor(long? id)
        {
            ModelFactorsService.DeleteManualModelFactor(id);

            return Json(new { Success = "Удаление выполненно" });
        }

        #endregion


        #region Метки

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult MarkCatalog()
        {
	        return View();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult MarksGrid(long groupId, long factorId)
        {
            ViewBag.GroupId = groupId;
            ViewBag.FactorId = factorId;

            return View();
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public JsonResult GetMarkCatalog(long groupId, long factorId)
        {
	        if (groupId == 0 || factorId == 0)
		        return Json(new List<MarkModel>());

            var marks = ModelFactorsService.GetMarks(groupId, factorId);

            var markModels = marks.Select(MarkModel.ToModel).ToList();

            return Json(markModels);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult CreateMark(MarkModel markCatalog)
        {
	        var id = ModelFactorsService.CreateMark(markCatalog.Value, markCatalog.Metka, markCatalog.FactorId, markCatalog.GroupId);
            markCatalog.Id = id;

            return Json(markCatalog);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult UpdateMark(MarkModel markCatalog)
        {
            ModelFactorsService.UpdateMark(markCatalog.Id, markCatalog.Value, markCatalog.Metka);

            return Json(markCatalog);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult DeleteMark(MarkModel markCatalog)
        {
            ModelFactorsService.DeleteMark(markCatalog.Id);

            return Json(markCatalog);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult MarksCatalogUploading(long groupId, long factorId)
        {
	        ViewBag.GroupId = groupId;
	        ViewBag.FactorId = factorId;

	        return View();
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public FileResult DownloadMarksCatalog(long groupId, long factorId)
        {
            var fileStream = DataExporterKO.ExportMarkerListToExcel(groupId, factorId);

            return File(fileStream, Consts.ExcelContentType, "Справочник меток (выгрузка)" + ".xlsx");
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult UploadMarksCatalog(long groupId, long factorId)
        {
	        ViewBag.GroupId = groupId;
	        ViewBag.FactorId = factorId;

	        return PartialView();
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult UploadMarksCatalog(IFormFile file, long groupId, long factorId, bool isDeleteOld)
        {
            if (file == null)
                throw new Exception("Не выбран файл для загрузки");
            if (!(file.FileName.EndsWith(".xlsx") || file.FileName.EndsWith(".xls")))
                throw new Exception("Загружен файл неправильного формата. Допустимые форматы: .xlsx и .xls");

            using (var stream = file.OpenReadStream())
            {
                var excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
                excelFile.DocumentProperties.Custom["FileName"] = file.FileName;

                var fileStream = DataImporterKO.ImportDataMarkerFromExcel(excelFile, nameof(OMMarkCatalog),
                    OMMarkCatalog.GetRegisterId(), groupId, factorId, isDeleteOld);

                var fileName = "Справочник меток (загрузка) " + file.FileName;
                HttpContext.Session.Set(fileName, fileStream.ToByteArray());

                return Content(JsonConvert.SerializeObject(new { success = true, fileName }), "application/json");
            }
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult DownloadExcelFile(string fileName)
        {
            var fileContent = HttpContext.Session.Get(fileName);
            if (fileContent == null)
            {
                return new EmptyResult();
            }

            HttpContext.Session.Remove(fileName);
            StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out string fileExtensiton, out string contentType);

            return File(fileContent, contentType, fileName);
        }

        #endregion

        #endregion


        #region Удаление модели

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public IActionResult ModelDelete(long modelId)
        {
	        try
	        {
		        var model = ModelingService.GetModelEntityById(modelId);

		        ViewBag.ModelName = model.Name;
		        ViewBag.ModelId = model.Id;

		        return View();
	        }
	        catch (Exception ex)
	        {
		        return SendErrorMessage(ex.Message);
	        }
        }

        [HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public IActionResult DeleteModel(long modelId)
        {
	        ModelingService.DeleteModel(modelId);

            return Json(new { Success = true });
        }

        #endregion


        #region Результаты обучения модели (из раскладки)

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult ModelTrainingResult(long modelId, KoAlgoritmType type)
        {
	        var trainingResult = ModelingService.GetTrainingResult(modelId, type);
	        
	        var model = TrainingDetailsModel.ToModel(trainingResult);

	        return View(model);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public JsonResult ExportTrainingResultToExcel(long modelId, KoAlgoritmType type)
        {
	        var fileStream = ModelingService.ExportTrainingResultToExcel(modelId, type);

	        HttpContext.Session.Set(modelId.ToString(), fileStream.ToByteArray());

	        return Json(new { FileName = modelId.ToString() });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public IActionResult DownloadTrainingResultFile(string fileName, string modelName)
        {
	        var fileInfo = GetFileFromSession(fileName, RegistersExportType.Xlsx);
	        if (fileInfo == null)
		        return new EmptyResult();

	        return File(fileInfo.FileContent, fileInfo.ContentType,
		        $"Результаты обучения модели {modelName} ({fileName}), {DateTime.Now}.{fileInfo.FileExtension}");
        }

        #region Support Methods

        private TrainingResponse GetTrainingDetails(string trainingResult)
        {
	        return string.IsNullOrWhiteSpace(trainingResult)
		        ? null
		        : JsonConvert.DeserializeObject<TrainingResponse>(trainingResult);
        }

        #endregion

        #endregion


        #region Список объектов, подобранных для процесса моделирования

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
		public ActionResult ModelObjects(long modelId)
		{
			var omModel = OMModel.Where(x => x.Id == modelId)
				.Select(x => new
				{
                    x.Name,
                    x.GroupId,
					x.ParentGroup.GroupName,
					x.ParentGroup.Number
				}).ExecuteFirstOrDefault();
			if (omModel == null)
				throw new Exception($"Не найдена модель с ИД '{modelId}'");

			var tour = ModelingService.GetModelTour(omModel.GroupId);

            var attributes = ModelFactorsService.GetGeneralModelAttributes(modelId);

            var model = ModelingObjectsModel.ToModel(omModel, tour.Year, attributes);

            return View(model);
		}

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
		public JsonResult GetObjectsForModel(long modelId)
		{
			var objectsDto = ModelingService.GetModelObjects(modelId);

            //var model = OMModel.Where(x => x.Id == modelId).Select(x => x.A0ForExponential).ExecuteFirstOrDefault();
            //if (model == null)
            //	throw new Exception($"Не найдена модель с ИД '{modelId}'");
            //TODO пока работаем только с Exp
            //var factors = ModelFactorsService.GetFactors(model.Id, KoAlgoritmType.Exp);

            //TODO код закомментирован по просьбе заказчиков, в дальнейшем он будет использоваться
            //var objectsWithErrors = new List<ModelMarketObjectRelationDto>();
            //for (var i = 0; i < objectsDto.Count; i++)
            //{
            //	var obj = objectsDto[i];
            //	var calculationParameters =
            //		ModelingService.GetModelCalculationParameters(model.A0ForExponential, obj.Price, factors,
            //			obj.Coefficients, obj.CadastralNumber);
            //	obj.ModelingPrice = calculationParameters.ModelingPrice;
            //	obj.Percent = calculationParameters.Percent;

            //	if (obj.ModelingPrice.GetValueOrDefault() == 0)
            //	{
            //		objectsWithErrors.Add(obj);
            //	}
            //}

            //var successfulModels = objectsDto.Except(objectsWithErrors).Select(ModelMarketObjectRelationModel.ToModel).ToList();
            //var errorModels = objectsWithErrors.Select(ModelMarketObjectRelationModel.ToModel).ToList();

            var successfulModels = objectsDto.Select(ModelMarketObjectRelationModel.ToModel).ToList();

            return Json(new { successfulModels });
        }

		//TODO код закомментирован по просьбе заказчиков, в дальнейшем он будет использоваться
        //[HttpGet]
        //[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        //public JsonResult GetCoefficientsForPreviousTour(long modelId)
        //{
        //	var factors = ModelFactorsService.GetFactors(modelId, KoAlgoritmType.Exp);

        //	var models = factors.Select(x => new
        //	{
        //              FactorId = x.FactorId,
        //		Name = RegisterCache.GetAttributeData(x.FactorId.GetValueOrDefault()).Name,
        //		Coefficient = x.PreviousWeight ?? 1
        //	}).OrderBy(x => x.Name).ToList();

        //	return Json(models);
        //}

        [HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
		public JsonResult ChangeObjectsStatusInCalculation(string objects)
		{
			var objectsJson = JObject.Parse(objects).SelectToken("objects").ToString();

			var allModels = JsonConvert.DeserializeObject<List<ModelMarketObjectRelationModel>>(objectsJson);
			var changedModels = allModels.Where(x => x.IsDirty).ToList();
			var objectsDtos = changedModels.Select(ModelMarketObjectRelationModel.FromModel).ToList();
			
			ModelingService.ChangeObjectsStatusInCalculation(objectsDtos);

			return Json(new { Message = "Данные успешно обновлены" });
		}

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public JsonResult ExportModelObjectsToExcel(long modelId)
        {
	        var fileStream = ModelingService.ExportMarketObjectsToExcel(modelId);

            HttpContext.Session.Set(modelId.ToString(), fileStream.ToByteArray());

            return Json(new { FileName = modelId.ToString() });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public IActionResult DownloadModelObjectsFromExcel(string fileName, string modelName)
        {
            var fileInfo = GetFileFromSession(fileName, RegistersExportType.Xlsx);
            if (fileInfo == null)
                return new EmptyResult();

            return File(fileInfo.FileContent, fileInfo.ContentType,
                $"Объекты модели {modelName} ({fileName}), {DateTime.Now}.{fileInfo.FileExtension}");
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public ActionResult UpdateModelObjects(long modelId, IFormFile file)
        {
            if (file == null)
                throw new Exception("Не выбран файл");

            ExcelFile excelFile;
            using (var stream = file.OpenReadStream())
            {
                excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
            }
            
            var updateResult = ModelingService.UpdateModelObjects(modelId, excelFile);
            
            var fileName = string.Empty;
            if (updateResult.File != null)
            {
	            fileName = "Не найденные объекты.xlsx";
	            HttpContext.Session.Set(fileName, updateResult.File.ToByteArray());
            }

            return Content(JsonConvert.SerializeObject(new
            {
	            updateResult.TotalCount, updateResult.UpdatedObjectsCount, updateResult.UnchangedObjectsCount,
	            updateResult.ErrorObjectsCount, updateResult.ErrorRowIndexes, fileName
            }), "application/json");
        }

        #endregion


        #region Корреляция

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRELATION)]
        public ActionResult Correlation()
        {
            var model = new CorrelationModel();
            try
            {
                var queryFromLayout = GetQueryFromLayout();
                model.QsQueryXmlStr = queryFromLayout.SerializeToXml();
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Дождитесь выполнения запроса в раскладке";
            }

            return View(model);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRELATION)]
        public JsonResult GetMarketObjectAttributes()
        {
            var attribute = typeof(OMCoreObject).GetProperty(nameof(OMCoreObject.Price))
                ?.GetCustomAttribute(typeof(RegisterAttributeAttribute));

            var priceAttributeId = (attribute as RegisterAttributeAttribute)?.AttributeID;

            var marketObjectAttributes = OMAttribute.Where(x =>
                    x.RegisterId == OMCoreObject.GetRegisterId() && x.Id != priceAttributeId && x.Type == 2)
                .Select(x => x.Id)
                .Select(x => x.Name)
                .OrderBy(x => x.Name)
                .Execute()
                .Select(x => new
                {
                    x.Id,
                    x.Name
                });

            return new JsonResult(marketObjectAttributes);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.MARKET_CORRELATION)]
        public JsonResult Correlation(CorrelationModel model)
        {
            if(string.IsNullOrWhiteSpace(model.QsQueryXmlStr))
                throw new Exception("Не переден запрос для формирования списка объектов");
            if (model.AttributeIds == null || model.AttributeIds.Count == 0)
                throw new Exception("Не выбраны атрибуты");
            if (model.AttributeIds.Count < 2)
                throw new Exception("Должно быть выбрано минимум два атрибута");

            var correlationInputParameters = new CorrelationInputParameters
            {
                AttributeIds = model.AttributeIds,
                QsQueryStr = model.QsQueryXmlStr
            };
            var inputRequest = new ModelingInputParameters
            {
                Mode = ModelingMode.Correlation,
                InputParametersXml = correlationInputParameters.SerializeToXml<CorrelationInputParameters>()
            };

            ////TODO код для отладки
            //var process = new ModelingProcess();
            //process.StartProcess(new OMProcessType(), new OMQueue
            //{
            //    Status_Code = Status.Added,
            //    UserId = SRDSession.GetCurrentUserId(),
            //    Parameters = inputRequest.SerializeToXml()
            //}, new CancellationToken());

            ModelingProcess.AddProcessToQueue(inputRequest);

            return new JsonResult(new {Message = "Процесс корреляции поставлен в очередь. Результат будет отправлен на почту."});
        }

        #endregion


        #region Словари моделирования

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES)]
        public ActionResult DictionaryCard(long dictionaryId, bool showItems = false)
        {
	        var dictionary = OMModelingDictionary.Where(x => x.Id == dictionaryId).SelectAll().ExecuteFirstOrDefault();
            var model = DictionaryModel.ToModel(dictionary, showItems);

	        return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_MODIFICATION)]
        public ActionResult DictionaryCard(DictionaryModel viewModel)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var id = viewModel.Id;
	        if (id == -1)
		        id = DictionaryService.CreateDictionary(viewModel.Name, viewModel.ValueType);
	        else
		        DictionaryService.UpdateDictionary(viewModel.Id, viewModel.Name, viewModel.ValueType);

            return Json(new { Success = "Сохранено успешно", Id = id });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_MODIFICATION)]
        public IActionResult DictionaryDelete(long dictionaryId)
        {
	        try
	        {
		        var dictionary = DictionaryService.GetDictionaryById(dictionaryId);

		        return View(DictionaryModel.ToModel(dictionary));
	        }
	        catch (Exception ex)
	        {
		        return SendErrorMessage(ex.Message);
	        }
        }

        [HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_MODIFICATION)]
        public IActionResult DeleteDictionary(long dictionaryId)
        {
	        try
	        {
		        DictionaryService.DeleteDictionary(dictionaryId);
	        }
	        catch (Exception ex)
	        {
		        return SendErrorMessage(ex.Message);
	        }

	        return Json(new { Success = true });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_MODIFICATION)]
        public IActionResult DictionaryImport()
        {
            ViewData["References"] = OMModelingDictionary.Where(x => true).SelectAll().Execute().Select(x => new
            {
                Text = x.Name,
                Value = x.Id
            }).ToList();

            return View(new DictionaryImportModel());
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_MODIFICATION)]
        public IActionResult DictionaryImport(IFormFile file, DictionaryImportModel viewModel)
        {
            if (!ModelState.IsValid)
	            return GenerateMessageNonValidModel();

            long? dictionaryId = null;
            object returnedData;
            try
            {
                using (var fileStream = file.OpenReadStream())
                {
	                var importInfo = new DictionaryImportFileInfoDto
                    {
                        FileName = file.FileName,
                        ValueColumnName = viewModel.Value,
                        CalcValueColumnName = viewModel.CalcValue,
                        ValueType = viewModel.ValueType
                    };

	                var import = DictionaryService.CreateDataFileImport(fileStream, importInfo.FileName);
	                fileStream.Seek(0, SeekOrigin.Begin);
                    if (DictionaryService.MustUseLongProcess(fileStream))
					{
						fileStream.Seek(0, SeekOrigin.Begin);
                        var inputParameters = new DictionaryImportFileFromExcelDto
						{
							DeleteOldValues = viewModel.Dictionary.DeleteOldValues,
							FileInfo = importInfo,
							DictionaryId = viewModel.Dictionary.DictionaryId.GetValueOrDefault(),
							IsNewDictionary = viewModel.Dictionary.IsNewDictionary,
							NewDictionaryName = viewModel.Dictionary.NewDictionaryName
						};
						////TODO для тестирования
						//new ModelDictionaryImportFromExcelLongProcess().StartProcess(new OMProcessType(), new OMQueue
						//{
						//	Status_Code = Status.Added,
						//	UserId = SRDSession.GetCurrentUserId(),
						//	ObjectId = import.Id,
						//	Parameters = inputParameters.SerializeToXml()
						//}, new CancellationToken());

						ModelDictionaryImportFromExcelLongProcess.AddProcessToQueue(fileStream, inputParameters, import);

						returnedData = new
						{
							Success = true,
							message = "Добавление справочника было поставленно в очередь долгих процессов. После добавления вы получите уведомление.",
							isLongProcess = true
						};
					}
					else
					{
						fileStream.Seek(0, SeekOrigin.Begin);

						var dictionary = viewModel.Dictionary;
						if (dictionary.IsNewDictionary)
						{
							dictionaryId = DictionaryService.CreateDictionaryFromExcel(fileStream, importInfo,
								dictionary.NewDictionaryName, import);
						}
						else
						{
							DictionaryService.UpdateDictionaryFromExcel(fileStream, importInfo,
								dictionary.DictionaryId.GetValueOrDefault(-1), dictionary.DeleteOldValues, import);
						}

						returnedData = new
						{
							Success = true,
							message = "Справочник успешно импортирован",
							newDictionaryId = viewModel.Dictionary.IsNewDictionary ? dictionaryId : null,
						};
					}
				}
			}
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return SendErrorMessage(ex.Message);
            }

            return Json(returnedData);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_VALUES)]
        public ActionResult DictionaryValueCard(long dictionaryValueId, long dictionaryId)
        {
	        var dictionaryValue = OMModelingDictionariesValues.Where(x => x.Id == dictionaryValueId).SelectAll().ExecuteFirstOrDefault();
	        dictionaryId = dictionaryValue == null ? dictionaryId : dictionaryValue.DictionaryId;
	        var dictionary = DictionaryService.GetDictionaryById(dictionaryId);

	        return View(DictionaryValueModel.ToModel(dictionaryValue, dictionary));
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_VALUES_MODIFICATION)]
        public ActionResult DictionaryValueCard(DictionaryValueModel viewModel)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        var id = viewModel.Id;
	        if (id == -1)
		        id = DictionaryService.CreateDictionaryValue(viewModel.ToDto());
	        else
		        DictionaryService.UpdateDictionaryValue(viewModel.ToDto());

            return Json(new { Success = "Сохранено успешно", Id = id });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_VALUES_MODIFICATION)]
        public IActionResult DictionaryValueDelete(long dictionaryValueId)
        {
	        try
	        {
		        var dictionaryValue = DictionaryService.GetDictionaryValueById(dictionaryValueId);
		        var dictionary = DictionaryService.GetDictionaryById(dictionaryValue.DictionaryId);

		        return View(DictionaryValueModel.ToModel(dictionaryValue, dictionary));
            }
	        catch (Exception ex)
	        {
		        return SendErrorMessage(ex.Message);
	        }
        }

        [HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_VALUES_MODIFICATION)]
        public IActionResult DeleteDictionaryValue(long dictionaryValueId)
        {
	        try
	        {
		        DictionaryService.DeleteDictionaryValue(dictionaryValueId);
            }
	        catch (Exception ex)
	        {
		        return SendErrorMessage(ex.Message);
	        }

            return Json(new { Success = true });
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES)]
        public JsonResult GetDictionaries()
        {
	        var dictionaries = DictionaryService.GetDictionaries().Select(x => new SelectListItem
            {
		        Text = x.Name,
		        Value = x.Id.ToString()
	        }).ToList();

	        return Json(dictionaries);
        }

        #endregion


        #region Support Methods

        private QSQuery GetQueryFromLayout()
        {
            var databaseFilters = OMQry.Where(x =>
                    x.RegisterViewId == "MarketObjects" && x.QryId > 1000000 &&
                    (x.IsCommon || x.UserId == SRDSession.GetCurrentUserId()))
                .Select(x => x.QSCondition)
                .Execute()
                ?.Select(x => x.QSCondition?.DeserializeFromXml<QSCondition>()).ToList();

            var conditionGroup = new QSConditionGroup(QSConditionGroupType.And);
            databaseFilters?.ForEach(x => conditionGroup.Add(x));

            var personalFilter = RegistersVariables.CurrentQueryFilter;
            if (personalFilter != null)
                conditionGroup.Add(personalFilter);

            var generalQuery = RegistersCommon.GetCurrentRegisterQuery();
            generalQuery.Condition = conditionGroup;

            return generalQuery;
        }

        #endregion
    }
}
