using CIPJS.DAL.Bank;
using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CIPJS.DAL.Subject
{
    public class SubjectDto
    {
        /// <summary>
        /// Уникальный номер записи
        /// </summary>
        public long EmpId { get; set; }

        /// <summary>
        /// Ссылка на реестр Округ
        /// </summary>
        public long? OkrugId { get; set; }

        /// <summary>
        /// Код организации
        /// </summary>
        public long? KodOrg { get; set; }

        /// <summary>
        /// Код управляющей компании
        /// </summary>
        public long? KodUpk { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        [Display(Name = "Наименование субъекта")]
        public string SubjectName { get; set; }

        /// <summary>
        /// Идентификатор организации
        /// </summary>
        public string OrgIdT { get; set; }

        /// <summary>
        /// Должность руководителя
        /// </summary>
        [Display(Name = "Должность руководителя")]
        public string EmplRole { get; set; }

        /// <summary>
        /// ФИО руководителя
        /// </summary>
        [Display(Name = "ФИО руководителя")]
        public string Fio { get; set; }

        /// <summary>
        /// Юридический адрес организации
        /// </summary>
        [Display(Name = "Юридический адрес")]
        public string OrgAdrU { get; set; }

        /// <summary>
        /// Физический адрес организации
        /// </summary>
        [Display(Name = "Физический адрес")]
        public string OrgAdrF { get; set; }

        /// <summary>
        /// Телефон организации
        /// </summary>
        [Display(Name = "Контактный телефон")]
        public string OrgPhone { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? DateInput { get; set; }

        /// <summary>
        /// Дата рождения (для физ лиц)
        /// </summary>
        [Display(Name = "Дата рождения")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        [Display(Name = "ИНН")]
        public string Inn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        [Display(Name = "КПП")]
        public string Kpp { get; set; }

        /// <summary>
        /// Расчетный счет
        /// </summary>
        [Display(Name = "Расчетный счет получателя")]
        public string RachAcc { get; set; }

        /// <summary>
        /// Номер банковской карточки
        /// </summary>
        [Display(Name = "Номер банковской карточки")]
        public string NumCard { get; set; }

        /// <summary>
        /// Номер документа, удостоверяющего личность
        /// </summary>
        [Display(Name = "Номер документа, удостоверяющего личность")]
        public string NomDoc { get; set; }

        /// <summary>
        /// Дата паспорта, удостоверяющего личность
        /// </summary>
        [Display(Name = "Дата паспорта, удостоверяющего личность")]
        public DateTime? DateDoc { get; set; }

        /// <summary>
        /// Дата выдачи документа, удостоверяющего личность
        /// </summary>
        [Display(Name = "Дата выдачи документа, удостоверяющего личность")]
        public DateTime? DateInDoc { get; set; }

        /// <summary>
        /// Организация выдавшая документ, удостоверяющего личность
        /// </summary>
        [Display(Name = "Организация выдавшая документ, удостоверяющего личность")]
        public string OrgDoc { get; set; }

        /// <summary>
        /// Тип субъекта (выбор из справочника «Тип субъекта» ­ 12142)
        /// </summary>
        [Display(Name = "Тип субъекта")]
        public SubjectType TypeSubjectCode { get; set; }

        /// <summary>
        /// Ссылка на EMP_ID в INSUR_BANK
        /// </summary>
        public long? BankId { get; set; }

        /// <summary>
        /// Наименование банка
        /// </summary>
        [Display(Name = "Наименование банка")]
        public string BankName { get; set; }

        /// <summary>
        /// ИНН банка
        /// </summary>
        [Display(Name = "ИНН банка")]
        public string BankInn { get; set; }

        /// <summary>
        /// КПП банка
        /// </summary>
        [Display(Name = "КПП банка")]
        public string BankKpp { get; set; }

        /// <summary>
        /// БИК банка
        /// </summary>
        [Display(Name = "БИК банка")]
        public string BankBic { get; set; }

        /// <summary>
        /// Корреспондентский счет
        /// </summary>
        [Display(Name = "Корреспондентский счет банка")]
        public string BankKorAcc { get; set; }

        [Display(Name = "Название субъекта для счета")]
        public string NameForInvoice { get; set; }

        /// <summary>
        /// Получение объекта Dto по записи
        /// </summary>
        /// <param name="EmpId">Идентфиикатор записи</param>
        /// <returns></returns>
        public static SubjectDto OMMap(long? EmpId)
        {
            SubjectDto subject = new SubjectDto
            {
                EmpId = -1,
                TypeSubjectCode = SubjectType.None
            };

            if (EmpId.HasValue && EmpId > -1)
            {
                OMSubject entity = OMSubject
                    .Where(x => x.EmpId == EmpId)
                    .SelectAll()
                    .Select(x => x.ParentBank.BankName)
                    .Select(x => x.ParentBank.BankInn)
                    .Select(x => x.ParentBank.BankKpp)
                    .Select(x => x.ParentBank.BankBic)
                    .Select(x => x.ParentBank.BankKorAcc)
                    .Execute()
                    .FirstOrDefault();

                if(entity.BankId.HasValue && entity.ParentBank == null)
                {
                    entity.ParentBank = OMBank.Where(x => x.EmpId == entity.BankId).SelectAll().Execute().FirstOrDefault();
                }

                if (entity != null)
                {
                    subject.EmpId = entity.EmpId;
                    subject.OkrugId = entity.OkrugId;
                    subject.KodOrg = entity.KodOrg;
                    subject.KodUpk = entity.KodUpk;
                    subject.SubjectName = entity.SubjectName;
                    subject.OrgIdT = entity.OrgIdT;
                    subject.EmplRole = entity.EmplRole;
                    subject.Fio = entity.FioAdm;
                    subject.OrgAdrU = entity.OrgAdrU;
                    subject.OrgAdrF = entity.OrgAdrF;
                    subject.OrgPhone = entity.OrgPhone;
                    subject.DateInput = entity.DateInput;
                    subject.Birthday = entity.Birthday;
                    subject.Inn = entity.Inn;
                    subject.Kpp = entity.Kpp;
                    subject.RachAcc = entity.RachAcc;
                    subject.NumCard = entity.NumCard;
                    subject.NomDoc = entity.NomDoc;
                    subject.DateDoc = entity.DateDoc;
                    subject.DateInDoc = entity.DateInDoc;
                    subject.OrgDoc = entity.OrgDoc;
                    subject.TypeSubjectCode = entity.TypeSubject_Code;
                    subject.BankId = entity.BankId;
                    subject.BankName = entity.ParentBank?.BankName;
                    subject.BankInn = entity.ParentBank?.BankInn;
                    subject.BankKpp = entity.ParentBank?.BankKpp;
                    subject.BankBic = entity.ParentBank?.BankBic;
                    subject.BankKorAcc = entity.ParentBank?.BankKorAcc;
                    subject.NameForInvoice = entity.NameForInvoice;
                }
            }

            return subject;
        }


        /// <summary>
        /// Получение объекта Dto по записи
        /// </summary>
        /// <param name="EmpId">Идентфиикатор записи</param>
        /// <returns></returns>
        public static SubjectDto OMMap(OMSubject entity)
        {
            SubjectDto subject = new SubjectDto
            {
                EmpId = -1,
                TypeSubjectCode = SubjectType.None
            };

            if (entity != null)
            {
                subject.EmpId = entity.EmpId;
                subject.OkrugId = entity.OkrugId;
                subject.KodOrg = entity.KodOrg;
                subject.KodUpk = entity.KodUpk;
                subject.SubjectName = entity.SubjectName;
                subject.OrgIdT = entity.OrgIdT;
                subject.EmplRole = entity.EmplRole;
                subject.Fio = entity.FioAdm;
                subject.OrgAdrU = entity.OrgAdrU;
                subject.OrgAdrF = entity.OrgAdrF;
                subject.OrgPhone = entity.OrgPhone;
                subject.DateInput = entity.DateInput;
                subject.Birthday = entity.Birthday;
                subject.Inn = entity.Inn;
                subject.Kpp = entity.Kpp;
                subject.RachAcc = entity.RachAcc;
                subject.NumCard = entity.NumCard;
                subject.NomDoc = entity.NomDoc;
                subject.DateDoc = entity.DateDoc;
                subject.DateInDoc = entity.DateInDoc;
                subject.OrgDoc = entity.OrgDoc;
                subject.TypeSubjectCode = entity.TypeSubject_Code;
                subject.BankId = entity.BankId;
                subject.BankName = entity.ParentBank?.BankName;
                subject.BankInn = entity.ParentBank?.BankInn;
                subject.BankKpp = entity.ParentBank?.BankKpp;
                subject.BankBic = entity.ParentBank?.BankBic;
                subject.BankKorAcc = entity.ParentBank?.BankKorAcc;
                subject.NameForInvoice = entity.NameForInvoice;
            }

            return subject;
        }

        public static OMSubject OMMap(SubjectDto model)
        {
            OMSubject subject = null;

            if (model.EmpId > 0)
            {
                subject = OMSubject.Where(x => x.EmpId == model.EmpId).SelectAll().Execute().FirstOrDefault();
            }

            if (subject == null)
            {
                subject = new OMSubject();
            }

            if (!model.BankId.HasValue && model.BankBic.IsNotEmpty())
            {
                model.BankId = OMBank.Where(x => x.BankBic == model.BankBic).ExecuteFirstOrDefault()?.EmpId
                    ?? new OMBank
                    {
                        EmpId = -1,
                        BankBic = model.BankBic,
                        BankName = model.BankName,
                        BankInn = model.BankInn,
                        BankKpp = model.BankKpp,
                        BankKorAcc = model.BankKorAcc
                    }.Save();
            }

            //Сохраняем только то, что выводится на форму
            subject.TypeSubject_Code = model.TypeSubjectCode;
            subject.SubjectName = model.SubjectName;
            subject.Birthday = model.Birthday;
            subject.Kpp = model.Kpp;
            subject.Inn = model.Inn;
            subject.RachAcc = model.RachAcc;
            subject.NumCard = model.NumCard;

            subject.NomDoc = model.NomDoc;
            subject.DateDoc = model.DateDoc;
            subject.DateInDoc = model.DateInDoc;
            subject.OrgDoc = model.OrgDoc;

            subject.OrgAdrU = model.OrgAdrU;
            subject.OrgAdrF = model.OrgAdrF;
            subject.OrgPhone = model.OrgPhone;
            subject.EmplRole = model.EmplRole;
            subject.FioAdm = model.Fio;
            subject.NameForInvoice = model.NameForInvoice;
            subject.BankId = model.BankId;

            return subject;
        }
    }
}