using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using ObjectModel.Gbu.GroupingAlgoritm;
using ObjectModel.Directory;
using ObjectModel.Core.TD;
using Core.ErrorManagment;
using System.Security.Principal;
using System.Text.RegularExpressions;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.GbuObject.Entities;
using KadOzenka.Dal.GbuObject.Exceptions;
using Microsoft.Extensions.Configuration;
using Serilog;
using Newtonsoft.Json;
using ObjectModel.KO;
using Serilog.Context;

namespace KadOzenka.Dal.GbuObject
{
    public struct ReportHeaderWithColumnDic
    {
        public List<string> Headers;

        public Dictionary<long, long> DictionaryColumns;
    }

    public struct DataLevel
    {
        public string Code;
        public long FactorId;
    }

    public class PriorityItem
    {
        static readonly ILogger Log = Serilog.Log.ForContext<PriorityItem>();

        #region Коды из разных источников
        private string Code_Source_01 = string.Empty;
        private string Code_Source_02 = string.Empty;
        private string Code_Source_03 = string.Empty;
        private string Code_Source_04 = string.Empty;
        private string Code_Source_05 = string.Empty;
        private string Code_Source_06 = string.Empty;
        private string Code_Source_07 = string.Empty;
        private string Code_Source_08 = string.Empty;
        private string Code_Source_09 = string.Empty;
        private string Code_Source_10 = string.Empty;
        private string Code_Source_11 = string.Empty;

        private string Doc_Source_01 = string.Empty;
        private string Doc_Source_02 = string.Empty;
        private string Doc_Source_03 = string.Empty;
        private string Doc_Source_04 = string.Empty;
        private string Doc_Source_05 = string.Empty;
        private string Doc_Source_06 = string.Empty;
        private string Doc_Source_07 = string.Empty;
        private string Doc_Source_08 = string.Empty;
        private string Doc_Source_09 = string.Empty;
        private string Doc_Source_10 = string.Empty;
        private string Doc_Source_11 = string.Empty;

        private long? Doc_Id_01 = null;
        private long? Doc_Id_02 = null;
        private long? Doc_Id_03 = null;
        private long? Doc_Id_04 = null;
        private long? Doc_Id_05 = null;
        private long? Doc_Id_06 = null;
        private long? Doc_Id_07 = null;
        private long? Doc_Id_08 = null;
        private long? Doc_Id_09 = null;
        private long? Doc_Id_10 = null;
        private long? Doc_Id_11 = null;
        #endregion

