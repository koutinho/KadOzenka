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
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.Registers;
using KadOzenka.Web.Helpers;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using ObjectModel.KO;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;
using System.IO;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.GbuObject.Dto;

namespace KadOzenka.Web.Controllers
{
	public class ModelingController : KoBaseController
	{
		public BaseModelingService ModelingService { get; set; }
        public TourFactorService TourFactorService { get; set; }
        public RegisterAttributeService RegisterAttributeService { get; set; }
        public DictionaryService DictionaryService { get; set; }
        public ModelFactorsService ModelFactorsService { get; set; }


        public ModelingController(BaseModelingService modelingService, TourFactorService tourFactorService,
            RegisterAttributeService registerAttributeService, DictionaryService dictionaryService,
            ModelFactorsService modelFactorsService)
        {
            ModelingService = modelingService;
            TourFactorService = tourFactorService;
            RegisterAttributeService = registerAttributeService;
            DictionaryService = dictionaryService;
            ModelFactorsService = modelFactorsService;
        }


        #region Карточка модели (из справочника моделей)

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
		public ActionResult ModelCard(long modelId)
		{
			var modelDto = ModelingService.GetModelById(modelId);

			var model = ModelingModel.ToModel(modelDto);

            return View(model);
		}

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult GetAllAttributes(long tourId, int objectType, List<long> exceptedAttributes)
        {
            var koAttributes = TourFactorService.GetTourAttributes(tourId, (ObjectTypeExtended)objectType);

            var availableAttributeTypes = new[]
            {
                Consts.IntegerAttributeType, Consts.DecimalAttributeType,
                Consts.StringAttributeType, Consts.DateAttributeType
            };
            var marketObjectAttributes = RegisterAttributeService
                .GetActiveRegisterAttributes(OMCoreObject.GetRegisterId())
                .Where(x => availableAttributeTypes.Contains(x.Type)).ToList();

            if (exceptedAttributes != null && exceptedAttributes.Count > 0)
            {
                koAttributes = koAttributes.Where(x => !exceptedAttributes.Contains(x.Id)).ToList();

                marketObjectAttributes = marketObjectAttributes.Where(x => !exceptedAttributes.Contains(x.Id)).ToList();
            }

            var tourFactorsRegister = RegisterCache.Registers.Values.FirstOrDefault(x => x.Id == koAttributes.FirstOrDefault()?.RegisterId);
            var tourAttributesTree = new DropDownTreeItemModel
            {
                Value = Guid.NewGuid().ToString(),
                Text = tourFactorsRegister?.Description,
                HasChildren = koAttributes.Count > 0,
                Items = koAttributes.Select(x => new DropDownTreeItemModel
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };

            var marketObjectsRegister = RegisterCache.Registers.Values.FirstOrDefault(x => x.Id == OMCoreObject.GetRegisterId());
            var marketObjectsRegisterAttributes = new DropDownTreeItemModel
            {
                Value = Guid.NewGuid().ToString(),
                Text = marketObjectsRegister?.Description,
                HasChildren = marketObjectAttributes.Count > 0,
                Items = marketObjectAttributes.Select(x => new DropDownTreeItemModel
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };

            var fullTree = new List<DropDownTreeItemModel>
            {
                tourAttributesTree,
                marketObjectsRegisterAttributes
            };

            return Json(fullTree);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult GetAllAttributesByModel(long generalModelId)
        {
	        var generalModel = ModelingService.GetModelById(generalModelId);
	        var objectType = generalModel.IsOksObjectType ? ObjectType.Oks : ObjectType.ZU;

	        return GetAllAttributes(generalModel.TourId, (int) objectType, null);
        }

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
		public JsonResult GetModelAttributes(long modelId, KoAlgoritmType type)
		{
			var attributes = ModelFactorsService.GetModelAttributes(modelId, type);

			return Json(attributes);
		}

		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult GetGroups(long tourId)
        {
            var groups = ModelingService.GetGroups(tourId)
                .Select(x => new SelectListItem
                {
                    Value = x.GroupId.ToString(),
                    Text = x.Name
                });

            return Json(groups);
        }

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_ADD_MODEL)]
		public ActionResult AddModel()
		{
            return View();
		}

		[HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_ADD_MODEL)]
		public JsonResult AddModel(ModelingModel modelingModel)
		{
			if (!ModelState.IsValid)
				return GenerateMessageNonValidModel();

			var modelDto = ModelingModel.FromModel(modelingModel);
			ModelingService.AddModel(modelDto);

			return Json(new {Message = "Сохранение выполнено"});
		}

