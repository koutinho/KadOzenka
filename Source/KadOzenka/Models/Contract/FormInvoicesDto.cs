using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.Contract
{
    public class FormInvoicesDto
    {
        [Display(Name = "Номер счета")]
        public string NumInvoice { get; set; }

        [Display(Name = "Дата счета")]
        public DateTime? DateInvoice { get; set; }

        public long? SubjectId { get; set; }

        [Display(Name = "Получатель")]
        public string SubjectName { get; set; }

        [Display(Name = "Контактный телефон")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "ИНН получателя")]
        public string Inn { get; set; }

        [Required]
        [Display(Name = "КПП получателя")]
        public string Kpp { get; set; }

        [Required]
        [Display(Name = "Р/С получателя")]
        public string RachAcc { get; set; }

        [Display(Name = "№ банковской карты")]
        public string BankCardNumber { get; set; }

        public long? BankId { get; set; }

        [Required]
        [Display(Name = "Название банка")]
        public string BankName { get; set; }

        [Display(Name = "ИНН банка")]
        public string InnBank { get; set; }

        [Display(Name = "КПП банка")]
        public string KppBank { get; set; }

        [Required]
        [Display(Name = "БИК")]
        public string BicBank { get; set; }

        [Display(Name = "К/С")]
        public string KorAcc { get; set; }

        [Display(Name = "Получатель (для счета)")]
        public string NameForInvoice { get; set; }

        public List<long> Ids { get; set; }

        public List<FormInvoicesAllPropertyDto> ErrorAllPropertyList { get; set; }

        public List<FormInvoicesResultDto> FutureSuccessRows { get; set; }

        public List<FormInvoicesResultDto> FutureErrorRows { get; set; }
    }
}