        #region Служебные операции
        private bool CheckAllCode(out string code, out string source)
        {
            source = string.Empty;
            bool res = true;
            List<string> tmpList = new List<string>();
            if (Code_Source_01 != string.Empty)
            {
                tmpList.Add(Code_Source_01);
                if (source == string.Empty) source = "01";
            }
            if (Code_Source_02 != string.Empty)
            {
                tmpList.Add(Code_Source_02);
                if (source == string.Empty) source = "02";
            }
            if (Code_Source_03 != string.Empty)
            {
                tmpList.Add(Code_Source_03);
                if (source == string.Empty) source = "03";
            }
            if (Code_Source_04 != string.Empty)
            {
                tmpList.Add(Code_Source_04);
                if (source == string.Empty) source = "04";
            }
            if (Code_Source_05 != string.Empty)
            {
                tmpList.Add(Code_Source_05);
                if (source == string.Empty) source = "05";
            }
            if (Code_Source_06 != string.Empty)
            {
                tmpList.Add(Code_Source_06);
                if (source == string.Empty) source = "06";
            }
            if (Code_Source_07 != string.Empty)
            {
                tmpList.Add(Code_Source_07);
                if (source == string.Empty) source = "07";
            }
            if (Code_Source_08 != string.Empty)
            {
                tmpList.Add(Code_Source_08);
                if (source == string.Empty) source = "08";
            }
            if (Code_Source_09 != string.Empty)
            {
                tmpList.Add(Code_Source_09);
                if (source == string.Empty) source = "09";
            }
            if (Code_Source_10 != string.Empty)
            {
                tmpList.Add(Code_Source_10);
                if (source == string.Empty) source = "10";
            }
            code = string.Empty;
            if (tmpList.Count > 0) code = tmpList[0];
            else
                res = false;
            foreach (string item in tmpList)
            {
                res &= (code == item);
            }
            return res;
        }
        private bool CheckNotEmptyCode()
        {
            bool res = false;
            List<string> tmpList = new List<string>();
            if (Code_Source_01 != string.Empty) tmpList.Add(Code_Source_01);
            if (Code_Source_02 != string.Empty) tmpList.Add(Code_Source_02);
            if (Code_Source_03 != string.Empty) tmpList.Add(Code_Source_03);
            if (Code_Source_04 != string.Empty) tmpList.Add(Code_Source_04);
            if (Code_Source_05 != string.Empty) tmpList.Add(Code_Source_05);
            if (Code_Source_06 != string.Empty) tmpList.Add(Code_Source_06);
            if (Code_Source_07 != string.Empty) tmpList.Add(Code_Source_07);
            if (Code_Source_08 != string.Empty) tmpList.Add(Code_Source_08);
            if (Code_Source_09 != string.Empty) tmpList.Add(Code_Source_09);
            if (Code_Source_10 != string.Empty) tmpList.Add(Code_Source_10);
            if (Code_Source_11 != string.Empty) tmpList.Add(Code_Source_11);
            if (tmpList.Count > 0) res = true;
            return res;
        }
        private bool Check2Code(string code1, string code2, out string code)
        {
            bool res = true;
            List<string> tmpList = new List<string>();
            if (code1 != string.Empty) tmpList.Add(code1);
            if (code2 != string.Empty) tmpList.Add(code2);
            code = string.Empty;
            if (tmpList.Count > 0) code = tmpList[0]; else res = false;
            foreach (string item in tmpList)
            {
                res &= (code == item);
            }
            return res;
        }
        private bool CheckAllOneCode()
        {
            bool res = true;
            res &= (!Code_Source_01.Contains(";"));
            res &= (!Code_Source_02.Contains(";"));
            res &= (!Code_Source_03.Contains(";"));
            res &= (!Code_Source_04.Contains(";"));
            res &= (!Code_Source_05.Contains(";"));
            res &= (!Code_Source_06.Contains(";"));
            res &= (!Code_Source_07.Contains(";"));
            res &= (!Code_Source_08.Contains(";"));
            res &= (!Code_Source_09.Contains(";"));
            res &= (!Code_Source_10.Contains(";"));
            res &= (!Code_Source_11.Contains(";"));
            return res;
        }
        private string GetMaxCodeValue(string items)
        {
            List<string> tmpList = new List<string>();
            tmpList.AddRange(items.Split(';'));
            return GetMaxCodeValue(tmpList);
        }
        private string GetMaxCodeValue(List<string> items)
        {
            string code = string.Empty;
            //Выбрать из значений Списка 3 приоритетный Код по таблице
            Dictionary<int, int> rangvalue = new Dictionary<int, int>();
            Dictionary<int, string> codevalue = new Dictionary<int, string>();
            int i = 1;
            foreach (string tcode in items)
            {
                i++;
                if (tcode != string.Empty) { rangvalue.Add(i, PriorityGrouping.PrioritetList.GetValueByCode(tcode)); codevalue.Add(i, tcode); }
            }

            int maxrang = -1;
            int maxind = -1;

            foreach (KeyValuePair<int, int> item in rangvalue)
            {
                if (item.Value > maxrang) { maxrang = item.Value; maxind = item.Key; }
            }
            if (maxrang >= 0) { if (!codevalue.TryGetValue(maxind, out code)) code = string.Empty; }
            return code;
        }
        private string GetMaxCodeValue(List<PriorityGroupItem> items, out string source)
        {
            source = "00";
            string code = string.Empty;
            //Выбрать из значений Списка 3 приоритетный Код по таблице
            Dictionary<int, int> rangvalue = new Dictionary<int, int>();
            Dictionary<int, string> codevalue = new Dictionary<int, string>();
            Dictionary<int, string> sourcevalue = new Dictionary<int, string>();
            int i = 1;
            foreach (PriorityGroupItem tcode in items)
            {
                i++;
                if (tcode.Group != string.Empty) { rangvalue.Add(i, PriorityGrouping.PrioritetList.GetValueByCode(tcode.Group)); codevalue.Add(i, tcode.Group); sourcevalue.Add(i, tcode.Source); }
            }

            int maxrang = -1;
            int maxind = -1;

            foreach (KeyValuePair<int, int> item in rangvalue)
            {
                if (item.Value > maxrang) { maxrang = item.Value; maxind = item.Key; }
            }
            if (maxrang >= 0) { if (!(codevalue.TryGetValue(maxind, out code) && (sourcevalue.TryGetValue(maxind, out source)))) { code = string.Empty; source = "00"; } }

            if (code != string.Empty)
            {
                source = string.Empty;
                foreach (PriorityGroupItem item in items)
                {
                    if (item.Group == code)
                        source += (item.Source + ";");
                }
                source = source.TrimEnd(';');
            }

            return code;
        }
        private bool CheckIn(List<PriorityGroupItem> l1, List<PriorityGroupItem> l2)
        {
            bool res = false;
            foreach (PriorityGroupItem tcode in l1)
            {
                foreach (PriorityGroupItem pcode in l2)
                {
                    if (tcode.Group == pcode.Group)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }
        private bool Get1234510(bool div, out string code, out string source)
        {
            source = "00";
            code = string.Empty;

            List<PriorityGroupItem> tmpList = new List<PriorityGroupItem>();
            if (Code_Source_01 != string.Empty) tmpList.AddRange(PriorityGroupItem.GetItem(Code_Source_01.Split(';'), "01"));
            if (Code_Source_02 != string.Empty) tmpList.AddRange(PriorityGroupItem.GetItem(Code_Source_02.Split(';'), "02"));
            if (Code_Source_03 != string.Empty) tmpList.AddRange(PriorityGroupItem.GetItem(Code_Source_03.Split(';'), "03"));
            if (Code_Source_04 != string.Empty) tmpList.AddRange(PriorityGroupItem.GetItem(Code_Source_04.Split(';'), "04"));
            if (Code_Source_05 != string.Empty) tmpList.AddRange(PriorityGroupItem.GetItem(Code_Source_05.Split(';'), "05"));
            if (Code_Source_10 != string.Empty) tmpList.AddRange(PriorityGroupItem.GetItem(Code_Source_10.Split(';'), "10"));
            if (tmpList.Count == 0) return false;

            List<PriorityGroupItem> tmpList1 = new List<PriorityGroupItem>();
            if (Code_Source_03 != string.Empty) tmpList1.AddRange(PriorityGroupItem.GetItem(Code_Source_03.Split(';'), "03"));
            if (Code_Source_04 != string.Empty) tmpList1.AddRange(PriorityGroupItem.GetItem(Code_Source_04.Split(';'), "04"));
            List<PriorityGroupItem> tmpList2 = new List<PriorityGroupItem>();
            if (Code_Source_01 != string.Empty) tmpList2.AddRange(PriorityGroupItem.GetItem(Code_Source_01.Split(';'), "01"));
            if (Code_Source_02 != string.Empty) tmpList2.AddRange(PriorityGroupItem.GetItem(Code_Source_02.Split(';'), "02"));

            bool useiskl = true;
            if (Check2Code(Code_Source_03, Code_Source_04, out string tmpcode))//Значения Характеристик 3 и 4 идентичны ?
            {
                if (!CheckIn(tmpList1, tmpList2))//Хотя бы одно значение входит в перечень значений Списка 2?
                    useiskl = false;
            }
            else
            {
                if (!CheckIn(tmpList1, tmpList2))//Значение из Характеристики 3 или Характеристики 4 совпали со значениями из Списка 2?
                    useiskl = false;
            }

            if (useiskl)
            {
                //Сформировать из значений Характеристик 3 и 4, которые совпали со значениями Характеристик 1 и 2, Список 3 (с сохранением привязки к источникам значений)
                List<PriorityGroupItem> tmpList3 = new List<PriorityGroupItem>();
                foreach (PriorityGroupItem tcode in tmpList1)
                {
                    foreach (PriorityGroupItem pcode in tmpList2)
                    {
                        if (tcode.Group == pcode.Group)
                        {
                            if (!tmpList3.Contains(tcode)) tmpList3.Add(tcode);
                            if (!tmpList3.Contains(pcode)) tmpList3.Add(pcode);
                        }
                    }
                }
                code = GetMaxCodeValue(tmpList3, out source);
            }
            else
            {
                //Выбрать наиболее приоритетный Код по таблице
                List<PriorityGroupItem> tmpList3 = new List<PriorityGroupItem>();
                if (Code_Source_01 != string.Empty)
                    tmpList3.AddRange(PriorityGroupItem.GetItem(Code_Source_01.Split(';'), "01"));
                if (Code_Source_02 != string.Empty)
                    tmpList3.AddRange(PriorityGroupItem.GetItem(Code_Source_02.Split(';'), "02"));
                if (Code_Source_03 != string.Empty)
                    tmpList3.AddRange(PriorityGroupItem.GetItem(Code_Source_03.Split(';'), "03"));
                if (Code_Source_04 != string.Empty)
                    tmpList3.AddRange(PriorityGroupItem.GetItem(Code_Source_04.Split(';'), "04"));
                if (Code_Source_05 != string.Empty)
                    tmpList3.AddRange(PriorityGroupItem.GetItem(Code_Source_05.Split(';'), "05"));
                if (Code_Source_10 != string.Empty)
                    tmpList3.AddRange(PriorityGroupItem.GetItem(Code_Source_10.Split(';'), "10"));

                code = GetMaxCodeValue(tmpList3, out source);
            }
            return true;
        }
        #endregion

        #region Методы 
        private string GetGroupCode(out string source)
        {
            source = "00";
            if (CheckNotEmptyCode())
            {
                if (CheckAllOneCode())//везде по одному коду
                {
                    if (CheckAllCode(out string allonecode, out source))//коды одинаковые
                    {
                        return allonecode;
                    }
                    else//коды разные
                    {
                        if (Code_Source_09 != string.Empty)
                        {
                            source = "09";
                            return Code_Source_09;
                        }
                        else
                        if (Code_Source_07 != string.Empty)
                        {
                            source = "07";
                            return Code_Source_07;
                        }
                        else
                        if (Code_Source_08 != string.Empty)
                        {
                            source = "08";
                            return Code_Source_08;
                        }
                        else
                        if (Get1234510(false, out string code, out source)) return code;
                        else
                        if (Code_Source_06 != string.Empty)
                        {
                            source = "06";
                            return Code_Source_06;
                        }
                        else
                        if (Code_Source_11 != string.Empty)
                        {
                            source = "11";
                            return Code_Source_11;
                        }
                        else
                        {
                            source = "00";
                            return "14:000";
                        }
                    }
                }
                else //есть множественные коды
                {
                    if (CheckAllCode(out string allonecode, out source))//коды одинаковые
                    {
                        return GetMaxCodeValue(allonecode);
                    }
                    else//коды разные
                    {
                        if (Code_Source_09 != string.Empty)
                        {
                            source = "09";
                            return GetMaxCodeValue(Code_Source_09);
                        }
                        else
                        if (Code_Source_07 != string.Empty)
                        {
                            source = "07";
                            return GetMaxCodeValue(Code_Source_07);
                        }
                        else
                        if (Code_Source_08 != string.Empty)
                        {
                            source = "08";
                            return GetMaxCodeValue(Code_Source_08);
                        }
                        else
                        if (Get1234510(false, out string code, out source)) return code;
                        else
                        {
                            source = "00";
                            return "14:000";
                        }
                    }
                }
            }
            else
            {
                source = "00";
                return "14:000";
            }
        }

        private ValueItem GetValueFactor(List<GbuObjectAttribute> objectAttributes, long? idFactor)
        {
	        var res = new ValueItem
            {
                Value = string.Empty,
                IdDocument = null,
                AttributeName = string.Empty
            };

	        var attribute = objectAttributes.FirstOrDefault(x => x.AttributeId == idFactor);
	        if (attribute == null || string.IsNullOrWhiteSpace(attribute.GetValueInString())) 
		        return res;
	        
	        res.Value = attribute.GetValueInString();
            res.AttributeName = attribute.AttributeData.Name;
            res.IdDocument = attribute.ChangeDocId;

            return res;
        }

        private void AddValueFactor(long objectId, long? idFactor, long? idDoc, DateTime date, string value)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idFactor.Value,
                ObjectId = objectId,
                ChangeDocId = (idDoc == null) ? -1 : idDoc.Value,
                S = date,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = date,
                StringValue = value,
            };

            attributeValue.Save();
        }

