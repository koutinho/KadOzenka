using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Bti;
using System.Linq;

namespace CIPJS.Controllers
{
    public class BtiOkrugController : Controller
    {
        public IActionResult GetObjById(long? id)
        {
            OMBtiOkrug okrug = OMBtiOkrug.Where(x => x.Id == id).SelectAll().Execute().FirstOrDefault();
            return Content(JsonConvert.SerializeObject(okrug), "application/json");
        }
    }
}