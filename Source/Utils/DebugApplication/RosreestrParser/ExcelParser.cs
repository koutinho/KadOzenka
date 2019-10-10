using Core.Shared.Extensions;
using IronXL;
using ObjectModel.Directory;
using ObjectModel.Market;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DebugApplication.RosreestrParser
{
    class ExcelParser
    {
        public void LoadRosreestrDeals()
        {
            string log = String.Empty;
            WorkBook workbook = WorkBook.Load(@"C:\Users\silanov\Desktop\Сделки.xlsx");
            //0   КН
            //1   КН Здания
            //2   КК
            //3   Подгруппа
            //4   Группа
            //5   Сегмент рынка
            //6   Адресный ориентир
            //7   Наименование Метро
            //8   Расстояние до ближайшей станции метро
            //9   Источник информации
            //10  Номер телефона
            //11  Дата предложения(сделки)
            //12  Текст объявления
            //13  Вид объекта недвижимости
            //14  Вид использования(функциональное назначение)
            //15  Текущее использование
            //16  Тип объекта недвижимости
            //17  Вид права на ОКС
            //18  Количество комнат
            //19  Факт сделки(сделка, предложение)
            //20  Тип сделки
            //21  Площадь ОКС, кв.м
            //22  Цена сделки/ предложения, руб.
            //23  Удельная цена сделки / предложения, руб./ кв.м
            //24  Класс качества
            //25  Зона_Район
            //26  Год постройки
            //27  Этажность
            //28  Материал стен(итоговый)
            //29  Материал стен_КОД
            WorkSheet sheet = workbook.WorkSheets.First();
            List<OMCoreObject> objects = new List<OMCoreObject>();
            foreach (RangeRow row in sheet.Rows)
            {
                try
                {
                    long? roomsCount = row.ElementAt(18).Equals("-") ? null : row.ElementAt(18).ParseToLongNullable();
                    var analogObject = new OMCoreObject
                    {
                        CadastralNumber = row.ElementAt(0).ToString(),
                        BuildingCadastralNumber = row.ElementAt(1).ToString() != "0" ? row.ElementAt(1).ToString() : String.Empty,
                        CadastralQuartal = row.ElementAt(2).ToString() != "0" ? row.ElementAt(2).ToString() : String.Empty,
                        //Subgroup_Code = row.ElementAt(3),
                        //Group_Code = row.ElementAt(4),
                        PropertyMarketSegment_Code = GetMarketSegment(row.ElementAt(5).ToString()),
                        Address = row.ElementAt(6).ToString(),
                        Metro = row.ElementAt(7).ToString(),
                        SubwaySpace = row.ElementAt(8).ParseToDecimalNullable(),
                        Market_Code = MarketTypes.Rosreestr, //9 - источник информации (Всегда росреестр)
                        //10 - телефонный номер
                        ParserTime = DateTime.ParseExact(row.ElementAt(11).ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        //12 - тип объяевления
                        PropertyType_Code = LoadRosreestrDealsGetPropertyType(row.ElementAt(13).ToString()),
                        //14 - Вид использования (функциональное назначение)
                        //15 - Текущее использование
                        //16 - Тип объекта недвижимости
                        //17 - Вид права на ОКС
                        RoomsCount = roomsCount,
                        DealType_Code = GetDealType(row.ElementAt(19).ToString(), row.ElementAt(20).ToString()),
                        Area = row.ElementAt(21).ParseToDecimalNullable(),
                        Price = row.ElementAt(22).ParseToLongNullable(),
                        //23 - Удельная цена сделки/предложения, руб./кв.м (PricePerMeter)
                        QualityClass_Code = GetQualityClass(row.ElementAt(24).ToString()),
                        Zone = row.ElementAt(25).ParseToLongNullable(),
                        BuildingYear = row.ElementAt(26).ParseToLongNullable(),
                        FloorsCount = row.ElementAt(27).ParseToLongNullable(),
                        //28 - Материал стен (итоговый)
                        WallMaterial_Code = GetWallMaterial(row.ElementAt(29).ToString())
                    };

                    objects.Add(analogObject);
                }
                catch (Exception ex)
                {
                    log += $"{row.ElementAt(0)};false;{ex.Message}\n";
                }
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
                    OMCoreObject existsObject = OMCoreObject.Where(y => y.CadastralNumber == x.CadastralNumber).ExecuteFirstOrDefault();
                    if (existsObject != null)x.Id = existsObject.Id;
                    x.Save();
                    log += $"{x.CadastralNumber};true;\n";
                }
                catch (Exception ex)
                {
                    log += $"{x.CadastralNumber};false;{ex.Message}\n";
                }
                iterator++;
                Console.Write($"{iterator}\r");
            });
            File.WriteAllText(@"log.cvs", log);
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
                    return PropertyTypes.Pllacement;
                case "нежилое здание":
                case "жилой дом":
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
