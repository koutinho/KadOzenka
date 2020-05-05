using System.Linq;
using System.Threading;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.Modeling;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Core.LongProcess;
using ObjectModel.Modeling;

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
		public JsonResult Calculate(ModelingModel modelingModel)
		{
			if (!ModelState.IsValid)
				return GenerateMessageNonValidModel();

			var modelDto = ModelingModel.FromModel(modelingModel);
			ModelingService.UpdateModel(modelDto);

			//TODO удалить код для отладки
			var process = new ModelingProcess();
			process.StartProcess(new OMProcessType(), new OMQueue{ObjectId = modelDto.ModelId}, new CancellationToken());

			return Json(new { Message = "Обновление выполнено. Процедура формирования модели поставлена в очередь." });
		}

		#endregion


		#region Model Objects

		[HttpGet]
		public ActionResult ObjectsFromModels()
		{
			return View();
		}

		[HttpGet]
		public JsonResult GetModels()
		{
			var models = OMModelingModel.Where(x => true).SelectAll().Execute()
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Name.ToString()
				});

			return Json(models);
		}

		[HttpGet]
		public JsonResult GetModelById(long modelId)
		{
			var modelDto = ModelingService.GetModelById(modelId);
			var model = ModelingModel.ToModel(modelDto);
			return Json(model);
		}

		[HttpGet]
		public JsonResult GetObjectsForModel(long modelId)
		{
			var models = OMModelToMarketObjects.Where(x => x.ModelId == modelId).SelectAll().Execute();

			return new JsonResult(models);
		}

		#endregion
	}
}