        private static bool CompareDictToValue(OMCodDictionary dict, ValueItem val)
        {
            string CleanUp(string x)
            {
                // Оставляем только значимые символы в строках и убираем мусор из значения
                return Regex
                    .Replace(x,"\\s+","")
                    .Replace("_x000D_","")
                    .Replace("-","")
                    .ToLower();
            }

            if (dict.Value.Contains("_x000D_") || val.Value.Contains("_x000D_"))
            {
                Log.ForContext("OMCodDictionary",dict,true)
                    .ForContext("valueItem", val,true)
                    .Verbose("_x000D_ при сопоставлении данных");
            }

            bool cleanUpResult = CleanUp(dict.Value) == CleanUp(val.Value);
            return cleanUpResult;
        }

        private static void LogNotFoundDictionaryValues(ValueItem valueLevel, DataLevel dataLevel, List<OMCodDictionary> list, GroupingItem item)
        {
            var codId = list?[0].IdCodjob;
            Log.ForContext("ValueLevel", valueLevel, true)
                .ForContext("DataLevel", dataLevel, true)
                .ForContext("DictionaryId",codId)
                .Verbose("[Нормализация] {CadastralNumber}: {AttributeName}. Значение: {Value} отсутствует в классификаторе",
                    item.CadastralNumber, valueLevel.AttributeName, valueLevel.Value);
        }

        private ValueItem GetDataLevel(LevelItem level, ValueItem valueLevel, GroupingItem item, 
	        List<OMCodDictionary> dictionary, ConcurrentDictionary<long, OMInstance> documents,
	        ref string errorCODStr, ref bool errorCOD, ref string Code, ref string Source, ref long? docId,
	        out DataLevel dataLevel)
        {
            dataLevel =  new DataLevel();
            if (level.IdFactor != null)
            {
                dataLevel.FactorId = level.IdFactor.GetValueOrDefault();
                if (level.UseDictionary)
                {
                    if (!((valueLevel.Value == string.Empty) || (valueLevel.Value == "-" && level.SkipDefis)))
                    {
                        var dictionaryRecord = dictionary.Find(x => CompareDictToValue(x,valueLevel));
                        if (dictionaryRecord != null)
                        {
                            string code = dictionaryRecord.Code.Replace(" ", "");
                            if (code != "0")
                            {
                                Code = code;
                                dataLevel.Code = code;
                                if (valueLevel.IdDocument != null)
                                {
	                                var doc = GetDocument(documents, valueLevel.IdDocument.Value);
	                                if (doc != null)
                                    {
                                        Source = doc.Description;
                                        docId = doc.Id;
                                    }
                                }
                            }
                        }
                        else
                        {
                            LogNotFoundDictionaryValues(valueLevel,dataLevel,dictionary,item);
                            errorCOD = true;
                            errorCODStr += item.CadastralNumber + ": " + valueLevel.AttributeName + ". Значение: " + valueLevel.Value + " отсутствует в классификаторе" + Environment.NewLine;
                        }
                    }
                }
                else
                {
                    Code = valueLevel.Value.Replace(" ", "");
                    dataLevel.Code = Code;
                    if (valueLevel.IdDocument != null)
                    {
	                    var doc = GetDocument(documents, valueLevel.IdDocument.Value);
                        if (doc != null)
                        {
                            Source = doc.Description;
                            docId = doc.Id;
                        }
                    }
                }
            }

            return valueLevel;
        }

