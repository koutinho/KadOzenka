using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Parser
{

    class DP
    {

        public string ToString<T>(T obj) where T : new()=>
            string.Join("\n", new T().GetType().GetProperties().ToList().Select(x =>
            {
                if (x.PropertyType == typeof(Int64)) return $"{x.Name}: {x.GetValue(obj, null)};";
                else if (x.PropertyType == typeof(Int64?)) return $"{x.Name}: {x.GetValue(obj, null) ?? "-"};";
                else if (x.PropertyType == typeof(string)) return $"{x.Name}: {x.GetValue(obj, null) ?? "-"};";
                else if (x.PropertyType == typeof(decimal)) return $"{x.Name}: {x.GetValue(obj, null)};";
                else if (x.PropertyType == typeof(decimal?)) return $"{x.Name}: {x.GetValue(obj, null) ?? "-"};";
                return $"Error! No such type {x.PropertyType}";
            }));

    }

}
