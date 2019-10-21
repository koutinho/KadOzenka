using Core.Main.FileStorages;
using Core.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Market;
using System.Linq;

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
            }

            return View(analogItem);
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
    }
}

//10003800