using System;
using System.IO;
using DocumentFormat.OpenXml.Drawing;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;
using Serilog;

namespace KadOzenka.Dal.CommonFunctions.ExistFolderChecker
{
	public class ExistFolderCheckerService
	{
		private static readonly ILogger _log = Log.ForContext(typeof(ExistFolderCheckerService));
		private static void CheckAndCreateFileStorageDirectories()
		{
			CoreConfigManager.FileStorageCurrent.FileStorages?.ForEach(item =>
				{
					try
					{
						if (Directory.Exists(item.Path)) return;
						var dInfo = Directory.CreateDirectory(item.Path);
						bool notExists = !dInfo.Exists;
						//TODO После мержа ветки с платформенным логированием нужно будет создать уникальную ошибку
						if (notExists) throw new Exception("Не удалось создать папку для файлового хранилища");
					}
					catch (Exception e)
					{
						_log.ForContext("key", item.Key)
							.ForContext("path", item.Path).Warning(e.Message + " : {description}", item.Description);
					}
				});
			}

		public static void Run()
		{
			CheckAndCreateFileStorageDirectories();
		}
	}
}