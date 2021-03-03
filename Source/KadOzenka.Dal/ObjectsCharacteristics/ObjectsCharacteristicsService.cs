using System;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using System.Transactions;
using Core.Register;
using Core.SRD;
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

        public ObjectsCharacteristicsService(IRegisterAttributeService registerAttributeService, IObjectCharacteristicsRepository objectCharacteristicsRepository)
        {
	        RegisterAttributeService = registerAttributeService;
	        ObjectCharacteristicsRepository = objectCharacteristicsRepository;
        }

        public OMAttributeSettings GetRegisterAttributeSettings(long attributeId)
        {
	        return OMAttributeSettings.Where(x => x.AttributeId == attributeId).SelectAll().ExecuteFirstOrDefault();
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
	        SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.ADMIN, exceptionOnAccessDenied: true);
            RegisterAttributeService.RemoveRegisterAttribute(characteristicId);
        }


        private void ValidateCharacteristic(CharacteristicDto characteristicDto)
        {
            if (string.IsNullOrWhiteSpace(characteristicDto.Name))
                throw new ArgumentException("Имя характеристики не может быть пустым");
        }
    }
}
