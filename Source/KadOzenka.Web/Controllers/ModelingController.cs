using System;
using System.Linq;
using KadOzenka.Dal.Modeling;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Modeling;
using System.Collections.Generic;
using System.Threading;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.CoreUI.Registers;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.Register;
using ObjectModel.Market;

namespace KadOzenka.Web.Controllers
{
	public class ModelingController : KoBaseController
	{
		public ModelingService ModelingService { get; set; }


        public ModelingController(ModelingService modelingService)
        {
            ModelingService = modelingService;
		}

		
		#region Model Card

		[HttpGet]
		public ActionResult ModelCard(long modelId)
		{
			var modelDto = ModelingService.GetModelById(modelId);
            var model = ModelingModel.ToModel(modelDto);
            return View(model);
		}

		[HttpGet]
		public JsonResult GetModelAttributes(long modelId)
		{
			var attributes = ModelingService.GetModelAttributes(modelId);
			return Json(attributes);
		}

        public JsonResult GetGroups()
        {
            var groups = ModelingService.GetGroups()
                .Select(x => new SelectListItem
                {
                    Value = x.GroupId.ToString(),
                    Text = x.Name
                });

            return Json(groups);
        }

        [HttpGet]
		public ActionResult AddModel()
		{
            return View();
		}

		[HttpPost]
		public JsonResult AddModel(ModelingModel modelingModel)
		{
			if (!ModelState.IsValid)
				return GenerateMessageNonValidModel();

			var modelDto = ModelingModel.FromModel(modelingModel);
			ModelingService.AddModel(modelDto);

			return Json(new {Message = "Сохранение выполнено"});
		}

		[HttpPost]
		public JsonResult UpdateModel(ModelingModel modelingModel)
		{
			if (!ModelState.IsValid)
				return GenerateMessageNonValidModel();

			var modelDto = ModelingModel.FromModel(modelingModel);
			var isModelChanged = ModelingService.UpdateModel(modelDto);

            return Json(new { IsModelWasChanged = isModelChanged, Message = "Обновление выполнено" });
		}

        [HttpPost]
        public JsonResult TrainModel(long modelId)
        {
            ////TODO код для отладки
            //var process = new ModelingProcess();
            var trainingInputParameters = new TrainingInputParameters
            {
                ModelId = modelId
            };
            //var inputRequest = new ModelingInputParameters
            //{
            //    ModelingType = ModelingType.Training,
            //    InputParametersXml = trainingInputParameters.SerializeToXml<TrainingInputParameters>()
            //};
            //process.StartProcess(new OMProcessType(), new OMQueue
            //{
            //    UserId = SRDSession.GetCurrentUserId(),
            //    Parameters = inputRequest.SerializeToXml()
            //}, new CancellationToken());

            var attributes = ModelingService.GetModelAttributes(modelId);
            if (attributes == null || attributes.Count == 0)
                throw new Exception("Для модели не найдено сохраненных атрибутов");

            ModelingProcess.AddProcessToQueue(new ModelingInputParameters
            {
                ModelingType = ModelingType.Training,
                InputParametersXml = trainingInputParameters.SerializeToXml<TrainingInputParameters>()
            });

            return Json(new { Message = "Процесс обучения модели поставлен в очередь" });
        }

        [HttpPost]
        public JsonResult Predict(long modelId, PredictionType predictionType)
        {
            ////TODO код для отладки
            //var process = new ModelingProcess();
            var predictionInputParameters = new PredictionInputParameters
            {
                ModelId = modelId,
                PredictionType = predictionType
            };
            //var inputRequest = new ModelingInputParameters
            //{
            //    ModelingType = ModelingType.Prediction,
            //    InputParametersXml = predictionInputParameters.SerializeToXml()
            //};
            //process.StartProcess(new OMProcessType(), new OMQueue
            //{
            //    UserId = SRDSession.GetCurrentUserId(),
            //    Parameters = inputRequest.SerializeToXml()
            //}, new CancellationToken());

            var model = GetModel(modelId);
            if (!model.WasTrained.GetValueOrDefault())
                throw new Exception("Модель не была обучена, процесс прогнозирования не запущен.");

            ModelingProcess.AddProcessToQueue(new ModelingInputParameters
            {
                ModelingType = ModelingType.Prediction,
                InputParametersXml = predictionInputParameters.SerializeToXml()
            });

            return Json(new { Message = "Процесс рассчета цены на основе модели поставлен в очередь" });
        }

