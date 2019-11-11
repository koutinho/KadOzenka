using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core.UI.Registers.Controllers;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using KadOzenka.Web.Models.Sud;
using Kendo.Mvc.Extensions;
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
			var drs = OMDRS
				.Where(x => x.IdObject == id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			return View(ObjectCardModel.FromOM(obj, drs));
		}

		[HttpPost]
		public ActionResult EditObjectCard(ObjectCardModel data)
		{
			return EmptyResponse();
		}

		[HttpGet]
		public ActionResult ReportLink(int reportLinkId)
		{
			OMOtchetLink reportLink;

			reportLink = OMOtchetLink
				.Where(x => x.Id == reportLinkId)
				.Execute().FirstOrDefault();

			ReportLinkModel model = reportLinkId != 0 && reportLink != null ? ReportLinkModel.FromEntity(reportLink) : ReportLinkModel.FromEntity(new OMOtchetLink());


			return View(model);
		}

		[HttpPost]
		public ActionResult ReportLink(ReportLinkModel reportLinkViewModel)
		{
			if (!ModelState.IsValid)
			{
				return Json(new
				{
					Errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new
					{
						Control = x.Key,
						Message = string.Join("\n", x.Value.Errors.Select(e =>
						{
							if (e.ErrorMessage == "The value '' is invalid.")
							{
								return $"{e.ErrorMessage} Поле {x.Key}";
							}

							return e.ErrorMessage;
						}))
					})
			});
			}

			OMOtchetLink reportLink = OMOtchetLink
				.Where(x => x.Id == reportLinkViewModel.Id)
				.Execute().FirstOrDefault(); ;

			if (reportLinkViewModel.Id != -1 && reportLink == null)
			{
				return NotFound();
			}

			reportLinkViewModel.ToEntity(reportLink);

			// вызов сервиса
			// передать reportLink


			return Json(new { Success = "Сохранено успешно" });
		}
	}
}
