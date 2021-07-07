using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace KadOzenka.WebClients.ConfigurationManagers
{
	public static class WrapperConfigurationWebClientSubscriber
	{
		public static IWebHostBuilder UseWebClientsConfigManager(this IWebHostBuilder builder, IConfigurationRoot config)
		{
			return ConfigurationWebClientsSubscriber.UseWebClientsConfigManager(builder, config);
		}
	}
	internal static class ConfigurationWebClientsSubscriber
	{
		private static readonly ILogger Log = Serilog.Log.ForContext(typeof(ConfigurationWebClientsSubscriber));

		private static WebClientsConfig _configManager;


		public static IWebHostBuilder UseWebClientsConfigManager(this IWebHostBuilder builder, IConfigurationRoot config)
		{
			Log.Debug("Заполняем конфигурационный менеджер WebClients");

			WebClientsConfig manger = new WebClientsConfig();
			config.GetSection("WebClientsConfig").Bind(manger);
			_configManager = manger;

			Log.ForContext("WebClientsConfig", manger, true).Debug("Конфигурационный менеджер WebClients");
			ChangeToken.OnChange(config.GetReloadToken, InvokeChanged, config);

			return builder;
		}

		public static WebClientsConfig GetCurrentWebClientsConfiguration()
		{
			return _configManager ?? new WebClientsConfig();
		}

		private static void InvokeChanged(IConfigurationRoot config)
		{
			WebClientsConfig manger = new WebClientsConfig();
			config.GetSection("WebClientsConfig").Bind(manger);
			_configManager = manger;
		}
	}
}