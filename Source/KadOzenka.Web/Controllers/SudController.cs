using Microsoft.AspNetCore.Mvc;
using Core.UI.Registers.Controllers;
using KadOzenka.Web.Models.Sud;
using ObjectModel.Sud;

namespace KadOzenka.Web.Controllers
{
	public class SudController : BaseController
	{
		[HttpGet]
		public IActionResult ObjectCard(long id)
		{
			var obj = OMObject
				.Where(x => x.Id == id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			return View(ObjectCardModel.FromOMObject(obj));
		}

		[HttpPost]
		public ActionResult EditObjectCard(ObjectCardModel data)
		{
			return EmptyResponse();
		}
	}
}
