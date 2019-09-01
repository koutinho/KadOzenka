using CIPJS.DAL.Bank;
using CIPJS.DAL.Contract;
using CIPJS.DAL.InputPlat;
using CIPJS.Models.Contract;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.SRD.DAL;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace CIPJS.Controllers
{
    public class ContractController : BaseController
    {
        private readonly ContractService _contractService;
        private readonly InputPlatService _inputPlatService;

        public ContractController(ContractService contractService, InputPlatService inputPlatService)
        {
            _contractService = contractService;
            _inputPlatService = inputPlatService;
        }

        public ActionResult Details(long? id)
        {
            if (id.HasValue)
            {
                OMAllProperty omAllProperty = _contractService.GetById(id.Value);
                OMParamCalculation omParamCalculation = OMParamCalculation.Where(x => x.ContractId == id.Value).SelectAll().Execute().FirstOrDefault();
                OMAgreementProject project = omParamCalculation != null ? OMAgreementProject.Where(x => x.CalculationId == omParamCalculation.EmpId).SelectAll().Execute().FirstOrDefault() : null;

                return View(AllPropertyDetails.Map(omAllProperty, omParamCalculation, project));
            }

            return View(new AllPropertyDetails { Id = -1 });
        }

        [HttpGet]
        public ActionResult ControlData(long? id)
        {
            if (id.HasValue)
            {
                OMAllProperty omAllProperty = _contractService.GetById(id.Value);
                var model = new AllPropertyDetails
                {
                    Status = omAllProperty?.Status_Code
                };

                AllPropertyDetails.FillControlData(omAllProperty, model);

                return PartialView(model);
            }

            return PartialView(new AllPropertyDetails { Id = -1 });
        }

        public ActionResult PaymentsGrid_Read(long? id)
        {
            List<OMInputPlat> omInputPlats;

            if (id.HasValue)
            {
                omInputPlats = _inputPlatService.GetByAllPropertyId(id.Value);
            }
            else
            {
                omInputPlats = new List<OMInputPlat>();
            }

            var models = omInputPlats.Select(x => new
            {
                x.Ndog,
                x.Ndogdat,
                x.Paynumber,
                x.PmtDate,
                x.SumOpl
            }).ToList();

            return Content(JsonConvert.SerializeObject(models), "application/json");
        }

        [HttpPost]
        public ActionResult Agreed(long id)
        {
            _contractService.Agreed(id);

            return JsonResponse(new
            {
                date = DateTime.Now,
                user = SRDSession.GetCurrentUsername(),
                post = SRDSession.Current.User.Position
            });
        }

        public ActionResult Print(long id)
        {
            return Content("Нет Данных");
        }

        [HttpPost]
        public IActionResult Update(AllPropertyDetails model)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP, true, false, true);

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                OMAllProperty omAllProperty = OMAllProperty.Where(x => x.EmpId == model.Id).ExecuteFirstOrDefault();

                if (omAllProperty == null) throw new Exception($"Сущность не найдена {model.Id}");

                omAllProperty.PartCity = model.PartMoscow;
                omAllProperty.Save();

                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP, true, "Договор обновлен", OMAllProperty.GetRegisterId(), omAllProperty.EmpId);

                ts.Complete();

                return EmptyResponse();
            }
        }

        public IActionResult FormInvoices(long id)
        {
            List<long> ids = RegistersVariables.CurrentList?.ToList() ?? new List<long>();

            if (!ids.Any()) ids.Add(id);

            List<OMAllProperty> omAllProperties = OMAllProperty.Where(x => ids.Contains(x.EmpId))
                .Select(x => x.OrgIdFile)
                .Select(x => x.Name)
                .Select(x => x.Ndog)
                .Select(x => x.Ndogdat)
                .Select(x => x.PartCity)
                .Select(x => x.RasPripay)
                .Execute();

            if (!omAllProperties.Any())
            {
                ViewBag.ErrorMessages = "Выбранные договора в БД не найдены";
                return View();
            }

            if (omAllProperties.GroupBy(x => x.OrgIdFile).Count() > 1 || omAllProperties.Any(x => !x.OrgIdFile.HasValue))
            {
                ViewBag.ErrorMessages = "Внимание! Сформировать счета оптом можно только для одного Страхователя. Среди выбранных договоров Страхователи разные (определяется на основании поля ORG_ID в файле COMM.DBF).";
                return View();
            }

            List<OMAllProperty> allPropertiesWithoutPartCity = omAllProperties.Where(x => x.PartCity == null || x.PartCity == 0).ToList();
            if (allPropertiesWithoutPartCity.Count > 0)
            {
                ViewBag.ErrorMessages = "Внимание! Среди выбранных записей, обнаружены договора, для которых значение \"Размер доли города в праве на ОИ\" равен 0";
                return View(new FormInvoicesDto
                {
                    ErrorAllPropertyList = allPropertiesWithoutPartCity.Select((x, y) => FormInvoicesAllPropertyDto.OMMap(x, y + 1)).ToList()
                });
            }

            List<long?> allPropertiesIds = omAllProperties.Select(x => (long?)x.EmpId).ToList();
            List<long?> allPropertiesInPlatIds = OMInputPlat
                .Where(x => x.TypeSource_Code == InsuranceSourceType.Sk && allPropertiesIds.Contains(x.LinkAllPropertyId)).Select(x => x.LinkAllPropertyId)
                .Execute()
                .Select(x => x.LinkAllPropertyId)
                .ToList();
            List<OMAllProperty> allPropertiesNotInPlat = omAllProperties.Where(x => !allPropertiesInPlatIds.Contains(x.EmpId)).ToList();
            if (allPropertiesNotInPlat.Count > 0)
            {
                ViewBag.ErrorMessages = "Внимание! Среди выбранных записей, обнаружены договора, для которых не загружены платежи из файла PAY_COMM.DBF";
                return View(new FormInvoicesDto
                {
                    ErrorAllPropertyList = allPropertiesNotInPlat.Select((x, y) => FormInvoicesAllPropertyDto.OMMap(x, y + 1)).ToList()
                });
            }

            OMSubject omSubject = OMSubject.Where(x => x.SubjectName.Contains(omAllProperties.First().Name))
                .SelectAll()
                .Select(x => x.ParentBank.BankName)
                .Select(x => x.ParentBank.BankBic)
                .Select(x => x.ParentBank.BankInn)
                .Select(x => x.ParentBank.BankKpp)
                .Select(x => x.ParentBank.BankKorAcc)
                .ExecuteFirstOrDefault();

            GetFutureInvoices(omAllProperties, out List<FormInvoicesResultDto> successRows, out List<FormInvoicesResultDto> errorRows);

            FormInvoicesDto model = new FormInvoicesDto
            {
                SubjectId = omSubject?.EmpId,
                SubjectName = omSubject?.SubjectName,
                Phone = omSubject?.OrgPhone,
                Inn = omSubject?.Inn,
                Kpp = omSubject?.Kpp,
                RachAcc = omSubject?.RachAcc,
                BankCardNumber = omSubject?.NumCard,
                BankId = omSubject?.BankId,
                BankName = omSubject?.ParentBank?.BankName,
                BicBank = omSubject?.ParentBank?.BankBic,
                InnBank = omSubject?.ParentBank?.BankInn,
                KppBank = omSubject?.ParentBank?.BankKpp,
                KorAcc = omSubject?.ParentBank?.BankKorAcc,
                Ids = ids,
                FutureSuccessRows = successRows,
                FutureErrorRows = errorRows,
                NameForInvoice = omSubject?.NameForInvoice
            };

            return View(model);
        }

        private void GetFutureInvoices(List<OMAllProperty> omAllProperties, out List<FormInvoicesResultDto> successRows, out List<FormInvoicesResultDto> errorRows)
        {
            successRows = new List<FormInvoicesResultDto>();
            errorRows = new List<FormInvoicesResultDto>();
            int successOrdinal = 0;
            int errorOrdinal = 0;

            foreach (OMAllProperty omAllProperty in omAllProperties)
            {
                try
                {
                    decimal sumOpl = GetSumToPayForFormInvoice(omAllProperty);

                    successRows.Add(new FormInvoicesResultDto
                    {
                        Ordinal = ++successOrdinal,
                        ContractNumber = omAllProperty.Ndog,
                        ContractDate = omAllProperty.Ndogdat,
                        RasPripay = omAllProperty.RasPripay,
                        Sum = sumOpl
                    });
                }
                catch (Exception ex)
                {
                    errorRows.Add(new FormInvoicesResultDto
                    {
                        Ordinal = ++errorOrdinal,
                        ContractNumber = omAllProperty.Ndog,
                        ContractDate = omAllProperty.Ndogdat,
                        RasPripay = omAllProperty.RasPripay,
                        Error = ex.Message
                    });
                }
            }
        }

        [HttpPost]
        public IActionResult FormInvoices(FormInvoicesDto model)
        {
            List<FormInvoicesResultDto> successRows = new List<FormInvoicesResultDto>();
            List<FormInvoicesResultDto> errorRows = new List<FormInvoicesResultDto>();

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                if (model.SubjectId.HasValue)
                {
                    OMSubject omSubject = OMSubject.Where(x => x.EmpId == model.SubjectId).ExecuteFirstOrDefault();

                    if (omSubject != null)
                    {
                        omSubject.SubjectName = model.SubjectName;
                        omSubject.OrgPhone = model.Phone;
                        omSubject.Kpp = model.Kpp;
                        omSubject.Inn = model.Inn;
                        omSubject.RachAcc = model.RachAcc;
                        omSubject.NumCard = model.BankCardNumber;
                        omSubject.BankId = model.BankId;
                        omSubject.Save();
                    }
                }

                if (!model.BankId.HasValue && model.BicBank.IsNotEmpty())
                {
                    model.BankId = OMBank.Where(x => x.BankBic == model.BicBank).ExecuteFirstOrDefault()?.EmpId
                        ?? new OMBank
                        {
                            EmpId = -1,
                            BankBic = model.BicBank,
                            BankName = model.BankName,
                            BankInn = model.InnBank,
                            BankKpp = model.KppBank,
                            BankKorAcc = model.KorAcc
                        }.Save();
                }

                List<OMAllProperty> omAllProperties = OMAllProperty.Where(x => model.Ids.Contains(x.EmpId))
                    .SelectAll().Execute();

                int successOrdinal = 0;
                int errorOrdinal = 0;
                string subjectName = model.NameForInvoice.IsNotEmpty() ? model.NameForInvoice : model.SubjectName;

                OMInvoiceSvod invoiceSvod = null;

                if (omAllProperties.Count > 0)
                {
                    invoiceSvod = new OMInvoiceSvod
                    {
                        SubjectId = model.SubjectId,
                        SubjectName = subjectName,
                        NumInvoice = model.NumInvoice,
                        DateInvoice = model.DateInvoice,
                        Status_Code = InvoiceStatus.Formed
                    };
                    invoiceSvod.Save();
                }

                foreach (OMAllProperty omAllProperty in omAllProperties)
                {
                    try
                    {
                        decimal sumOpl = GetSumToPayForFormInvoice(omAllProperty);

                        new OMInvoice
                        {
                            LinkAllProperty = omAllProperty.EmpId,
                            NumInvoice = model.NumInvoice,
                            dateInvoice = model.DateInvoice,
                            SubjectId = model.SubjectId,
                            SubjectName = subjectName,
                            Inn = model.Inn,
                            Kpp = model.Kpp,
                            RachAcc = model.RachAcc,
                            NumCard = model.BankCardNumber,
                            Phone = model.Phone,
                            KorAcc = model.KorAcc,
                            BicBank = model.BicBank,
                            KppBank = model.KppBank,
                            InnBank = model.InnBank,
                            BankId = model.BankId,
                            Status_Code = InvoiceStatus.Formed,
                            SumOpl = sumOpl,
                            BankName = model.BankName,
                            DataInput = DateTime.Now,
                            UserId = SRDSession.GetCurrentUserId(),
                            LinkInvoiceSvod = invoiceSvod?.EmpId
                        }.Save();

                        successRows.Add(new FormInvoicesResultDto
                        {
                            Ordinal = ++successOrdinal,
                            ContractNumber = omAllProperty.Ndog,
                            ContractDate = omAllProperty.Ndogdat,
                            RasPripay = omAllProperty.RasPripay,
                            SubjectName = model.SubjectName,
                            Sum = sumOpl,
                            InvoiceNumber = model.NumInvoice,
                            InvoiceDate = model.DateInvoice,
                            BicBank = model.BicBank,
                            BankName = model.BankName
                        });
                    }
                    catch (Exception ex)
                    {
                        errorRows.Add(new FormInvoicesResultDto
                        {
                            Ordinal = ++errorOrdinal,
                            ContractNumber = omAllProperty.Ndog,
                            ContractDate = omAllProperty.Ndogdat,
                            RasPripay = omAllProperty.RasPripay,
                            Error = ex.Message
                        });
                    }
                }

                if (invoiceSvod != null)
                {
                    invoiceSvod.InvoiceVsego = successRows.Count;
                    invoiceSvod.SumSvod = successRows.Sum(x => x.Sum);
                    invoiceSvod.Save();
                }

                ts.Complete();
            }

            return JsonResponse(new { successRows, errorRows });
        }

        private decimal GetSumToPayForFormInvoice(OMAllProperty omAllProperty)
        {
            if ((omAllProperty.PartCity ?? 0) == 0)
            {
                throw new Exception("Внимание! Параметр \"Доля города Москвы в праве на общее имущество\" не заполнен, создание счета невозможно!");
            }

            decimal sumToPay = 0;
            decimal? oplDog = OMInputPlat.Where(x => x.LinkAllPropertyId == omAllProperty.EmpId).Select(x => x.SumOpl).Execute().Sum(x => x.SumOpl);
            decimal? invocieSum = OMInvoice.Where(x => x.LinkAllProperty == omAllProperty.EmpId
                && (x.Status_Code == InvoiceStatus.Included || x.Status_Code == InvoiceStatus.TransferredPayment))
                .Select(x => x.SumOpl).Execute().Sum(x => x.SumOpl);
            decimal? totalSumPay = omAllProperty.RasPripay * omAllProperty.PartCity / 100;

            //CIPJS-718 если рассчитанный размер премии меньше, либо равен сумме оплат по зачислениям
            if (omAllProperty.RasPripay <= oplDog)
            {
                sumToPay = Math.Round((totalSumPay ?? 0) - (invocieSum ?? 0), 2);
            }
            //CIPJS-136 Если INSUR_ALL_PROPERTY.RAS_PRIPAY ( размер годовой премии) > OPL_DOG, то
            //считаем ДОЛЮ, сколько уже выплачено от суммы премии = OPL_DOG / INSUR_ALL_PROPERTY.RAS_PRIPAY(округлили до 2 - х знаков после запятой)
            //СУММА СЧЕТА = ДОЛЯ * Nmax – OPL_max
            else if (omAllProperty.RasPripay > oplDog)
            {
                decimal partDog = (oplDog / omAllProperty.RasPripay) ?? 0;
                sumToPay = totalSumPay.HasValue && invocieSum.HasValue ? Math.Round(partDog * (totalSumPay.Value - invocieSum.Value), 2) : 0m;
            }

            //decimal rasPripay = omAllProperty.RasPripay ?? 0m;
            //decimal? Nmax = rasPripay * omAllProperty.PartCity / 100;
            //decimal? oplDog = OMInputPlat.Where(x => x.LinkAllPropertyId == omAllProperty.EmpId)
            //    .Select(x => x.SumOpl).Execute().Sum(x => x.SumOpl);
            //decimal? invocieSum = OMInvoice.Where(x => x.LinkAllProperty == omAllProperty.EmpId 
            //    && (x.Status_Code != InvoiceStatus.ErrorInDetails || x.Status_Code != InvoiceStatus.Denied))
            //    .Select(x => x.SumOpl).Execute().Sum(x => x.SumOpl);
            //decimal sumToPay = Math.Round((rasPripay == oplDog ? Nmax - invocieSum 
            //    : (rasPripay > oplDog ? Math.Round((Nmax == 0 ? 0 : oplDog / Nmax) ?? 0, 2) * rasPripay - invocieSum : 0)) ?? 0, 2);
            sumToPay = Math.Max(sumToPay, 0);

            if (sumToPay == 0)
            {
                throw new Exception("Счет не может быть сформирован, все выплаты произведены или находятся в состоянии переданном на оплату");
            }

            return sumToPay;
        }

        public ActionResult Terms(long propertyId)
        {
            OMAllProperty omAllProperty = OMAllProperty
                .Where(x => x.EmpId == propertyId)
                .Select(x => x.St1)
                .Select(x => x.St2)
                .Select(x => x.St3)
                .Select(x => x.Ss1)
                .Select(x => x.Ss2)
                .Select(x => x.Ss3)
                .Select(x => x.RasPripay)
                .ExecuteFirstOrDefault();

            OMParamCalculation omParamCalculation = OMParamCalculation
                .Where(x => x.ContractId == propertyId)
                .Select(x => x.TotalCost1)
                .Select(x => x.TotalCost2)
                .Select(x => x.TotalCost3)
                .Select(x => x.DesignCost1)
                .Select(x => x.DesignCost2)
                .Select(x => x.DesignCost3)
                .Execute()
                .FirstOrDefault();
            OMAgreementProject project = omParamCalculation != null
                ? OMAgreementProject
                    .Where(x => x.CalculationId == omParamCalculation.EmpId)
                    .Select(x => x.Kat1)
                    .Select(x => x.Kat2)
                    .Select(x => x.Kat3)
                    .Select(x => x.SizeBonusMkd)
                    .Execute()
                    .FirstOrDefault()
                : null;

            // Ищем Damage.
            OMDamage damage = null;
            if (omAllProperty != null)
            {
                damage = OMDamage.Where(x => x.AllPropertyId == propertyId)
                        .Select(x => x.DamageElem1)
                        .Select(x => x.DamageElem2)
                        .Select(x => x.DamageElem3)
                        .ExecuteFirstOrDefault();
            }

            var model = new AllPropertyDetails
            {
                Id = propertyId,
                St1 = project != null && !project.Kat1.GetValueOrDefault() ? 0 : omAllProperty.St1,
                TotalCost1Limit = project != null && project.Kat1.GetValueOrDefault() ? 0 : omParamCalculation?.TotalCost1,
                St2 = project != null && !project.Kat2.GetValueOrDefault() ? 0 : omAllProperty.St2,
                TotalCost2Limit = project != null && !project.Kat2.GetValueOrDefault() ? 0 : omParamCalculation?.TotalCost2,
                St3 = project != null && !project.Kat3.GetValueOrDefault() ? 0 : omAllProperty.St3,
                TotalCost3Limit = project != null && !project.Kat3.GetValueOrDefault() ? 0 : omParamCalculation?.TotalCost3,
                Ss1 = project != null && !project.Kat1.GetValueOrDefault() ? 0 : omAllProperty.Ss1,
                DesignCost1Limit = project != null && !project.Kat1.GetValueOrDefault() ? 0 : omParamCalculation?.DesignCost1,
                Ss2 = project != null && !project.Kat2.GetValueOrDefault() ? 0 : omAllProperty.Ss2,
                DesignCost2Limit = project != null && !project.Kat2.GetValueOrDefault() ? 0 : omParamCalculation?.DesignCost2,
                Ss3 = project != null && !project.Kat3.GetValueOrDefault() ? 0 : omAllProperty.Ss3,
                DesignCost3Limit = project != null && !project.Kat3.GetValueOrDefault() ? 0 : omParamCalculation?.DesignCost3,
                RasPripay = omAllProperty.RasPripay,
                SizeBonusMkd = project?.SizeBonusMkd,
                DamageElem1 = damage?.DamageElem1 ?? 0,
                DamageElem2 = damage?.DamageElem2 ?? 0,
                DamageElem3 = damage?.DamageElem3 ?? 0

            };
            return PartialView(model);
        }

        public ActionResult ContractOITerms(long propertyId)
        {
            OMAllProperty omAllProperty = OMAllProperty
                .Where(x => x.EmpId == propertyId)
                .Select(x => x.St1)
                .Select(x => x.St2)
                .Select(x => x.St3)
                .Select(x => x.Ss1)
                .Select(x => x.Ss2)
                .Select(x => x.Ss3)
                .Select(x => x.RasPripay)
                .ExecuteFirstOrDefault();

            OMParamCalculation omParamCalculation = OMParamCalculation
                .Where(x => x.ContractId == propertyId)
                .Select(x => x.TotalCost1)
                .Select(x => x.TotalCost2)
                .Select(x => x.TotalCost3)
                .Select(x => x.DesignCost1)
                .Select(x => x.DesignCost2)
                .Select(x => x.DesignCost3)
                .Execute()
                .FirstOrDefault();
            OMAgreementProject project = omParamCalculation != null
                ? OMAgreementProject
                    .Where(x => x.CalculationId == omParamCalculation.EmpId)
                    .Select(x => x.Kat1)
                    .Select(x => x.Kat2)
                    .Select(x => x.Kat3)
                    .Select(x => x.SizeBonusMkd)
                    .Execute()
                    .FirstOrDefault()
                : null;

            // Ищем Damage.
            OMDamage damage = null;
            if (omAllProperty != null)
            {
                damage = OMDamage.Where(x => x.AllPropertyId == propertyId)
                        .Select(x => x.DamageElem1)
                        .Select(x => x.DamageElem2)
                        .Select(x => x.DamageElem3)
                        .Select(x => x.SumDamage)
                        .Select(x => x.StrahPlat) 
                        .ExecuteFirstOrDefault();
            }

            ViewBag.SumDamage = damage.SumDamage ?? 0;
            ViewBag.StrahPlat = damage.StrahPlat ?? 0;

            var model = new AllPropertyDetails
            {
                Id = propertyId,
                St1 = project != null && !project.Kat1.GetValueOrDefault() ? 0 : omAllProperty.St1,
                TotalCost1Limit = project != null && project.Kat1.GetValueOrDefault() ? 0 : omParamCalculation?.TotalCost1,
                St2 = project != null && !project.Kat2.GetValueOrDefault() ? 0 : omAllProperty.St2,
                TotalCost2Limit = project != null && !project.Kat2.GetValueOrDefault() ? 0 : omParamCalculation?.TotalCost2,
                St3 = project != null && !project.Kat3.GetValueOrDefault() ? 0 : omAllProperty.St3,
                TotalCost3Limit = project != null && !project.Kat3.GetValueOrDefault() ? 0 : omParamCalculation?.TotalCost3,
                Ss1 = project != null && !project.Kat1.GetValueOrDefault() ? 0 : omAllProperty.Ss1,
                DesignCost1Limit = project != null && !project.Kat1.GetValueOrDefault() ? 0 : omParamCalculation?.DesignCost1,
                Ss2 = project != null && !project.Kat2.GetValueOrDefault() ? 0 : omAllProperty.Ss2,
                DesignCost2Limit = project != null && !project.Kat2.GetValueOrDefault() ? 0 : omParamCalculation?.DesignCost2,
                Ss3 = project != null && !project.Kat3.GetValueOrDefault() ? 0 : omAllProperty.Ss3,
                DesignCost3Limit = project != null && !project.Kat3.GetValueOrDefault() ? 0 : omParamCalculation?.DesignCost3,
                RasPripay = omAllProperty.RasPripay,
                SizeBonusMkd = project?.SizeBonusMkd,
                DamageElem1 = damage?.DamageElem1 ?? 0,
                DamageElem2 = damage?.DamageElem2 ?? 0,
                DamageElem3 = damage?.DamageElem3 ?? 0

            };
            return PartialView(model);
        }

        [HttpPost]
        [Consumes("application/json")]
        public bool SaveDamageElemnts([FromBody]SaveDamageElementsDTO dto)
        {
            // Ищем Damage.
            OMDamage damage = null;
            if (dto?.PropertyId != null)
            {
                damage = OMDamage.Where(x => x.AllPropertyId == dto.PropertyId)
                        .Select(x => x.DamageElem1)
                        .Select(x => x.DamageElem2)
                        .Select(x => x.DamageElem3)
                        .ExecuteFirstOrDefault();
            }

            if (damage != null)
            {
                damage.DamageElem1 = dto.DamageElem1;
                damage.DamageElem2 = dto.DamageElem2;
                damage.DamageElem3 = dto.DamageElem3;
                damage.Save();
                return true;
            }
            
            return false;

        }
    }
}