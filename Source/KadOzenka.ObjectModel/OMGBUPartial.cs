using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using Core.Shared.Misc;
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
    public struct ValueItem
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
        /// Список заданий на оценку
        /// </summary>
        public List<long> TaskFilter;

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
        /// <summary>
        /// Дата на которую делается группировка 
        /// </summary>
        public DateTime? DateActual;
    }

    #endregion
}

namespace ObjectModel.Gbu.Harmonization
{
    public struct AdditionalLevelsForHarmonization
    {
        public int LevelNumber { get; set; }
        public long? AttributeId { get; set; }
    }

    /// <summary>
    /// Настройки простой гармонизации
    /// </summary>
    public abstract class ABaseHarmonizationSettings
    {
        /// <summary>
        /// Идентификатор атрибута, куда будет записан результат 
        /// </summary>
        public long IdAttributeResult;
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
        /// Список заданий на оценку
        /// </summary>
        public List<long> TaskFilter;

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
        /// <summary>
        /// Факторы, добавленные юзером
        /// </summary>
        public List<AdditionalLevelsForHarmonization> AdditionalLevels;
        /// <summary>
        /// Дата на которую делается гармонизация 
        /// </summary>
        public DateTime? DateActual;
    }

    /// <summary>
    /// Настройки простой гармонизации
    /// </summary>
    public class HarmonizationSettings : ABaseHarmonizationSettings
    {
        
    }

    /// <summary>
    /// Настройки гармонизации с использованием справочника ЦОД
    /// </summary>
    public class HarmonizationCODSettings : ABaseHarmonizationSettings
    {
        /// <summary>
        /// Идентификатор задания ЦОД
        /// </summary>
        public long? IdCodJob;
        /// <summary>
        /// Значение по умолчанию 
        /// </summary>
        public string DefaultValue;
        /// <summary>
        /// Документ для значения по умолчанию 
        /// </summary>
        public long? IdDocument;
    }
}

namespace ObjectModel.Gbu.CodSelection
{
    /// <summary>
    /// Настройки выборки из справочника ЦОД
    /// </summary>
    public struct CodSelectionSettings
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
        /// Документ 
        /// </summary>
        public long? IdDocument;

    }
}

namespace ObjectModel.Gbu.ExportAttribute
{
    /// <summary>
    /// Соответствие атрибутов ГБУ и КО
    /// </summary>
    public struct ExportAttributeItem
    {
        /// <summary>
        /// Идентификатор фактора КО
        /// </summary>
        public long IdAttributeKO;
        /// <summary>
        /// Идентификатор фактора ГБУ
        /// </summary>
        public long IdAttributeGBU;
    }

    /// <summary>
    /// Настройки переноса атрибутов из ГБУшной части в КОшную
    /// </summary>
    public struct GbuExportAttributeSettings
    {
        /// <summary>
        /// Список заданий на оценку
        /// </summary>
        public List<long> TaskFilter;

        /// <summary>
        /// Список сопоставленных атрибутов
        /// </summary>
        public List<ExportAttributeItem> Attributes;


    }
}

namespace ObjectModel.Gbu.InheritanceAttribute
{
    /// <summary>
    /// Настройки наследования атрибутов
    /// </summary>
    public struct GbuInheritanceAttributeSettings
    {
        /// <summary>
        /// Список заданий на оценку
        /// </summary>
        public List<long> TaskFilter;

        /// <summary>
        /// Тип наследования: Кадастровый квартал -> Земельный участок
        /// </summary>
        public bool CadastralBlockToParcel;
        /// <summary>
        /// Тип наследования: Кадастровый квартал -> Здание
        /// </summary>
        public bool CadastralBlockToBuilding;
        /// <summary>
        /// Тип наследования: Кадастровый квартал -> Сооружение
        /// </summary>
        public bool CadastralBlockToConstruction;
        /// <summary>
        /// Тип наследования: Кадастровый квартал -> Объект незавершенного строительства
        /// </summary>
        public bool CadastralBlockToUncomplited;
        /// <summary>
        /// Тип наследования: Земельный участок -> Здание
        /// </summary>
        public bool ParcelToBuilding;
        /// <summary>
        /// Тип наследования: Земельный участок -> Сооружение
        /// </summary>
        public bool ParcelToConstruction;
        /// <summary>
        /// Тип наследования: Земельный участок -> Объект незавершенного строительства
        /// </summary>
        public bool ParcelToUncomplited;
        /// <summary>
        /// Тип наследования: Здание -> Помещение
        /// </summary>
        public bool BuildToFlat;



        /// <summary>
        /// Фактор, содержащий родительский кадастровый номер, по которому осуществляется сопоставление с родительским объектом
        /// </summary>
        public long ParentCadastralNumberAttribute;

        /// <summary>
        /// Список выбранных атрибутов
        /// </summary>
        public List<long> Attributes;


    }
}


