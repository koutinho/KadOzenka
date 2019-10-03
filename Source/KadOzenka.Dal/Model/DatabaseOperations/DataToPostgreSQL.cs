using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ObjectModel.Market;

namespace OuterMarketParser.Model.DatabaseOperations
{
    class DataToPostgreSQL : IDataToPostgreSQL
    {
        public DataToPostgreSQL(List<PropertyObject> coreObjects) => coreObjects.GroupBy(x => x.Url).Select(x => x.First()).ToList().ForEach(element => SaveObject(element));

        public void SaveObject(PropertyObject element)
        {
            if (OMCoreObject.Where(x => x.Url == element.Url).Execute().Count == 0)
            {
                OMCoreObject obj = new OMCoreObject
                {
                    Url = element.Url,
                    Market_Code = ObjectModel.Directory.MarketTypes.Cian,
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
                    DealType_Code = ObjectModel.Directory.DealTypes.Sale,
                    DealView_Code = ObjectModel.Directory.DealViews.Offer,
                    RoomsCount = element.Rooms_count,
                    FloorNumber = element.Floor_number,
                    FloorsCount = element.Floors_count,
                    Area = element.Area,
                    AreaKitchen = element.Area_kitchen,
                    AreaLiving = element.Area_living,
                    AreaLand = element.Area_land,
                    BuildingYear = element.Building_year,
                    DealType = element.Deal_type,
                    Category = element.Category,
                    Subcategory = element.Subcategory,
                    CategoryId = element.Category_Id,
                    RegionId = element.Region_Id,
                    CityId = element.City_Id
                };
                obj.Save();
            }
        }
    }
}
