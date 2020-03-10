using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.Register;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.Oks;
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
        public RegisterAttributeService RegisterAttributeService { get; set; }

        public TourFactorService()
        {
            RegisterService = new RegisterService();
            RegisterAttributeService = new RegisterAttributeService();
        }

        public List<OMAttribute> GetTourAttributes(long tourId, ObjectType objectType)
        {
            var existedTourFactorRegisters = objectType == ObjectType.ZU
                ? OMTourFactorRegister
                    .Where(x => x.TourId == tourId && x.ObjectType_Code == PropertyTypes.Stead)
                    .SelectAll().Execute()
                : OMTourFactorRegister
                    .Where(x => x.TourId == tourId && x.ObjectType_Code != PropertyTypes.Stead)
                    .SelectAll().Execute();

            if (existedTourFactorRegisters.Count == 0)
                return new List<OMAttribute>();

            var registerId = existedTourFactorRegisters.First().RegisterId;

            return OMAttribute.Where(x => x.RegisterId == registerId && x.IsDeleted.Coalesce(false) == false).OrderBy(x => x.Name).SelectAll().Execute();
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
                var registerName = $"KO.Tour{(isStead ? "Zu" : "Oks")}Factors{tourId}";
                var registerDescription = $"Факторы {(isStead ? "Земельных участков" : "ОКС")} Тура {tourId}";
                var quantTable = registerName.Replace(".", "_");
                omRegister = RegisterService.CreateRegister(registerName, registerDescription, quantTable);
                var registerId = omRegister.RegisterId;

                RegisterService.CreateIdColumnForRegister(registerId);

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
                        RegisterService.RemoveRegister(omTourFactorRegisterId.Value);
                    }

                    foreach (var omTourFactorRegister in omTourFactorRegisters)
                    {
                        omTourFactorRegister.Destroy();
                    }
                }

                ts.Complete();
            }
        }

        public long CreateTourFactorRegisterAttribute(string attributeName, long registerId, RegisterAttributeType type, long? referenceId = null)
        {
            long id;
            using (var ts = new TransactionScope())
            {
                var omAttribute = RegisterAttributeService.CreateRegisterAttribute(attributeName, registerId, type, referenceId);
                id = omAttribute.Id;

                DbConfiguratorBase dbConfigurator = RegisterConfigurator.GetDbConfigurator();
                RegisterConfigurator.CreateDbColumnForRegister(omAttribute, dbConfigurator);

                ts.Complete();
            }

            return id;
        }

        public void RenameTourFactorRegisterAttribute(long attributeId, string newAttributeName)
        {
            RegisterAttributeService.RenameRegisterAttribute(attributeId, newAttributeName);
        }

        public void RemoveTourFactorRegisterAttribute(long attributeId)
        {
            RegisterAttributeService.RemoveRegisterAttribute(attributeId);
        }

        #region Support Methods

        private static void CreateTourFactorRegister(long tourId, long registerId, PropertyTypes propertyType)
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
