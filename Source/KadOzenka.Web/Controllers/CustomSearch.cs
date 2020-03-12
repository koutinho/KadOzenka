using System.Linq;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Gbu;

namespace KadOzenka.Web.Controllers
{
	public class CustomSearch : Controller
	{
		// GET
		public IActionResult CadastralQuarterFilter()
		{
			return PartialView();
		}


		public JsonResult AllCadastralsQuarters([DataSourceRequest] DataSourceRequest request)
		{
			var result = OMKadastrKvartal.Where(x => x).SelectAll().Execute().AsEnumerable().ToTreeDataSourceResult(request,
				e => e.Id,
				e => e.ParentId,
				e => e
			);

			return Json(result);
		}
	}
}