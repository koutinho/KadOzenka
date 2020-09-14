using System;
using System.Linq;
using KadOzenka.Dal.Modeling;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Modeling;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Core.ObjectModel.CustomAttribute;
using Core.Register;
using Core.Register.Enums;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.CoreUI.Registers;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport;
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
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
	public class ModelingController : KoBaseController
	{
		public ModelingService ModelingService { get; set; }
        public TourFactorService TourFactorService { get; set; }
        public RegisterAttributeService RegisterAttributeService { get; set; }


        public ModelingController(ModelingService modelingService, TourFactorService tourFactorService,
            RegisterAttributeService registerAttributeService)
        {
            ModelingService = modelingService;
            TourFactorService = tourFactorService;
            RegisterAttributeService = registerAttributeService;
        }


        #region Model Card

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
            var koAttributes = TourFactorService.GetTourAttributes(tourId, (ObjectType)objectType);

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

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
		public JsonResult GetModelAttributes(long modelId)
		{
			var attributes = ModelingService.GetModelAttributes(modelId);
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
            var attributes = ModelingService.GetModelAttributes(modelId);
            if (attributes == null || attributes.Count == 0)
                throw new Exception("Для модели не найдено сохраненных атрибутов");

            var inputParameters = new GeneralModelingInputParameters
            {
                ModelId = modelId,
                ModelType = modelType
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
                ModelType = modelType
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

        #endregion


        #region Models Training Results

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult LinearModelDetails(long modelId)
        {
            var model = GetModel(modelId);

            var trainingResult = GetDetails(model.LinearTrainingResult);
            var details = TrainingDetailsModel.ToModel(trainingResult);

            return View("ModelTrainingResult", details);
        }

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult ExponentialModelDetails(long modelId)
        {
            var model = GetModel(modelId);

            var trainingResult = GetDetails(model.ExponentialTrainingResult);
            var details = TrainingDetailsModel.ToModel(trainingResult);

            return View("ModelTrainingResult", details);
        }

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult MultiplicativeModelDetails(long modelId)
        {
            var model = GetModel(modelId);

            var trainingResult = GetDetails(model.MultiplicativeTrainingResult);
            var details = TrainingDetailsModel.ToModel(trainingResult);

            return View("ModelTrainingResult", details);
        }

        #endregion


        #region Market Objects For Model

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
		public ActionResult ModelObjects(long modelId)
		{
            var modelDto = ModelingService.GetModelById(modelId);
            modelDto.Attributes = ModelingService.GetModelAttributes(modelId);

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

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS_MODEL_OBJECTS)]
        public ActionResult ExportModelObjectsToExcel(long modelId)
        {
            var fileStream = ModelingService.ExportMarketObjectsToExcel(modelId);

            StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out var fileExtenstion, out var contentType);

            return File(fileStream, contentType, $"Объекты модели {modelId}, {DateTime.Now}." + fileExtenstion);
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


        #region Correlation

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


        #region Support Methods

        private OMModelingModel GetModel(long modelId)
        {
            var model = OMModelingModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
            if (model == null)
                throw new Exception($"Не найдена модель с Id='{modelId}'");

            return model;
        }

        private TrainingResponse GetDetails(string trainingResult)
        {
            return string.IsNullOrWhiteSpace(trainingResult)
                ? null
                : JsonConvert.DeserializeObject<TrainingResponse>(trainingResult);
        }

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
