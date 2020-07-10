using Microsoft.AspNetCore.Mvc;
using KadOzenka.Web.Models.BackgroundReportingForms;

namespace KadOzenka.Web.Controllers
{
    public class BackgroundReportingFormsController : KoBaseController
	{
        [HttpGet]
        public ActionResult ScheduleBackgroundProcess()
        {
            var model = new BackgroundReportLongProcessModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult ScheduleBackgroundProcess(BackgroundReportLongProcessModel model)
        {
            if(!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            return new JsonResult(Ok());
        }
    }
}
