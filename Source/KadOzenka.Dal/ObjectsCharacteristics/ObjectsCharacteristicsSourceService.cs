using System.Transactions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Dal.ObjectsCharacteristics.Exceptions;
using KadOzenka.Dal.ObjectsCharacteristics.Repositories;
using KadOzenka.Dal.ObjectsCharacteristics.Resources;
using KadOzenka.Dal.Registers;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.ObjectsCharacteristics
{
    public class ObjectsCharacteristicsSourceService : IObjectsCharacteristicsSourceService
    {
        public static readonly int RegisterStorageType = 5;
        public IRegisterService RegisterService { get; }
        public IObjectCharacteristicsRepository ObjectCharacteristicsRepository { get; }
        public IRegisterConfiguratorWrapper RegisterConfiguratorWrapper { get; }

        public ObjectsCharacteristicsSourceService(IRegisterService registerService,
            IObjectCharacteristicsRepository objectCharacteristicsRepository,
            IRegisterConfiguratorWrapper registerConfiguratorWrapper)
        {
            RegisterService = registerService;
            ObjectCharacteristicsRepository = objectCharacteristicsRepository;
            RegisterConfiguratorWrapper = registerConfiguratorWrapper;
        }

        public SourceDto GetSource(long registerId)
        {
            var register = RegisterService.GetRegister(registerId);
            if (register == null)
                throw new SourceDoesNotExistException(registerId);

            var characteristicsRegister = OMObjectsCharacteristicsRegister.Where(x => x.RegisterId == registerId)
                .SelectAll().ExecuteFirstOrDefault();

            return new SourceDto
            {
                RegisterId = registerId,
                RegisterDescription = register.RegisterDescription,
                DisableAttributeEditing = characteristicsRegister.DisableEditing
            };
        }

        public long AddSource(SourceDto sourceDto)
        {
            ValidateSource(sourceDto);

            OMRegister omRegister;
            using (var ts = new TransactionScope())
            {
                var numberOfExistingRegistersWithCharacteristics =
                    ObjectCharacteristicsRepository.GetNumberOfExistingRegistersWithCharacteristics();
                numberOfExistingRegistersWithCharacteristics++;
                var registerName = string.Format(Fields.RegisterName, numberOfExistingRegistersWithCharacteristics);
                var allpriTable = string.Format(Fields.AllpriTable, numberOfExistingRegistersWithCharacteristics);
                var registerDescription = string.Format(Fields.RegisterDescription, sourceDto.RegisterDescription);
                omRegister = RegisterService.CreateRegister(registerName, registerDescription, Fields.QuantTable,
                    allpriTable, RegisterStorageType);

                RegisterService.CreateIdColumnForRegister(omRegister.RegisterId);

                RegisterConfiguratorWrapper.CreateDbTableForRegister(omRegister.RegisterId);

                //DataCompositionByCharacteristicsService.CreateTriggerForRegister(omRegister.RegisterId);

                ObjectCharacteristicsRepository.CreateObjectCharacteristics(omRegister.RegisterId,
                    sourceDto.DisableAttributeEditing.GetValueOrDefault(false));

                ts.Complete();
            }

            return omRegister.RegisterId;
        }

        public void EditSource(SourceDto sourceDto)
        {
            ValidateSource(sourceDto);

            var register = RegisterService.GetRegister(sourceDto.RegisterId);
            if (register == null)
                throw new SourceDoesNotExistException(sourceDto.RegisterId);

            register.RegisterDescription = sourceDto.RegisterDescription;
            ObjectCharacteristicsRepository.SaveRegister(register, sourceDto.DisableAttributeEditing);
        }

        private void ValidateSource(SourceDto sourceDto)
        {
            if (string.IsNullOrWhiteSpace(sourceDto.RegisterDescription))
                throw new EmptyCharacteristicSourceNameException();
        }
    }
}