using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using System.Transactions;
using KadOzenka.Dal.Registers;
using ObjectModel.Core.Register;
using ObjectModel.KO;
using Platform.Configurator;

namespace KadOzenka.Dal.ObjectsCharacteristics
{
    public class ObjectsCharacteristicsService
    {
        public RegisterService RegisterService { get; set; }

        public ObjectsCharacteristicsService()
        {
            RegisterService = new RegisterService();
        }

        public long AddRegister(ObjectsCharacteristicDto characteristic)
        {
            long registerId;
            using (var ts = new TransactionScope())
            {
                var omRegister = new OMRegister
                {
                    RegisterId = -1,
                    RegisterName = $"source_{GetNumberOfExistingRegistersWithCharacteristics()}_q",
                    RegisterDescription = characteristic.Name
                };
                omRegister.QuantTable = omRegister.RegisterName.Replace(".", "_");
                omRegister.StorageType = 4;
                omRegister.ObjectSequence = "REG_OBJECT_SEQ";
                registerId = omRegister.Save();

                var attribute = new OMAttribute
                {
                    Id = RegisterService.GetFirstAttributeId(registerId),
                    RegisterId = registerId,
                    Type = 1,
                    Name = "Идентификатор",
                    ValueField = "ID",
                    IsPrimaryKey = true,
                    IsNullable = false
                };
                attribute.Save();

                RegisterConfigurator.CreateDbTableForRegister(registerId);

                CreateObjectCharacteristics(registerId);

                ts.Complete();
            }

            return registerId;
        }

        public long EditRegister(ObjectsCharacteristicDto characteristic)
        {
            return 0;
        }


        #region Support Methods

        public void CreateObjectCharacteristics(long registerId)
        {
            new OMObjectsCharacteristicsRegister
            {
                RegisterId = registerId
            }.Save();
        }

        public int GetNumberOfExistingRegistersWithCharacteristics()
        {
            return OMObjectsCharacteristicsRegister.Where(x => true).SelectAll().ExecuteCount();
        }

        #endregion
    }
}
