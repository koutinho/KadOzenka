using System;
using System.Globalization;
using System.Linq;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Web.Models.ExpressScoreReference;
using KadOzenka.Web.Models.GbuCod;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.ES;
using ObjectModel.KO;

namespace KadOzenka.Web.Controllers
{
    public class ExpressScopeReferenceController : KoBaseController
    {
        public ExpressScoreReferenceService ReferenceService { get; set; }

        public ExpressScopeReferenceController(ExpressScoreReferenceService service)
        {
            ReferenceService = service;
        }

        [HttpGet]
        public ActionResult ReferenceCard(long id, bool showItems = false)
        {
            var entity = OMEsReference.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();

            return View(ReferenceViewModel.FromEntity(entity, showItems));
        }

        [HttpPost]
        public ActionResult ReferenceCard(ReferenceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GenerateMessageNonValidModel();
            }

            var id = viewModel.Id;
            try
            {
                if (id == -1)
                {
                    id = ReferenceService.CreateReference(viewModel.Name);
                }
                else
                {
                    ReferenceService.UpdateReference(viewModel.Id, viewModel.Name);
                }
            }
            catch (Exception e)
            {
                return SendErrorMessage(e.Message);
            }

            return Json(new { Success = "Сохранено успешно", Id = id });
        }

        [HttpGet]
        public IActionResult DeleteReference(long id)
        {
            var entity = OMEsReference.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            return View(ReferenceViewModel.FromEntity(entity));
        }

        [HttpPost]
        [ActionName("DeleteReference")]
        public IActionResult DeleteReferenceAction(long id)
        {
            try
            {
                ReferenceService.DeleteReference(id);
            }
            catch (Exception ex)
            {
                return SendErrorMessage(ex.Message);
            }

            return Json(new { Success = true });
        }

        [HttpGet]
        public ActionResult ReferenceItemCard(long id, long referenceId)
        {
            var entity = OMEsReferenceItem.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            referenceId = entity != null ? entity.ReferenceId : referenceId;
            var referenceEntity = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault();

            return View(ReferenceItemViewModel.ToModel(entity, referenceEntity));
        }

        [HttpPost]
        public ActionResult ReferenceItemCard(ReferenceItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GenerateMessageNonValidModel();
            }

            var id = viewModel.Id;
            try
            {
                if (id == -1)
                {
                    id = ReferenceService.CreateReferenceItem(viewModel.ToDto());
                }
                else
                {
                    ReferenceService.UpdateReferenceItem(viewModel.ToDto());
                }
            }
            catch (Exception e)
            {
                return SendErrorMessage(e.Message);
            }

            return Json(new { Success = "Сохранено успешно", Id = id });
        }

        [HttpGet]
        public IActionResult DeleteReferenceItem(long id)
        {
            var entity = OMEsReferenceItem.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            var referenceEntity = entity != null
                ? OMEsReference.Where(x => x.Id == entity.ReferenceId).SelectAll().ExecuteFirstOrDefault()
                : null;

            return View(ReferenceItemViewModel.ToModel(entity, referenceEntity));
        }

        [HttpPost]
        [ActionName("DeleteReferenceItem")]
        public IActionResult DeleteReferenceItemAction(long id)
        {
            try
            {
                ReferenceService.DeleteReferenceItem(id);
            }
            catch (Exception ex)
            {
                return SendErrorMessage(ex.Message);
            }

            return Json(new { Success = true });
        }
    }
}
