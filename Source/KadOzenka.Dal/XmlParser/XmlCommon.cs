using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using ObjectModel.Commission;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace KadOzenka.Dal.XmlParser
{
    public class ObjectCheckItem
    {
        public static bool Check(string old_value, string new_value)
        {
            return old_value.ToUpper() == new_value.ToUpper();
        }
        public static bool Check(double old_value, double new_value)
        {
            return old_value == new_value;
        }
        public static bool Check(List<xmlCodeName> old_value, List<xmlCodeName> new_value)
        {
            bool prFind = true;

            foreach (xmlCodeName oldItem in old_value)
            {
                bool findOld = false;
                foreach (xmlCodeName newItem in new_value)
                {
                    if (oldItem.Code == newItem.Code) findOld = true;
                }
                prFind &= findOld;
            }

            foreach (xmlCodeName newItem in new_value)
            {
                bool findNew = false;
                foreach (xmlCodeName oldItem in old_value)
                {
                    if (oldItem.Code == newItem.Code) findNew = true;
                }
                prFind &= findNew;
            }

            return prFind;
        }

    }
    public class recRecord
    {
        private string code;
        private string value;
        private object obj;

        public recRecord(string _code, string _value)
        {
            code = _code;
            value = _value;
            obj = null;
        }
        public recRecord(string _code, string _value, object _obj)
        {
            code = _code;
            value = _value;
            obj = _obj;
        }

        public string Code { get { return code; } }
        public string Value { get { return value; } }
        public object Object { get { return obj; } }
    }
    public class recArray
    {
        public string NameDictionary;

        private List<recRecord> records;

        public recArray()
        {
            records = new List<recRecord>();
            records.Clear();
            NameDictionary = "Справочник";
        }

        public string GetValueByCode(string code)
        {
            foreach (recRecord rec in records)
            {
                if (rec.Code == code) return rec.Value;
            }
            return String.Empty;
        }
        public recRecord GetRecByCode(string code)
        {
            foreach (recRecord rec in records)
            {
                if (rec.Code == code) return rec;
            }
            return new recRecord("", "");
        }
        public recRecord GetRecByCode(string code, string _default)
        {
            foreach (recRecord rec in records)
            {
                if (rec.Code == code) return rec;
            }
            return new recRecord(code, _default);
        }

        public recRecord GetRecByValue(string value)
        {
            foreach (recRecord rec in records)
            {
                if (rec.Value == value) return rec;
            }
            return new recRecord("", "");
        }

        public List<recRecord> Records { get { return records; } set { records = value; } }

        public void Add(string code, string value)
        {
            Records.Add(new recRecord(code, value));
        }

        public void Clear()
        {
            Records.Clear();
        }

    }
    public class xsdDictionary
    {
        public recArray Records;
        public xsdDictionary(string file, string simpletype)
        {
            Records = new recArray();
            XmlDocument _shema;
            _shema = new XmlDocument();
            _shema.Load(file);
            XmlNodeList xnBuildings = _shema.GetElementsByTagName("xs:simpleType");
            foreach (XmlNode xnBuilding in xnBuildings)
            {
                if (xnBuilding.Attributes["name"] != null)
                {
                    if (xnBuilding.Attributes["name"].Value == simpletype)
                    {
                        foreach (XmlNode xnChild in xnBuilding.ChildNodes)
                        {
                            if (xnChild.Name == "xs:annotation")
                            {
                                Records.NameDictionary = GetData(xnChild);
                            }
                            if (xnChild.Name == "xs:restriction")
                            {
                                foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                                {
                                    GetRecordData(xnChild1);
                                }
                            }
                        }
                    }
                }
            }
        }
        public string GetData(XmlNode xnNode)
        {
            string res = string.Empty;
            foreach (XmlNode xnChild in xnNode.ChildNodes)
            {
                if (xnChild.Name == "xs:documentation")
                {
                    res = xnChild.InnerText;
                }
            }
            return res;
        }
        public void GetRecordData(XmlNode xnNode)
        {
            if (xnNode.Name == "xs:enumeration")
            {
                string valuecode = xnNode.Attributes["value"].Value;
                string res = string.Empty;
                foreach (XmlNode xnChild in xnNode.ChildNodes)
                {
                    if (xnChild.Name == "xs:annotation")
                    {
                        res = GetData(xnChild);
                    }
                }
                Records.Add(valuecode, res);
            }
        }
    }
    public enum enTypeObject : int
    {
        toBuilding = 1,
        toConstruction = 2,
        toFlat = 3,
        toCarPlace = 4,
        toUncomplited = 5,
        toParcel = 6
    }
    public class xmlCodeName
    {
        public string Name;
        public string Code;

        public static string GetNames(List<xmlCodeName> items)
        {
            string res = string.Empty;
            foreach(xmlCodeName item in items)
            {
                res += item.Name + "; ";
            }
            return res.Trim().TrimEnd(';');
        }
        public static string GetNames(List<string> items)
        {
            string res = string.Empty;
            foreach (string item in items)
            {
                res += item + "; ";
            }
            return res.Trim().TrimEnd(';');
        }

    }
    public class xmlCodeNameValue
    {
        public string Name;
        public string Code;
        public string Value;

        public static string GetNames(List<xmlCodeNameValue> items)
        {
            string res = string.Empty;
            foreach (xmlCodeNameValue item in items)
            {
                res += item.Name+": "+item.Value + "; ";
            }
            return res.Trim().TrimEnd(';');
        }

    }
    public class xmlNumberDate
    {
        public string Number;
        public DateTime Date;
    }
}
