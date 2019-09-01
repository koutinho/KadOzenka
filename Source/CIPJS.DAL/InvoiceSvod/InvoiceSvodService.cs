using ObjectModel.Insur;
using System.Collections.Generic;

namespace CIPJS.DAL.InvoiceSvod
{
    public class InvoiceSvodService
    {
        public List<OMInvoice> GetInvoices(long invoiceSvodId)
        {
            return OMInvoice.Where(x => x.LinkInvoiceSvod == invoiceSvodId)
                .SelectAll()
                .Execute();
        }
    }
}
