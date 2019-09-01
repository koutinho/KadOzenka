using CIPJS.DAL.DamageAssessmentMethod;
using Core.UI.Registers.Controllers;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Insur;

namespace CIPJS.Controllers
{
    public class DamageAssessmentMethodController : BaseController
    {
        private DamageAssessmentMethodService service = new DamageAssessmentMethodService();

        public IActionResult GetById(long? id)
        {
            DamageAssessmentMethodDto model = service.Get(id);
            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        [HttpGet]
        public IActionResult DamageAssessmentMethodGridSelect(long? code, long? refId, long? refItemId)
        {
            DamageAssessmentMethodGridSelectDto model = new DamageAssessmentMethodGridSelectDto
            {
                Code = code,
                RefId = refId,
                RefItemId = refItemId
            };
            return View(model);
        }

        public ActionResult DamageAssessmentMethodTitlesRead([DataSourceRequest] DataSourceRequest request, DamageAssessmentMethodGridSelectDto model)
        {
            return Content(JsonConvert.SerializeObject(service.GetTitlestByElemConstructionCode(model)), "application/json");
        }

        [HttpGet]
        public IActionResult DamageAssessmentMethodSelectMaterialDamage(long? id)
        {
            DamageAssessmentMethodDto model = service.Get(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult DamageAssessmentMethodSelectMaterialDamageWithLoad(long? id, long? recordId)
        {
            DamageAssessmentMethodDto model = service.GetWithLoad(id, recordId);
            return View("DamageAssessmentMethodSelectMaterialDamage", model);
        }

        public ActionResult DamageAssessmentMethodsRead([DataSourceRequest] DataSourceRequest request, string title)
        {
            return Content(JsonConvert.SerializeObject(service.GetElemConstructionCode(title)), "application/json");
        }
    }
}