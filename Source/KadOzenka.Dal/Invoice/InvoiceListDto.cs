using System.Collections.Generic;

namespace CIPJS.DAL.Invoice
{
    public class InvoiceListDto
    {
        public long? InvoiceDamageId { get; set; }

        public long? InvoiceAllPropertyId { get; set; }

        public bool CanAdd { get; set; }

        public bool IsFisical { get; set; }

        public decimal? SumDamage { get; set; }

        public decimal? PartTown { get; set; }

        public List<InvoiceDto> Details { get; set; }
    }
}