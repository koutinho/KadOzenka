﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Tours.Dto;
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

            return OMAttribute
                .Where(x => x.RegisterId == registerId && x.IsDeleted.Coalesce(false) == false &&
                            x.IsPrimaryKey.Coalesce(false) == false).OrderBy(x => x.Name).SelectAll().Execute();
        }

        public OMRegister GetTourRegister(long tourId, ObjectType objectType)
        {
            var existedTourFactorRegister = objectType == ObjectType.ZU
                ? OMTourFactorRegister
                    .Where(x => x.TourId == tourId && x.ObjectType_Code == PropertyTypes.Stead)
                    .SelectAll().Execute().FirstOrDefault()
                : OMTourFactorRegister
                    .Where(x => x.TourId == tourId && x.ObjectType_Code != PropertyTypes.Stead)
                    .SelectAll().Execute().FirstOrDefault();

            if (existedTourFactorRegister == null)
                return null;

            var register = OMRegister.Where(x => x.RegisterId == existedTourFactorRegister.RegisterId).SelectAll()
                .Execute().FirstOrDefault();

            return register;
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
                var omAttribute = RegisterAttributeService.CreateRegisterAttribute(attributeName, registerId, type, true, referenceId);
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


        #region Tour Settings

        public List<AttributeSettingsDto> GetTourAttributesFromSettings(long tourId)
        {
            var tourAttributeSettings = OMTourAttributeSettings.Where(x => x.TourId == tourId)
                .SelectAll().Execute();

            return tourAttributeSettings
                .Select(x =>
                    new AttributeSettingsDto
                        { AttributeId = x.AttributeId, KoAttributeUsingType = x.AttributeUsingType_Code }
                )
                .ToList();
        }

        public RegisterAttribute GetTourAttributeFromSettings(long tourId, KoAttributeUsingType type)
        {
            var attributeId = OMTourAttributeSettings
                .Where(x => x.TourId == tourId && x.AttributeUsingType_Code == type)
                .Select(x => x.AttributeId)
                .ExecuteFirstOrDefault()?.AttributeId;

            return attributeId == null ? null : RegisterCache.RegisterAttributes.Values.First(x => x.Id == attributeId);
        }

        public TourEstimatedGroupAttributeParamsDto GetEstimatedGroupModelParamsForTask(long taskId)
        {
            var task = OMTask.Where(x => x.Id == taskId).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                throw new Exception($"Не найдено задание на оценку с ИД {taskId}");
            }
            var tour = OMTour.Where(x => x.Id == task.TourId.GetValueOrDefault()).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
            {
                throw new Exception($"Не найден тур для задания на оценку с ИД {taskId}");
            }

            var tourKoAttributeSettings = OMTourAttributeSettings.Where(x => x.TourId == tour.Id)
                .SelectAll().Execute();
            if (tourKoAttributeSettings.IsEmpty())
            {
                throw new Exception($"Для тура {tour.Year} не заданы настройки использования заданных атрибутов");
            }

            var paramsDto = new TourEstimatedGroupAttributeParamsDto();
            var tourCodeGroupAttributeId = tourKoAttributeSettings.FirstOrDefault(x =>
                x.AttributeUsingType_Code == KoAttributeUsingType.CodeGroupAttribute && x.AttributeId.HasValue)?.AttributeId;
            if (!tourCodeGroupAttributeId.HasValue)
            {
                throw new Exception($"Для тура {tour.Year} не задан {KoAttributeUsingType.CodeGroupAttribute.GetEnumDescription()}");
            }
            paramsDto.IdCodeGroup = tourCodeGroupAttributeId.Value;

            var codeQuarterAttributeId = tourKoAttributeSettings.FirstOrDefault(x =>
                x.AttributeUsingType_Code == KoAttributeUsingType.CodeQuarterAttribute && x.AttributeId.HasValue)?.AttributeId;
            if (!codeQuarterAttributeId.HasValue)
            {
                throw new Exception($"Для тура {tour.Year} не задан {KoAttributeUsingType.CodeQuarterAttribute.GetEnumDescription()}");
            }
            paramsDto.IdCodeQuarter = codeQuarterAttributeId.Value;

            var tourTypeRoomAttributeId = tourKoAttributeSettings.FirstOrDefault(x =>
                x.AttributeUsingType_Code == KoAttributeUsingType.TypeRoomAttribute && x.AttributeId.HasValue)?.AttributeId;
            if (!tourTypeRoomAttributeId.HasValue)
            {
                throw new Exception($"Для тура {tour.Year} не задан {KoAttributeUsingType.TypeRoomAttribute.GetEnumDescription()}");
            }
            paramsDto.IdTypeRoom = tourTypeRoomAttributeId.Value;

            return paramsDto;
        }

        #endregion

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