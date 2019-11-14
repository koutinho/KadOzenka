using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core.UI.Registers.Controllers;
using KadOzenka.Web.Models.Sud;
using ObjectModel.Sud;
using System.Transactions;
using Newtonsoft.Json;

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

			ObjectCardModel model;
			if (id != 0 && obj != null && drs != null)
			{
				model = ObjectCardModel.FromOM(obj, drs);
			}
			else
			{
				model = ObjectCardModel.FromOM(new OMObject(), new OMDRS());
			}

			return View(model);
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
			var obj = OMObject
				.Where(x => x.Id == data.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var drs = OMDRS
				.Where(x => x.IdObject == data.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (data.Id != -1 && (obj == null || drs == null))
			{
				return NotFound();
			}

			long objId;
			if (data.Id == -1)
			{
				obj = new OMObject();
				drs = new OMDRS();
			}
			using (var ts = new TransactionScope())
			{
				ObjectCardModel.ToOM(data, ref obj, ref drs);
				objId = obj.Save();
				if (data.Id == -1)
				{
					drs.IdObject = objId;
				}
				drs.Save();
				ts.Complete();
			}

			return Json(new { Success = "Сохранено успешно", ObjectId = objId.ToString() });
		}

		[HttpGet]
		public ActionResult EditReportLink(int reportLinkId, long sudObjectId)
		{
			if (sudObjectId == 0 && reportLinkId == 0)
			{
				throw new Exception("В указанном запросе отсутствует ИД объекта. ?sudObjectId=IdObject");
			}
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

			model.SudObjectId = reportLink != null && reportLinkId != 0 ? reportLink.IdObject.GetValueOrDefault() : sudObjectId;
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
				.SelectAll()
				.ExecuteFirstOrDefault();

			var model = reportId != 0 && report != null
				? ReportModel.FromEntity(report)
				: ReportModel.FromEntity(new OMOtchet());
			return View(model);
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

			OMOtchet report = OMOtchet
				.Where(x => x.Id == reportViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (reportViewModel.Id != -1 && report == null)
			{
				return NotFound();
			}

			if (report == null)
			{
				report = new OMOtchet();
			}

			ReportModel.ToEntity(reportViewModel, ref report);

			long id;
			using (var ts = new TransactionScope())
			{
				id = report.Save();
				ts.Complete();
			}

			reportViewModel.Id = id;

			return Json(new {Success = "Сохранено успешно", data = reportViewModel});
		}

		[HttpGet]
		public ActionResult EditConclusionLink(int conclusionLinkId)
		{
			OMZakLink conclusionLink = OMZakLink
				.Where(x => x.Id == conclusionLinkId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			OMZak conclusion = null;

			if (conclusionLink != null)
			{
				conclusion = OMZak
					.Where(x => x.Id == conclusionLink.IdZak)
					.SelectAll()
					.ExecuteFirstOrDefault();
			}

			ConclusionLinkModel model = conclusionLinkId != 0 && conclusionLink != null && conclusion != null
				? ConclusionLinkModel.FromEntity(conclusionLink, conclusion) : ConclusionLinkModel.FromEntity(new OMZakLink(), new OMZak());

			return View(model);
		}

		[HttpPost]
		public ActionResult EditConclusionLink(ConclusionLinkModel conclusionLinkViewModel)
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

			OMZakLink conclusionLink = OMZakLink
				.Where(x => x.Id == conclusionLinkViewModel.Id)
				.SelectAll()
				.Execute().FirstOrDefault();

			if (conclusionLinkViewModel.Id != -1 && conclusionLink == null)
			{
				return NotFound();
			}

			if (conclusionLink == null)
			{
				conclusionLink = new OMZakLink();
			}

			ConclusionLinkModel.ToEntity(conclusionLinkViewModel, ref conclusionLink);
			long id;
			using (var ts = new TransactionScope())
			{
				id = conclusionLink.Save();
				ts.Complete();
			}

			conclusionLinkViewModel.Id = id;
			return Json(new { Success = "Сохранено успешно", data = conclusionLinkViewModel });
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

		[HttpGet]
		public JsonResult GetConclusionData(int сonclusionId)
		{
			OMZak сonclusion = OMZak
				.Where(x => x.Id == сonclusionId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			return Json(new { data = сonclusion });
		}

		[HttpGet]
		public JsonResult GetDictionary(int type)
		{
			List<OMDict> dictList = OMDict
				.Where(x => x.Type == type && x.Name != string.Empty)
				.OrderBy(x => x.Name)
				.SelectAll()
				.Execute().ToList();

			return Json(dictList);
		}

		[HttpGet]
		public ActionResult EditCourt(int courtId)
		{
			var omSud = OMSud
				.Where(x => x.Id == courtId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			var model = courtId != 0 && omSud != null
				? CourtModel.FromEntity(omSud)
				: CourtModel.FromEntity(new OMSud());
			return View(model);
		}

		[HttpPost]
		public ActionResult EditCourt(CourtModel courtViewModel)
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

			var omSud = OMSud
				.Where(x => x.Id == courtViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (courtViewModel.Id != -1 && omSud == null)
			{
				return NotFound();
			}

			if (omSud == null)
			{
				omSud = new OMSud();
			}
			CourtModel.ToEntity(courtViewModel, ref omSud);
			long id;
			using (var ts = new TransactionScope())
			{
				id = omSud.Save();
				ts.Complete();
			}
			courtViewModel.Id = id;

			return Json(new { Success = "Сохранено успешно", data = courtViewModel });
		}
	}
}