        [HttpGet]
        public FileResult DownloadLogs(long modelId)
        {
            var fileStream = ModelingService.GetLogs(modelId);

            return File(fileStream, Helpers.Consts.ExcelContentType, $"Логи для модели {modelId}" + ".xlsx");
        }

        #endregion


        #region Models Training Results

        [HttpGet]
        public ActionResult LinearModelDetails(long modelId)
        {
            var model = GetModel(modelId);

            var trainingResult = GetDetails(model.LinearTrainingResult);
            var details = TrainingDetailsModel.ToModel(trainingResult);

            return View("ModelTrainingResult", details);
        }

        [HttpGet]
        public ActionResult ExponentialModelDetails(long modelId)
        {
            var model = GetModel(modelId);

            var trainingResult = GetDetails(model.ExponentialTrainingResult);
            var details = TrainingDetailsModel.ToModel(trainingResult);

            return View("ModelTrainingResult", details);
        }

        [HttpGet]
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
		public ActionResult ModelObjects(long modelId)
		{
            var modelDto = ModelingService.GetModelById(modelId);
            modelDto.Attributes = ModelingService.GetModelAttributes(modelId);
            var model = ModelingModel.ToModel(modelDto);
            return View(model);
		}

        [HttpGet]
		public JsonResult GetObjectsForModel(long modelId)
		{
			var objectsDto = ModelingService.GetMarketObjectsForModel(modelId);
			var models = objectsDto.Select(ModelMarketObjectRelationModel.ToModel).ToList();
            return Json(models);
        }

		[HttpPost]
		public JsonResult ChangeObjectsStatusInCalculation(string objects, long modelId)
		{
			var objectsJson = JObject.Parse(objects).SelectToken("objects").ToString();

			var allModels = JsonConvert.DeserializeObject<List<ModelMarketObjectRelationModel>>(objectsJson);
			var changedModels = allModels.Where(x => x.IsDirty).ToList();
			var objectsDtos = changedModels.Select(ModelMarketObjectRelationModel.FromModel).ToList();
			
			ModelingService.ChangeObjectsStatusInCalculation(objectsDtos);

			return Json(new { Message = "Данные успешно обновлены" });
		}

        #endregion


        #region Correlation

        [HttpGet]
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
        public JsonResult GetMarketObjectAttributes()
        {
            var marketObjectAttributes = OMAttribute.Where(x => x.RegisterId == OMCoreObject.GetRegisterId() && x.Type == 2)
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
        public JsonResult Correlation(CorrelationModel model)
        {
            if(string.IsNullOrWhiteSpace(model.QsQueryXmlStr))
                throw new Exception("Не переден запрос для формирования списка объектов");
            if (model.AttributeIds == null || model.AttributeIds.Count == 0)
                throw new Exception("Не выбраны атрибуты");

            var correlationInputParameters = new CorrelationInputParameters
            {
                AttributeIds = model.AttributeIds,
                QsQueryStr = model.QsQueryXmlStr
            };
            var inputRequest = new ModelingInputParameters
            {
                ModelingType = ModelingType.Correlation,
                InputParametersXml = correlationInputParameters.SerializeToXml<CorrelationInputParameters>()
            };

            //TODO код для отладки
            //var process = new ModelingProcess();
            //process.StartProcess(new OMProcessType(), new OMQueue
            //{
            //    UserId = SRDSession.GetCurrentUserId(),
            //    Parameters = inputRequest.SerializeToXml()
            //}, new CancellationToken());

            ModelingProcess.AddProcessToQueue(inputRequest);

            return new JsonResult(new {Message = "Процесс корреляции поставлен в очередь."});
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

        private TrainingResult GetDetails(string trainingResult)
        {
            return string.IsNullOrWhiteSpace(trainingResult)
                ? null
                : JsonConvert.DeserializeObject<TrainingResult>(trainingResult);
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
