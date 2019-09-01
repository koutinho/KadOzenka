using Core.Shared.Extensions;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.DAL.Bank
{
    public class BankService
    {
        public BankDto Get(long? id)
        {
            OMBank omBank = null;

            if (id.HasValue)
            {
                omBank = OMBank.Where(x => x.EmpId == id).SelectAll().ExecuteFirstOrDefault();
            }

            return BankDto.Map(omBank);
        }

        public void Delete(long id) => OMBank
            .Where(x => x.EmpId == id)
            .ExecuteFirstOrDefault()
            .Destroy();

        public bool HasInvoiceLinks(long id) => OMInvoice
               .Where(x => x.BankId == id)
               .SetPackageSize(1)
               .ExecuteFirstOrDefault() != null;

        public void Save(BankDto model)
        {
            OMBank omBank = model.EmpId != -1
                ? OMBank.Where(x => x.EmpId == model.EmpId).SelectAll().ExecuteFirstOrDefault()
                : new OMBank { EmpId = -1 };

            if (model.BankBic.IsNotEmpty()
                && OMBank.Where(x => x.EmpId != model.EmpId && x.BankBic == model.BankBic).ExecuteFirstOrDefault() != null)
            {
                throw new Exception("Банк с таким БИК уже создан, сохранить второй банк с таким наименованием нельзя");
            }

            omBank.BankName = model.BankName;
            omBank.BankInn = model.BankInn;
            omBank.BankKpp = model.BankKpp;
            omBank.BankBic = model.BankBic;
            omBank.BankKorAcc = model.BankKorAcc;
            omBank.Save();
        }

        public List<BankDto> GetByBic(string bic) => OMBank
                .Where(x => x.BankBic.StartsWith(bic))
                .SelectAll()
                .Execute()
                .Select(x => BankDto.Map(x))
                .ToList();
    }
}
