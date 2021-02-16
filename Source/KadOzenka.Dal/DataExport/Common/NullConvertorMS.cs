using System;

namespace KadOzenka.Dal.DataExport
{
    public class NullConvertorMS
    {
        public static string DTtoSTRDOC(DateTime value)
        {
            if (value <= Convert.ToDateTime("01.01.1901")) return String.Empty;
            else return " от " + value.ToString("dd.MM.yyyy");
        }

        public static object StringToDB(string value)
        {
            if (value == null)
                return DBNull.Value;
            if (value.Equals(String.Empty))
                return DBNull.Value;
            return value;
        }

        public static string ToString(object value)
        {
            return (value == null) ? String.Empty : value.ToString();
        }

        public static string ToStringOrNull(object value)
        {
            return ((value == null) || (value == DBNull.Value)) ? null : value.ToString();
        }

        public static string ToString(string value)
        {
            return (value == null) ? String.Empty : value;
        }

        public static void SwapString(ref string sold, ref string snew)
        {
            if (snew == null)
                snew = sold;
        }

        public static void SwapDouble(ref double dold, ref double dnew)
        {
            if (dnew == double.MinValue)
                dnew = dold;
        }

        public static void SwapInt(ref Int32 dold, ref Int32 dnew)
        {
            if (dnew == Int32.MinValue)
                dnew = dold;
        }

        public static object IntIdToDB(int value)
        {
            if (value < 0)
                return DBNull.Value;
            else
                return value;
        }

        public static object IntIdToDB(Int64 value)
        {
            if (value < 0)
                return DBNull.Value;
            else
                return value;
        }

        public static object Int64ToDB(Int64 value)
        {
            if (value < 0)
                return DBNull.Value;
            else
                return value;
        }

        public static object IntToSTR(int value)
        {
            if (value == int.MinValue)
                return String.Empty;
            else
                return value.ToString();
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
            else if (Convert.ToInt32(value) > 0) return true;
            else
                return false;
        }

        public static byte[] DBToByteArray(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return null;
            else
                return (byte[]) (value);
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
                return -1;
            else
                return Convert.ToInt64(value);
        }

        public static object DoubleToDB(double value)
        {
            if (value == double.MinValue)
                return DBNull.Value;
            else
                return value;
        }

        public static object DoubleToObject(double value)
        {
            if (value == double.MinValue)
                return "-";
            else
                return value;
        }

        public static double DBToDouble(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return double.MinValue;
            else
                return Convert.ToDouble(value);
        }

        public static decimal DBToDecimal(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return decimal.MinValue;
            else
                return Convert.ToDecimal(value);
        }

        public static string DBDoubleNullToString(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return String.Empty;
            else
                return Convert.ToDouble(value).ToString("0.##");
        }

        public static object DateTimeToDB(DateTime value)
        {
            if (value <= Convert.ToDateTime("01.01.1901"))
                return DBNull.Value;
            else
                return value;
        }

        public static DateTime DBToDateTime(object value)
        {
            if ((value == null) || (value == DBNull.Value))
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(value);
        }

        public static object IdNameToDB(int id, string name)
        {
            if (id != int.MinValue)
                return DBNull.Value;
            else
            {
                return name;
            }
        }

        public static string DTtoSTR(DateTime value)
        {
            if (value <= Convert.ToDateTime("01.01.1901")) return String.Empty;
            else return value.ToString("dd.MM.yyyy");
        }

        public static string DTtoSTRTime(DateTime value)
        {
            if (value <= Convert.ToDateTime("01.01.1901")) return String.Empty;
            else return value.ToString("dd.MM.yyyy") + " " + value.ToLongTimeString();
        }

        public static string DoubletoSTR(double value)
        {
            if (value == double.MinValue) return String.Empty;
            else return value.ToString();
        }

        public static string PriceToSTR(double value)
        {
            if (value == double.MinValue) return String.Empty;
            else return value.ToString("0.00");
        }

        public static double PriceToDouble(double value)
        {
            if (value == double.MinValue) return 0;
            else return Convert.ToDouble(value.ToString("0.00"));
        }

        public static string DecimaltoSTR(decimal? value)
        {
            if (value == decimal.MinValue) return String.Empty;
            else return value.ToString();
        }

        public static string BooltoSTR(bool value)
        {
            if (!value) return String.Empty;
            else return "Да";
        }
    }
}