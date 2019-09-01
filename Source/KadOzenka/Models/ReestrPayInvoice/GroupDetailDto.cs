using CIPJS.DAL.Invoice;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.ReestrPayInvoice
{
    public class GroupDetailDto
    {
        [Display(Name = "Идентификатор счета")]
        public long InvoiceId { get; set; }

        [Display(Name = "Идентификатор реестра платежей")]
        public long? PayId { get; set; }

        [Display(Name = "Номер договора")]
        public string ContractNumber { get; set; }

        [Display(Name = "ID договора")]
        public long? ContractId { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Сумма к выплате")]
        public decimal? SumOpl { get; set; }

        public static GroupDetailDto OMMap(OMInvoice invoice)
        {
            InvoiceService invoiceService = new InvoiceService();

            GroupDetailDto model = new GroupDetailDto
            {
                InvoiceId = invoice.EmpId,
                PayId = invoice.LinkReestrPay,
                SumOpl = invoice.SumOpl,
                ContractNumber = invoice.ParentAllProperty?.Ndog,
                ContractId = invoice.ParentAllProperty?.EmpId
            };

            if (invoice.LinkAllProperty.HasValue)
            {
                model.Address = invoiceService.GetAddress(invoice.LinkAllProperty.Value);
            }

            return model;
        }
    }
}
