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
using ObjectModel.Market;

namespace KadOzenka.Web.Controllers
{
    public class CheckDuplicatesController : Controller
    {

        private class Data
        {
            public double Area { get; set; }
            public double Price { get; set; }
            public int Market { get; set; }
            public bool InProgress { get; set; }
        }

        public ActionResult ProgressBar() => View();

        public JsonResult StartDuplicatesChecking()
        {
            Data data = JsonConvert.DeserializeObject<Data>(new StreamReader(HttpContext.Request.Body).ReadToEnd());
            new Thread(() => { new Duplicates(data.Area, data.Price, data.Market).Detect(logData:false); }).Start();
            Duplicates.InProgress = data.InProgress;
            return Json(new { type = "started" });
        }

    }
}