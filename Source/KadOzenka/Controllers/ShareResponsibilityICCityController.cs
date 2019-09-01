using CIPJS.DAL.ShareResponsibilityICCity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIPJS.Controllers
{
    public class ShareResponsibilityICCityController : Controller
    {
        private ShareResponsibilityICCityService service = new ShareResponsibilityICCityService();

        public IActionResult GetById(long? id)
        {
            ShareResponsibilityICCityDto model = service.Get(id);
            return Content(JsonConvert.SerializeObject(model), "application/json");
        }
    }
}