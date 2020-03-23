using System;
using System.ComponentModel;

namespace KadOzenka.Dal.FastReports.Common
{
    public static class ReportHelper
    {
        public static long? GetEnumCodeByDescription<T>(string description) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an Enum type");

            long? result = null;
            var enumValues = System.Enum.GetValues(typeof(T));

            foreach (var enumValue in enumValues)
            {
                var fieldInfo = typeof(T).GetField(enumValue.ToString());
                var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (((DescriptionAttribute)attributes[0]).Description != description)
                    continue;

                result = (long)System.Enum.Parse(typeof(T), enumValue.ToString());
                break;
            }

            return result;
        }
    }
}
