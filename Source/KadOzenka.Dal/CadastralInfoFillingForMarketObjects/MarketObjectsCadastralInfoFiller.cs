using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Logger;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Interfaces;
using MarketPlaceBusiness.Interfaces.Utils;
using ObjectModel.Gbu;
using ObjectModel.Market;

namespace KadOzenka.Dal.CadastralInfoFillingForMarketObjects
{
    /// <summary>
    /// Привязка к объектам аналогам кадастровых кварталов
    /// </summary>
    public class MarketObjectsCadastralInfoFiller
    {
        public static long RosreestrRegisterId => 2;
        public static long RosreestrCadastralQuarterAttributeId => 601;

        public GbuObjectService GbuObjectService { get; set; }
        private IMarketObjectForCadastralInfoFiller MarketObjectService { get; set; }

        public MarketObjectsCadastralInfoFiller()
        {
            GbuObjectService = new GbuObjectService();
            MarketObjectService = new MarketObjectForUtilsService();
        }

        public void PerformFillingCadastralQuarterProc()
        {
            var marketObjectsWithCadastralNumber = MarketObjectService.GetObjectsWithCadastralNumber();
            Console.WriteLine($"Найдено {marketObjectsWithCadastralNumber.Count} объектов-аналогов с заполненными кадастровыми номерами");

            int totalCount = marketObjectsWithCadastralNumber.Count, currentCount = 0;
            int fromMainObjectCount = 0, fromAllpriRosreestrSourceCount = 0, fromCadastralNumber = 0;
            foreach (var marketObject in marketObjectsWithCadastralNumber)
            {
                var sameGbuObjects = OMMainObject.Where(x => x.CadastralNumber == marketObject.CadastralNumber)
                    .SelectAll()
                    .Execute();

                if (sameGbuObjects.Count == 1)
                {
                    if (!string.IsNullOrEmpty(sameGbuObjects.First().KadastrKvartal))
                    {
                        marketObject.CadastralQuartal = sameGbuObjects.First().KadastrKvartal;
                        fromMainObjectCount++;
                    }
                    else
                    {
                        var res = GbuObjectService.GetAllAttributes(sameGbuObjects.First().Id,
                            new List<long> { RosreestrRegisterId }, new List<long> { RosreestrCadastralQuarterAttributeId }, DateTime.Now);

                        if (res.Count != 0 && !string.IsNullOrEmpty(res.First().StringValue))
                        {
                            marketObject.CadastralQuartal = res.First().StringValue;
                            fromAllpriRosreestrSourceCount++;
                        }
                        else
                        {
                            FillQuarterByCadastralNumber(marketObject);
                            fromCadastralNumber++;
                        }
                    }
                }
                else
                {
                    FillQuarterByCadastralNumber(marketObject);
                    fromCadastralNumber++;
                }

                marketObject.Save();

                currentCount++;
                ConsoleLog.WriteGbuCadastralFillingData(totalCount, currentCount,
                    fromMainObjectCount, fromAllpriRosreestrSourceCount, fromCadastralNumber);
            }

            Console.WriteLine($"Обработка завершена");
        }

        public void PerformFillingCadastralInfoByQuarterProc()
        {
            var quartalDictionary = OMQuartalDictionary.Where(x => true)
                .SelectAll()
                .Execute().ToDictionary(x => x.CadastralQuartal);
            var marketObjectsWithCadastralNumber = MarketObjectService.GetObjectsWithCadastralQuartal();
            Console.WriteLine($"Найдено {marketObjectsWithCadastralNumber.Count} объектов-аналогов с заполненными кадастровым кварталом");

            int totalCount = marketObjectsWithCadastralNumber.Count, currentCount = 0, correctCount = 0, errorCount = 0;
            foreach (var marketObject in marketObjectsWithCadastralNumber)
            {
                try
                {
                    if (!quartalDictionary.TryGetValue(marketObject.CadastralQuartal, out var quarterInfo))
                    {
                        throw new Exception(
                            $"Отсутствует информация по кварталу {marketObject.CadastralQuartal}");
                    }

                    marketObject.District_Code = quarterInfo.District_Code;
                    marketObject.Neighborhood_Code = quarterInfo.Region_Code;
                    marketObject.Zone = quarterInfo.Zone;
                    marketObject.ZoneRegion = quarterInfo.ZoneRegion;

                    marketObject.Save();

                    correctCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n{ex.Message}");
                    errorCount++;
                }
                currentCount++;
                ConsoleLog.WriteData("Обработка объектов", totalCount, currentCount, correctCount, errorCount);
            }

            Console.WriteLine($"Обработка завершена");
        }

        private void FillQuarterByCadastralNumber(OMCoreObject marketObject)
        {
            var ellipsisLastIndex = marketObject.CadastralNumber.LastIndexOf(":");
            marketObject.CadastralQuartal = marketObject.CadastralNumber.Substring(0, ellipsisLastIndex);
        }
    }
}