		[HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
		public JsonResult UpdateModel(ModelingModel modelingModel)
		{
			if (!ModelState.IsValid)
				return GenerateMessageNonValidModel();

			var modelDto = ModelingModel.FromModel(modelingModel);
			var isModelChanged = ModelingService.UpdateModel(modelDto);

            return Json(new { IsModelWasChanged = isModelChanged, Message = "Обновление выполнено" });
		}

        [HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public JsonResult TrainModel(long modelId, ModelType modelType)
        {
	        var type = ConvertModelType(modelType);
            var areAttributesExist = OMModelFactor.Where(x => x.ModelId == modelId && x.AlgorithmType_Code == type).ExecuteExists();
            if (!areAttributesExist)
                throw new Exception("Для модели не найдено сохраненных атрибутов");

            var inputParameters = new GeneralModelingInputParameters
            {
                ModelId = modelId,
                ModelType = type
            };
            //////TODO код для отладки
            //new ModelingProcess().StartProcess(new OMProcessType(), new OMQueue
            //{
            //    Status_Code = Status.Added,
            //    UserId = SRDSession.GetCurrentUserId(),
            //    Parameters = new ModelingInputParameters
            //    {
            //        Mode = ModelingMode.Training,
            //        InputParametersXml = inputParameters.SerializeToXml()
            //    }.SerializeToXml()
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
            //    Status_Code = Status.Added,
            //    UserId = SRDSession.GetCurrentUserId(),
            //    Parameters = new ModelingInputParameters
            //    {
            //        Mode = ModelingMode.Prediction,
            //        InputParametersXml = inputParameters.SerializeToXml()
            //    }.SerializeToXml()
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
        public FileResult DownloadLogs(long modelId)
        {
            var fileStream = ModelingService.GetLogs(modelId);

            return File(fileStream, Helpers.Consts.ExcelContentType, $"Логи для модели {modelId}" + ".xlsx");
        }


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

        #endregion

        #endregion


        #region Результаты обучения модели (из раскладки)

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult LinearModelDetails(long modelId)
        {
	        var model = ModelingService.GetModelEntityById(modelId);

	        var trainingResult = GetTrainingDetails(model.LinearTrainingResult);
	        var details = TrainingDetailsModel.ToModel(trainingResult);

	        return View("ModelTrainingResult", details);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult ExponentialModelDetails(long modelId)
        {
	        var model = ModelingService.GetModelEntityById(modelId);

	        var trainingResult = GetTrainingDetails(model.ExponentialTrainingResult);
	        var details = TrainingDetailsModel.ToModel(trainingResult);

	        return View("ModelTrainingResult", details);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult MultiplicativeModelDetails(long modelId)
        {
	        var model = ModelingService.GetModelEntityById(modelId);

	        var trainingResult = GetTrainingDetails(model.MultiplicativeTrainingResult);
	        var details = TrainingDetailsModel.ToModel(trainingResult);

	        return View("ModelTrainingResult", details);
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
            var modelDto = ModelingService.GetModelById(modelId);
            modelDto.Attributes = ModelFactorsService.GetGeneralModelAttributes(modelId);

            var model = ModelingModel.ToModel(modelDto);

            return View(model);
		}

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
		public JsonResult GetObjectsForModel(long modelId)
		{
			var objectsDto = ModelingService.GetMarketObjectsForModel(modelId);

			var models = objectsDto.Select(ModelMarketObjectRelationModel.ToModel).ToList();

            return Json(models);
        }

		[HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
		public JsonResult ChangeObjectsStatusInCalculation(string objects, long modelId)
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
        public JsonResult ExportModelObjectsToExcel(string objectIdsStr, long modelId)
        {
            var objectsJson = JObject.Parse(objectIdsStr).SelectToken("objectIds").ToString();
            var objectIds = JsonConvert.DeserializeObject<List<long>>(objectsJson);

            var fileStream = ModelingService.ExportMarketObjectsToExcel(objectIds, modelId);

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
        public JsonResult ImportModelObjectsFromExcel(IFormFile file)
        {
            if (file == null)
                throw new Exception("Не выбран файл");

            ExcelFile excelFile;
            using (var stream = file.OpenReadStream())
            {
                excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
            }
            
            ModelingService.ImportModelObjectsFromExcel(excelFile);

            return new JsonResult(new { Message = "Данные сохранены."});
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
	        var dictionaries = DictionaryService.GetDictionaries().Select(x => new
	        {
		        Text = x.Name,
		        Value = (int)x.Id
	        }).ToList();

	        dictionaries.Insert(0, new { Text = string.Empty, Value = 0 });

	        return Json(dictionaries);
        }

        #endregion


        #region Карточка модели из Тура


        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public ActionResult Model(long groupId, bool isPartial)
        {
	        var modelDto = ModelingService.GetModelEntityByGroupId(groupId);
	        var model = ModelFromTourCardModel.ToModel(modelDto);

	        if (isPartial)
	        {
		        model.IsPartial = true;
		        return PartialView("TourCard/Model", model);
	        }

	        return View("TourCard/Model", model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public ActionResult Model(ModelFromTourCardModel model)
        {
	        var omModel = ModelingService.GetModelEntityById(model.GeneralModelId);
	        if (omModel.Type_Code == KoModelType.Automatic)
		        throw new Exception($"Нельзя обновить модель с типом '{KoModelType.Automatic.GetEnumDescription()}'");

	        omModel.Name = model.Name;
	        omModel.Description = model.Description;
	        omModel.AlgoritmType_Code = model.AlgorithmTypeCode;
	        omModel.A0 = model.A0;

	        omModel.CalculationMethod_Code = model.CalculationTypeCode == KoCalculationType.Comparative
		        ? model.CalculationMethodCode
		        : KoCalculationMethod.None;

	        omModel.CalculationType_Code = model.CalculationTypeCode;
	        omModel.Formula = omModel.GetFormulaFull(true);
	        omModel.Save();

	        return Ok();
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public JsonResult GetFormula(long modelId, long algType)
        {
	        var model = ModelingService.GetModelEntityById(modelId);

            model.AlgoritmType_Code = (KoAlgoritmType)algType;
	        var formula = model.GetFormulaFull(true);

	        return Json(new { formula });
        }

        #region Факторы

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public ActionResult EditModelFactor(long? id, long generalModelId)
        {
	        FactorModel factorDto;

	        if (id.HasValue)
	        {
		        var factor = ModelFactorsService.GetFactorById(id);

		        factorDto = new FactorModel
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
		        factorDto = new FactorModel
		        {
			        Id = -1,
			        GeneralModelId = generalModelId,
			        FactorId = -1,
			        MarkerId = -1
		        };
	        }

	        return View("TourCard/EditModelFactor", factorDto);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public ActionResult EditModelFactor(FactorModel factorModel)
        {
	        var dto = factorModel.ToDto();
	        if (factorModel.Id == -1)
	        {
		        ModelFactorsService.AddFactor(dto);
	        }
	        else
	        {
		        ModelFactorsService.UpdateFactor(dto);
	        }

	        return Ok();
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public ActionResult DeleteModelFactor(long? id)
        {
	        var factor = OMModelFactor.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
	        factor.Destroy();

	        var model = OMModel.Where(x => x.Id == factor.ModelId).SelectAll().ExecuteFirstOrDefault();
	        model.Formula = model.GetFormulaFull(true);
	        model.Save();

	        return Json(new { Success = "Удаление выполненно" });
        }

        #endregion


        #region Метки

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult MarksGrid(long generalModelId, long factorId)
        {
	        ViewBag.GeneralModelId = generalModelId;
	        ViewBag.FactorId = factorId;

	        return View("TourCard/MarksGrid");
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public JsonResult GetMarkCatalog(long generalModelId, long factorId)
        {
	        var marks = ModelFactorsService.GetMarks(generalModelId, factorId);

	        var markModels = marks.Select(MarkModel.ToModel).ToList();

	        return Json(markModels);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult CreateMark(MarkModel markCatalog)
        {
	        var id = ModelFactorsService.CreateMark(markCatalog.Value, markCatalog.Metka, markCatalog.FactorId, markCatalog.GeneralModelId);
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
        public FileResult DownloadMarksCatalog(long generalModelId, long factorId)
        {
	        var fileStream = DataExporterKO.ExportMarkerListToExcel(generalModelId, factorId);

	        return File(fileStream, Consts.ExcelContentType, "Справочник меток (выгрузка)" + ".xlsx");
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_MARK_CATALOG)]
        public ActionResult UploadMarksCatalog(IFormFile file, long generalModelId, long factorId, bool isDeleteOld)
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
			        OMMarkCatalog.GetRegisterId(), generalModelId, factorId, isDeleteOld);

		        var fileName = "Справочник меток (загрузка) " + file.FileName;
		        HttpContext.Session.Set(fileName, fileStream.ToByteArray());

		        return Content(JsonConvert.SerializeObject(new { success = true, fileName = fileName }), "application/json");
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
