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

        public ObjectsCharacteristicDto GetCharacteristics(long characteristicsId)
        {
            var characteristic = GetCharacteristicsInternal(characteristicsId);
            if (characteristic == null)
                return null;

            var register = RegisterService.GetRegister(characteristic.RegisterId);

            return new ObjectsCharacteristicDto
            {
                Id = characteristic.Id,
                RegisterId = characteristic.RegisterId,
                RegisterDescription = register.RegisterDescription
            };
        }

        public long AddRegister(ObjectsCharacteristicDto characteristic)
        {
            OMRegister omRegister;
            using (var ts = new TransactionScope())
            {
                var registerName = $"source_{GetNumberOfExistingRegistersWithCharacteristics()}_q";
                omRegister = RegisterService.CreateRegister(registerName, characteristic.RegisterDescription, registerName);

                RegisterService.CreateIdColumnForRegister(omRegister.RegisterId);

                RegisterConfigurator.CreateDbTableForRegister(omRegister.RegisterId);

                CreateObjectCharacteristics(omRegister.RegisterId);

                ts.Complete();
            }

            return omRegister.RegisterId;
        }

        public void EditRegister(ObjectsCharacteristicDto characteristicDto)
        {
            var characteristic = GetCharacteristicsInternal(characteristicDto.Id);
            if (characteristic == null)
                return;

            var register = RegisterService.GetRegister(characteristic.RegisterId);
            using (var ts = new TransactionScope())
            {
                register.RegisterDescription = characteristicDto.RegisterDescription;
                register.Save();

                ts.Complete();
            }
        }


        #region Support Methods

        private void CreateObjectCharacteristics(long registerId)
        {
            new OMObjectsCharacteristicsRegister
            {
                RegisterId = registerId
            }.Save();
        }

        private int GetNumberOfExistingRegistersWithCharacteristics()
        {
            return OMObjectsCharacteristicsRegister.Where(x => true).SelectAll().ExecuteCount();
        }

        private OMObjectsCharacteristicsRegister GetCharacteristicsInternal(long characteristicsId)
        {
            return OMObjectsCharacteristicsRegister.Where(x => x.Id == characteristicsId).SelectAll()
                .ExecuteFirstOrDefault();
        }

        #endregion
    }
}
