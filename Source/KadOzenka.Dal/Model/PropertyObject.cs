using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using ObjectModel.Directory;

namespace OuterMarketParser.Model
{
    class PropertyObject
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public long? Price { get; set; }
        public DateTime? Time { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Metro { get; set; }
        public long? Rooms_count { get; set; }
        public long? Floor_number { get; set; }
        public long? Floors_count { get; set; }
        public decimal? Area { get; set; }
        public decimal? Area_kitchen { get; set; }
        public decimal? Area_living { get; set; }
        public decimal? Area_land { get; set; }
        public long? Building_year { get; set; }
        public string Deal_type { get; set; }
        public string Images { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public long? Category_Id { get; set; }
        public long? Region_Id { get; set; }
        public long? City_Id { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public PropertyTypes propertyType { get; set; }

        public PropertyObject(RestAPICianPropertyObject cianObject)
        {
            Id = cianObject.Id;
            Url = cianObject.Url;
            Price = cianObject.Price;
            Time = ToNullableDateTime(cianObject.Time_publish);
            Region = cianObject.Region;
            City = cianObject.City;
            Address = cianObject.Address;
            Metro = cianObject.Metro;
            Rooms_count = cianObject.Rooms_count;
            Floor_number = cianObject.Floor_number;
            Floors_count = cianObject.Floors_count;
            Area = ToNullableDecimal(cianObject.Area);
            Area_kitchen = ToNullableDecimal(cianObject.Area_kitchen);
            Area_living = ToNullableDecimal(cianObject.Area_living);
            Area_land = ToNullableDecimal(cianObject.Area_land);
            Building_year = cianObject.Building_year;
            Deal_type = cianObject.Deal_type;
            Images = cianObject.Images;
            Description = cianObject.Description;
            Category = cianObject.Category;
            Subcategory = cianObject.Subcategory;
            Category_Id = cianObject.Category_Id;
            Region_Id = cianObject.Region_Id;
            City_Id = cianObject.City_Id;
            Lat = ToNullableDecimal(cianObject.Coords.Lat);
            Lng = ToNullableDecimal(cianObject.Coords.Lng);
            propertyType = GetPropertyObjectType(Building_year, Category_Id, Subcategory);
        }

        private static decimal? ToNullableDecimal(string str) => string.IsNullOrEmpty(str) ? null : (decimal?)decimal.Parse(str, CultureInfo.InvariantCulture);
        private static decimal? ToNullableDecimal(double? value) => value.Equals(null) ? null : (decimal?)value;
        private static DateTime? ToNullableDateTime(string time) => string.IsNullOrEmpty(time) ? null : (DateTime?)DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        private PropertyTypes GetPropertyObjectType(long? buildingYear, long? categoryId, string subcategory)
        {
            if (buildingYear != null && buildingYear != 0 && buildingYear > DateTime.Now.Year) return PropertyTypes.UncompletedBuilding;
            else
            {
                switch (categoryId)
                {
                    case 1:
                    case 2:
                        return PropertyTypes.Pllacement;
                    case 3:
                        switch (subcategory)
                        {
                            case "Офисная":
                                return PropertyTypes.Pllacement;
                            case "Гараж":
                                return PropertyTypes.Parking;
                            case "Готовый бизнес":
                            case "Свободного назначения":
                            case "Торговая":
                            case "Производственная":
                                return PropertyTypes.Company;
                            case "Здание":
                                return PropertyTypes.Building;
                            default:
                                Console.WriteLine(subcategory);
                                return PropertyTypes.Other;
                        }
                    case 4:
                        switch (subcategory)
                        {
                            case "Дом":
                                return PropertyTypes.Building;
                            case "Участок":
                                return PropertyTypes.Stead;
                            case "Таунхаус":
                                return PropertyTypes.Building;
                            default:
                                Console.WriteLine(subcategory);
                                return PropertyTypes.Other;
                        }
                    default:
                        return PropertyTypes.Other;
                }
            }
        }

    }
}
