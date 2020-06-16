using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class PreviousToursReport : StatisticalDataReport
    {
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

        protected override string TemplateName(NameValueCollection query)
        {
            return "PricingFactorsCompositionForPreviousToursReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var tourId = GetTourId(query);
            var tour = OMTour.Where(x => x.Id == tourId).SelectAll().ExecuteFirstOrDefault();
            if(tour == null)
                throw new Exception($"Не найден тур с Id='{tourId}'");

            var previousTours = OMTour.Where(x => x.Year <= tour.Year).OrderBy(x => x.Year).SelectAll().Execute();

            var operations = GetOperations(previousTours);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);

            return HadleData(dataSet);
        }


        #region Support Methods

        private List<ReportItem> GetOperations(List<OMTour> tours)
        {
            var units = GetUnitsByTour(tours.Select(x => x.Id).ToList());
            ////для тестирования
            //var units = new List<OMUnit>
            //{
            //    new OMUnit{CadastralNumber = "KN_1", Id = 12435691, ObjectId = 11188991, TourId = 2016 },
            //    new OMUnit{CadastralNumber = "KN_2", Id = 12402753, ObjectId = 11251387, TourId = 2018 }
            //};
            if (units == null || units.Count == 0)
                return new List<ReportItem>();

            var tourAttributes = GetTourAttributes(tours.Select(x => x.Id).Distinct().ToList());
            var rosreestrAttributes = GetRosreestrAttributes();
            var objectIds = units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList();
            var gbuAttributes = GetGbuAttributes(objectIds, tourAttributes, rosreestrAttributes);

            var result = new List<ReportItem>();
            units.ToList().ForEach(unit =>
            {
                var tour = tours.FirstOrDefault(x => x.Id == unit.TourId);
                var item = new ReportItem
                {
                    Tour = tour,
                    CadastralNumber = unit.CadastralNumber,
                    Square = unit.Square,
                    CadastralCost = unit.CadastralCost
                };

                var objectAttributes = gbuAttributes.Where(x => x.ObjectId == unit.ObjectId).ToList();
                SetRosreestrAttributes(objectAttributes, rosreestrAttributes, item);
                SetAttributesFromTourSettings(objectAttributes, tour?.Id, tourAttributes, item);
                SetAttributesAccordingToObjectType(unit.PropertyType_Code, item);

                result.Add(item);
            });

            return result;
        }

        private List<OMUnit> GetUnitsByTour(List<long> tourIds)
        {
            if (tourIds == null || tourIds.Count == 0)
                return new List<OMUnit>();

            return OMUnit.Where(x => tourIds.Contains((long)x.TourId)).SelectAll().Execute();
        }

        private Dictionary<long, TourAttributesFromSettings> GetTourAttributes(List<long> tourIds)
        {
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
                DateTime.Now.GetEndOfTheDay());
        }

        private static void SetRosreestrAttributes(List<GbuObjectAttribute> objectAttributes, Dictionary<string, RegisterAttribute> rosreestrAttributes, 
            ReportItem item)
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
            Dictionary<long, TourAttributesFromSettings> tourAttributesFromSettings, ReportItem item)
        {
            if (tourId == null || !tourAttributesFromSettings.TryGetValue(tourId.Value, out var tourAttributes))
                return;

            item.ObjectType = objectAttributes.FirstOrDefault(x => x.AttributeId == tourAttributes.ObjectType?.Id)?.GetValueInString();
            item.CadastralQuartal = objectAttributes.FirstOrDefault(x => x.AttributeId == tourAttributes.CadastralQuartal?.Id)?.GetValueInString();
            item.SubGroupNumber = objectAttributes.FirstOrDefault(x => x.AttributeId == tourAttributes.Group?.Id)?.GetValueInString();
        }

        private void SetAttributesAccordingToObjectType(PropertyTypes objectType, ReportItem item)
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

            attributesDictionary.Add(nameof(ReportItem.OksName), StatisticalDataService.GetRosreestrObjectNameAttribute());
            attributesDictionary.Add(nameof(ReportItem.ZuName), StatisticalDataService.GetRosreestrParcelNameAttribute());
            attributesDictionary.Add(nameof(ReportItem.BuildingPurpose), StatisticalDataService.GetRosreestrBuildingPurposeAttribute());
            attributesDictionary.Add(nameof(ReportItem.PlacementPurpose), StatisticalDataService.GetRosreestrPlacementPurposeAttribute());
            attributesDictionary.Add(nameof(ReportItem.ConstructionPurpose), StatisticalDataService.GetRosreestrConstructionPurposeAttribute());
            attributesDictionary.Add(nameof(ReportItem.PermittedUse), StatisticalDataService.GetRosreestrTypeOfUseByDocumentsAttribute());
            attributesDictionary.Add(nameof(ReportItem.Address), StatisticalDataService.GetRosreestrAddressAttribute());
            attributesDictionary.Add(nameof(ReportItem.Location), StatisticalDataService.GetRosreestrLocationAttribute());
            attributesDictionary.Add(nameof(ReportItem.ParentCadastralNumberForOks), StatisticalDataService.GetRosreestrParcelAttribute());
            attributesDictionary.Add(nameof(ReportItem.BuildYear), StatisticalDataService.GetRosreestrBuildYearAttribute());
            attributesDictionary.Add(nameof(ReportItem.CommissioningYear), StatisticalDataService.GetRosreestrCommissioningYearAttribute());
            attributesDictionary.Add(nameof(ReportItem.FloorsNumber), StatisticalDataService.GetRosreestrFloorsNumberAttribute());
            attributesDictionary.Add(nameof(ReportItem.UndergroundFloorsNumber), StatisticalDataService.GetRosreestrUndergroundFloorsNumberAttribute());
            attributesDictionary.Add(nameof(ReportItem.WallMaterial), StatisticalDataService.GetRosreestrWallMaterialAttribute());

            return attributesDictionary;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("Data");

            dataTable.Columns.Add("Number");

            dataTable.Columns.Add("CadastralNumber");

            dataTable.Columns.Add("ColumnTitle");
            dataTable.Columns.Add("TourYear");
            dataTable.Columns.Add("ValueForTour");

            ////для тестирования
            //operations = new List<ReportItem>
            //{
            //    new ReportItem{CadastralNumber = "KN_1", Tour = new OMTour{Id = 1, Year = 2016}, Square = 16, ObjectType = "type_16"},
            //    new ReportItem{CadastralNumber = "KN_1", Tour = new OMTour{Id = 2, Year = 2018}, Square = 18, ObjectType = "type_18"},
            //    new ReportItem{CadastralNumber = "KN_3", Tour = new OMTour{Id = 3, Year = 2020}, Square = 20, ObjectType = "type_16"}
            //};

            var groupedByCadastralNumberItems = operations
                .OrderBy(x => x.CadastralNumber)
                .GroupBy(x => new
                {
                    x.CadastralNumber
                });
            var counter = 0;
            foreach (var items in groupedByCadastralNumberItems)
            {
                counter++;
                _columnTitles.ForEach(title =>
                {
                    items.ToList().ForEach(item =>
                    {
                        var value = GetValueForReportItem(title, item);

                        dataTable.Rows.Add(counter,
                            items.Key.CadastralNumber,
                            title,
                            item.Tour?.Year,
                            value);
                    });
                });

            }
            
            return dataTable;
        }

        private object GetValueForReportItem(string title, ReportItem item)
        {
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
                //TODO factors
            }

            return null;
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

        //private class Attribute
        //{
        //    public long Id { get; set; }
        //    public string Name { get; set; }
        //    public string Value { get; set; }
        //}

        private class ReportItem : InfoFromTourSettings
        {
            public OMTour Tour { get; set; }

            //From Unit
            public string CadastralNumber { get; set; }
            public decimal? Square { get; set; }
            public decimal? CadastralCost { get; set; }

            //From Rosreestr
            public string OksName { get; set; }
            public string ZuName { get; set; }
            public string ResultName { get; set; }
            public string BuildingPurpose { get; set; }
            public string PlacementPurpose { get; set; }
            public string ConstructionPurpose { get; set; }
            public string ResultPurpose { get; set; }
            public string PermittedUse { get; set; }
            public string Address { get; set; }
            public string Location { get; set; }
            public string ParentCadastralNumberForOks { get; set; }
            public string BuildYear { get; set; }
            public string CommissioningYear { get; set; }
            public string FloorsNumber { get; set; }
            public string UndergroundFloorsNumber { get; set; }
            public string WallMaterial { get; set; }

            //From Tour Settings

            ////Factors from Modeling
            //public List<Attribute> Factors { get; set; }
        }

        #endregion
    }
}
