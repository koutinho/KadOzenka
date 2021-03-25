using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace KadOzenka.WebClients.ConfigurationManagers
{
	public static class WrapperConfigurationReonSubscriber
	{
		public static IWebHostBuilder UseReonConfigManager(this IWebHostBuilder builder, IConfigurationRoot config)
		{
			return ConfigurationReonSubscriber.UseReonConfigManager(builder, config);
		}
	}
	internal static class ConfigurationReonSubscriber
	{
		private static readonly ILogger Log = Serilog.Log.ForContext(typeof(ConfigurationReonSubscriber));

		private static ReonServiceConfig _configManager;


		public static IWebHostBuilder UseReonConfigManager(this IWebHostBuilder builder, IConfigurationRoot config)
		{
			Log.Debug("Заполняем конфигурационный менеджер Реон");

			ReonServiceConfig manger = new ReonServiceConfig();
			config.GetSection("ReonConfig").Bind(manger);
			_configManager = manger;

			Log.ForContext("ReonConfig", manger, true).Debug("Конфигурационный менеджер кад Реон");
			ChangeToken.OnChange(config.GetReloadToken, InvokeChanged, config);

			return builder;
		}

		public static ReonServiceConfig GetCurrentReonConfiguration()
		{
			return _configManager ?? new ReonServiceConfig();
		}

		private static void InvokeChanged(IConfigurationRoot config)
		{
			ReonServiceConfig manger = new ReonServiceConfig();
			config.GetSection("ReonConfig").Bind(manger);
			_configManager = manger;
		}
	}
}