//using System;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//using IronXL;
//using ObjectModel.Market;
//using ObjectModel.Directory;
//using Core.Shared.Extensions;

//namespace KadOzenka.BlFrontEnd.RosreestrParser
//{

//    class ExcelParser
//    {

//        private MarketSegment GetMarketSegment(string value)
//        {
//            switch (value)
//            {
//                case "Апартаменты": return MarketSegment.Appartment;
//                case "Гаражи": return MarketSegment.Parking;
//                case "Гостиницы": return MarketSegment.Hotel;
//                case "ИЖС": return MarketSegment.IZHS;
//                case "Машиноместа": return MarketSegment.CarParking;
//                case "МЖС": return MarketSegment.MZHS;
//                case "Офисы": return MarketSegment.Office;
//                case "Производство и склады": return MarketSegment.Factory;
//                case "Садоводческое, огородническое и дачное использование": return MarketSegment.Garden;
//                case "Санатории": return MarketSegment.Sanatorium;
//                case "Торговля": return MarketSegment.Trading;
//                default: return MarketSegment.None;
//            }
//        }

//        private WallMaterial GetWallMaterial(string value)
//        {
//            switch (value)
//            {
//                case "1_Кирпичные": return WallMaterial.Brick;
//                case "2_Монолитные": return WallMaterial.Monolit;
//                case "3_Панельные и блочные": return WallMaterial.Panel;
//                case "4_Иное": return WallMaterial.Other;
//                default: return WallMaterial.None;
//            }
//        }

//        private QualityClass GetQualityClass(string value)
//        {
//            switch (value)
//            {
//                case "-": return QualityClass.None;
//                case "А": return QualityClass.A;
//                case "В": return QualityClass.B;
//                case "В+": return QualityClass.Bplus;
//                default: return QualityClass.None;
//            }
//        }

//        private PropertyTypes LoadRosreestrDealsGetPropertyType(string type)
//        {
//            type = type.ToLower();
//            switch (type)
//            {
//                case "нежилое помещение":
//                case "квартира":
//                case "комната":
//                case "помещение":
//                    return PropertyTypes.Pllacement;
//                case "нежилое здание":
//                case "жилой дом":
//                case "здание":
//                    return PropertyTypes.Building;
//                case "сооружение":
//                    return PropertyTypes.Construction;
//                case "машино-место":
//                    return PropertyTypes.Parking;
//                default:
//                    throw new Exception($"Не известный тип объекта: {type}");
//            }
//        }

//        private DealType GetDealType(string dealFact, string dealType)
//        {
//            switch (dealFact)
//            {
//                case "Сделка":
//                    switch (dealType)
//                    {
//                        case "Сделка купли-продажи": return DealType.SaleDeal;
//                        case "Сделка аренда": return DealType.RentDeal;
//                        default: return DealType.SaleDeal;
//                    }
//                case "Предложение":
//                    switch (dealType)
//                    {
//                        case "Предложение купли-продажи": return DealType.SaleSuggestion;
//                        case "Предложение аренда": return DealType.RentSuggestion;
//                        default: return DealType.SaleSuggestion;
//                    }
//                default: return DealType.SaleDeal;
//            }
//        }

//    }

//}