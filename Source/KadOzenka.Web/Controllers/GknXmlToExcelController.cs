using System;
using Core.Main.FileStorages;
using Core.SRD;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
    public class GknXmlToExcelController : Controller
    {
        private readonly string FileStorage = "DataExporterByTemplate";

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_OBJECTS)]
        public IActionResult Configurator()
        {
            return View("/Views/DataExport/GknXmlToExcel.cshtml");
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_OBJECTS)]
        public FileResult DownloadResult(string dt, int id)
        {
            var dateTime = DateTime.Parse(dt);
            var st = FileStorageManager.GetFileStream(FileStorage, dateTime, $"{id}_ExcelConversion.zip");
            return File(st, "application/zip",
                $"{id}_{dt:ddMMyyyy}_ExcelConversion.zip");
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_OBJECTS)]
        public IActionResult Convert(long id, int[] config)
        {
            try
            {
                GknXmlToExcelLongProcess.AddProcessToQueue(
                    new GknXmlToExcelLongProcess.GknXmlConvertParams
                    {
                        KOTaskId = id,
                        config = config,
                        UserId = SRDSession.GetCurrentUserId()
                    });
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Возникла ошибка при постановке задачи в очередь");
            }
        }
    }
}