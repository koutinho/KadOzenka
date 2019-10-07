using System;
using System.Collections.Generic;
using System.Linq;

using Core.Register.LongProcessManagment;
using IronXL;
using ObjectModel.Directory;
using ObjectModel.Market;
using OuterMarketParser;


namespace DebugApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoadExcel();
            //LongProcessManagementService service = new LongProcessManagementService();
            //service.Start();
            new OuterMarketParser.Launcher.OuterMarketParser().StartProcess();
            return;
        }

        static void LoadExcel()
        {
            string log = String.Empty;

            WorkBook workbook = WorkBook.Load(@"C:\Users\silanov\Desktop\Лист Microsoft Excel.xlsx");
            WorkSheet sheet = workbook.WorkSheets.First();
            int iterator = 0;
            foreach (RangeRow row in sheet.Rows)
            {

                try
                {
                    var analogObject = new OMCoreObject
                    {
                        Market_Code = MarketTypes.Rosreestr,
                        CadastralNumber = row.ElementAt(0).ToString(),
                        PropertyType_Code = GetPropertyType(row.ElementAt(12).ToString()),
                        DealType_Code =  DealType.SaleDeal
                    };
                    analogObject.Save();
                    log += $"{sheet.Rows.IndexOf(row)};true;\n";
                }
                catch(Exception ex)
                {
                    log += $"Error===>{sheet.Rows.IndexOf(row)};{row.ElementAt(0)};false;{ex.Message}\n";
                }
                if (iterator >= 10) break;
                iterator++;
            }
            Console.WriteLine(log);
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
