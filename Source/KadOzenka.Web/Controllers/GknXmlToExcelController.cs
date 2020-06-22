using System;
using Core.Main.FileStorages;
using Core.SRD;
using KadOzenka.Dal.LongProcess;
using Microsoft.AspNetCore.Mvc;

namespace KadOzenka.Web.Controllers
{
    public class GknXmlToExcelController : Controller
    {
        private readonly string FileStorage = "DataExporterByTemplate";

        [HttpGet]
        public IActionResult Configurator()
        {
            return View("/Views/DataExport/GknXmlToExcel.cshtml");
        }

        public FileResult DownloadResult(string dt, int id)
        {
            var dateTime = DateTime.Parse(dt);
            var st = FileStorageManager.GetFileStream(FileStorage, dateTime, $"{id}_ExcelConversion.zip");
            return File(st, "application/zip",
                $"{id}_{dt:ddMMyyyy}_ExcelConversion.zip");
        }

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