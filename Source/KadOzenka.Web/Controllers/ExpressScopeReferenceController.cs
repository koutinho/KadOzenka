using System;
using System.IO;
using System.Linq;
using Core.ErrorManagment;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Dal.LongProcess.ExpressScore;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.ExpressScoreReference;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.ES;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

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
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_REFERENCES)]
        public ActionResult ReferenceCard(long id, bool showItems = false)
        {
            var entity = OMEsReference.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            var isReferenceEmpty = entity == null || !OMEsReferenceItem.Where(x => x.ReferenceId == id).ExecuteExists();
            
            return View(ReferenceViewModel.FromEntity(entity, isReferenceEmpty, showItems));
        }

        [HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_REFERENCES_EDIT)]
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
                    id = ReferenceService.CreateReference(viewModel.Name, viewModel.ValueType);
                }
                else
                {
                    ReferenceService.UpdateReference(viewModel.Id, viewModel.Name, viewModel.ValueType);
                }
            }
            catch (Exception e)
            {
                return SendErrorMessage(e.Message);
            }

            return Json(new { Success = "Сохранено успешно", Id = id });
        }

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_REFERENCES_DELETE)]
        public IActionResult DeleteReference(long id)
        {
            var entity = OMEsReference.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            return View(ReferenceViewModel.FromEntity(entity));
        }

        [HttpPost]
        [ActionName("DeleteReference")]
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_REFERENCES_DELETE)]
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
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_REFERENCES)]
        public ActionResult ReferenceItemCard(long id, long referenceId)
        {
            var entity = OMEsReferenceItem.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            referenceId = entity != null ? entity.ReferenceId : referenceId;
            var referenceEntity = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault();

            return View(ReferenceItemViewModel.ToModel(entity, referenceEntity));
        }

        [HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_REFERENCES_EDIT)]
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
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_REFERENCES)]
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
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_REFERENCES_EDIT)]
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

        [HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_REFERENCES_IMPORT)]
        public IActionResult DataImport()
        {
            ViewData["References"] = OMEsReference.Where(x => x).SelectAll().Execute().Select(x => new
            {
                Text = x.Name,
                Value = x.Id,
            }).ToList();

            return View(new ImportDataViewModel());
        }

        [HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_REFERENCES_IMPORT)]
        public IActionResult DataImport(IFormFile file, ImportDataViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GenerateMessageNonValidModel();
            }

            long? referenceId = null;
            object returnedData;
            try
            {
	            using (Stream fileStream = file.OpenReadStream())
	            {

		            var importInfo = new ImportReferenceFileInfoDto
		            {
			            FileName = file.FileName,
			            ValueColumnName = viewModel.Value,
			            CalcValueColumnName = viewModel.CalcValue,
			            ValueType = viewModel.ValueType
		            };

		            if (ReferenceService.UseLongProcess(fileStream))
		            {
			            fileStream.Seek(0, SeekOrigin.Begin);
			            EsUnloadReferenceFromExcel.AddProcessToQueue(fileStream, new ImportFileFromExcelDto
			            {
				            DeleteOldValues = viewModel.Reference.DeleteOldValues,
				            FileInfo = importInfo,
				            IdReference = viewModel.Reference.IdReference.GetValueOrDefault(),
				            IsNewReference = viewModel.Reference.IsNewReference,
				            NewReferenceName = viewModel.Reference.NewReferenceName
			            });

			            returnedData = new
			            {
				            Success = true,
				            message =
					            "Добавление справочника было поставленно в очередь долгих процессов. После добавления вы получите уведомление.",
				            isLongProcess = true
			            };
		            }
		            else
		            {
			            fileStream.Seek(0, SeekOrigin.Begin);

			            if (viewModel.Reference.IsNewReference)
			            {
				            referenceId = ReferenceService.CreateReferenceFromExcel(fileStream, importInfo,
					            viewModel.Reference.NewReferenceName);

			            }
			            else
			            {
				            ReferenceService.UpdateReferenceFromExcel(fileStream, importInfo,
					            viewModel.Reference.IdReference.Value, viewModel.Reference.DeleteOldValues);
			            }

			            returnedData = new
			            {
				            Success = true,
				            message = "Справочник успешно импортирован",
				            idNewReference = viewModel.Reference.IsNewReference ? referenceId : null,
			            };
                    }

	            }
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return SendErrorMessage(ex.Message);
            }

            return Json(returnedData);
        }
    }
}
