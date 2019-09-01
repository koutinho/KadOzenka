using Core.Shared.Extensions;
using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CIPJS.Models.ReestrPayInvoice
{
    public class GroupDto
    {
        [Display(Name = "Идентификатор реестра платежей")]
        public long? PayId { get; set; }

        [Display(Name = "ИНН")]
        public string SubjectName { get; set; }

        public long? SubjectId { get; set; }

        [Display(Name = "Номер счета")]
        public string NumInvoice { get; set; }

        [Display(Name = "Дата счета")]
        public DateTime? DateInvoice { get; set; }

        [Display(Name = "Сумма счета")]
        public decimal? SumOpl { get; set; }

        public static GroupDto OMMap(DataRow row)
        {
            return new GroupDto
            {
                PayId = row["PayId"] != DBNull.Value ? (long?)row["PayId"].ParseToLong() : null,
                SubjectId = row["SubjectId"] != DBNull.Value ? row["SubjectId"].ParseToLong() : (long?)null,
                SubjectName = row["SubjectName"] != DBNull.Value ? row["SubjectName"].ToString() : null,
                NumInvoice = row["NumInvoice"] != DBNull.Value ? row["NumInvoice"].ToString() : null,
                DateInvoice = row["DateInvoice"] != DBNull.Value ? (DateTime?)row["DateInvoice"].ParseToDateTime() : null,
                SumOpl = row["SumOpl"] != DBNull.Value ? (decimal?)row["SumOpl"].ParseToDecimal() : null,
            };
        }

        public static GroupDto OMMap(OMInvoiceSvod row)
        {
            return new GroupDto
            {
                PayId = row.LinkReestrPay,
                SubjectId = row.SubjectId,
                SubjectName = row.SubjectName,
                NumInvoice = row.NumInvoice,
                DateInvoice = row.DateInvoice,
                SumOpl = row.SumSvod
            };
        }
    }
}