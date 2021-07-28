using System;
using System.Linq;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.Register;
using Core.Register.Enums;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.CoreUI.Registers;
using GemBox.Spreadsheet;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectModel.Core.Register;
using KadOzenka.Dal.Registers;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using ObjectModel.KO;
using System.IO;
using System.Threading;
using CommonSdks;
using CommonSdks.PlatformWrappers;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Directory;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.LongProcess.Common;
using KadOzenka.Dal.LongProcess.Modeling;
using KadOzenka.Dal.LongProcess.Modeling.InputParameters;
using KadOzenka.Web.Exceptions;
using KadOzenka.Web.Helpers;
using Microsoft.Practices.ObjectBuilder2;
using Npgsql;
using ObjectModel.Ko;
using ObjectModel.Modeling;
using Consts = KadOzenka.Web.Helpers.Consts;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;
using Kendo.Mvc.Extensions;
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Dictionaries.Entities;
using ModelingBusiness.Factors;
using ModelingBusiness.Factors.Repositories;
using ModelingBusiness.Model;
using ModelingBusiness.Model.Entities;
using ModelingBusiness.Model.Repositories;
using ModelingBusiness.Modeling;
using ModelingBusiness.Modeling.Entities;
using ModelingBusiness.Modeling.InputParameters;
using ModelingBusiness.Objects;
using ModelingBusiness.Objects.Entities;
using ModelingBusiness.Objects.Import;
using ModelingBusiness.Objects.Repositories;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.Directory.Ko;
using StringExtensions = Core.Shared.Extensions.StringExtensions;

namespace KadOzenka.Web.Controllers
{
    public class ModelingController : KoBaseController
    {
        public IModelService ModelService { get; set; }
        public IModelingService ModelingService { get; set; }
        public IModelObjectsService ModelObjectsService { get; set; }
        public TourFactorService TourFactorService { get; set; }
        public IRegisterAttributeService RegisterAttributeService { get; set; }
        public ModelDictionaryService ModelDictionaryService { get; set; }
        public IModelFactorsRepository ModelFactorsRepository { get; set; }
        public IModelFactorsService ModelFactorsService { get; set; }
        public GroupService GroupService { get; set; }
        public IModelObjectsRepository ModelObjectsRepository { get; set; }
        public IModelRepository ModelRepository { get; set; }
        public ILongProcessService LongProcessService { get; set; }
        public IBaseModelObjectsImporter ModelObjectsImporterService { get; set; }


        public ModelingController(IModelService modelService, TourFactorService tourFactorService,
	        IRegisterAttributeService registerAttributeService, ModelDictionaryService modelDictionaryService,
	        IModelFactorsService modelFactorsService, GroupService groupService,
	        IModelObjectsRepository modelObjectsRepository, IModelRepository modelRepository,
	        IModelObjectsService modelObjectsService, ILongProcessService longProcessService,
	        IRegisterCacheWrapper registerCacheWrapper, IGbuObjectService gbuObjectService,
	        IModelFactorsRepository modelFactorsRepository,
            IModelingService modelingService,
	        IBaseModelObjectsImporter modelObjectsImporterService)
	        : base(gbuObjectService, registerCacheWrapper)
        {
            ModelService = modelService;
            TourFactorService = tourFactorService;
            RegisterAttributeService = registerAttributeService;
            ModelDictionaryService = modelDictionaryService;
            ModelFactorsService = modelFactorsService;
            GroupService = groupService;
            ModelObjectsRepository = modelObjectsRepository;
            ModelRepository = modelRepository;
            ModelObjectsService = modelObjectsService;
            LongProcessService = longProcessService;
            ModelFactorsRepository = modelFactorsRepository;
            ModelingService = modelingService;
            ModelObjectsImporterService = modelObjectsImporterService;
        }



        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult ModelCard(long modelId, bool isPartial = false, bool isReadOnly = false)
        {
            var isGroupExists = ModelService.IsModelGroupExist(modelId);
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
        [JsonExceptionOverwrite(ExceptionType = nameof(PostgresException), ExceptionCode = PostgresErrorCodes.UniqueViolation, Message = "Модель с заданным именем уже существует")]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_ADD_MODEL)]
        public JsonResult AddModel(GeneralModelingModel modelingModel)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var modelDto = modelingModel.ToDto();
            if (modelDto.Type == KoModelType.Automatic)
            {
                ModelService.AddAutomaticModel(modelDto);
            }
            else
            {
                ModelService.AddManualModel(modelDto);
            }

