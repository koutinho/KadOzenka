using CIPJS.DAL.FileStorage;
using Core.Numerator;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.SRD.DAL;
using Microsoft.AspNetCore.Http;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using Core.Register.QuerySubsystem;
using System.Text.RegularExpressions;

namespace CIPJS.DAL.ReestrPay
{
    public class ReestrPayService
    {
        private readonly FileStorageService _fileStorageService;

        public ReestrPayService(FileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public List<OMInvoice> GetInvoices(List<long?> ids)
        {
            List<OMInvoice> omInvoices = OMInvoice.Where(x => ids.Contains(x.EmpId))
                .SelectAll()
                .Select(x => x.ParentDamage.NomDoc)
                .Select(x => x.ParentDamage.NomDate)
                .Select(x => x.ParentDamage.SumDamage)
                .Select(x => x.ParentAllProperty.Ndog)
                .Select(x => x.ParentAllProperty.Ndogdat)
                .Select(x => x.ParentAllProperty.RasPripay)
                .Execute();
            return omInvoices;
        }

        public List<OMInvoice> GetInvoices(List<long?> ids, ReestrPayType type)
        {
            if (ids == null || !ids.Any()) throw new Exception("Необходимо выбрать, как минимум одну строку");

            List<OMInvoice> omInvoices;
            if (type == ReestrPayType.DamageGP || type == ReestrPayType.DamageOI)
            {
                omInvoices = OMInvoice.Where(x => ids.Contains(x.EmpId) && x.Status_Code == InvoiceStatus.Agreed)
                    .Select(x => x.ParentDamage.NomDoc)
                    .Select(x => x.ParentDamage.NomDate)
                    .Select(x => x.ParentDamage.SumDamage)
                    .Select(x => x.DataZakluchenia)
                    .Select(x => x.SubjectName)
                    .Select(x => x.SumOpl)
                    .Select(x => x.BicBank)
                    .Select(x => x.BankName)
                    .Select(x => x.RachAcc)
                    .Select(x => x.Status)
                    .SetOrder(new List<QSOrder>
                    {
                        new QSOrder
                        {
                            Column = new QSColumnFunctionExternal(
                                "get_313_doc_nom_for_sort",
                                "doc_nom_sort",
                                OMDamage.GetColumn(x => x.NomDoc))
                        }
                    })
                    .Execute();
            }
            else if (type == ReestrPayType.ReturnBonusOI)
            {
                omInvoices = OMInvoice.Where(x => ids.Contains(x.EmpId) && x.Status_Code == InvoiceStatus.Agreed)
                    .Select(x => x.ParentAllProperty.Ndog)
                    .Select(x => x.ParentAllProperty.Ndogdat)
                    .Select(x => x.ParentAllProperty.RasPripay)
                    .Select(x => x.SubjectName)
                    .Select(x => x.SumOpl)
                    .Select(x => x.NumInvoice)
                    .Select(x => x.dateInvoice)
                    .Select(x => x.BicBank)
                    .Select(x => x.BankName)
                    .Select(x => x.RachAcc)
                    .Select(x => x.Status)
                    .Execute();
            }
            else
            {
                throw new ArgumentException("Wrong ReestrPayType", nameof(type));
            }

            if (!omInvoices.Any())
                throw new Exception("Нет ни одного счета в статусе \"Согласован счет\"");
            return omInvoices;
        }

        public List<OMInvoiceSvod> GetInvoiceSvods(List<long?> ids, ReestrPayType type)
        {
            if (ids == null || !ids.Any()) throw new Exception("Необходимо выбрать, как минимум одну строку");

            List<OMInvoiceSvod> invoiceSvodList;
            if (type == ReestrPayType.DamageGP || type == ReestrPayType.DamageOI || type == ReestrPayType.ReturnBonusOI)
            {
                invoiceSvodList = OMInvoiceSvod.Where(x => ids.Contains(x.EmpId) && x.Status_Code == InvoiceStatus.Agreed)
                    .SelectAll()
                    .OrderBy(x => x.DateInvoice)
                    .Execute();
            }
            else
            {
                throw new ArgumentException("Wrong ReestrPayType", nameof(type));
            }

            if (!invoiceSvodList.Any())
                throw new Exception("Нет ни одного сводного счета в статусе \"Согласован счет\"");
            return invoiceSvodList;
        }

        public OMReestrPay Form(List<long?> ids, ReestrPayType type)
        {
            OMReestrPay reestPay = null;

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                reestPay = new OMReestrPay
                {
                    Num = GetNumOfReestPay(type),
                    Date = DateTime.Now,
                    DataCreation = DateTime.Now,
                    UserCreation = SRDSession.GetCurrentUsername(),
                    Status_Code = ReestrPayStatus.Formed,
                    Type_Code = type
                };

                reestPay.Save();
                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Создана оплата в системе ОПС", OMReestrPay.GetRegisterId(), reestPay.EmpId);

                //CIPJS-694 статус в INSUR_INVIOICE изменяет и статус записи в INSUR_INVOICE_SVOD
                List<long> invoiceSvodIds = null;

                if (type == ReestrPayType.DamageGP || type == ReestrPayType.DamageOI)
                {
                    List<OMInvoice> omInvoices = OMInvoice.Where(x => ids.Contains(x.EmpId) && x.Status_Code == InvoiceStatus.Agreed)
                        .Select(x => x.ParentDamage.EmpId)
                        .Select(x => x.ParentDamage.DamageAmountStatus)
                        .Select(x => x.ParentDamage.DamageAmountStatus_Code)
                        .Select(x => x.LinkInvoiceSvod)
                        .Select(x => x.Status)
                        .Select(x => x.Status_Code)
                        .Execute();
                    
                    invoiceSvodIds = omInvoices.Where(x => x.LinkInvoiceSvod.HasValue).Select(x => x.LinkInvoiceSvod.Value).ToList();

                    foreach (OMInvoice omInvoice in omInvoices)
                    {
                        omInvoice.LinkReestrPay = reestPay.EmpId;
                        omInvoice.Status_Code = InvoiceStatus.Included;
                        omInvoice.Save();
                        SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен счет", OMInvoice.GetRegisterId(), omInvoice.EmpId);

                        if (omInvoice.ParentDamage != null)
                        {
                            omInvoice.ParentDamage.DamageAmountStatus_Code = StatusDamageAmount.PaymentRegisterFormed;
                            omInvoice.ParentDamage.Save();
                            SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменено дело по расчету суммы ущерба", OMDamage.GetRegisterId(), omInvoice.ParentDamage.EmpId);
                        }
                    }
                }
                else if (type == ReestrPayType.ReturnBonusOI)
                {
                    List<OMInvoice> omInvoices = OMInvoice.Where(x => ids.Contains(x.EmpId) && x.Status_Code == InvoiceStatus.Agreed)
                        .Select(x => x.ParentAllProperty.EmpId)
                        .Select(x => x.ParentAllProperty.Status)
                        .Select(x => x.ParentAllProperty.Status_Code)
                        .Select(x => x.LinkInvoiceSvod)
                        .Select(x => x.Status)
                        .Select(x => x.Status_Code)
                        .Execute();
                    
                    invoiceSvodIds = omInvoices.Where(x => x.LinkInvoiceSvod.HasValue).Select(x => x.LinkInvoiceSvod.Value).ToList();

                    foreach (OMInvoice omInvoice in omInvoices)
                    {
                        omInvoice.LinkReestrPay = reestPay.EmpId;
                        omInvoice.Status_Code = InvoiceStatus.Included;
                        omInvoice.Save();
                        SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен счет", OMInvoice.GetRegisterId(), omInvoice.EmpId);

                        if (omInvoice.ParentAllProperty != null)
                        {
                            omInvoice.ParentAllProperty.Status_Code = ContractStatus.Formed;
                            omInvoice.ParentAllProperty.Save();
                            SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен договор", OMAllProperty.GetRegisterId(), omInvoice.ParentAllProperty.EmpId);
                        }
                    }
                }

                //CIPJS-694 статус в INSUR_INVIOICE изменяет и статус записи в INSUR_INVOICE_SVOD
                if (invoiceSvodIds.Count > 0)
                {
                    List<OMInvoiceSvod> svodList = OMInvoiceSvod.Where(x => invoiceSvodIds.Contains(x.EmpId))
                        .Select(x => x.Status)
                        .Select(x => x.Status_Code)
                        .Execute();

                    foreach (OMInvoiceSvod svod in svodList)
                    {
                        svod.Status_Code = InvoiceStatus.Included;
                        svod.Save();
                    }
                }

                ts.Complete();
            }

            return reestPay;
        }

