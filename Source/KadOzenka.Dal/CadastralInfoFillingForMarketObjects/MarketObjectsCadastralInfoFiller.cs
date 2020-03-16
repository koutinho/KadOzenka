﻿using System;
using System.Collections.Generic;
using System.Linq;
using GemBox.Spreadsheet;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Gbu;
using ObjectModel.Market;

namespace KadOzenka.Dal.CadastralInfoFillingForMarketObjects
{
    public class MarketObjectsCadastralInfoFiller
    {
        public static long RosreestrRegisterId => 2;
        public static long RosreestrCadastralQuarterAttributeId => 601;

        public GbuObjectService GbuObjectService { get; set; }

        private Dictionary<string, CadastralQuarterInfo> CadastralQuarters { get; set; }

        public MarketObjectsCadastralInfoFiller()
        {
            GbuObjectService = new GbuObjectService();
        }

        public void PerformFillingCadastralQuarterProc()
        {
            var marketObjectsWithCadastralNumber = OMCoreObject.Where(x => x.CadastralNumber != null && x.CadastralNumber != string.Empty)
                .Select(x => x.Id)
                .Select(x => x.CadastralNumber)
                .Select(x => x.CadastralQuartal)
                .Execute();
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

        public void PerformFillingCadastralInfoByQuarterProc(string cadastralQuartersInfoFilePath)
        {
            InitCadastralQuartersInfoFromFile(cadastralQuartersInfoFilePath);
            FillCadastralQuarterInfo();
        }

        private void InitCadastralQuartersInfoFromFile(string cadastralQuartersInfoFilePath)
        {
            var file = ExcelFile.Load(cadastralQuartersInfoFilePath, new XlsxLoadOptions());
            var mainWorkSheet = file.Worksheets[0];

            var maxColumns = mainWorkSheet.CalculateMaxUsedColumns();
            var columnNames = new List<string>();
            for (var i = 0; i < maxColumns; i++)
            {
                columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
            }

            var dataRows = mainWorkSheet.Rows.Where(x => x.Index != 0);
            CadastralQuarters = new Dictionary<string, CadastralQuarterInfo>();
            foreach (var dataRow in dataRows)
            {
                var info = new CadastralQuarterInfo();
                info.DistrictName = dataRow.Cells[columnNames.IndexOf("Наименование АО")].StringValue.Trim();
                info.RegionName = dataRow.Cells[columnNames.IndexOf("РАЙОНЫ")].StringValue.Trim();
                info.ZoneNumber = dataRow.Cells[columnNames.IndexOf("Номер Зоны")].IntValue;
                info.ZoneAndDistrictName = dataRow.Cells[columnNames.IndexOf("ЗОНА+ОКРУГ")].StringValue.Trim();

                CadastralQuarters.Add(dataRow.Cells[columnNames.IndexOf("КК")].StringValue.Trim(), info);
            }
        }

        private void FillCadastralQuarterInfo()
        {
            var marketObjectsWithCadastralNumber = OMCoreObject.Where(x => x.CadastralQuartal != null && x.CadastralQuartal != string.Empty)
                .Select(x => x.Id)
                .Select(x => x.CadastralNumber)
                .Select(x => x.CadastralQuartal)
                .Select(x => x.District)
                .Select(x => x.District_Code)
                .Select(x => x.Region)
                .Select(x => x.RegionId)
                .Select(x => x.Zone)
                .Execute();
            Console.WriteLine($"Найдено {marketObjectsWithCadastralNumber.Count} объектов-аналогов с заполненными кадастровым кварталом");

            int totalCount = marketObjectsWithCadastralNumber.Count, currentCount = 0, correctCount = 0, errorCount = 0;
            foreach (var marketObject in marketObjectsWithCadastralNumber)
            {
                try
                {
                    
                    if (!CadastralQuarters.TryGetValue(marketObject.CadastralQuartal, out var quarterInfo))
                        throw new Exception(
                            $"В файле отсутствует информация по кварталу {marketObject.CadastralQuartal}");

                    //TODO: дополнить после измения структуры БД для объектов-аналогов
                    marketObject.District = XMLPolyLineDictionary.getCorrectNameForDistrict(quarterInfo.DistrictName);
                    marketObject.Region = XMLPolyLineDictionary.getCorrectNameForRegion(quarterInfo.RegionName);
                    marketObject.Zone = quarterInfo.ZoneNumber;

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