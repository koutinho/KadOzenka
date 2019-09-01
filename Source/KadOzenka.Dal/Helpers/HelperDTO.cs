using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace CIPJS.DAL.Helpers
{
    public static class HelperDto
    {
        public static List<T> GetModelsByDataReader<T>(this DataTable dt) where T : new()
        {
            var result = new List<T>(dt.Rows.Count);

            foreach (DataRow row in dt.Rows)
            {
                var item = new T();

                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    var nameField = dt.Columns[i].ColumnName;
                    var valueField = row[nameField];

                    if (valueField != DBNull.Value)
                    {
                        var property = typeof(T).GetProperties().Where(x => x.Name.ToUpper() == nameField.ToUpper()).FirstOrDefault();

                        if (property != null)
                        {
                            var value = Convert.ChangeType(valueField, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                            if (property.GetSetMethod() != null) property.SetValue(item, value, null);
                        }
                    }
                }

                result.Add(item);
            }

            return result;
        }
    }
}
