using System;
using Core.Register;
using Microsoft.AspNetCore.Mvc;
using KadOzenka.Dal.ObjectsCharacteristics;
using KadOzenka.Dal.Registers;
using KadOzenka.Web.Models.ObjectsCharacteristics;
using ObjectModel.Core.Register;

namespace KadOzenka.Web.Controllers
{
    public class ObjectsCharacteristicsController : KoBaseController
    {
        private ObjectsCharacteristicsService ObjectsCharacteristicsService { get; }
        private RegisterAttributeService RegisterAttributeService { get; }

        public ObjectsCharacteristicsController()
        {
            ObjectsCharacteristicsService = new ObjectsCharacteristicsService();
            RegisterAttributeService = new RegisterAttributeService();
        }


        #region Source

        [HttpGet]
        public ActionResult AddSource()
        {
            var model = new SourceModel
            {
                RegisterId = -1
            };

            return View("~/Views/ObjectsCharacteristics/EditSource.cshtml", model);
        }

        [HttpGet]
        public ActionResult EditSource(long registerId)
        {
            var source = ObjectsCharacteristicsService.GetSource(registerId);
            if (source == null)
                throw new Exception($"Источник с Id {registerId} не найден");

            var model = new SourceModel
            {
                RegisterId = source.RegisterId,
                Name = source.RegisterDescription
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
                if (model.RegisterId == -1)
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


        #region Characteristic

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

        [HttpGet]
        public ActionResult EditCharacteristic(long characteristicId)
        {
            var attribute = GetAttribute(characteristicId);

            var model = new CharacteristicModel
            {
                Id = attribute.Id,
                RegisterId = attribute.RegisterId,
                Name = attribute.Name,
                Type = attribute.ReferenceId.HasValue
                    ? RegisterAttributeType.REFERENCE
                    : (RegisterAttributeType) attribute.Type,
                ReferenceId = attribute.ReferenceId
            };

            return View(model);
        }

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
                    model.Id = ObjectsCharacteristicsService.AddCharacteristic(CharacteristicModel.UnMap(model));
                    message = "Характеристика успешно сохранена";
                }
                else
                {
                    ObjectsCharacteristicsService.EditCharacteristic(CharacteristicModel.UnMap(model));
                    message = "Характеристика успешно обновлена";
                }
            }
            catch (Exception e)
            {
                message = $"Во время работы с характеристикой произошла ошибка: {e.Message}";
            }

            return Json(new { Message = message, data = model });
        }

        [HttpGet]
        public ActionResult DeleteCharacteristic(long characteristicId)
        {
            var attribute = GetAttribute(characteristicId);

            var model = new CharacteristicModel
            {
                Id = attribute.Id,
                Name = attribute.Name
            };

            return View(model);
        }

        [HttpPost]
        public void DeleteCharacteristic(CharacteristicModel model)
        {
            ObjectsCharacteristicsService.DeleteCharacteristic(model.Id);
        }

        
        #region Support Methods

        private OMAttribute GetAttribute(long characteristicId)
        {
            var attribute = RegisterAttributeService.GetRegisterAttribute(characteristicId);
            if (attribute == null)
                throw new Exception($"Характеристика с Id '{characteristicId}' не найдена");

            return attribute;
        }

        #endregion

        #endregion
    }
}
