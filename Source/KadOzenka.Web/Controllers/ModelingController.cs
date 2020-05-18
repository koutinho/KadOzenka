using System;
using System.Linq;
using KadOzenka.Dal.Modeling;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Modeling;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Core.Shared.Extensions;
using DevExpress.DataProcessing;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectModel.Core.LongProcess;

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
            //var inputRequest = new ModelingInputParameters
            //{
            //    ModelId = modelId,
            //    IsTrainingMode = true
            //};
            //process.StartProcess(new OMProcessType(), new OMQueue
            //{
            //    Parameters = inputRequest.SerializeToXml()
            //}, new CancellationToken());

            var attributes = ModelingService.GetModelAttributes(modelId);
            if (attributes == null || attributes.Count == 0)
                throw new Exception("Для модели не найдено сохраненных атрибутов");

            ModelingProcess.AddProcessToQueue(modelId, new ModelingInputParameters
            {
                ModelId = modelId,
                IsTrainingMode = true
            });

            return Json(new { Message = "Процесс обучения модели поставлен в очередь" });
        }

        [HttpPost]
        public JsonResult Predict(long modelId, PredictionType predictionType)
        {
            ////TODO код для отладки
            //var process = new ModelingProcess();
            //var inputRequest = new ModelingInputParameters
            //{
            //    ModelId = modelId,
            //    IsTrainingMode = false,
            //    PredictionType = predictionType
            //};
            //process.StartProcess(new OMProcessType(), new OMQueue
            //{
            //    Parameters = inputRequest.SerializeToXml()
            //}, new CancellationToken());

            var model = GetModel(modelId);
            if (!model.WasTrained.GetValueOrDefault())
                throw new Exception("Модель не была обучена, процесс прогнозирования не запущен.");

            ModelingProcess.AddProcessToQueue(modelId, new ModelingInputParameters
            {
                ModelId = modelId,
                IsTrainingMode = false,
                PredictionType = predictionType
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

        #endregion
    }
}
