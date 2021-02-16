using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using System.Linq;
using System.Configuration;

namespace Parser
{

    class PG
    {

        private string ConnectionString { get; set; }

        public PG(string connectionString) =>
            ConnectionString = connectionString;

        public List<T> ReadHash<T>(string tableName) where T : new()
        {
            List<T> result = new List<T>();
            using(NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                string request = $"SELECT * FROM {tableName};";
                NpgsqlCommand command = new NpgsqlCommand(request, connection);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T resObj = new T();
                        resObj.GetType().GetProperties().ToList().ForEach(x => 
                        {
                            if(x.PropertyType == typeof(Int64)) resObj.GetType().GetProperty(x.Name).SetValue(resObj, reader[x.Name]);
                            else if (x.PropertyType == typeof(Int64?)) resObj.GetType().GetProperty(x.Name).SetValue(resObj, reader[x.Name] != DBNull.Value ? reader[x.Name] : null);
                            else if (x.PropertyType == typeof(string)) resObj.GetType().GetProperty(x.Name).SetValue(resObj, reader[x.Name] != DBNull.Value ? reader[x.Name] : null);
                            else if (x.PropertyType == typeof(decimal)) resObj.GetType().GetProperty(x.Name).SetValue(resObj, reader[x.Name] != DBNull.Value ? reader[x.Name] : null);
                            else if (x.PropertyType == typeof(decimal?)) resObj.GetType().GetProperty(x.Name).SetValue(resObj, reader[x.Name] != DBNull.Value ? reader[x.Name] : null);
                        });
                        result.Add(resObj);
                    }
                }
                connection.Close();
            }
            return result;
        }

        public void UpdateValue(string tableName, List<KeyValuePair<string, string>> listOfParameters, List<KeyValuePair<string, string>> listOfConditions)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                string request = $"UPDATE {tableName} SET {string.Join(", ", listOfParameters.Select(x => $"{x.Key}='{x.Value}'"))} WHERE {string.Join(" AND ", listOfConditions.Select(x => $"{x.Key}={x.Value}"))}";
                //Console.WriteLine(request);
                NpgsqlCommand command = new NpgsqlCommand(request, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }

}
