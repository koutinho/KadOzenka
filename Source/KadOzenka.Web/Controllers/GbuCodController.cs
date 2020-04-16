using System;
using System.IO;
using System.Transactions;
using Core.ErrorManagment;
using Core.UI.Registers.Controllers;
using KadOzenka.Dal.DataImport;
using KadOzenka.Web.Models.GbuCod;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Core.Reports;
using ObjectModel.Declarations;
using ObjectModel.KO;

namespace KadOzenka.Web.Controllers
{
	public class GbuCodController : KoBaseController
	{
		#region CodJob

		[HttpGet]
		public ActionResult CodJobObjectCard(long id)
		{
			var codJob = OMCodJob.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();

			return View(CodJobViewModel.FromEntity(codJob));
		}

		[HttpPost]
		public ActionResult CodJobObjectCard(CodJobViewModel viewModel)
		{
			var codJob = OMCodJob.Where(x => x.Id == viewModel.Id).SelectAll().ExecuteFirstOrDefault();

			if (viewModel.Id != -1 && codJob == null)
			{
				return NotFound();
			}
			if (codJob == null)
			{
				codJob = new OMCodJob();
			}

			CodJobViewModel.ToEntity(viewModel, ref codJob);
			long id;
			try
			{
                id = codJob.Save();
			}
			catch (Exception e)
			{
			    return SendErrorMessage(e.Message);
            }
			viewModel.Id = id;
			return Json(new { Success = "Сохранено успешно", data = viewModel });
		}

		[HttpGet]
		public IActionResult DeleteCodJob(long codJobId)
		{
			var codJob = OMCodJob.Where(x => x.Id == codJobId).SelectAll().ExecuteFirstOrDefault();
			if (codJob == null)
			{
				throw new Exception($"Задание ЦОД с ИД {codJobId} не найдено");
			}

			return View(CodJobViewModel.FromEntity(codJob));
		}

		[HttpPost]
		public IActionResult DeleteCodJob(CodJobViewModel viewModel)
		{
			var codJob = OMCodJob.Where(x => x.Id == viewModel.Id).SelectAll().ExecuteFirstOrDefault();
			if (codJob == null)
			{
				throw new Exception($"Задание ЦОД с ИД {viewModel.Id} не найдено");
			}

			using (var ts = new TransactionScope())
			{
				var codJobDictionaries =
					OMCodDictionary.Where(x => x.IdCodjob == codJob.Id).SelectAll().Execute();
				foreach (var codDictionary in codJobDictionaries)
				{
					codDictionary.Destroy();
				}
				codJob.Destroy();
				ts.Complete();
			}

			return EmptyResponse();
		}

		#endregion CodJob

		#region CodDictionary

		[HttpGet]
		public ActionResult CodDictionaryObjectCard(long id, long jobId)
		{
			if (jobId == 0)
			{
				throw new Exception("В указанном запросе отсутствует ИД задания ЦОД");
			}

			var codJob = OMCodJob.Where(x => x.Id == jobId).SelectAll().ExecuteFirstOrDefault();
			if (codJob == null)
			{
				throw new Exception($"Задание ЦОД с ИД {jobId} не найдено");
			}

			var codDictionary = OMCodDictionary
				.Where(x => x.Id == id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			var model = CodDictionaryViewModel.FromEntity(codDictionary);
			model.JobId = codJob.Id;

			return View(model);
		}

		[HttpPost]
		public ActionResult CodDictionaryObjectCard(CodDictionaryViewModel viewModel)
		{
			var codDictionary = OMCodDictionary.Where(x => x.Id == viewModel.Id).SelectAll().ExecuteFirstOrDefault();

			if (viewModel.Id != -1 && codDictionary == null)
			{
				return NotFound();
			}
			if (codDictionary == null)
			{
				codDictionary = new OMCodDictionary();
			}

			CodDictionaryViewModel.ToEntity(viewModel, ref codDictionary);
			long id;
			try
			{
				id = codDictionary.Save();
			}
			catch (Exception e)
			{
			    return SendErrorMessage(e.Message);
			}
			viewModel.Id = id;
			return Json(new { Success = "Сохранено успешно", data = viewModel });
		}

		[HttpGet]
		public IActionResult DeleteCodDictionary(long codDictionaryId)
		{
			var codDictionary = OMCodDictionary.Where(x => x.Id == codDictionaryId).SelectAll().ExecuteFirstOrDefault();
			if (codDictionary == null)
			{
				throw new Exception($"Справочник ЦОД с ИД {codDictionaryId} не найден");
			}

			return View(CodDictionaryViewModel.FromEntity(codDictionary));
		}

		[HttpPost]
		public IActionResult DeleteCodDictionary(CodDictionaryViewModel viewModel)
		{
			var codDictionary = OMCodDictionary.Where(x => x.Id == viewModel.Id).SelectAll().ExecuteFirstOrDefault();
			if (codDictionary == null)
			{
				throw new Exception($"Справочник ЦОД с ИД {viewModel.Id} не найден");
			}

			codDictionary.Destroy();

			return EmptyResponse();
		}

		#endregion CodDictionary

		#region CodImport

		[HttpGet]
		public ActionResult ImportCod(long codJobId)
		{
			var codJob = OMCodJob.Where(x => x.Id == codJobId).SelectAll().ExecuteFirstOrDefault();
			if (codJob == null)
			{
				throw new Exception($"Задание ЦОД с ИД {codJobId} не найдено");
			}

			return View(CodJobViewModel.FromEntity(codJob));
		}

		[HttpPost]
		public ActionResult ImportCod(IFormFile file, long codId, bool deleteOld)
		{
			try
			{
				using (Stream fileStream = file.OpenReadStream())
				{
					DataImporterCod.ImportDataCodFromXml(fileStream, codId, deleteOld);
				}
			}
			catch (Exception ex)
			{
				ErrorManager.LogError(ex);
				return BadRequest();
			}
			return Ok();
		}

		#endregion CodImport
	}
}