        private OMInstance GetDocument(ConcurrentDictionary<long, OMInstance> documents, long documentId)
        {
	        if(documents.ContainsKey(documentId))
	        {
		        return documents[documentId];
	        }

	        var document = OMInstance.Where(x => x.Id == documentId).Select(x => x.Description).ExecuteFirstOrDefault();
	        documents.TryAdd(document.Id, document);
	        Serilog.Log.Logger.Debug("Документ с ИД {DocumentId} добавлен в словарь", document.Id);

            return document;
        }

        public void SetPriorityGroup(GroupingSettings setting, List<OMCodDictionary> dictionaryItems,
	        List<long> allAttributeIds, GroupingItem inputItem,  DateTime dateActual, List<GbuObjectAttribute> objectAttributes,
	        GbuReportService reportService, Dictionary<long, long> dicColumns,
	        ConcurrentDictionary<long, OMInstance> documents)
        {
            GbuReportService.Row currentRow;
            lock (PriorityGrouping.locked)
            {
                currentRow = reportService.GetCurrentRow();
                PriorityGrouping.CurrentCount++;
                reportService.AddValue(inputItem.CadastralNumber, PriorityGrouping.KnColumn, currentRow);
            }

            Code_Source_01 = string.Empty;
            Code_Source_02 = string.Empty;
            Code_Source_03 = string.Empty;
            Code_Source_04 = string.Empty;
            Code_Source_05 = string.Empty;
            Code_Source_06 = string.Empty;
            Code_Source_07 = string.Empty;
            Code_Source_08 = string.Empty;
            Code_Source_09 = string.Empty;
            Code_Source_10 = string.Empty;
            Code_Source_11 = string.Empty;

            Doc_Source_01 = string.Empty;
            Doc_Source_02 = string.Empty;
            Doc_Source_03 = string.Empty;
            Doc_Source_04 = string.Empty;
            Doc_Source_05 = string.Empty;
            Doc_Source_06 = string.Empty;
            Doc_Source_07 = string.Empty;
            Doc_Source_08 = string.Empty;
            Doc_Source_09 = string.Empty;
            Doc_Source_10 = string.Empty;
            Doc_Source_11 = string.Empty;

            Doc_Id_01 = null;
            Doc_Id_02 = null;
            Doc_Id_03 = null;
            Doc_Id_04 = null;
            Doc_Id_05 = null;
            Doc_Id_06 = null;
            Doc_Id_07 = null;
            Doc_Id_08 = null;
            Doc_Id_09 = null;
            Doc_Id_10 = null;
            Doc_Id_11 = null;

            bool errorCOD = false;
            string errorCODStr = string.Empty;
            ValueItem Level1 = new ValueItem();
            ValueItem Level2 = new ValueItem();
            ValueItem Level3 = new ValueItem();
            ValueItem Level4 = new ValueItem();
            ValueItem Level5 = new ValueItem();
            ValueItem Level6 = new ValueItem();
            ValueItem Level7 = new ValueItem();
            ValueItem Level8 = new ValueItem();
            ValueItem Level9 = new ValueItem();
            ValueItem Level10 = new ValueItem();
            ValueItem Level11 = new ValueItem();
            try
            {
	            //по начальному порядку находим значение ГБУ-атрибутов всех уровней
				var valueItems = new List<ValueItem>();
                allAttributeIds.ForEach(x =>
                {
                    ////TODO для тестирования
                    //var attribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(c => c.Id == x)?.Name;
                    valueItems.Add(GetValueFactor(objectAttributes, x));
                });

                Level1 = GetDataLevel(setting.Level1, valueItems[0], inputItem, dictionaryItems, documents, ref errorCODStr, ref errorCOD, ref Code_Source_01, ref Doc_Source_01, ref Doc_Id_01, out DataLevel dataLevel1);
                Level2 = GetDataLevel(setting.Level2, valueItems[1], inputItem, dictionaryItems, documents, ref errorCODStr, ref errorCOD, ref Code_Source_02, ref Doc_Source_02, ref Doc_Id_02, out DataLevel dataLevel2);
                Level3 = GetDataLevel(setting.Level3, valueItems[2], inputItem, dictionaryItems, documents, ref errorCODStr, ref errorCOD, ref Code_Source_03, ref Doc_Source_03, ref Doc_Id_03, out DataLevel dataLevel3);
                Level4 = GetDataLevel(setting.Level4, valueItems[3], inputItem, dictionaryItems, documents, ref errorCODStr, ref errorCOD, ref Code_Source_04, ref Doc_Source_04, ref Doc_Id_04, out DataLevel dataLevel4);
                Level5 = GetDataLevel(setting.Level5, valueItems[4], inputItem, dictionaryItems, documents, ref errorCODStr, ref errorCOD, ref Code_Source_05, ref Doc_Source_05, ref Doc_Id_05, out DataLevel dataLevel5);
                Level6 = GetDataLevel(setting.Level6, valueItems[5], inputItem, dictionaryItems, documents, ref errorCODStr, ref errorCOD, ref Code_Source_06, ref Doc_Source_06, ref Doc_Id_06, out DataLevel dataLevel6);
                Level7 = GetDataLevel(setting.Level7, valueItems[6], inputItem, dictionaryItems, documents, ref errorCODStr, ref errorCOD, ref Code_Source_07, ref Doc_Source_07, ref Doc_Id_07, out DataLevel dataLevel7);
                Level8 = GetDataLevel(setting.Level8, valueItems[7], inputItem, dictionaryItems, documents, ref errorCODStr, ref errorCOD, ref Code_Source_08, ref Doc_Source_08, ref Doc_Id_08, out DataLevel dataLevel8);
                Level9 = GetDataLevel(setting.Level9, valueItems[8], inputItem, dictionaryItems, documents, ref errorCODStr, ref errorCOD, ref Code_Source_09, ref Doc_Source_09, ref Doc_Id_09, out DataLevel dataLevel9);
                Level10 = GetDataLevel(setting.Level10, valueItems[9], inputItem, dictionaryItems, documents, ref errorCODStr, ref errorCOD, ref Code_Source_10, ref Doc_Source_10, ref Doc_Id_10, out DataLevel dataLevel10);
                Level11 = GetDataLevel(setting.Level11, valueItems[10], inputItem, dictionaryItems, documents, ref errorCODStr, ref errorCOD, ref Code_Source_11, ref Doc_Source_11, ref Doc_Id_11, out DataLevel dataLevel11);

                    lock (PriorityGrouping.locked)
                    {
                        var levelsData = new List<DataLevel>
                        {
                            dataLevel1, dataLevel2, dataLevel3, dataLevel4, dataLevel5, dataLevel6, dataLevel7, dataLevel8,
                            dataLevel9, dataLevel10, dataLevel11
                        };

                        PriorityGrouping.AddInfoToReport(levelsData, currentRow, dicColumns, reportService);
                    }
              
                    string resGroup = GetGroupCode(out string source);

                    #region Результат
                    if (!errorCOD)
                    {
                        if (setting.IdAttributeResult != null)
                        {
                            long? id_doc = null;
                            if (source.Contains("09")) id_doc = Doc_Id_09;
                            else
                            if (source.Contains("07")) id_doc = Doc_Id_07;
                            else
                            if (source.Contains("08")) id_doc = Doc_Id_08;
                            else
                            if (source.Contains("01")) id_doc = Doc_Id_01;
                            else
                            if (source.Contains("04")) id_doc = Doc_Id_04;
                            else
                            if (source.Contains("03")) id_doc = Doc_Id_03;
                            else
                            if (source.Contains("05")) id_doc = Doc_Id_05;
                            else
                            if (source.Contains("06")) id_doc = Doc_Id_06;
                            else
                            if (source.Contains("02")) id_doc = Doc_Id_02;
                            else
                            if (source.Contains("10")) id_doc = Doc_Id_10;
                            else
                            if (source.Contains("11")) id_doc = Doc_Id_11;

                            try
                            {
	                            AddValueFactor(inputItem.ObjectId, setting.IdAttributeResult, id_doc, dateActual, resGroup);
							}
                            catch (Exception e)
                            {
	                            throw new GroupingAttributeSavingException($"Ошибка при сохрании значения '{resGroup}' в Характеристику", e);
                            }

                            lock (PriorityGrouping.locked)
                            {
                                reportService.AddValue(GbuObjectService.GetAttributeNameById(setting.IdAttributeResult.GetValueOrDefault()), PriorityGrouping.ResultColumn, currentRow);
                                reportService.AddValue(resGroup, PriorityGrouping.ValueColumn, currentRow);
                            }

                            {
                                string[] arrsource = source.Split(';');
                                string strsource = string.Empty;
                                foreach (string item in arrsource)
                                {
                                    if (item == "01")
                                    {
                                        strsource += (Level1.AttributeName + "; ");
                                    }

                                    if (item == "02")
                                    {
                                        strsource += (Level2.AttributeName + "; ");
                                    }

                                    if (item == "03")
                                    {
                                        strsource += (Level3.AttributeName + "; ");
                                    }

                                    if (item == "04")
                                    {
                                        strsource += (Level4.AttributeName + "; ");
                                    }

                                    if (item == "05")
                                    {
                                        strsource += (Level5.AttributeName + "; ");
                                    }

                                    if (item == "06")
                                    {
                                        strsource += (Level6.AttributeName + "; ");
                                    }

                                    if (item == "07")
                                    {
                                        strsource += (Level7.AttributeName + "; ");
                                    }

                                    if (item == "08")
                                    {
                                        strsource += (Level8.AttributeName + "; ");
                                    }

                                    if (item == "09")
                                    {
                                        strsource += (Level9.AttributeName + "; ");
                                    }

                                    if (item == "10")
                                    {
                                        strsource += (Level10.AttributeName + "; ");
                                    }

                                    if (item == "11")
                                    {
                                        strsource += (Level11.AttributeName + "; ");
                                    }
                                }

                                strsource = strsource.Trim().TrimEnd(';');

                                lock (PriorityGrouping.locked)
                                {
                                    reportService.AddValue(strsource, PriorityGrouping.SourceColumn, currentRow);
                                }

                                if (setting.IdAttributeSource != null)
                                {
	                                try
                                    {
										AddValueFactor(inputItem.ObjectId, setting.IdAttributeSource, null, dateActual, strsource);
									}
                                    catch (Exception e)
                                    {
	                                    throw new GroupingAttributeSavingException($"Ошибка при сохрании значения '{strsource}' в Источник", e);
                                    }
								}
                            }
                            if (setting.IdAttributeDocument != null)
                            {
                                string[] arrsource = source.Split(';');
                                string strsource = string.Empty;
                                foreach (string item in arrsource)
                                {
                                    if (item == "01")
                                    {
                                        strsource += (Doc_Source_01 + "; ");
                                    }
                                    if (item == "02")
                                    {
                                        strsource += (Doc_Source_02 + "; ");
                                    }
                                    if (item == "03")
                                    {
                                        strsource += (Doc_Source_03 + "; ");
                                    }
                                    if (item == "04")
                                    {
                                        strsource += (Doc_Source_04 + "; ");
                                    }
                                    if (item == "05")
                                    {
                                        strsource += (Doc_Source_05 + "; ");
                                    }
                                    if (item == "06")
                                    {
                                        strsource += (Doc_Source_06 + "; ");
                                    }
                                    if (item == "07")
                                    {
                                        strsource += (Doc_Source_07 + "; ");
                                    }
                                    if (item == "08")
                                    {
                                        strsource += (Doc_Source_08 + "; ");
                                    }
                                    if (item == "09")
                                    {
                                        strsource += (Doc_Source_09 + "; ");
                                    }
                                    if (item == "10")
                                    {
                                        strsource += (Doc_Source_10 + "; ");
                                    }
                                    if (item == "11")
                                    {
                                        strsource += (Doc_Source_11 + "; ");
                                    }
                                }
                                strsource = strsource.Trim().TrimEnd(';');
                                
                                try
                                {
									AddValueFactor(inputItem.ObjectId, setting.IdAttributeDocument, null, dateActual, strsource);
								}
                                catch (Exception e)
                                {
	                                throw new GroupingAttributeSavingException($"Ошибка при сохрании значения '{strsource}' в Документ", e);
                                }
							}

                            lock (PriorityGrouping.locked)
                            {
                                reportService.AddValue(errorCODStr, PriorityGrouping.ErrorColumn, currentRow);
                            }
                        }
                    }
                    else
                    {
                        lock (PriorityGrouping.locked)
                        {
                            reportService.AddValue(errorCODStr, PriorityGrouping.ErrorColumn, currentRow);
                        }
                    }
                    #endregion
            }
            catch (GroupingAttributeSavingException exception)
            {
	            var message = exception.Message;
	            LogException(message, inputItem, reportService, exception.InnerException, currentRow);
            }
            catch (Exception ex)
            {
	            var message = "В ходе обработки оъекта возникла ошибка";
	            LogException(message, inputItem, reportService, ex, currentRow);
            }
        }

