using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIPJS.DAL.LivingPremiseInsurCost;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIPJS.Controllers
{
    public class LivingPremiseInsurCostController : Controller
    {
        private LivingPremiseInsurCostService service = new LivingPremiseInsurCostService();

        public IActionResult GetById(long? id)
        {
            LivingPremiseInsurCostDto model = service.Get(id);
            return Content(JsonConvert.SerializeObject(model), "application/json");
        }
    }
}