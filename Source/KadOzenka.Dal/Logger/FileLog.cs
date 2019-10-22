using System;
using System.IO;
using System.Text;
using System.Configuration;

namespace KadOzenka.Dal.Logger
{

    class FileLog
    {

        public static void StartLogger() => File.AppendAllText(ConfigurationManager.AppSettings["LogerPath"], $"\n\n[{DateTime.Now}]:");

        public static void LogError(string info, string error) => File.AppendAllText(ConfigurationManager.AppSettings["LogerPath"], $"\n{info}:{error}");

    }

}