        private static void LogException(string message, GroupingItem inputItem, GbuReportService reportService, Exception ex, GbuReportService.Row currentRow)
        {
	        lock (PriorityGrouping.locked)
	        {
		        //var errorId = ErrorManager.LogError(ex);
		        var fullMessage = $"{message}. {ex.Message}.";
		        reportService.AddValue(fullMessage, PriorityGrouping.GeneralErrorColumn, currentRow);
	        }

	        Serilog.Log.Logger.ForContext("Item", JsonConvert.SerializeObject(inputItem))
		        .ForContext("HumanMessage", message)
		        .Error(ex, "Ошибка группировки по КН {CadastralNumber}", inputItem.CadastralNumber);
        }

        #endregion
    }



    public class PriorityGroupingItemsGetter : AItemsGetter<GroupingItem>
    {
        private GroupingSettings Settings { get; }

        public PriorityGroupingItemsGetter(ILogger logger, GroupingSettings setting) : base(logger)
        {
            Settings = setting;
        }


        public override List<GroupingItem> GetItems()
        {
            return Settings.TaskFilter?.Count > 0
                ? GetUnits()
                : GetObjects();
        }

        private List<GroupingItem> GetUnits()
        {
            return OMUnit.Where(x => x.PropertyType_Code == PropertyTypes.Stead && Settings.TaskFilter.Contains((long)x.TaskId) && x.ObjectId != null)
                .Select(x => new
                {
                    x.ObjectId,
                    x.CadastralNumber,
                    x.CreationDate
                })
                .Execute()
                .Select(x => new GroupingItem
                {
                    Id = x.Id,
                    ObjectId = x.ObjectId.GetValueOrDefault(),
                    CadastralNumber = x.CadastralNumber,
                    CreationDate = x.CreationDate
                }).ToList();
        }