        public OMReestrPay FormSvod(List<long?> ids, ReestrPayType type)
        {
            OMReestrPay reestPay = null;

            if (ids == null || ids.Count == 0)
            {
                throw new Exception("Не выбрано ни одной строки для формирования реестра выплат по сводным счетам");
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
               
                reestPay = new OMReestrPay
                {
                    Num = GetNumOfReestPay(type),
                    Date = DateTime.Now,
                    DataCreation = DateTime.Now,
                    UserCreation = SRDSession.GetCurrentUsername(),
                    Status_Code = ReestrPayStatus.Formed,
                    Type_Code = type
                };

                reestPay.Save();
                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Создана оплата в системе ОПС", OMReestrPay.GetRegisterId(), reestPay.EmpId);

                List<OMInvoiceSvod> svodList = OMInvoiceSvod.Where(x => ids.Contains(x.EmpId))
                        .SelectAll()
                        .Execute();

                if (svodList.Count == 0)
                {
                    throw new Exception("Не удалось определить ни одной записи сводного счета с переданными идентификаторами");
                }

                List<long?> invoiceSvodIds = svodList.Select(x => (long?)x.EmpId).ToList();

                foreach (OMInvoiceSvod invoiceSvod in svodList)
                {
                    invoiceSvod.LinkReestrPay = reestPay.EmpId;
                    invoiceSvod.Status_Code = InvoiceStatus.Included;
                    invoiceSvod.Save();
                }

                if (type == ReestrPayType.DamageGP || type == ReestrPayType.DamageOI)
                {
                    List<OMInvoice> omInvoices = OMInvoice.Where(x => invoiceSvodIds.Contains(x.LinkInvoiceSvod) && x.Status_Code == InvoiceStatus.Agreed)
                        .Select(x => x.ParentDamage.EmpId)
                        .Select(x => x.ParentDamage.DamageAmountStatus)
                        .Select(x => x.ParentDamage.DamageAmountStatus_Code)
                        .Select(x => x.LinkInvoiceSvod)
                        .Select(x => x.Status)
                        .Select(x => x.Status_Code)
                        .Execute();

                    foreach (OMInvoice omInvoice in omInvoices)
                    {
                        omInvoice.LinkReestrPay = reestPay.EmpId;
                        omInvoice.Status_Code = InvoiceStatus.Included;
                        omInvoice.Save();
                        SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен счет", OMInvoice.GetRegisterId(), omInvoice.EmpId);

                        if (omInvoice.ParentDamage != null)
                        {
                            omInvoice.ParentDamage.DamageAmountStatus_Code = StatusDamageAmount.PaymentRegisterFormed;
                            omInvoice.ParentDamage.Save();
                            SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменено дело по расчету суммы ущерба", OMDamage.GetRegisterId(), omInvoice.ParentDamage.EmpId);
                        }
                    }
                }
                else if (type == ReestrPayType.ReturnBonusOI)
                {
                    List<OMInvoice> omInvoices = OMInvoice.Where(x => invoiceSvodIds.Contains(x.LinkInvoiceSvod) && x.Status_Code == InvoiceStatus.Agreed)
                        .Select(x => x.ParentAllProperty.EmpId)
                        .Select(x => x.ParentAllProperty.Status)
                        .Select(x => x.ParentAllProperty.Status_Code)
                        .Select(x => x.LinkInvoiceSvod)
                        .Select(x => x.Status)
                        .Select(x => x.Status_Code)
                        .Execute();

                    foreach (OMInvoice omInvoice in omInvoices)
                    {
                        omInvoice.LinkReestrPay = reestPay.EmpId;
                        omInvoice.Status_Code = InvoiceStatus.Included;
                        omInvoice.Save();
                        SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен счет", OMInvoice.GetRegisterId(), omInvoice.EmpId);

                        if (omInvoice.ParentAllProperty != null)
                        {
                            omInvoice.ParentAllProperty.Status_Code = ContractStatus.Formed;
                            omInvoice.ParentAllProperty.Save();
                            SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен договор", OMAllProperty.GetRegisterId(), omInvoice.ParentAllProperty.EmpId);
                        }
                    }
                }

                ts.Complete();
            }

            return reestPay;
        }

