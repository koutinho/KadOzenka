using CIPJS.DAL.DocBaseType;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIPJS.Controllers
{
    public class DocBaseTypeController : Controller
    {
        private DocBaseTypeService service = new DocBaseTypeService();

        public IActionResult GetById(long? id)
        {
            DocBaseTypeDto model = service.Get(id);
            return Content(JsonConvert.SerializeObject(model), "application/json");
        }
    }
}