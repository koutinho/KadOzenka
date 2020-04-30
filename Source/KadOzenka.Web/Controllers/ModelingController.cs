using System;
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
			return View();
		}

		[HttpGet]
		public ActionResult EditModel()
		{
			return View();
		}

		[HttpPost]
		public JsonResult EditModel(ModelingModel modelingModel)
		{
			if (!ModelState.IsValid)
				return GenerateMessageNonValidModel();

			if (modelingModel == null)
				throw new Exception("Не передана модель для сохранения");

			var modelDto = ModelingModel.FromModel(modelingModel);

			ModelingService.AddModel(modelDto);

			return Json(new {Message = "Сохранение выполненно"});
		}

		[HttpPost]
		public void Calculate(ModelingModel model)
		{

		}
	}
}