        private string GetNumOfReestPay(ReestrPayType type)
        {
            OMReestrPay previous = OMReestrPay.Where(x => x.Num != null && x.Type_Code == (type == ReestrPayType.DamageOI ? ReestrPayType.DamageGP : type)).OrderByDescending(x => x.EmpId).SelectAll().ExecuteFirstOrDefault();
            var num = previous != null && previous?.Status_Code == ReestrPayStatus.Disbaned ?
                new Regex(@"^\d{1,}").Match(previous.Num)?.Value :
                Generator.GetRegNumber($"<root><Year>{DateTime.Now.Year}</Year><FormType>{(type == ReestrPayType.DamageOI ? (long)ReestrPayType.DamageGP : (long)type)}</FormType></root>", 354);
            return num;
        }


        public List<OMReestrPay> GetReestrPaysByIds(List<long> ids)
        {
            if (ids == null || !ids.Any()) return new List<OMReestrPay>();

            return OMReestrPay.Where(x => ids.Contains(x.EmpId)).Select(x => x.Status_Code).Select(x => x.Date).Execute();
        }

        public void ChangeStatus(ChangeStatusDto dto)
        {
            if (dto.Ids == null || !dto.Ids.Any() || dto.Status == ReestrPayStatus.None) return;

            if(dto.Status == ReestrPayStatus.ApprovedDGI && dto.File == null)
            {
                throw new Exception("Прикрепление файла обязательно при установлении статуса \"Утвержден в ДГИ\"");
            }
            if (dto.Status == ReestrPayStatus.TransferredPayment && dto.File == null)
            {
                throw new Exception("Прикрепление файла обязательно при установлении статуса \"Передано в оплату\"");
            }

            List<OMReestrPay> omReestrPays = OMReestrPay
                .Where(x => dto.Ids.Contains(x.EmpId))
                .Select(x => x.Note)
                .Select(x => x.Type_Code)
                .Select(x => x.Status_Code)
                .Select(x => x.Date)
                .Execute();
            ReestrPayType reestrPayType = omReestrPays.First().Type_Code;
            List<OMInvoice> omInvoices = OMInvoice
                .Where(x => dto.Ids.Contains((long) x.LinkReestrPay))
                .Select(x => x.LinkDamage)
                .Select(x => x.LinkAllProperty)
                .Select(x => x.LinkReestrPay)
                .Execute();
            List<long?> entityIds;
            List<OMDamage> omDamages = null;
            List<OMAllProperty> omAllProperties = null;
            List<OMInvoice> allOMInvoice = null;

            if (reestrPayType == ReestrPayType.DamageGP || reestrPayType == ReestrPayType.DamageOI)
            {
                entityIds = omInvoices.Where(x => x.LinkDamage.HasValue).Select(x => x.LinkDamage).ToList();
                omDamages = entityIds.Any() ? OMDamage.Where(x => entityIds.Contains(x.EmpId)).Execute() : new List<OMDamage>();
                allOMInvoice = entityIds.Any() ? OMInvoice.Where(x => entityIds.Contains(x.LinkDamage)).Select(x => x.LinkDamage).Execute() : new List<OMInvoice>();
            }
            else if (reestrPayType == ReestrPayType.ReturnBonusOI)
            {
                entityIds = omInvoices.Where(x => x.LinkAllProperty.HasValue).Select(x => x.LinkAllProperty).ToList();
                omAllProperties = entityIds.Any() ? OMAllProperty.Where(x => entityIds.Contains(x.EmpId)).Execute() : new List<OMAllProperty>();
                allOMInvoice = entityIds.Any() ? OMInvoice.Where(x => entityIds.Contains(x.LinkAllProperty)).Select(x => x.LinkAllProperty).Execute() : new List<OMInvoice>();
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                long? fileId = null;

                if (dto.File != null)
                {
                    using (Stream stream = dto.File.OpenReadStream())
                    {
                        fileId = _fileStorageService.Save(stream, dto.File.FileName);
                        SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Создан файл", OMFileStorage.GetRegisterId(), fileId);
                    }
                }

                foreach (OMReestrPay omReestrPay in omReestrPays)
                {
                    omReestrPay.Status_Code = dto.Status;
                    omReestrPay.Date = dto.Date ?? omReestrPay.Date;

                    if (fileId.HasValue)
                    {
                        if (dto.Status == ReestrPayStatus.ApprovedDGI)
                        {
                            omReestrPay.FileStorageIdDGI = fileId;
                        }
                        if (dto.Status == ReestrPayStatus.TransferredPayment)
                        {
                            omReestrPay.FileStorageIdPay = fileId;
                        }
                    }

                    if (!dto.Note.IsNullOrEmpty())
                    {
                        omReestrPay.Note = omReestrPay.Note.IsNullOrEmpty() ? dto.Note : $"{omReestrPay.Note} {dto.Note}";
                    }

                    omReestrPay.Save();
                    SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменена оплата в системе ОПС", OMReestrPay.GetRegisterId(), omReestrPay.EmpId);

                    if (dto.Status == ReestrPayStatus.TransferredPayment)
                    {
                        List<OMInvoice> targetOMInvoices = omInvoices.Where(x => x.LinkReestrPay == omReestrPay.EmpId).ToList();

                        foreach (OMInvoice omInvoice in targetOMInvoices)
                        {
                            omInvoice.Status_Code = InvoiceStatus.TransferredPayment;
                            omInvoice.Save();
                            SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен счет", OMInvoice.GetRegisterId(), omInvoice.EmpId);

                            if (reestrPayType == ReestrPayType.DamageGP || reestrPayType == ReestrPayType.DamageOI)
                            {
                                OMDamage omDamage = omDamages.FirstOrDefault(x => x.EmpId == omInvoice.LinkDamage);

                                if (omDamage != null)
                                {
                                    if (allOMInvoice.Count(x => x.LinkDamage == omDamage.EmpId) == targetOMInvoices.Count(x => x.LinkDamage == omDamage.EmpId))
                                    {
                                        omDamage.DamageAmountStatus_Code = StatusDamageAmount.FullPaid;
                                    }
                                    else
                                    {
                                        omDamage.DamageAmountStatus_Code = StatusDamageAmount.PartPaid;
                                    }
                                    omDamage.Save();
                                    SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменено дело по расчету суммы ущерба", OMDamage.GetRegisterId(), omDamage.EmpId);
                                }
                            }
                            else if (reestrPayType == ReestrPayType.ReturnBonusOI)
                            {
                                OMAllProperty omAllProperty = omAllProperties.FirstOrDefault(x => x.EmpId == omInvoice.LinkAllProperty);

                                if (omAllProperty != null)
                                {
                                    if (allOMInvoice.Count(x => x.LinkAllProperty == omAllProperty.EmpId) == targetOMInvoices.Count(x => x.LinkAllProperty == omAllProperty.EmpId))
                                    {
                                        omAllProperty.Status_Code = ContractStatus.FullPaymentMade;
                                    }
                                    else
                                    {
                                        omAllProperty.Status_Code = ContractStatus.PartialPaymentMade;
                                    }
                                    omAllProperty.Save();
                                    SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен договор", OMAllProperty.GetRegisterId(), omAllProperty.EmpId);
                                }
                            }
                        }
                    }
                }

                ts.Complete();
            }
        }

