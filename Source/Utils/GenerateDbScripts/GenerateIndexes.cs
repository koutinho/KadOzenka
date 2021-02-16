using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace GenerateDbScripts
{

    class GenerateIndexes
    {

        private string Data { get; set; }
        private string SQLRequest { get; set; }
        private string ConnectionString { get; set; }
        private string Template { get; set; }
        private string Path { get; set; }

        public GenerateIndexes(string connectionString, string request, string template, string savePath, string fileName)
        {
            SQLRequest = request;
            ConnectionString = connectionString;
            Template = template;
            Path = $@"{savePath}\{fileName}";
        }

        public void Generate()
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(SQLRequest, connection))
                {
                    var reader = command.ExecuteReader();
                    int counter = 0;
                    while (reader.Read())
                    {
                        Console.WriteLine($"Экспорт индекса: {reader[0]}");
                        Data += string.Format(Template, reader[0].ToString().Any(char.IsUpper) ? $"\"{reader[0]}\"" : reader[0], ++counter, reader[1], reader[2]);
                    }
                }
                connection.Close();
            }
            Console.WriteLine("Экспорт индексов закончен");
            Save();
        }

        private void Save() => File.WriteAllText(Path, Data);

    }

}
