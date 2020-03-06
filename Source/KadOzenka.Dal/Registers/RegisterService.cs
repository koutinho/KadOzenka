using System;
using Core.Shared.Extensions;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Registers
{
    public class RegisterService
    {
        public OMRegister CreateRegister(string registerName, string registerDescription, string quantTable)
        {
            var omRegister = new OMRegister
            {
                RegisterId = -1,
                RegisterName = registerName,
                RegisterDescription = registerDescription,
                QuantTable = quantTable,
                StorageType = 4,
                ObjectSequence = "REG_OBJECT_SEQ"
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


        #region Support Methods

        private long GetFirstAttributeId(long registerId)
        {
            return registerId * Math.Pow(10, (8 - Math.Floor(Math.Log10(registerId) + 1))).ParseToLong() + 100;
        }

        #endregion
    }
}
