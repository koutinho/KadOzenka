using System;
using System.Linq;
using System.Transactions;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.Registers;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.KO;
using Platform.Configurator;
using Platform.Configurator.DbConfigurator;

namespace KadOzenka.Dal.Tours
{
    public class TourFactorService
    {
        public RegisterService RegisterService { get; set; }

        public TourFactorService()
        {
            RegisterService = new RegisterService();
        }


        public OMRegister CreateTourFactorRegister(long tourId, bool isStead)
        {
            var tour = OMTour.Where(x => x.Id == tourId).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                throw new Exception($"Не найден тур с ИД {tourId}");
            }

            OMRegister omRegister;
            using (var ts = new TransactionScope())
            {
                omRegister = new OMRegister
                {
                    RegisterId = -1,
                    RegisterName = $"KO.Tour{(isStead ? "Zu" : "Oks")}Factors{tourId}",
                    RegisterDescription = $"Факторы {(isStead ? "Земельных участков" : "ОКС")} Тура {tourId}"
                };
                omRegister.QuantTable = omRegister.RegisterName.Replace(".", "_");
                omRegister.StorageType = 4;
                omRegister.ObjectSequence = "REG_OBJECT_SEQ";
                var registerId = omRegister.Save();

                var attribute = new OMAttribute
                {
                    Id = RegisterService.GetFirstAttributeId(registerId),
                    RegisterId = registerId,
                    Type = 1,
                    Name = "Идентификатор",
                    ValueField = "ID",
                    IsPrimaryKey = true,
                    IsNullable = false,
                };
                attribute.Save();

                RegisterConfigurator.CreateDbTableForRegister(registerId);

                if (isStead)
                {
                    CreateTourFactorRegister(tourId, registerId, PropertyTypes.Stead);
                }
                else
                {
                    CreateTourFactorRegister(tourId, registerId, PropertyTypes.Building);
                    CreateTourFactorRegister(tourId, registerId, PropertyTypes.Pllacement);
                    CreateTourFactorRegister(tourId, registerId, PropertyTypes.Construction);
                    CreateTourFactorRegister(tourId, registerId, PropertyTypes.UncompletedBuilding);
                }

                ts.Complete();
            }

            return omRegister;
        }

        public void RemoveTourFactorRegisters(long tourId)
        {
            var tour = OMTour.Where(x => x.Id == tourId).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                throw new Exception($"Не найден тур с ИД {tourId}");
            }

            using (var ts = new TransactionScope())
            {
                var omTourFactorRegisters = OMTourFactorRegister.Where(x => x.TourId == tourId).SelectAll().Execute();
                if (omTourFactorRegisters.Count > 0)
                {
                    foreach (var omTourFactorRegisterId in omTourFactorRegisters.Select(x => x.RegisterId).Distinct().ToList())
                    {
                        var omRegister = OMRegister.Where(x => x.RegisterId == omTourFactorRegisterId).SelectAll().ExecuteFirstOrDefault();
                        if (omRegister == null)
                        {
                            throw new Exception($"Не найден реестр факторов тура с ИД {omTourFactorRegisterId}");
                        }
                        omRegister.IsDeleted = true;
                        omRegister.Save();

                        var omRegisterAttributes = OMAttribute.Where(x => x.RegisterId == omTourFactorRegisterId).SelectAll().Execute();
                        foreach (var omRegisterAttribute in omRegisterAttributes)
                        {
                            omRegisterAttribute.IsDeleted = true;
                            omRegisterAttribute.Save();
                        }
                    }

                    foreach (var omTourFactorRegister in omTourFactorRegisters)
                    {
                        omTourFactorRegister.Destroy();
                    }
                }

                ts.Complete();
            }
        }

        public int CreateTourFactorRegisterAttribute(string attributeName, long registerId, RegisterAttributeType type, long? referenceId = null)
        {
            int id;
            using (var ts = new TransactionScope())
            {
                var omAttribute = new OMAttribute
                {
                    Id = -1,
                    RegisterId = registerId,
                    Name = attributeName,
                    Type = type == RegisterAttributeType.REFERENCE ? (long) RegisterAttributeType.STRING : (long) type,
                    IsNullable = true
                };
                id = omAttribute.Save();
                omAttribute.ValueField = $"field{id}";
                if (type == RegisterAttributeType.REFERENCE)
                {
                    omAttribute.CodeField = $"field{id}_code";
                    omAttribute.ReferenceId = referenceId;
                }
                omAttribute.Save();

                DbConfiguratorBase dbConfigurator = RegisterConfigurator.GetDbConfigurator();
                RegisterConfigurator.CreateDbColumnForRegister(omAttribute, dbConfigurator);

                ts.Complete();
            }

            return id;
        }

        public void RenameTourFactorRegisterAttribute(long attributeId, string newAttributeName)
        {
            var attribute = OMAttribute.Where(x => x.Id == attributeId).SelectAll().ExecuteFirstOrDefault();
            if (attribute == null)
            {
                throw new Exception($"Не найден атрибут с ИД {attributeId}");
            }
            attribute.Name = newAttributeName;

            attribute.Save();
        }

        public void RemoveTourFactorRegisterAttribute(long attributeId)
        {
            var attribute = OMAttribute.Where(x => x.Id == attributeId).SelectAll().ExecuteFirstOrDefault();
            if (attribute == null)
            {
                throw new Exception($"Не найден атрибут с ИД {attributeId}");
            }
            attribute.IsDeleted = true;
            attribute.Save();
        }

        #region Support Methods

        private static void CreateTourFactorRegister(long tourId, int registerId, PropertyTypes propertyType)
        {
            var omTourFactorRegister = new OMTourFactorRegister();
            omTourFactorRegister.Id = -1;
            omTourFactorRegister.TourId = tourId;
            omTourFactorRegister.RegisterId = registerId;
            omTourFactorRegister.ObjectType_Code = propertyType;
            omTourFactorRegister.Save();
        }

        #endregion
    }
}
