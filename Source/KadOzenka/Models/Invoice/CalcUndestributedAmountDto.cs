namespace CIPJS.Models.Invoice
{
    public class CalcUndestributedAmountDto
    {
        public decimal? PartTownSum { get; set; }

        public decimal?[] InvoiceSums { get; set; }
    }
}
