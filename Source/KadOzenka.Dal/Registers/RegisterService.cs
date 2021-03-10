using System;
using System.Linq;
using System.Transactions;
using Core.Shared.Extensions;
using KadOzenka.Dal.RecycleBin;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.Registers
{
    public class RegisterService : IRegisterService
    {
	    public IRecycleBinService RecycleBinService { get; }

	    public RegisterService(IRecycleBinService recycleBinService)
	    {
		    RecycleBinService = recycleBinService;
	    }

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
                StorageType = storageType,
                ObjectSequence = "REG_OBJECT_SEQ",
                ContainsQuantInFuture = false
            };

            if (!string.IsNullOrWhiteSpace(allPriTable))
            {
                omRegister.AllpriTable = allPriTable;
                omRegister.AllpriPartitioning = 1;
                omRegister.MainRegister = OMMainObject.GetRegisterId();
            }

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

        public void RemoveRegister(long registerId, long eventId)
        {
            using (var ts = new TransactionScope())
            {
                var omRegister = OMRegister.Where(x => x.RegisterId == registerId).SelectAll().ExecuteFirstOrDefault();
                if (omRegister == null)
                {
                    throw new Exception($"Не найден реестр с ИД {registerId}");
                }
                RecycleBinService.MoveObjectToRecycleBin(omRegister.RegisterId, OMRegister.GetRegisterId(), eventId);

                var omRegisterAttributes = OMAttribute.Where(x => x.RegisterId == registerId).SelectAll().Execute();
                RecycleBinService.MoveObjectsToRecycleBin(omRegisterAttributes.Select(x => x.Id).ToList(),
	                OMAttribute.GetRegisterId(), eventId);

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
