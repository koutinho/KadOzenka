using System;
using System.IO;
using System.Text;
using KadOzenka.Dal.Api.Models;
using Newtonsoft.Json;
using Serilog;

namespace KadOzenka.Dal.Api.Service
{
	public class ConfigService
	{
		private ILogger _log;

		private string _envPath;
		public ConfigService(ILogger logger,string env)
		{
			_log = logger;
			_envPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + $"appsettings.{env}.json";
		}
		private readonly string _rootPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "appsettings.json";

		public ConfigDto GetSerilogConfig()
		{
			var file = File.ReadAllText(_rootPath);
			var config = JsonConvert.DeserializeObject<dynamic>(file);
			var serilogConfig = config?.Serilog;
			if (serilogConfig == null)
			{
				throw new Exception("Конфигурация для Серилога не найдена");
			}

			var res = new ConfigDto();
			res.RootConfig = JsonConvert.SerializeObject(serilogConfig);

			if (File.Exists(_envPath))
			{
				var fileEnv = File.ReadAllText(_envPath);
				var configEnv = JsonConvert.DeserializeObject<dynamic>(fileEnv);
				var serilogConfigEnv = configEnv?.Serilog;

				if (serilogConfigEnv != null)
				{
					res.EnvConfig = JsonConvert.SerializeObject(serilogConfigEnv);
				}
			}


			return res;
		}

		public void SetSerilogConfig(ConfigDto configDto)
		{
			var file = File.ReadAllText(_rootPath);
			var config = JsonConvert.DeserializeObject<dynamic>(file);
			if (config?.Serilog == null)
			{
				throw new Exception("Конфигурация для Серилога не найдена");
			}
			config.Serilog = configDto.RootConfig;
			File.WriteAllText(_rootPath, string.Empty);

			using (FileStream fs = File.OpenWrite(_rootPath))
			{
				var str = JsonConvert.SerializeObject(config);
				byte[] bytes = Encoding.UTF8.GetBytes(str);

				if (fs.CanWrite)
				{
					fs.Write(bytes);
				}
			}

			if (File.Exists(_envPath))
			{
				var fileEnv = File.ReadAllText(_envPath);
				var configEnv = JsonConvert.DeserializeObject<dynamic>(fileEnv);
				if (configEnv?.Serilog != null)
				{
					configEnv.Serilog = configDto.EnvConfig;
					File.WriteAllText(_envPath, string.Empty);

					using (FileStream fs = File.OpenWrite(_rootPath))
					{
						var str = JsonConvert.SerializeObject(configEnv);
						byte[] bytes = Encoding.UTF8.GetBytes(str);

						if (fs.CanWrite)
						{
							fs.Write(bytes);
						}
					}

				}

			}

		}
	}
}