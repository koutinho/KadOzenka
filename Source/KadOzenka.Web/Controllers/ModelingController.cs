using KadOzenka.Dal.Modeling;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;

namespace KadOzenka.Web.Controllers
{
	public class ModelingController : KoBaseController
	{
		public ModelingService ModelingService { get; set; }

		public ModelingController(ModelingService modelingService)
		{
			ModelingService = modelingService;
		}

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

			//TODO расчет модели

			return Json(new { Message = "Обновление выполнено" });
		}
	}
}
