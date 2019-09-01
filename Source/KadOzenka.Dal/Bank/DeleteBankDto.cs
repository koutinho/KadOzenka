namespace CIPJS.DAL.Bank
{
    public class DeleteBankDto
    {
        public BankDto Bank { get; set; }

        public bool HasInvoiceLinks { get; set; }
    }
}
