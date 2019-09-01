using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.DAL.Bank
{
    public class BankDto
    {
        /// <summary>
        /// Уникальный номер записи
        /// </summary>
        public long EmpId { get; set; }

        /// <summary>
        /// Наименование банка
        /// </summary>
        [Display(Name = "Наименование банка")]
        public string BankName { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [Display(Name = "Дата создания")]
        public DateTime? DateInput { get; set; }

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
        [Display(Name = "Корреспондентский счет")]
        public string BankKorAcc { get; set; }

        public bool HasInvoiceLinks => OMInvoice
                                       .Where(x => x.BankId == EmpId)
                                       .SetPackageSize(1)
                                       .ExecuteFirstOrDefault() != null;

        public static BankDto Map(OMBank entity) => new BankDto
        {
            EmpId = entity?.EmpId ?? -1,
            BankName = entity?.BankName,
            DateInput = entity?.DateInput,
            BankInn = entity?.BankInn,
            BankKpp = entity?.BankKpp,
            BankBic = entity?.BankBic,
            BankKorAcc = entity?.BankKorAcc,
        };
    }
}