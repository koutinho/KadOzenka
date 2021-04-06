using System;
using System.Transactions;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.GbuCod;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.KO;
using ObjectModel.SRD;

namespace KadOzenka.Web.Controllers
{
	public class GbuCodController : KoBaseController
	{
		private ICodDictionaryService CodDictionaryService { get; }

		public GbuCodController(ICodDictionaryService codDictionaryService)
        {
            CodDictionaryService = codDictionaryService;
        }
		

        #region CodJob

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
		public ActionResult CodJobObjectCard(long id)
		{
			var codJob = OMCodJob.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();

			return View(CodJobViewModel.FromEntity(codJob));
		}

		//todo KOMO-7
		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
		public ActionResult CodJobObjectCard(CodJobViewModel viewModel)
		{
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            if (viewModel.Id == -1)
            {
                CodDictionaryService.AddCodDictionary(viewModel.ToDto());

                return Json(new { Success = "Сохранено успешно", data = viewModel });
			}

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
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD_JOB_DELETE)]
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
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD_JOB_DELETE)]
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
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD_JOB_ADD)]
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
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD_JOB_ADD)]
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
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
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
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
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
    }
}
