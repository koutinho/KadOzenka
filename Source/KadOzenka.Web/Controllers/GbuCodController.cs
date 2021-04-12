using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.Registers.Entities;
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


		#region Словарь

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

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD_JOB_DELETE)]
		public IActionResult DeleteCodJob(long dictionaryId)
        {
			var codJob = CodDictionaryService.GetDictionary(dictionaryId);

            ViewBag.DictionaryId = codJob.Id;
            ViewBag.DictionaryName = codJob.NameJob;

            return View();
		}

		[HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD_JOB_DELETE)]
		public IActionResult DeleteDictionary(long dictionaryId)
		{
            CodDictionaryService.DeleteDictionary(dictionaryId);

            return Ok();
		}

		#endregion


		#region Значения словаря

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
        public JsonResult GetDictionaryValues(long dictionaryId)
        {
            var dictionary = CodDictionaryService.GetDictionary(dictionaryId);

            var dictionaryValues = CodDictionaryService.GetDictionaryValues(dictionary.RegisterId);

            return Json(dictionaryValues);
        }

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

			var model = CodDictionaryValueModel.FromEntity(codDictionary);
			model.JobId = codJob.Id;

			return View(model);
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD_JOB_ADD)]
		public ActionResult CodDictionaryObjectCard(CodDictionaryValueModel valueModel)
		{
			var codDictionary = OMCodDictionary.Where(x => x.Id == valueModel.Id).SelectAll().ExecuteFirstOrDefault();

			if (valueModel.Id != -1 && codDictionary == null)
			{
				return NotFound();
			}
			if (codDictionary == null)
			{
				codDictionary = new OMCodDictionary();
			}

			CodDictionaryValueModel.ToEntity(valueModel, ref codDictionary);
            long id;
			try
			{
				id = codDictionary.Save();
			}
			catch (Exception e)
			{
			    return SendErrorMessage(e.Message);
			}
			valueModel.Id = id;
			return Json(new { Success = "Сохранено успешно", data = valueModel });
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

			return View(CodDictionaryValueModel.FromEntity(codDictionary));
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
		public IActionResult DeleteCodDictionary(CodDictionaryValueModel valueModel)
		{
			var codDictionary = OMCodDictionary.Where(x => x.Id == valueModel.Id).SelectAll().ExecuteFirstOrDefault();
			if (codDictionary == null)
			{
				throw new Exception($"Справочник ЦОД с ИД {valueModel.Id} не найден");
			}

			codDictionary.Destroy();

			return EmptyResponse();
		}

		#endregion CodDictionary
    }
}
