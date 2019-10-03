﻿using System;
using System.Collections.Generic;
using System.Linq;

using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using IronXL;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace DebugApplication
{
    class Program
    {
        static void Main(string[] args)
        {
			LoadRosreestrDeals();
            //LongProcessManagementService service = new LongProcessManagementService();
            //service.Start();
            //return;
        }

        static void LoadRosreestrDeals()
        {
            string log = String.Empty;
			
			//WorkBook workbook = WorkBook.Load(@"C:\Users\silanov\Desktop\Лист Microsoft Excel.xlsx");
			WorkBook workbook = WorkBook.Load(@"D:\2. Рабочая папка\Кадастровая оценка\Сделки Росреестра 2018, 2019.xlsx");

			//0   КН
			//1   КН Здания
			//2   КК
			//3   подгруппа
			//4   № п / п
			//5   Группа сегмента рынка
			//6   Адресный ориентир
			//7   Наименование Метро
			//8   Источник информации
			//9   Номер телефона
			//10  Дата предложения(сделки)
			//11  Текст объявления
			//12  Вид объекта недвижимости
			//13  Вид использования(функциональное назначение)
			//14  Текущее использование
			//15  Вид права на ОКС
			//16  Количество комнат
			//17  Факт сделки(сделка, предложение)
			//18  Площадь ОКС, кв.м
			//19  Цена сделки/ предложения, руб.
			//20  Удельная цена сделки / предложения, руб./ кв.м
			//21  Статус
			//22  Клас здания
			//23  КК
			//24  Район
			//25  Зона
			//26  Зона_Район

			WorkSheet sheet = workbook.WorkSheets.First();
            int iterator = 0;
            foreach (RangeRow row in sheet.Rows)
            {
                try
                {
                    var analogObject = new OMCoreObject
                    {
                        Market_Code = MarketTypes.Rosreestr,
						DealType_Code = DealType.SaleDeal,

						CadastralNumber = row.ElementAt(0).ToString(),
						BuildingCadastralNumber = row.ElementAt(1).ToString() != "0" ? row.ElementAt(1).ToString() : String.Empty,
						CadastralQuartal = row.ElementAt(2).ToString() != "0" ? row.ElementAt(1).ToString() : String.Empty,
						//GroupCode =  row.ElementAt(3)
						Group = row.ElementAt(5).ToString(),
						Subgroup = row.ElementAt(3).ToString(),
						Address = row.ElementAt(6).ToString(),
						ParserTime = row.ElementAt(10).ParseToDateTime(),
						PropertyType_Code = LoadRosreestrDealsGetPropertyType(row.ElementAt(12).ToString()),
						Area = row.ElementAt(18).ParseToDecimal(),
						Price = row.ElementAt(19).ParseToLong(),
						PricePerMeter = row.ElementAt(20).ParseToDecimal(),
						District = row.ElementAt(24).ToString(),
						Zone = row.ElementAt(25).ParseToLong(),
					};
                    analogObject.Save();
                    log += $"{sheet.Rows.IndexOf(row)};true;\n";
                }
                catch(Exception ex)
                {
                    log += $"Error===>{sheet.Rows.IndexOf(row)};{row.ElementAt(0)};false;{ex.Message}\n";
                }
                //if (iterator >= 10) break;
                iterator++;

				Console.Write(iterator);
            }
            Console.WriteLine(log);
        }

		private static PropertyTypes LoadRosreestrDealsGetPropertyType(string type)
		{
			type = type.ToLower();

			switch(type)
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
				case "машино - место":
					return PropertyTypes.Parking;
				default:
					throw new Exception($"Не известный тип объекта: {type}");
			}
		}
		
		static PropertyTypes GetPropertyType(string data)
        {
            switch (data)
            {
                case "нежилое здание":
                case "жилой дом": return PropertyTypes.Building;
                case "комната":
                case "нежилое помещение":
                case "квартира": return PropertyTypes.Pllacement;
                case "машино-место": return PropertyTypes.Parking;
                case "сооружение": return PropertyTypes.Construction;
            }
            return PropertyTypes.None;
        }

    }
}
