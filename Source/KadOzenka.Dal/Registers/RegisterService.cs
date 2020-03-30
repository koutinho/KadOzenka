using System;
using System.Transactions;
using Core.Shared.Extensions;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Registers
{
    public class RegisterService
    {
        public OMRegister GetRegister(long? registerId)
        {
            return OMRegister.Where(x => x.RegisterId == registerId).SelectAll().ExecuteFirstOrDefault();
        }

        public OMRegister CreateRegister(string registerName, string registerDescription, string quantTable,
            string allPriTable = null, long? storageType = 4)
        {
            var omRegister = new OMRegister
            {
                RegisterId = -1,
                RegisterName = registerName,
                RegisterDescription = registerDescription,
                QuantTable = quantTable,
                AllpriTable = allPriTable,
                StorageType = storageType,
                ObjectSequence = "REG_OBJECT_SEQ",
                ContainsQuantInFuture = false
            };

            omRegister.Save();

            return omRegister;
        }

        public int CreateIdColumnForRegister(long registerId)
        {
            return new OMAttribute
            {
                Id = GetFirstAttributeId(registerId),
                RegisterId = registerId,
                Type = 1,
                Name = "Идентификатор",
                ValueField = "ID",
                IsPrimaryKey = true,
                IsNullable = false
            }.Save();
        }

        public void RemoveRegister(long registerId)
        {
            using (var ts = new TransactionScope())
            {
                var omRegister = OMRegister.Where(x => x.RegisterId == registerId).SelectAll().ExecuteFirstOrDefault();
                if (omRegister == null)
                {
                    throw new Exception($"Не найден реестр с ИД {registerId}");
                }
                omRegister.IsDeleted = true;
                omRegister.Save();

                var omRegisterAttributes = OMAttribute.Where(x => x.RegisterId == registerId).SelectAll().Execute();
                foreach (var omRegisterAttribute in omRegisterAttributes)
                {
                    omRegisterAttribute.IsDeleted = true;
                    omRegisterAttribute.Save();
                }

                ts.Complete();
            }
        }

        #region Support Methods

        private long GetFirstAttributeId(long registerId)
        {
            return registerId * Math.Pow(10, (8 - Math.Floor(Math.Log10(registerId) + 1))).ParseToLong() + 100;
        }

        #endregion
    }
}