        public OMFileStorage GetFileDGI(long id)
        {
            long? fileStorageId = OMReestrPay.Where(x => x.EmpId == id).Select(x => x.FileStorageIdDGI).Execute().FirstOrDefault()?.FileStorageIdDGI;

            if (!fileStorageId.HasValue) return null;

            return _fileStorageService.Get(fileStorageId.Value);
        }

        public OMFileStorage GetFilePay(long id)
        {
            long? fileStorageId = OMReestrPay.Where(x => x.EmpId == id).Select(x => x.FileStorageIdPay).Execute().FirstOrDefault()?.FileStorageIdPay;

            if (!fileStorageId.HasValue) return null;

            return _fileStorageService.Get(fileStorageId.Value);
        }

        public void DeletePayReturn(long payId)
        {
            OMReestrPay reestrPay = OMReestrPay.Where(x => x.EmpId == payId).SelectAll().ExecuteFirstOrDefault();
            if (reestrPay == null)
            {
                return;
            }            
  
            List<OMInvoiceSvod> invoiceSvod = OMInvoiceSvod.Where(x => x.LinkReestrPay == payId).SelectAll().Execute();
            List<OMInvoice> invoices = OMInvoice.Where(x => x.ParentInvoiceSvod.LinkReestrPay == payId).SelectAll().Execute();
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                reestrPay.Num = reestrPay.Num != null && reestrPay.Num.Contains("Р") ? reestrPay.Num : $"{reestrPay.Num}-Р";
                reestrPay.DateCancel = DateTime.Now;
                reestrPay.CancelUserId = SRDSession.GetCurrentUserId();
                reestrPay.Status_Code = ReestrPayStatus.Disbaned;
                reestrPay.Save();
                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменена оплата в системе ОПС", OMReestrPay.GetRegisterId(), reestrPay.EmpId);

                foreach (OMInvoice invoice in invoices)
                {
                    if (invoice.Status_Code == InvoiceStatus.Denied)
                    {
                        invoice.Status_Code = InvoiceStatus.DeniedAgreed;
                    }
                    else if (invoice.Status_Code == InvoiceStatus.Formed || invoice.Status_Code == InvoiceStatus.Included)
                    {
                        invoice.Status_Code = InvoiceStatus.Agreed;
                    }
                    invoice.LinkReestrPay = null;
                    invoice.Save();

                    if (invoice.LinkDamage.HasValue)
                    {
                        OMDamage damage = OMDamage.Where(x => x.EmpId == invoice.LinkDamage.Value)
                            .Select(x => x.DamageAmountStatus)
                            .Select(x => x.DamageAmountStatus_Code)
                            .ExecuteFirstOrDefault();

                        if (damage != null)
                        {
                            damage.DamageAmountStatus_Code = StatusDamageAmount.Agreed;
                            damage.Save();
                        }
                    }

                    var allProp = OMAllProperty.Where(x => x.EmpId == invoice.LinkAllProperty).SelectAll().Execute().FirstOrDefault();
                    if (allProp != null)
                    {
                        allProp.Status_Code = ContractStatus.Agreed;                    
                        allProp.Save();
                    }

                    SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен счет", OMInvoice.GetRegisterId(), invoice.EmpId);
                }

                foreach (var i in invoiceSvod)
                {
                    i.Status_Code = InvoiceStatus.Agreed;
                    i.Save();
                }

                //Generator.DecreaseRegNumber($"<root><Year>{DateTime.Now.Year}</Year><FormType>{(reestrPay.Type_Code == ReestrPayType.DamageOI ? (long)ReestrPayType.DamageGP : (long)reestrPay.Type_Code)}</FormType></root>", 354);
                ts.Complete();
            }
        }

