
using System;
using System.Collections.Generic;
using System.IO;
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
	public class CommissionController : KoBaseController
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
			    return GenerateMessageNonValidModel();
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
					return SendErrorMessage(e.Message);
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

				string registerViewId = "CommissionCost";
				int mainRegisterId = 400;

				MemoryStream str = new MemoryStream();
				excelFile.Save(str, SaveOptions.XlsxDefault);
				str.Seek(0, SeekOrigin.Begin);
				DataImporterCommission.SaveImportFile(str, excelFile, registerViewId, mainRegisterId);
				DataImporterCommission.ImportDataCommissionFromExcel(excelFile, registerViewId, mainRegisterId);
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