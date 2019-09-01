using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace CIPJS.Controllers
{
    public class ApiController : BaseController
    {
        public ActionResult EgasImport()
        {
            try
            {
                //new DAL.Egas.EgasImportLoadProcess().StartProcess(null, null, new System.Threading.CancellationToken());
                LongProcessManager.AddTaskToQueue("EgasImportLoadProcess");

                return JsonResponse("Импорт Егас запущен");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Ошибка при запуске импорта Егас. {ex.Message}");
            }
        }

        public ActionResult SetContracts()
        {
            return Content(new DAL.SK.SkProcessLoadProcess().SetContracts());
        }
        /// <summary>
        /// Связка расчетов и договоров, проставление доли города.
        /// </summary>
        /// <returns></returns>
        public ActionResult LinkСalculationToContract()
        {
            try
            {
                string response = new DAL.Calculation.CalculationService().LinkСalculationToContract();
                return Content($"Операция успешно выполнена! {response}");
            }
            catch(Exception ex)
            {
                return Content($"Возникла ошибка во время выполнения операции: {ex.Message}");
            }
        }

        public ActionResult FspRecalcChangedOplKodplProcess()
        {
            try
            {
                LongProcessManager.AddTaskToQueue("FspRecalcChangedOplKodplProcess");

                return JsonResponse("Учет изменения площади ФСП успешно запущен");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Ошибка при запуске процесса. {ex.Message}");
            }
        }

        public ActionResult FspOplKodoplUpdateProcess()
        {
            try
            {
                LongProcessManager.AddTaskToQueue("FspOplKodoplUpdateProcess");

                return JsonResponse("Обновление площади ФСП и перерасчет баланса успешно запущены");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Ошибка при запуске процесса. {ex.Message}");
            }
        }

        /// <summary>
        /// CIPJS-907: Обработать массово записи с измененными площадями ФСП
        /// </summary>
        /// <returns></returns>
        public ActionResult FspOplDifferentKodoplUpdateProcess()
        {
            try
            {
                LongProcessManager.AddTaskToQueue("FspOplDifferentKodoplUpdateProcess");

                return Content("Перерасчет ФСП успешно запущен");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Ошибка при запуске процесса. {ex.Message}");
            }
        }

        public ActionResult FspPolisUpdateBalancesProcess()
        {
            try
            {
                LongProcessManager.AddTaskToQueue("FspPolisUpdateBalancesProcess");

                return JsonResponse("Обновление балансов для ФСП типа полис и перерасчет успешно запущены");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Ошибка при запуске процесса. {ex.Message}");
            }
        }

        public IActionResult SetOrganizationToFile()
        {
            int successCount = 0;
            int errorCount = 0;
            List<OMInputFile> omInputFiles = OMInputFile.Where(x => x.TypeSource_Code == InsuranceSourceType.Sk 
                && (x.Status_Code == UFKFileProcessingStatus.ProcessedCompletely || x.Status_Code == UFKFileProcessingStatus.Loaded)
                && x.InsuranceOrganizationId == null)
                .Select(x => x.TypeFile_Code)
                .Execute();

            foreach (OMInputFile omInputFile in omInputFiles)
            {
                try
                {
                    if (omInputFile.TypeFile_Code == TypeFile.Policy)
                    {
                        omInputFile.InsuranceOrganizationId = OMPolicySvd.Where(x => x.LinkIdFile == omInputFile.EmpId && x.InsuranceOrganizationId != null)
                            .Select(x => x.InsuranceOrganizationId)
                            .SetPackageSize(1).ExecuteFirstOrDefault()?.InsuranceOrganizationId;
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.TerminatedPolicy)
                    {
                        omInputFile.InsuranceOrganizationId = OMPolicySvd.Where(x => x.LinkIdFileEnd == omInputFile.EmpId && x.InsuranceOrganizationId != null)
                            .Select(x => x.InsuranceOrganizationId)
                            .SetPackageSize(1).ExecuteFirstOrDefault()?.InsuranceOrganizationId;
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.Certificate)
                    {
                        omInputFile.InsuranceOrganizationId = OMPolicySvd.Where(x => x.LinkIdFile == omInputFile.EmpId && x.InsuranceOrganizationId != null)
                            .Select(x => x.InsuranceOrganizationId)
                            .SetPackageSize(1).ExecuteFirstOrDefault()?.InsuranceOrganizationId;
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.InsurancePayments)
                    {
                        omInputFile.InsuranceOrganizationId = OMPayTo.Where(x => x.LinkIdFile == omInputFile.EmpId && x.InsuranceOrganizationId != null)
                            .Select(x => x.InsuranceOrganizationId)
                            .SetPackageSize(1).ExecuteFirstOrDefault()?.InsuranceOrganizationId;
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.InsurancePaymentsRefusal)
                    {
                        omInputFile.InsuranceOrganizationId = OMNoPay.Where(x => x.LinkIdFile == omInputFile.EmpId && x.InsuranceOrganizationId != null)
                            .Select(x => x.InsuranceOrganizationId)
                            .SetPackageSize(1).ExecuteFirstOrDefault()?.InsuranceOrganizationId;
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.InsuranceContractConcluded)
                    {
                        omInputFile.InsuranceOrganizationId = OMAllProperty.Where(x => x.LinkIdFile == omInputFile.EmpId && x.InsuranceId != null)
                            .Select(x => x.InsuranceId)
                            .SetPackageSize(1).ExecuteFirstOrDefault()?.InsuranceId;
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.AddInsuranceContractConcluded)
                    {
                        long? kod = OMDopAllProperty.Where(x => x.LinkIdFile == omInputFile.EmpId && x.Kod != null)
                            .Select(x => x.Kod).SetPackageSize(1).ExecuteFirstOrDefault()?.Kod;

                        if (kod.HasValue)
                        {
                            omInputFile.InsuranceOrganizationId = OMInsuranceOrganization.Where(x => x.Code == kod).ExecuteFirstOrDefault()?.Id;
                        }
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.PaymentReceived)
                    {
                        long? kod = OMInputPlat.Where(x => x.LinkIdFile == omInputFile.EmpId && x.Kod != null)
                            .Select(x => x.Kod).SetPackageSize(1).ExecuteFirstOrDefault()?.Kod;

                        if (kod.HasValue)
                        {
                            omInputFile.InsuranceOrganizationId = OMInsuranceOrganization.Where(x => x.Code == kod).ExecuteFirstOrDefault()?.Id;
                        }
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.InsurancePaymentsUnderContracts)
                    {
                        omInputFile.InsuranceOrganizationId = OMPayTo.Where(x => x.LinkIdFile == omInputFile.EmpId && x.InsuranceOrganizationId != null)
                            .Select(x => x.InsuranceOrganizationId)
                            .SetPackageSize(1).ExecuteFirstOrDefault()?.InsuranceOrganizationId;
                    }
                    else if (omInputFile.TypeFile_Code == TypeFile.DeclaredUnsettledInsuranceEvents)
                    {
                        omInputFile.InsuranceOrganizationId = OMNoPay.Where(x => x.LinkIdFile == omInputFile.EmpId && x.InsuranceOrganizationId != null)
                            .Select(x => x.InsuranceOrganizationId)
                            .SetPackageSize(1).ExecuteFirstOrDefault()?.InsuranceOrganizationId;
                    }
                    else
                    {
                        throw new Exception($"Для типа {omInputFile.TypeFile_Code} нет обработчика");
                    }

                    if (omInputFile.InsuranceOrganizationId.HasValue)
                    {
                        omInputFile.Save();
                        successCount++;
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    errorCount++;
                }
            }

            return Content($"Всего файлов СК без связи: {omInputFiles.Count}; Установлено связей: {successCount}; Ошибок: {errorCount}");
        }

        
        public ActionResult FspLinkObjectProcess()
        {
            try
            {
                LongProcessManager.AddTaskToQueue("FspLinkObjectProcess");

                return JsonResponse("Процесс связки ФСП с квартирой успешно запущен");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Ошибка при запуске процесса. {ex.Message}");
            }
        }

        public ActionResult OrgUnomUpdateProcess()
        {
            try
            {
                LongProcessManager.AddTaskToQueue("OrgUnomUpdateProcess");

                return JsonResponse("Обновление реестра взаимосвязей  МКД и Управляющей организации на основании банковских строк успешно запущены");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Ошибка при запуске процесса. {ex.Message}");
            }
        }
    }
}