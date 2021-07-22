using Core.Register;

namespace KadOzenka.Web.Helpers
{
    public static class Consts
    {
        public static string ExcelContentType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public static long IntegerAttributeType => (long) RegisterAttributeType.INTEGER;
        public static long DecimalAttributeType => (long) RegisterAttributeType.DECIMAL;
        public static long StringAttributeType => (long) RegisterAttributeType.STRING;
        public static long DateAttributeType => (long) RegisterAttributeType.DATE;
        public static long BoolAttributeType => (long) RegisterAttributeType.BOOLEAN;
    }
}