        /// <summary>
        /// Расформировывает реестр оплат
        /// </summary>
        /// <param name="payId">Идентифкатор реестра оплат</param>
        public void Delete(long payId)
        {
            //CIPJS-346 Реестру присваивается номер  = № INSUR_REESTR_PAY.NUM + ‘Р’
            //Например, был номер 1 , стал 1-Р(расформирован)
            //INSUR_REESTR_PAY.STATUS(STATUS_CODE) изменить на  «Расформирован»
            //Находим все счета, связанные с реестром, В INSUR_INVOICE найти все записи, для которых
            //INSUR_INVOICE.LINK_ REESTR_PAY = INSUR_REESTR_PAY.EMP_ID  
            //Для всех записей изменяем статус INSUR_INVOICE.STATUS(STATUS_CODE)= Согласован
            //INSUR_REESTR_PAY.DATE_CANCEL – Заполнить дату расформирования реестра
            //NSUR_REESTR_PAY.USER_CANCEL- Заполнить Пользователь, расформировавший реестр
            OMReestrPay reestrPay = OMReestrPay.Where(x => x.EmpId == payId).SelectAll().ExecuteFirstOrDefault();
            if (reestrPay == null)
            {
                return;
            }

            List<OMInvoice> invoices = OMInvoice.Where(x => x.LinkReestrPay == payId).SelectAll().Execute();
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                reestrPay.Num = reestrPay.Num != null && reestrPay.Num.Contains("Р") ? reestrPay.Num : $"{reestrPay.Num}-Р";
                reestrPay.DateCancel = DateTime.Now;
                reestrPay.CancelUserId = SRDSession.GetCurrentUserId();
                reestrPay.Status_Code = ReestrPayStatus.Disbaned;
                reestrPay.Save();
                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменена оплата в системе ОПС", OMReestrPay.GetRegisterId(), reestrPay.EmpId);
                
                foreach (OMInvoice invoice in invoices)
                {
                    //CIPJS-434 При процессе согласования счетов ( этот функционал может вызываться как с карточки, так и по кнопке "Согласовать счета" в представлениях со счетами.
                    //Счет в статусе "Отказано в выплате" переводить не в статус "Согласовано", а в статус ""Отказано в выплате / Согласован"
                    if (invoice.Status_Code == InvoiceStatus.Denied)
                    {
                        invoice.Status_Code = InvoiceStatus.DeniedAgreed;
                    }
                    else if(invoice.Status_Code == InvoiceStatus.Formed || invoice.Status_Code == InvoiceStatus.Included)
                    {
                        invoice.Status_Code = InvoiceStatus.Agreed;
                    }
                    invoice.LinkReestrPay = null;
                    invoice.Save();

                    if (invoice.LinkDamage.HasValue)
                    {
                        OMDamage damage = OMDamage.Where(x => x.EmpId == invoice.LinkDamage.Value)
                            .Select(x => x.DamageAmountStatus)
                            .Select(x => x.DamageAmountStatus_Code)
                            .ExecuteFirstOrDefault();

                        if (damage != null)
                        {
                            damage.DamageAmountStatus_Code = StatusDamageAmount.Agreed;
                            damage.Save();
                        }
                    }

                    SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен счет", OMInvoice.GetRegisterId(), invoice.EmpId);
                }

                //Generator.DecreaseRegNumber($"<root><Year>{DateTime.Now.Year}</Year><FormType>{(reestrPay.Type_Code == ReestrPayType.DamageOI ? (long)ReestrPayType.DamageGP : (long)reestrPay.Type_Code)}</FormType></root>", 354);
                ts.Complete();
            }
        }

        public void ChangeDate(long payId, DateTime? date)
        {
            OMReestrPay reestrPay = OMReestrPay.Where(x => x.EmpId == payId).SelectAll().ExecuteFirstOrDefault();
            reestrPay.Date = date;
            reestrPay.Save();
        }
    }
}
