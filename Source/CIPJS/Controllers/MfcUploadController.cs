using CIPJS.DAL.FileStorage;
using CIPJS.DAL.Mfc.Upload;
using CIPJS.Models.MfcUpload;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.Models.CoreUi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Transactions;

namespace CIPJS.Controllers
{
    public class MfcUploadController : BaseController
    {
        private readonly MfcUploadService _mfcUploadService;
        private readonly FileStorageService _fileStorageService;

        public MfcUploadController(MfcUploadService mfcUploadService, FileStorageService fileStorageService)
        {
            _mfcUploadService = mfcUploadService;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ContentResult Upload(DateTime period, List<IFormFile> files)
        {
            try
            {
                DateTime periodRegDate = new DateTime(period.Year, period.Month, 1);

                if (files == null || files.Count == 0)
                {
                    return EmptyResponse();
                }

                if (files.GroupBy(g => g.FileName).Where(g => g.Skip(1).Any()).Any())
                {
                    throw new Exception("Обнаружены дубликаты файлов");
                }

                long fileId;

                if (files.Count == 1)
                {
                    IFormFile file = files.FirstOrDefault();
                    using (Stream stream = file.OpenReadStream())
                    {
                        fileId = _fileStorageService.Save(stream, file.FileName, periodRegDate: periodRegDate);
                    }
                }
                else
                {
                    fileId = _fileStorageService.CreateVirtualDirectory(periodRegDate);
                    foreach(IFormFile file in files)
                    {
                        using (Stream stream = file.OpenReadStream())
                        {
                            _fileStorageService.Save(stream, file.FileName, fileId, periodRegDate);
                        }
                    }
                }

                OMLogFile logFile = new OMLogFile();
                logFile.FileStorageId = fileId;
                logFile.Status_Code = MfcUploadFileStatus.Prepare;
                logFile.Loaddate = DateTime.Now;
                logFile.GeneralStatus_Code = MfcGeneralUploadStatus.Loading;
                logFile.Save();

                LongProcessManager.AddTaskToQueue("MfcUploadLoadProcess", OMLogFile.GetRegisterId(), logFile.EmpId);
            }
            catch(Exception ex)
            {
                return ErrorResponse(ex.Message);
            }

            return EmptyResponse();
        }

        [HttpGet]
        public ActionResult Delete(long okrugCode)
        {
            return View(_mfcUploadService.GetLastOkrugPeriod(okrugCode));
        }

        [HttpPost]
        public ContentResult Delete(long okrugCode, DateTime periodRegDate)
        {
            try
            {
                _mfcUploadService.Delete(okrugCode, periodRegDate);

                return EmptyResponse();
            }
            catch(ValidationException ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        public ActionResult LogFile(long logFileId)
        {
            OMLogFile logFile = OMLogFile.Where(x => x.EmpId == logFileId)
                    .SelectAll()
                    .Execute()
                    .FirstOrDefault();

            if (logFile == null)
            {
                throw new Exception("Не удалось определить лог загрузки");
            }

            return View(new LogFileDto
            {
                Id = logFile.EmpId,
                Status = logFile.Status,
                Percent = logFile.Status_Code.GetEnumCode().ParseToDecimal(),
                TraceData = logFile.Tracedata
            });
        }

        public ContentResult GetLogFileStatus(long logFileId)
        {
            try
            {
                OMLogFile logFile = OMLogFile.Where(x => x.EmpId == logFileId)
                    .Select(x => x.Status)
                    .Select(x => x.Status_Code)
                    .Select(x => x.Tracedata)
                    .Execute()
                    .FirstOrDefault();

                if (logFile == null)
                {
                    throw new Exception("Не удалось определить журнал загрузки МФЦ");
                }

                return JsonResponse(new
                {
                    Status = logFile.Status_Code.GetEnumDescription(),
                    Percent = logFile.Status_Code.GetEnumCode().ParseToDecimal(),
                    TraceData = logFile.Tracedata
                });
            }
            catch(Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("RestartUpload")]
        public ActionResult RestartUploadConfirm(long? logFileId)
        {
            ModalDialogDetails model = new ModalDialogDetails();
            OMLogFile logFile = null;
            if (logFileId.HasValue)
            {
                logFile = OMLogFile.Where(l => l.EmpId == logFileId).SelectAll().Execute().FirstOrDefault();
            }

            if (logFile != null)
            {
                model.Message = "Вы действительно хотите перезапустить процесс загрузки?";
            }
            else
            {
                model.Message = "Не удалось определить журнал загрузки МФЦ";
                model.Icon = ModalDialogDetails.IconType.Warning;
                model.Buttons = ModalDialogDetails.ButtonType.Ok;
            }

            return View("~/Views/Shared/ModalDialogDetails.cshtml", model);
        }

        [HttpPost]
        public ActionResult RestartUpload(long? logFileId)
        {
            if (logFileId.HasValue)
            {
                try
                {
                    OMLogFile logFile = OMLogFile.Where(x => x.EmpId == logFileId)
                        .Select(x => x.Status)
                        .Select(x => x.Status_Code)
                        .Select(x => x.Loaddate)
                        .Select(x => x.GeneralStatus)
                        .Select(x => x.GeneralStatus_Code)
                        .Select(x => x.Tracedata)
                        .ExecuteFirstOrDefault();

                    if (logFile == null)
                    {
                        throw new Exception("Не удалось определить журнал загрузки МФЦ");
                    }

                    using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                    {
                        logFile.Status_Code = MfcUploadFileStatus.Prepare;
                        logFile.Loaddate = DateTime.Now;
                        logFile.GeneralStatus_Code = MfcGeneralUploadStatus.Loading;
                        logFile.Save();

                        //запускаем новый процесс
                        LongProcessManager.AddTaskToQueue("MfcUploadLoadProcess", OMLogFile.GetRegisterId(), logFileId.Value);

                        ts.Complete();
                    }
                }
                catch (Exception e)
                {
                    return Json(new
                    {
                        type = "Error",
                        message = e.Message
                    });
                }
            }
            else
            {
                return Json(new
                {
                    type = "Error",
                    message = "Не удалось определить журнал загрузки МФЦ"
                });
            }

            return Json(new
            {
                type = "Success",
                message = "Процесс успешно перезапущен",
                reload = true
            });
        }
    }
}
