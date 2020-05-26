using System;
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
        public RegisterAttributeService RegisterAttributeService { get; set; }

        public ObjectsCharacteristicsService()
        {
            RegisterService = new RegisterService();
            RegisterAttributeService = new RegisterAttributeService();
        }

        #region Source

        public SourceDto GetSource(long registerId)
        {
            var register = RegisterService.GetRegister(registerId);
            if (register == null)
                throw new Exception($"Источник с Id {registerId} не найден");

            return new SourceDto
            {
                RegisterId = registerId,
                RegisterDescription = register.RegisterDescription
            };
        }

        public long AddSource(SourceDto sourceDto)
        {
            ValidateSource(sourceDto);

            OMRegister omRegister;
            using (var ts = new TransactionScope())
            {
                var numberOfExistingRegistersWithCharacteristics = GetNumberOfExistingRegistersWithCharacteristics();
                var registerName = $"Gbu.Custom.Source{numberOfExistingRegistersWithCharacteristics}";
                var allpriTable = $"gbu_custom_source_{numberOfExistingRegistersWithCharacteristics}";
                var registerDescription = $"Источник: {sourceDto.RegisterDescription}";
                omRegister = RegisterService.CreateRegister(registerName, registerDescription, "GBU_MAIN_OBJECT", allpriTable, 5);

                RegisterService.CreateIdColumnForRegister(omRegister.RegisterId);

                RegisterConfigurator.CreateDbTableForRegister(omRegister.RegisterId);

                CreateObjectCharacteristics(omRegister.RegisterId);

                ts.Complete();
            }

            return omRegister.RegisterId;
        }

        public void EditSource(SourceDto sourceDto)
        {
            ValidateSource(sourceDto);

            var register = RegisterService.GetRegister(sourceDto.RegisterId);

            using (var ts = new TransactionScope())
            {
                register.RegisterDescription = sourceDto.RegisterDescription;
                register.Save();

                ts.Complete();
            }
        }

        #region Support Methods

        private void ValidateSource(SourceDto sourceDto)
        {
            if (string.IsNullOrWhiteSpace(sourceDto.RegisterDescription))
                throw new ArgumentException("Имя источника не может быть пустым");
        }

        private int GetNumberOfExistingRegistersWithCharacteristics()
        {
            return OMObjectsCharacteristicsRegister.Where(x => true).SelectAll().ExecuteCount();
        }

        #endregion

        #endregion


        #region Characteristic

        public long AddCharacteristic(CharacteristicDto characteristicDto, bool withValueField = false)
        {
            ValidateCharacteristic(characteristicDto);

            long id;
            using (var ts = new TransactionScope())
            {
                var omAttribute = RegisterAttributeService.CreateRegisterAttribute(characteristicDto.Name,
                    characteristicDto.RegisterId, characteristicDto.Type, withValueField, characteristicDto.ReferenceId);
                id = omAttribute.Id;

                var dbConfigurator = RegisterConfigurator.GetDbConfigurator();
                RegisterConfigurator.CreateDbColumnForRegister(omAttribute, dbConfigurator);

                ts.Complete();
            }

            return id;
        }

        public void EditCharacteristic(CharacteristicDto characteristicDto)
        {
            ValidateCharacteristic(characteristicDto);

            RegisterAttributeService.RenameRegisterAttribute(characteristicDto.Id, characteristicDto.Name);
        }

        public void DeleteCharacteristic(long characteristicId)
        {
            RegisterAttributeService.RemoveRegisterAttribute(characteristicId);
        }

        
        #region Support Methods

        private void ValidateCharacteristic(CharacteristicDto characteristicDto)
        {
            if (string.IsNullOrWhiteSpace(characteristicDto.Name))
                throw new ArgumentException("Имя характеристики не может быть пустым");
        }

        private void CreateObjectCharacteristics(long registerId)
        {
            new OMObjectsCharacteristicsRegister
            {
                RegisterId = registerId
            }.Save();
        }

        #endregion

        #endregion
    }
}
