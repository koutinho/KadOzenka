using CIPJS.DAL.InputFile;
using CIPJS.DAL.InputFilePackage;
using CIPJS.DAL.Mfc;
using CIPJS.Models.InputFilePackage;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CIPJS.Controllers
{
    public class InputPackageFileController : BaseController
    {
        private readonly InputFileService _inputFileService;

        public InputPackageFileController(InputFileService inputFileService)
        {
            _inputFileService = inputFileService;
        }

        public ViewResult Statistics(long inputPackageFileId)
        {
            OMInputFilePackage filePackage = OMInputFilePackage
                .Where(x => x.Id == inputPackageFileId)
                .Select(x => x.OkrugId)
                .Select(x => x.ParentOkrug.Name)
                .Select(x => x.ParentOkrug.ShortName)
                .Select(x => x.PeriodRegDate)
                .Execute()
                .FirstOrDefault();

            if (filePackage == null)
            {
                throw new Exception($"Не удалось определить пакет загрузки с идентификатором: {inputPackageFileId}");
            }

            return View(new InputFilePackageDto
            {
                Id = filePackage.Id,
                OkrugId = filePackage.OkrugId,
                OkrugName = filePackage.ParentOkrug != null ? filePackage.ParentOkrug.ShortName : null,
                PeriodRegDate = filePackage.PeriodRegDate
            });
        }

        public ViewResult DataAnalysis(long inputPackageFileId)
        {
            return View(inputPackageFileId);
        }

        [HttpPost]
        public ContentResult Process([FromBody]InputFilePackageProcessDto processDto)
        {
            try
            {
                if (processDto == null ||
                    ((processDto.NachInputFileIds == null || processDto.NachInputFileIds.Count == 0)
                    && (processDto.PlatInputFileIds == null || processDto.PlatInputFileIds.Count == 0)))
                {
                    throw new Exception("Обработка не была запущена, т.к. не было передано строк для обработки");
                }

                List<long> inputFileIds = new List<long>();

                if (processDto.NachInputFileIds != null && processDto.NachInputFileIds.Count > 0)
                {
                    inputFileIds.AddRange(processDto.NachInputFileIds);
                }

                if (processDto.PlatInputFileIds != null && processDto.PlatInputFileIds.Count > 0)
                {
                    inputFileIds.AddRange(processDto.PlatInputFileIds);
                }

                List<OMInputFile> inputFileList = OMInputFile.Where(x => inputFileIds.Contains(x.EmpId))
                    .SelectAll()
                    .Select(x => x.ParentDistrict.Code)
                    .Execute();

                if (inputFileList.Count == 0)
                {
                    throw new Exception("Обработка не была запущена, т.к. не было найдено файлов загрузки с переданными идентификаторами");
                }

                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    foreach (OMInputFile inputFile in inputFileList)
                    {
                        if ((inputFile.TypeFile_Code == TypeFile.Nach && inputFile.Status_Code != UFKFileProcessingStatus.Loaded && inputFile.Status_Code != UFKFileProcessingStatus.ProcessedPartially)
                            || (inputFile.TypeFile_Code == TypeFile.Strah && inputFile.Status_Code != UFKFileProcessingStatus.ProcessedPartially && inputFile.Status_Code != UFKFileProcessingStatus.LinkedBankPartially && inputFile.Status_Code != UFKFileProcessingStatus.LinkedBankCompletely))
                        {
                            throw new Exception($"Обработка не была запущена, " +
                                $"т.к. файл загрузки " +
                                $"{ (inputFile.ParentDistrict != null && inputFile.ParentDistrict.Code.HasValue ? $"для района с кодом {inputFile.ParentDistrict.Code.Value} " : string.Empty) }" +
                                $"{ (inputFile.TypeFile_Code == TypeFile.Nach ? "не находится в статусе \"Загружен\" или \"Обработан частично\"" : "не находится в статусе \"Обработан частично\", \"Связан с банком (частично)\", \"Связан с банком (полностью)\"")}");
                        }

                        OMFileProcessLog processLog = new OMFileProcessLog();
                        processLog.InputFileId = inputFile.EmpId;
                        processLog.Status_Code = FileProcessStatus.Prepare;
                        processLog.UserId = SRDSession.GetCurrentUserId();
                        processLog.Save();

                        LongProcessManager.AddTaskToQueue("InputFileProcess", OMFileProcessLog.GetRegisterId(), processLog.EmpId);

                        inputFile.Status_Code = UFKFileProcessingStatus.InProcess;
                        inputFile.Save();
                    }

                    ts.Complete();
                }

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult Delete([FromBody]InputFilePackageProcessDto processDto)
        {
            try
            {
                if (processDto == null ||
                        ((processDto.NachInputFileIds == null || processDto.NachInputFileIds.Count == 0)
                        && (processDto.PlatInputFileIds == null || processDto.PlatInputFileIds.Count == 0)
                        && (processDto.OplDistrictIds == null || processDto.OplDistrictIds.Count == 0)))
                {
                    throw new Exception("Удаление не было запущено, т.к. не было передано строк для удаления");
                }

                List<OMInputFile> nachInputFiles = null;
                List<OMInputFile> platInputFiles = null;
                List<OMInputFile> oplInputFiles = null;

                if (processDto.NachInputFileIds != null && processDto.NachInputFileIds.Count > 0)
                {
                    nachInputFiles = OMInputFile
                        .Where(x => processDto.NachInputFileIds.Contains(x.EmpId))
                        .SelectAll()
                        .Execute();
                }

                if (processDto.PlatInputFileIds != null && processDto.PlatInputFileIds.Count > 0)
                {
                    platInputFiles = OMInputFile
                        .Where(x => processDto.PlatInputFileIds.Contains(x.EmpId))
                        .SelectAll()
                        .Execute();
                }

                //файлы загрузки для оплат определяем по районам и периоду
                if (processDto.OplDistrictIds != null && processDto.OplDistrictIds.Count > 0)
                {
                    if (!processDto.PeriodRegDate.HasValue)
                    {
                        throw new Exception("Удаление не было запущено. Невозможно удалить файлы оплат, т.к. не был определен период учета");
                    }

                    oplInputFiles = OMInputFile
                        .Where(x => x.PeriodRegDate == processDto.PeriodRegDate.Value
                        && processDto.OplDistrictIds.Contains(x.DistrictId)
                        && x.TypeFile_Code == TypeFile.BankPayment)
                        .SelectAll()
                        .Execute();
                }

                if ((nachInputFiles != null && nachInputFiles.Any(x => x.Status_Code != UFKFileProcessingStatus.Loaded))
                    || (platInputFiles != null && platInputFiles.Any(x => x.Status_Code != UFKFileProcessingStatus.Loaded))
                    || (oplInputFiles != null && oplInputFiles.Any(x => x.Status_Code != UFKFileProcessingStatus.Loaded)))
                {
                    throw new Exception("Удаление не было запущено, т.к. не все выбранные файлы загрузки находятся в статусе \"Загружен\"");
                }

                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    StringBuilder cmdText = new StringBuilder();

                    List<long> inputFileIds = new List<long>();

                    if (nachInputFiles != null && nachInputFiles.Count > 0)
                    {
                        List<long> nachInputFileIds = nachInputFiles.Select(x => x.EmpId).ToList();
                        cmdText.AppendLine($"delete from insur_input_nach where link_id_file in ({string.Join(',', nachInputFileIds)});");
                        inputFileIds.AddRange(nachInputFileIds);
                    }

                    if (platInputFiles != null && platInputFiles.Count > 0)
                    {
                        List<long> platInputFileIds = platInputFiles.Select(x => x.EmpId).ToList();
                        cmdText.AppendLine($"delete from insur_input_plat where link_id_file in ({string.Join(',', platInputFileIds)});");
                        inputFileIds.AddRange(platInputFileIds);
                    }

                    if (oplInputFiles != null && oplInputFiles.Count > 0)
                    {
                        List<long> oplInputFileIds = oplInputFiles.Select(x => x.EmpId).ToList();
                        cmdText.AppendLine($"update insur_input_plat set status_identif_code = {(int)StatusIdentifikacii.None}, " +
                            $"status_identif = '{ StatusIdentifikacii.None.GetEnumDescription() }', link_bank_id = null " +
                            $"where link_bank_id in (select emp_id from insur_bank_plat where link_id_file in ({string.Join(',', oplInputFileIds)}));");
                        cmdText.AppendLine($"delete from insur_bank_plat where link_id_file in ({string.Join(',', oplInputFileIds)});");
                        cmdText.AppendLine($"delete from insur_svod_bank where link_id_file in ({string.Join(',', oplInputFileIds)});");
                        inputFileIds.AddRange(oplInputFileIds);
                    }

                    cmdText.AppendLine($"delete from insur_input_file where emp_id in ({string.Join(',', inputFileIds)});");

                    DbCommand command = DBMngr.Realty.GetSqlStringCommand(cmdText.ToString());
                    DBMngr.Realty.ExecuteNonQuery(command);

                    ts.Complete();
                }

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult PlatIdentifyProcess([FromBody]InputFilePackageProcessDto processDto)
        {
            try
            {
                if (processDto == null ||
                    processDto.PlatInputFileIds == null || processDto.PlatInputFileIds.Count == 0)
                {
                    throw new Exception("Идентификация не была запущена, т.к. не было передано строк для обработки");
                }

                List<OMInputFile> inputFileList = OMInputFile
                    .Where(x => processDto.PlatInputFileIds.Contains(x.EmpId))
                    .SelectAll()
                    .Execute();

                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    foreach (OMInputFile inputFile in inputFileList)
                    {
                        _inputFileService.StartIdentifyProcess(inputFile);
                    }

                    ts.Complete();
                }

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        public ContentResult GetCriteriaList(long inputPackageFileId)
        {
            OMInputFilePackage filePackage = OMInputFilePackage
                .Where(x => x.Id == inputPackageFileId)
                .Select(x => x.OkrugId)
                .Select(x => x.PeriodRegDate)
                .Execute()
                .FirstOrDefault();

            if (filePackage == null)
            {
                throw new Exception($"Не удалось определить пакет загрузки с идентификатором: {inputPackageFileId}");
            }

            List<InputFilePackageCriteriaDto> criteriaList = new List<InputFilePackageCriteriaDto>();

            if (!filePackage.PeriodRegDate.HasValue)
            {
                return Content(JsonConvert.SerializeObject(criteriaList), "application/json");
            }

            int criteriaOrder = 0;

            Task<DataTable> nachTask = Task.Run(() =>
            {
                DbCommand command = DBMngr.Realty.GetSqlStringCommand($@"select count(1) filter (where strpos(iin.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.CalcSumMismatch, Value = 1 })}') > 0) as ""CalcSumMismatch"",
count(1) filter(where strpos(iin.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.UnomAddressMismatch, Value = 1 })}') > 0) as ""UnomAddressMismatch"",
count(1) filter(where strpos(iin.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.SuspiciousUnom, Value = 1 })}') > 0) as ""SuspiciousUnom"",
count(1) filter(where strpos(iin.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.KvnomNomMismatch, Value = 1 })}') > 0) as ""KvnomNomMismatch"",
count(1) filter(where strpos(iin.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.MoreThanOneNach, Value = 1 })}') > 0) as ""MoreThanOneNach"",
count(1) filter(where strpos(iin.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.NachWithoutOpl, Value = 1 })}') > 0) as ""NachWithoutOpl"",
count(1) filter(where strpos(iin.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FoplOplMismatch, Value = 1 })}') > 0) as ""FoplOplMismatch"",
count(1) filter(where strpos(iin.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FlatNotFound, Value = 1 })}') > 0) as ""FlatNotFound"",
count(1) filter(where strpos(iin.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FlatOplMismatch, Value = 1 })}') > 0) as ""FlatOplMismatch"",
count(1) filter(where strpos(iin.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FlatKolgpMismatch, Value = 1 })}') > 0) as ""FlatKolgpMismatch""
from insur_input_nach iin
join insur_district ind on ind.id = iin.district_id
where ind.okrug_id = {filePackage.OkrugId} and iin.period_reg_date = {DBMngr.Realty.FormatDate(filePackage.PeriodRegDate.Value)}");
                return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
            });

            Task<DataTable> platTask = Task.Run(() =>
            {
                DbCommand command = DBMngr.Realty.GetSqlStringCommand($@"select count(1) filter(where strpos(iip.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.UnomAddressMismatch, Value = 1 })}') > 0) as ""UnomAddressMismatch"",
count(1) filter(where strpos(iip.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.SuspiciousUnom, Value = 1 })}') > 0) as ""SuspiciousUnom"",
count(1) filter(where strpos(iip.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.KvnomNomMismatch, Value = 1 })}') > 0) as ""KvnomNomMismatch"",
count(1) filter(where strpos(iip.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FlatNotFound, Value = 1 })}') > 0) as ""FlatNotFound"",
count(1) filter(where strpos(iip.criteria_json, '{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FlatOplMismatch, Value = 1 })}') > 0) as ""FlatOplMismatch"",
count(1) filter(where iip.status_identif_code = 0) as ""NotIdentifiedPlat""
from insur_input_plat iip
join insur_district ind on ind.id = iip.district_id
where ind.okrug_id = {filePackage.OkrugId} and iip.period_reg_date = {DBMngr.Realty.FormatDate(filePackage.PeriodRegDate.Value)}");
                return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
            });

            Task<DataTable> bankPlatTask = Task.Run(() =>
            {
                DbCommand command = DBMngr.Realty.GetSqlStringCommand($@"select count(1) as  ""BankPlatWithoutPlat"" from insur_bank_plat ibp
join insur_district ind on ind.id = ibp.district_id
left join insur_input_plat iip on iip.link_bank_id = ibp.emp_id
where ind.okrug_id = {filePackage.OkrugId} and ibp.period_reg_date = {DBMngr.Realty.FormatDate(filePackage.PeriodRegDate.Value)}
and iip.emp_id is null");
                return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
            });

            DataTable nachTable = nachTask.Result;

            DataTable platTable = platTask.Result;

            DataTable bankPlatTable = bankPlatTask.Result;

            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерии сверки данных МФЦ с банковскими строками оплат",
                IsGroup = true
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – Для зачислений МФЦ отсутствуют соответствующие строки банка",
                Url = ($"~/RegistersView/MfcInputPlat?Transition=1" +
                    $"&qs=(301001000equal'{inputPackageFileId}'" +
                    $"and306002200equal'0'")
                    .ResolveClientUrl(),
                Count = platTable.Rows[0]["NotIdentifiedPlat"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – Для банковских строк оплат отсутствуют соответствующие зачисления в данных МФЦ",
                Url = ($"~/RegistersView/BankPaymentFiles?Transition=1" +
                    $"&qs=(301001000equal'{inputPackageFileId}')" +
                    "and306000400isnull)")
                    .ResolveClientUrl(),
                Count = bankPlatTable.Rows[0]["BankPlatWithoutPlat"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерии сверки данных МФЦ по UNOM, адресам, плательщикам и площадям",
                IsGroup = true
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – Расхождения в суммах начислений",
                Url = ($"~/RegistersView/MfcInputNach?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and305002900contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.CalcSumMismatch, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = nachTable.Rows[0]["CalcSumMismatch"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – UNOM с разными адресами (начисления)",
                Url = ($"~/RegistersView/MfcInputNach?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and305002900contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.UnomAddressMismatch, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = nachTable.Rows[0]["UnomAddressMismatch"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – UNOM с разными адресами (зачисления)",
                Url = ($"~/RegistersView/MfcInputPlat?Transition=1" +
                    $"&qs=(301001000equal'{inputPackageFileId}'" +
                    $"and306003600contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.UnomAddressMismatch, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = platTable.Rows[0]["UnomAddressMismatch"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – Подозрительные UNOM (начисления)",
                Url = ($"~/RegistersView/MfcInputNach?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and305002900contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.SuspiciousUnom, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = nachTable.Rows[0]["SuspiciousUnom"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – Подозрительные UNOM (зачисления)",
                Url = ($"~/RegistersView/MfcInputPlat?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and306003600contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.SuspiciousUnom, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = platTable.Rows[0]["SuspiciousUnom"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} - Несовпадение NOM+NOMI (начисления)",
                Url = ($"~/RegistersView/MfcInputNach?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and305002900contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.KvnomNomMismatch, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = nachTable.Rows[0]["KvnomNomMismatch"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – Несовпадение NOM+NOMI (зачисления)",
                Url = ($"~/RegistersView/MfcInputPlat?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and306003600contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.KvnomNomMismatch, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = platTable.Rows[0]["KvnomNomMismatch"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – Наличие более одного начисления на одного плательщика",
                Url = ($"~/RegistersView/MfcInputNach?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and305002900contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.MoreThanOneNach, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = nachTable.Rows[0]["MoreThanOneNach"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – Есть начисление, нет площади",
                Url = ($"~/RegistersView/MfcInputNach?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and305002900contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.NachWithoutOpl, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = nachTable.Rows[0]["NachWithoutOpl"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – Площадь страхования не совпадает с площадью квартиры, для отдельных квартир (начисления)",
                Url = ($"~/RegistersView/MfcInputNach?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and305002900contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FoplOplMismatch, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = nachTable.Rows[0]["FoplOplMismatch"].ParseToLong()
            });

            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерии сверки данных МФЦ по квартире с данными объекта страхования (квартире) в Системе",
                IsGroup = true
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – В данных МФЦ квартира присутствует, а объект страхования (квартира) в Системе не найден (начисления)",
                Url = ($"~/RegistersView/MfcInputNach?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and305002900contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FlatNotFound, Value = 1 })}')"),
                Count = nachTable.Rows[0]["FlatNotFound"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – В данных МФЦ квартира присутствует, а объект страхования (квартира) в Системе не найден (зачисления)",
                Url = ($"~/RegistersView/MfcInputPlat?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and306003600contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FlatNotFound, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = platTable.Rows[0]["FlatNotFound"].ParseToLong()
            });
            //TODO критерий в аналитике
            //criteriaList.Add(new InputFilePackageCriteriaDto
            //{
            //    Name = $"Критерий № {++criteriaOrder} – В данных МФЦ квартира отсутствует, а объект страхования (квартира) в Системе присутствует (начисления)",
            //    Url = $"~/RegistersView/MfcInputNach?Transition=1&301001000={inputPackageFileId}".ResolveClientUrl()
            //});
            //criteriaList.Add(new InputFilePackageCriteriaDto
            //{
            //    Name = $"Критерий № {++criteriaOrder} – В данных МФЦ квартира отсутствует, а объект страхования (квартира) в Системе присутствует (зачисления)",
            //    Url = $"~/RegistersView/MfcInputPlat?Transition=1&301001000={inputPackageFileId}".ResolveClientUrl()
            //});
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – В данных МФЦ неверная общая площадь квартиры (начисления)",
                Url = ($"~/RegistersView/MfcInputNach?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and305002900contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FlatOplMismatch, Value = 1 })}')"),
                Count = nachTable.Rows[0]["FlatOplMismatch"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – В данных МФЦ неверная общая площадь квартиры (зачисления)",
                Url = ($"~/RegistersView/MfcInputPlat?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and306003600contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FlatOplMismatch, Value = 1 })}')")
                    .ResolveClientUrl(),
                Count = platTable.Rows[0]["FlatOplMismatch"].ParseToLong()
            });
            criteriaList.Add(new InputFilePackageCriteriaDto
            {
                Name = $"Критерий № {++criteriaOrder} – В данных МФЦ неверное количество комнат в квартире",
                Url = ($"~/RegistersView/MfcInputNach?Transition=1&" +
                    $"qs=(301001000equal'{inputPackageFileId}'" +
                    $"and305002900contains'{JsonConvert.SerializeObject(new VerificationCriteriaModel { Id = (long)VerificationCriteria.FlatKolgpMismatch, Value = 1 })}')"),
                Count = nachTable.Rows[0]["FlatKolgpMismatch"].ParseToLong()
            });

            return Content(JsonConvert.SerializeObject(criteriaList), "application/json");
        }

        [HttpGet]
        public ViewResult ProcessOkrug(long inputPackageFileId)
        {
            List<long> inputPackageFileIds = RegistersVariables.CurrentList?.ToList() ?? new List<long>();

            if (inputPackageFileIds.Count == 0)
            {
                inputPackageFileIds.Add(inputPackageFileId);
            }

            List<InputFilePackageProcessOkrugDto> inputFilePackages = new List<InputFilePackageProcessOkrugDto>();

            foreach (long packageFileId in inputPackageFileIds)
            {
                inputFilePackages.Add(InputFilePackageProcessOkrugDto.OMMap(packageFileId));
            }

            return View(inputFilePackages);
        }

        [HttpPost]
        public ContentResult ProcessOkrug([FromBody]long[] inputFilePackageIds)
        {
            try
            {
                if (inputFilePackageIds != null)
                {
                    foreach (long inputFilePackageId in inputFilePackageIds)
                    {
                        List<OMInputFile> inputFiles = OMInputFile
                            .Where(x => x.LinkPackage == inputFilePackageId && (x.Status_Code == UFKFileProcessingStatus.Loaded || x.Status_Code == UFKFileProcessingStatus.LinkedBankCompletely || x.Status_Code == UFKFileProcessingStatus.LinkedBankPartially))
                            .SelectAll()
                            .Execute();

                        foreach (OMInputFile inputFile in inputFiles.Where(x => x.TypeFile_Code == TypeFile.Nach || x.Status_Code == UFKFileProcessingStatus.LinkedBankCompletely || x.Status_Code == UFKFileProcessingStatus.LinkedBankPartially))
                        {
                            _inputFileService.StartProcess(inputFile);
                        }

                        foreach (OMInputFile inputFile in inputFiles.Where(x => x.TypeFile_Code == TypeFile.Strah && x.Status_Code == UFKFileProcessingStatus.Loaded))
                        {
                            _inputFileService.StartIdentifyProcess(inputFile, true);
                        }
                    }
                }

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        /// <summary>
        /// CIPJS-831 
        /// Кнопка обновления статистики МФЦ. 
        /// </summary>
        /// <param name="inputPackageFileId"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult UpdateMfcDataStatistics(long inputPackageFileId)
        {
            List<long> inputPackageFileIds = RegistersVariables.CurrentList?.ToList() ?? new List<long>();

            if (inputPackageFileIds.Count == 0)
            {
                inputPackageFileIds.Add(inputPackageFileId);
            }

            List<InputFilePackageProcessOkrugDto> inputFilePackages = new List<InputFilePackageProcessOkrugDto>();

            foreach (long packageFileId in inputPackageFileIds)
            {
                inputFilePackages.Add(InputFilePackageProcessOkrugDto.OMMap(packageFileId));
            }

            return View(inputFilePackages);
        }

        [HttpPost]
        public ContentResult UpdateMfcDataStatistics([FromBody]long[] inputFilePackageIds)
        {
            try
            {
                if (_inputFileService.UpdateMfcDataStatistics(inputFilePackageIds))
                {
                    return EmptyResponse();
                }
                else
                {
                    return ErrorResponse("Возникла ошибка обновления статистических данных!");
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public ViewResult IdentifyOkrug(long inputPackageFileId)
        {
            List<long> inputPackageFileIds = RegistersVariables.CurrentList?.ToList() ?? new List<long>();

            if (inputPackageFileIds.Count == 0)
            {
                inputPackageFileIds.Add(inputPackageFileId);
            }

            List<InputFilePackageIdentifyOkrugDto> inputFilePackages = new List<InputFilePackageIdentifyOkrugDto>();

            foreach (long packageFileId in inputPackageFileIds)
            {
                inputFilePackages.Add(InputFilePackageIdentifyOkrugDto.OMMap(packageFileId));
            }

            return View(inputFilePackages);
        }

        [HttpPost]
        public ContentResult IdentifyOkrug([FromBody]long[] inputFilePackageIds)
        {
            try
            {
                if (inputFilePackageIds != null)
                {
                    foreach (long inputFilePackageId in inputFilePackageIds)
                    {
                        List<OMInputFile> inputFiles = OMInputFile
                            .Where(x => x.LinkPackage == inputFilePackageId
                            && x.TypeFile_Code == TypeFile.Strah
                            && x.Status_Code == UFKFileProcessingStatus.Loaded)
                            .SelectAll()
                            .Execute();

                        foreach (OMInputFile inputFile in inputFiles)
                        {
                            _inputFileService.StartIdentifyProcess(inputFile);
                        }
                    }
                }

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }
    }
}