using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Insur;
using System.Linq;

namespace CIPJS.Controllers
{
    public class AddressController : Controller
    {
        public IActionResult GetObjById(long? id)
        {
            OMAddress address = OMAddress.Where(x => x.EmpId == id).SelectAll().Execute().FirstOrDefault();
            return Content(JsonConvert.SerializeObject(address), "application/json");
        }
    }
}