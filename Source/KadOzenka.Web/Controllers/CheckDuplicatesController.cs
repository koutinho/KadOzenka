using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using KadOzenka.Dal.DuplicateCleaner;

using Newtonsoft.Json;

namespace KadOzenka.Web.Controllers
{
    public class CheckDuplicatesController : Controller
    {

        private class Data
        {
            public int Area { get; set; }
            public int Price { get; set; }
            public bool InProgress { get; set; }
        }

        public ActionResult ProgressBar() => View();

        public JsonResult StartDuplicatesChecking()
        {
            Data data = JsonConvert.DeserializeObject<Data>(new StreamReader(HttpContext.Request.Body).ReadToEnd());
            new Thread(() => { new Duplicates().Detect(area: ((double)data.Area)/100, price: ((double)data.Price) / 100); }).Start();
            Duplicates.inProgress = data.InProgress;
            return Json(new { type = "started" });
        }

        public JsonResult GetProgress() => Json(new { currentProgress = Duplicates.currentPersent, inProgress = Duplicates.inProgress });

    }
}