using Microsoft.AspNetCore.Mvc;
using ObjectModel.Market;
using System.Diagnostics;

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
            return View(analogItem);
		}
	}
}

//10003800