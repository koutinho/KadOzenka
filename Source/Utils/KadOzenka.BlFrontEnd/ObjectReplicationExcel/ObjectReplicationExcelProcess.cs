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

namespace KadOzenka.BlFrontEnd.ObjectReplicationExcel
{

    public class ObjectReplicationExcelProcess
    {
        public static string BaseDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["ObjectReplicationExcelBaseFolder"];
            }
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
            for (int i = 0; i < headerRow.AllocatedCells.Count; i++)
            {
                dt.Columns.Add(headerRow.Cells[i].Value != null ? headerRow.Cells[i].Value.ToString() : i.ToString());
            }

            ExtractToDataTableOptions options = new ExtractToDataTableOptions(1, 0, ws.Rows.Count - 1)
            {
                ExtractDataOptions = ExtractDataOptions.SkipEmptyRows
            };

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

        public static void GAF()
        {
            ExcelFile workbook = ExcelFile.Load(@"C:\Users\silanov\Desktop\На привязку адреса.xlsx");
            ExcelWorksheet worksheet = workbook.Worksheets[0];
            int ACtr = 24000, CCur = 0, SCtr = 0, ECtr = 0;
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
                    catch (Exception) { ECtr++; }
                    if(yAddress == null)
                    {
                        row.Cells[1].Value = "-";
                        row.Cells[2].Value = "-";
                    }
                    else
                    {
                        row.Cells[1].Value = yAddress.FormalizedAddress;
                        OMYandexAddress obj = OMYandexAddress.Where(x => x.FormalizedAddress == yAddress.FormalizedAddress).SelectAll().ExecuteFirstOrDefault();
                        if(obj == null) row.Cells[2].Value = "-";
                        else row.Cells[2].Value = obj.CadastralNumber;
                    }
                    CCur++;
                    ConsoleLog.WriteData("Присвоение координат объектам из исходного файла", ACtr, CCur, SCtr, ECtr);
                }
            }
            ConsoleLog.WriteFotter("Присвоение координат объектам из исходного файла завершено");
            workbook.Save(@"C:\Users\silanov\Desktop\На привязку адреса.xlsx");
        }

        public static void FormFile()
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
            workbook.Save(@"C:\Users\silanov\Desktop\Workbook.xls");
        }

        private class ExceleData
        {
            public string address;
            public List<int> ids;
            public int status;
            public int currentCounter;
        }

        public static void REX()
        {
            ExcelFile workbook = ExcelFile.Load(@"C:\Users\silanov\Desktop\Workbook.xls");
            ExcelWorksheet worksheet = workbook.Worksheets[0];
            List<ExceleData> buffer = new List<ExceleData>();
            int i = 1, ACtr = 25000, CCur = 0, SCtr = 0, ECtr = 0;
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
                catch (Exception){ ECtr++; }
                worksheet.Cells[$"D{x.currentCounter}"].Value = 1;
                ConsoleLog.WriteData("Присвоение координат объектам росреестра", ACtr, CCur, SCtr, ECtr);
            });
            ConsoleLog.WriteFotter("Присвоение координат объектам росреестра завершено");
            workbook.Save(@"C:\Users\silanov\Desktop\Workbook.xls");
        }



	}

}
