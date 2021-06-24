using System;
using System.Threading;
using System.Threading.Tasks;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports
{
	public abstract class LongProcessForReportsBase : LongProcess
	{
		protected CustomReportsService CustomReportsService { get; }
		protected ILogger Logger { get; }

		protected LongProcessForReportsBase(ILogger logger)
		{
			CustomReportsService = new CustomReportsService();
			Logger = logger;
		}

		public virtual void AddToQueue(object input)
		{

		}

		protected ReportsConfig GetProcessConfigFromSettings(string reportSectionName, int defaultPackageSize,
			int defaultThreadsCount)
		{
			var fileName = "appsettings.json";
			Logger.Debug("Поиск настроек конфигурации из файла {FileName}", fileName);

			var configuration = new ConfigurationBuilder()
				.AddJsonFile(path: fileName, optional: false, reloadOnChange: true)
				.Build();

			var config = new ReportsConfig();
			configuration.GetSection($"Reports:{reportSectionName}").Bind(config);
			Logger.ForContext("Configs", config, true).Debug("Полученные настройки конфигурации для секции {SectionName}", reportSectionName);

			var packageSize = config.PackageSize == 0 ? defaultPackageSize : config.PackageSize;
			var threadsCount = config.ThreadsCount == 0 ? defaultThreadsCount : config.ThreadsCount;

			config.PackageSize = packageSize;
			config.ThreadsCount= threadsCount;

			Logger.ForContext("ResultConfigs", config, true).Debug("Итоговые настройки конфигурации для секции {SectionName}", reportSectionName);

			return config;
		}

		protected string ProcessDate(string dateStr)
		{
			if (!string.IsNullOrWhiteSpace(dateStr) && DateTime.TryParse(dateStr, out var date))
			{
				dateStr = date.ToString("dd.MM.yyyy");
			}

			return dateStr;
		}
	}
}
