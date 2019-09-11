using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using ObjectModel.Directory;

namespace OuterMarketParser.Model
{
    class RestAPICianPropertyObject
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public long? Price { get; set; }
        public string Time { get; set; }
        public string Time_publish { get; set; }
        public string Phone { get; set; }
        public string Person_type { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Metro { get; set; }
        public int? Rooms_count { get; set; }
        public int? Floor_number { get; set; }
        public int? Floors_count { get; set; }
        public string Area { get; set; }
        public string Area_kitchen { get; set; }
        public string Area_living { get; set; }
        public string Area_land { get; set; }
        public int? Building_year { get; set; }
        public string Deal_type { get; set; }
        public string Images { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public int? Category_Id { get; set; }
        public int? Region_Id { get; set; }
        public int? City_Id { get; set; }
        public Coordinates Coords { get; set; }
        public override string ToString() => $"Id: {Id}\nUrl: {Url}\nPrice: {Price}\nTime: {Time}\nTime_publish: {Time_publish}\nPhone: {Phone}\nPerson_type: {Person_type}" +
                                             $"\nRegion: {Region}\nCity: {City}\nAdress: {Address}\nMetro: {Metro}\nRooms_count: {Rooms_count}\nFloor_number: {Floor_number}" +
                                             $"\nFloors_count: {Floors_count}\nArea: {Area}\nArea_kitchen: {Area_kitchen}\nArea_living: {Area_living}\nArea_land: {Area_land}" +
                                             $"\nBuilding_year: {Building_year}\nDeal_type: {Deal_type}\nImages: {Images}\nDescription: {Description}\nCategory: {Category}" +
                                             $"\nSubcategory: {Subcategory}\nCategory_Id: {Category_Id}\nRegion_Id: {Region_Id}\nCity_Id: {City_Id}\nCoords:\n{Coords}";

        public new PropertyTypes GetType()
        {
            switch (Category_Id)
            {
                case 1:
                case 2:
                    return PropertyTypes.Pllacement;
                case 3:
                    switch (Subcategory)
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
                            Console.WriteLine(Subcategory);
                            return PropertyTypes.Other;
                    }
                case 4:
                    switch (Subcategory)
                    {
                        case "Дом":
                            return PropertyTypes.Building;
                        case "Участок":
                            return PropertyTypes.Stead;
                        case "Таунхаус":
                            return PropertyTypes.Building;
                        default:
                            Console.WriteLine(Subcategory);
                            return PropertyTypes.Other;
                    }
                default:
                    return PropertyTypes.Other;
            }
        }

        public double GetArea()
        {
            switch (Category_Id)
            {
                case 4:
                    switch (Subcategory)
                    {
                        case "Участок":
                            return double.Parse(Area_land, CultureInfo.InvariantCulture) * 100;
                        default:
                            return double.Parse(Area, CultureInfo.InvariantCulture);
                    }
                default:
                    return double.Parse(Area, CultureInfo.InvariantCulture);
            }
        }
        public int GetUid() => Int32.Parse($"1{Id}");
        public string GetName() => $"1_{Id}";
        public int GetTarget() => Category_Id == 3 ? 1 : 2;
        public DateTime GetTimePublished() => DateTime.ParseExact(Time_publish, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }
}
