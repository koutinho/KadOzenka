using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using ObjectModel.Directory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectModel.Gbu.GroupingAlgoritm
{
    #region Приоритет для группировки
    struct ValueItem
    {
        public string Value { get; set; }
        public long? IdDocument { get; set; }
        public string AttributeName { get; set; }
    }
    /// <summary>
    /// Настройки уровня группировки
    /// </summary>
    public struct LevelItem
    {
        /// <summary>
        /// Признак использования классификатора ЦОД
        /// </summary>
        public bool UseDictionary { get; set; }
        /// <summary>
        /// Признак пропуска дефиса
        /// </summary>
        public bool SkipDefis { get; set; }
        /// <summary>
        /// Идентификатор аттрибута
        /// </summary>
        public long? IdFactor { get; set; }
    }
    public class PriorityRecordItem
    {
        public Int64 Id { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }

        public PriorityRecordItem(Int64 id, string code, int value)
        {
            Id = id;
            Code = code;
            Value = value;
        }

        public override string ToString()
        {
            return Code + " -> " + Value.ToString();
        }

    }
    public class PriorityRecordList
    {
        public string NameDictionary { get; set; }
        public long IdDictionary { get; set; }
        private List<PriorityRecordItem> _records;
        public PriorityRecordList()
        {
            _records = new List<PriorityRecordItem>();
            _records.Clear();
            NameDictionary = "Справочник";
        }
        public PriorityRecordList(Int64 id, string name)
        {
            _records = new List<PriorityRecordItem>();
            _records.Clear();
            NameDictionary = name;
            IdDictionary = id;
        }
        public int GetValueByCode(string code)
        {
            return GetRecByCode(code).Value;
        }
        public string GetCodeById(Int64 id)
        {
            return GetRecById(id).Code;
        }
        public int GetValueById(Int64 id)
        {
            return GetRecById(id).Value;
        }
        public PriorityRecordItem GetRecByCode(string code)
        {
            foreach (PriorityRecordItem rec in _records)
            {
                if (rec.Code == code) return rec;
            }
            return new PriorityRecordItem(-1, String.Empty, 0);
        }
        public PriorityRecordItem GetRecById(Int64 id)
        {
            foreach (PriorityRecordItem rec in _records)
            {
                if (rec.Id == id) return rec;
            }
            return new PriorityRecordItem(-1, String.Empty, 0);
        }
        public PriorityRecordItem[] GetRecordsByValue(int value)
        {
            List<PriorityRecordItem> res = new List<PriorityRecordItem>();
            foreach (PriorityRecordItem rec in _records)
            {
                if (rec.Value == value) res.Add(rec);
            }
            return res.ToArray();
        }
        public List<PriorityRecordItem> Records { get { return _records; } set { _records = value; } }
        public void Add(Int64 id, string code, int value)
        {
            Records.Add(new PriorityRecordItem(id, code, value));
        }
        public void Clear()
        {
            Records.Clear();
        }
    }
    public class PriorityGroupItem
    {
        public string Group { get; set; }
        public string Source { get; set; }
        public PriorityGroupItem(string group, string source)
        {
            Group = group;
            Source = source;
        }
        public static List<PriorityGroupItem> GetItem(string[] items, string source)
        {
            List<PriorityGroupItem> res = new List<PriorityGroupItem>();
            foreach (string item in items)
            {
                res.Add(new PriorityGroupItem(item, source));
            }
            return res;
        }
    }
    public class PriorityGroupList : PriorityRecordList
    {
        public PriorityGroupList()
        {
            this.Records = new List<PriorityRecordItem>();
            Records.Add(new PriorityRecordItem(66, "02:060", 66));
            Records.Add(new PriorityRecordItem(65, "02:010", 65));
            Records.Add(new PriorityRecordItem(64, "13:021", 64));
            Records.Add(new PriorityRecordItem(63, "13:011", 63));
            Records.Add(new PriorityRecordItem(62, "04:020", 62));
            Records.Add(new PriorityRecordItem(61, "04:060", 61));
            Records.Add(new PriorityRecordItem(60, "04:030", 60));
            Records.Add(new PriorityRecordItem(59, "04:080", 59));
            Records.Add(new PriorityRecordItem(58, "04:100", 58));
            Records.Add(new PriorityRecordItem(57, "04:095", 57));
            Records.Add(new PriorityRecordItem(56, "04:050", 56));
            Records.Add(new PriorityRecordItem(55, "04:010", 55));
            Records.Add(new PriorityRecordItem(54, "04:070", 54));
            Records.Add(new PriorityRecordItem(53, "09:021", 53));
            Records.Add(new PriorityRecordItem(52, "05:020", 52));
            Records.Add(new PriorityRecordItem(51, "09:020", 51));
            Records.Add(new PriorityRecordItem(50, "02:071", 50));
            Records.Add(new PriorityRecordItem(49, "03:020", 49));
            Records.Add(new PriorityRecordItem(48, "03:040", 48));
            Records.Add(new PriorityRecordItem(47, "09:030", 47));
            Records.Add(new PriorityRecordItem(46, "03:060", 46));
            Records.Add(new PriorityRecordItem(45, "05:010", 45));
            Records.Add(new PriorityRecordItem(44, "03:070", 44));
            Records.Add(new PriorityRecordItem(43, "03:090", 43));
            Records.Add(new PriorityRecordItem(42, "03:050", 42));
            Records.Add(new PriorityRecordItem(41, "03:030", 41));
            Records.Add(new PriorityRecordItem(40, "03:080", 40));
            Records.Add(new PriorityRecordItem(39, "12:010", 39));
            Records.Add(new PriorityRecordItem(38, "03:100", 38));
            Records.Add(new PriorityRecordItem(37, "08:030", 37));
            Records.Add(new PriorityRecordItem(36, "08:020", 36));
            Records.Add(new PriorityRecordItem(35, "08:040", 35));
            Records.Add(new PriorityRecordItem(34, "06:030", 34));
            Records.Add(new PriorityRecordItem(33, "06:090", 33));
            Records.Add(new PriorityRecordItem(32, "06:050", 32));
            Records.Add(new PriorityRecordItem(31, "06:040", 31));
            Records.Add(new PriorityRecordItem(30, "06:060", 30));
            Records.Add(new PriorityRecordItem(29, "06:020", 29));
            Records.Add(new PriorityRecordItem(28, "06:031", 28));
            Records.Add(new PriorityRecordItem(27, "06:111", 27));
            Records.Add(new PriorityRecordItem(26, "06:110", 26));
            Records.Add(new PriorityRecordItem(25, "06:080", 25));
            Records.Add(new PriorityRecordItem(24, "07:011", 24));
            Records.Add(new PriorityRecordItem(23, "07:024", 23));
            Records.Add(new PriorityRecordItem(22, "07:041", 22));
            Records.Add(new PriorityRecordItem(21, "07:012", 21));
            Records.Add(new PriorityRecordItem(20, "07:031", 20));
            Records.Add(new PriorityRecordItem(19, "07:010", 19));
            Records.Add(new PriorityRecordItem(18, "03:093", 18));
            Records.Add(new PriorityRecordItem(17, "06:071", 17));
            Records.Add(new PriorityRecordItem(16, "11:030", 16));
            Records.Add(new PriorityRecordItem(15, "07:051", 15));
            Records.Add(new PriorityRecordItem(14, "11:010", 14));
            Records.Add(new PriorityRecordItem(13, "03:010", 13));
            Records.Add(new PriorityRecordItem(12, "01:010", 12));
            Records.Add(new PriorityRecordItem(11, "12:030", 11));
            Records.Add(new PriorityRecordItem(10, "12:020", 10));
            Records.Add(new PriorityRecordItem(9, "08:011", 9));
            Records.Add(new PriorityRecordItem(8, "06:010", 8));
            Records.Add(new PriorityRecordItem(7, "05:031", 7));
            Records.Add(new PriorityRecordItem(6, "01:130", 6));
            Records.Add(new PriorityRecordItem(5, "10:010", 5));
            Records.Add(new PriorityRecordItem(4, "09:010", 4));
            Records.Add(new PriorityRecordItem(3, "12:003", 3));
            Records.Add(new PriorityRecordItem(2, "12:001", 2));
            Records.Add(new PriorityRecordItem(1, "14:000", 1));
        }
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
        private ValueItem GetValueFactor(ObjectModel.KO.OMUnit unit, long? idFactor, DateTime? date)
        {
            ValueItem res = new ValueItem
            {
                Value = string.Empty,
                IdDocument = null,
                AttributeName = string.Empty
            };
            return res;
        }
        private void AddValueFactor(ObjectModel.KO.OMUnit unit, long? idFactor, long? idDoc, DateTime? date, string value)
        {

        }
        private ValueItem GetDataLevel(LevelItem level, ObjectModel.KO.OMUnit unit, List<ObjectModel.KO.OMCodDictionary> Dictionary,
            ref string errorCODStr, ref bool errorCOD, ref string Code, ref string Source, ref long? DocId)
        {
            ValueItem ValueLevel = new ValueItem();
            if (level.IdFactor != null)
            {
                ValueLevel = GetValueFactor(unit, level.IdFactor, unit.CreationDate);
                if (level.UseDictionary)
                {
                    if (!((ValueLevel.Value == string.Empty) || (ValueLevel.Value == "-" && level.SkipDefis)))
                    {
                        ObjectModel.KO.OMCodDictionary dictionaryRecord = Dictionary.Find(x => x.Value == ValueLevel.Value);
                        if (dictionaryRecord != null)
                        {
                            string code = dictionaryRecord.Code.Replace(" ", "");
                            if (code != "0")
                            {
                                Code = code;
                                if (ValueLevel.IdDocument != null)
                                {
                                    Core.TD.OMInstance doc = Core.TD.OMInstance.Where(x => x.Id == ValueLevel.IdDocument.Value).SelectAll().ExecuteFirstOrDefault();
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
                    if (ValueLevel.IdDocument != null)
                    {
                        Core.TD.OMInstance doc = Core.TD.OMInstance.Where(x => x.Id == ValueLevel.IdDocument.Value).SelectAll().ExecuteFirstOrDefault();
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
        public void SetPriorityGroup(GroupingSettings setting, List<ObjectModel.KO.OMCodDictionary> DictionaryItem, ObjectModel.KO.OMUnit unit)
        {
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
                Level1 = GetDataLevel(setting.Level1, unit, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_01, ref Doc_Source_01, ref Doc_Id_01);
                Level2 = GetDataLevel(setting.Level2, unit, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_02, ref Doc_Source_02, ref Doc_Id_02);
                Level3 = GetDataLevel(setting.Level3, unit, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_03, ref Doc_Source_03, ref Doc_Id_03);
                Level4 = GetDataLevel(setting.Level4, unit, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_04, ref Doc_Source_04, ref Doc_Id_04);
                Level5 = GetDataLevel(setting.Level5, unit, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_05, ref Doc_Source_05, ref Doc_Id_05);
                Level6 = GetDataLevel(setting.Level6, unit, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_06, ref Doc_Source_06, ref Doc_Id_06);
                Level7 = GetDataLevel(setting.Level7, unit, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_07, ref Doc_Source_07, ref Doc_Id_07);
                Level8 = GetDataLevel(setting.Level8, unit, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_08, ref Doc_Source_08, ref Doc_Id_08);
                Level9 = GetDataLevel(setting.Level9, unit, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_09, ref Doc_Source_09, ref Doc_Id_09);
                Level10 = GetDataLevel(setting.Level10, unit, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_10, ref Doc_Source_10, ref Doc_Id_10);
                Level11 = GetDataLevel(setting.Level11, unit, DictionaryItem, ref errorCODStr, ref errorCOD, ref Code_Source_11, ref Doc_Source_11, ref Doc_Id_11);


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

                        AddValueFactor(unit, setting.IdAttributeResult, id_doc, DateTime.Now, resGroup);

                        if (setting.IdAttributeSource != null)
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
                            AddValueFactor(unit, setting.IdAttributeSource, null, DateTime.Now, strsource);
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
                            AddValueFactor(unit, setting.IdAttributeDocument, null, DateTime.Now, strsource);
                        }
                    }
                }
                #endregion
            }

        }
        #endregion

    }

    /// <summary>
    /// Настройки группировки
    /// </summary>
    public struct GroupingSettings
    {
        /// <summary>
        /// Идентификатор задания ЦОД
        /// </summary>
        public long? IdCodJob;

        /// <summary>
        /// Выборка по всем объектам
        /// </summary>
        public bool SelectAllObject;
        /// <summary>
        /// Идентификатор аттрибута - фильтра
        /// </summary>
        public long? IdAttributeFilter;
        /// <summary>
        /// Список значений фильтра
        /// </summary>
        public List<string> ValuesFilter;

        /// <summary>
        /// Настройки 1 уровня группировки
        /// </summary>
        public LevelItem Level1;
        /// <summary>
        /// Настройки 2 уровня группировки
        /// </summary>
        public LevelItem Level2;
        /// <summary>
        /// Настройки 3 уровня группировки
        /// </summary>
        public LevelItem Level3;
        /// <summary>
        /// Настройки 4 уровня группировки
        /// </summary>
        public LevelItem Level4;
        /// <summary>
        /// Настройки 5 уровня группировки
        /// </summary>
        public LevelItem Level5;
        /// <summary>
        /// Настройки 6 уровня группировки
        /// </summary>
        public LevelItem Level6;
        /// <summary>
        /// Настройки 7 уровня группировки
        /// </summary>
        public LevelItem Level7;
        /// <summary>
        /// Настройки 8 уровня группировки
        /// </summary>
        public LevelItem Level8;
        /// <summary>
        /// Настройки 9 уровня группировки
        /// </summary>
        public LevelItem Level9;
        /// <summary>
        /// Настройки 10 уровня группировки
        /// </summary>
        public LevelItem Level10;
        /// <summary>
        /// Настройки 11 уровня группировки
        /// </summary>
        public LevelItem Level11;

        /// <summary>
        /// Идентификатор атрибута, куда будет записан результат 
        /// </summary>
        public long? IdAttributeResult;
        /// <summary>
        /// Идентификатор атрибута, куда будут записаны источники 
        /// </summary>
        public long? IdAttributeSource;
        /// <summary>
        /// Идентификатор атрибута, куда будут записаны документы 
        /// </summary>
        public long? IdAttributeDocument;
    }

    /// <summary>
    /// Приоритет группировки
    /// </summary>
    public class PriorityGrouping
    {
        /// <summary>
        /// Справочник приоритетов
        /// </summary>
        public static PriorityGroupList PrioritetList = null;

        /// <summary>
        /// Выполнение операции группировки
        /// </summary>
        public static void SetPriorityGroup(GroupingSettings setting)
        {
            PrioritetList = new PriorityGroupList();

            List<ObjectModel.KO.OMCodDictionary> DictionaryItem = new List<KO.OMCodDictionary>();
            if (setting.IdCodJob != null)
                DictionaryItem = ObjectModel.KO.OMCodDictionary.Where(x => x.IdCodjob == setting.IdCodJob).SelectAll().Execute();
            List<ObjectModel.KO.OMUnit> units = ObjectModel.KO.OMUnit.Where(x => x.PropertyType_Code == PropertyTypes.Stead).SelectAll().Execute();

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 1
            };

            Parallel.ForEach(units, options, item => { new PriorityItem().SetPriorityGroup(setting, DictionaryItem, item); });
        }
    }
    #endregion
}
namespace ObjectModel.Gbu.Harmonization
{
    /// <summary>
    /// Настройки простой гармонизации
    /// </summary>
    public struct HarmonizationSettings
    {
        /// <summary>
        /// Идентификатор атрибута, куда будет записан результат 
        /// </summary>
        public long? IdAttributeResult;
        /// <summary>
        /// Тип объекта 
        /// </summary>
        public PropertyTypes PropertyType;

        /// <summary>
        /// Выборка по всем объектам
        /// </summary>
        public bool SelectAllObject;
        /// <summary>
        /// Идентификатор аттрибута - фильтра
        /// </summary>
        public long? IdAttributeFilter;
        /// <summary>
        /// Список значений фильтра
        /// </summary>
        public List<string> ValuesFilter;

        /// <summary>
        /// Фактор 1 уровня 
        /// </summary>
        public long? Level1Attribute;
        /// <summary>
        /// Фактор 2 уровня 
        /// </summary>
        public long? Level2Attribute;
        /// <summary>
        /// Фактор 3 уровня 
        /// </summary>
        public long? Level3Attribute;
        /// <summary>
        /// Фактор 4 уровня 
        /// </summary>
        public long? Level4Attribute;
        /// <summary>
        /// Фактор 5 уровня 
        /// </summary>
        public long? Level5Attribute;
        /// <summary>
        /// Фактор 6 уровня 
        /// </summary>
        public long? Level6Attribute;
        /// <summary>
        /// Фактор 7 уровня 
        /// </summary>
        public long? Level7Attribute;
        /// <summary>
        /// Фактор 8 уровня 
        /// </summary>
        public long? Level8Attribute;
        /// <summary>
        /// Фактор 9 уровня 
        /// </summary>
        public long? Level9Attribute;
        /// <summary>
        /// Фактор 10 уровня 
        /// </summary>
        public long? Level10Attribute;
    }
    /// <summary>
    /// Простая гармонизация
    /// </summary>
    public class Harmonization
    {
        /// <summary>
        /// Выполнение операции гармонизации
        /// </summary>
        public static void Run(HarmonizationSettings setting)
        {
            //TODO: реализацию надо перенести из старого комплекса
        }
    }

    /// <summary>
    /// Настройки гармонизации с использованием справочника ЦОД
    /// </summary>
    public struct HarmonizationCODSettings
    {
        /// <summary>
        /// Идентификатор задания ЦОД
        /// </summary>
        public long? IdCodJob;

        /// <summary>
        /// Идентификатор атрибута, куда будет записан результат 
        /// </summary>
        public long? IdAttributeResult;
        /// <summary>
        /// Тип объекта 
        /// </summary>
        public PropertyTypes PropertyType;

        /// <summary>
        /// Выборка по всем объектам
        /// </summary>
        public bool SelectAllObject;
        /// <summary>
        /// Идентификатор аттрибута - фильтра
        /// </summary>
        public long? IdAttributeFilter;
        /// <summary>
        /// Список значений фильтра
        /// </summary>
        public List<string> ValuesFilter;

        /// <summary>
        /// Простановка по кадастровому номеру
        /// </summary>
        public bool UseCadastralNumber;


        /// <summary>
        /// Фактор 1 уровня 
        /// </summary>
        public long? Level1Attribute;
        /// <summary>
        /// Фактор 2 уровня 
        /// </summary>
        public long? Level2Attribute;
        /// <summary>
        /// Фактор 3 уровня 
        /// </summary>
        public long? Level3Attribute;
        /// <summary>
        /// Фактор 4 уровня 
        /// </summary>
        public long? Level4Attribute;
        /// <summary>
        /// Фактор 5 уровня 
        /// </summary>
        public long? Level5Attribute;
        /// <summary>
        /// Фактор 6 уровня 
        /// </summary>
        public long? Level6Attribute;
        /// <summary>
        /// Фактор 7 уровня 
        /// </summary>
        public long? Level7Attribute;
        /// <summary>
        /// Фактор 8 уровня 
        /// </summary>
        public long? Level8Attribute;
        /// <summary>
        /// Фактор 9 уровня 
        /// </summary>
        public long? Level9Attribute;
        /// <summary>
        /// Фактор 10 уровня 
        /// </summary>
        public long? Level10Attribute;
    }
    /// <summary>
    /// Гармонизация с использованием справочника ЦОД
    /// </summary>
    public class HarmonizationCOD
    {
        /// <summary>
        /// Выполнение операции гармонизации
        /// </summary>
        public static void Run(HarmonizationCODSettings setting)
        {
            //TODO: реализацию надо перенести из старого комплекса
        }
    }

}
