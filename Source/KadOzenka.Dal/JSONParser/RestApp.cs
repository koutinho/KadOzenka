using System;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

using ObjectModel.Market;
using ObjectModel.Directory;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace KadOzenka.Dal.JSONParser
{

    public class RestApp
    {

        public int GetRestData(string JSON) => 
            Int32.Parse(JObject.Parse(JSON)["data"]["api_limit"]["of"].ToString()) - Int32.Parse(JObject.Parse(JSON)["data"]["api_limit"]["used"].ToString());

        public List<OMCoreObject> ParseCoreObject(string JSON, ref int CCTR, ref int ECTR)
        {
            List<OMCoreObject> result = new List<OMCoreObject>();
            JArray elements = JArray.Parse(JObject.Parse(JSON)["data"].ToString());
            for (int i = 0; i < elements.Count; i++)
            {
                try
                {
                    string subCategory = elements[i]["subcategory"].ToString();
                    string category = elements[i]["category"].ToString();
                    string description = elements[i]["description"].ToString();
                    long marketId = long.Parse(elements[i]["Id"].ToString());
                    long? price = 1;
                    long? roomsCount = long.TryParse(elements[i]["rooms_count"].ToString(), out var tempRC) ? tempRC : (long?)null;
                    long? floorNumber = long.TryParse(elements[i]["floor_number"].ToString(), out var tempFN) ? tempFN : (long?)null;
                    long? floorsCount = long.TryParse(elements[i]["floors_count"].ToString(), out var tempFC) ? tempFC : (long?)null;
                    long? buildingYear = long.TryParse(elements[i]["building_year"].ToString(), out var tempBY) ? tempBY : (long?)null;
                    long? categoryId = long.TryParse(elements[i]["category_Id"].ToString(), out var tempCI) ? tempCI : (long?)null;
                    decimal? lng = decimal.TryParse(elements[i]["coords"]["lng"].ToString(), out var tempLNG) ? tempLNG : (decimal?)null;
                    decimal? lat = decimal.TryParse(elements[i]["coords"]["lat"].ToString(), out var tempLAT) ? tempLAT : (decimal?)null;
                    decimal? area = decimal.TryParse(elements[i]["area"].ToString().Replace('.', ','), out var tempArea) ? tempArea : (decimal?)null;
                    decimal? areaKitchen = decimal.TryParse(elements[i]["area_kitchen"].ToString().Replace('.', ','), out var tempAK) ? tempAK : (decimal?)null;
                    decimal? areaLiving = decimal.TryParse(elements[i]["area_living"].ToString().Replace('.', ','), out var tempAL) ? tempAL : (decimal?)null;
                    decimal? areaLand = decimal.TryParse(elements[i]["area_land"].ToString().Replace('.', ','), out var tempALA) ? tempALA : (decimal?)null;
                    DealType dealType = GetDealType(elements[i]["deal_type"].ToString());
                    ExclusionStatus? exclusionStatus = GetExclusionStatus(price, area, areaLand, description, category, subCategory, dealType);

                    OMCoreObject CO = new OMCoreObject();
                    CO.Url = GetURL(MarketTypes.Cian, dealType, categoryId, subCategory, marketId);
                    CO.Market_Code = MarketTypes.Cian;
                    CO.MarketId = marketId;
                    CO.Price = price;
                    CO.ParserTime = DateTime.ParseExact(elements[i]["time"].ToString(), "yyyy-MM-dd HH:mm:ss", null);
                    CO.Region = elements[i]["region"].ToString();
                    CO.City = elements[i]["city"].ToString();
                    CO.Address = elements[i]["address"].ToString();
                    CO.Metro = elements[i]["metro"].ToString();
                    CO.Images = elements[i]["images"].ToString();
                    CO.Description = description;
                    CO.Lng = lng;
                    CO.Lat = lat;
                    CO.DealType_Code = GetDealType(elements[i]["deal_type"].ToString());
                    CO.RoomsCount = roomsCount;
                    CO.FloorNumber = floorNumber;
                    CO.FloorsCount = floorsCount;
                    CO.Area = area;
                    CO.AreaKitchen = areaKitchen;
                    CO.AreaLiving = areaLiving;
                    CO.AreaLand = areaLand;
                    CO.BuildingYear = buildingYear;
                    CO.PropertyTypesCIPJS_Code = GetPropertyObjectTypeCIPJS(categoryId, subCategory);
                    CO.PropertyMarketSegment_Code = GetPropertyMarketSegment(categoryId, subCategory);
                    CO.ProcessType_Code = ProcessStep.DoNotProcessed;
                    if (exclusionStatus != null)
                    {
                        CO.ExclusionStatus_Code = exclusionStatus.GetValueOrDefault();
                        CO.ProcessType_Code = ProcessStep.Excluded;
                    }
                    result.Add(CO);
                    CCTR++;
                    //Console.WriteLine($"URL: {CO.Url}\nКод площадки: {CO.Market_Code}\nКод типа недвижимости: {CO.PropertyType_Code}\nId маркета: {CO.MarketId}\n" +
                    //                  $"Цена: {CO.Price}\nВремя парсинга: {CO.ParserTime}\nРегион: {CO.Region}\nГород: {CO.City}\nАдрес: {CO.Address}\n" +
                    //                  $"Метро: {CO.Metro}\nИзображения: {CO.Images}\nОписание: {CO.Description}\nДолгота: {CO.Lng}\nШирота: {CO.Lat}\n" +
                    //                  $"Тип сделки: {CO.DealType_Code}\nКоличество комнат: {CO.RoomsCount}\nЭтаж: {CO.FloorNumber}\nКоличество этажей: {CO.FloorsCount}\n" +
                    //                  $"Площадь: {CO.Area}\nПлощадь кухни: {CO.AreaKitchen}\nЖилая площадь: {CO.AreaLiving}\nПлощадь земли: {CO.AreaLand}\n" +
                    //                  $"Год постройки: {CO.BuildingYear}\nКатегория: {CO.Category}\nПодкатегория: {CO.Subcategory}\nИдентификатор категории: {CO.Subcategory}\n" +
                    //                  $"Код статуса обработки: {CO.CategoryId}\n");
                }
                catch (Exception) { ECTR++; }
            }
            return result;
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

        private PropertyTypesCIPJS GetPropertyObjectTypeCIPJS(long? categoryId, string subcategory)
        {
            switch (categoryId)
            {
                case 3: return (subcategory.Equals("Производственная") || subcategory.Equals("Здание")) ? PropertyTypesCIPJS.Buildings : PropertyTypesCIPJS.Placements;
                case 4: return subcategory.Equals("Участок") ? PropertyTypesCIPJS.LandArea : PropertyTypesCIPJS.Buildings;
                default: return PropertyTypesCIPJS.Placements;
            }
        }

        private MarketSegment GetPropertyMarketSegment(long? categoryId, string subcategory)
        {
            switch (categoryId)
            {
                case 1:
                case 2: return MarketSegment.MZHS;
                case 3:
                    switch (subcategory)
                    {
                        case "Офисная": return MarketSegment.Office;
                        case "Гараж": return MarketSegment.CarParking;
                        case "Торговая": return MarketSegment.Trading;
                        case "Производственная": return MarketSegment.Factory;
                        default: return MarketSegment.NoSegment;
                    }
                case 4: return MarketSegment.IZHS;
                default: return MarketSegment.NoSegment;
            }
        }

        private ExclusionStatus? GetExclusionStatus(long? price, decimal? area, decimal? area_land, string description, string category, string subCategory, DealType dealType)
        {
            if (price == 0 || price == null) return ExclusionStatus.NoPrice;
            if ((area == 0 || area == null) && (area_land == 0 || area_land == null)) return ExclusionStatus.NoArea;
            if (category == "Коммерческая недвижимость" && (subCategory == "Офисная" || subCategory == "Торговая") && dealType == DealType.SaleSuggestion)
            {
                if (Regex.IsMatch(description, @"(мебель[^(ный)])")) return ExclusionStatus.ContainsFurniture;
                if (Regex.IsMatch(description, @"([^А-Яа-я]ппа)|(прав аренды)|(права аренды)")) return ExclusionStatus.ContainsPPA;
            }
            if (description.Length < 100) return ExclusionStatus.DoNotHaveDescription;
            return null;
        }

    }

}