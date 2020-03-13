using System;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using System.Transactions;
using KadOzenka.Dal.Registers;
using ObjectModel.Core.Register;
using ObjectModel.KO;
using Platform.Configurator;
using Core.Register;

namespace KadOzenka.Dal.ObjectsCharacteristics
{
    public class ObjectsCharacteristicsService
    {
        public RegisterService RegisterService { get; set; }
        public RegisterAttributeService RegisterAttributeService { get; set; }

        public ObjectsCharacteristicsService()
        {
            RegisterService = new RegisterService();
            RegisterAttributeService = new RegisterAttributeService();
        }

        #region Source

        public SourceDto GetSource(long characteristicsId)
        {
            var characteristic = GetCharacteristicsInternal(characteristicsId);
            if (characteristic == null)
                return null;

            var register = RegisterService.GetRegister(characteristic.RegisterId);

            return new SourceDto
            {
                Id = characteristic.Id,
                RegisterId = characteristic.RegisterId,
                RegisterDescription = register.RegisterDescription
            };
        }

        public long AddSource(SourceDto sourceDto)
        {
            if (string.IsNullOrWhiteSpace(sourceDto.RegisterDescription))
                throw new ArgumentException("Имя источника не может быть пустым");

            OMRegister omRegister;
            using (var ts = new TransactionScope())
            {
                var registerName = $"source_{GetNumberOfExistingRegistersWithCharacteristics()}_q";
                omRegister = RegisterService.CreateRegister(registerName, sourceDto.RegisterDescription, registerName);

                RegisterService.CreateIdColumnForRegister(omRegister.RegisterId);

                RegisterConfigurator.CreateDbTableForRegister(omRegister.RegisterId);

                CreateObjectCharacteristics(omRegister.RegisterId);

                ts.Complete();
            }

            return omRegister.RegisterId;
        }

        public void EditSource(SourceDto sourceDto)
        {
            if (string.IsNullOrWhiteSpace(sourceDto.RegisterDescription))
                throw new ArgumentException("Имя источника не может быть пустым");

            var characteristic = GetCharacteristicsInternal(sourceDto.Id);
            if (characteristic == null)
                return;

            var register = RegisterService.GetRegister(characteristic.RegisterId);
            using (var ts = new TransactionScope())
            {
                register.RegisterDescription = sourceDto.RegisterDescription;
                register.Save();

                ts.Complete();
            }
        }

        #endregion

        #region Characteristics

        public long AddCharacteristic(string attributeName, long registerId, RegisterAttributeType type, long? referenceId = null)
        {
            if (string.IsNullOrWhiteSpace(attributeName))
                throw new ArgumentException("Имя характеристики не может быть пустым");

            long id;
            using (var ts = new TransactionScope())
            {
                var omAttribute = RegisterAttributeService.CreateRegisterAttribute(attributeName, registerId, type, referenceId);
                id = omAttribute.Id;

                var dbConfigurator = RegisterConfigurator.GetDbConfigurator();
                RegisterConfigurator.CreateDbColumnForRegister(omAttribute, dbConfigurator);

                ts.Complete();
            }

            return id;
        }

        #endregion


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
