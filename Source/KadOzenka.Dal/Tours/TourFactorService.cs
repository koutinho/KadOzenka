using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using CommonSdks.RecycleBin;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Tours.Dto;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using ModelingBusiness.Factors;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.KO;
using Platform.Configurator;
using Platform.Configurator.DbConfigurator;

namespace KadOzenka.Dal.Tours
{
    public class TourFactorService : ITourFactorService
    {
        public RegisterService RegisterService { get; set; }
        public RegisterAttributeService RegisterAttributeService { get; set; }
        public RecycleBinService RecycleBinService { get; }

        public TourFactorService()
        {
	        RecycleBinService = new RecycleBinService();
            RegisterService = new RegisterService(RecycleBinService, new RegisterRepository());
            RegisterAttributeService = new RegisterAttributeService();
        }

        public List<OMAttribute> GetTourAttributes(long tourId, ObjectTypeExtended objectType)
        {
            List<long> existedTourFactorRegisters;
            switch (objectType)
            {
                case ObjectTypeExtended.Oks:
                    existedTourFactorRegisters = GetTourRegisterIds(tourId, true);
                    break;
                case ObjectTypeExtended.Zu:
                    existedTourFactorRegisters = GetTourRegisterIds(tourId, false);
                    break;
                case ObjectTypeExtended.Both:
                    existedTourFactorRegisters = OMTourFactorRegister
                        .Where(x => x.TourId == tourId && x.RegisterId != null)
                        .Select(x => x.RegisterId).Execute()
                        .Select(x => x.RegisterId.Value).Distinct().ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(objectType), objectType, null);
            }

            if (existedTourFactorRegisters.Count == 0)
                return new List<OMAttribute>();

            return GetTourRegistersAttributes(existedTourFactorRegisters);
        }

