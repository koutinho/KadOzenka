using Core.Register;
using Core.Shared.Extensions;
using DevExpress.CodeParser;
using ObjectModel.Core.Shared;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.GbuObject;


namespace KadOzenka.BlFrontEnd.ExportMSSQL
{
    public class NullConvertor
    {
        public static string ToString(object value)
        {
            return (value == null) ? String.Empty : value.ToString();
        }
        public static int DBToInt(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return int.MinValue;
            else
                return Convert.ToInt32(value);
        }
        public static bool DBToBoolean(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return false;
            else
                return Convert.ToString(value).ToUpper() == "ДА";
        }

        public static int DBToInt(object value, int _default)
        {
            if ((value == null) || (value == DBNull.Value))
                return _default;
            else
                return Convert.ToInt32(value);
        }
        public static Int64 DBToInt64(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return int.MinValue;
            else
                return Convert.ToInt64(value);
        }
        public static long? DBToProcent(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return null;
            else
            {
                string proc = Convert.ToString(value);
                if (proc != string.Empty) return proc.ParseToLongNullable();
                else
                    return null;
            }
        }

        public static double DBToDouble(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return double.MinValue;
            else
                if (value.ToString() == "-")
                return 0;
            else
                return Convert.ToDouble(value);
        }
        public static DateTime DBToDateTime(object value)
        {
            if ((value == null) || (value == DBNull.Value) || (value.ToString() == String.Empty))
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(value);
        }
        public static decimal DBToDecimal(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return decimal.MinValue;
            else
                if (value.ToString() == "-")
                return 0;
            else
                return Convert.ToDecimal(value);
        }
        public static decimal? DBToDecimalNull(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return null;
            else
                if (value.ToString() == "-")
                return null;
            else
                return Convert.ToDecimal(value);
        }
        public static bool ToString(object value, out string result)
        {
            result = ToString(value);
            return (result != null);
        }
        public static bool DBToInt(object value, out int result)
        {
            result = DBToInt(value);
            return (result != int.MinValue);
        }
        public static bool DBToInt64(object value, out long result)
        {
            result = DBToInt64(value);
            return (result != int.MinValue);
        }
        public static bool DBToDouble(object value, out double result)
        {
            result = DBToDouble(value);
            return (result != double.MinValue);
        }
        public static bool DBToDateTime(object value, out DateTime result)
        {
            result = DBToDateTime(value);
            return (result != DateTime.MinValue);
        }
        public static bool DBToDecimal(object value, out decimal result)
        {
            result = DBToDecimal(value);
            return (result != decimal.MinValue);
        }
    }
}
