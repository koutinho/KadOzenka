using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GenerateDbScripts
{

    class GenerateFunctions
    {

        private string Data { get; set; }
        private string SQLRequest { get; set; }
        private string ConnectionString { get; set; }
        private string Template { get; set; }
        private string Path { get; set; }

        public GenerateFunctions(string connectionString, string username, string request, string template, string savePath, string fileName)
        {
            SQLRequest = string.Format(request, username);
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
                        Console.WriteLine($"Экспорт функции: {reader[0]}");
                        Data += string.Format(Template, reader[0], ++counter, reader[1].ToString().TrimEnd('\n'));
                    }
                }
                connection.Close();
            }
            Console.WriteLine("Экспорт функций закончен");
            Save();
        }

        private void Save() => File.WriteAllText(Path, Data);

    }

}
