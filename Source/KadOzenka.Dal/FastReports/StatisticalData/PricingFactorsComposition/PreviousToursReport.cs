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
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    //TODO отчет работает очень медленно (большой объем данных + сложная матрица FastReport),
    //TODO поэтому для него дополнительно создан длительный процесс PreviousToursReportProcess
    public class PreviousToursReport : StatisticalDataReport
    {
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

        protected override string TemplateName(NameValueCollection query)
        {
            return "PricingFactorsCompositionForPreviousToursReport";
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialization, List<FilterValue> filterValues)
        {
            GroupFilter.InitializeFilterValues(StatisticalDataType.PricingFactorsCompositionForPreviousTours, initialization, filterValues);
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIds = GetTaskIdList(query)?.ToList();

            var groupId = GetGroupIdFromFilter(query);

            var operations = GetReportInfo(taskIds, groupId);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations.Items);
            var commonTable = GetCommonDataTable();
            dataSet.Tables.Add(itemTable);
            dataSet.Tables.Add(commonTable);

            return HadleData(dataSet);
        }


        #region Support Methods

        public PreviousToursReportInfo GetReportInfo(List<long> taskIds, long groupId)
        {
            var factorsByRegisters = GetPricingFactors(groupId);
            var attributes = factorsByRegisters.SelectMany(x => x.Attributes).ToList();

            var tours = GetToursByTasks(taskIds);
            var units = GetUnits(taskIds);
            ////TODO: для тестирования
            //var units = new List<OMUnit>
            //{
            //    new OMUnit {CadastralNumber = "KN_1", Id = 12435691, ObjectId = 11188991, TourId = 2016},
            //    new OMUnit {CadastralNumber = "KN_2", Id = 15731468, ObjectId = 11404578, TourId = 2018}
            //};
            //var tours = new List<OMTour> { new OMTour { Id = 2016, Year = 2016 }, new OMTour { Id = 2018, Year = 2018 } };
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
                    Tour = tour,
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
                Items = items.OrderBy(x => x.CadastralNumber).ThenBy(x => x.Tour?.Year).ToList(),
                Tours = tours.OrderBy(x => x.Year).ToList(),
                PricingFactors = attributes.OrderBy(x => x.Name).ToList()
            };
        }

        private List<OMTour> GetToursByTasks(List<long> taskIds)
        {
            return OMTask.Where(x => taskIds.Contains(x.Id))
                .Select(x => x.ParentTour.Id)
                .Select(x => x.ParentTour.Year)
                .SelectAll()
                .Execute()
                .Select(x => x.ParentTour)
                .DistinctBy(x => x.Id)
                .ToList();
        }

        private List<FactorsService.PricingFactors> GetPricingFactors(long groupId)
        {
            var model = OMModel.Where(x => x.GroupId == groupId).SelectAll().ExecuteFirstOrDefault();
            ////TODO: для тестирования
            //var model = OMModel.Where(x => x.Id == 7977478).SelectAll().ExecuteFirstOrDefault();
            return model == null
                ? new List<FactorsService.PricingFactors>()
                : FactorsService.GetGroupedModelFactors(model.Id);
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
                DateTime.Now.GetEndOfTheDay());
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

            attributesDictionary.Add(nameof(PreviousTourReportItem.OksName), StatisticalDataService.GetRosreestrObjectNameAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.ZuName), StatisticalDataService.GetRosreestrParcelNameAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.BuildingPurpose), StatisticalDataService.GetRosreestrBuildingPurposeAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.PlacementPurpose), StatisticalDataService.GetRosreestrPlacementPurposeAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.ConstructionPurpose), StatisticalDataService.GetRosreestrConstructionPurposeAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.PermittedUse), StatisticalDataService.GetRosreestrTypeOfUseByDocumentsAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.Address), StatisticalDataService.GetRosreestrAddressAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.Location), StatisticalDataService.GetRosreestrLocationAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.ParentCadastralNumberForOks), StatisticalDataService.GetRosreestrParcelAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.BuildYear), StatisticalDataService.GetRosreestrBuildYearAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.CommissioningYear), StatisticalDataService.GetRosreestrCommissioningYearAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.FloorsNumber), StatisticalDataService.GetRosreestrFloorsNumberAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.UndergroundFloorsNumber), StatisticalDataService.GetRosreestrUndergroundFloorsNumberAttribute());
            attributesDictionary.Add(nameof(PreviousTourReportItem.WallMaterial), StatisticalDataService.GetRosreestrWallMaterialAttribute());

            return attributesDictionary;
        }

        private DataTable GetCommonDataTable()
        {
            var dataTable = new DataTable("Common");

            dataTable.Columns.Add("Title");

            dataTable.Rows.Add(ReportTitle);

            return dataTable;
        }

        private DataTable GetItemDataTable(List<PreviousTourReportItem> operations)
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

            var counter = 0;
            foreach (var item in operations)
            {
                counter++;
                _columnTitles.ForEach(title =>
                {
                    var value = GetValueForReportItem(title, item);
                    dataTable.Rows.Add(counter,
                        item.CadastralNumber,
                        title,
                        item.Tour?.Year,
                        value);
                });

                foreach (var keyValuePair in item.Factors)
                {
                    dataTable.Rows.Add(counter,
                        item.CadastralNumber,
                        keyValuePair.Name,
                        item.Tour?.Year,
                        keyValuePair.Value);
                }
            }
            
            return dataTable;
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

        public class PreviousToursReportInfo
        {
            public string Title { get; set; }
            public List<string> ColumnTitles { get; set; }
            public List<PreviousTourReportItem> Items { get; set; }
            public List<OMTour> Tours { get; set; }
            public List<FactorsService.PricingFactor> PricingFactors { get; set; }

            public PreviousToursReportInfo()
            {
                Items = new List<PreviousTourReportItem>();
                Tours = new List<OMTour>();
                PricingFactors = new List<FactorsService.PricingFactor>();
            }
        }

        public class PreviousTourReportItem : InfoFromTourSettings
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

            //Factors from Model
            public List<FactorsService.PricingFactor> Factors { get; set; }
        }

        #endregion
    }
}
