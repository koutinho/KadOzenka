using System;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.GbuObject;
using Microsoft.AspNetCore.Mvc;
using KadOzenka.Dal.ObjectsCharacteristics;
using KadOzenka.Dal.Registers;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.ObjectsCharacteristics;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;
using ObjectModel.SRD;

namespace KadOzenka.Web.Controllers
{
    public class ObjectsCharacteristicsController : KoBaseController
    {
        private IObjectsCharacteristicsService ObjectsCharacteristicsService { get; }
        private IObjectsCharacteristicsSourceService ObjectsCharacteristicsSourceService { get; }
        private IRegisterAttributeService RegisterAttributeService { get; }

        public ObjectsCharacteristicsController(IObjectsCharacteristicsService objectsCharacteristicsService, IObjectsCharacteristicsSourceService objectsCharacteristicsSourceService, 
	        IRegisterAttributeService registerAttributeService, IRegisterCacheWrapper registerCacheWrapper,
	        IGbuObjectService gbuObjectService)
	        : base(gbuObjectService, registerCacheWrapper)
        {
            ObjectsCharacteristicsService = objectsCharacteristicsService;
            ObjectsCharacteristicsSourceService = objectsCharacteristicsSourceService;
            RegisterAttributeService = registerAttributeService;
        }


        #region Source

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJ_PARAM_ADD_SOURCE)]
        public ActionResult AddSource()
        {
            var model = new SourceModel
            {
                RegisterId = -1
            };

            return View("~/Views/ObjectsCharacteristics/EditSource.cshtml", model);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJ_PARAM_EDIT_SOURCE)]
        public ActionResult EditSource(long registerId)
        {
            var source = ObjectsCharacteristicsSourceService.GetSource(registerId);
            
            var model = SourceModel.Map(source);

            return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJ_PARAM_EDIT_SOURCE)]
        public JsonResult EditSource(SourceModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            string message;
            if (model.RegisterId == -1)
            {
                ObjectsCharacteristicsSourceService.AddSource(SourceModel.UnMap(model));
                message = "Источник успешно сохранен";
            }
            else
            {
                ObjectsCharacteristicsSourceService.EditSource(SourceModel.UnMap(model));
                message = "Источник успешно обновлен";
            }

            return Json(new { Message = message });
        }

        #endregion


        #region Characteristic

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJ_PARAM)]
        public ActionResult AddCharacteristic(long registerId)
        {
            var model = new CharacteristicModel
            {
                Id = -1,
                RegisterId = registerId,
                DisableAttributeEditing = ObjectsCharacteristicsService.GetObjectRegisterEditSettings(registerId)
            };

            return View("~/Views/ObjectsCharacteristics/EditCharacteristic.cshtml", model);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJ_PARAM)]
        public ActionResult EditCharacteristic(long attributeId)
        {
            var attribute = GetAttribute(attributeId);
            var setting = GetGbuAttributeSettings(attributeId);
            var model = CharacteristicModel.Map(attribute, setting);

            return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJ_PARAM)]
        public JsonResult EditCharacteristic(CharacteristicModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            string message;
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

            return Json(new { Message = message, data = model });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJ_PARAM)]
        public ActionResult DeleteCharacteristic(long attributeId)
        {
            var attribute = GetAttribute(attributeId);
            var setting = GetGbuAttributeSettings(attributeId);
            var model = CharacteristicModel.Map(attribute, setting);

            return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJ_PARAM)]
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

        private OMAttributeSettings GetGbuAttributeSettings(long attributeId)
        {
            var setting = ObjectsCharacteristicsService.GetRegisterAttributeSettings(attributeId);
            return setting;
        }

        #endregion

        #endregion
    }
}
