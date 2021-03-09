using System;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using System.Transactions;
using Core.Register;
using Core.SRD;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.ObjectsCharacteristics.Exceptions;
using KadOzenka.Dal.ObjectsCharacteristics.Repositories;
using KadOzenka.Dal.ObjectsCharacteristics.Resources;
using KadOzenka.Dal.Registers;
using ObjectModel.Gbu;
using Platform.Configurator;
using Platform.Register;

namespace KadOzenka.Dal.ObjectsCharacteristics
{
    public class ObjectsCharacteristicsService : IObjectsCharacteristicsService
    {
	    public IRegisterAttributeService RegisterAttributeService { get; }
        public IObjectCharacteristicsRepository ObjectCharacteristicsRepository { get; }
        public ISRDSessionWrapper SRDSessionWrapper { get; }
        public IRegisterConfiguratorWrapper RegisterConfiguratorWrapper { get; }
        public IRegisterCacheWrapper RegisterCacheWrapper { get; }

        public ObjectsCharacteristicsService(IRegisterAttributeService registerAttributeService, IObjectCharacteristicsRepository objectCharacteristicsRepository,
	        ISRDSessionWrapper srdSessionWrapper, IRegisterConfiguratorWrapper registerConfiguratorWrapper, IRegisterCacheWrapper registerCacheWrapper)
        {
	        RegisterAttributeService = registerAttributeService;
	        ObjectCharacteristicsRepository = objectCharacteristicsRepository;
	        SRDSessionWrapper = srdSessionWrapper;
	        RegisterConfiguratorWrapper = registerConfiguratorWrapper;
	        RegisterCacheWrapper = registerCacheWrapper;
        }

        public OMAttributeSettings GetRegisterAttributeSettings(long attributeId)
        {
            return ObjectCharacteristicsRepository.GetRegisterAttributeSettings(attributeId);
        }

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
                var register = RegisterCacheWrapper.GetRegisterData((int) characteristicDto.RegisterId);
                if (register.AllpriPartitioning == AllpriPartitioningType.AttributeId)
                {
	                SRDSessionWrapper.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.ADMIN, true);
	                RegisterConfiguratorWrapper.CreateDbTableForRegister(characteristicDto.RegisterId);
	                //DataCompositionByCharacteristicsService.CreateTriggerForRegister(register.Id, id);
                }
	            else
	            {
		            var dbConfigurator = RegisterConfiguratorWrapper.GetDbConfigurator();
		            RegisterConfiguratorWrapper.CreateDbColumnForRegister(omAttribute, dbConfigurator);
                }

                ObjectCharacteristicsRepository.CreateOrUpdateCharacteristicSetting(id,
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
            ObjectCharacteristicsRepository.CreateOrUpdateCharacteristicSetting(characteristicDto.Id,
	            characteristicDto.UseParentAttributeForLivingPlacement,
	            characteristicDto.UseParentAttributeForNotLivingPlacement,
	            characteristicDto.UseParentAttributeForCarPlace);
        }

        public void DeleteCharacteristic(long characteristicId)
        {
	        SRDSessionWrapper.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.ADMIN, true);
	        RegisterAttributeService.RemoveRegisterAttribute(characteristicId);
        }

        private void ValidateCharacteristic(CharacteristicDto characteristicDto)
        {
            if (string.IsNullOrWhiteSpace(characteristicDto.Name))
                throw new EmptyCharacteristicNameException();
        }
    }
}
