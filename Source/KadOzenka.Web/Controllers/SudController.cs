﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core.UI.Registers.Controllers;
using KadOzenka.Web.Models.Sud;
using Kendo.Mvc.Extensions;
using ObjectModel.Sud;
using System.Transactions;
using Microsoft.AspNetCore.Http;

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
		public ActionResult EditReportLink(int reportLinkId)
		{
			OMOtchetLink reportLink = OMOtchetLink
				.Where(x => x.Id == reportLinkId)
				.SelectAll()
				.Execute().FirstOrDefault();

			OMOtchet report = null;

			if (reportLink != null)
			{
				report = OMOtchet
					.Where(x => x.Id == reportLink.IdOtchet)
					.SelectAll()
					.ExecuteFirstOrDefault();
			}

			ReportLinkModel model = reportLinkId != 0 && reportLink != null && report !=null
				? ReportLinkModel.FromEntity(reportLink, report) : ReportLinkModel.FromEntity(new OMOtchetLink(), new OMOtchet());


			return View(model);
		}

		[HttpPost]
		public ActionResult EditReportLink(ReportLinkModel reportLinkViewModel)
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

			return EmptyResponse();
		}

		[HttpGet]
		public JsonResult GetReportData(int reportId)
		{
			OMOtchet report = OMOtchet
				.Where(x => x.Id == reportId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			return Json(new {data = report});
		}
	}
}
