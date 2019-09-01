using Core.Register.QuerySubsystem;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;

namespace CIPJS.DAL.ReestrPayInvoice
{
    public class ReestrPayInvoiceService
    {
        public List<OMInvoiceSvod> GetGroupInvoiceList(long payId)
        {
            return OMInvoiceSvod
                .Where(x => x.LinkReestrPay == payId)
                .SelectAll()
                .Execute();
        }

        public List<OMInvoice> GetGroupDetailList(long payId, string subjectName, string numInvoice, DateTime? dateInvoice)
        {
            return OMInvoice.Where(x => x.LinkReestrPay == payId
                && x.SubjectName == subjectName
                && x.NumInvoice == numInvoice
                && x.dateInvoice == dateInvoice)
                .SelectAll()
                .Select(x => x.ParentAllProperty.EmpId)
                .Select(x => x.ParentAllProperty.Ndog)
                .Execute();
        }
    }
}
