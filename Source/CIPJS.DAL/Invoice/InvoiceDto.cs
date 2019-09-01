using CIPJS.DAL.Bank;
using CIPJS.DAL.Fsp;
using Core.Shared.Extensions;
using Core.SRD;
using ObjectModel.Core.Reports;
using ObjectModel.Core.SRD;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CIPJS.DAL.Invoice
{
    public class InvoiceDto
    {
        /// <summary>
        /// Уникальный номер для представления
        /// </summary>
        public string GuidId { get; set; }

        public bool IsFisical { get; set; }

        /// <summary>
        /// Уникальный номер записи
        /// </summary>
        public long InvoiceId { get; set; }

        /// <summary>
        /// Номер счета
        /// </summary>
        [Display(Name = "Номер счета")]
        public string NumInvoice { get; set; }

        /// <summary>
        /// Дата счета
        /// </summary>
        [Display(Name = "Дата счета")]
        public DateTime? DateInvoice { get; set; }

        /// <summary>
        /// Заголовок, выводимый в шапке
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// Можно ли редактировать запись
        /// </summary>
        public bool ReadOnly { get; set; }

        #region Связи
        /// <summary>
        /// Связь с делом
        /// </summary>
        public long? DamageId { get; set; }

        /// <summary>
        /// Связь с договором
        /// </summary>
        [Display(Name = "Связать с договором")]
        public long? AllPropertyId { get; set; }

        /// <summary>
        /// Связь с ФСП
        /// </summary>
        [Display(Name = "Связать с договором/ЕПД")]
        public long? FspId { get; set; }
        #endregion

        [Display(Name = "Номер договора")]
        public string ContractNumber { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime? DataInput { get; set; }
        [Display(Name = "Пользователь")]
        public string UserName { get; set; }
        [Display(Name = "Статус счета")]
        public string Status { get; set; }
        [Display(Name = "Сумма к выплате, руб")]
        public decimal? SumOpl { get; set; }
        [Display(Name = "Доля в праве")]
        public decimal? PartDog { get; set; }
        [Display(Name = "Свидетельство/Полис")]
        public string SvidPolycNum { get; set; }
        [Display(Name = "Свидетельство/Полис дата")]
        public DateTime? SvdPolyceDate { get; set; }

        #region Получатель
        public long? SubjectId { get; set; }
        [Display(Name = "Получатель")]
        public string SubjectName { get; set; }
        [Display(Name = "Контактный телефон")]
        public string Phone { get; set; }
        [Display(Name = "ИНН получателя")]
        public string Inn { get; set; }
        [Display(Name = "КПП получателя")]
        public string Kpp { get; set; }
        [Display(Name = "Р/С получателя")]
        public string RachAcc { get; set; }
        [Display(Name = "№ банковской карты")]
        public string BankCardNumber { get; set; }

        [Display(Name = "КБК")]
        public string Kbk { get; set; }
        [Display(Name = "ОКТМО")]
        public string Oktmo { get; set; }
        #endregion

        #region Банк
        public long? BankId { get; set; }
        [Display(Name = "Название банка")]
        public string BankName { get; set; }
        [Display(Name = "ИНН банка")]
        public string InnBank { get; set; }
        [Display(Name = "КПП банка")]
        public string KppBank { get; set; }
        [Display(Name = "БИК")]
        public string BicBank { get; set; }
        [Display(Name = "К/С")]
        public string KorAcc { get; set; }
        #endregion

        [Display(Name = "Причина отказа в выплате")]
        public long? NoPayNoteId { get; set; }
        [Display(Name = "Причина отказа в выплате")]
        public string NoPayNoteReason { get; set; }
        [Display(Name = "Комментарий к счету")]
        public string Comment { get; set; }

        [Display(Name = "Дата согласования")]
        public DateTime? DateAgree { get; set; }
        [Display(Name = "Пользователь")]
        public string UserNameAgree { get; set; }

        [Display(Name = "Расчетная стоимость для Заключения")]
        public decimal? InsurSymmaOtchet { get; set; }

        [Display(Name = "префикс")]
        public decimal? Sort { get; set; }

        [Display(Name = "Дата выпуска Заключения")]
        public DateTime? DateZackluchenia { get; set; }

        public bool InsurSymmaOtchetChanged { get; set; }

        public InvoiceStatus StatusCode { get; set; }
        public bool HasCCCReport { get; set; }

        public static InvoiceDto OMMap(long? Id)
        {
            InvoiceDto model = null;

            if (Id.HasValue)
            {
                OMInvoice entity = OMInvoice.Where(x => x.EmpId == Id)
                      .SelectAll()
                      .Select(x => x.ParentGbuNoPayReason.Reason)
                      .Execute()
                      .FirstOrDefault();

                if (entity != null)
                {
                    model = OMMap(entity);
                }
            }

            if(model == null)
            {
                model = new InvoiceDto
                {
                    InvoiceId = -1,
                    GuidId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6),
                    StatusCode = InvoiceStatus.None
                };
            }

            return model;
        }

        public static InvoiceDto OMMap(OMInvoice entity)
        {
            InvoiceService invoiceService = new InvoiceService();
            FspService fspService = new FspService();
            InvoiceDto model = new InvoiceDto
            {
                InvoiceId = -1,
                GuidId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6),
                StatusCode = InvoiceStatus.None
            };

            if (entity != null)
            {
                string nomDoc = null;
                decimal? estimatedValue = null;

                if (entity.LinkDamage.HasValue)
                {
                    OMDamage damage = OMDamage.Where(x => x.EmpId == entity.LinkDamage.Value)
                        .Select(x => x.NomDoc)
                        .Select(x => x.EstimatedValue)
                        .Select(x => x.DamageData)
                        .ExecuteFirstOrDefault();

                    if (damage != null)
                    {
                        nomDoc = damage.NomDoc;
                        estimatedValue = damage.EstimatedValue;

                        if (entity.LinkFsp.HasValue && damage.DamageData.HasValue)
                        {
                            model.ContractNumber = fspService.GetContractNumber(entity.LinkFsp.Value, damage.DamageData.Value);
                        }
                    }
                }

                if (entity.UserId.HasValue)
                {
                    OMUser user = OMUser.Where(x => x.Id == entity.UserId.Value).Select(x => x.FullNameForDoc).Execute().FirstOrDefault();

                    if (user != null)
                    {
                        model.UserName = user.FullNameForDoc;
                    }
                }

                if (entity.UserAgreeId.HasValue)
                {
                    OMUser user = OMUser.Where(x => x.Id == entity.UserAgreeId.Value).Select(x => x.FullNameForDoc).Execute().FirstOrDefault();

                    if (user != null)
                    {
                        model.UserNameAgree = user.FullNameForDoc;
                    }
                }

                if(entity.NoteNoPayId.HasValue && entity.ParentGbuNoPayReason == null)
                {
                    entity.ParentGbuNoPayReason = OMGbuNoPayReason.Where(x => x.Id == entity.NoteNoPayId).Select(x => x.Reason).Execute().FirstOrDefault();
                }

                string payReestrNum = null;

                if (entity.Status_Code == InvoiceStatus.Included && entity.LinkReestrPay != null)
                {
                    OMReestrPay reestrPay = OMReestrPay.Where(x => x.EmpId == entity.LinkReestrPay)
                        .Select(x => x.Num)
                        .Select(x => x.Date)
                        .ExecuteFirstOrDefault();

                    if (reestrPay != null)
                    {
                        payReestrNum = $" № {reestrPay.Num} от {reestrPay.Date:dd.MM.yyyy}";
                    }
                }

                model.HasCCCReport = OMSavedReport.Where(x => x.Code == 1001 &&
                    x.ObjectRegisterId == OMInvoice.GetRegisterId() &&
                    x.ObjectId == entity.EmpId &&
                    (x.IsDeleted == null || x.IsDeleted == false))
                    .GetCountQuery()
                    .ExecuteQuery()
                    .Rows[0]["Count"].ParseToInt() > 0;

                model.InvoiceId = entity.EmpId;
                model.NumInvoice = entity.NumInvoice;
                model.DateInvoice = entity.dateInvoice;
                model.HeaderText = string.Format("Статус: {0}{1}{2}{3}{4}{5}",
                            entity.Status,
                            payReestrNum.IsNotEmpty() ? $"{payReestrNum}" : string.Empty,
                            (entity.Status_Code != InvoiceStatus.Denied ? string.Format(".   Сумма к выплате: {0}", entity.SumOpl?.ToString("n2") ?? "-") : ""),
                            nomDoc.IsNotEmpty() ? $".   № дела: {nomDoc}" : string.Empty,
                            entity.SvidPolycNum.IsNotEmpty() ? $".   Договор №: {entity.SvidPolycNum}" : string.Empty,
                            entity.SubjectName.IsNotEmpty() ? $".   Получатель: {entity.SubjectName}" : string.Empty);

                model.FspId = entity.LinkFsp;
                model.DamageId = entity.LinkDamage;
                model.AllPropertyId = entity.LinkAllProperty;

                model.Status = entity.Status;

                model.SubjectId = entity.SubjectId;
                model.SubjectName = entity.SubjectName;
                model.Phone = entity.Phone;
                model.Inn = entity.Inn;
                model.Kpp = entity.Kpp;
                model.RachAcc = entity.RachAcc;
                model.BankCardNumber = entity.NumCard;

                model.Oktmo = entity.Oktmo;
                model.Kbk = entity.Kbk;


                model.BankId = entity.BankId;
                model.BankName = entity.BankName;
                model.BicBank = entity.BicBank;
                model.InnBank = entity.InnBank;
                model.KppBank = entity.KppBank;
                model.KorAcc = entity.KorAcc;

                model.NoPayNoteId = entity.NoteNoPayId;
                model.NoPayNoteReason = entity.ParentGbuNoPayReason?.Reason ?? "";
                model.DataInput = entity.DataInput;
                model.SumOpl = entity.SumOpl;
                model.Comment = entity.Comment;
                model.StatusCode = entity.Status_Code;
                model.DateAgree = entity.DateAgree;

                model.PartDog = entity.PartDog;
                model.SvidPolycNum = entity.SvidPolycNum;
                model.SvdPolyceDate = entity.SvdPolyceDate;
                model.InsurSymmaOtchet = entity.InsurSymmaOtchet;
                model.DateZackluchenia = entity.DataZakluchenia;
                model.InsurSymmaOtchetChanged = estimatedValue.HasValue && entity.InsurSymmaOtchet.HasValue 
                    && estimatedValue.Value != entity.InsurSymmaOtchet;
                model.Sort = entity.Sort;
            }

            return model;
        }

        public static OMInvoice OMMap(InvoiceDto model)
        {
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
            
            OMSubject subject = null;

            // Сохраняем информацию по субъетку
            if (model.SubjectId.HasValue)
            {
                subject = OMSubject.Where(x => x.EmpId == model.SubjectId).Execute().FirstOrDefault();
            }
            else
            {
                subject = new OMSubject();
            }

            if (subject != null)
            {
                subject.SubjectName = model.SubjectName;
                subject.OrgPhone = model.Phone;
                subject.Kpp = model.Kpp;
                subject.Inn = model.Inn;
                subject.RachAcc = model.RachAcc;
                subject.NumCard = model.BankCardNumber;
                subject.BankId = model.BankId;

                subject.Save();
            }

            OMInvoice invoice = null;

            if (model.InvoiceId > 0)
            {
                invoice = OMInvoice.Where(x => x.EmpId == model.InvoiceId).SelectAll().Execute().FirstOrDefault();
            }

            if (invoice == null)
            {
                invoice = new OMInvoice
                {
                    DataInput = DateTime.Now,
                    UserId = SRDSession.GetCurrentUserId(),
                    Status_Code = model.StatusCode
                };
            }
            invoice.NumInvoice = model.NumInvoice;
            invoice.dateInvoice = model.DateInvoice;

            invoice.LinkFsp = model.FspId;
            invoice.LinkDamage = model.DamageId;
            invoice.LinkAllProperty = model.AllPropertyId;

            invoice.SubjectId = model.SubjectId;
            invoice.SubjectName = model.SubjectName;
            invoice.Kpp = model.Kpp;
            invoice.Inn = model.Inn;
            invoice.RachAcc = model.RachAcc;
            invoice.NumCard = model.BankCardNumber;

            invoice.BankId = model.BankId;
            invoice.BankName = model.BankName;
            invoice.InnBank = model.InnBank;
            invoice.KppBank = model.KppBank;
            invoice.BicBank = model.BicBank;
            invoice.KorAcc = model.KorAcc;

            invoice.NoteNoPayId = model.NoPayNoteId;
            invoice.Phone = model.Phone;
            invoice.SumOpl = model.SumOpl;
            invoice.Comment = model.Comment;

            invoice.Oktmo = model.Oktmo;
            invoice.Kbk = model.Kbk;


            invoice.PartDog = model.PartDog;
            invoice.SvidPolycNum = model.SvidPolycNum;
            invoice.SvdPolyceDate = model.SvdPolyceDate;
            invoice.InsurSymmaOtchet = model.InsurSymmaOtchet;
            invoice.DataZakluchenia = model.DateZackluchenia;
            invoice.Sort = model.Sort;

            return invoice;
        }

        public static InvoiceDto CreateNew(long? damageId, long? allPropertyId, long? fspId, decimal? sum, decimal? partDog, InvoiceStatus statusCode = InvoiceStatus.None)
        {
            InvoiceDto model = new InvoiceDto
            {
                GuidId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6),
                InvoiceId = -1,
                DamageId = damageId,
                AllPropertyId = allPropertyId,
                FspId = fspId,
                SumOpl = sum,
                HeaderText = "Новая запись",
                StatusCode = statusCode,
                PartDog = partDog
            };
            
            if (model.DamageId.HasValue)
            {
                model.DateZackluchenia = DateTime.Today;
                OMDamage damage = OMDamage.Where(x => x.EmpId == model.DamageId.Value && x.DamageData != null)
                    .Select(x => x.TypeDoc)
                    .Select(x => x.TypeDoc_Code)
                    .Select(x => x.DamageData)
                    .Select(x => x.EstimatedValue)
                    .Select(x => x.NomDoc)
                    .ExecuteFirstOrDefault();

                if (damage != null && damage.DamageData.HasValue)
                {
                    model.IsFisical = damage.TypeDoc_Code == ContractType.Dwelling;
                    model.InsurSymmaOtchet = damage.EstimatedValue;
                    model.NumInvoice = damage.NomDoc;
                    model.Sort = OMInvoice
                        .Where(x => x.LinkDamage == damageId)
                        .GetCountQuery()
                        .ExecuteQuery()
                        .Rows[0]["Count"]
                        .ParseToDecimal();

                    if (model.FspId.HasValue)
                    {
                        OMFsp fsp = OMFsp.Where(x => x.EmpId == model.FspId)
                            .Select(x => x.FspType)
                            .Select(x => x.FspType_Code)
                            .Select(x => x.Kodpl)
                            .Select(x => x.ContractId)
                            .ExecuteFirstOrDefault();

                        if (fsp != null)
                        {
                            OMPolicySvd policy = new FspService().GetPolicyByDate(fsp.EmpId, damage.DamageData.Value);

                            if (policy != null)
                            {
                                model.SvidPolycNum = policy.Npol;
                                model.SvdPolyceDate = policy.Dat;
                            }
                        }
                    }
                }
            }

            if(model.AllPropertyId.HasValue)
            {
                OMAllProperty prop = OMAllProperty
                    .Where(x => x.EmpId == model.AllPropertyId)
                    .Select(x => x.Name)
                    .Select(x => x.Ndog)
                    .Select(x => x.Ndogdat)
                    .Execute()
                    .FirstOrDefault();
                if(prop != null)
                {
                    model.SvidPolycNum = prop.Ndog;
                    model.SvdPolyceDate = prop.Ndogdat;

                    OMSubject subj = OMSubject.Where(x => x.SubjectName.Contains(prop.Name))
                        .SelectAll()
                        .Select(x => x.ParentBank.BankName)
                        .Select(x => x.ParentBank.BankBic)
                        .Select(x => x.ParentBank.BankInn)
                        .Select(x => x.ParentBank.BankKpp)
                        .Select(x => x.ParentBank.BankKorAcc)
                        .Execute().FirstOrDefault();

                    model.SubjectId = subj?.EmpId;
                    model.SubjectName = subj?.SubjectName;
                    model.Phone = subj?.OrgPhone;
                    model.Inn = subj?.Inn;
                    model.Kpp = subj?.Kpp;
                    model.RachAcc = subj?.RachAcc;
                    model.BankCardNumber = subj?.NumCard;

                    model.BankId = subj?.BankId;
                    model.BankName = subj?.ParentBank?.BankName;
                    model.BicBank = subj?.ParentBank?.BankBic;
                    model.InnBank = subj?.ParentBank?.BankInn;
                    model.KppBank = subj?.ParentBank?.BankKpp;
                    model.KorAcc = subj?.ParentBank?.BankKorAcc;
                }
            }

            return model;
        }
    }
}
