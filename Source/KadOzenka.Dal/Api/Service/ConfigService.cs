using System;
using System.IO;
using System.Text;
using KadOzenka.Dal.Api.Models;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Api.Service
{
	public class ConfigService
	{
		private string rootPath = AppDomain.CurrentDomain.BaseDirectory + Path.PathSeparator + "appsettings.json";

		public ConfigDto GetSerilogConfig(string env)
		{
			var file = File.ReadAllText(rootPath);
			var config = JsonConvert.DeserializeObject<dynamic>(file);
			var serilogConfig = config?.Serilog;
			if (serilogConfig == null)
			{
				throw new Exception("Конфигурация для Серилога не найдена");
			}

			var res = new ConfigDto();
			res.RootConfig = JsonConvert.SerializeObject(serilogConfig);
			return res;
		}

		public void SetSerilogConfig(string env, ConfigDto configDto)
		{
			var file = File.ReadAllText(rootPath);
			var config = JsonConvert.DeserializeObject<dynamic>(file);
			if (config?.Serilog == null)
			{
				throw new Exception("Конфигурация для Серилога не найдена");
			}
			config.Serilog = configDto.RootConfig;
			File.WriteAllText(rootPath, string.Empty);

			using (FileStream fs = File.OpenWrite(rootPath))
			{
				var str = JsonConvert.SerializeObject(config);
				byte[] bytes = Encoding.UTF8.GetBytes(str);

				if (fs.CanWrite)
				{
					fs.Write(bytes);
				}
			}
		}
	}
}