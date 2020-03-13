using System;
using System.Linq;
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


        #region Source

        [HttpGet]
        public ActionResult AddSource()
        {
            var model = new SourceModel
            {
                Id = -1
            };

            return View("~/Views/ObjectsCharacteristics/EditSource.cshtml", model);
        }

        [HttpGet]
        public ActionResult EditSource(long characteristicsId)
        {
            var characteristics = ObjectsCharacteristicsService.GetSource(characteristicsId);
            if (characteristics == null)
                throw new Exception($"Характеристика с Id {characteristicsId} не найдена");

            var model = new SourceModel
            {
                Id = characteristics.Id,
                Name = characteristics.RegisterDescription
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult EditSource(SourceModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            string message;
            try
            {
                if (model.Id == -1)
                {
                    ObjectsCharacteristicsService.AddSource(SourceModel.UnMap(model));
                    message = "Источник успешно сохранен";
                }
                else
                {
                    ObjectsCharacteristicsService.EditSource(SourceModel.UnMap(model));
                    message = "Источник успешно обновлен";
                }
            }
            catch (Exception e)
            {
                message = $"Во время работы с источником произошла ошибка: {e.Message}";
            }

            return Json(new { Message = message });
        }

        #endregion


        #region Characteristics

        [HttpGet]
        public ActionResult Characteristics(long registerId)
        {
            var model = new SourceModel
            {
                RegisterId = registerId
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult AddCharacteristic(long sourceId)
        {
            var model = new CharacteristicModel
            {
                Id = -1,
                RegisterId = sourceId
            };

            return View("~/Views/ObjectsCharacteristics/EditCharacteristic.cshtml", model);
        }

        //TODO
        [HttpPost]
        public JsonResult EditCharacteristic(CharacteristicModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            string message;
            try
            {
                if (model.Id == -1)
                {
                    //model.Id = ObjectsCharacteristicsService.AddCharacteristic(model.Name, model.RegisterId, model.Type, model.ReferenceId);
                    message = "Характеристика успешно сохранена";
                }
                else
                {
                    //TODO
                    //if (string.IsNullOrWhiteSpace(model.Name))
                    //    throw new ArgumentException("Имя источника не может быть пустым");

                    //ObjectsCharacteristicsService.EditSource(SourceModel.UnMap(model));
                    message = "Характеристика успешно обновлена";
                }
            }
            catch (Exception e)
            {
                message = $"Во время работы с характеристикой произошла ошибка: {e.Message}";
            }

            return Json(new { Message = message, data = model });
        }

        #endregion


        #region Support Methods

        private JsonResult GenerateMessageNonValidModel()
        {
            return Json(new
            {
                Errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new
                {
                    Control = x.Key,
                    Message = string.Join("\n", x.Value.Errors.Select(e =>
                    {
                        if (e.ErrorMessage == "The value '' is invalid.")
                        {
                            return $"{e.ErrorMessage} Поле {x.Key}";
                        }

                        return e.ErrorMessage;
                    }))
                })
            });
        }

        #endregion
    }
}
