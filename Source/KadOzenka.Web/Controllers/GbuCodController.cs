using System;
using System.Linq;
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


		#region Словарь

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
		public ActionResult AddDictionary()
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
        public ActionResult DictionaryCard(long id)
        {
            var dictionary = CodDictionaryService.GetDictionary(id);

            var model = CodDictionaryUpdatingModel.ToModel(dictionary);

			return View(model);
        }

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
		public ActionResult DictionaryCard(CodDictionaryUpdatingModel viewModel)
		{
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var dto = viewModel.ToDto();
            CodDictionaryService.UpdateCodDictionary(dto);

			return Ok();
		}

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD_JOB_DELETE)]
		public IActionResult DeleteDictionary(long dictionaryId)
        {
			var codJob = CodDictionaryService.GetDictionary(dictionaryId);

            ViewBag.DictionaryId = codJob.Id;
            ViewBag.DictionaryName = codJob.NameJob;

            return View();
		}

		[HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD_JOB_DELETE)]
		public IActionResult DoDeleteDictionary(long dictionaryId)
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

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult EditDictionaryValue(long dictionaryId, long dictionaryValueId)
        {
            var dictionary = CodDictionaryService.GetDictionary(dictionaryId);
            var value = CodDictionaryService.GetDictionaryValues(dictionary.RegisterId)
                .FirstOrDefault(x => x.Id == dictionaryValueId);
            if (value == null)
                throw new Exception($"Не найдено значение словаря '{dictionary.NameJob}' с ИД {dictionaryValueId}");

            var model = CodDictionaryValueModel.ToModel(dictionary, value);

            return View("EditDictionaryValue", model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult EditDictionaryValue(CodDictionaryValueModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            CodDictionaryService.UpdateDictionaryValue(model.DictionaryId, model.ToDto());

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

		#endregion
    }
}
