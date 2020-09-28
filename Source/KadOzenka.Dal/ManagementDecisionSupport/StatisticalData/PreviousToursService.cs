﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Directory;
using ObjectModel.KO;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using KadOzenka.Dal.Registers;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
    public class PreviousToursService
    {
        protected readonly GbuObjectService GbuObjectService;
        protected readonly StatisticalDataService StatisticalDataService;
        protected readonly RosreestrRegisterService RosreestrRegisterService;
        protected readonly FactorsService FactorsService;

        private const string ReportTitle = "Таблица. Состав данных о результатах кадастровой оценки предыдущих туров";
        private const string TypeTitle = "Тип";
        private const string SquareTitle = "Площадь";
        private const string NameTitle = "Наименование";
        private const string PurposeTitle = "Назначение (для ОКС)";
        private const string PermittedUseTitle = "Разрешенное использование (для ЗУ)";
        private const string AddressTitle = "Адрес";
        private const string LocationTitle = "Местоположение";
        private const string CadastralQuartalTitle = "Кадастровый квартал";
        private const string ParentCadastralNumberTitle = "Кадастровый номер земельного участка в пределах которого расположен объект недвижимости";
        private const string BuildYearTitle = "Год постройки (для ОКС)";
        private const string CommissioningYearTitle = "Год ввода в эксплуатацию (для ОКС)";
        private const string FloorsNumberTitle = "Кол-во этажей (для ОКС)";
        private const string UnderGroundFloorsNumberTitle = "Подземных этажей (для ОКС)";
        private const string WallMaterialTitle = "Материал стен (для ОКС)";
        private const string GroupOrSubgroupTitle = "Группа/ Подгруппа";
        private const string CadastralCostTitle = "Кадастровая стоимость";

        private readonly List<string> _columnTitles = new List<string>
        {
            TypeTitle, SquareTitle, NameTitle, PurposeTitle, PermittedUseTitle,
            AddressTitle, LocationTitle, CadastralQuartalTitle, ParentCadastralNumberTitle,
            BuildYearTitle, CommissioningYearTitle, FloorsNumberTitle, UnderGroundFloorsNumberTitle,
            WallMaterialTitle, GroupOrSubgroupTitle, CadastralCostTitle
        };

        public PreviousToursService()
        {
            GbuObjectService = new GbuObjectService();
            StatisticalDataService = new StatisticalDataService();
            RosreestrRegisterService = new RosreestrRegisterService();
            FactorsService = new FactorsService();
        }

        public PreviousToursReportInfo GetReportInfo(List<long> taskIds, long groupId)
        {
	        var model = OMModel.Where(x => x.GroupId == groupId).ExecuteFirstOrDefault();
	        var factorsByRegisters = model == null
		        ? new List<FactorsService.PricingFactors>()
		        : FactorsService.GetGroupedModelFactors(model.Id);
	        var attributes = factorsByRegisters.SelectMany(x => x.Attributes).ToList();

            var tours = GetToursByTasks(taskIds);
            var units = GetUnits(taskIds, groupId);
            if (units == null || units.Count == 0)
            {
                return new PreviousToursReportInfo
                {
                    Title = ReportTitle,
                    ColumnTitles = _columnTitles,
                    Tours = tours.OrderBy(x => x.Year).ToList(),
                    PricingFactors = attributes.OrderBy(x => x.Name).ToList()
                };
            }

            var objectIds = units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList();
            var tourAttributes = GetToursAttributes(tours);
            var rosreestrAttributes = GetRosreestrAttributes();
            var gbuAttributes = GetGbuAttributes(objectIds, tourAttributes, rosreestrAttributes);

            var pricingFactors = FactorsService.GetPricingFactorsForUnits(units.Select(x => x.Id).Distinct().ToList(), factorsByRegisters);

            var items = new List<PreviousTourReportItem>();
            units.ToList().ForEach(unit =>
            {
                var tour = tours.FirstOrDefault(x => x.Id == unit.TourId);
                var objectFactors = pricingFactors.TryGetValue(unit.Id, out var value) ? value : attributes;

                var item = new PreviousTourReportItem
                {
                    TourYear = tour?.Year,
                    CadastralNumber = unit.CadastralNumber,
                    Square = unit.Square,
                    CadastralCost = unit.CadastralCost,
                    Factors = objectFactors
                };

                var objectAttributes = gbuAttributes.Where(x => x.ObjectId == unit.ObjectId).ToList();
                SetRosreestrAttributes(objectAttributes, rosreestrAttributes, item);
                SetAttributesFromTourSettings(objectAttributes, tour?.Id, tourAttributes, item);
                SetAttributesAccordingToObjectType(unit.PropertyType_Code, item);

                items.Add(item);
            });

            return new PreviousToursReportInfo
            {
                Title = ReportTitle,
                ColumnTitles = _columnTitles,
                Items = items.OrderBy(x => x.CadastralNumber).ThenBy(x => x.TourYear).ToList(),
                Tours = tours.OrderBy(x => x.Year).ToList(),
                PricingFactors = attributes.OrderBy(x => x.Name).ToList()
            };
        }

        public object GetValueForReportItem(string title, PreviousTourReportItem item)
        {
            if (item == null)
                return null;

            switch (title)
            {
                case TypeTitle:
                    return item.ObjectType;
                case SquareTitle:
                    return item.Square;
                case NameTitle:
                    return item.ResultName;
                case PurposeTitle:
                    return item.ResultPurpose;
                case PermittedUseTitle:
                    return item.PermittedUse;
                case AddressTitle:
                    return item.Address;
                case LocationTitle:
                    return item.Location;
                case CadastralQuartalTitle:
                    return item.CadastralQuartal;
                case ParentCadastralNumberTitle:
                    return item.ParentCadastralNumberForOks;
                case BuildYearTitle:
                    return item.BuildYear;
                case CommissioningYearTitle:
                    return item.CommissioningYear;
                case FloorsNumberTitle:
                    return item.FloorsNumber;
                case UnderGroundFloorsNumberTitle:
                    return item.UndergroundFloorsNumber;
                case WallMaterialTitle:
                    return item.WallMaterial;
                case GroupOrSubgroupTitle:
                    return item.SubGroupNumber;
                case CadastralCostTitle:
                    return item.CadastralCost;
            }

            return null;
        }


        #region Support Methods

        private List<OMTour> GetToursByTasks(List<long> taskIds)
        {
	        return OMTask.Where(x => taskIds.Contains(x.Id))
		        .Select(x => new
		        {
			        x.ParentTour.Id,
			        x.ParentTour.Year
                })
		        .Execute()
                .Select(x => x.ParentTour)
                .DistinctBy(x => x.Id)
                .ToList();
        }

        private Dictionary<long, TourAttributesFromSettings> GetToursAttributes(List<OMTour> tours)
        {
	        var tourIds = tours.Select(x => x.Id).ToList();
            var tourAttributes = new Dictionary<long, TourAttributesFromSettings>();
            tourIds.ForEach(tourId =>
            {
                var objectTypeAttribute = StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId);
                var cadastralQuartalAttribute = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
                var groupAttribute = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);
                var item = new TourAttributesFromSettings
                {
                    ObjectType = objectTypeAttribute,
                    CadastralQuartal = cadastralQuartalAttribute,
                    Group = groupAttribute
                };

                if (objectTypeAttribute != null)
                {
                    item.RegisterIds.Add(objectTypeAttribute.RegisterId);
                    item.AttributeIds.Add(objectTypeAttribute.Id);
                }
                if (cadastralQuartalAttribute != null)
                {
                    item.RegisterIds.Add(cadastralQuartalAttribute.RegisterId);
                    item.AttributeIds.Add(cadastralQuartalAttribute.Id);
                }
                if (groupAttribute != null)
                {
                    item.RegisterIds.Add(groupAttribute.RegisterId);
                    item.AttributeIds.Add(groupAttribute.Id);
                }

                tourAttributes[tourId] = item;
            });

            return tourAttributes;
        }

        private List<GbuObjectAttribute> GetGbuAttributes(List<long> objectIds, Dictionary<long, TourAttributesFromSettings> tourAttributes,
            Dictionary<string, RegisterAttribute> rosreestrAttributes)
        {
            var allRegisterIds = tourAttributes.Values.SelectMany(x => x.RegisterIds).ToList();
            var allAttributeIds = tourAttributes.Values.SelectMany(x => x.AttributeIds).ToList();

            allRegisterIds.AddRange(rosreestrAttributes.Values.Select(x => (long)x.RegisterId));
            allAttributeIds.AddRange(rosreestrAttributes.Values.Select(x => x.Id));

            return GbuObjectService.GetAllAttributes(
                objectIds,
                allRegisterIds.Distinct().ToList(),
                allAttributeIds.Distinct().ToList(),
                DateTime.Now.GetEndOfTheDay(),
                isLight: true);
        }

        private static void SetRosreestrAttributes(List<GbuObjectAttribute> objectAttributes, Dictionary<string, RegisterAttribute> rosreestrAttributes,
            PreviousTourReportItem item)
        {
            foreach (var objectAttribute in objectAttributes)
            {
                var attributeKeys = rosreestrAttributes.Where(x => x.Value.Id == objectAttribute.AttributeId)
                    .Select(x => x.Key);
                foreach (var key in attributeKeys)
                {
                    item.GetType().GetProperty(key)?.SetValue(item, objectAttribute.GetValueInString());
                }
            }
        }

        private static void SetAttributesFromTourSettings(List<GbuObjectAttribute> objectAttributes, long? tourId,
            Dictionary<long, TourAttributesFromSettings> tourAttributesFromSettings, PreviousTourReportItem item)
        {
            if (tourId == null || !tourAttributesFromSettings.TryGetValue(tourId.Value, out var tourAttributes))
                return;

            item.ObjectType = objectAttributes.FirstOrDefault(x => x.AttributeId == tourAttributes.ObjectType?.Id)?.GetValueInString();
            item.CadastralQuartal = objectAttributes.FirstOrDefault(x => x.AttributeId == tourAttributes.CadastralQuartal?.Id)?.GetValueInString();
            item.SubGroupNumber = objectAttributes.FirstOrDefault(x => x.AttributeId == tourAttributes.Group?.Id)?.GetValueInString();
        }

        private void SetAttributesAccordingToObjectType(PropertyTypes objectType, PreviousTourReportItem item)
        {
            string purpose = null, name = null, parentCadastralNumberForOks = item.ParentCadastralNumberForOks;
            switch (objectType)
            {
                case PropertyTypes.Building:
                    purpose = item.BuildingPurpose;
                    name = item.OksName;
                    break;
                case PropertyTypes.Pllacement:
                    purpose = item.PlacementPurpose;
                    name = item.OksName;
                    break;
                case PropertyTypes.Construction:
                    purpose = item.ConstructionPurpose;
                    name = item.OksName;
                    break;
                case PropertyTypes.Stead:
                    name = item.ZuName;
                    parentCadastralNumberForOks = null;
                    break;
            }

            item.ResultPurpose = purpose;
            item.ResultName = name;
            item.ParentCadastralNumberForOks = parentCadastralNumberForOks;
        }

        private Dictionary<string, RegisterAttribute> GetRosreestrAttributes()
        {
            var attributesDictionary = new Dictionary<string, RegisterAttribute>();

            attributesDictionary.Add(nameof(PreviousTourReportItem.OksName), RosreestrRegisterService.GetObjectNameAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.ZuName), RosreestrRegisterService.GetParcelNameAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.BuildingPurpose), RosreestrRegisterService.GetBuildingPurposeAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.PlacementPurpose), RosreestrRegisterService.GetPlacementPurposeAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.ConstructionPurpose), RosreestrRegisterService.GetConstructionPurposeAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.PermittedUse), RosreestrRegisterService.GetTypeOfUseByDocumentsAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.Address), RosreestrRegisterService.GetAddressAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.Location), RosreestrRegisterService.GetLocationAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.ParentCadastralNumberForOks), RosreestrRegisterService.GetParcelAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.BuildYear), RosreestrRegisterService.GetBuildYearAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.CommissioningYear), RosreestrRegisterService.GetCommissioningYearAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.FloorsNumber), RosreestrRegisterService.GetFloorsNumberAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.UndergroundFloorsNumber), RosreestrRegisterService.GetUndergroundFloorsNumberAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.WallMaterial), RosreestrRegisterService.GetWallMaterialAttribute());

            return attributesDictionary;
        }

        protected List<OMUnit> GetUnits(List<long> taskIds, long groupId)
        {
	        return OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && x.ObjectId != null && x.GroupId == groupId)
	            .Select(x => new
	            {
                    x.ObjectId,
                    x.TourId,
                    x.CadastralNumber,
                    x.Square,
                    x.CadastralCost,
                    x.PropertyType_Code
                }).Execute();
        }

        #endregion

        #region Entities

        private class TourAttributesFromSettings
        {
            public RegisterAttribute ObjectType { get; set; }
            public RegisterAttribute CadastralQuartal { get; set; }
            public RegisterAttribute Group { get; set; }
            public List<long> RegisterIds { get; set; }
            public List<long> AttributeIds { get; set; }

            public TourAttributesFromSettings()
            {
                RegisterIds = new List<long>();
                AttributeIds = new List<long>();
            }
        }

        #endregion
    }
}
