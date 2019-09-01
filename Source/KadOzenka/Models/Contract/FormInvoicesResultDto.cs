using System;

namespace CIPJS.Models.Contract
{
    public class FormInvoicesResultDto
    {
        public int Ordinal { get; set; }

        public string ContractNumber { get; set; }

        public DateTime? ContractDate { get; set; }

        public decimal? RasPripay { get; set; }

        public string SubjectName { get; set; }

        public decimal? Sum { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public string BicBank { get; set; }

        public string BankName { get; set; }

        public string Error { get; set; }
    }
}