        public TourAttributesDto GetAllTourAttributes(long tourId)
        {
	        var oksRegisterIds = GetTourRegisterIds(tourId, true);
	        var zuRegisterIds = GetTourRegisterIds(tourId, false);

	        if (oksRegisterIds.Count == 0 && zuRegisterIds.Count == 0)
		        return new TourAttributesDto();

	        return new TourAttributesDto
	        {
		        Oks = GetTourRegistersAttributes(oksRegisterIds),
		        Zu = GetTourRegistersAttributes(zuRegisterIds)
            };
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
                var registerDescription = $"Факторы {(isStead ? "Земельных участков" : "ОКС")} Тура {tour.Year}";
                var quantTable = registerName.Replace(".", "_");
                omRegister = RegisterService.CreateRegister(registerName, registerDescription, quantTable);
                var registerId = omRegister.RegisterId;

                RegisterService.CreateIdColumnForRegister(registerId);

                RegisterConfigurator.CreateDbTableForRegister(registerId);

                RecycleBinService.CreateDeletedTable(registerId, quantTable);

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

        public void RemoveTourFactorRegistersLogically(long tourId, long eventId)
        {
            using (var ts = new TransactionScope())
            {
                var omTourFactorRegisters = OMTourFactorRegister.Where(x => x.TourId == tourId).SelectAll().Execute();
                if (omTourFactorRegisters.Count > 0)
                {
                    foreach (var omTourFactorRegisterId in omTourFactorRegisters.Select(x => x.RegisterId).Distinct()
                        .ToList())
                    {
                        RegisterService.RemoveRegister(omTourFactorRegisterId.Value, eventId);
                    }

                    RecycleBinService.MoveObjectsToRecycleBin(omTourFactorRegisters.Select(x => x.Id).ToList(),
                        OMTourFactorRegister.GetRegisterId(), eventId);
                }

                ts.Complete();
            }
        }

        public long CreateTourFactorRegisterAttribute(string attributeName, long registerId, RegisterAttributeType type,
            long? referenceId = null)
        {
            if (string.IsNullOrWhiteSpace(attributeName))
                throw new ArgumentException("Имя фактора не может быть пустым");

            long id;
            using (var ts = new TransactionScope())
            {
                var omAttribute =
                    RegisterAttributeService.CreateRegisterAttribute(attributeName, registerId, type, true,
                        referenceId);
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
            var modelFactorService = new ModelFactorsService();
            var attrUsage = CheckFactorUsage(attributeId);
            if (attrUsage.ApprovedModels.Count == 0)
            {
                foreach (var modelDto in attrUsage.AffectedModels)
                {
                    var model = OMModel.Where(x => x.Id == modelDto.Id).SelectAll().ExecuteFirstOrDefault();
                    var modelFactor = OMModelFactor.Where(x => x.FactorId == attributeId && x.ModelId == model.Id)
                        .ExecuteFirstOrDefault();
                    if (model.Type_Code == KoModelType.Automatic)
                    {
                        modelFactorService.DeleteAutomaticModelFactor(modelFactor.Id);
                    }

                    if (model.Type_Code == KoModelType.Manual)
                    {
                        modelFactorService.DeleteManualModelFactor(modelFactor.Id);
                    }
                }
                RegisterAttributeService.RemoveRegisterAttribute(attributeId);
            }
            else
            {
                throw new Exception($"Аттрибут с номером {attributeId} используется в утвержденных моделях");
            }
        }

        public static AttributeUsageStats CheckFactorUsage(long attributeId)
        {
            var attributeStats = new AttributeUsageStats();
            var modelsUsingFactor = OMModelFactor.Where(x => x.FactorId == attributeId).SelectAll().Execute();
            if (modelsUsingFactor.Count == 0) return attributeStats;

            var modelIdsUsingFactor = modelsUsingFactor.Select(x => x.ModelId).ToList();
            var models = OMModel.Where(x => modelIdsUsingFactor.Contains(x.Id)).SelectAll().Execute();
            models.Where(x => x.IsActive == true)
                .ForEach(x => attributeStats.ApprovedModels.Add(new ModelDto(x.Id, x.Name)));
            models.Where(x => x.IsActive != true)
                .ForEach(x => attributeStats.AffectedModels.Add(new ModelDto(x.Id, x.Name)));
            return attributeStats;
        }


        #region Tour Settings

        public List<AttributeSettingsDto> GetTourAttributesFromSettings(long tourId)
        {
            var tourAttributeSettings = OMTourAttributeSettings.Where(x => x.TourId == tourId)
                .SelectAll().Execute();

            return tourAttributeSettings
                .Select(x =>
                    new AttributeSettingsDto
                        {AttributeId = x.AttributeId, KoAttributeUsingType = x.AttributeUsingType_Code}
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

            // var tourKoAttributeSettings = OMTourAttributeSettings.Where(x => x.TourId == tour.Id)
            //     .SelectAll().Execute();
            // if (tourKoAttributeSettings.IsEmpty())
            // {
            //     throw new Exception($"Для тура {tour.Year} не заданы настройки использования заданных атрибутов");
            // }

            var paramsDto = new TourEstimatedGroupAttributeParamsDto();
            // var tourCodeGroupAttributeId = tourKoAttributeSettings.FirstOrDefault(x =>
            //         x.AttributeUsingType_Code == KoAttributeUsingType.CodeGroupAttribute && x.AttributeId.HasValue)
            //     ?.AttributeId;
            // if (!tourCodeGroupAttributeId.HasValue)
            // {
            //     throw new Exception(
            //         $"Для тура {tour.Year} не задан {KoAttributeUsingType.CodeGroupAttribute.GetEnumDescription()}");
            // }
            //
            // paramsDto.IdCodeGroup = tourCodeGroupAttributeId.Value;

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

        private List<OMAttribute> GetTourRegistersAttributes(List<long> registerIds)
        {
	        if (registerIds.Count == 0)
		        return new List<OMAttribute>();

	        return OMAttribute
		        .Where(x => registerIds.Contains(x.RegisterId) && x.IsDeleted.Coalesce(false) == false &&
		                    x.IsPrimaryKey.Coalesce(false) == false).OrderBy(x => x.Name).SelectAll().Execute();
        }

        private List<long> GetTourRegisterIds(long tourId, bool isOks)
        {
	        Expression<Func<OMTourFactorRegister, bool>> baseExpression = x => x.TourId == tourId && x.RegisterId != null;

	        Expression<Func<OMTourFactorRegister, bool>> typeCondition = isOks
		        ? x => x.ObjectType_Code != PropertyTypes.Stead
		        : x => x.ObjectType_Code == PropertyTypes.Stead;

	        var body = Expression.AndAlso(baseExpression.Body, typeCondition.Body);
	        var lambda = Expression.Lambda<Func<OMTourFactorRegister, bool>>(body, baseExpression.Parameters[0]);

	        return OMTourFactorRegister
		        .Where(lambda)
		        .Select(x => x.RegisterId)
		        .Execute().Select(x => x.RegisterId.Value).Distinct().ToList();
        }

        #endregion

        #region Support Classes

        public class AttributeUsageStats
        {
            /// <summary>
            /// Список утверждённых моделей, которые используют атрибут
            /// </summary>
            public List<ModelDto> ApprovedModels { get; set; } = new List<ModelDto>();

            /// <summary>
            /// Список не утверждённых моделей, которые используют атрибут
            /// </summary>
            public List<ModelDto> AffectedModels { get; set; } = new List<ModelDto>();
        }

        public class ModelDto
        {
            public long Id { get; set; }
            public string Name { get; set; }

            public ModelDto(long Id, string Name)
            {
                this.Id = Id;
                this.Name = Name;
            }
        }

        #endregion
    }
}