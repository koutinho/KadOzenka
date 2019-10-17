using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ObjectModel.Market;
using ObjectModel.Directory;

namespace OuterMarketParser.Model.DatabaseOperations
{
    class DataToPostgreSQL : IDataToPostgreSQL
    {
        public DataToPostgreSQL(List<PropertyObject> coreObjects)
        {
            coreObjects.ForEach(x => x.Url = GetURL(MarketTypes.Cian, GetDealType(x.Deal_type), x.Category_Id, x.Subcategory, x.Id));
            Console.WriteLine(coreObjects.Count + " " + coreObjects.GroupBy(x => x.Url).Select(x => x.First()).ToList().Count);
            coreObjects.GroupBy(x => x.Url).Select(x => x.First()).ToList().ForEach(element => SaveObject(element));
        }

        public void SaveObject(PropertyObject element)
        {
            if (((element.Price > 26000 && GetDealType(element.Deal_type) == DealType.RentSuggestion) || GetDealType(element.Deal_type) == DealType.SaleSuggestion) && 
                  OMCoreObject.Where(x => x.Url == element.Url).Execute().Count == 0)
            {
                OMCoreObject obj = new OMCoreObject
                {
                    Url = element.Url,
                    Market_Code = MarketTypes.Cian,
                    PropertyType_Code = element.propertyType,
                    MarketId = element.Id,
                    Price = element.Price,
                    ParserTime = element.Time,
                    Region = element.Region,
                    City = element.City,
                    Address = element.Address,
                    Metro = element.Metro,
                    Images = element.Images,
                    Description = element.Description,
                    Lat = element.Lat,
                    Lng = element.Lng,
                    DealType_Code = GetDealType(element.Deal_type),
                    RoomsCount = element.Rooms_count,
                    FloorNumber = element.Floor_number,
                    FloorsCount = element.Floors_count,
                    Area = element.Area,
                    AreaKitchen = element.Area_kitchen,
                    AreaLiving = element.Area_living,
                    AreaLand = element.Area_land,
                    BuildingYear = element.Building_year,
                    //DealType = element.Deal_type,
                    Category = element.Category,
                    Subcategory = element.Subcategory,
                    CategoryId = element.Category_Id,
                    ProcessType_Code = ProcessStep.DoNotProcessed
                };
                if (GetExclusionStatus(element.Price, element.Area) != null) obj.ExclusionStatus_Code = (ExclusionStatus)GetExclusionStatus(obj.Price, obj.Area);
                obj.Save();
            }
        }

        private DealType GetDealType(string initialData)
        {
            switch (initialData)
            {
                case "продам":
                    return DealType.SaleSuggestion;
                case "сдам":
                    return DealType.RentSuggestion;
                default:
                    return DealType.None;
            }
        }

        private string GetURL(MarketTypes marketType, DealType dealType, long? categoryId, string subcategory, long id)
        {
            string result = null;
            switch (marketType)
            {
                case MarketTypes.Cian:
                    string dt = "sale", cat = "flat";
                    switch (dealType)
                    {
                        case DealType.RentSuggestion:
                            dt = "rent";
                            break;
                        case DealType.SaleSuggestion:
                            dt = "sale";
                            break;
                    }
                    switch (categoryId)
                    {
                        case 3:
                            cat = string.IsNullOrEmpty(subcategory) ? "flat" : "commercial";
                            break;
                        case 4:
                            cat = string.IsNullOrEmpty(subcategory) ? "flat" : "suburban";
                            break;
                    }
                    result = string.Format("https://www.cian.ru/{0}/{1}/{2}", dt, cat, id.ToString());
                    break;
            }
            return result;
        }

        private ExclusionStatus? GetExclusionStatus(long? price, decimal? area) 
        {
            if (price == 0 || price == null) return ExclusionStatus.NoPrice;
            if (area == 0 || area == null) return ExclusionStatus.NoArea;
            return null;
        }

    }
}
