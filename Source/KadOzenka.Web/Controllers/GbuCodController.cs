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
		public ActionResult AddCodDictionary()
        {
            var model = new CodDictionaryAdditionModel {Id = -1};

			return View(model);
		}

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
        public ActionResult AddDictionary(CodDictionaryAdditionModel viewModel)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var dto = viewModel.ToDto();
			CodDictionaryService.AddCodDictionary(dto);

			return Ok();
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
        public ActionResult CodJobObjectCard(long id)
        {
            var dictionary = CodDictionaryService.GetDictionary(id);

            var model = CodDictionaryUpdatingModel.ToModel(dictionary);

			return View(model);
        }

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
		public ActionResult CodJobObjectCard(CodDictionaryUpdatingModel viewModel)
		{
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var dto = viewModel.ToDto();
            CodDictionaryService.UpdateCodDictionary(dto);

			return Ok();
		}

		//TODO KOMO-7
		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD_JOB_DELETE)]
		public IActionResult DeleteCodJob(long codJobId)
		{
			var codJob = OMCodJob.Where(x => x.Id == codJobId).SelectAll().ExecuteFirstOrDefault();
			if (codJob == null)
			{
				throw new Exception($"Задание ЦОД с ИД {codJobId} не найдено");
			}

			return View(CodDictionaryUpdatingModel.ToModel(codJob));
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
