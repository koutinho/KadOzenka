namespace KadOzenka.Dal.DataExport
{
    public class CheckNullEmpty
    {
        public static string CheckStringOut(string value)
        {
            return string.IsNullOrEmpty(value) ? "-" : value;
        }

        public static string CheckString(string value)
        {
            return string.IsNullOrEmpty(value) ? "" : value;
        }

        public static decimal? CheckDecimal(decimal? value)
        {
            return (value == null) ? 0 : value;
        }
    }
}