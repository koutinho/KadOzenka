using System;
using System.Collections.Generic;
using System.Text;

using ObjectModel.Market;

namespace DebugApplication.Model.DatabaseOperations
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
            OMCianObject cianObj = new OMCianObject
            {
                RoomsCount = element.Rooms_count
                //<=====Продолжить отсюда
            };
            obj.Save();
            cianObj.Save();
        }
    }
}
