using System;
using Microsoft.AspNetCore.Mvc;
using Core.UI.Registers.Controllers;
using KadOzenka.Dal.ObjectsCharacteristics;
using KadOzenka.Web.Models.ObjectsCharacteristics;

namespace KadOzenka.Web.Controllers
{
    //TODO
	public class ObjectsCharacteristicsController : BaseController
	{
        private ObjectsCharacteristicsService ObjectsCharacteristicsService { get; set; }

        public ObjectsCharacteristicsController()
        {
            ObjectsCharacteristicsService = new ObjectsCharacteristicsService();
        }


        [HttpGet]
        public ActionResult AddSource()
        {
            var model = new ObjectsCharacteristicModel
            {
                Id = -1
            };

            return View("~/Views/ObjectsCharacteristics/EditSource.cshtml", model);
        }

        [HttpGet]
        public ActionResult EditSource(long characteristicsId)
        {
            var characteristics = ObjectsCharacteristicsService.GetCharacteristics(characteristicsId);
            if (characteristics == null)
                throw new Exception($"Характеристика с Id {characteristicsId} не найдена");

            var model = new ObjectsCharacteristicModel
            {
                Id = characteristics.Id,
                Name = characteristics.RegisterDescription
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult EditSource(ObjectsCharacteristicModel model)
        {
            string message;
            try
            {
                if (model.Id == -1)
                {
                    ObjectsCharacteristicsService.AddRegister(ObjectsCharacteristicModel.UnMap(model));
                    message = "Источник успешно сохранен";
                }
                else
                {
                    if(string.IsNullOrWhiteSpace(model.Name))
                        throw new ArgumentException("Имя источника не может быть пустым");

                    ObjectsCharacteristicsService.EditRegister(ObjectsCharacteristicModel.UnMap(model));
                    message = "Источник успешно обновлен";
                }
            }
            catch (Exception e)
            {
                message = $"Во время работы с источником произошла ошибка: {e.Message}";
            }

            return Json(new { Message = message });
        }
    }
}
