using ObjectModel.Insur;
using System;
using System.Linq;

namespace CIPJS.DAL.BankPlat
{
    public class BankPlatService
    {
        public OMBankPlat Get(long id)
        {
            OMBankPlat bankPlat = OMBankPlat.Where(x => x.EmpId == id)
                .SelectAll()
                .Execute()
                .FirstOrDefault();

            if (bankPlat == null)
            {
                throw new Exception($"Не удалось определить банковскую строку с идентификатором {id}");
            }

            return bankPlat;
        }
    }
}
