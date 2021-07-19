using System;
using System.Linq;
using CommonSdks;
using Core.Register;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.GbuObject;
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

		public GbuCodController(ICodDictionaryService codDictionaryService, IRegisterCacheWrapper registerCacheWrapper,
			IGbuObjectService gbuObjectService)
			: base(gbuObjectService, registerCacheWrapper)
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

            var model = CodDictionaryUpdatingModel.ToModel(dictionary, CodDictionaryService);

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

            var model = CodDictionaryValueModel.ToModel(dictionary, CodDictionaryService);

            return View("EditDictionaryValue", model);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult EditDictionaryValue(long dictionaryId, long dictionaryValueId)
        {
            var dictionary = CodDictionaryService.GetDictionary(dictionaryId);
            var value = CodDictionaryService.GetDictionaryValue(dictionary, dictionaryValueId);

            var model = CodDictionaryValueModel.ToModel(dictionary, CodDictionaryService, value);

            return View("EditDictionaryValue", model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_MODELS)]
        public ActionResult EditDictionaryValue(CodDictionaryValueModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            CodDictionaryService.EditDictionaryValue(model.DictionaryId, model.ToDto());

            return Ok();
        }

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
		public IActionResult DeleteDictionaryValue(long dictionaryId, long dictionaryValueId)
        {
            var dictionary = CodDictionaryService.GetDictionary(dictionaryId);
            var dictionaryValue = CodDictionaryService.GetDictionaryValue(dictionary, dictionaryValueId);

            ViewBag.RegisterId = dictionary.RegisterId;
            ViewBag.DictionaryValueId = dictionaryValueId;
            ViewBag.DictionaryValue = dictionaryValue.ToString();

            return View();
		}

		[HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.GBU_COD)]
		public IActionResult DoDeleteDictionaryValue(long registerId, long dictionaryValueId)
        {
            RegisterStorage.Destroy((int) registerId, (int) dictionaryValueId);

            return Ok();
		}

        #endregion
	}
}
