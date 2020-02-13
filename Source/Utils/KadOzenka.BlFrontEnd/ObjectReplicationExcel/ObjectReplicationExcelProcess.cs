using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

using ObjectModel.Gbu;
using GemBox.Spreadsheet;
using ObjectModel.Core.TD;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.Shared;
using KadOzenka.Dal.WebRequest;
using ObjectModel.Market;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;
using KadOzenka.Dal.Logger;
using System.Diagnostics;

namespace KadOzenka.BlFrontEnd.ObjectReplicationExcel
{

    public class ObjectReplicationExcelProcess
    {
        public static string BaseDirectory
        {
            get { return ConfigurationManager.AppSettings["ObjectReplicationExcelBaseFolder"]; }
        }

        private static Dictionary<long, OMInstance> _documents = new Dictionary<long, OMInstance>();

        private static Dictionary<long, OMReferenceItem> _refItems = new Dictionary<long, OMReferenceItem>();

        public static void StartImport()
        {
            if (!Directory.Exists(BaseDirectory)) throw new Exception($"Директория не существует: {BaseDirectory}");
            LoadDocuments();
            LoadObjects();
            LoadReference();
            LoadAttributes();
        }

        private static void LoadDocuments()
        {
            var dt = GetData($"{BaseDirectory}tbDocument.xlsx");
            foreach (DataRow row in dt.Rows)
            {
                OMInstance document = new OMInstance
                {
                    Id = row["id_document"].ParseToLong(),
                    RegNumber = row["num_document"].ToString(),
                    CreateDate = row["date_document"].ParseToDateTime(),
                    ApproveDate = row["date_document"].ParseToDateTime(),
                    Description = row["name_document"].ToString(),
                };
                document.Save();
                _documents.Add(document.Id, document);
            }
        }

        private static void LoadObjects()
        {
            var dt = GetData($"{BaseDirectory}tbObject.xlsx");
            foreach (DataRow row in dt.Rows)
            {
                OMMainObject gbuObject = new OMMainObject
                {
                    Id = row["id_object"].ParseToLong(),
                    CadastralNumber = row["kn_object"].ToString(),
                    ObjectType_Code = ObjectModel.Directory.PropertyTypes.Building,
                    IsActive = row["status_object"].ParseToBoolean()
                };
                gbuObject.Save();
            }
        }

        private static void LoadReference()
        {
            var dt = GetData($"{BaseDirectory}DICTIONARYRECORD.xlsx");
            foreach (DataRow row in dt.Rows)
            {
                OMReferenceItem refItem = new OMReferenceItem
                {
                    ItemId = row["ID_RECORD"].ParseToLong(),
                    Value = row["VAL_RECORD"].ToString()
                };
                //document.Save();
                _refItems.Add(refItem.ItemId, refItem);
            }
        }

        private static void LoadAttributes()
        {
            var dt = GetData($"{BaseDirectory}tbFactorDateValue.xlsx");
            foreach (DataRow row in dt.Rows)
            {
                var attributeValue = GetAttributeValueCommon(row);
                attributeValue.DtValue = row["value"].ParseToDateTime();
                attributeValue.Save();
            }
            dt = GetData($"{BaseDirectory}tbFactorDoubleValue.xlsx");
            foreach (DataRow row in dt.Rows)
            {
                var attributeValue = GetAttributeValueCommon(row);
                attributeValue.NumValue = row["value"].ParseToDecimal();
                attributeValue.Save();
            }
            dt = GetData($"{BaseDirectory}tbFactorTextValue.xlsx");
            foreach (DataRow row in dt.Rows)
            {
                var attributeValue = GetAttributeValueCommon(row);
                attributeValue.StringValue = row["value"].ToString();
                attributeValue.Save();
            }
            dt = GetData($"{BaseDirectory}tbFactorLinkValue.xlsx");
            foreach (DataRow row in dt.Rows)
            {
                var attributeValue = GetAttributeValueCommon(row);
                attributeValue.RefItemId = row["value"].ParseToLong();
                attributeValue.StringValue = _refItems[attributeValue.RefItemId].Value;
                attributeValue.Save();
            }
        }