            return Json(new { Message = "Сохранение выполнено" });
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult ActivateModel(long modelId)
        {
	        ModelService.ActivateModel(modelId);

            return Ok();
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult DeactivateModel(long groupId)
        {
	        ModelService.DeactivateModel(groupId);

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
            var modelDto = ModelService.GetModelById(modelId);

            var hasFormedObjectArray = ModelObjectsRepository.AreIncludedModelObjectsExist(modelId, IncludedObjectsMode.All);
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
        //используется в modeling.js
        public JsonResult GetModelFactors(long modelId, KoAlgoritmType type)
        {
	        var model = ModelService.GetModelEntityById(modelId);
            var attributes = ModelFactorsService.GetFactors(modelId);

            var resultType = model.IsAutomatic ? type : model.AlgoritmType_Code;
            attributes.ForEach(x => x.Coefficient = x.GetCoefficient(resultType));

            return Json(attributes);
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
                    FullGroupName = $"{x.ParentGroup?.Number}. {x.ParentGroup?.GroupName}",
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
            ModelService.UpdateAutomaticModel(modelDto);

            return Ok();
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult CalculateMarks(long modelId)
        {
	        ////TODO код для отладки
			//new MarksCalculationLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//  ObjectId = modelId
			//}, new CancellationToken());

			MarksCalculationLongProcess.AddProcessToQueue(modelId);

	        return Ok();
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult CalculateAutomaticModelParameters(long modelId)
        {
			////TODO код для отладки
			//new AutomaticModelParametersCalculationLongProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	ObjectId = modelId
			//}, new CancellationToken());

			AutomaticModelParametersCalculationLongProcess.AddProcessToQueue(modelId);

	        return Ok();
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult FormObjectArray(long modelId)
        {
            if (modelId == 0)
                throw new Exception("Не передан ИД модели");

            var isProcessExists = LongProcessService.HasActiveProcessInQueue(ObjectFormationForModelingProcess.ProcessId, modelId);
            if (isProcessExists)
                throw new Exception(Messages.ObjectFormationProcessAlreadyAdded);

            var inputParameters = new ObjectFormationInputParameters { ModelId = modelId };
			////TODO код для отладки
			//new ObjectFormationForModelingProcess().StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	Parameters = inputParameters.SerializeToXml()
			//}, new CancellationToken());

			ObjectFormationForModelingProcess.AddProcessToQueue(inputParameters);

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
	        var unActiveAttributes = ModelFactorsService.GetAttributesWhichMustBeUnActive();
	        var attributes = GetPossibleFactorsForAutomaticModel(generalModelId);
            var factorDto = new AutomaticFactorModel(attributes, unActiveAttributes)
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

           var unActiveAttributes = ModelFactorsService.GetAttributesWhichMustBeUnActive();
           var attributes = GetPossibleFactorsForAutomaticModel(factor.ModelId.GetValueOrDefault());
           var model = AutomaticFactorModel.ToModel(factor, attributes, unActiveAttributes);

           if (factor.DictionaryId != null)
           {
               //todo после ТЗ на авто. модель объединить с ручной
	           var dictionary = ModelDictionaryService.GetDictionaryById(factor.DictionaryId.Value);
	           model.DictionaryName = dictionary.Name;
           }

           return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult EditAutomaticModelFactor(AutomaticFactorModel factorModel)
        {
            var dto = factorModel.ToDto();

            var isProcessForFactorAdditionCreated = false;
            if (factorModel.IsNewFactor)
            {
                //var hasFormedObjectArray = OMModelToMarketObjects.Where(x => x.ModelId == factorModel.ModelId).ExecuteExists();
                //var queue = LongProcessService.GetProcessActiveQueue(FactorAdditionToModelObjectsLongProcess.ProcessId, factorModel.ModelId);
                //if (hasFormedObjectArray && queue != null)
                //{
                //    var existedInputParameters = queue.Parameters?.DeserializeFromXml<FactorAdditionToModelObjectsInputParameters>();
                //    var attributeName = existedInputParameters?.AttributeId == null
                //        ? string.Empty
                //        : RegisterCache.GetAttributeData(existedInputParameters.AttributeId).Name;
                //    throw new Exception($"В очередь уже поставлен процесс сбора данных для фактора '{attributeName}'. Дождитесь его окончания");
                //}

                dto.DictionaryId = CreateDictionary(factorModel.DictionaryName, factorModel.FactorId, factorModel.MarkType, factorModel.ModelId);
                ModelFactorsService.AddAutomaticFactor(dto);
                ModelingService.ResetTrainingResults(factorModel.ModelId, KoAlgoritmType.None);

                //if (hasFormedObjectArray)
                //{
                //    isProcessForFactorAdditionCreated = true;
                //    var inputParameters = new FactorAdditionToModelObjectsInputParameters
                //    {
                //        ModelId = factorModel.ModelId.GetValueOrDefault(),
                //        AttributeId = factorModel.FactorId.GetValueOrDefault()
                //    };
                //    ////TODO код для отладки
                //    //new FactorAdditionToModelObjectsLongProcess().StartProcess(new OMProcessType(), new OMQueue
                //    //{
                //    //	Status_Code = Status.Added,
                //    //	UserId = SRDSession.GetCurrentUserId(),
                //    //	Parameters = inputParameters.SerializeToXml()
                //    //}, new CancellationToken());

                //    FactorAdditionToModelObjectsLongProcess.AddProcessToQueue(inputParameters);
                //}
            }
            else
            {
	            if (factorModel.DictionaryId != null)
	            {
		            UpdateDictionary(factorModel.DictionaryId.Value, factorModel.DictionaryName, factorModel.ModelId);
	            }
	            else
	            {
		            dto.DictionaryId = CreateDictionary(factorModel.DictionaryName, factorModel.FactorId, factorModel.MarkType, factorModel.ModelId);
	            }
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
            ModelFactorsService.DeleteAutomaticModelFactor(id);
            return Ok();
        }

        #endregion


        #region Support Methods

        private List<DropDownTreeItemModel> GetPossibleFactorsForAutomaticModel(long modelId)
        {
            var model = ModelService.GetModelEntityById(modelId);

            var tour = ModelService.GetModelTour(model.GroupId);
            var type = model.IsOksObjectType.GetValueOrDefault() ? ObjectTypeExtended.Oks : ObjectTypeExtended.Zu;

            var availableAttributeTypes = new[]
            {
                Consts.IntegerAttributeType, Consts.DecimalAttributeType,
                Consts.StringAttributeType, Consts.DateAttributeType,
                Consts.BoolAttributeType
            };

            var tourAttributes = TourFactorService.GetTourAttributes(tour.Id, type)
                .Where(x => availableAttributeTypes.Contains(x.Type)).ToList();

            var marketObjectAttributes = RegisterAttributeService
                .GetActiveRegisterAttributes(MarketPlaceBusiness.Common.Consts.RegisterId)
                .Where(x => availableAttributeTypes.Contains(x.Type)).ToList();

            var tourAttributesTree = MapAttributes(tourAttributes.FirstOrDefault()?.RegisterId, tourAttributes);
            var marketObjectsAttributesTree = MapAttributes(MarketPlaceBusiness.Common.Consts.RegisterId, marketObjectAttributes);

            var fullTree = new List<DropDownTreeItemModel>
            {
                tourAttributesTree,
                marketObjectsAttributesTree
            };

            return fullTree;
        }

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
            var modelDto = ModelService.GetModelById(modelId);
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
            var model = ModelService.GetModelEntityById(modelId);

            var tour = ModelService.GetModelTour(model.GroupId);

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

            ModelService.UpdateManualModel(dto);

            return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        //используется в modeling.js
        public JsonResult GetFormulaInfo(long modelId, long algType)
        {
            var model = ModelService.GetModelEntityById(modelId);

            var type =  (KoAlgoritmType)algType;
            var formula = ModelService.GetFormula(model, type);
            var a0 = model.GetA0(type);

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
	            var model = ModelService.GetModelEntityById(generalModelId);
                var factor = ModelFactorsService.GetFactorById(id);

                manualFactorDto = ManualFactorModel.ToModel(factor, model.AlgoritmType_Code);
                if (factor.DictionaryId != null)
                {
	                var dictionary = ModelDictionaryService.GetDictionaryById(factor.DictionaryId.Value);
	                manualFactorDto.DictionaryName = dictionary.Name;
                }
            }
            else
            {
                manualFactorDto = new ManualFactorModel
                {
                    Id = -1,
                    ModelId = generalModelId,
                    FactorId = -1
                };
            }

            return View(manualFactorDto);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult EditManualModelFactor(ManualFactorModel manualFactorModel)
        {
            var dto = manualFactorModel.ToDto();

            var model = ModelService.GetModelEntityById(manualFactorModel.ModelId);
            dto.Type = model.AlgoritmType_Code;

            if (manualFactorModel.IsNewFactor)
            {
	            dto.DictionaryId = CreateDictionary(manualFactorModel.DictionaryName, manualFactorModel.FactorId, manualFactorModel.MarkType, manualFactorModel.ModelId);
	            ModelFactorsService.AddManualFactor(dto);
            }
            else
            {
	            if (dto.DictionaryId != null)
	            {
		            UpdateDictionary(dto.DictionaryId.Value, manualFactorModel.DictionaryName, manualFactorModel.ModelId);
	            }
	            else
	            {
		            dto.DictionaryId = CreateDictionary(manualFactorModel.DictionaryName, manualFactorModel.FactorId, manualFactorModel.MarkType, manualFactorModel.ModelId);
	            }
                ModelFactorsService.UpdateManualFactor(dto);
            }

            return Ok();
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult DeleteManualModelFactor(long? id)
        {
            ModelFactorsService.DeleteManualModelFactor(id);

            return Ok();
        }

        #endregion


        #region Метки

        /// <summary>
        /// Метод из левого меню в Турах
        /// </summary>
        /// <returns></returns>
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult MarkCatalog()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDictionaryId(long groupId, long factorId)
        {
	        var dictionaryId = ModelingService.GetDictionaryId(groupId, factorId);

            return new JsonResult(dictionaryId);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult MarksGrid(bool isReadOnly, long dictionaryId)
        {
	        ViewBag.IsReadOnly = isReadOnly;
	        ViewBag.DictionaryId = dictionaryId;

	        return PartialView();
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public JsonResult GetMarks(long dictionaryId)
        {
	        var marks = ModelDictionaryService.GetMarks(dictionaryId).Select(MarkModel.ToModel).ToList();

            return Json(marks);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult CreateMark(long dictionaryId, MarkModel markCatalog)
        {
	        ValidateMarkModification(dictionaryId);

	        var dto = markCatalog.ToDto(dictionaryId);
	        
	        var id = ModelDictionaryService.CreateMark(dto);
	        markCatalog.Id = id;

	        return Json(markCatalog);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult UpdateMark(long dictionaryId, MarkModel markCatalog)
        {
	        ValidateMarkModification(dictionaryId);

            var dto = markCatalog.ToDto(dictionaryId);
	        ModelDictionaryService.UpdateMark(dto);

            return Json(markCatalog);
        }

        [HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult DeleteMark(long dictionaryId, long markId)
        {
			ValidateMarkModification(dictionaryId);

			ModelDictionaryService.DeleteMark(markId);

			return Ok();
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public FileResult DownloadMarks(long dictionaryId)
        {
	        var fileStream = ModelDictionaryService.ExportMarksToExcel(dictionaryId);

	        return File(fileStream, Consts.ExcelContentType, "Справочник меток (выгрузка)" + ".xlsx");
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult ModelDictionaries(long modelId, bool isPartial = false)
        {
	        var model = ModelService.GetModelById(modelId);

	        ViewBag.ModelId = model.ModelId;
	        ViewBag.IsReadOnly = model.Type == KoModelType.Automatic;

	        if (isPartial)
		        return PartialView();

            return View();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public JsonResult GetModelDictionaries([DataSourceRequest] DataSourceRequest request, long modelId)
        {
	        var modelAttributes = ModelFactorsService.GetFactors(modelId)
		        .Where(x => x.IsNormalized).Select(DictionaryModel.ToModel)
		        .ToList();

	        return Json(modelAttributes.ToDataSourceResult(request));
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_MODIFICATION)]
        public IActionResult DictionaryImport(long dictionaryId)
        {
            ValidateMarkModification(dictionaryId);

            var model = new DictionaryImportModel
            {
                DictionaryId = dictionaryId
            };

            return PartialView(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_MODIFICATION)]
        public IActionResult DictionaryImport(IFormFile file, DictionaryImportModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            ValidateMarkModification(model.DictionaryId);

	        OMImportDataLog import;
	        bool isViaLongProcess;
	        using (var fileStream = file.OpenReadStream())
	        {
		        import = ModelDictionaryService.CreateDataFileImport(fileStream, file.FileName);
		        isViaLongProcess = MustUseLongProcessToImportDictionary(fileStream);
	        }

	        var importInfo = new DictionaryImportFileInfoDto
	        {
		        FileName = file.FileName,
		        ValueColumnName = model.Value,
		        CalcValueColumnName = model.CalculationValue
	        };
            if (isViaLongProcess)
	        {
		        var inputParameters = new DictionaryImportFileFromExcelDto
		        {
			        DeleteOldValues = model.IsDeleteOldValues,
			        FileInfo = importInfo,
			        DictionaryId = model.DictionaryId
		        };
				////TODO для тестирования
				//new ModelDictionaryImportFromExcelLongProcess().StartProcess(new OMProcessType(), new OMQueue
				//{
				//	Status_Code = Status.Added,
				//	UserId = SRDSession.GetCurrentUserId(),
				//	ObjectId = import.Id,
				//	Parameters = inputParameters.SerializeToXml()
				//}, new CancellationToken());

				ModelDictionaryImportFromExcelLongProcess.AddProcessToQueue(inputParameters, import);
	        }
	        else
	        {
		        ModelDictionaryService.UpdateDictionaryFromExcel(import, importInfo,
			        model.DictionaryId, model.IsDeleteOldValues);
	        }

	        return Json(new {isViaLongProcess});
        }

        #region Support Methods

        private void ValidateMarkModification(long dictionaryId)
        {
	        var factor = ModelFactorsRepository.GetFactorByDictionary(dictionaryId);
	        var model = ModelService.GetModelById(factor.ModelId.GetValueOrDefault());
	        if (model.Type == KoModelType.Automatic)
		        throw new AutomaticModelMarkModificationException();
        }

        private bool MustUseLongProcessToImportDictionary(Stream fileStream)
        {
	        fileStream.Seek(0, SeekOrigin.Begin);

	        var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);

	        var maxRowCount = 1000;
	        var mainWorkSheet = excelFile.Worksheets[0];

	        return mainWorkSheet.Rows.Count > maxRowCount;
        }

        #endregion

        #endregion

        #endregion


        #region Удаление модели

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public IActionResult ModelDelete(long modelId)
        {
            try
            {
                var model = ModelService.GetModelEntityById(modelId);

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
            ModelService.DeleteModel(modelId);

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

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public IActionResult UpdateTrainingQualityInfo(TrainingDetailsModel model)
        {
            var dto = model.TrainingQualityInfoModel.FromModel();

            ModelingService.UpdateTrainingQualityInfo(model.ModelId, model.Type, dto);

            return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public JsonResult ExportTrainingResultToExcel(long modelId, KoAlgoritmType type)
        {
            var fileStream = ModelingService.ExportQualityInfoToExcel(modelId, type);

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

        #endregion


        #region Список объектов, подобранных для процесса моделирования

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public ActionResult ModelObjects(long modelId)
        {
	        var modelDto = ModelService.GetModelById(modelId);
            
            var attributes = ModelFactorsService.GetFactors(modelId);

            var model = ModelingObjectsModel.ToModel(modelDto, attributes);

            return View(model);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public JsonResult GetObjectsForModel(long modelId)
        {
            var objectsDto = ModelObjectsService.GetModelObjects(modelId);

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
            //		ModelService.GetModelCalculationParameters(model.A0ForExponential, obj.Price, factors,
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

            ModelObjectsService.ChangeObjectsStatusInCalculation(objectsDtos);

            return Json(new { Message = "Данные успешно обновлены" });
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public JsonResult ExportModelObjectsToExcel(long modelId)
        {
            var factors = ModelFactorsService.GetFactors(modelId);
            var fileStream = ModelObjectsService.ExportMarketObjectsToExcel(modelId, factors);

            var modelName = ModelRepository.GetById(modelId, x => new { x.Name })?.Name;
            var fileName = $"Объекты модели {modelName}";
            HttpContext.Session.Set(fileName, fileStream.ToByteArray());

            return Json(new { FileName = fileName });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public IActionResult ModelObjectsImporter(long modelId)
        {
            MarksCalculationLongProcess.CheckActiveProcessInQueue(modelId);
            ModelingProcess.CheckActiveProcessInQueue(modelId);

            var modelObjectsRegister = RegisterCache.GetRegisterData(OMModelToMarketObjects.GetRegisterId());
            var registerInfo = new
            {
                Description = modelObjectsRegister.Description,
                AttributeId = modelObjectsRegister.Id
            };

            var source = new List<object> { registerInfo };
            var exceptions = new List<long>
            {
                //OMModelToMarketObjects.GetColumnAttributeId(x => x.Id),
                OMModelToMarketObjects.GetColumnAttributeId(x => x.ModelId),
                OMModelToMarketObjects.GetColumnAttributeId(x => x.Coefficients),
                //OMModelToMarketObjects.GetColumnAttributeId(x => x.CadastralNumber),
                OMModelToMarketObjects.GetColumnAttributeId(x => x.PriceFromModel),
                OMModelToMarketObjects.GetColumnAttributeId(x => x.MarketObjectId),
                OMModelToMarketObjects.GetColumnAttributeId(x => x.UnitId),
                //OMModelToMarketObjects.GetColumnAttributeId(x => x.UnitPropertyType),
                //OMModelToMarketObjects.GetColumnAttributeId(x => x.UnitPropertyType_Code)
            };

            RegisterCache.RegisterAttributes.Values
                .Where(x => x.RegisterId == modelObjectsRegister.Id && !exceptions.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ForEach(x =>
                {
                    source.Add(new
                    {
                        Description = x.Name,
                        AttributeId = x.Id,
                        ParentId = modelObjectsRegister.Id
                    });
                });

            var attributes = ModelFactorsService.GetFactors(modelId);
            attributes.ForEach(x =>
            {
	            source.Add(new
	            {
		            Description = $"{x.AttributeName} (значение)",
		            AttributeId = x.AttributeId,
		            ParentId = modelObjectsRegister.Id
	            });
            });

            ViewBag.Attributes = source;
            ViewBag.ModelId = modelId;

            return PartialView();
        }


        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public ActionResult ChangeModelObjects(ModelObjectsImporterModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var fileName = model.File.FileName;
            if (model.IsBackgroundDownload)
            {
                using (var stream = model.File.OpenReadStream())
                {
                    var importDataLogId = UpdateModelObjectsLongProcess.AddToQueue(stream, fileName, model.Map());

                    return new JsonResult(new { importDataLogId = importDataLogId });
                }
            }

            ExcelFile excelFile;
            using (var stream = model.File.OpenReadStream())
            {
                excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
            }
            var resultStream = ModelObjectsImporterService.ChangeObjects(excelFile, model.Map());

            HttpContext.Session.Set(fileName, resultStream.ToByteArray());

            return Json(new { fileName = fileName });
        }


        #region График вылетов

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public ActionResult ModelObjectsDiagram(long modelId)
        {
	        var model = new ModelingObjectsDiagramModel
	        {
		        ModelId = modelId,
		        TrainingSampleType = TrainingSampleType.Control
	        };

	        return PartialView(model);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public ActionResult GetObjectsForDiagram(long modelId, TrainingSampleType trainingSampleType)
        {
			////TODO для тестирования
			//var objects = new List<ObjectInfo>();
			//for (var i = 0; i < 30000; i++)
			//{
			// objects.Add(new ObjectInfo
			// {
			//  Id = i,
			//  Price = i // new Random().Next(100, int.MaxValue)
			// });
			//}

			//var averagePrice = objects.Average(x => x.Price);
			//var delta = averagePrice * 5 / 100;
			//var model = new ObjectsInfoForDiagram
			//{
			// Average = objects.Average(x => x.Price),
			// Delta = delta,
			// Info = objects.OrderBy(x => x.Price).ToList()
			//};

			//return Json(model);

			var objects = ModelObjectsRepository.GetIncludedObjectsForTraining(modelId, trainingSampleType, select => new { select.Price }).ToList();
			if (objects.Count == 0)
		        return Json(new ObjectsInfoForDiagram());

	        var averagePrice = objects.Average(x => x.Price);
	        // 5%
	        var delta = averagePrice * 5 / 100;
	        var mappedObjects = objects.OrderBy(x => x.Price).Select(x => new ObjectInfo
	        {
		        Id = x.Id,
		        Price = x.Price
	        }).ToList();

	        var model = new ObjectsInfoForDiagram
	        {
		        Average = averagePrice,
		        Delta = delta,
		        Info = mappedObjects
	        };

	        return Json(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public ActionResult ExcludeObjectFromCalculation(long objectId)
        {
	        ModelObjectsService.ExcludeObjectFromCalculation(objectId);

            return Ok();
        }

        #endregion

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
	        var marketObjectAttributes = OMAttribute.Where(x =>
			        x.RegisterId == MarketPlaceBusiness.Common.Consts.RegisterId &&
			        x.Id != MarketPlaceBusiness.Common.Consts.PriceAttributeId &&
			        x.Type == (int) RegisterAttributeType.DECIMAL)
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

        private long? CreateDictionary(string dictionaryName, long? factorId, MarkType markType, long? modelId)
        {
	        if (string.IsNullOrWhiteSpace(dictionaryName)) 
		        return null;

	        if (markType != MarkType.Default)
		        throw new Exception($"Словарь может быть добавлен только для фактора с типом метки '{MarkType.Default.GetEnumDescription()}'");

	        var attribute = RegisterCacheWrapper.GetAttributeData(factorId.GetValueOrDefault());
	        var modelDictionariesIds = GetModelDictionariesIds(modelId);

            return ModelDictionaryService.CreateDictionary(dictionaryName, attribute.Type, modelDictionariesIds);
        }

        private void UpdateDictionary(long dictionaryId, string dictionaryName, long? modelId)
        {
	        var modelDictionariesIds = GetModelDictionariesIds(modelId);

	        ModelDictionaryService.UpdateDictionary(dictionaryId, dictionaryName, modelDictionariesIds);
        }

        private List<long> GetModelDictionariesIds(long? modelId)
        {
	        return ModelFactorsService
		        .GetFactors(modelId.GetValueOrDefault())
		        .Select(x => x.DictionaryId.GetValueOrDefault()).Distinct().ToList();
        }

        #endregion
    }
}
