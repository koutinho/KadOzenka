using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.RecycleBin;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Tours.Dto;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
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
        public RecycleBinService RecycleBinService { get; }

        public TourFactorService()
        {
	        RecycleBinService = new RecycleBinService();
            RegisterService = new RegisterService(RecycleBinService, new RegisterRepository());
            RegisterAttributeService = new RegisterAttributeService();
        }

        public List<OMAttribute> GetTourAttributes(long tourId, ObjectTypeExtended objectType)
        {
            List<OMTourFactorRegister> existedTourFactorRegisters;
            switch (objectType)
            {
                case ObjectTypeExtended.Oks:
                    existedTourFactorRegisters = OMTourFactorRegister
                        .Where(x => x.TourId == tourId && x.ObjectType_Code != PropertyTypes.Stead)
                        .SelectAll().Execute();
                    break;
                case ObjectTypeExtended.Zu:
                    existedTourFactorRegisters = OMTourFactorRegister
                        .Where(x => x.TourId == tourId && x.ObjectType_Code == PropertyTypes.Stead)
                        .SelectAll().Execute();
                    break;
                case ObjectTypeExtended.Both:
                    existedTourFactorRegisters = OMTourFactorRegister
                        .Where(x => x.TourId == tourId)
                        .SelectAll().Execute();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(objectType), objectType, null);
            }

            if (existedTourFactorRegisters.Count == 0)
                return new List<OMAttribute>();

            var registerIds = existedTourFactorRegisters.Select(x => x.RegisterId).Distinct().ToList();

            return OMAttribute
                .Where(x => registerIds.Contains(x.RegisterId) && x.IsDeleted.Coalesce(false) == false &&
                            x.IsPrimaryKey.Coalesce(false) == false).OrderBy(x => x.Name).SelectAll().Execute();
        }

        public List<OMAttribute> GetTourAllAttributes(long tourId)
        {
            var result = GetTourAttributes(tourId, ObjectTypeExtended.Oks);
            result.AddRange(GetTourAttributes(tourId, ObjectTypeExtended.Zu));

            return result;
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

        public List<UnitFactor> GetUnitFactorValues(OMUnit unit, List<long> attributes = null)
        {
            var tourRegister = GetTourRegister(unit.TourId.GetValueOrDefault(),
                unit.PropertyType_Code == PropertyTypes.Stead ? ObjectType.ZU : ObjectType.Oks);
            if (tourRegister == null)
                throw new Exception(
                    $"Не найден реестр факторов для тура с ИД {unit.TourId} для типа объекта {unit.PropertyType_Code.GetEnumDescription()}");

            var tourAttributes =
                RegisterAttributeService.GetActiveRegisterAttributes(tourRegister.RegisterId, attributes);
            if (tourAttributes.IsEmpty())
                return new List<UnitFactor>();

            var query = GetUnitFactorsQuery(unit.Id, tourRegister);
            foreach (var factor in tourAttributes)
            {
                if (factor.IsPrimaryKey != null && factor.IsPrimaryKey.Value)
                    continue;

                query.AddColumn(factor.Id, factor.Id.ToString());
            }

            var results = new List<UnitFactor>();
            var table = query.ExecuteQuery();
            foreach (var factor in tourAttributes)
            {
                if (factor.IsPrimaryKey != null && factor.IsPrimaryKey.Value)
                    continue;

                var attr = new UnitFactor(factor.Id);
                if (table.Rows.Count > 0)
                {
                    attr.SetFactorValue(table.Rows[0][factor.Id.ToString()].ParseToStringNullable());
                }

                results.Add(attr);
            }

            return results;
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

            var tourKoAttributeSettings = OMTourAttributeSettings.Where(x => x.TourId == tour.Id)
                .SelectAll().Execute();
            if (tourKoAttributeSettings.IsEmpty())
            {
                throw new Exception($"Для тура {tour.Year} не заданы настройки использования заданных атрибутов");
            }

            var paramsDto = new TourEstimatedGroupAttributeParamsDto();
            var tourCodeGroupAttributeId = tourKoAttributeSettings.FirstOrDefault(x =>
                    x.AttributeUsingType_Code == KoAttributeUsingType.CodeGroupAttribute && x.AttributeId.HasValue)
                ?.AttributeId;
            if (!tourCodeGroupAttributeId.HasValue)
            {
                throw new Exception(
                    $"Для тура {tour.Year} не задан {KoAttributeUsingType.CodeGroupAttribute.GetEnumDescription()}");
            }

            paramsDto.IdCodeGroup = tourCodeGroupAttributeId.Value;

            var codeQuarterAttributeId = tourKoAttributeSettings.FirstOrDefault(x =>
                    x.AttributeUsingType_Code == KoAttributeUsingType.CodeQuarterAttribute && x.AttributeId.HasValue)
                ?.AttributeId;
            if (!codeQuarterAttributeId.HasValue)
            {
                throw new Exception(
                    $"Для тура {tour.Year} не задан {KoAttributeUsingType.CodeQuarterAttribute.GetEnumDescription()}");
            }

            paramsDto.IdCodeQuarter = codeQuarterAttributeId.Value;

            var tourTerritoryTypeAttributeId = tourKoAttributeSettings.FirstOrDefault(x =>
                    x.AttributeUsingType_Code == KoAttributeUsingType.TerritoryTypeAttribute && x.AttributeId.HasValue)
                ?.AttributeId;
            if (!tourTerritoryTypeAttributeId.HasValue)
            {
                throw new Exception(
                    $"Для тура {tour.Year} не задан {KoAttributeUsingType.TerritoryTypeAttribute.GetEnumDescription()}");
            }

            paramsDto.IdTerritoryType = tourTerritoryTypeAttributeId.Value;

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

        private QSQuery GetUnitFactorsQuery(long unitId, OMRegister tourRegister)
        {
            var tourRegisterPrimaryKeyId = RegisterCache.RegisterAttributes.Values
                .FirstOrDefault(x => x.RegisterId == tourRegister.RegisterId && x.IsPrimaryKey)?.Id;
            var qsConditionGroup = new QSConditionGroup(QSConditionGroupType.And);
            qsConditionGroup.Add(new QSConditionSimple
            {
                ConditionType = QSConditionType.Equal,
                LeftOperand = new QSColumnSimple(tourRegisterPrimaryKeyId.GetValueOrDefault()),
                RightOperand = new QSColumnConstant(unitId)
            });
            var query = new QSQuery
            {
                MainRegisterID = (int) tourRegister.RegisterId,
                Condition = qsConditionGroup
            };

            return query;
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