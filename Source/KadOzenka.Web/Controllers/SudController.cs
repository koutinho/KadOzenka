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
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ObjectModel.Directory.Sud;

namespace KadOzenka.Web.Controllers
{
	public class SudController : BaseController
	{
		#region ObjectCard

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

				objId = obj.SaveAndCheckParam();

                if (data.Id == -1)
				{
					drs.IdObject = objId;
				}
				drs.Save();
				ts.Complete();
			}

			return Json(new { Success = "Сохранено успешно", ObjectId = objId.ToString() });
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
				id = reportLink.SaveAndCheckParam();
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
				id = report.SaveAndCheckParam();
				ts.Complete();
			}

			reportViewModel.Id = id;

			return Json(new {Success = "Сохранено успешно", data = reportViewModel});
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
				id = conclusionLink.SaveAndCheckParam();
				ts.Complete();
			}

			conclusionLinkViewModel.Id = id;
			return Json(new { Success = "Сохранено успешно", data = conclusionLinkViewModel });
		}


		[HttpGet]
		public ActionResult EditConclusion(int conclusionId)
		{
			OMZak conclusion = OMZak
				.Where(x => x.Id == conclusionId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			var model = conclusionId != 0 && conclusion != null
				? ConclusionModel.FromEntity(conclusion)
				: ConclusionModel.FromEntity(new OMZak());
			return View(model);
		}

		[HttpPost]
		public ActionResult EditConclusion(ConclusionModel conclusionViewModel)
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

			return Json(new {data = report});
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

			return Json( new {data = сonclusion});
		}

        [HttpGet]
        public JsonResult GetApprovalFieldData(OMTableParam idTable, int objectId, string paramName, bool isActual)
        {
            var act = isActual ? OMParam.GetActual(idTable, objectId, paramName) : null;
            var paramValues = isActual ? act != null ? new List<OMParam>{ OMParam.GetActual(idTable, objectId, paramName)} : new List<OMParam>() :
                OMParam.GetParams(idTable, objectId, paramName);

            List<SelectListItem> res =  paramValues.Select(x => new SelectListItem
                {
                    Value = $"{x.Pid}",
                    Text = $"({(SRDCache.Users.ContainsKey((int)x.IdUser) ? SRDCache.Users[(int)x.IdUser].FullName : String.Empty)}, {x.DateUser.ToString("dd.MM.yyyy")}) {x}"
                }).ToList();

            if (!isActual)
            {
                res.Insert(0, new SelectListItem("-", ""));
            }
            return Json(res);
        }

        public JsonResult GetApprovalReportFieldData(int objectId, string paramName, bool isActual)
        {
            return Json(new {});
        }
        #endregion

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
				.Where(x => x.Number.StartsWith(searchText))
				.SelectAll().OrderBy(x => x.Number).Execute().Select(x => new
				{
					x.Id,
					Value = x.Date != null ? $"{x.Number} от {x.Date.GetString()}" : x.Number
				}).AsQueryable();
		}

		#endregion

		#region Court 

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
				id = omSud.SaveAndCheckParam();
				ts.Complete();
			}
			courtViewModel.Id = id;

			return Json(new { Success = "Сохранено успешно", data = courtViewModel });
		}

		[HttpGet]
		public ActionResult EditCourtLink(int courtLinkId, long sudObjectId)
		{
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

		#region Load Document

		[HttpGet]
		public ActionResult LoadDocument()
		{
			return View();
		}

		[HttpPost]
		public ActionResult LoadDocument(IFormFile file)
		{

			try
			{
				ExcelFile excelFile;
				using (var stream = file.OpenReadStream())
				{
					excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
					excelFile.DocumentProperties.Custom["FileName"] = file.FileName;
				}

				string registerViewId = "SudObjects";
				int mainRegisterId = 315;

				MemoryStream str = new MemoryStream();
				excelFile.Save(str, SaveOptions.XlsxDefault);
				str.Seek(0, SeekOrigin.Begin);
				DataImporterSud.SaveImportFile(str, excelFile, registerViewId, mainRegisterId);
				DataImporterSud.ImportDataSudFromExcel(excelFile, registerViewId, mainRegisterId);
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return NoContent();
		}

        #endregion

        #region ApprovalCard
        [HttpGet]
        public ActionResult EditApprovalObject(int idObject)
        {
            List<OMParam> paramValues = OMParam.GetAllParamsById(OMTableParam.Object, idObject)
                .Where(x => x.ParamStatus_Code == ProcessingStatus.Processed).ToList();

            var model = EditApprovalObjectModel.FromEntity(paramValues);
            model.Id = idObject;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult EditApprovalObject(EditApprovalObjectModel model)
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

            OMObject sudObject = OMObject.Where(x => x.Id == model.Id).SelectAll().ExecuteFirstOrDefault();
            if(sudObject == null)
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

            sudObject.UpdateAndCheckParam(pKn, pType, pSquare, pKc, pDate, pNameCenter, pStatDgi, pAdres, pOwner);

            return Json(new { Success = "Утверждено успешно" });
        }

        public ActionResult GetReportContent(int idObject)
        {
            List<OMOtchetLink> reportLinks = OMOtchetLink.Where(x => x.IdObject == idObject).SelectAll().Execute();

            List<long> idLinks = reportLinks.Select(x => x.Id).ToList();
            List<long?> idReports = reportLinks.Select(x => x.IdOtchet).Where(x => x != null).ToList();

            if (idReports.Count == 0)
            {
                return View("~/Views/Sud/TabContent/ReportContent.cshtml", new List<EditApprovalReportLinkModel>());

            }
            List<OMParam> param =  OMParam.Where(x => (x.IdTable == (long) OMTableParam.OtchetLink  && idLinks.Contains(x.Id) || x.IdTable == (long) OMTableParam.Otchet
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
                

                model.Add(tempModel);
                forModel.Clear();
            }

            return View("~/Views/Sud/TabContent/ReportContent.cshtml", model);
        }

        public ActionResult EditApprovalReportLink(EditApprovalReportLinkModel model)
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
            List<OMSudLink> courtLinks = OMSudLink.Where(x => x.IdObject == idObject).SelectAll().Execute();

            List<long> idLinks = courtLinks.Select(x => x.Id).ToList();
            List<long?> idCourts = courtLinks.Select(x => x.IdSud).Where(x => x != null).ToList();

            if (idCourts.Count == 0)
            {
                return View("~/Views/Sud/TabContent/CourtContent.cshtml", new List<EditApprovalCourtLinkModel>());
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

                model.Add(tempModel);
                forModel.Clear();
            }
            return View("~/Views/Sud/TabContent/CourtContent.cshtml", model);
        }
        public ActionResult EditApprovalCourt(EditApprovalCourtModel model)
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

            court.UpdateAndCheckParam(pNumber, pName, pDate, pSudDate, pStatus);

            return Json(new { Success = "Утверждено успешно" });
        }

        public ActionResult EditApprovalCourtLink(EditApprovalCourtLinkModel model)
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

            List<OMZakLink> conclusionLinks = OMZakLink.Where(x => x.IdObject == idObject).SelectAll().Execute();

            List<long> idLinks = conclusionLinks.Select(x => x.Id).ToList();
            List<long?> idConclusions = conclusionLinks.Select(x => x.IdZak).Where(x => x != null).ToList();

            if (idConclusions.Count == 0)
            {
                return View("~/Views/Sud/TabContent/ConclusionContent.cshtml", new List<EditApprovalConclusionLinkModel>());
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

                model.Add(tempModel);
                forModel.Clear();
            }
            return View("~/Views/Sud/TabContent/ConclusionContent.cshtml", model);
        }

        public ActionResult EditApprovalConclusion(EditApprovalConclusionModel model)
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
        }
}
