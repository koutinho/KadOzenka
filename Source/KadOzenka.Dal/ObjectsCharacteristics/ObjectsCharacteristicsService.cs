using System;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using System.Transactions;
using Core.Register;
using Core.SRD;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using KadOzenka.Dal.Registers;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;
using ObjectModel.KO;
using Platform.Configurator;
using Platform.Register;

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

        public static OMAttributeSettings GetRegisterAttributeSettings(long attributeId)
        {
	        return OMAttributeSettings.Where(x => x.AttributeId == attributeId).SelectAll().ExecuteFirstOrDefault();
        }


        #region Источник (Реестр)

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
                numberOfExistingRegistersWithCharacteristics++;
                var registerName = $"Gbu.Custom.Source{numberOfExistingRegistersWithCharacteristics}";
                var allpriTable = $"gbu_custom_source_{numberOfExistingRegistersWithCharacteristics}";
                var registerDescription = $"Источник: {sourceDto.RegisterDescription}";
                omRegister = RegisterService.CreateRegister(registerName, registerDescription, "GBU_MAIN_OBJECT", allpriTable, 5);

                RegisterService.CreateIdColumnForRegister(omRegister.RegisterId);

                RegisterConfigurator.CreateDbTableForRegister(omRegister.RegisterId);

                //DataCompositionByCharacteristicsService.CreateTriggerForRegister(omRegister.RegisterId);

                CreateObjectCharacteristics(omRegister.RegisterId);

                ts.Complete();
            }

            return omRegister.RegisterId;
        }

        public void EditSource(SourceDto sourceDto)
        {
            ValidateSource(sourceDto);

            var register = RegisterService.GetRegister(sourceDto.RegisterId);
            register.RegisterDescription = sourceDto.RegisterDescription;
            register.Save();
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


        #region Характеристика (Атрибут)

        public long AddCharacteristic(CharacteristicDto characteristicDto)
        {
            ValidateCharacteristic(characteristicDto);

            long id;
            using (var ts = new TransactionScope())
            {
	            var omAttribute = RegisterAttributeService.CreateRegisterAttribute(characteristicDto.Name,
		            characteristicDto.RegisterId, characteristicDto.Type, false, characteristicDto.ReferenceId);
	            id = omAttribute.Id;

                //TODO если будут еще различия, то разнести по двум разным сервисам
                //TODO (для ресстров с разделением по типу данных и реестров с разделением по атрибуту)
                var register = RegisterCache.GetRegisterData((int) characteristicDto.RegisterId);
                if (register.AllpriPartitioning == AllpriPartitioningType.AttributeId)
                {
	                SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.ADMIN, exceptionOnAccessDenied: true);
	                RegisterConfigurator.CreateDbTableForRegister(characteristicDto.RegisterId);
	                //DataCompositionByCharacteristicsService.CreateTriggerForRegister(register.Id, id);
                }
	            else
	            {
		            var dbConfigurator = RegisterConfigurator.GetDbConfigurator();
		            RegisterConfigurator.CreateDbColumnForRegister(omAttribute, dbConfigurator);
                }

	            CreateOrUpdateCharacteristicSetting(id,
		            characteristicDto.UseParentAttributeForLivingPlacement,
		            characteristicDto.UseParentAttributeForNotLivingPlacement,
		            characteristicDto.UseParentAttributeForCarPlace);

                ts.Complete();
            }

            return id;
        }

        public void EditCharacteristic(CharacteristicDto characteristicDto)
        {
            ValidateCharacteristic(characteristicDto);

            RegisterAttributeService.RenameRegisterAttribute(characteristicDto.Id, characteristicDto.Name);
            CreateOrUpdateCharacteristicSetting(characteristicDto.Id,
	            characteristicDto.UseParentAttributeForLivingPlacement,
	            characteristicDto.UseParentAttributeForNotLivingPlacement,
	            characteristicDto.UseParentAttributeForCarPlace);
        }

        public void DeleteCharacteristic(long characteristicId)
        {
	        SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.ADMIN, exceptionOnAccessDenied: true);
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

        private void CreateOrUpdateCharacteristicSetting(long attributeId, bool useParentAttributeForLivingPlacement, bool useParentAttributeForNotLivingPlacement, bool useParentAttributeForCarPlace)
        {
            var settings = OMAttributeSettings.Where(x => x.AttributeId == attributeId).SelectAll().ExecuteFirstOrDefault();
            if (settings == null)
            {
	            settings = new OMAttributeSettings
	            {
		            AttributeId = attributeId
	            };
            }

            settings.UseParentAttributeForLivingPlacements = useParentAttributeForLivingPlacement;
            settings.UseParentAttributeForNotLivingPlacements = useParentAttributeForNotLivingPlacement;
            settings.UseParentAttributeForCarPlace = useParentAttributeForCarPlace;
            settings.Save();
        }

        #endregion

        #endregion
    }
}
