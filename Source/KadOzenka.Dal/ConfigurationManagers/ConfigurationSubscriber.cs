using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace KadOzenka.Dal.ConfigurationManagers
{
	public static class ConfigurationSubscriber
	{
		private static readonly ILogger Log = Serilog.Log.ForContext(typeof(ConfigurationSubscriber));

		private static KoConfigManager _configManager;


		public static IWebHostBuilder UseKoConfigManager(this IWebHostBuilder builder, IConfigurationRoot config)
		{
			Log.Debug("Заполняем конфигурационный менеджер кад оценки");

			KoConfigManager manger = new KoConfigManager();
			config.GetSection("KoConfig").Bind(manger);
			_configManager = manger;

			Log.ForContext("KoConfig", manger, true).Debug("Конфигурационный менеджер кад оценки заполнен");

			ChangeToken.OnChange(config.GetReloadToken, InvokeChanged, config);

			return builder;
		}

		public static KoConfigManager GetCurrentKoConfiguration()
		{
			return _configManager ?? new KoConfigManager();
		}

		private static void InvokeChanged(IConfigurationRoot config)
		{
			Log.Debug("Спаботала подписка на изменение конфигурационного файла");

			KoConfigManager manger = new KoConfigManager();
			config.GetSection("KoConfig").Bind(manger);
			_configManager = manger;
		}

	}
}