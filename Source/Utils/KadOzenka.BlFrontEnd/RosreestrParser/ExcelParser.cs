using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using IronXL;
using ObjectModel.Market;
using ObjectModel.Directory;
using Core.Shared.Extensions;

namespace KadOzenka.BlFrontEnd.RosreestrParser
{

    class ExcelParser
    {

        public void LoadRosreestrDeals()
        {
            string log = String.Empty;
            WorkBook workbook = WorkBook.Load(@"C:\Users\silanov\Desktop\Аренда.xlsx");
            WorkSheet sheet = workbook.WorkSheets.First();
            List<OMCoreObject> objects = new List<OMCoreObject>();
            int counter = 0;
            foreach (RangeRow row in sheet.Rows)
            {
                counter++;
                try
                {
                    /*
                     * Продажа
                     */
                    //long? roomsCount = row.ElementAt(18).Equals("-") ? null : row.ElementAt(18).ParseToLongNullable();
                    //var analogObject = new OMCoreObject
                    //{
                    //    CadastralNumber = row.ElementAt(0).ToString(),
                    //    BuildingCadastralNumber = row.ElementAt(1).ToString() != "0" ? row.ElementAt(1).ToString() : String.Empty,
                    //    CadastralQuartal = row.ElementAt(2).ToString() != "0" ? row.ElementAt(2).ToString() : String.Empty,
                    //    //Subgroup_Code = row.ElementAt(3),
                    //    //Group_Code = row.ElementAt(4),
                    //    PropertyMarketSegment_Code = GetMarketSegment(row.ElementAt(5).ToString()),
                    //    Address = row.ElementAt(6).ToString(),
                    //    Metro = row.ElementAt(7).ToString(),
                    //    SubwaySpace = row.ElementAt(8).ParseToDecimalNullable(),
                    //    Market_Code = MarketTypes.Rosreestr, //9 - источник информации (Всегда росреестр)
                    //    //10 - телефонный номер
                    //    ParserTime = DateTime.ParseExact(row.ElementAt(11).ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    //    //12 - тип объяевления
                    //    PropertyType_Code = LoadRosreestrDealsGetPropertyType(row.ElementAt(13).ToString()),
                    //    //14 - Вид использования (функциональное назначение)
                    //    //15 - Текущее использование
                    //    //16 - Тип объекта недвижимости
                    //    //17 - Вид права на ОКС
                    //    RoomsCount = roomsCount,
                    //    DealType_Code = GetDealType(row.ElementAt(19).ToString(), row.ElementAt(20).ToString()),
                    //    Area = row.ElementAt(21).ParseToDecimalNullable(),
                    //    Price = (long?)row.ElementAt(22).ParseToDecimalNullable(),
                    //    //23 - Удельная цена сделки/предложения, руб./кв.м (PricePerMeter)
                    //    QualityClass_Code = GetQualityClass(row.ElementAt(24).ToString()),
                    //    Zone = row.ElementAt(25).ParseToLongNullable(),
                    //    BuildingYear = row.ElementAt(26).ParseToLongNullable(),
                    //    FloorsCount = row.ElementAt(27).ParseToLongNullable(),
                    //    //28 - Материал стен (итоговый)
                    //    WallMaterial_Code = GetWallMaterial(row.ElementAt(29).ToString())
                    //};
                    /*
                     * Аренда
                     */
                    long? roomsCount = row.ElementAt(19).Equals("-") ? null : row.ElementAt(18).ParseToLongNullable();
                    var analogObject = new OMCoreObject
                    {
                        //0 - ID Объекта недвижимости 
                        CadastralNumber = row.ElementAt(1).ToString(),
                        BuildingCadastralNumber = row.ElementAt(2).ToString() != "0" ? row.ElementAt(2).ToString() : String.Empty,
                        CadastralQuartal = row.ElementAt(3).ToString() != "0" ? row.ElementAt(3).ToString() : String.Empty,
                        //4 - Группа 2018
                        //Group_Code = row.ElementAt(5),
                        PropertyMarketSegment_Code = GetMarketSegment(row.ElementAt(6).ToString()),
                        Address = row.ElementAt(7).ToString(),
                        Metro = row.ElementAt(8).ToString(),
                        SubwaySpace = row.ElementAt(9).ParseToDecimalNullable(),
                        Market_Code = MarketTypes.Rosreestr, //10 - источник информации (Всегда росреестр)
                        //11 - телефонный номер
                        //ParserTime = DateTime.ParseExact(row.ElementAt(12).ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        ParserTime = row.ElementAt(12).ParseToDateTime(),
                        //13 - текст объявления
                        PropertyType_Code = LoadRosreestrDealsGetPropertyType(row.ElementAt(14).ToString()),
                        //15 - Вид объекта недвижимости
                        //16 - Вид использования (функциональное назначение)
                        //17 - Текущее использование
                        //18 - Вид права на ОКС
                        RoomsCount = roomsCount,
                        DealType_Code = GetDealType(row.ElementAt(20).ToString(), row.ElementAt(21).ToString()),
                        Area = row.ElementAt(22).ParseToDecimalNullable(),
                        //23 - Условия опр-я цены
                        //24 - Аренда всего, руб.
                        //25 - Аренда за 1 кв. м, руб. 
                        Price = row.ElementAt(26).ToString() == "год" ? (long?)(row.ElementAt(24).ParseToDecimalNullable() / 12) : (long?)row.ElementAt(24).ParseToDecimalNullable(),
                        //27 - Итоговая годовая арендная ставка, руб./кв. м/год
                        QualityClass_Code = GetQualityClass(row.ElementAt(28).ToString()),
                        BuildingYear = row.ElementAt(29).ParseToLongNullable(),
                        FloorsCount = row.ElementAt(30).ParseToLongNullable(),
                        //31 - Материал стен (итоговый)
                        WallMaterial_Code = GetWallMaterial(row.ElementAt(32).ToString())
                    };
                    objects.Add(analogObject);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.StackTrace);
                    //Console.WriteLine(ex.Message);
                    log += $"{row.ElementAt(0)};false;{ex.Message}\n";
                }
                Console.Write($"Excel process: {counter};\r");
            }

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 5
            };
            int iterator = 0;
            Parallel.ForEach(objects, options, x =>
            {
                try
                {
                    /*
                     * Продажа
                     */
                    //OMCoreObject existsObject = OMCoreObject.Where(y => y.CadastralNumber == x.CadastralNumber && y.DealType_Code == DealType.SaleDeal).ExecuteFirstOrDefault();
                    /*
                     * Аренда
                     */
                    OMCoreObject existsObject = OMCoreObject.Where(y => y.CadastralNumber == x.CadastralNumber && 
                                                                        y.DealType_Code == DealType.RentDeal && 
                                                                        y.ParserTime <= x.ParserTime).ExecuteFirstOrDefault();
                    if (existsObject != null) x.Id = existsObject.Id;
                    x.ProcessType_Code = ProcessStep.DoNotProcessed;
                    x.Save();
                    log += $"{x.CadastralNumber};true;\n";
                }
                catch (Exception ex)
                {
                    log += $"{x.CadastralNumber};false;{ex.Message}\n";
                }
                iterator++;
                Console.Write($"Postgress process: {iterator}\r");
            });
            File.WriteAllText(@"C:\Users\silanov\Desktop\log.cvs", log);
        }

        private MarketSegment GetMarketSegment(string value)
        {
            switch (value)
            {
                case "Апартаменты": return MarketSegment.Appartment;
                case "Гаражи": return MarketSegment.Parking;
                case "Гостиницы": return MarketSegment.Hotel;
                case "ИЖС": return MarketSegment.IZHS;
                case "Машиноместа": return MarketSegment.CarParking;
                case "МЖС": return MarketSegment.MZHS;
                case "Офисы": return MarketSegment.Office;
                case "Производство и склады": return MarketSegment.Factory;
                case "Садоводческое, огородническое и дачное использование": return MarketSegment.Garden;
                case "Санатории": return MarketSegment.Sanatorium;
                case "Торговля": return MarketSegment.Trading;
                default: return MarketSegment.None;
            }
        }

        private WallMaterial GetWallMaterial(string value)
        {
            switch (value)
            {
                case "1_Кирпичные": return WallMaterial.Brick;
                case "2_Монолитные": return WallMaterial.Monolit;
                case "3_Панельные и блочные": return WallMaterial.Panel;
                case "4_Иное": return WallMaterial.Other;
                default: return WallMaterial.None;
            }
        }

        private QualityClass GetQualityClass(string value)
        {
            switch (value)
            {
                case "-": return QualityClass.None;
                case "А": return QualityClass.A;
                case "В": return QualityClass.B;
                case "В+": return QualityClass.Bplus;
                default: return QualityClass.None;
            }
        }

        private PropertyTypes LoadRosreestrDealsGetPropertyType(string type)
        {
            type = type.ToLower();
            switch (type)
            {
                case "нежилое помещение":
                case "квартира":
                case "комната":
                case "помещение":
                    return PropertyTypes.Pllacement;
                case "нежилое здание":
                case "жилой дом":
                case "здание":
                    return PropertyTypes.Building;
                case "сооружение":
                    return PropertyTypes.Construction;
                case "машино-место":
                    return PropertyTypes.Parking;
                default:
                    throw new Exception($"Не известный тип объекта: {type}");
            }
        }

        private DealType GetDealType(string dealFact, string dealType)
        {
            switch (dealFact)
            {
                case "Сделка":
                    switch (dealType)
                    {
                        case "Сделка купли-продажи": return DealType.SaleDeal;
                        case "Сделка аренда": return DealType.RentDeal;
                        default: return DealType.SaleDeal;
                    }
                case "Предложение":
                    switch (dealType)
                    {
                        case "Предложение купли-продажи": return DealType.SaleSuggestion;
                        case "Предложение аренда": return DealType.RentSuggestion;
                        default: return DealType.SaleSuggestion;
                    }
                default: return DealType.SaleDeal;
            }
        }

    }

}