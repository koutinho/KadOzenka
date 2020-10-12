using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core.UI.Registers.Controllers;
using KadOzenka.Web.Models.Sud;
using ObjectModel.Sud;
using System.Transactions;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register.DAL;
using Core.Register.Enums;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Models.CoreUi;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.SudLongProcesses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Common;
using ObjectModel.Directory.Sud;

namespace KadOzenka.Web.Controllers
{
	public class SudController : BaseController
	{
		#region ObjectCard

		[HttpGet]
		public IActionResult ObjectCard(long id)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_EDIT, true, false, true);
			var obj = OMObject
				.Where(x => x.Id == id)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var drs = OMDRS
				.Where(x => x.IdObject == id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (obj == null)
			{
				obj = new OMObject();
			}

			if (drs == null)
			{
				drs = new OMDRS();
			}
			bool isEditPermission = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_EDIT);

			var model = ObjectCardModel.FromOM(obj, drs);
			model.IsEditPermission = isEditPermission;
			model.IsApprovePermission =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_RESH_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_OTCHET_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_RESH_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OTCHET_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_ZAK_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_ZAK_APPROVE);
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
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_EDIT, true, false, true);
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

			if (data.Id != -1 && obj == null)
			{
				return NotFound();
			}

			long objId;
			if (data.Id == -1)
			{
				obj = new OMObject();
				drs = new OMDRS();
			}

			if (drs == null)
			{
				drs = new OMDRS();
			}

			var existObject = OMObject.Where(x => x.Date == data.Date && x.Kn == data.Kn).ExecuteFirstOrDefault();
			if (data.Id == -1 && existObject != null || data.Id != -1 && existObject != null && data.Id != existObject?.Id)
			{
				return NotFound($"Объект {data.Kn} на {data.Date?.ToShortDateString()} уже внесен");
			}

			using (var ts = new TransactionScope())
			{
				ObjectCardModel.ToOM(data, ref obj, ref drs);

				objId = obj.SaveAndCheckParam();

				if (data.Id == -1 || drs.IdObject == -1)
				{
					drs.IdObject = objId;
				}
				drs.Save();
				ts.Complete();
			}

			return Json(new { Success = "Сохранено успешно", ObjectId = objId.ToString() });
		}

		[HttpGet]
		public ActionResult RemoveObject(int objectId)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_REMOVE, true, false, true);
			if (objectId == 0)
			{
				return NotFound("Ид объекта не может быть 0");
			}

			ViewBag.RegistryId = OMObject.GetRegisterId();
			return View(objectId);
		}

		[HttpPost]
		public JsonResult RemoveObject([FromForm]int idObject, [FromForm]string reason)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_REMOVE, true, false, true);
			if (idObject == 0)
			{
				return Json(new { error = "Ид объекта не может быть равен 0. Обратитесь к Администратору" });
			}

			if (string.IsNullOrEmpty(reason) &&
				!TdAttachmentsDAL.GetAllAttachments(idObject, OMObject.GetRegisterId()).Any())
			{
				return Json(new { error = "Укажите причину или загрузите файлы" });
			}

			var obj = OMObject.Where(x => x.Id == idObject).SelectAll().ExecuteFirstOrDefault();

			if (obj == null)
			{
				return Json(new { error = "Объект не найден" });
			}

			try
			{
				obj.ReasonForRemove = reason;
				obj.IsRemoved = true;
				obj.SaveAndCheckParam();
			}
			catch (Exception e)
			{
				throw new Exception($"Ошибка при сохранении. Сообщение: {e.Message}");
			}

			return Json(new { success = "Удаление выполнено успешно." });
		}
		#endregion

		#region Report

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

			ReportLinkModel model = reportLinkId != 0 && reportLink != null && report != null
				? ReportLinkModel.FromEntity(reportLink, report) : ReportLinkModel.FromEntity(new OMOtchetLink(), new OMOtchet());

			model.SudObjectId = reportLink != null && reportLinkId != 0 ? reportLink.IdObject.GetValueOrDefault() : sudObjectId;

			model.SquareObject = OMObject.Where(x => x.Id == model.SudObjectId).Select(x => x.Square)
				.ExecuteFirstOrDefault().Square.GetValueOrDefault();
			model.IsEditReport = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OTCHET_EDIT);
			model.IsEditReportLink = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_EDIT);

			return View(model);
		}

		[HttpPost]
		public ActionResult EditReportLink(ReportLinkModel reportLinkViewModel)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_EDIT, true, false, true);
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

			var links = OMOtchetLink.Where(x => x.IdObject == reportLinkViewModel.SudObjectId &&
											 x.IdOtchet == reportLinkViewModel.IdReport && x.Id != reportLinkViewModel.Id).Execute();
			if (links.Any())
			{
				return Json(new
				{
					Errors = links.Select(x => new
					{
						Control = 0,
						Message = "Объект с таким отчетом уже существует. Выберите другое отчет."
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
				id = reportLink.SaveAndCheckParam();
				ts.Complete();
			}

			reportLinkViewModel.Id = id;
			return Json(new { Success = "Сохранено успешно", data = reportLinkViewModel });
		}

		[HttpGet]
		public ActionResult EditReport(int reportId)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OTCHET, true, false, true);

			OMOtchet report = OMOtchet
				.Where(x => x.Id == reportId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			var model = reportId != 0 && report != null
				? ReportModel.FromEntity(report)
				: ReportModel.FromEntity(new OMOtchet());

			model.IsEditReport = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OTCHET_EDIT);
			return View(model);
		}

		[HttpPost]
		public ActionResult EditReport(ReportModel reportViewModel)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OTCHET_EDIT, true, false, true);
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
				id = report.SaveAndCheckParam();
				ts.Complete();
			}

			reportViewModel.Id = id;

			return Json(new { Success = "Сохранено успешно", data = reportViewModel });
		}

		#endregion

		#region Conclusion

		[HttpGet]
		public ActionResult EditConclusionLink(int conclusionLinkId, long sudObjectId)
		{
			if (sudObjectId == 0 && conclusionLinkId == 0)
			{
				throw new Exception("В указанном запросе отсутствует ИД объекта. ?sudObjectId=IdObject");
			}
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

			model.SudObjectId = conclusionLink != null && conclusionLinkId != 0 ? conclusionLink.IdObject.GetValueOrDefault() : sudObjectId;

			model.SquareObject = OMObject.Where(x => x.Id == model.SudObjectId).Select(x => x.Square)
				.ExecuteFirstOrDefault().Square.GetValueOrDefault();

			model.IsEditConclusion =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_ZAK_EDIT);

			model.IsEditConclusionLink = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_EDIT);

			return View(model);
		}

		[HttpPost]
		public ActionResult EditConclusionLink(ConclusionLinkModel conclusionLinkViewModel)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_ZAK_EDIT, true, false, true);
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

			var links = OMZakLink.Where(x => x.IdObject == conclusionLinkViewModel.SudObjectId &&
											 x.IdZak == conclusionLinkViewModel.IdConclusion && x.Id != conclusionLinkViewModel.Id).Execute();
			if (links.Any())
			{
				return Json(new
				{
					Errors = links.Select(x => new
					{
						Control = 0,
						Message = "Объект с таким заключением уже существует. Выберите другое заключение."
					})
				});
			}

			var conclusionLink = OMZakLink
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
				id = conclusionLink.SaveAndCheckParam();
				ts.Complete();
			}

			conclusionLinkViewModel.Id = id;
			return Json(new { Success = "Сохранено успешно", data = conclusionLinkViewModel });
		}


		[HttpGet]
		public ActionResult EditConclusion(int conclusionId)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_ZAK, true, false, true);

			OMZak conclusion = OMZak
				.Where(x => x.Id == conclusionId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			var model = conclusionId != 0 && conclusion != null
				? ConclusionModel.FromEntity(conclusion)
				: ConclusionModel.FromEntity(new OMZak());

			model.IsEditConclusion =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_ZAK_EDIT);
			return View(model);
		}

		[HttpPost]
		public ActionResult EditConclusion(ConclusionModel conclusionViewModel)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_ZAK_EDIT, true, false, true);
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

			OMZak conclusion = OMZak
				.Where(x => x.Id == conclusionViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (conclusionViewModel.Id != -1 && conclusion == null)
			{
				return NotFound();
			}

			if (conclusion == null)
			{
				conclusion = new OMZak();
			}

			ConclusionModel.ToEntity(conclusionViewModel, ref conclusion);

			long id;
			using (var ts = new TransactionScope())
			{
				id = conclusion.SaveAndCheckParam();
				ts.Complete();
			}

			conclusionViewModel.Id = id;

			return Json(new { Success = "Сохранено успешно", data = conclusionViewModel });
		}
		#endregion

		#region OMData
		[HttpGet]
		public JsonResult GetReportData(int reportId)
		{
			var report = OMOtchet
				.Where(x => x.Id == reportId)
				.SelectAll()
				.Execute().Select(x => new
				{
					x.Id,
					Value = $"{x.Number} от {x.Date.GetString()}"
				}).FirstOrDefault();

			return Json(new { data = report });
		}


		[HttpGet]
		public JsonResult GetCourtData(int sudId)
		{
			var court = OMSud
				.Where(x => x.Id == sudId)
				.SelectAll()
				.Execute().Select(x => new
				{
					x.Id,
					Value = x.Date != null ? $"{x.Number} от {x.Date.GetString()}" : x.Number

				}).FirstOrDefault();

			return Json(new { data = court });
		}

		[HttpGet]
		public JsonResult GetConclusionData(int сonclusionId)
		{
			var сonclusion = OMZak
				.Where(x => x.Id == сonclusionId)
				.SelectAll()
				.Execute().Select(x => new
				{
					x.Id,
					Value = $"{x.Number} от {x.Date.GetString()}"
				}).FirstOrDefault();

			return Json(new { data = сonclusion });
		}

		[HttpGet]
		public JsonResult GetApprovalFieldData(OMTableParam idTable, int objectId, string paramName, bool isActual)
		{
			var act = isActual ? OMParam.GetActual(idTable, objectId, paramName) : null;
			var paramValues = isActual ? act != null ? new List<OMParam> { OMParam.GetActual(idTable, objectId, paramName) } : new List<OMParam>() :
				OMParam.GetParams(idTable, objectId, paramName).OrderByDescending(x => x.DateUser).ToList();

			List<SelectListItem> res = paramValues.Select(x => new SelectListItem
			{
				Value = $"{x.Pid}",
				Text = $"{x} ({(SRDCache.Users.ContainsKey((int)x.IdUser) ? SRDCache.Users[(int)x.IdUser].FullName : String.Empty)}, {x.DateUser.ToString("dd.MM.yyyy")})"
			}).ToList();

			return Json(res);
		}

		[HttpGet]
		public JsonResult GetAutoFillDataByKn(string kn)
		{
			return Json(new { Address = "address", Type = SudObjectType.None });
		}

		#endregion

		[HttpGet]
		public JsonResult GetDictionary(int type)
		{
			List<OMDict> dictList = OMDict
				.Where(x => x.Type == type && x.Name != string.Empty)
				.SelectAll()
				.Execute().ToList();

			return Json(dictList);
		}
		#region Autocomplite
		public IQueryable GetAutoCompleteReport(string searchText)
		{
			return OMOtchet
				.Where(x => x.Number.StartsWith(searchText))
				.SelectAll().OrderBy(x => x.Number).Execute().Select(x => new
				{
					x.Id,
					Value = $"{x.Number} от {x.Date.GetString()}"
				}).AsQueryable();
		}

		public IQueryable GetAutoCompleteConclusion(string searchText)
		{
			return OMZak
				.Where(x => x.Number.StartsWith(searchText))
				.SelectAll().OrderBy(x => x.Number).Execute().Select(x => new
				{
					x.Id,
					Value = $"{x.Number} от {x.Date.GetString()}"
				}).AsQueryable();
		}

		public IQueryable GetAutoCompleteCourt(string searchText)
		{
			return OMSud
				.Where(x => x.Number.StartsWith(searchText) || x.AppealNumber.StartsWith(searchText) || x.ArchiveNumber.StartsWith(searchText))
				.SelectAll().OrderBy(x => x.Number).Execute().Select(x => new
				{
					x.Id,
					Value = x.Date != null ? $"{x.Number} от {x.Date.GetString()}" : x.Number
				}).AsQueryable();
		}

		[HttpGet]
		public IQueryable GetKnData(string searchText)
		{
			//OMSource2.Where(x => x.)

			return new List<Object>() { new { text = "123", value = "123" }, new { text = "456", value = "456" } }.AsQueryable();
		}

		#endregion

		#region Court 

		[HttpGet]
		public ActionResult EditCourt(int courtId)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_RESH, true, false, true);
			var omSud = OMSud
				.Where(x => x.Id == courtId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			var model = courtId != 0 && omSud != null
				? CourtModel.FromEntity(omSud)
				: CourtModel.FromEntity(new OMSud());

			model.IsEditCourt =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_RESH_EDIT);
			return View(model);
		}

		[HttpPost]
		public ActionResult EditCourt(CourtModel courtViewModel)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_RESH_EDIT, true, false, true);
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
				id = omSud.SaveAndCheckParam();
				ts.Complete();
			}
			courtViewModel.Id = id;

			return Json(new { Success = "Сохранено успешно", data = courtViewModel });
		}

		[HttpGet]
		public ActionResult EditCourtLink(int courtLinkId, long sudObjectId)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_EDIT, true, false, true);
			if (sudObjectId == 0 && courtLinkId == 0)
			{
				throw new Exception("В указанном запросе отсутствует ИД объекта. ?sudObjectId=IdObject");
			}
			var courtLink = OMSudLink
				.Where(x => x.Id == courtLinkId)
				.SelectAll()
				.Execute().FirstOrDefault();

			OMSud court = null;
			if (courtLink != null)
			{
				court = OMSud
					.Where(x => x.Id == courtLink.IdSud)
					.SelectAll()
					.ExecuteFirstOrDefault();
			}
			var model = courtLinkId != 0 && courtLink != null && court != null
				? CourtLinkModel.FromEntity(courtLink, court)
				: CourtLinkModel.FromEntity(new OMSudLink(), new OMSud());
			model.ObjectId = courtLink != null && courtLinkId != 0
				? courtLink.IdObject.GetValueOrDefault()
				: sudObjectId;

			var currentSudObjectId = courtLink != null && courtLinkId != 0 ? courtLink.IdObject.GetValueOrDefault() : sudObjectId;

			model.SquareObject = OMObject.Where(x => x.Id == currentSudObjectId).Select(x => x.Square)
				.ExecuteFirstOrDefault().Square.GetValueOrDefault();

			model.IsEditCourt =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_RESH_EDIT);

			model.IsEditCourtLink =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_EDIT);
			return View(model);
		}

		[HttpPost]
		public ActionResult EditCourtLink(CourtLinkModel courtLinkViewModel)
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

			var links = OMSudLink.Where(x => x.IdObject == courtLinkViewModel.ObjectId &&
												x.IdSud == courtLinkViewModel.SudId && x.Id != courtLinkViewModel.Id).Execute();
			if (links.Any())
			{
				return Json(new
				{
					Errors = links.Select(x => new
					{
						Control = 0,
						Message = "Объект с таким решением уже существует. Выберите другое решение."
					})
				});
			}

			var sudLink = OMSudLink
				.Where(x => x.Id == courtLinkViewModel.Id)
				.SelectAll()
				.Execute().FirstOrDefault();

			if (courtLinkViewModel.Id != -1 && sudLink == null)
			{
				return NotFound();
			}

			if (sudLink == null)
			{
				sudLink = new OMSudLink();
			}
			CourtLinkModel.ToEntity(courtLinkViewModel, ref sudLink);
			long id;
			using (var ts = new TransactionScope())
			{
				id = sudLink.SaveAndCheckParam();
				ts.Complete();
			}
			courtLinkViewModel.Id = id;

			return Json(new { Success = "Сохранено успешно", data = courtLinkViewModel });
		}
		#endregion

		#region Upload Document

		[HttpGet]
		public ActionResult LoadDocument()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_IMPORT, true, false, true);

			return View();
		}

		[HttpPost]
		public ActionResult LoadDocument(IFormFile file)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_IMPORT, true, false, true);
			try
			{
				using (var stream = file.OpenReadStream())
				{
					DataImporterSud.AddImportToQueue(OMObject.GetRegisterId(), "SudObjects", file.FileName, stream);
				}
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return NoContent();
		}

		#endregion

		#region Approval Card
		[HttpGet]
		public ActionResult EditApprovalObject(int idObject)
		{

			var isApproved = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_APPROVE);

			List<OMParam> paramValues = OMParam.GetAllParamsById(OMTableParam.Object, idObject)
				.Where(x => x.ParamStatus_Code == ProcessingStatus.Processed).ToList();

			var model = EditApprovalObjectModel.FromEntity(paramValues);
			model.Id = idObject;
			model.IsDisableButton = model.IsDisableButton || !isApproved;

			return View(model);
		}

		[HttpPost]
		public ActionResult EditApprovalObject(EditApprovalObjectModel model)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_APPROVE, true, false, true);

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

			OMObject sudObject = OMObject.Where(x => x.Id == model.Id).SelectAll().ExecuteFirstOrDefault();
			if (sudObject == null)
			{
				return NotFound();
			}

			OMParam pKn = OMParam.Where(x => x.Pid == long.Parse(model.Kn)).SelectAll().ExecuteFirstOrDefault();
			OMParam pType = OMParam.Where(x => x.Pid == long.Parse(model.TypeObj)).SelectAll().ExecuteFirstOrDefault();
			OMParam pSquare = OMParam.Where(x => x.Pid == long.Parse(model.Square)).SelectAll().ExecuteFirstOrDefault();
			OMParam pKc = OMParam.Where(x => x.Pid == long.Parse(model.Kc)).SelectAll().ExecuteFirstOrDefault();
			OMParam pDate = OMParam.Where(x => x.Pid == long.Parse(model.Date)).SelectAll().ExecuteFirstOrDefault();
			OMParam pNameCenter = OMParam.Where(x => x.Pid == long.Parse(model.NameCenter)).SelectAll().ExecuteFirstOrDefault();
			OMParam pStatDgi = OMParam.Where(x => x.Pid == long.Parse(model.StatDgi)).SelectAll().ExecuteFirstOrDefault();
			OMParam pAdres = OMParam.Where(x => x.Pid == long.Parse(model.Adres)).SelectAll().ExecuteFirstOrDefault();
			OMParam pOwner = OMParam.Where(x => x.Pid == long.Parse(model.Owner)).SelectAll().ExecuteFirstOrDefault();
			OMParam pApplicantType = OMParam.Where(x => x.Pid == long.Parse(model.ApplicantType)).SelectAll().ExecuteFirstOrDefault();
			OMParam pTypeOfOwnership = OMParam.Where(x => x.Pid == long.Parse(model.TypeOfOwnership)).SelectAll().ExecuteFirstOrDefault();
			OMParam pAdditionalAnalysisRequired = OMParam.Where(x => x.Pid == long.Parse(model.AdditionalAnalysisRequired)).SelectAll().ExecuteFirstOrDefault();
			OMParam pException = OMParam.Where(x => x.Pid == long.Parse(model.IsException)).SelectAll().ExecuteFirstOrDefault();
			OMParam pSatisfied = OMParam.Where(x => x.Pid == long.Parse(model.IsSatisfied)).SelectAll().ExecuteFirstOrDefault();

			sudObject.UpdateAndCheckParam(pKn, pType, pSquare, pKc, pDate, pNameCenter, pStatDgi, pAdres, pOwner, pApplicantType, pTypeOfOwnership, pAdditionalAnalysisRequired, pException, pSatisfied);

			return Json(new { Success = "Утверждено успешно" });
		}

		public ActionResult GetReportContent(int idObject)
		{
			bool isApprovedReportLink = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_OTCHET_APPROVE);

			bool isApprovedReport = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OTCHET_APPROVE);

			List<OMOtchetLink> reportLinks = OMOtchetLink.Where(x => x.IdObject == idObject).SelectAll().Execute();

			List<long> idLinks = reportLinks.Select(x => x.Id).ToList();
			List<long?> idReports = reportLinks.Select(x => x.IdOtchet).Where(x => x != null).ToList();

			if (idReports.Count == 0)
			{
				return PartialView("~/Views/Sud/TabContent/ReportContent.cshtml", new List<EditApprovalReportLinkModel>());

			}
			List<OMParam> param = OMParam.Where(x => (x.IdTable == (long)OMTableParam.OtchetLink && idLinks.Contains(x.Id) || x.IdTable == (long)OMTableParam.Otchet
							  && idReports.Contains(x.Id)) && x.ParamStatus_Code == ProcessingStatus.Processed).SelectAll().Execute();


			List<OMParam> forModel = new List<OMParam>();

			List<EditApprovalReportLinkModel> model = new List<EditApprovalReportLinkModel>();

			foreach (var reportLink in reportLinks)
			{
				forModel.AddRange(param.Where(x => x.Id == reportLink.Id));
				forModel.AddRange(param.Where(x => x.Id == reportLink.IdOtchet));

				var tempModel = EditApprovalReportLinkModel.FromEntity(forModel);
				tempModel.Id = reportLink.Id;
				tempModel.Report.Id = reportLink.IdOtchet;
				tempModel.Report.IsDisableButton = tempModel.Report.IsDisableButton || !isApprovedReport;
				tempModel.IsDisableButton = tempModel.IsDisableButton || !isApprovedReportLink;



				model.Add(tempModel);
				forModel.Clear();
			}

			return PartialView("~/Views/Sud/TabContent/ReportContent.cshtml", model);
		}

		public ActionResult EditApprovalReportLink(EditApprovalReportLinkModel model)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_OTCHET_APPROVE, true, false, true);

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

			OMOtchetLink reportLink = OMOtchetLink.Where(x => x.Id == model.Id).SelectAll().ExecuteFirstOrDefault();
			if (reportLink == null)
			{
				throw new Exception("Не надена модель по указанному ИД");
			}

			OMParam pUse = OMParam.Where(x => x.Pid == long.Parse(model.Use)).SelectAll().ExecuteFirstOrDefault();
			OMParam pDescr = OMParam.Where(x => x.Pid == long.Parse(model.Descr)).SelectAll().ExecuteFirstOrDefault();
			OMParam pRs = OMParam.Where(x => x.Pid == long.Parse(model.Rs)).SelectAll().ExecuteFirstOrDefault();
			OMParam pUprs = OMParam.Where(x => x.Pid == long.Parse(model.Uprs)).SelectAll().ExecuteFirstOrDefault();
			OMParam pIdOtchet = OMParam.Where(x => x.Pid == long.Parse(model.IdReport)).SelectAll().ExecuteFirstOrDefault();

			reportLink.UpdateAndCheckParam(pUse, pDescr, pRs, pUprs, pIdOtchet);

			return Json(new { Success = "Утверждено успешно" });
		}
		public ActionResult EditApprovalReport(EditApprovalReportModel model)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OTCHET_APPROVE, true, false, true);

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

			OMOtchet report = OMOtchet.Where(x => x.Id == model.Id).SelectAll().ExecuteFirstOrDefault();
			if (report == null)
			{
				throw new Exception("Не надена модель по указанному ИД");
			}

			OMParam pNumber = OMParam.Where(x => x.Pid == long.Parse(model.Number)).SelectAll().ExecuteFirstOrDefault();
			OMParam pDate = OMParam.Where(x => x.Pid == long.Parse(model.ReportDate)).SelectAll().ExecuteFirstOrDefault();
			OMParam pDateIn = OMParam.Where(x => x.Pid == long.Parse(model.DateIn)).SelectAll().ExecuteFirstOrDefault();
			OMParam pJalob = OMParam.Where(x => x.Pid == long.Parse(model.Claim)).SelectAll().ExecuteFirstOrDefault();
			OMParam pOrg = OMParam.Where(x => x.Pid == long.Parse(model.Org)).SelectAll().ExecuteFirstOrDefault();
			OMParam pFio = OMParam.Where(x => x.Pid == long.Parse(model.Fio)).SelectAll().ExecuteFirstOrDefault();
			OMParam pSro = OMParam.Where(x => x.Pid == long.Parse(model.Sro)).SelectAll().ExecuteFirstOrDefault();

			report.UpdateAndCheckParam(pNumber, pDate, pDateIn, pJalob, pOrg, pFio, pSro);

			return Json(new { Success = "Утверждено успешно" });
		}

		public ActionResult GetCourtContent(int idObject)
		{
			bool isApprovedCourtLink = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_RESH_APPROVE);

			bool isApprovedCourt = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_RESH_APPROVE);

			List<OMSudLink> courtLinks = OMSudLink.Where(x => x.IdObject == idObject).SelectAll().Execute();

			List<long> idLinks = courtLinks.Select(x => x.Id).ToList();
			List<long?> idCourts = courtLinks.Select(x => x.IdSud).Where(x => x != null).ToList();

			if (idCourts.Count == 0)
			{
				return PartialView("~/Views/Sud/TabContent/CourtContent.cshtml", new List<EditApprovalCourtLinkModel>());
			}

			List<OMParam> param = OMParam.Where(x => (x.IdTable == (long)OMTableParam.SudLink && idLinks.Contains(x.Id) || x.IdTable == (long)OMTableParam.Sud
																																&& idCourts.Contains(x.Id)) && x.ParamStatus_Code == ProcessingStatus.Processed).SelectAll().Execute();


			List<OMParam> forModel = new List<OMParam>();

			List<EditApprovalCourtLinkModel> model = new List<EditApprovalCourtLinkModel>();

			foreach (var courtLink in courtLinks)
			{
				forModel.AddRange(param.Where(x => x.Id == courtLink.Id));
				forModel.AddRange(param.Where(x => x.Id == courtLink.IdSud));

				var tempModel = EditApprovalCourtLinkModel.FromEntity(forModel);
				tempModel.Id = courtLink.Id;
				tempModel.Court.Id = courtLink.IdSud;
				tempModel.IsDisableButton = tempModel.IsDisableButton || !isApprovedCourtLink;
				tempModel.Court.IsDisableButton = tempModel.Court.IsDisableButton || !isApprovedCourt;


				model.Add(tempModel);
				forModel.Clear();
			}
			return PartialView("~/Views/Sud/TabContent/CourtContent.cshtml", model);
		}
		public ActionResult EditApprovalCourt(EditApprovalCourtModel model)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_RESH_APPROVE, true, false, true);

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

			OMSud court = OMSud.Where(x => x.Id == model.Id).SelectAll().ExecuteFirstOrDefault();
			if (court == null)
			{
				throw new Exception("Не надена модель по указанному ИД");
			}

			OMParam pNumber = OMParam.Where(x => x.Pid == long.Parse(model.Number)).SelectAll().ExecuteFirstOrDefault();
			OMParam pName = OMParam.Where(x => x.Pid == long.Parse(model.Name)).SelectAll().ExecuteFirstOrDefault();
			OMParam pDate = OMParam.Where(x => x.Pid == long.Parse(model.Date)).SelectAll().ExecuteFirstOrDefault();
			OMParam pSudDate = OMParam.Where(x => x.Pid == long.Parse(model.SudDate)).SelectAll().ExecuteFirstOrDefault();
			OMParam pStatus = OMParam.Where(x => x.Pid == long.Parse(model.Status)).SelectAll().ExecuteFirstOrDefault();
			OMParam pArchiveNumber = OMParam.Where(x => x.Pid == long.Parse(model.ArchiveNumber)).SelectAll().ExecuteFirstOrDefault();
			OMParam pAppealNumber = OMParam.Where(x => x.Pid == long.Parse(model.AppealNumber)).SelectAll().ExecuteFirstOrDefault();

			court.UpdateAndCheckParam(pNumber, pName, pDate, pSudDate, pStatus, pArchiveNumber, pAppealNumber);

			return Json(new { Success = "Утверждено успешно" });
		}

		public ActionResult EditApprovalCourtLink(EditApprovalCourtLinkModel model)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_RESH_APPROVE, true, false, true);

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

			OMSudLink courtLink = OMSudLink.Where(x => x.Id == model.Id).SelectAll().ExecuteFirstOrDefault();
			if (courtLink == null)
			{
				throw new Exception("Не надена модель по указанному ИД");
			}

			OMParam pUse = OMParam.Where(x => x.Pid == long.Parse(model.Use)).SelectAll().ExecuteFirstOrDefault();
			OMParam pDescr = OMParam.Where(x => x.Pid == long.Parse(model.Description)).SelectAll().ExecuteFirstOrDefault();
			OMParam pRs = OMParam.Where(x => x.Pid == long.Parse(model.Rs)).SelectAll().ExecuteFirstOrDefault();
			OMParam pUprs = OMParam.Where(x => x.Pid == long.Parse(model.Uprs)).SelectAll().ExecuteFirstOrDefault();
			OMParam pIdSud = OMParam.Where(x => x.Pid == long.Parse(model.SudId)).SelectAll().ExecuteFirstOrDefault();

			courtLink.UpdateAndCheckParam(pUse, pDescr, pRs, pUprs, pIdSud);

			return Json(new { Success = "Утверждено успешно" });
		}
		public ActionResult GetConclusionContent(int idObject)
		{
			bool isApprovedConclusion = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_ZAK_APPROVE);

			bool isApprovedConclusionLink = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_ZAK_APPROVE);

			List<OMZakLink> conclusionLinks = OMZakLink.Where(x => x.IdObject == idObject).SelectAll().Execute();

			List<long> idLinks = conclusionLinks.Select(x => x.Id).ToList();
			List<long?> idConclusions = conclusionLinks.Select(x => x.IdZak).Where(x => x != null).ToList();

			if (idConclusions.Count == 0)
			{
				return PartialView("~/Views/Sud/TabContent/ConclusionContent.cshtml", new List<EditApprovalConclusionLinkModel>());
			}

			List<OMParam> param = OMParam.Where(x => (x.IdTable == (long)OMTableParam.ZakLink && idLinks.Contains(x.Id) || x.IdTable == (long)OMTableParam.Zak
																															 && idConclusions.Contains(x.Id)) && x.ParamStatus_Code == ProcessingStatus.Processed).SelectAll().Execute();


			List<OMParam> forModel = new List<OMParam>();

			List<EditApprovalConclusionLinkModel> model = new List<EditApprovalConclusionLinkModel>();

			foreach (var conclusionLink in conclusionLinks)
			{
				forModel.AddRange(param.Where(x => x.Id == conclusionLink.Id));
				forModel.AddRange(param.Where(x => x.Id == conclusionLink.IdZak));

				var tempModel = EditApprovalConclusionLinkModel.FromEntity(forModel);
				tempModel.Id = conclusionLink.Id;
				tempModel.Conclusion.Id = conclusionLink.IdZak;
				tempModel.IsDisableButton = tempModel.IsDisableButton || !isApprovedConclusionLink;
				tempModel.Conclusion.IsDisableButton = tempModel.Conclusion.IsDisableButton || !isApprovedConclusion;


				model.Add(tempModel);
				forModel.Clear();
			}
			return PartialView("~/Views/Sud/TabContent/ConclusionContent.cshtml", model);
		}

		public ActionResult EditApprovalConclusion(EditApprovalConclusionModel model)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_ZAK_APPROVE, true, false, true);

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

			OMZak conclusion = OMZak.Where(x => x.Id == model.Id).SelectAll().ExecuteFirstOrDefault();
			if (conclusion == null)
			{
				return NotFound();
			}

			OMParam pNumber = OMParam.Where(x => x.Pid == long.Parse(model.Number)).SelectAll().ExecuteFirstOrDefault();
			OMParam pDate = OMParam.Where(x => x.Pid == long.Parse(model.CreateDate)).SelectAll().ExecuteFirstOrDefault();
			OMParam pRecDate = OMParam.Where(x => x.Pid == long.Parse(model.RecDate)).SelectAll().ExecuteFirstOrDefault();
			OMParam pRecLetter = OMParam.Where(x => x.Pid == long.Parse(model.RecLetter)).SelectAll().ExecuteFirstOrDefault();
			OMParam pRecUser = OMParam.Where(x => x.Pid == long.Parse(model.RecUser)).SelectAll().ExecuteFirstOrDefault();
			OMParam pOrg = OMParam.Where(x => x.Pid == long.Parse(model.Org)).SelectAll().ExecuteFirstOrDefault();
			OMParam pFio = OMParam.Where(x => x.Pid == long.Parse(model.Fio)).SelectAll().ExecuteFirstOrDefault();
			OMParam pSro = OMParam.Where(x => x.Pid == long.Parse(model.Sro)).SelectAll().ExecuteFirstOrDefault();
			OMParam pRecBefore = OMParam.Where(x => x.Pid == long.Parse(model.RecBefore)).SelectAll().ExecuteFirstOrDefault();
			OMParam pRecAfter = OMParam.Where(x => x.Pid == long.Parse(model.RecAfter)).SelectAll().ExecuteFirstOrDefault();
			OMParam pRecSoglas = OMParam.Where(x => x.Pid == long.Parse(model.RecSoglas)).SelectAll().ExecuteFirstOrDefault();

			conclusion.UpdateAndCheckParam(pNumber, pDate, pRecDate, pRecLetter, pRecUser, pOrg, pFio, pSro, pRecBefore, pRecAfter, pRecSoglas);

			return Json(new { Success = "Утверждено успешно" });
		}

		public ActionResult EditApprovalConclusionLink(EditApprovalConclusionLinkModel model)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_ZAK_APPROVE, true, false, true);

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

			OMZakLink conclusionLink = OMZakLink.Where(x => x.Id == model.Id).SelectAll().ExecuteFirstOrDefault();
			if (conclusionLink == null)
			{
				return NotFound();
			}

			OMParam pUse = OMParam.Where(x => x.Pid == long.Parse(model.Use)).SelectAll().ExecuteFirstOrDefault();
			OMParam pDescr = OMParam.Where(x => x.Pid == long.Parse(model.Descr)).SelectAll().ExecuteFirstOrDefault();
			OMParam pRs = OMParam.Where(x => x.Pid == long.Parse(model.Rs)).SelectAll().ExecuteFirstOrDefault();
			OMParam pUprs = OMParam.Where(x => x.Pid == long.Parse(model.Uprs)).SelectAll().ExecuteFirstOrDefault();
			OMParam pIdZak = OMParam.Where(x => x.Pid == long.Parse(model.IdConclusion)).SelectAll().ExecuteFirstOrDefault();

			conclusionLink.UpdateAndCheckParam(pUse, pDescr, pRs, pUprs, pIdZak);

			return Json(new { Success = "Утверждено успешно" });
		}

		#endregion

		#region Load Files

		public ActionResult GetExportDataToExcelGbu()
		{
			var model = new ExportDataFormModel
			{
				NotifyMessage = "Выполнить Выгрузку судебных решений для ГБУ?",
				ExportDataMethodName = nameof(ExportDataToExcelGbu),
				ExportDataInBackgroundModeMethodName = nameof(ExportDataToExcelGbuInBackgroundMode),
				Filename = "Выгрузка судебных решений для ГБУ.xlsx"
			};

			return View("ExportData", model);
		}

		public FileResult ExportDataToExcelGbu()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_EXPORT_GBU, true, false, true);
			var file = DataExporterSud.ExportDataToExcelGbu();
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				"Выгрузка судебных решений для ГБУ" + ".xlsx");
		}

		public ActionResult ExportDataToExcelGbuInBackgroundMode()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_EXPORT_GBU, true, false, true);
			try
			{
				SudExportDataToExcelGbuProcess.AddImportToQueue();
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new
			{ Success = "Процедура Выгрузки судебных решений для ГБУ в формате Excel успешно добавлена в очередь" });
		}

		public ActionResult GetExportDataToXml()
		{
			var model = new ExportDataFormModel
			{
				NotifyMessage = "Выполнить Выгрузку судебных решений на сайт в формате XML?",
				ExportDataMethodName = nameof(ExportDataToXml),
				ExportDataInBackgroundModeMethodName = nameof(ExportDataToXmlInBackgroundMode),
				Filename = "Выгрузка судебных решений на сайт в формате XML.xml"
			};

			return View("ExportData", model);
		}

		public FileResult ExportDataToXml()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_EXPORT_XML, true, false, true);
			var file = DataExporterSud.ExportDataToXml();
			return File(file, "application/xml",
				"Выгрузка судебных решений на сайт в формате XML" + ".xml");
		}

		public ActionResult ExportDataToXmlInBackgroundMode()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_EXPORT_XML, true, false, true);
			try
			{
				SudExportDataToXmlProcess.AddImportToQueue();
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new
			{ Success = "Процедура Выгрузки судебных решений на сайт в формате XML успешно добавлена в очередь" });
		}

		public ActionResult GetExportAllDataToExcel()
		{
			var model = new ExportDataFormModel
			{
				NotifyMessage = "Выполнить Полную выгрузку в Excel?",
				ExportDataMethodName = nameof(ExportAllDataToExcel),
				ExportDataInBackgroundModeMethodName = nameof(ExportAllDataToExcelInBackgroundMode),
				Filename = "Полная выгрузка.xlsx"
			};

			return View("ExportData", model);
		}

		public FileResult ExportAllDataToExcel()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_EXPORT_ALL, true, false, true);
			var file = DataExporterSud.ExportAllDataToExcel();
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				"Полная выгрузка" + ".xlsx");
		}

		public ActionResult ExportAllDataToExcelInBackgroundMode()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_EXPORT_ALL, true, false, true);
			try
			{
				SudExportAllDataToExcelProcess.AddImportToQueue();
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new
			{ Success = "Процедура Полной выгрузки в Excel успешно добавлена в очередь" });
		}

		public ActionResult GetExportStatisticCheck()
		{
			var model = new ExportDataFormModel
			{
				NotifyMessage = "Выполнить выгрузку Статистики по положительным судебным решениям?",
				ExportDataMethodName = nameof(ExportStatisticCheck),
				ExportDataInBackgroundModeMethodName = nameof(ExportStatisticCheckInBackgroundMode),
				Filename = "Статистика по положительным судебным решениям.xlsx"
			};

			return View("ExportData", model);
		}

		public FileResult ExportStatisticCheck()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_STATISTICS_TRUE, true, false, true);
			var file = DataExporterSud.ExportStatisticCheck();
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				"Статистика по положительным судебным решениям" + ".xlsx");
		}

		public ActionResult ExportStatisticCheckInBackgroundMode()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_STATISTICS_TRUE, true, false, true);
			try
			{
				SudExportStatisticCheckProcess.AddImportToQueue();
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new
			{ Success = "Процедура выгрузки Статистики по положительным судебным решениям успешно добавлена в очередь" });
		}

		public ActionResult GetExportStatistic()
		{
			var model = new ExportDataFormModel
			{
				NotifyMessage = "Выполнить выгрузку Cводной cтатистики?",
				ExportDataMethodName = nameof(ExportStatistic),
				ExportDataInBackgroundModeMethodName = nameof(ExportStatisticInBackgroundMode),
				Filename = "Статистика сводная.xlsx"
			};

			return View("ExportData", model);
		}

		public FileResult ExportStatistic()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_STATISTICS_SUMMARY, true, false, true);
			var file = DataExporterSud.ExportStatistic();
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				"Статистика сводная" + ".xlsx");
		}

		public ActionResult ExportStatisticInBackgroundMode()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_STATISTICS_SUMMARY, true, false, true);
			try
			{
				SudExportStatisticProcess.AddImportToQueue();
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new
			{ Success = "Процедура выгрузки сводной Статистики успешно добавлена в очередь" });
		}

		public ActionResult GetExportStatisticObject()
		{
			var model = new ExportDataFormModel
			{
				NotifyMessage = "Выполнить выгрузку Статистики по объектам недвижимости?",
				ExportDataMethodName = nameof(ExportStatisticObject),
				ExportDataInBackgroundModeMethodName = nameof(ExportStatisticObjectInBackgroundMode),
				Filename = "Статистика по объектам недвижимости.xlsx"
			};

			return View("ExportData", model);
		}

		public FileResult ExportStatisticObject()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_STATISTICS_OBJECT, true, false, true);
			var file = DataExporterSud.ExportStatisticObject();
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				"Статистика по объектам недвижимости" + ".xlsx");
		}

		public ActionResult ExportStatisticObjectInBackgroundMode()
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_STATISTICS_OBJECT, true, false, true);
			try
			{
				SudExportStatisticObjectProcess.AddImportToQueue();
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return Json(new
			{ Success = "Процедура выгрузки Статистики по объектам недвижимости успешно добавлена в очередь" });
		}

		#endregion

		#region Attachments

		public ActionResult GetAllAttachmentsReport(int objectId, int isFile)
		{
			if (isFile == 0)
			{
				return NoContent();
			}
			return RedirectToAction("AttachmentView", "CoreAttachment",
				new { objectId, registerId = OMOtchet.GetRegisterId() });
		}

		public ActionResult GetAllAttachmentsConclusion(int objectId, int isFile)
		{
			if (isFile == 0)
			{
				return NoContent();
			}
			return RedirectToAction("AttachmentView", "CoreAttachment",
				new { objectId, registerId = OMZak.GetRegisterId() });
		}

		public ActionResult GetAllAttachmentsCourt(int objectId, int isFile)
		{
			if (isFile == 0)
			{
				return NoContent();
			}
			return RedirectToAction("AttachmentView", "CoreAttachment",
				new { objectId, registerId = OMSud.GetRegisterId() });
		}

		public ActionResult GetAllAttachmentsRemovedObject(int objectId, int isFile)
		{
			if (isFile == 0)
			{
				return NoContent();
			}
			return RedirectToAction("AttachmentView", "CoreAttachment",
				new { objectId, registerId = OMObject.GetRegisterId() });
		}

		#endregion

		#region Approval Object

		public ActionResult SatisfiedObject(int idObject, bool isCancel)
		{
			var objects = new List<OMObject>();
			if (RegistersVariables.CurrentList != null)
			{
				objects = OMObject.Where(x => RegistersVariables.CurrentList.Contains(x.Id)).SelectAll().Execute();

				if (objects.Any(x => x.IsSatisfied == 1) && objects.Any(x => x.IsSatisfied == 0))
				{
					var modelError = new ModalDialogDetails()
					{
						Action = ModalDialogDetails.ActionType.None,
						Buttons = ModalDialogDetails.ButtonType.Ok,
						Icon = ModalDialogDetails.IconType.Warning,
						IsProgress = false,
						Message = "Выбраны одновременно объекты с значением удовлеворонно и отказано!"
					};
					return View("~/Views/Shared/ModalDialogDetails.cshtml", modelError);
				}
			}

			if (RegistersVariables.CurrentList == null)
			{
				objects = OMObject.Where(x => x.Id == idObject).SelectAll().Execute();
			}

			if (objects.Count == 0)
			{
				var modelError = new ModalDialogDetails()
				{
					Action = ModalDialogDetails.ActionType.None,
					Buttons = ModalDialogDetails.ButtonType.Ok,
					Icon = ModalDialogDetails.IconType.Warning,
					IsProgress = false,
					Message = "Объекты не найдены!"
				};
				return View("~/Views/Shared/ModalDialogDetails.cshtml", modelError);
			}


			using (var ts = new TransactionScope())
			{
				foreach (var omObject in objects)
				{
					omObject.IsSatisfied = isCancel ? 0 : 1;
					omObject.SaveAndCheckParam();
				}
				ts.Complete();
			}

			var model = new ModalDialogDetails()
			{
				Action = ModalDialogDetails.ActionType.Reload,
				Buttons = ModalDialogDetails.ButtonType.Ok,
				Icon = ModalDialogDetails.IconType.Ok,
				IsProgress = false,
				Message = "Объекты изменены!"
			};
			return View("~/Views/Shared/ModalDialogDetails.cshtml", model);
		}

		#endregion

		#region Report By Additional checker

		public FileResult GetReportAdditionalCheck(int idProcess)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_EXPORT_DOP_PARAM, true, false, true);
			var file = AdditionalAnalysisChecker.GetReportAdditionalCheck(idProcess);
			return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				"Результат проставления признака доп. анализа" + ".xlsx");
		}

		#endregion


		[HttpGet]
		public ActionResult RedirectToSpd(string appId)
		{
			return Redirect("http://webspd.mlc.gov/gosusl/gosuslweb/WebFormDoc.aspx?APPID=" + appId);
		}
	}
}