        private static GbuObjectAttribute GetAttributeValueCommon(DataRow row)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = row["id_value"].ParseToInt(),
                AttributeId = row["id_factor"].ParseToInt(),
                ObjectId = row["id_object"].ParseToInt(),
                ChangeDocId = row["id_document"].ParseToInt(),
                S = row["date_value"].ParseToDateTime(),
                ChangeUserId = row["id_user"].ParseToInt(),
                ChangeDate = row["date_user"].ParseToDateTime(),
            };
            attributeValue.Ot = _documents[attributeValue.ChangeDocId].CreateDate;
            return attributeValue;
        }

        private static DataTable GetData(string filePath)
        {
            ExcelFile excelFile = ExcelFile.Load(filePath, new XlsxLoadOptions());
            ExcelWorksheet ws = excelFile.Worksheets[0];
            DataTable dt = new DataTable();
            ExcelRow headerRow = ws.Rows[0];
            for (int i = 0; i < headerRow.AllocatedCells.Count; i++) dt.Columns.Add(headerRow.Cells[i].Value != null ? headerRow.Cells[i].Value.ToString() : i.ToString());
            ExtractToDataTableOptions options = new ExtractToDataTableOptions(1, 0, ws.Rows.Count - 1){ ExtractDataOptions = ExtractDataOptions.SkipEmptyRows };
            options.ExcelCellToDataTableCellConverting += (sender, e) =>
            {
                if (e.IsDataTableValueValid) return;
                // GemBox.Spreadsheet doesn't automatically convert numbers to strings in ExtractToDataTable() method because of culture issues; 
                // someone would expect the number 12.4 as "12.4" and someone else as "12,4".
                e.DataTableValue = e.ExcelCellValue == null ? null : e.ExcelCellValue.ToString();
                e.Action = ExtractDataEventAction.Continue;
            };
            ws.ExtractToDataTable(dt, options);
            return dt;
        }

        public static void SetCadastralNumber(string filePath, string defaultExcelValue)
        {
            ExcelFile workbook = ExcelFile.Load(filePath);
            ExcelWorksheet worksheet = workbook.Worksheets[0];
            int ACtr = 25000, CCur = 0, SCtr = 0, ECtr = 0, MCtr = worksheet.Rows.Take(ACtr).Count();
            List<string> errorLog = new List<string>();
            foreach (var row in worksheet.Rows)
            {
                if(row.Cells[1].Value == null && CCur < ACtr)
                {
                    string address = row.Cells[0].Value.ToString();
                    if (!address.Contains("Москва")) address = $"Москва, {address}";
                    OMYandexAddress yAddress = null;
                    try
                    {
                        yAddress = new Dal.JSONParser.YandexGeocoder().ParseYandexAddress(new YandexGeocoder().GetDataByAddress(address));
                        row.Cells[1].Value = yAddress.FormalizedAddress;
                        SCtr++;
                    }
                    catch (Exception ex) 
                    {
                        errorLog.Add($"[{DateTime.Now}]: {address}\n{ex.Message}\n");
                        ECtr++; 
                    }
                    if(yAddress == null)
                    {
                        row.Cells[1].Value = defaultExcelValue;
                        row.Cells[2].Value = defaultExcelValue;
                    }
                    else
                    {
                        row.Cells[1].Value = yAddress.FormalizedAddress;
                        OMYandexAddress obj = OMYandexAddress.Where(x => x.FormalizedAddress == yAddress.FormalizedAddress).SelectAll().ExecuteFirstOrDefault();
                        if (obj == null)
                        {
                            row.Cells[2].Value = defaultExcelValue;
                            errorLog.Add($"[{DateTime.Now}]: {address}\nКадастровый номер для данного адреса не найден в базе данных\n");
                        }
                        else row.Cells[2].Value = obj.CadastralNumber;
                    }
                    CCur++;
                    ConsoleLog.WriteData("Присвоение координат объектам из исходного файла", MCtr, CCur, SCtr, ECtr);
                }
            }
            errorLog.Add($"========> Присвоение координат объектам из исходного файла завершено ({ConsoleLog.GetResultData(MCtr, CCur, SCtr, ECtr)})\n");
            ConsoleLog.WriteFotter("Присвоение координат объектам из исходного файла завершено");
            ConsoleLog.LogError(errorLog.ToArray(), "Присвоение координат объектам из исходного файла");
            workbook.Save(filePath);
        }

        public static void FormFile(string filePath)
        {
            List<OMCoreObject> initial = OMCoreObject.Where(x => x.Market_Code == ObjectModel.Directory.MarketTypes.Rosreestr).Select(x => new { x.Address }).OrderBy(x => x.Address).Execute().ToList();
            ExcelFile workbook = new ExcelFile();
            ExcelWorksheet worksheet = workbook.Worksheets.Add("В единственном экземпляре");
            int i = 1;
            Regex regexMain = 
                new Regex("(^[0-9]{6}[ ])|" +
                          "(, кв .*)|(, кв[.] .*)|(, кв[.].*)|( кв[.].*)|( кв .*)|(,ап[.].*)|(, квартира.*)|(,кв[.][0-9].*)|" +
                          "(, административные помещен.*)|(, нежилое помещен.*)|(, нежилые помещен.*)|(, кладовое помещен.*)|(, помещен.*)|( помещен.*)|(, пом([ ]|[.]|[ещ.]|[I]).*)|" +
                          "(, м/м.*)|(, I м/м.*)|(, машиноместо.*)|(, машино-место.*)|" +
                          "(, бокс.*)|(, гараж-бокс.*)|(, гаражный бокс.*)|( гар.бокс.*)|(, гараж.*)", RegexOptions.IgnoreCase);
            Regex regexSpaces = new Regex("[ ]{2,}");
            initial
                .Select(x => new { Address = regexMain.Replace(regexSpaces.Replace(x.Address, " "), string.Empty), id=x.Id })
                .GroupBy(x => x.Address)
                .Select(x => new { Address = x.Key, IDs = x.Select(p => p.id) })
                .ToList()
                .ForEach(x => 
                {
                    worksheet.Cells[$"A{i}"].Value = x.Address;
                    worksheet.Cells[$"B{i}"].Value = x.IDs.Count();
                    worksheet.Cells[$"C{i}"].Value = string.Join(",", x.IDs);
                    worksheet.Cells[$"D{i}"].Value = 0;
                    i++;
                });
            workbook.Save(filePath);
        }

        public static void UploadRosreestrObjectsToDatabase()
        {
            ExcelFile excelFile = ExcelFile.Load(@"C:\Users\silanov\Documents\Дженикс\ЦИПЖС Объекты аналоги\Росреестр (27.09.2019)\Сделки Росреестра 2018, 2019.xlsx", new XlsxLoadOptions());
            ExcelWorksheet ws = excelFile.Worksheets[0];
            List<OMCoreObject> list = new List<OMCoreObject>(), result = new List<OMCoreObject>();
            foreach(var row in ws.Rows.Skip(1))
            {
                OMCoreObject obj = new OMCoreObject();
                obj.CadastralNumber = row.Cells[0].Value.ToString();
                obj.BuildingCadastralNumber = row.Cells[1].Value.ToString().Equals("0") ? row.Cells[0].Value.ToString() : row.Cells[1].Value.ToString();
                obj.CadastralQuartal = getQuartal(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString());
                obj.Subgroup = row.Cells[3].Value.ToString();
                obj.Group = row.Cells[5].Value.ToString();
                obj.Address = row.Cells[6].Value.ToString();
                obj.Market_Code = ObjectModel.Directory.MarketTypes.Rosreestr;
                obj.ParserTime = DateTime.ParseExact(row.Cells[10].Value.ToString().Split(' ')[0], "dd.MM.yyyy", null);
                detectCategories(obj, row.Cells[12].Value.ToString(), row.Cells[13].Value.ToString(), row.Cells[14].Value.ToString());
                obj.DealType_Code = ObjectModel.Directory.DealType.SaleDeal;
                obj.Area = decimal.Parse(row.Cells[18].Value.ToString());
                obj.Price = decimal.ToInt64(decimal.Parse(row.Cells[19].Value.ToString()));
                obj.PricePerMeter = decimal.Parse(row.Cells[20].Value.ToString());
                ObjectModel.Directory.QualityClass? qualityClass = getBuildingType(row.Cells[22].Value.ToString());
                if (qualityClass != null) obj.QualityClass_Code = (ObjectModel.Directory.QualityClass)qualityClass;
                obj.District = row.Cells[24].Value.ToString();
                obj.Zone = int.TryParse(row.Cells[25].Value.ToString(), out int n) ? (long?)long.Parse(row.Cells[25].Value.ToString()) : null;
                obj.ProcessType_Code = ObjectModel.Directory.ProcessStep.AddressStep;
                if (obj.PricePerMeter > 19000) list.Add(obj);
            }
            list.GroupBy(x => x.CadastralNumber).ToList().ForEach(x => result.Add(x.OrderByDescending(y => y.ParserTime).First()));
            result.ForEach(x => x.Save());
        }

        public static string getQuartal(string KN, string BKN, string KQ) => KQ.Equals(string.Empty) ? getCadastralQuartal(BKN.Equals("0") ? KN : BKN) : KQ;

        public static ObjectModel.Directory.QualityClass? getBuildingType(string buildingType)
        {
            switch (buildingType)
            {
                case "А": return ObjectModel.Directory.QualityClass.A;
                case "А+": return ObjectModel.Directory.QualityClass.Aplus;
                case "В": return ObjectModel.Directory.QualityClass.B;
                case "В+": return ObjectModel.Directory.QualityClass.Bplus;
                case "С": return ObjectModel.Directory.QualityClass.C;
            }
            return null;
        }

        public static string getCadastralQuartal(string cadastralNumber) => cadastralNumber.Substring(0, cadastralNumber.LastIndexOf(":"));

        public static string getFormalizedAddress(string initialAddress)
        {
            Regex regexMain = new Regex("(^[0-9]{6}[ ])|" +
              "(, кв .*)|(, кв[.] .*)|(, кв[.].*)|( кв[.].*)|( кв .*)|(,ап[.].*)|(, квартира.*)|(,кв[.][0-9].*)|" +
              "(, административные помещен.*)|(, нежилое помещен.*)|(, нежилые помещен.*)|(, кладовое помещен.*)|(, помещен.*)|( помещен.*)|(, пом([ ]|[.]|[ещ.]|[I]).*)|" +
              "(, м/м.*)|(, I м/м.*)|(, машиноместо.*)|(, машино-место.*)|" +
              "(, бокс.*)|(, гараж-бокс.*)|(, гаражный бокс.*)|( гар.бокс.*)|(, гараж.*)", RegexOptions.IgnoreCase), regexSpaces = new Regex("[ ]{2,}");
            return regexMain.Replace(regexSpaces.Replace(initialAddress, " "), string.Empty);
        }

        public static void detectCategories(OMCoreObject obj, string propType, string propUse, string curUse)
        {
            curUse = curUse.ToLower();
            switch (propType)
            {
                case "жилой дом":
                    obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Buildings;
                    switch (propUse)
                    {
                        case "Объекты индивидуальной жилой застройки":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.IZHS;
                            break;
                        case "Объекты многоквартирной жилой застройки":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.MZHS;
                            break;
                        case "Объекты неустановленного назначения":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты садового, огородного и дачного строительства":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Garden;
                            break;
                        case "Объекты социальной инфраструктуры":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты, предназначенные для размещения санаториев":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Sanatorium;
                            break;
                    }
                    break;
                case "квартира":
                    obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Placements;
                    switch (propUse)
                    {
                        case "Объекты индивидуальной жилой застройки":
                            if (curUse.Contains("дома")) obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Buildings;
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.IZHS;
                            break;
                        case "Объекты коммерческого назначения":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты многоквартирной жилой застройки":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.MZHS;
                            break;
                        case "Объекты неустановленного назначения":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты садового, огородного и дачного строительства":
                            if (curUse.Contains("дома"))
                            {
                                obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Buildings;
                                obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.IZHS;
                            }
                            else obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Garden;
                            break;
                        case "Объекты социальной инфраструктуры":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты, предназначенные для размещения административных и офисных зданий":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Office;
                            break;
                        case "Объекты, предназначенные для размещения гостиниц":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Appartment;
                            break;
                        case "Объекты, предназначенные для хранения индивидуального транспорта":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.CarParking;
                            break;
                    }
                    break;
                case "комната":
                    obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Placements;
                    switch(propUse)
                    {
                        case "Объекты индивидуальной жилой застройки":
                            if (curUse.Contains("дома")) obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Buildings;
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.IZHS;
                            break;
                        case "Объекты многоквартирной жилой застройки":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.MZHS;
                            break;
                        case "Объекты неустановленного назначения":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты садового, огородного и дачного строительства":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Garden;
                            break;
                    }
                    break;
                case "машино-место":
                    obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Placements;
                    switch (propUse)
                    {
                        case "Объекты коммерческого назначения":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.CarParking;
                            break;
                        case "Объекты неустановленного назначения":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.CarParking;
                            break;
                        case "Объекты производственного назначения":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.CarParking;
                            break;
                        case "Объекты, предназначенные для хранения индивидуального транспорта":
                            if(curUse.Contains("бокс")) obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Parking;
                            else obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.CarParking;
                            break;
                    }
                    break;
                case "нежилое здание":
                    obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Buildings;
                    switch (propUse)
                    {
                        case "Объекты и сооружения общественного назначения":
                            obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.OtherAndMore;
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты коммерческого назначения":
                            if (curUse == "-" || curUse.Contains("баня") || curUse.Contains("санкомплекс") || curUse.Contains("помещение") || curUse.Contains("контора"))
                                obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            else if (curUse.Contains("кафе") || curUse.Contains("столовая")) obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.PublicCatering;
                            else if (curUse.Contains("аптека") || curUse.Contains("придорожного") || curUse.Contains("мойка") ||
                                     curUse.Contains("торговый") || curUse.Contains("павильон") || curUse.Contains("магазин") || curUse.Contains("торговое"))
                                obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Trading;
                            else if (curUse.Contains("производственн") || curUse.Contains("промбаза") || curUse.Contains("складской корпус"))
                                obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Factory;
                            else if (curUse == "Офисно-торговое здание") obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            else obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            if(curUse.Contains("помещение")) obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Placements;
                            break;
                        case "Объекты неустановленного назначения":
                            if (curUse.Contains("бытовка") || curUse.Contains("въездная") || curUse.Contains("производственный") || curUse.Contains("цех") || curUse.Contains("производственное"))
                                obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.OtherAndMore;
                            else if (curUse.Contains("помещение")) obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Placements;
                            if (curUse.Contains("садовый") || curUse.Contains("сарай") || curUse.Contains("хоз") || curUse.Contains("сенной"))
                                obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Garden;
                            else if (curUse.Contains("бвк") || curUse.Contains("быстровозводимых") || curUse.Contains("термического") || curUse.Contains("огнеупоров"))
                                obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Trading;
                            else if (curUse.Contains("цех") || curUse.Contains("производственное") || curUse.Contains("производственный")) 
                                obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Factory;
                            else if (curUse.Contains("гостевой") || curUse == "жилое строение") obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.IZHS;
                            else if (curUse.Contains("нежилые помещения")) obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.MZHS;
                            else obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты портов, вокзалов, станций":
                            if(curUse.Contains("павильон")) obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.OtherAndMore;
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты производственного назначения":
                            if (curUse.Contains("помещение") && !curUse.Contains("помещением")) obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Placements;
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Factory;
                            break;
                        case "Объекты садового, огородного и дачного строительства":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Garden;
                            break;
                        case "Объекты социальной инфраструктуры":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты, предназначенные для размещения административных и офисных зданий":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Office;
                            break;
                        case "Объекты, предназначенные для размещения гостиниц":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Hotel;
                            break;
                        case "Объекты, предназначенные для размещения санаториев":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Sanatorium;
                            break;
                        case "Объекты, предназначенные для хранения индивидуального транспорта":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Parking;
                            break;
                    }
                    break;
                case "нежилое помещение":
                    obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Placements;
                    switch (propUse)
                    {
                        case "Объекты вспомогательного назначения":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты коммерческого назначения":
                            if (curUse.Contains("машино")) obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.CarParking;
                            else if (curUse.Contains("чайная")) obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Trading;
                            else if (curUse.Contains("часть здания"))
                            {
                                obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Buildings;
                                obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.IZHS;
                            }
                            else obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты многоквартирной жилой застройки":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.MZHS;
                            break;
                        case "Объекты неустановленного назначения":
                            if (curUse.Contains("бокс")) obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.CarParking;
                            else if(curUse.Contains("машино")) obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Parking;
                            else if (curUse.Contains("часть здания"))
                            {
                                obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Buildings;
                                obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.IZHS;
                            }
                            else obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты портов, вокзалов, станций":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты производственного назначения":
                            if (curUse.Contains("бокс")) obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Parking;
                            else obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Factory;
                            break;
                        case "Объекты садового, огородного и дачного строительства":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Garden;
                            break;
                        case "Объекты социальной инфраструктуры":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;
                        case "Объекты, предназначенные для размещения административных и офисных зданий":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Office;
                            break;
                        case "Объекты, предназначенные для размещения гостиниц":
                            obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Appartment;
                            break;
                        case "Объекты, предназначенные для хранения индивидуального транспорта":
                            if (curUse.Contains("бокс")) obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.CarParking;
                            else if (curUse.Contains("машино")) obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Parking;
                            else if (curUse.Contains("мойка")) obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Trading;
                            else if (curUse.Contains("часть здания"))
                            {
                                obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.Buildings;
                                obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.IZHS;
                            }
                            else obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.NoSegment;
                            break;

                    }
                    break;
                case "сооружение":
                    obj.PropertyTypesCIPJS_Code = ObjectModel.Directory.PropertyTypesCIPJS.OtherAndMore;
                    obj.PropertyMarketSegment_Code = ObjectModel.Directory.MarketSegment.Factory;
                    break;
            }
        }

        private class ExceleData
        {
            public string address;
            public List<int> ids;
            public int status;
            public int currentCounter;
        }

        public static void SetRRFDBCoordinatesByYandex() 
        {
            List<OMCoreObject> AllObjects =
                OMCoreObject.Where(x => x.ProcessType_Code == ObjectModel.Directory.ProcessStep.AddressStep && x.Market_Code == ObjectModel.Directory.MarketTypes.Rosreestr)
                            .Select(x => new { x.ProcessType_Code, x.Address, x.Lng, x.Lat, x.ExclusionStatus_Code }).Execute().Take(Int32.Parse(ConfigurationManager.AppSettings["YandexLimit"])).ToList();
            int ACtr = AllObjects.Count, CCur = 0, SCtr = 0, ECtr = 0;
            AllObjects.ForEach(x => 
            {
                CCur++;
                try
                {
                    OMYandexAddress address = new Dal.JSONParser.YandexGeocoder().ParseYandexAddress(new YandexGeocoder().GetDataByAddress(getFormalizedAddress(x.Address)));
                    x.Lng = address.Lng;
                    x.Lat = address.Lat;
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Dealed;
                    SCtr++;
                }
                catch (Exception) 
                {
                    x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Excluded;
                    x.ExclusionStatus_Code = ObjectModel.Directory.ExclusionStatus.NoAddress;
                    ECtr++; 
                }
                x.Save();
                ConsoleLog.WriteData("Присвоение координат объектам росреестра", ACtr, CCur, SCtr, ECtr);
            });
            ConsoleLog.WriteFotter("Присвоение координат объектам росреестра завершено");
        }

        public static void SetRRCoordinatesByYandex(string filePath)
        {
            ExcelFile workbook = ExcelFile.Load(filePath);
            ExcelWorksheet worksheet = workbook.Worksheets[0];
            List<ExceleData> buffer = new List<ExceleData>();
            int i = 1, ACtr = 25000, CCur = 0, SCtr = 0, ECtr = 0, MCtr = 0;
            List<string> errorLog = new List<string>();
            foreach (var row in worksheet.Rows)
            {
                if(row.Cells[3].Value.ParseToInt() == 0)
                {
                    buffer.Add(new ExceleData
                    {
                        address = row.Cells[0].Value.ToString(),
                        ids = row.Cells[2].Value.ToString().Split(",").Select(x => x.ParseToInt()).ToList(),
                        status = row.Cells[3].Value.ToString().ParseToInt(),
                        currentCounter = i
                    });
                }
                i++;
            }
            MCtr = buffer.Take(ACtr).Count();
            buffer.Take(ACtr).ToList().ForEach(x => 
            {
                CCur++;
                try
                {
                    OMYandexAddress address = new Dal.JSONParser.YandexGeocoder().ParseYandexAddress(new YandexGeocoder().GetDataByAddress(x.address));
                    x.ids.ForEach(y => 
                    {
                        OMCoreObject obj = OMCoreObject.Where(z => z.Id == y).Select(z => new { z.Address, z.Lng, z.Lat }).ExecuteFirstOrDefault();
                        obj.Lng = address.Lng;
                        obj.Lat = address.Lat;
                        obj.Save();
                    });
                    SCtr++;
                }
                catch (Exception ex)
                {
                    errorLog.Add($"[{DateTime.Now}]: {x.address}\n{ex.Message}\n");
                    ECtr++; 
                }
                worksheet.Cells[$"D{x.currentCounter}"].Value = 1;
                ConsoleLog.WriteData("Присвоение координат объектам росреестра", MCtr, CCur, SCtr, ECtr);
            });
            errorLog.Add($"========> Присвоение координат объектам росреестра завершено ({ConsoleLog.GetResultData(MCtr, CCur, SCtr, ECtr)})\n");
            ConsoleLog.WriteFotter("Присвоение координат объектам росреестра завершено");
            ConsoleLog.LogError(errorLog.ToArray(), "Присвоение координат объектам росреестра");
            workbook.Save(filePath);
        }

	}

}
