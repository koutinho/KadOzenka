using System;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ConfigurationManagers;
using KadOzenka.Dal.DataComparing.Files;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataComparing.Configs
{
	public class TaskChangesDataComparingConfig : DataComparingConfig
	{
		private static TaskChangesDataComparingConfig instance;
		private static object syncRoot = new object();

		public static TaskChangesDataComparingConfig Current
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new TaskChangesDataComparingConfig();
					}
				}

				instance.TaskChangesComparingFolder = ConfigurationManager.KoConfig.DataComparingConfig.TaskChangesComparingFolder;
				instance.CadastralCostComparingFolder = ConfigurationManager.KoConfig.DataComparingConfig.CadastralCostComparingFolder;

				return instance;
			}
		}

		public string TaskChangesFilePrefix => "Протокол_изм";
		public string ComparingDataFolder => "Протоколы изменений";
		public string ResultDataFolder => "Результаты сравнения протоколов изменений";

		public string GetRsmFileName(OMTask task) => GetNameFromTask(task, RsmSuffix);
		public string GetPkkoFileName(OMTask task) => GetNameFromTask(task, PkkoSuffix);
		public string GetResultFileName(OMTask task) => GetNameFromTask(task, ResultSuffix);

		protected override string ComposeName(OMTask task, OMInstance document, string fileSuffix = null)
		{
			return  TaskChangesFilePrefix + NameSeparator + document.RegNumber + NameSeparator +
			        task.EstimationDate?.ToString(DateFormat) + NameSeparator +
			        document.ApproveDate?.ToString(DateFormat) + NameSeparator + fileSuffix + ".xlsx";
		}

		public OMTask GetTaskInfoFromFileName(TaskChangesFile file)
		{
			var fileWithoutPrefix = file.FileNameWithoutSystemSuffix.TrimStart(TaskChangesFilePrefix + NameSeparator);

			var fileNameParts = fileWithoutPrefix.Split(NameSeparator, StringSplitOptions.RemoveEmptyEntries).ToList();
			if (fileNameParts.Count < 3)
				throw new Exception(
					"Не удалось получить сведения о задании на оценку из имени файла: название файла не удовлетворяет правилам формирования");

			var taskNumber = string.Join(NameSeparator, fileNameParts.Take(fileNameParts.Count - 2));
			var estimationDateString = fileNameParts[fileNameParts.Count - 2];
			var approveDateString = fileNameParts[fileNameParts.Count - 1];
			DateTime.TryParseExact(estimationDateString, DateFormat,
				System.Globalization.CultureInfo.InvariantCulture,
				System.Globalization.DateTimeStyles.None, out var estimationDate);
			DateTime.TryParseExact(approveDateString, DateFormat,
				System.Globalization.CultureInfo.InvariantCulture,
				System.Globalization.DateTimeStyles.None, out var approveDate);

			var task = GetTask(taskNumber, estimationDate, approveDate);
			if (task == null)
			{
				throw new Exception(
					"Не удалось получить сведения о задании на оценку из имени файла: задание на оценку не найдено");
			}

			return task;
		}

		public string GetEmailMessageForTask(OMTask task)
		{
			var taskName = GetTaskTemplateName(task);
			var message =
				$"Результат проверки протоколов загрузки для задания на оценку '{taskName}' ({task.Id}): {task.DataChangesComparingStatus_Code.GetEnumDescription()}\n";

			return message;
		}
	}
}
