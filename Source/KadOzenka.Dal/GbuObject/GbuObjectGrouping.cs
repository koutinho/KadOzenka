using System;
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
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject.Decorators;
using KadOzenka.Dal.GbuObject.Dto;
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

        private ValueItem GetValueFactor(long objectId, long? idFactor, DateTime? date)
        {
            ValueItem res = new ValueItem
            {
                Value = string.Empty,
                IdDocument = null,
                AttributeName = string.Empty
            };

            List<long> lstIds = new List<long>();
            if (idFactor != null) lstIds.Add(idFactor.Value);
            List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(objectId, null, lstIds, date);
            if (attribs.Count > 0)
            {
                if (attribs[0].GetValueInString() != string.Empty && attribs[0].GetValueInString() != null)
                {
                    res.Value = attribs[0].GetValueInString();
                    res.AttributeName = attribs[0].AttributeData.Name;
                    res.IdDocument = attribs[0].ChangeDocId;
                }
            }

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
                // Оставляем только значимые символы в строках
                return x
                    .Replace(" ", "")
                    .Replace("\n","")
                    .Replace("\r","")
                    .ToLower();
            }

            bool toLowerResult = dict.Value.ToLower() == val.Value.ToLower();
            bool cleanUpResult = CleanUp(dict.Value) == CleanUp(val.Value);

            if (toLowerResult != cleanUpResult)
            {
                Log.ForContext("dictValue", JsonConvert.SerializeObject(dict))
                    .ForContext("checkedValue", JsonConvert.SerializeObject(val))
                    .ForContext("toLowerEquality",toLowerResult)
                    .ForContext("cleanUpEquality",cleanUpResult)
                    .Warning("Нормализация: Разные результаты методов сравнения значений.");
            }

            return cleanUpResult || toLowerResult;
        }


        private ValueItem GetDataLevelForObject(LevelItem level, ObjectModel.Gbu.OMMainObject obj, DateTime dateActual, List<ObjectModel.KO.OMCodDictionary> Dictionary, 
	        ref string errorCODStr, ref bool errorCOD, ref string Code, ref string Source, ref long? DocId, out DataLevel dataLevel)
        {
	        dataLevel =  new DataLevel();
	        ValueItem ValueLevel = new ValueItem();
            if (level.IdFactor != null)
            {
	            dataLevel.FactorId = level.IdFactor.GetValueOrDefault();
				   ValueLevel = GetValueFactor(obj.Id, level.IdFactor, dateActual);
                if (level.UseDictionary)
                {
                    if (!((ValueLevel.Value == string.Empty) || (ValueLevel.Value == "-" && level.SkipDefis)))
                    {
                        var dictionaryRecord = Dictionary.Find(x => CompareDictToValue(x,ValueLevel));

                        if (dictionaryRecord != null)
                        {
                            string code = dictionaryRecord.Code.Replace(" ", "");
                            if (code != "0")
                            {
                                Code = code;
                                dataLevel.Code = code;
                                if (ValueLevel.IdDocument != null)
                                {
                                    OMInstance doc = OMInstance.Where(x => x.Id == ValueLevel.IdDocument.Value).SelectAll().ExecuteFirstOrDefault();
                                    if (doc != null)
                                    {
                                        Source = doc.Description;
                                        DocId = doc.Id;
                                    }
                                }
                            }
                        }
                        else
                        {
                            errorCOD = true;
                            errorCODStr += (obj.CadastralNumber + ": " + ValueLevel.AttributeName + ". Значение: " + ValueLevel.Value + " отсутствует в классификаторе" + Environment.NewLine);
                        }
                    }

                }
                else
                {
                    Code = ValueLevel.Value.Replace(" ", "");
                    dataLevel.Code = Code;
                    if (ValueLevel.IdDocument != null)
                    {
                        OMInstance doc = OMInstance.Where(x => x.Id == ValueLevel.IdDocument.Value).SelectAll().ExecuteFirstOrDefault();
                        if (doc != null)
                        {
                            Source = doc.Description;
                            DocId = doc.Id;
                        }
                    }
                }
            }

            return ValueLevel;
        }

        private ValueItem GetDataLevelForUnit(LevelItem level, ObjectModel.KO.OMUnit unit, DateTime dateActual, List<ObjectModel.KO.OMCodDictionary> Dictionary, 
	        ref string errorCODStr, ref bool errorCOD, ref string Code, ref string Source, ref long? DocId, out DataLevel dataLevel)
        {
	        dataLevel = new DataLevel();
			ValueItem ValueLevel = new ValueItem();
            if (level.IdFactor != null)
            {
	            dataLevel.FactorId = level.IdFactor.GetValueOrDefault();
	            ValueLevel = GetValueFactor(unit.ObjectId.Value, level.IdFactor, dateActual);
                if (level.UseDictionary)
                {
                    if (new Random().Next(0, 10000) > 9960)
                        Log.Debug("Значение атрибута уровня {Value} {FactorId}", ValueLevel.Value, dataLevel.FactorId);
                    if (!((ValueLevel.Value == string.Empty) || (ValueLevel.Value == "-" && level.SkipDefis)))
                    {
                        var dictionaryRecord =  Dictionary.Find(x => CompareDictToValue(x,ValueLevel));

                        if (dictionaryRecord != null)
                        {
                            string code = dictionaryRecord.Code.Replace(" ", "");
                            if (code != "0")
                            {
                                Code = code;
                                dataLevel.Code = code;

								if (ValueLevel.IdDocument != null)
                                {
                                    OMInstance doc = OMInstance.Where(x => x.Id == ValueLevel.IdDocument.Value).SelectAll().ExecuteFirstOrDefault();
                                    if (doc != null)
                                    {
                                        Source = doc.Description;
                                        DocId = doc.Id;
                                    }
                                }
                            }
                        }
                        else
                        {
                            errorCOD = true;
                            errorCODStr += (unit.CadastralNumber + ": " + ValueLevel.AttributeName + ". Значение: " + ValueLevel.Value + " отсутствует в классификаторе" + Environment.NewLine);
                        }
                    }

                }
                else
                {
                    Code = ValueLevel.Value.Replace(" ", "");
                    dataLevel.Code = Code;
					if (ValueLevel.IdDocument != null)
                    {
                        OMInstance doc = OMInstance.Where(x => x.Id == ValueLevel.IdDocument.Value).SelectAll().ExecuteFirstOrDefault();
                        if (doc != null)
                        {
                            Source = doc.Description;
                            DocId = doc.Id;
                        }
                    }
                }
            }

            return ValueLevel;
        }

        public void SetPriorityGroupForObject(GroupingSettings setting, List<ObjectModel.KO.OMCodDictionary> DictionaryItem, ObjectModel.Gbu.OMMainObject obj, 
	        DateTime dateActual, GbuReportService reportService, Dictionary<long, long> dicColumns)
        {
	        GbuReportService.Row currentRow;
	        lock (PriorityGrouping.locked)
	        {
		        currentRow = reportService.GetCurrentRow();
		        PriorityGrouping.CurrentCount++;
		        reportService.AddValue(obj.CadastralNumber, PriorityGrouping.KnColumn, currentRow);
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
            {
                Level1 = GetDataLevelForObject(setting.Level1, obj, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_01, ref Doc_Source_01, ref Doc_Id_01, out DataLevel dataLevel1);
                Level2 = GetDataLevelForObject(setting.Level2, obj, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_02, ref Doc_Source_02, ref Doc_Id_02, out DataLevel dataLevel2);
                Level3 = GetDataLevelForObject(setting.Level3, obj, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_03, ref Doc_Source_03, ref Doc_Id_03, out DataLevel dataLevel3);
                Level4 = GetDataLevelForObject(setting.Level4, obj, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_04, ref Doc_Source_04, ref Doc_Id_04, out DataLevel dataLevel4);
                Level5 = GetDataLevelForObject(setting.Level5, obj, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_05, ref Doc_Source_05, ref Doc_Id_05, out DataLevel dataLevel5);
                Level6 = GetDataLevelForObject(setting.Level6, obj, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_06, ref Doc_Source_06, ref Doc_Id_06, out DataLevel dataLevel6);
                Level7 = GetDataLevelForObject(setting.Level7, obj, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_07, ref Doc_Source_07, ref Doc_Id_07, out DataLevel dataLevel7);
                Level8 = GetDataLevelForObject(setting.Level8, obj, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_08, ref Doc_Source_08, ref Doc_Id_08, out DataLevel dataLevel8);
                Level9 = GetDataLevelForObject(setting.Level9, obj, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_09, ref Doc_Source_09, ref Doc_Id_09, out DataLevel dataLevel9);
                Level10 = GetDataLevelForObject(setting.Level10, obj, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_10, ref Doc_Source_10, ref Doc_Id_10, out DataLevel dataLevel10);
                Level11 = GetDataLevelForObject(setting.Level11, obj, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_11, ref Doc_Source_11, ref Doc_Id_11, out DataLevel dataLevel11);

                try {
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

                            AddValueFactor(obj.Id, setting.IdAttributeResult, id_doc, dateActual, resGroup);

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
								    AddValueFactor(obj.Id, setting.IdAttributeSource, null, dateActual, strsource);
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
                                AddValueFactor(obj.Id, setting.IdAttributeDocument, null, dateActual, strsource);
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
                catch (Exception ex)
                {
                    if(PriorityGrouping.CurrentCount < 10)
                        Serilog.Log.Logger.Warning("SetPriorityGroup", ex);
                }
            }
        }

        public void SetPriorityGroupForUnit(GroupingSettings setting, List<ObjectModel.KO.OMCodDictionary> DictionaryItem, ObjectModel.KO.OMUnit unit, DateTime dateActual,
	        GbuReportService reportService, Dictionary<long, long> dicColumns)
        {
	        GbuReportService.Row currentRow;
	        lock (PriorityGrouping.locked)
	        {
		        PriorityGrouping.CurrentCount++;
		        currentRow = reportService.GetCurrentRow();
		        reportService.AddValue(unit.CadastralNumber, PriorityGrouping.KnColumn, currentRow);
            }

	        if (unit.ObjectId != null)
            {
                #region Поля
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
                #endregion

                {
                    Level1 = GetDataLevelForUnit(setting.Level1, unit, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_01, ref Doc_Source_01, ref Doc_Id_01, out DataLevel dataLevel1);
                    Level2 = GetDataLevelForUnit(setting.Level2, unit, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_02, ref Doc_Source_02, ref Doc_Id_02, out DataLevel dataLevel2);
                    Level3 = GetDataLevelForUnit(setting.Level3, unit, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_03, ref Doc_Source_03, ref Doc_Id_03, out DataLevel dataLevel3);
                    Level4 = GetDataLevelForUnit(setting.Level4, unit, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_04, ref Doc_Source_04, ref Doc_Id_04, out DataLevel dataLevel4);
                    Level5 = GetDataLevelForUnit(setting.Level5, unit, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_05, ref Doc_Source_05, ref Doc_Id_05, out DataLevel dataLevel5);
                    Level6 = GetDataLevelForUnit(setting.Level6, unit, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_06, ref Doc_Source_06, ref Doc_Id_06, out DataLevel dataLevel6);
                    Level7 = GetDataLevelForUnit(setting.Level7, unit, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_07, ref Doc_Source_07, ref Doc_Id_07, out DataLevel dataLevel7);
                    Level8 = GetDataLevelForUnit(setting.Level8, unit, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_08, ref Doc_Source_08, ref Doc_Id_08, out DataLevel dataLevel8);
                    Level9 = GetDataLevelForUnit(setting.Level9, unit, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_09, ref Doc_Source_09, ref Doc_Id_09, out DataLevel dataLevel9);
                    Level10 = GetDataLevelForUnit(setting.Level10, unit, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_10, ref Doc_Source_10, ref Doc_Id_10, out DataLevel dataLevel10);
                    Level11 = GetDataLevelForUnit(setting.Level11, unit, dateActual, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_11, ref Doc_Source_11, ref Doc_Id_11, out DataLevel dataLevel11);

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

                            AddValueFactor(unit.ObjectId.Value, setting.IdAttributeResult, id_doc, dateActual, resGroup);

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
									AddValueFactor(unit.ObjectId.Value, setting.IdAttributeSource, null, dateActual, strsource);
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
                                AddValueFactor(unit.ObjectId.Value, setting.IdAttributeDocument, null, dateActual, strsource);
                            }
                           
							lock (PriorityGrouping.locked)
                            {
	                            reportService.AddValue(errorCODStr, PriorityGrouping.ErrorColumn, currentRow);

                                PriorityGrouping.SuccessCount++;
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
            }
        }
        #endregion
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
        public static long SetPriorityGroup(GroupingSettings setting)
        {
			var reportService =  new GbuReportService();
			var dataHeaderAndColumnNumber = GenerateReportHeaderWithColumnNumber(setting);
          
            _log.Debug("Заголовки отчета и номера столбцов ${DictionaryColumns} ${Headers}", dataHeaderAndColumnNumber.DictionaryColumns, dataHeaderAndColumnNumber.Headers);
 
            reportService.AddHeaders(dataHeaderAndColumnNumber.Headers);
			long reportId = 0;

	        ErrorMessages = new List<string>();
			locked = new object();
            PrioritetList = new PriorityGroupList();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };

            List<ObjectModel.KO.OMCodDictionary> DictionaryItem = new List<ObjectModel.KO.OMCodDictionary>();
            if (setting.IdCodJob != null)
                DictionaryItem = ObjectModel.KO.OMCodDictionary.Where(x => x.IdCodjob == setting.IdCodJob).SelectAll().Execute();

            bool useTask = false;
            if (setting.TaskFilter != null) useTask = setting.TaskFilter.Count > 0;

            var userId = SRDSession.GetCurrentUserId().Value;

            if (useTask)
            {
                List<ObjectModel.KO.OMUnit> Objs = new List<ObjectModel.KO.OMUnit>();
                foreach (long taskId in setting.TaskFilter)
                {
                    Objs.AddRange(ObjectModel.KO.OMUnit.Where(x => x.PropertyType_Code == PropertyTypes.Stead && x.TaskId == taskId && x.ObjectId != null).SelectAll().Execute());
                }
                MaxCount = Objs.Count;
				CurrentCount = 0;

                _log.ForContext("useTask", useTask)
                    .ForContext("Objs_0", JsonConvert.SerializeObject(Objs[0]))
                    .Debug("Выполнение операции группировки по Задачам на  оценку. Всего {Count} объектов", MaxCount);

                Parallel.ForEach(Objs, options, item =>
                {
                    SetThreadCurrentPrincipal(userId);

                    try
                    {
                        new PriorityItem().SetPriorityGroupForUnit(setting, DictionaryItem, item, (item.CreationDate == null) ? DateTime.Now.Date : item.CreationDate.Value.Date, reportService, dataHeaderAndColumnNumber.DictionaryColumns);
                    }
                    catch (Exception ex)
                    {
                        _log.Error("Ошибка группировки по КН {CadastralNumber}", item.CadastralNumber);
                        LogContext.PushProperty("Param", JsonConvert.SerializeObject(item));
                        ErrorManager.LogError(ex);
                    }
                });

                Objs.Clear();
            }
			else
            {
                List<ObjectModel.Gbu.OMMainObject> Objs = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == PropertyTypes.Stead).SelectAll().Execute();
                MaxCount = Objs.Count;
                CurrentCount = 0;

                _log.ForContext("useTask", useTask)
                    .ForContext("Objs_0", JsonConvert.SerializeObject(Objs[0]))
                    .Debug("Выполнение операции группировки по Объектам ГБУ. Всего {Count} объектов", MaxCount);

                Parallel.ForEach(Objs, options, item =>
                {
                    SetThreadCurrentPrincipal(userId);

                    try
                    {
                        new PriorityItem().SetPriorityGroupForObject(setting, DictionaryItem, item, (setting.DateActual == null) ? DateTime.Now.Date : setting.DateActual.Value.Date, reportService, dataHeaderAndColumnNumber.DictionaryColumns);
                    }
                    catch (Exception ex)
                    {
                        _log.Error("Ошибка группировки по КН {CadastralNumber}", item.CadastralNumber);
                        LogContext.PushProperty("Param", JsonConvert.SerializeObject(item));
                        ErrorManager.LogError(ex);
                    }
                });

                Objs.Clear();
            }

            try
            {
                _log.Verbose("Применение стилей SetStyle");
                reportService.SetStyle();
                reportService.SetIndividualWidth(KnColumn, 4);
                reportService.SetIndividualWidth(ResultColumn, 6);
                reportService.SetIndividualWidth(ValueColumn, 3);
                reportService.SetIndividualWidth(SourceColumn, 6);
                reportService.SetIndividualWidth(ErrorColumn, 5);

                foreach (var dictionaryColumn in dataHeaderAndColumnNumber.DictionaryColumns)
                {
                    reportService.SetIndividualWidth((int)dictionaryColumn.Value, 3);
                    reportService.SetIndividualWidth((int)dictionaryColumn.Value + 1, 3);
                }
                reportId = reportService.SaveReport("Отчет нормализации");
                return reportId;
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Форматирование и сохранение отчета завершилось с ошибкой");
                throw;
            }
        }

        public static ReportHeaderWithColumnDic GenerateReportHeaderWithColumnNumber(GroupingSettings setting)
        {
			List<string> resHeaderList = new List<string>{ "КН", "Поле в которое вносилось значение", "Внесенное значение", "Источник внесенного значения", "Ошибка" };
			ReportHeaderWithColumnDic res = new ReportHeaderWithColumnDic();

			var dicColumns = new Dictionary<long, long>();
			int lastColumn = ErrorColumn;
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
                        
                       /// Serilog.Context.LogContext.PushProperty("lItem.IdFactor", lItem.IdFactor);
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
}
