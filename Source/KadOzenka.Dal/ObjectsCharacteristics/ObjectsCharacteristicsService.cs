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

        public OMObjectsCharacteristicsRegister GetOMObjectsCharacteristics(long characteristicsId)
        {
            return OMObjectsCharacteristicsRegister.Where(x => x.Id == characteristicsId).SelectAll()
                .ExecuteFirstOrDefault();
        }

        public long AddRegister(ObjectsCharacteristicDto characteristic)
        {
            OMRegister omRegister;
            using (var ts = new TransactionScope())
            {
                var registerName = $"source_{GetNumberOfExistingRegistersWithCharacteristics()}_q";
                omRegister = RegisterService.CreateRegister(registerName, characteristic.Name, registerName);

                RegisterService.CreateIdColumnForRegister(omRegister.RegisterId);

                RegisterConfigurator.CreateDbTableForRegister(omRegister.RegisterId);

                CreateObjectCharacteristics(omRegister.RegisterId);

                ts.Complete();
            }

            return omRegister.RegisterId;
        }

        public long EditRegister(ObjectsCharacteristicDto characteristic)
        {
            var register = RegisterService.GetRegister(characteristic.Id);

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
