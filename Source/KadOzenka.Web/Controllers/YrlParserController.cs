using System;
using System.Threading;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.Controllers;
using KadOzenka.Dal.ChunkUpload;
using KadOzenka.Dal.LongProcess;
using Microsoft.AspNetCore.Mvc;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.YrlParser;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
    public class YrlParserController : BaseController
    {
        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
        public IActionResult YrlParserCreateImportTask()
        {
            return View(new YrlFeedParserTaskViewModel());
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
        public IActionResult YrlParserCreateImportTask(YrlFeedParserTaskViewModel model, Guid uuid)
        {
            // new YrlParserLongProcess().StartProcess(new OMProcessType(), new OMQueue
            // {
            //     Status_Code = Status.Added,
            //     UserId = SRDSession.GetCurrentUserId(),
            //     Parameters = uuid.SerializeToXml()
            // }, new CancellationToken());

            YrlParserLongProcess.AddProcessToQueue(uuid);
            return Json( new {type = "Success", Msg = "Файлы добавлены для импорта"});
        }
    }
}