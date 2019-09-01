using CIPJS.DAL.DamageAmount;
using Core.UI.Registers.Controllers;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CIPJS.Controllers
{
    public class DamageAmountController : BaseController
    {
        private DamageAmountService _daService;

        public DamageAmountController()
        {
            _daService = new DamageAmountService();
        }

        public ActionResult DamageAmountRead([DataSourceRequest] DataSourceRequest request, long damageId)
        {
            return Content(JsonConvert.SerializeObject(_daService.GetDamageAmountData(damageId)), "application/json");
        }

        [HttpPost]
        public ActionResult DamageAmountUpdate([Bind(Prefix = "models")]string damageAmountsJson)
        {
            var results = new List<DamageAmountDto>();
            List<DamageAmountDto> damageAmounts = JsonConvert.DeserializeObject<List<DamageAmountDto>>(damageAmountsJson);

            if (damageAmounts != null && ModelState.IsValid)
            {
                foreach (var deal in damageAmounts)
                {
                    results.Add(_daService.UpdateDamageAmount(deal));
                }
            }

            return Content(JsonConvert.SerializeObject(results), "application/json");
        }

        public ActionResult DamageInformationRead([DataSourceRequest] DataSourceRequest request, long damageId)
        {
            return Content(JsonConvert.SerializeObject(_daService.GetDamageAmounts(damageId)), "application/json");
        }
    }
}