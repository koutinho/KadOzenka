using System;
using System.Linq;
using Core.Shared.Extensions;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataComparing.Configs
{
	public class CadastralCostDataComparingConfig : DataComparingConfig
	{
		private static CadastralCostDataComparingConfig instance;
		private static object syncRoot = new object();

		public static CadastralCostDataComparingConfig Current
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new CadastralCostDataComparingConfig();
					}
				}

				var baseConfig = Core.ConfigParam.Configuration.GetParam<DataComparingConfig>("DataComparingConfig");
				instance.TaskChangesComparingFolder = baseConfig.TaskChangesComparingFolder;
				instance.CadastralCostComparingFolder = baseConfig.CadastralCostComparingFolder;

				return instance;
			}
		}

		public string TaskRmsFolder => "Данные результатов оценки РСМ";
		public string TaskPkkoFolder => "Данные результатов оценки ПККО";
		public string TaskResultFolder => "Результаты сравнения";
		public string TaskResultFile => "Результат сравнения.xlsx";
		public string TaskFDResultFile => "Результат сравнения FD файлов.xlsx";

		public string GetTaskFolderName(OMTask task) => GetNameFromTask(task);

		public OMTask GetTaskInfoFromFolderName(string folderName)
		{
			var fileNameParts = folderName.Split(NameSeparator, StringSplitOptions.RemoveEmptyEntries).ToList();
			if (fileNameParts.Count < 4)
				throw new Exception(
					"Не удалось получить сведения о задании на оценку из имени директории: название директории не удовлетворяет правилам формирования");

			var taskNumber = string.Join(NameSeparator, fileNameParts.Skip(1).Take((fileNameParts.Count - 1) - 2));
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
					"Не удалось получить сведения о задании на оценку из имени директории: задание на оценку не найдено");
			}

			return task;
		}

		public string GetFileNameForResultFolder(string fileName, SystemType fileSystemType)
		{
			var suffix = fileSystemType == SystemType.Rsm ? RsmSuffix : PkkoSuffix;
			return System.IO.Path.GetFileNameWithoutExtension(fileName) + NameSeparator + suffix +
			       System.IO.Path.GetExtension(fileName);
		}

		public string GetCostEmailMessageForTask(OMTask task)
		{
			var taskName = GetTaskTemplateName(task);
			var message = $"Результат проверки протоколов загрузки для задания на оценку '{taskName}' ({task.Id}): ";
			if (task.CadastralCostComparingStatus_Code == KoDataComparingCadastralCostStatus.ThereAreUnitSetsInconsistencies 
			    || task.CadastralCostComparingStatus_Code == KoDataComparingCadastralCostStatus.ThereAreUnitCostsInconsistencies)
			{
				message += task.CadastralCostComparingStatus_Code.GetEnumDescription() + "\n";
				if (task.CadastralCostComparingStatus_Code ==
				    KoDataComparingCadastralCostStatus.ThereAreUnitCostsInconsistencies)
				{
					message += $@"<a href=""/Task/ExportTaskCadastralCostFDData?taskId={task.Id}"">Запустить формирование FD файлов</a>" + "\n";
				}
			}
			else
			{
				message += "Данные совпадают";
			}

			return message;
		}

		public string GetFDEmailMessageForTask(OMTask task)
		{
			var taskName = GetTaskTemplateName(task);
			var message = $"Результат сравнения FD файлов для задания на оценку '{taskName}' ({task.Id}): ";
			message += $@"<a href=""/Task/DownloadTaskCadastralCostFDDataComparingResult?taskId={task.Id}"">Скачать результат</a>";

			return message;
		}

		protected override string ComposeName(OMTask task, OMInstance document, bool resultFile = false)
		{
			return task.NoteType_Code.GetEnumDescription() + NameSeparator + document.RegNumber + NameSeparator +
			       task.EstimationDate?.ToString(DateFormat) + NameSeparator + document.ApproveDate?.ToString(DateFormat);
		}
	}
}
