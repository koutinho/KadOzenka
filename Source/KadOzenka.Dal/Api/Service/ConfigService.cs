using System;
using System.IO;
using System.Text;
using Core.Shared.Extensions;
using KadOzenka.Dal.Api.Enums;
using KadOzenka.Dal.Api.Models;
using Newtonsoft.Json;
using Serilog;

namespace KadOzenka.Dal.Api.Service
{
	public class ConfigService
	{
		private readonly ILogger _log;
		private readonly string _envPath;
		private readonly string _rootPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "appsettings.json";
		public ConfigService(ILogger logger,string env)
		{
			_log = logger;
			_envPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + $"appsettings.{env}.json";
		}

		#region serilog section

		public ConfigDto GetSerilogConfig()
		{
			return GetConfig(ConfigType.Serilog);
		}

		public void SetSerilogConfig(ConfigDto configDto)
		{
			SetConfig(configDto, ConfigType.Serilog);

		}

		#endregion

		#region core section

		public ConfigDto GetCoreConfig()
		{
			return GetConfig(ConfigType.Core);
		}

		public void SetCoreConfig(ConfigDto configDto)
		{
			SetConfig(configDto, ConfigType.Core);

		}

		#endregion

		#region ko section

		public ConfigDto GetKoConfig()
		{
			return GetConfig(ConfigType.KoConfig);
		}

		public void SetKoConfig(ConfigDto configDto)
		{
			SetConfig(configDto, ConfigType.KoConfig);

		}

		#endregion

		private ConfigDto GetConfig(ConfigType configType)
		{

			var file = File.ReadAllText(_rootPath);
			var config = JsonConvert.DeserializeObject<dynamic>(file);

			dynamic сonfigSection = null;
	
			switch (configType)
			{
				case ConfigType.Serilog: сonfigSection = config?.Serilog; break;
				case ConfigType.Core: сonfigSection = config?.Core; break;
				case ConfigType.KoConfig: сonfigSection = config?.KoConfig; break;
			}

			var res = new ConfigDto();
			if (сonfigSection != null)
			{
				res.RootConfig = JsonConvert.SerializeObject(сonfigSection);
			}
			else
			{
				_log.Verbose("Секция {configType} не найдена", configType.GetEnumDescription());
			}


			if (File.Exists(_envPath))
			{
				var fileEnv = File.ReadAllText(_envPath);
				var configEnv = JsonConvert.DeserializeObject<dynamic>(fileEnv);
				string currentSection = configType.ToString();
				var currentConfigEnv = configEnv?[currentSection];

				if (currentConfigEnv != null)
				{
					res.EnvConfig = JsonConvert.SerializeObject(currentConfigEnv);
				}
			}


			return res;
		}

		private void SetConfig(ConfigDto configDto, ConfigType configType)
		{
			var file = File.ReadAllText(_rootPath);
			var config = JsonConvert.DeserializeObject<dynamic>(file);

			dynamic сonfigSection = null;
			switch (configType)
			{
				case ConfigType.Serilog: сonfigSection = config?.Serilog; break;
				case ConfigType.Core: сonfigSection = config?.Core; break;
				case ConfigType.KoConfig: сonfigSection = config?.KoConfig; break;
			}

			if (сonfigSection != null)
			{
				string sectionName = configType.ToString();
				config[sectionName] = JsonConvert.DeserializeObject<dynamic>(configDto.RootConfig);
			}
			else
			{
				_log.Verbose("Секция {section} не существует в основном файле конфигурации", configType.GetEnumDescription());
			}

			using (FileStream fs = File.OpenWrite(_rootPath))
			{
				string str = JsonConvert.SerializeObject(config);
				byte[] bytes = Encoding.UTF8.GetBytes(str);

				fs.SetLength(0);
				if (fs.CanWrite)
				{
					fs.Write(bytes);
				}
			}

			if (File.Exists(_envPath))
			{
				var fileEnv = File.ReadAllText(_envPath);
				var configEnv = JsonConvert.DeserializeObject<dynamic>(fileEnv);

				switch (configType)
				{
					case ConfigType.Serilog: сonfigSection = configEnv?.Serilog; break;
					case ConfigType.Core: сonfigSection = configEnv?.Core; break;
					case ConfigType.KoConfig: сonfigSection = configEnv?.KoCongig; break;
				}
				if (сonfigSection != null)
				{
					string sectionName = configType.ToString();
					configEnv[sectionName] = JsonConvert.DeserializeObject<dynamic>(configDto.EnvConfig);

					using (FileStream fs = File.OpenWrite(_envPath))
					{
						var str = JsonConvert.SerializeObject(configEnv);
						byte[] bytes = Encoding.UTF8.GetBytes(str);

						fs.SetLength(0);
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