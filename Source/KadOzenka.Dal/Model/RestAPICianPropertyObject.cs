using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using ObjectModel.Directory;
using OuterMarketParser.Exceptions; 

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

    }
}
