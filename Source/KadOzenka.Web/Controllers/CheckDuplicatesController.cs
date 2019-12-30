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
            public bool InProgress { get; set; }
        }

        public ActionResult ProgressBar() => View();

        public JsonResult StartDuplicatesChecking()
        {
            Data data = JsonConvert.DeserializeObject<Data>(new StreamReader(HttpContext.Request.Body).ReadToEnd());
            new Thread(() => { new Duplicates(data.Area, data.Price).Detect(); }).Start();
            Duplicates.InProgress = data.InProgress;
            return Json(new { type = "started" });
        }

        public JsonResult GetProgress()
        {
            OMDuplicatesHistory history = OMDuplicatesHistory.Where(x => true).SelectAll().OrderByDescending(x => x.CheckDate).ExecuteFirstOrDefault();
            return Json(new
            {
                checkDate = history == null ? null : history.CheckDate?.ToString("yyyy.MM.dd HH:mm:ss"),
                marketSegment = history == null ? null : history.MarketSegment,
                areaDelta = history == null ? null : $"{(int)(history.AreaDelta * 100)}&nbsp;%",
                priceDelta = history == null ? null : $"{(int)(history.PriceDelta * 100)}&nbsp;%",
                commonCount = history == null ? null : history.CommonCount,
                inProgressCount = history == null ? null : history.InProgressCount,
                duplicateCount = history == null ? null : history.DuplicateObjects,
                currentProgress = Duplicates.CurrentProgress,
                inProgress = Duplicates.InProgress
            });
        }

    }
}