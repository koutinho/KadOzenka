using KadOzenka.Web.Models.BackgroundScheduler;
using Microsoft.AspNetCore.Mvc;

namespace KadOzenka.Web.Controllers
{
    public class BackgroundSchedulerController : KoBaseController
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
