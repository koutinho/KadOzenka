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
			_log.Debug("Путь к файлу дополнительных ресурсов: {_envPath}", _envPath);
		}

		#region serilog section

		public ConfigDto GetSerilogConfig()
		{
			return GetConfig(SectionType.Serilog);
		}

		public void SetSerilogConfig(ConfigDto configDto)
		{
			SetConfig(configDto, SectionType.Serilog);

		}

		#endregion

		#region core section

		public ConfigDto GetCoreConfig()
		{
			return GetConfig(SectionType.Core);
		}

		public void SetCoreConfig(ConfigDto configDto)
		{
			SetConfig(configDto, SectionType.Core);

		}

		#endregion

		#region ko section

		public ConfigDto GetKoConfig()
		{
			return GetConfig(SectionType.KoConfig);
		}

		public void SetKoConfig(ConfigDto configDto)
		{
			SetConfig(configDto, SectionType.KoConfig);

		}

		#endregion

		private ConfigDto GetConfig(SectionType sectionType)
		{

			var file = File.ReadAllText(_rootPath);
			var config = JsonConvert.DeserializeObject<dynamic>(file);

			dynamic сonfigSection = null;
	
			switch (sectionType)
			{
				case SectionType.Serilog: сonfigSection = config?.Serilog; break;
				case SectionType.Core: сonfigSection = config?.Core; break;
				case SectionType.KoConfig: сonfigSection = config?.KoConfig; break;
			}

			var res = new ConfigDto();
			if (сonfigSection != null)
			{
				res.RootConfig = JsonConvert.SerializeObject(сonfigSection);
			}
			else
			{
				_log.Debug("Секция {sectionType} не найдена", sectionType.GetEnumDescription());
			}


			if (File.Exists(_envPath))
			{
				var fileEnv = File.ReadAllText(_envPath);
				var configEnv = JsonConvert.DeserializeObject<dynamic>(fileEnv);
				string currentSection = sectionType.ToString();
				var currentConfigEnv = configEnv?[currentSection];

				if (currentConfigEnv != null)
				{
					res.EnvConfig = JsonConvert.SerializeObject(currentConfigEnv);
				}
			}
			else
			{
				_log.Debug("Дополнительный конфигурационный файл не найден");
			}


			return res;
		}

		private void SetConfig(ConfigDto configDto, SectionType sectionType)
		{
			var file = File.ReadAllText(_rootPath);
			var config = JsonConvert.DeserializeObject<dynamic>(file);

			dynamic сonfigSection = null;
			switch (sectionType)
			{
				case SectionType.Serilog: сonfigSection = config?.Serilog; break;
				case SectionType.Core: сonfigSection = config?.Core; break;
				case SectionType.KoConfig: сonfigSection = config?.KoConfig; break;
			}

			if (сonfigSection != null)
			{
				string sectionName = sectionType.ToString();
				config[sectionName] = JsonConvert.DeserializeObject<dynamic>(configDto.RootConfig);
			}
			else
			{
				_log.Debug("Секция {section} не существует в основном файле конфигурации", sectionType.GetEnumDescription());
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

				switch (sectionType)
				{
					case SectionType.Serilog: сonfigSection = configEnv?.Serilog; break;
					case SectionType.Core: сonfigSection = configEnv?.Core; break;
					case SectionType.KoConfig: сonfigSection = configEnv?.KoCongig; break;
				}
				if (сonfigSection != null)
				{
					string sectionName = sectionType.ToString();
					configEnv[sectionName] = JsonConvert.DeserializeObject<dynamic>(configDto.EnvConfig ?? "");

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
				else
				{
					_log.Debug("Секция {section} не существует в дополнительном файле конфигурации", sectionType.GetEnumDescription());

				}

			}
		}
	}
}