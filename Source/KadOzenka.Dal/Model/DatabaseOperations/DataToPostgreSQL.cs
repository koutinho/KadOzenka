using System;
using System.Collections.Generic;
using System.Text;

using ObjectModel.Market;

namespace OuterMarketParser.Model.DatabaseOperations
{
    class DataToPostgreSQL : IDataToPostgreSQL
    {
        public DataToPostgreSQL(List<PropertyObject> coreObjects) => coreObjects.ForEach(element => SaveObject(element));

        public void SaveObject(PropertyObject element)
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
                Lng = element.Lng
            };
            obj.Save();
            OMCianObject cianObj = new OMCianObject
            {
                Id = obj.Id,
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
            cianObj.Save();
        }
    }
}
