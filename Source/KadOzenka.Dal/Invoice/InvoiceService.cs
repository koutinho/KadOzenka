using CIPJS.DAL.DamageAnalysis;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.SRD.DAL;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace CIPJS.DAL.Invoice
{
    public class InvoiceService
    {
        /// <summary>
        /// Получение счета
        /// </summary>
        /// <param name="damageId"></param>
        /// <param name="fspId"></param>
        /// <param name="createDefaultIfNotExist"></param>
        /// <returns></returns>
        public InvoiceDto GetByDamageFsp(long? damageId, long fspId, bool createDefaultIfNotExist = false)
        {
            OMInvoice invoice = null;

            if (damageId.HasValue && damageId.Value > 0)
            {
                invoice = OMInvoice.Where(x => x.LinkDamage == damageId.Value && x.LinkFsp == fspId)
                    .SelectAll()
                    .Select(x => x.ParentGbuNoPayReason.Reason)
                    .Execute()
                    .FirstOrDefault();
            }

            return invoice != null ?
                InvoiceDto.OMMap(invoice) :
                createDefaultIfNotExist ? InvoiceDto.CreateNew(damageId, null, fspId, null, null) : null;
        }

        /// <summary>
        /// Получение счета или создание нового
        /// </summary>
        /// <param name="id"></param>
        /// <param name="damageId"></param>
        /// <param name="allPropertyId"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public InvoiceDto GetInvoice(long? id, long? damageId, long? allPropertyId, decimal? sum, long? fspId, decimal? partDog)
        {
            if (id.HasValue && id > -1)
            {
                return InvoiceDto.OMMap(id);
            }

            return InvoiceDto.CreateNew(damageId, allPropertyId, fspId, sum, partDog);
        }

        /// <summary>
        /// Получение модели для счетов
        /// </summary>
        /// <param name="damageId"></param>
        /// <param name="allPropertyId"></param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public InvoiceListDto Get(long? damageId, long? allPropertyId, bool isReadOnly)
        {
            InvoiceListDto model = new InvoiceListDto
            {
                InvoiceAllPropertyId = allPropertyId,
                InvoiceDamageId = damageId,
                IsFisical = false,
                CanAdd = true
            };

            if(damageId.HasValue)
            {
                OMDamage damage = OMDamage.Where(x => x.EmpId == damageId)
                    .Select(x => x.TypeDoc_Code)
                    .Select(x => x.DamageAmountStatus)
                    .Select(x => x.DamageAmountStatus_Code)
                    .Select(x => x.FlagSlygebka)
                    .Execute()
                    .FirstOrDefault();
                if(damage != null)
                {
                    model.IsFisical = damage.TypeDoc_Code == ContractType.Dwelling;
                    //CIPJS-357 Блокировать кнопку создания нового счета в разделе "Выплаты города/отказы", если выполняется условие : 
                    //Статус дела = «Расхождение с СК в расчете ущерба" И (FLAG_SLYGEBKA =0 или пусто ) ИЛИ Статус дела= Создано
                    model.CanAdd = (damage.DamageAmountStatus_Code != StatusDamageAmount.DamageAmountDiscrepancies || (damage.FlagSlygebka.HasValue && damage.FlagSlygebka.Value))
                        && (damage.TypeDoc_Code == ContractType.CommonOwnership || damage.DamageAmountStatus_Code != StatusDamageAmount.Created);
                }
            }
            model.Details = GetInovices(damageId, allPropertyId, isReadOnly, model.IsFisical);

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Save(InvoiceListDto model)
        {
            if (model.Details != null && model.Details.Count > 0)
            {
                List<DamageAnalysisContractDto> contracts = null;

                if (model.InvoiceDamageId.HasValue)
                {
                    contracts = new DamageAnalysisService().GetDamageContracts(model.InvoiceDamageId.Value);
                }

                foreach (InvoiceDto invoiceDetails in model.Details)
                {
                    invoiceDetails.DamageId = model.InvoiceDamageId;
                    invoiceDetails.AllPropertyId = model.InvoiceAllPropertyId ?? invoiceDetails.AllPropertyId;
                    OMInvoice invoice = InvoiceDto.OMMap(invoiceDetails);
                    
                    //TODO разобраться со статусами
                    if (invoice.Status_Code != InvoiceStatus.DeniedAgreed)
                    {
                        if (invoice.NoteNoPayId.HasValue)
                        {
                            invoice.Status_Code = InvoiceStatus.Denied;
                        }
                        else if (invoice.Status_Code == InvoiceStatus.Denied 
                            || invoice.Status_Code == InvoiceStatus.Formed
                            || invoice.Status_Code == InvoiceStatus.None)
                        {
                            //CIPJS-443 добавить проверку, если выбран не застрохванный ФСП и флаг оплачен = 0,
                            //то устанавливаем статус "Отказано в выплате"
                            if (model.InvoiceDamageId.HasValue && invoice.LinkFsp.HasValue &&
                                (contracts == null || !contracts.Any(x => x.FspId == invoice.LinkFsp.Value && x.IsPaid)))
                            {
                                invoice.Status_Code = InvoiceStatus.Denied;
                            }
                            else
                            {
                                invoice.Status_Code = InvoiceStatus.Formed;
                            }
                        }
                    }

                    invoice?.Save();
                }
            }
        }

        public List<string> Validate(InvoiceListDto model)
        {
            List<string> errors = new List<string>();

            if (model == null || model.Details == null || model.Details.Count == 0)
            {
                return errors;
            }

            decimal maxSumOpl = 0;
            decimal overallSumOpl = 0;
            decimal overallPartDog = 0;

            if (model.SumDamage.HasValue && model.PartTown.HasValue)
            {
                maxSumOpl = Math.Round(model.SumDamage.Value * model.PartTown.Value / 100, 2, MidpointRounding.AwayFromZero);
            }

            foreach (InvoiceDto invoice in model.Details)
            {
                if (invoice.StatusCode == InvoiceStatus.ErrorInDetails
                    || invoice.StatusCode == InvoiceStatus.DeniedAgreed)
                {
                    continue;
                }

                if (!invoice.NoPayNoteId.HasValue)
                {
                    if (invoice.SubjectName.IsNullOrEmpty())
                    {
                        errors.Add("\"Получатель\" для счета обязателен для заполнения");
                    }
                    if (invoice.SvidPolycNum.IsNullOrEmpty())
                    {
                        errors.Add("\"Номер свидетельства/полис\" для счета обязателен для заполнения");
                    }
                    if (invoice.BicBank.IsNullOrEmpty())
                    {
                        errors.Add("\"БИК банка\" для счета обязателен для заполнения");
                    }
                    if (invoice.BankName.IsNullOrEmpty())
                    {
                        errors.Add("\"Название банка\" для счета обязателен для заполнения");
                    }
                    if (invoice.InnBank.IsNullOrEmpty())
                    {
                        errors.Add("\"ИНН банка\" для счета обязателен для заполнения");
                    }
                    if (invoice.KppBank.IsNullOrEmpty())
                    {
                        errors.Add("\"КПП банка\" для счета обязателен для заполнения");
                    }
                    if (invoice.KorAcc.IsNullOrEmpty())
                    {
                        errors.Add("\"К/С\" для счета обязателен для заполнения");
                    }
                    if (invoice.RachAcc.IsNullOrEmpty())
                    {
                        errors.Add("\"Р/С\" для счета обязателен для заполнения");
                    }
                    
                    if (invoice.SumOpl.HasValue &&
                        invoice.StatusCode != InvoiceStatus.Denied &&
                        invoice.StatusCode != InvoiceStatus.DeniedAgreed &&
                        invoice.StatusCode != InvoiceStatus.ErrorInDetails)
                    {
                        overallSumOpl += invoice.SumOpl.Value;
                    }

                    if (invoice.PartDog.HasValue &&
                        invoice.StatusCode != InvoiceStatus.Denied &&
                        invoice.StatusCode != InvoiceStatus.DeniedAgreed &&
                        invoice.StatusCode != InvoiceStatus.ErrorInDetails)
                    {
                        overallPartDog += invoice.PartDog.Value;
                    }

                    OMDamage invoiceDamage = OMDamage.Where(x => x.EmpId == invoice.DamageId).Select(x => x.DamageData).ExecuteFirstOrDefault();
                    if(invoice != null && invoiceDamage.DamageData.HasValue && invoice.SvdPolyceDate.HasValue)
                    {
                        var invoiceDamageDate = invoiceDamage.DamageData.Value;
                        var invoiceSvdPolyceDate = invoice.SvdPolyceDate.Value;
                        if(!(invoiceDamageDate >= invoiceSvdPolyceDate && invoiceDamageDate <= invoiceSvdPolyceDate.AddYears(1)))
                        {
                            errors.Add("Внимание! Дата СС не попадает в период действия полиса/свидетельства, требуется изменить дату.");
                        }
                    }
                    else
                    {
                        errors.Add("Внимание! Дата СС не попадает в период действия полиса/свидетельства, требуется изменить дату.");
                    }
                }
                else
                {
                    if (invoice.SubjectName.IsNullOrEmpty())
                    {
                        errors.Add("\"Получатель\" для счета обязателен для заполнения");
                    }
                }
            }

            //CIPJS-704 по ОИ долю и сумму не проверяем
            if (model.InvoiceDamageId.HasValue && !model.Details.Any(x => x.AllPropertyId.HasValue))
            {
                if (overallPartDog > 1)
                {
                    errors.Add("Внимание! Установлена некорректная доля в праве счетов! Максимальная сумма доля в праве 1. Сохранение счета невозможно!");
                }
                if (maxSumOpl < overallSumOpl)
                {
                    errors.Add($"Внимание! Вы ввели некорректную сумму счета, превышение максимально возможной суммы равно = {overallSumOpl - maxSumOpl:N2}! Сохранение счета невозможно!");
                }
            }

            return errors;
        }

        /// <summary>
        /// Смена статуса счета на "Согласован"
        /// </summary>
        /// <param name="id">Идентифиикатор счета</param>
        public void Agreed(long? id)
        {
            if (id.HasValue)
            {
                var invoice = OMInvoice.Where(x => x.EmpId == id).SelectAll().Execute().FirstOrDefault();
                invoice.DateAgree = DateTime.Now;
                invoice.UserAgreeId = SRDSession.GetCurrentUserId();

                //CIPJS-434 При процессе согласования счетов ( этот функционал может вызываться как с карточки, так и по кнопке "Согласовать счета" в представлениях со счетами.
                //Счет в статусе "Отказано в выплате" переводить не в статус "Согласовано", а в статус ""Отказано в выплате / Согласован"
                if (invoice.Status_Code == InvoiceStatus.Denied)
                {
                    invoice.Status_Code = InvoiceStatus.DeniedAgreed;
                }
                else if(invoice.Status_Code == InvoiceStatus.Formed)
                {
                    invoice.Status_Code = InvoiceStatus.Agreed;
                }
                invoice.Save();

                //CIPJS-694 статус в INSUR_INVIOICE изменяет и статус записи в INSUR_INVOICE_SVOD
                if (invoice.LinkInvoiceSvod.HasValue)
                {
                    OMInvoiceSvod svod = OMInvoiceSvod.Where(x => x.EmpId == invoice.LinkInvoiceSvod.Value)
                        .Select(x => x.Status)
                        .Select(x => x.Status_Code)
                        .ExecuteFirstOrDefault();

                    if (svod != null)
                    {
                        svod.Status_Code = InvoiceStatus.Agreed;
                        svod.Save();
                    }
                }
            }
        }

        /// <summary>
        /// Смена статуса счетов на "Согласован"
        /// </summary>
        /// <param name="ids">Список идентификаторов счетов</param>
        public void Agreed(List<long?> ids)
        {
            if (ids.IsNotEmpty())
            {
                var invoiceList = OMInvoice.Where(x => ids.Contains(x.EmpId)).SelectAll().Execute();

                invoiceList.ForEach(c => 
                {
                    c.DateAgree = DateTime.Now;
                    c.UserAgreeId = SRDSession.GetCurrentUserId();
                    //CIPJS-434 При процессе согласования счетов ( этот функционал может вызываться как с карточки, так и по кнопке "Согласовать счета" в представлениях со счетами.
                    //Счет в статусе "Отказано в выплате" переводить не в статус "Согласовано", а в статус ""Отказано в выплате / Согласован"
                    if (c.Status_Code == InvoiceStatus.Denied)
                    {
                        c.Status_Code = InvoiceStatus.DeniedAgreed;
                    }
                    else if (c.Status_Code == InvoiceStatus.Formed)
                    {
                        c.Status_Code = InvoiceStatus.Agreed;
                    }
                    
                    c.Save();
                });

                //CIPJS-694 статус в INSUR_INVIOICE изменяет и статус записи в INSUR_INVOICE_SVOD
                List<long> invoiceSvodIds = invoiceList.Where(x => x.LinkInvoiceSvod.HasValue).Select(x => x.LinkInvoiceSvod.Value).ToList();
                if (invoiceSvodIds.Count > 0)
                {
                    List<OMInvoiceSvod> svodList = OMInvoiceSvod.Where(x => invoiceSvodIds.Contains(x.EmpId))
                        .Select(x => x.Status)
                        .Select(x => x.Status_Code)
                        .Execute();

                    foreach(OMInvoiceSvod svod in svodList)
                    {
                        svod.Status_Code = InvoiceStatus.Agreed;
                        svod.Save();
                    }
                }
            }
        }

        /// <summary>
        /// Проверка счета при создании. Есть ограничения по созданию счета
        /// </summary>
        /// <param name="damageId"></param>
        /// <param name="allPropertyId"></param>
        /// <returns>Максимальную сумму, на которую можно создать счет</returns>
        public InvoiceDto CheckInvoice(long? damageId, long? allPropertyId)
        {
            if (allPropertyId.HasValue)
            {
                OMAllProperty allProperty = OMAllProperty.Where(x => x.EmpId == allPropertyId).Select(x => x.RasPripay).Select(x => x.PartCity).Execute().FirstOrDefault();
                if (allProperty != null)
                {
                    if ((allProperty.PartCity ?? 0) == 0)
                    {
                        throw new Exception("Внимание! Параметр \"Доля города Москвы в праве на общее имущество\" не заполнен, создание счета невозможно!");
                    }

                    decimal? Nmax = allProperty.RasPripay * allProperty.PartCity / 100;

                    decimal? oplDog = OMInputPlat.Where(x => x.LinkAllPropertyId == allPropertyId).Select(x => x.SumOpl).Execute().Sum(x => x.SumOpl);

                    decimal? invoiceSum = OMInvoice.Where(x => x.LinkAllProperty == allPropertyId && (x.Status_Code != InvoiceStatus.ErrorInDetails || x.Status_Code != InvoiceStatus.Denied))
                        .Select(x => x.SumOpl).Execute().Sum(x => x.SumOpl);

                    decimal sumToPay = 0;

                    if (allProperty.RasPripay == oplDog)
                    {
                        sumToPay = Math.Round((Nmax ?? 0) - (invoiceSum ?? 0), 2);
                    }
                    //CIPJS-136 Если INSUR_ALL_PROPERTY.RAS_PRIPAY ( размер годовой премии) > OPL_DOG, то
                    //считаем ДОЛЮ, сколько уже выплачено от суммы премии = OPL_DOG / INSUR_ALL_PROPERTY.RAS_PRIPAY(округлили до 2 - х знаков после запятой)
                    //СУММА СЧЕТА = ДОЛЯ * Nmax – OPL_max
                    else if (allProperty.RasPripay > oplDog)
                    {
                        decimal partDog = (oplDog / allProperty.RasPripay) ?? 0;
                        sumToPay = Nmax.HasValue && invoiceSum.HasValue ? Math.Round(partDog * (Nmax.Value - invoiceSum.Value), 2) : 0m;
                    }

                    if (sumToPay == 0)
                    {
                        throw new Exception("Счет не может быть сформирован, все выплаты произведены или находятся в состоянии переданном на оплату");
                    }

                    return new InvoiceDto
                    {
                        SumOpl = sumToPay
                    };
                }
            }
            if (damageId.HasValue)
            {
                OMDamage damage = OMDamage.Where(x => x.EmpId == damageId).SelectAll().Execute().FirstOrDefault();

                if (damage != null)
                {
                    decimal? area = null;
                    long? selectedFspId = null;
                    long? selectedAllPropertyId = null;
                    //CIPJS-413 "ДОЛЯ ФАКТИЧЕСКАЯ"
                    decimal? selectedFspPartSum = 0;
                    bool haveNoInsurContract = false;

                    decimal? partDog;
                    decimal? sumToPay;

                    switch (damage.TypeDoc_Code)
                    {
                        case ContractType.Dwelling:
                            OMFlat flat = OMFlat.Where(x => x.EmpId == damage.ObjId).Select(x => x.Fopl).Execute().FirstOrDefault();
                            if (flat != null)
                            {
                                area = flat.Fopl;
                            }
                            break;
                        case ContractType.CommonOwnership:
                            area = OMBuilding.Where(x => x.EmpId == damage.ObjId).Select(x => x.Opl).ExecuteFirstOrDefault()?.Opl;
                            break;
                    }
                    if (!area.HasValue)
                    {
                        throw new Exception("Невозможно сформировать счет, необходимо заполнить площадь в объекте");
                    }

                    List<DamageAnalysisContractDto> contracts = new DamageAnalysisService().GetDamageContracts(damage.EmpId, area.Value);

                    List<OMInvoice> invoices = OMInvoice.Where(x => x.LinkDamage == damageId && x.Status_Code != InvoiceStatus.ErrorInDetails && x.Status_Code != InvoiceStatus.Denied && x.Status_Code != InvoiceStatus.DeniedAgreed)
                        .Select(x => x.SumOpl)
                        .Select(x => x.LinkFsp)
                        .Select(x => x.PartDog)
                        .Execute();

                    //CIPJS-413 в новый счет подкидывать номер договора , любой, который не найден в п.4 , 
                    //если уже для всех договоров созданы счета/ не созданы вообще , 
                    //то подкидывать первый по списку
                    if (damage.TypeDoc_Code == ContractType.Dwelling)
                    {
                        selectedFspId = contracts.FirstOrDefault(x => x.FspId.HasValue && x.IsPaid && !invoices.Any(y => y.LinkFsp == x.FspId.Value))?.FspId ??
                            contracts.FirstOrDefault(x => x.FspId.HasValue && !invoices.Any(y => y.LinkFsp == x.FspId.Value))?.FspId ??
                            contracts.FirstOrDefault(x => x.FspId.HasValue)?.FspId;

                        if (contracts.Count == 1)
                        {
                            selectedFspPartSum = 1;
                        }
                        else
                        {
                            foreach (DamageAnalysisContractDto contract in contracts)
                            {
                                if (contract.FspId != selectedFspId)
                                {
                                    continue;
                                }

                                if (!contract.PartInRoom.HasValue)
                                {
                                    throw new Exception("Невозможно сформировать счет, необходимо заполнить площадь в Договоре страхования");
                                }

                                if (contract.IsPaid)
                                {

                                    selectedFspPartSum = contract.PartInRoom.Value;
                                }
                                else
                                {
                                    haveNoInsurContract = true;
                                }
                            }
                        }
                        if (selectedFspPartSum > 1)
                        {
                            throw new Exception("Невозможно сформировать счет, необходимо заполнить доли по договору в разделе Договора страхования");
                        }

                        //CIPJS-413 Определяем " ДОПУСТИМЫЙ РАЗМЕР ДОЛИ" = 1- СУММА долей в праве для счетов в статусе "Создан"/"Включен в реестр оплат"/"Передан на оплату" для этого дела
                        //ЕСЛИ "ДОЛЯ ФАКТИЧЕСКАЯ" <= ДОПУСТИМЫЙ РАЗМЕР ДОЛИ, то подкидываем с счет "размер доли " = "ДОЛЯ ФАКТИЧЕСКАЯ" и для нее считаем СУММУ СЧЕТА
                        //ЕСЛИ "ДОЛЯ ФАКТИЧЕСКАЯ" > ДОПУСТИМЫЙ РАЗМЕР ДОЛИ, то подкидываем с счет "размер доли " = "ДОПУСТИМЫЙ РАЗМЕР ДОЛИ" и для нее считаем СУММУ СЧЕТАdecimal availablePartDog = 1;
                        decimal availablePartDog = availablePartDog = 1 - (invoices.Sum(x => x.PartDog) ?? 0);

                        partDog = selectedFspPartSum > availablePartDog ? availablePartDog : selectedFspPartSum;
                        sumToPay = (partDog ?? 0) * damage.SumDamage * damage.PartTown / 100;
                    }
                    else
                    {
                        DamageAnalysisContractDto contract =
                            contracts.FirstOrDefault(x => x.AllPropertyId.HasValue && x.IsPaid && !invoices.Any(y => y.LinkAllProperty == x.AllPropertyId.Value)) ??
                            contracts.FirstOrDefault(x => x.AllPropertyId.HasValue && !invoices.Any(y => y.LinkAllProperty == x.AllPropertyId.Value)) ??
                            contracts.FirstOrDefault(x => x.AllPropertyId.HasValue);
                        
                        selectedAllPropertyId = contract?.AllPropertyId;
                        partDog = 1;
                        sumToPay = damage.SumDamage * (damage.PartTown ?? 75m) / 100;
                    }

                    if (sumToPay == 0)
                    {
                        if (haveNoInsurContract)
                        {
                            Save(new InvoiceListDto
                            {
                                InvoiceDamageId = damageId,
                                Details = new List<InvoiceDto>
                                {
                                    InvoiceDto.CreateNew(null, null, null, null, null, InvoiceStatus.Denied)
                                }
                            });

                            return null;
                        }
                        else
                        {
                            throw new Exception("Счет не может быть сформирован, все выплаты произведены или находятся в состоянии переданном на оплату");
                        }
                    }

                    return new InvoiceDto
                    {
                        PartDog = partDog,
                        SumOpl = sumToPay,
                        FspId = selectedFspId,
                        AllPropertyId = selectedAllPropertyId,
                        NumInvoice = damage.NomDoc,
                        Sort = invoices.Count,
                        InsurSymmaOtchet = damage.EstimatedValue
                    };
                }
            }

            return null;
        }

        /// <summary>
        /// Удаляет счет
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void Delete(long? id)
        {
            if (!id.HasValue) return;

            OMInvoice.Where(x => x.EmpId == id).ExecuteFirstOrDefault()?.Destroy();
        }

        /// <summary>
        /// Удаляет список счетов
        /// </summary>
        /// <param name="ids">Список идентификаторов</param>
        public void Delete(List<long> ids)
        {
            if (ids.IsEmpty()) return;

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                OMInvoice.Where(x => ids.Contains(x.EmpId))
                         .Execute()
                         .ForEach(x => 
                            {
                                x.Destroy();
                                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Удален счет", OMInvoice.GetRegisterId(), x.EmpId);
                            });

                ts.Complete();
            }
        }

        public List<InvoiceDto> GetSummaries(long? damageId, long? allPropertyId)
        {
            return Get(damageId, allPropertyId, true).Details
                .Where(x => x.StatusCode != InvoiceStatus.Denied 
                && x.StatusCode != InvoiceStatus.DeniedAgreed
                && x.StatusCode != InvoiceStatus.ErrorInDetails)
                .ToList();
        }

        public List<InvoiceDto> UpdateInvoiceSums(InvoiceListDto invoiceListDto)
        {
            List<InvoiceDto> invoiceSums = new List<InvoiceDto>();

            decimal maxSumOpl = 0;

            if (invoiceListDto != null && invoiceListDto.Details != null && invoiceListDto.Details.Count > 0)
            {
                if (invoiceListDto.SumDamage.HasValue && invoiceListDto.PartTown.HasValue)
                {
                    maxSumOpl = Math.Round(invoiceListDto.SumDamage.Value * invoiceListDto.PartTown.Value / 100, 2, MidpointRounding.AwayFromZero);
                }

                decimal paidSum = invoiceListDto.Details.Where(x => (x.StatusCode == InvoiceStatus.TransferredPayment || x.StatusCode == InvoiceStatus.Included)).Sum(x => x.SumOpl) ?? 0;

                foreach (InvoiceDto invoice in invoiceListDto.Details)
                {
                    if (invoice.GuidId.IsNullOrEmpty())
                    {
                        continue;
                    }
                    if (!invoice.PartDog.HasValue)
                    {
                        continue;
                    }
                    if (invoice.StatusCode != InvoiceStatus.None && invoice.StatusCode != InvoiceStatus.Formed)
                    {
                        continue;
                    }

                    if (maxSumOpl > paidSum)
                    {
                        decimal invoicePaySum = Math.Round((maxSumOpl - paidSum) * invoice.PartDog.Value, 2, MidpointRounding.AwayFromZero);
                        invoiceSums.Add(new InvoiceDto
                        {
                            GuidId= invoice.GuidId,
                            SumOpl = invoicePaySum
                        });
                        paidSum += invoicePaySum;
                    }
                }
            }
            return invoiceSums;
        }

        public decimal? CalcUndestributedAmount(decimal? partTownSum, decimal?[] invoiceSums)
        {
            if (partTownSum.HasValue && invoiceSums != null)
            {
                return partTownSum.Value - invoiceSums.Sum() ?? 0;
            }

            return null;
        }

        /// <summary>
        /// Получение всех счетов
        /// </summary>
        /// <param name="damageId"></param>
        /// <param name="allPropertyId"></param>
        /// <returns></returns>
        private List<InvoiceDto> GetInovices(long? damageId, long? allPropertyId, bool readOnly, bool isFisical)
        {
            List<InvoiceDto> list = new List<InvoiceDto>();
            List<OMInvoice> invoices = null;
            if (damageId.HasValue)
            {
                invoices = OMInvoice.Where(x => x.LinkDamage == damageId).OrderBy(x => x.DataInput).SelectAll(false).Execute().ToList();
            }
            if (allPropertyId.HasValue)
            {
                invoices = OMInvoice.Where(x => x.LinkAllProperty == allPropertyId).OrderBy(x => x.DataInput).SelectAll(false).Execute().ToList();
            }
            if (invoices != null)
            {
                foreach (OMInvoice invoice in invoices)
                {
                    InvoiceDto detail = InvoiceDto.OMMap(invoice);
                    detail.ReadOnly = readOnly;
                    detail.IsFisical = isFisical;
                    if (detail.StatusCode != InvoiceStatus.Formed && detail.StatusCode != InvoiceStatus.Denied) { detail.ReadOnly = true; }
                    list.Add(detail);
                }
            }

            return list;
        }

        public string GetAddress(long allPropertyId)
        {
            return OMAllProperty.Where(x => x.EmpId == allPropertyId)
                .Select(x => x.ParentBuilding.ParentAddress.FullAddress)
                .ExecuteFirstOrDefault()?.ParentBuilding?.ParentAddress?.FullAddress;
        }
    }
}
