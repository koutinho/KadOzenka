using Core.Main.FileStorages;
using Core.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Market;
using System.Linq;
using KadOzenka.Web.Models.MarketObject;
using System.IO.Compression;
using System.IO;
using System;

namespace KadOzenka.Web.Controllers
{
	public class MarketObjectsController : Controller
	{
		[HttpGet]
		public IActionResult ObjectCard(long id)
		{
			var analogItem = OMCoreObject
				.Where(x => x.Id == id)
				.SelectAll()
				.ExecuteFirstOrDefault();

            if (analogItem != null)
            {
                var screenList = OMScreenshots.Where(x => x.InitialId == id)
                    .SelectAll().Execute()
                    .Select(x => (x.Id, x.CreationDate)).ToList();

                if(screenList.IsNotEmpty())
                    ViewBag.ScreenShots = screenList;

	            analogItem.PriceHistory = ObjectModel.Market.OMPriceHistory
		            .Where(x => x.InitialId == id)
		            .SelectAll()
		            .Execute();
            }

            return View(CoreObjectDto.OMMap(analogItem));
		}

        /*private List<(DateTime, string)> GetScreenShots(long id)
        {
            var result = new List<(DateTime, string)>();

            var screenList =  OMScreenshots.Where(x => x.InitialId == id).SelectAll().Execute();

            foreach (OMScreenshots screen in screenList)
            {
                var filePath = FileStorageManager.GetPathForFileFolder("MarketObjectScreenShot", screen.CreationDate.Value);
                string fullFileName = Path.Combine(filePath, screen.Id.ToString());

                result.Add((screen.CreationDate.Value, fullFileName));
            }

            return result;
        }*/

        [HttpGet]
        public FileResult ShowFile(long id)
        {
            var screen = OMScreenshots.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            var fileStream = FileStorageManager.GetFileStream("MarketObjectScreenShot", screen.CreationDate.Value, id.ToString());
            return File(fileStream, screen.Type, id.ToString());
        }

		[HttpGet]
		public FileResult UnloadScreenshots(long objectId)
		{
			var analogItem = OMCoreObject
				.Where(x => x.Id == objectId)
				.Select(x => x.CadastralNumber)
				.ExecuteFirstOrDefault();

			if (analogItem == null)
			{
				throw new Exception($"Аналог с id = {objectId} не найден");
			}

			var screenList = OMScreenshots.Where(x => x.InitialId == objectId)
				.SelectAll().Execute()
				.ToList();

			using (MemoryStream zipStream = new MemoryStream())
			{
				using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
				{
					int n = 1;
					foreach (OMScreenshots screenshot in screenList)
					{
						FileStream fileStream = FileStorageManager.GetFileStream("MarketObjectScreenShot", screenshot.CreationDate.Value, screenshot.Id.ToString());

						string entryName = $"Скриншот_{n++}_{screenshot.CreationDate?.ToShortDateString().Replace(".", "")}";
						string ext = screenshot.Type.Replace("image/", ".");
						entryName += ext;

						ZipArchiveEntry zipEntry = zip.CreateEntry(entryName);
						
						using (Stream sWriter = zipEntry.Open())
						{							
							byte[] bytes = new byte[fileStream.Length];							
							int count = fileStream.Read(bytes);

							sWriter.Write(bytes);
						}
					}
				}
	
				return File(zipStream.ToArray(), "application/zip",	analogItem.CadastralNumber + ".zip");				
			}
		}

	}
}

//10003800