        private List<GroupingItem> GetObjects()
        {
            return ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == PropertyTypes.Stead)
                .Select(x => x.CadastralNumber)
                .Execute()
                .Select(x => new GroupingItem
                {
                    Id = x.Id,
                    ObjectId = x.Id,
                    CadastralNumber = x.CadastralNumber
                }).ToList();
        }
    }


    /// <summary>
    /// Приоритет группировки
    /// </summary>
    public class PriorityGrouping
    {
        private static readonly ILogger _log = Log.ForContext<PriorityGrouping>();

        #region Номера колонок отчета

        public static int KnColumn = 0;

        public static int ResultColumn = 1;

        public static int ValueColumn = 2;

        public static int SourceColumn = 3;

        public static int ErrorColumn = 4;

        public static int GeneralErrorColumn = 5;


        #endregion

        /// <summary>
        /// Объект для блокировки счетчика в многопоточке
        /// </summary>
        public static object locked;

        /// <summary>
        /// Общее число объектов
        /// </summary>
        public static int MaxCount = 0;
        /// <summary>
        /// Индекс текущего объекта
        /// </summary>
        public static int CurrentCount = 0;

        /// <summary>
        /// Количество объектов прошедших процедуру
        /// </summary>
        public static int SuccessCount = 0;

        /// <summary>
        /// Справочник приоритетов
        /// </summary>
        public static PriorityGroupList PrioritetList = null;

        public static List<string> ErrorMessages;

        // TODO: заменить на SRDSession.SetThreadCurrentPrincipal после обновления платформы
        public static void SetThreadCurrentPrincipal(long userId)
        {
            SRDUserBase userData = SRDCache.Users[(int)userId];
            GenericIdentity genericIdentity = new GenericIdentity(userData.Username);
            Thread.CurrentPrincipal = new GenericPrincipal(genericIdentity, new string[] { });
        }

        /// <summary>
        /// Выполнение операции группировки
        /// </summary>
        public static string SetPriorityGroup(GroupingSettings setting, CancellationToken processCancellationToken)
        {
            using var reportService =  new GbuReportService("Отчет нормализации");
            var dataHeaderAndColumnNumber = GenerateReportHeaderWithColumnNumber(setting);
          
            _log.Debug("Заголовки отчета и номера столбцов ${DictionaryColumns} ${Headers}", dataHeaderAndColumnNumber.DictionaryColumns, dataHeaderAndColumnNumber.Headers);
 
            reportService.AddHeaders(dataHeaderAndColumnNumber.Headers);
            try
            {
	            _log.Verbose("Применение стилей SetStyle");
	            reportService.SetIndividualWidth(KnColumn, 4);
	            reportService.SetIndividualWidth(ResultColumn, 6);
	            reportService.SetIndividualWidth(ValueColumn, 3);
	            reportService.SetIndividualWidth(SourceColumn, 6);
	            reportService.SetIndividualWidth(ErrorColumn, 5);
	            reportService.SetIndividualWidth(GeneralErrorColumn, 5);

	            foreach (var dictionaryColumn in dataHeaderAndColumnNumber.DictionaryColumns)
	            {
		            reportService.SetIndividualWidth((int)dictionaryColumn.Value, 3);
		            reportService.SetIndividualWidth((int)dictionaryColumn.Value + 1, 3);
	            }
            }
            catch (Exception ex)
            {
	            _log.Error(ex, "Форматирование отчета завершилось с ошибкой");
	            throw;
            }

            long reportId = 0;

            ErrorMessages = new List<string>();
            locked = new object();
            PrioritetList = new PriorityGroupList();

            var itemsGetter = new PriorityGroupingItemsGetter(_log, setting) as AItemsGetter<GroupingItem>;
            var actualDate = setting.DateActual?.Date ?? DateTime.Now.Date;
            itemsGetter = new GbuObjectStatusFilterDecorator<GroupingItem>(itemsGetter, _log, setting.ObjectChangeStatus, actualDate);

            var dictionaryItems = new List<OMCodDictionary>();
            if (setting.IdCodJob != null)
	            dictionaryItems = OMCodDictionary.Where(x => x.IdCodjob == setting.IdCodJob).Select(x => new
	            {
		            x.IdCodjob,
		            x.Value,
		            x.Code
	            }).Execute();
            _log.ForContext("CodDictionaryId", setting.IdCodJob).Debug("Найдено {Count} значений словаря", dictionaryItems?.Count);

            bool useTask = false;
            if (setting.TaskFilter != null) useTask = setting.TaskFilter.Count > 0;

			////TODO для тестирования
			//var objectIdsForTesting = new List<long> { 11614530, 13445766, 13664618, 11657698 };
			//var items = itemsGetter.GetItems().Where(x => objectIdsForTesting.Contains(x.ObjectId)).ToList();
			var items = itemsGetter.GetItems();
			MaxCount = items.Count;
            CurrentCount = 0;
            _log.ForContext("useTask", useTask)
	            .ForContext("Objs_0", JsonConvert.SerializeObject(items.ElementAtOrDefault(0)))
	            .Debug(useTask
			            ? "Нормализация по Заданиям на оценку. Всего {Count} единиц оценки"
			            : "Нормализация по Объектам Недвижимости. Всего {Count} объектов", MaxCount);


            var allAttributeIds = GetAttributes(setting);
            var queryManager = new QueryManager();
            queryManager.SetBaseToken(processCancellationToken);

            var gbuObjectService = new GbuObjectService(queryManager);
            //TODO если будут еще различия - вынести в интрефейс/класс
            if (useTask)
            {
	            ProcessUnits(setting, dictionaryItems, items, allAttributeIds, gbuObjectService, reportService,
		            dataHeaderAndColumnNumber, processCancellationToken);
            }
            else
            {
	            ProcessObjects(setting, dictionaryItems, items, allAttributeIds, actualDate, gbuObjectService, reportService,
		            dataHeaderAndColumnNumber, processCancellationToken);
            }

            items.Clear();

            reportId = reportService.SaveReport();

            return reportService.GetUrlToDownloadFile(reportId);
        }

        private static void ProcessUnits(GroupingSettings setting, List<OMCodDictionary> dictionaryItems, 
	        List<GroupingItem> units, List<long> allAttributeIds,
	        GbuObjectService gbuObjectService, GbuReportService reportService, 
	        ReportHeaderWithColumnDic dataHeaderAndColumnNumber, CancellationToken processCancellationToken)
		{
			var config = GetProcessConfigFromSettings();
			var localCancelTokenSource = new CancellationTokenSource();
			var options = new ParallelOptions
			{
				CancellationToken = localCancelTokenSource.Token,
				MaxDegreeOfParallelism = config.ThreadsCountForUnits
			};

			var documents = new ConcurrentDictionary<long, OMInstance>();
			var unitsGroupedByCreationDate = units.GroupBy(x => x.CreationDate ?? DateTime.Now.Date).ToDictionary(k => k.Key, v => v.ToList());
			Parallel.ForEach(unitsGroupedByCreationDate, options, groupedUnits =>
			{
				CheckCancellationToken(processCancellationToken, localCancelTokenSource, options);
				
				var localActualDate = groupedUnits.Key;
				_log.Debug("Начата работа с группой, у которой дата актуальности = '{ActualDate}'. Всего групп: {GroupsCount}", localActualDate, unitsGroupedByCreationDate.Count);

				//TODO если в группе будет много юнитов, сделать пагинацию внутри группы
				//if (groupedUnits.Value.Count > config.PackageSize)
				//{
				//	_log.Debug("Количество ЕО в группе ({CurrentUnitsCount}) больше размера пакета ({PackageSize}), дополнительно отправляем их в пакеты",
				//		groupedUnits.Value.Count, config.PackageSize);
				//}

				var objectIds = groupedUnits.Value.Select(x => x.ObjectId).ToList();
                _log.Debug("Отобрано {ObjectsCount} объектов группы, у которой дата актуальности = '{ActualDate}'", objectIds.Count, localActualDate);

                var objectAttributes = gbuObjectService.GetAllAttributes(objectIds, null, allAttributeIds, localActualDate,
	                attributesToDownload: new List<GbuColumnsToDownload> {GbuColumnsToDownload.Value, GbuColumnsToDownload.DocumentId})
	                .GroupBy(x => x.ObjectId).ToList();
                _log.Debug("Найдено {AttributesCount} атрибутов для объектов группы, у которой дата актуальности = '{ActualDate}'", objectAttributes.Count, localActualDate);

                ProcessItems(setting, dictionaryItems, documents, groupedUnits.Value, objectAttributes, allAttributeIds,
	                true, localActualDate, reportService, dataHeaderAndColumnNumber, processCancellationToken);
			});
		}

        private static void ProcessObjects(GroupingSettings setting, List<OMCodDictionary> dictionaryItems,
	        List<GroupingItem> objects, List<long> allAttributeIds, DateTime actualDate,
	        GbuObjectService gbuObjectService, GbuReportService reportService,
	        ReportHeaderWithColumnDic dataHeaderAndColumnNumber, CancellationToken processCancellationToken)
        {
	        var config = GetProcessConfigFromSettings();
            var localCancelTokenSource = new CancellationTokenSource();
	        var options = new ParallelOptions
	        {
		        CancellationToken = localCancelTokenSource.Token,
		        MaxDegreeOfParallelism = config.ThreadsCountForObjects
            };

            var packageSize = config.PackageSize;
	        var numberOfPackages = MaxCount / packageSize + 1;
	        var documents = new ConcurrentDictionary<long, OMInstance>();
            Parallel.For(0, numberOfPackages, options, (i, s) =>
	        {
		        CheckCancellationToken(processCancellationToken, localCancelTokenSource, options);

                _log.Debug("Начата работа с пакетом №{PackageNumber} из {MaxPackagesCount}", i, numberOfPackages);

                var objectsPage = objects.Skip(i * packageSize).Take(packageSize).ToList();
                _log.Debug("Отобрано {ObjectsCount} объектов для пакета №{PackageNumber} из {MaxPackagesCount}", objectsPage.Count, i, numberOfPackages);

                var objectAttributes = gbuObjectService.GetAllAttributes(objectsPage.Select(x => x.ObjectId).ToList(), null, allAttributeIds, actualDate,
		                attributesToDownload: new List<GbuColumnsToDownload> {GbuColumnsToDownload.Value, GbuColumnsToDownload.DocumentId})
	                .GroupBy(x => x.ObjectId).ToList();
                _log.Debug("Найдено {AttributesCount} атрибутов для пакета №{PackageNumber} из {MaxPackagesCount}", objectAttributes.Count, i, numberOfPackages);

                ProcessItems(setting, dictionaryItems, documents, objectsPage, objectAttributes, allAttributeIds,
	                false, actualDate, reportService, dataHeaderAndColumnNumber, processCancellationToken);
            });
        }


        private static void ProcessItems(GroupingSettings setting, 
	        List<OMCodDictionary> dictionaryItems, ConcurrentDictionary<long, OMInstance> documents,
            List<GroupingItem> items, List<IGrouping<long, GbuObjectAttribute>> objectAttributes, List<long> allAttributeIds, 
	        bool useTask, DateTime actualDate,
	        GbuReportService reportService, ReportHeaderWithColumnDic dataHeaderAndColumnNumber, 
	        CancellationToken processCancellationToken)
        {
	        var localCancelTokenSource = new CancellationTokenSource();
	        var options = new ParallelOptions
	        {
		        CancellationToken = localCancelTokenSource.Token,
		        MaxDegreeOfParallelism = 20
	        };

	        var objectAttributesDictionary = objectAttributes.ToDictionary(k => k.Key, v => v.ToList());
	        var defaultAttributes = allAttributeIds.Select(x => new GbuObjectAttribute { AttributeId = x }).ToList();
	        var userId = SRDSession.GetCurrentUserId().GetValueOrDefault();
	        Parallel.ForEach(items, options, item =>
	        {
		        CheckCancellationToken(processCancellationToken, localCancelTokenSource, options);

		        //если работаем с единицами оценки, дата актуальности должна браться из них
		        var localActualDate = useTask
			        ? item.CreationDate?.Date ?? DateTime.Now.Date
			        : actualDate;

		        SetThreadCurrentPrincipal(userId);

		        var currentObjectAttributes = objectAttributesDictionary.TryGetValue(item.ObjectId, out var attributes)
			        ? attributes
			        : defaultAttributes;

		        new PriorityItem().SetPriorityGroup(setting, dictionaryItems, allAttributeIds, item, localActualDate,
			        currentObjectAttributes, reportService, dataHeaderAndColumnNumber.DictionaryColumns, documents);
	        });
        }

        private static ProcessConfig GetProcessConfigFromSettings()
        {
	        var fileName = "appsettings.json";
	        _log.Debug("Поиск настроек конфигурации из файла {FileName}", fileName);

	        var configuration = new ConfigurationBuilder()
		        .AddJsonFile(path: fileName, optional: false, reloadOnChange: true)
		        .Build();

	        var config = new ProcessConfig();
	        var sectionName = "MainOperations:Grouping";
	        configuration.GetSection(sectionName).Bind(config);
	        _log.ForContext("Configs", config, true).Debug("Полученные настройки конфигурации для секции {SectionName}", sectionName);

	        var packageSize = config.PackageSize == 0 ? 100000 : config.PackageSize;
	        var threadsCountForObjects = config.ThreadsCountForObjects == 0 ? 20 : config.ThreadsCountForObjects;
	        var threadsCountForUnits = config.ThreadsCountForUnits == 0 ? 20 : config.ThreadsCountForUnits;

	        config.PackageSize = packageSize;
	        config.ThreadsCountForObjects = threadsCountForObjects;
	        config.ThreadsCountForUnits = threadsCountForUnits;

	        _log.ForContext("ResultConfigs", config, true).Debug("Итоговые настройки конфигурации для секции {SectionName}", sectionName);

	        return config;
        }

        private static void CheckCancellationToken(CancellationToken processCancellationToken,
	        CancellationTokenSource localCancellationToken, ParallelOptions options)
        {
	        if (!processCancellationToken.IsCancellationRequested)
		        return;

	        localCancellationToken.Cancel();
	        options.CancellationToken.ThrowIfCancellationRequested();
        }

        private static List<long> GetAttributes(GroupingSettings setting)
        {
            return new List<long>
            {
	            setting.Level1.IdFactor.GetValueOrDefault(), setting.Level2.IdFactor.GetValueOrDefault(),
	            setting.Level3.IdFactor.GetValueOrDefault(), setting.Level4.IdFactor.GetValueOrDefault(),
	            setting.Level5.IdFactor.GetValueOrDefault(), setting.Level6.IdFactor.GetValueOrDefault(),
	            setting.Level7.IdFactor.GetValueOrDefault(), setting.Level8.IdFactor.GetValueOrDefault(),
	            setting.Level9.IdFactor.GetValueOrDefault(), setting.Level10.IdFactor.GetValueOrDefault(),
	            setting.Level11.IdFactor.GetValueOrDefault()
            };
        }

        public static ReportHeaderWithColumnDic GenerateReportHeaderWithColumnNumber(GroupingSettings setting)
        {
            List<string> resHeaderList = new List<string>{ "КН", "Поле в которое вносилось значение", "Внесенное значение", "Источник внесенного значения", "Ошибка внесенного значения", "Ошибка" };
            ReportHeaderWithColumnDic res = new ReportHeaderWithColumnDic();

            var dicColumns = new Dictionary<long, long>();
            int lastColumn = GeneralErrorColumn;
            int levelTitle = 1;
            foreach (FieldInfo propertyInfo in typeof(GroupingSettings).GetFields(BindingFlags.Instance |
                                                                                     BindingFlags.NonPublic |
                                                                                     BindingFlags.Public))
            {
                if (propertyInfo.Name.IndexOf("Level", StringComparison.Ordinal) != -1)
                {
                    if (propertyInfo.GetValue(setting) is LevelItem && ((LevelItem)propertyInfo.GetValue(setting)).IdFactor != null)
                    {
                        lastColumn++;
                        var lItem = (LevelItem)propertyInfo.GetValue(setting);
                        resHeaderList.AddRange(new List<string>{ GbuObjectService.GetAttributeNameById(lItem.IdFactor.GetValueOrDefault()), $"(Уровень - {levelTitle}) Источник информации" });
                        
                        Log.Verbose("Атрибут ОН. {FactorName}, Id {FactorID}", lItem.IdFactor.GetValueOrDefault(), lItem.IdFactor);
                        
                        dicColumns.Add(lItem.IdFactor.GetValueOrDefault(), lastColumn);
                        lastColumn++;
                        levelTitle++;
                    }
                }
            }

            res.Headers = resHeaderList;
            res.DictionaryColumns = dicColumns;
            return res;
        }

        public static void AddInfoToReport(List<DataLevel> dataLevels, GbuReportService.Row rowNumber, Dictionary<long, long> dictionaryColumns, GbuReportService reportService)
        {
            lock (locked)
            {
                foreach (var dataLevel in dataLevels)
                {
                    if (!dataLevel.Code.IsNullOrEmpty())
                    {
                        string registerName = GbuObjectService.GetRegisterNameByAttributeId(dataLevel.FactorId);
                        long column = dictionaryColumns.FirstOrDefault(x => x.Key == dataLevel.FactorId).Value;
                        reportService.AddValue(dataLevel.Code, (int)column, rowNumber);
                        reportService.AddValue(registerName, (int)column + 1, rowNumber);
                    }
                }
            }
        }
    }

    #region Entities

    public class GroupingItem : ItemBase
    {
        public string CadastralNumber { get; set; }
        public DateTime? CreationDate { get; set; }
    }

    internal class ProcessConfig
    {
	    public int PackageSize { get; set; }
	    public int ThreadsCountForObjects { get; set; }
	    public int ThreadsCountForUnits { get; set; }
    }

    #endregion
}
