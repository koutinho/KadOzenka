
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using CIPJS.Models.Commission;
using Core.ErrorManagment;
using Core.UI.Registers.Controllers;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Commission;

namespace KadOzenka.Web.Controllers
{
	public class CommissionController : BaseController
	{
		[HttpGet]
		public ActionResult EditCommission(long id)
		{
			OMCost commission = OMCost
				.Where(x => x.Id == id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			var model = commission != null
				? CommissionModel.FromEntity(commission)
				: CommissionModel.FromEntity(null);

			return View(model);
		}

		[HttpPost]
		public ActionResult EditCommission(CommissionModel commissionViewModel)
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

			OMCost commission = OMCost
				.Where(x => x.Id == commissionViewModel.Id)
				.SelectAll()
				.Execute().FirstOrDefault();

			if (commissionViewModel.Id != -1 && commission == null)
			{
				return NotFound();
			}

			if (commission == null)
			{
				commission = new OMCost();
			}

			CommissionModel.ToEntity(commissionViewModel, ref commission);
			long id;
			using (var ts = new TransactionScope())
			{
				try
				{
					id = commission.Save();
					ts.Complete();
				}
				catch (Exception e)
				{
					return Json(new
					{
						Errors =
							new
							{
								Control = string.Empty,
								e.Message
							}
					});
				}
			}

			commissionViewModel.Id = id;
			return Json(new { Success = "Сохранено успешно", data = commissionViewModel });
		}

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

				DataImporter.ImportDataCommissionFromExcel(excelFile);
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return NoContent();
		}

		#endregion

	}
}