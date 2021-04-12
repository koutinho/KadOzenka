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
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult AddDictionaryValue(long dictionaryId)
        {
            var dictionary = CodDictionaryService.GetDictionary(dictionaryId);

            var model = CodDictionaryValueModel.ToModel(dictionary);

            return View("EditDictionaryValue", model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult EditDictionaryValue(CodDictionaryValueModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            if (model.Id == -1)
            {
				CodDictionaryService.AddDictionaryValue(model.JobId, model.ToDto());
            }

            return Ok();
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
