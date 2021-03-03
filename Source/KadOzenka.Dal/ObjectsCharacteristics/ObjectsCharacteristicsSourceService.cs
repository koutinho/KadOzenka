using System;
using System.Transactions;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Dal.Registers;
using ObjectModel.Core.Register;
using Platform.Configurator;

namespace KadOzenka.Dal.ObjectsCharacteristics
{
	public class ObjectsCharacteristicsSourceService : IObjectsCharacteristicsSourceService
	{
		public IRegisterService RegisterService { get; }
		public IObjectCharacteristicsRepository ObjectCharacteristicsRepository { get; }

        public ObjectsCharacteristicsSourceService(IRegisterService registerService, IObjectCharacteristicsRepository objectCharacteristicsRepository)
		{
			RegisterService = registerService;
			ObjectCharacteristicsRepository = objectCharacteristicsRepository;
        }

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
                var numberOfExistingRegistersWithCharacteristics = ObjectCharacteristicsRepository.GetNumberOfExistingRegistersWithCharacteristics();
                numberOfExistingRegistersWithCharacteristics++;
                var registerName = $"Gbu.Custom.Source{numberOfExistingRegistersWithCharacteristics}";
                var allpriTable = $"gbu_custom_source_{numberOfExistingRegistersWithCharacteristics}";
                var registerDescription = $"Источник: {sourceDto.RegisterDescription}";
                omRegister = RegisterService.CreateRegister(registerName, registerDescription, "GBU_MAIN_OBJECT", allpriTable, 5);

                RegisterService.CreateIdColumnForRegister(omRegister.RegisterId);

                RegisterConfigurator.CreateDbTableForRegister(omRegister.RegisterId);

                //DataCompositionByCharacteristicsService.CreateTriggerForRegister(omRegister.RegisterId);

                ObjectCharacteristicsRepository.CreateObjectCharacteristics(omRegister.RegisterId);

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

        private void ValidateSource(SourceDto sourceDto)
        {
            if (string.IsNullOrWhiteSpace(sourceDto.RegisterDescription))
                throw new ArgumentException("Имя источника не может быть пустым");
        }
	}
}
