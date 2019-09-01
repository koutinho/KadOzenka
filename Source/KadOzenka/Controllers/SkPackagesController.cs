using CIPJS.DAL.FileStorage;
using CIPJS.DAL.SK;
using CIPJS.Models.MfcUpload;
using Core.FastDBF;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Models.CoreUi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CIPJS.Controllers
{
    public class SkPackagesController : BaseController
    {
        private readonly FileStorageService _fileStorageService;

        public SkPackagesController(FileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public IActionResult Load()
        {
            return View();
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Load(DateTime period, List<IFormFile> files)
        {
            if (files == null || !files.Any()) return ErrorResponse("На сервер не передано ни одного файла");
            if (files.GroupBy(x => x.FileName).Count() != files.Count) return ErrorResponse("На сервер переданы дублирующие файлы");

            period = new DateTime(period.Year, period.Month, 1);

            List<string> errorMessages = new List<string>();
            List<string> fileNames = files.Select(x => x.FileName.ToUpper()).ToList();

            List<OMInputFile> omInputFiles = OMInputFile.Where(x => fileNames.Contains(x.FileName.ToUpper()) && x.PeriodRegDate == period)
                .SelectAll(false).Execute();

            foreach (IFormFile file in files)
            {
                int rowCount = GetRowCountFromDbf(file.OpenReadStream());
                OMInputFile omInputFile = omInputFiles.FirstOrDefault(x => x.FileName.ToUpper() == file.FileName.ToUpper()
                    && x.CountStr == rowCount
                    && (x.Status_Code == UFKFileProcessingStatus.Loaded || x.Status_Code != UFKFileProcessingStatus.ProcessedCompletely)
                    && x.TypeSource_Code == InsuranceSourceType.Sk);

                if (omInputFile != null) errorMessages.Add($"Ранее файл \"{file.FileName}\" был загружен");
                if (!ValidateFile(file.OpenReadStream(), file.FileName.ToLower(), out string message)) errorMessages.Add(message);
            }

            if (errorMessages.Any()) return ErrorResponse(errorMessages);

            foreach (IFormFile file in files)
            {
                using (Stream stream = file.OpenReadStream())
                {
                    long fileId = _fileStorageService.Save(stream, file.FileName, periodRegDate: period, checkExistsFile: true);

                    //new DAL.SK.SkImportLoadProcess().StartProcess(null, new ObjectModel.Core.LongProcess.OMQueue { ObjectId = fileId }, new System.Threading.CancellationToken());
                    LongProcessManager.AddTaskToQueue("SkImportLoadProcess", OMFileStorage.GetRegisterId(), fileId);
                }
            }

            return EmptyResponse();
        }

        public IActionResult Process(long id)
        {
            List<long> ids = RegistersVariables.CurrentList?.ToList() ?? new List<long>();

            if (!ids.Any()) ids.Add(id);

            Dictionary<long, string> idNames = OMInputFile.Where(x => ids.Contains(x.EmpId)).Select(x => x.FileName).Execute()
                .ToDictionary(x => x.EmpId, x => x.FileName);

            return View(idNames);
        }

        [HttpPost]
        public IActionResult Process([FromBody]Dictionary<long, string> models)
        {
            List<long> ids = models.Select(x => x.Key).ToList();

            if (!ids.Any()) return ErrorResponse("На сервер пришла пустая модель");

            foreach (long id in ids)
            {
                //new DAL.SK.SkProcessLoadProcess().StartProcess(null, new ObjectModel.Core.LongProcess.OMQueue { ObjectId = id }, new System.Threading.CancellationToken());
                LongProcessManager.AddTaskToQueue("SkProcessLoadProcess", OMInputFile.GetRegisterId(), id);
            }

            return EmptyResponse();
        }

        public IActionResult Transition(long id)
        {
            OMInputFile omInputFile = OMInputFile.Where(x => x.EmpId == id).Select(x => x.TypeFile_Code).Execute().FirstOrDefault();

            if (omInputFile == null) throw new Exception($"Запись о файле не найдена (ИД={id})");

            string url = string.Empty;

            if (omInputFile.TypeFile_Code == TypeFile.Policy) url = $"/RegistersView/SkPolicySvdPolis?Transition=1&309000400={id}";
            else if (omInputFile.TypeFile_Code == TypeFile.TerminatedPolicy) url = $"/RegistersView/SkPolicySvdPolis?Transition=1&309000400={id}";
            else if (omInputFile.TypeFile_Code == TypeFile.Certificate) url = $"/RegistersView/SkPolicySvdCertificate?Transition=1&309000400={id}";
            else if (omInputFile.TypeFile_Code == TypeFile.InsurancePayments) url = $"/RegistersView/InsurancePaymentsTo?Transition=1&314002400={id}";
            else if (omInputFile.TypeFile_Code == TypeFile.InsurancePaymentsRefusal) url = $"/RegistersView/InsuranceNoPayments?Transition=1&315003200={id}";
            else if (omInputFile.TypeFile_Code == TypeFile.InsuranceContractConcluded) url = $"/RegistersView/Contracts?Transition=1&310002100={id}";
            //else if (omInputFile.TypeFile_Code == TypeFile.AddInsuranceContractConcluded) url = $"todo"; // 311001800
            else if (omInputFile.TypeFile_Code == TypeFile.PaymentReceived) url = $"/RegistersView/InputPlatOI?Transition=1&306000200={id}";
            else if (omInputFile.TypeFile_Code == TypeFile.InsurancePaymentsUnderContracts) url = $"/RegistersView/InsurancePaymentsTo?Transition=1&314002400={id}";
            else if (omInputFile.TypeFile_Code == TypeFile.DeclaredUnsettledInsuranceEvents) url = $"/RegistersView/Refusals?Transition=1&315003200={id}";

            if (url.IsNullOrEmpty()) return new EmptyResult();

            return Redirect(Path.Combine(HttpContextHelper.WebRootPath, url));
        }

        public IActionResult Delete(long id)
        {
            List<long> ids = RegistersVariables.CurrentList?.ToList() ?? new List<long>();

            if (!ids.Any()) ids.Add(id);

            List<OMInputFile> files = OMInputFile
                .Where(x => ids.Contains(x.EmpId))
                .Select(x => x.FileName)
                .Select(x => x.TypeFile_Code)
                .Execute();
            Dictionary<long, string> idNames = files.ToDictionary(x => x.EmpId, x => x.FileName);

            if (files.Any(x => x.TypeFile_Code == TypeFile.InsuranceContractConcluded))
            {
                string filesList = string.Join(',', files.Select(x => x.EmpId));
                string qry =
                    $@"select c.contract_id
                    from insur_input_file f
                    join insur_all_property p on p.link_id_file=f.emp_id
                    join insur_param_calculation c on c.contract_id=p.emp_id
                    where f.emp_id in ({ filesList })
                    limit 1";
                ViewBag.ConfirmParamCalcLinks = DBMngr.Realty.ExecuteScalar(CommandType.Text, qry) != null;
            }
            else
            {
                ViewBag.ConfirmParamCalcLinks = false;
            }

            return View(idNames);
        }

        [HttpPost]
        public IActionResult Delete([FromBody]Dictionary<long, string> models)
        {
            List<long> ids = models.Select(x => x.Key).ToList();

            if (!ids.Any()) return ErrorResponse("На сервер пришла пустая модель");

            List<OMInputFile> omInputFiles = OMInputFile.Where(x => ids.Contains(x.EmpId))
                .Select(x => x.TypeFile_Code)
                .Select(x => x.TypeFile)
                .Select(x => x.FileName)
                .Select(x => x.ParentFileStorage.Id)
                .Execute();
            List<string> errorMessages = new List<string>();

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                foreach (OMInputFile omInputFile in omInputFiles)
                {
                    omInputFile.ParentFileStorage?.Destroy();

                    string commandText = string.Empty;

                    if (omInputFile.TypeFile_Code == TypeFile.AddInsuranceContractConcluded)
                    {
                        commandText = $"DELETE FROM INSUR_DOP_ALL_PROPERTY WHERE LINK_ID_FILE={omInputFile.EmpId}";
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.Certificate || omInputFile.TypeFile_Code == TypeFile.Policy)
                    {
                        commandText = $"DELETE FROM INSUR_POLICY_SVD WHERE LINK_ID_FILE={omInputFile.EmpId}";
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.DeclaredUnsettledInsuranceEvents || omInputFile.TypeFile_Code == TypeFile.InsurancePaymentsRefusal)
                    {
                        commandText = $"DELETE FROM INSUR_NO_PAY WHERE LINK_ID_FILE={omInputFile.EmpId}";
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.InsuranceContractConcluded)
                    {
                        string commandTextLinks = $"UPDATE INSUR_INPUT_PLAT SET LINK_ALL_PROPERTY_ID=NULL WHERE LINK_ALL_PROPERTY_ID IN (SELECT EMP_ID FROM INSUR_ALL_PROPERTY WHERE LINK_ID_FILE={omInputFile.EmpId})";
                        DbCommand commandLinks = DBMngr.Realty.GetSqlStringCommand(commandTextLinks);
                        DBMngr.Realty.ExecuteNonQuery(commandLinks);

                        string commandTextParamCalcContract = $"UPDATE insur_param_calculation SET contract_id=NULL WHERE contract_id IN (select emp_id from insur_all_property where link_id_file={omInputFile.EmpId})";
                        DbCommand commandParamCalcContract = DBMngr.Realty.GetSqlStringCommand(commandTextParamCalcContract);
                        DBMngr.Realty.ExecuteNonQuery(commandParamCalcContract);

                        commandText = $"DELETE FROM INSUR_ALL_PROPERTY WHERE LINK_ID_FILE={omInputFile.EmpId}";
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.InsurancePayments || omInputFile.TypeFile_Code == TypeFile.InsurancePaymentsUnderContracts)
                    {
                        commandText = $"DELETE FROM INSUR_PAY_TO WHERE LINK_ID_FILE={omInputFile.EmpId}";
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.PaymentReceived)
                    {
                        commandText = $"DELETE FROM INSUR_INPUT_PLAT WHERE LINK_ID_FILE={omInputFile.EmpId}";
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.TerminatedPolicy)
                    {
                        // todo
                    }

                    if (commandText.IsNullOrEmpty())
                    {
                        errorMessages.Add($"Файл {omInputFile.FileName} не удален. Для типа файла \"{omInputFile.TypeFile}\" не реализована логика удаления");
                        continue;
                    }

                    DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
                    DBMngr.Realty.ExecuteNonQuery(command);
                    omInputFile.Destroy();
                }

                ts.Complete();
            }

            if (errorMessages.Any()) return ErrorResponse(errorMessages);

            return EmptyResponse();
        }

        public IActionResult Status(long id)
        {
            OMQueue omQueue = OMQueue.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();

            if (omQueue == null) throw new Exception("Лог не найден");

            decimal.TryParse(omQueue.Message, out decimal percent);
            string status = "";

            if (omQueue.Status == 0) { status = "Добавлен"; percent = -1; }
            else if (omQueue.Status == 1) { status = "Подготовлен к запуску"; percent = -1; }
            else if (omQueue.Status == 2) status = "Выполняется";
            else if (omQueue.Status == 3) { status = "Завершен успешно"; percent = 100; }
            else if (omQueue.Status == 4) { status = "Завершен с ошибкой"; percent = -1; }
            else if (omQueue.Status == 5) { status = "Отправлен запрос на остановку"; percent = -1; }
            else if (omQueue.Status == 6) { status = "Остановлен"; percent = -1; }

            LogFileDto result = new LogFileDto
            {
                Id = id,
                Percent = percent,
                Status = status,
                TraceData = omQueue.Message
            };

            return View(result);
        }

        public IActionResult UpdateStatus(long id)
        {
            OMQueue omQueue = OMQueue.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();

            if (omQueue == null) return null;

            decimal.TryParse(omQueue.Message, out decimal percent);
            string status = "Добавлен";

            if (omQueue.Status == 1) status = "Подготовлен к запуску";
            else if (omQueue.Status == 2) status = "Выполняется";
            else if (omQueue.Status == 3) { status = "Завершен успешно"; percent = 100; }
            else if (omQueue.Status == 4) status = "Завершен с ошибкой";
            else if (omQueue.Status == 5) status = "Отправлен запрос на остановку";
            else if (omQueue.Status == 6) status = "Остановлен";

            return JsonResponse(new LogFileDto
            {
                Percent = percent,
                Status = status,
                TraceData = omQueue.Message
            });
        }

        private int GetRowCountFromDbf(Stream fileStream)
        {
            DbfFile dbfFile = new DbfFile(Encoding.GetEncoding(new CultureInfo("ru-RU").TextInfo.OEMCodePage));

            dbfFile.Open(fileStream);

            int result = 0;

            while (dbfFile.ReadNext() != null) { result++; }

            return result;
        }

        private bool ValidateFile(Stream fileStream, string fileName, out string message)
        {
            SkImportLoadProcess skImportLoadProcess = new SkImportLoadProcess();

            try
            {
                if (fileName == "policy.dbf")
                {
                    skImportLoadProcess.ReadPolicyFile(fileStream, true);
                }
                else if (fileName == "policy_d.dbf")
                {
                    skImportLoadProcess.ReadPolicyDFile(fileStream, true);
                }
                else if (fileName == "svd.dbf")
                {
                    skImportLoadProcess.ReadSvdFile(fileStream, true);
                }
                else if (fileName == "pl.dbf")
                {
                    skImportLoadProcess.ReadPlFile(fileStream, true);
                }
                else if (fileName == "pl_no.dbf")
                {
                    skImportLoadProcess.ReadPlNoFile(fileStream, true);
                }
                else if (fileName == "comm.dbf")
                {
                    skImportLoadProcess.ReadCommFile(fileStream, true);
                }
                else if (fileName == "comm_dop.dbf")
                {
                    skImportLoadProcess.ReadCommDopFile(fileStream, true);
                }
                else if (fileName == "pay_comm.dbf")
                {
                    skImportLoadProcess.ReadPayCommFile(fileStream, true);
                }
                else if (fileName == "pl_comm.dbf")
                {
                    skImportLoadProcess.ReadPlCommFile(fileStream, true);
                }
                else if (fileName == "pl_comm_no.dbf")
                {
                    skImportLoadProcess.ReadPlCommNoFile(fileStream, true);
                }
                else
                {
                    throw new Exception($"Для файла {fileName} нет обработчика");
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }

            message = string.Empty;
            return true;
        }

        [HttpGet]
        [ActionName("RestartUpload")]
        public ActionResult RestartUploadConfirm(long? queueId)
        {
            ModalDialogDetails model = new ModalDialogDetails();
            OMQueue omQueue = null;
            if (queueId.HasValue)
            {
                omQueue = OMQueue.Where(x => x.Id == queueId).SelectAll().ExecuteFirstOrDefault();
            }

            if (omQueue != null && omQueue.ObjectId.HasValue)
            {
                model.Message = "Вы действительно хотите перезапустить процесс загрузки?";
            }
            else
            {
                model.Message = "Не удалось определить файл загрузки";
                model.Icon = ModalDialogDetails.IconType.Warning;
                model.Buttons = ModalDialogDetails.ButtonType.Ok;
            }

            return View("~/Views/Shared/ModalDialogDetails.cshtml", model);
        }

        [HttpPost]
        public ActionResult RestartUpload(long? queueId)
        {
            if (queueId.HasValue)
            {
                try
                {
                    OMQueue queue = OMQueue.Where(x => x.Id == queueId).SelectAll().ExecuteFirstOrDefault();

                    if (queue == null || !queue.ObjectId.HasValue)
                    {
                        throw new Exception("Не удалось определить файл загрузки");
                    }

                    if (queue.Status == (long)OMQueueStatus.Running)
                    {
                        throw new Exception("Невозможно перезапустить процесс, т.к. он находиться в статусе \"Выполняется\"");
                    }

                    //запускаем новый процесс
                    LongProcessManager.AddTaskToQueue("SkImportLoadProcess", OMFileStorage.GetRegisterId(), queue.ObjectId);
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
                    message = "Не удалось определить журнал файл загрузки"
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