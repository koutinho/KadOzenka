using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core.UI.Registers.Controllers;
using KadOzenka.Web.Models.Sud;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using ObjectModel.Sud;
using System.Transactions;

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

		[HttpGet]
		public IActionResult GetObjectInfo(long id)
		{
			var obj = OMObject
				.Where(x => x.Id == id)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var drs = OMDRS
				.Where(x => x.IdObject == id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			return JsonResponse(ObjectCardModel.FromOM(obj, drs));
		}

		[HttpGet]
		public ContentResult GetObjectTypes(long objectId)
		{
			var list = new List<object>();
			list.Add(new {Id=1, Name="Тип объекта 1"});
			list.Add(new {Id=2, Name="Тип объекта 2"});
			list.Add(new {Id=3, Name="Тип объекта 3"});

			return Content(JsonConvert.SerializeObject(list), "application/json");
		}

		[HttpPost]
		public ActionResult EditObjectCard(ObjectCardModel data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(ObjectCardModel));
			}
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

			using (var ts = new TransactionScope())
			{
				var obj = OMObject
					.Where(x => x.Id == data.Id)
					.SelectAll()
					.ExecuteFirstOrDefault();
				var drs = OMDRS
					.Where(x => x.IdObject == data.Id)
					.SelectAll()
					.ExecuteFirstOrDefault();
				ObjectCardModel.ToOM(data, ref obj, ref drs);

				obj.Save();
				drs.Save();
				ts.Complete();
			}

			return Json(new { Success = "Сохранено успешно" });
		}

		[HttpGet]
		public ActionResult ReportLink(int reportLinkId)
		{
			OMOtchetLink reportLink;

			reportLink = OMOtchetLink
				.Where(x => x.Id == reportLinkId)
				.SelectAll()
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
				.SelectAll()
				.Execute().FirstOrDefault();

			if (reportLinkViewModel.Id != -1 && reportLink == null)
			{
				return NotFound();
			}

			if (reportLink == null)
			{
				reportLink = new OMOtchetLink();
			}

			ReportLinkModel.ToEntity(reportLinkViewModel, ref reportLink);
			long id;
			using (var ts = new TransactionScope())
			{
				id = reportLink.Save();
				ts.Complete();
			}

			reportLinkViewModel.Id = id;
			return Json(new { Success = "Сохранено успешно", data = reportLinkViewModel });
		}

		[HttpGet]
		public ActionResult EditReport(int reportId)
		{
			OMOtchet report = OMOtchet
				.Where(x => x.Id == reportId)
				.ExecuteFirstOrDefault();

			return View(new ReportModel());
		}

		[HttpPost]
		public ActionResult EditReport(ReportModel reportViewModel)
		{
			return EmptyResponse();
		}
	}
}
