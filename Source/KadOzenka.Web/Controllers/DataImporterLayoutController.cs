using System.Linq;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Common;

namespace KadOzenka.Web.Controllers
{
	public class DataImporterLayoutController : BaseController
	{
		[HttpGet]
		public ActionResult MainData(long importId)
		{
			var import = OMImportFromTemplates
				.Where(x => x.Id == importId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			return View(importId);
		}

		[HttpGet]
		public ActionResult Download(long importId)
		{
			return EmptyResponse();
		}

		[HttpGet]
		public ActionResult ImportReStart(long importId)
		{
			return EmptyResponse();
		}
	}
}
