using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Bti;
using System.Linq;

namespace CIPJS.Controllers
{
    public class BtiDistrictController : Controller
    {
        public IActionResult GetObjById(long? id)
        {
            OMBtiDistrict district = OMBtiDistrict.Where(x => x.Id == id).SelectAll().Execute().FirstOrDefault();
            return Content(JsonConvert.SerializeObject(district), "application/json");
        }
    }
}