using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register.Enums;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Web.Models.ExpressScoreReference;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.ES;

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
            var isReferenceEmpty = entity == null || !OMEsReferenceItem.Where(x => x.ReferenceId == id).ExecuteExists();

            return View(ReferenceViewModel.FromEntity(entity, isReferenceEmpty, showItems));
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

        [HttpGet]
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
        public IActionResult DataImport(IFormFile file, ImportDataViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GenerateMessageNonValidModel();
            }

            long? referenceId = null;
            try
            {
                using (Stream fileStream = file.OpenReadStream())
                {
                    var importInfo = new ImportReferenceFileInfoDto
                    {
                        FileName = Path.GetFileNameWithoutExtension(file.FileName),
                        ValueColumnName = viewModel.Value,
                        CalcValueColumnName = viewModel.CalcValue,
                        ValueType = viewModel.ValueType
                    };

                    if (viewModel.Reference.IsNewReference)
                    {
                        referenceId = ReferenceService.CreateReferenceFromExcel(fileStream, importInfo, viewModel.Reference.NewReferenceName);
                       
                    }
                    else
                    {
                        ReferenceService.UpdateReferenceFromExcel(fileStream, importInfo, viewModel.Reference.IdReference.Value, viewModel.Reference.DeleteOldValues);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return SendErrorMessage(ex.Message);
            }

            return Json(new { Success = true, idNewReference = viewModel.Reference.IsNewReference ? referenceId : null });
        }

        [HttpGet]
        public FileContentResult DownloadImportedFile(string fileName, string dateCreatedString, bool downloadResult)
        {
            var dateCreated = DateTime.ParseExact(dateCreatedString, ExpressScoreReferenceService.DateCreatedStringFormat, CultureInfo.CurrentCulture);
            var templateFile = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, dateCreated,
                fileName);
            var bytes = new byte[templateFile.Length];
            templateFile.Read(bytes);
            StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out string fileExtension, out string contentType);

            return File(bytes, contentType, $"{fileName}.{fileExtension}");
        }
    }
}
