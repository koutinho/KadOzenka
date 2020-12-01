using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ObjectModel.Core.TD;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataComparing
{
	public class DataComparingStorageManager
	{
		private static readonly ILogger _log = Log.ForContext<DataComparingStorageManager>();

		public static string GetTaskChangesComparingDataFolder()
		{
			ValidateBaseFolder(DataComparingConfig.Current.DataComparingFolder);

			var taskChangesComparingDataFolder = $"{DataComparingConfig.Current.DataComparingFolder}\\Протоколы изменений";
			if (!Directory.Exists(taskChangesComparingDataFolder))
				Directory.CreateDirectory(taskChangesComparingDataFolder);

			return taskChangesComparingDataFolder;
		}

		public static string GetTaskChangesResultDataFolder()
		{
			ValidateBaseFolder(DataComparingConfig.Current.DataComparingFolder);

			var taskChangesComparingDataFolder = $"{DataComparingConfig.Current.DataComparingFolder}\\Результаты сравнения протоколов изменений";
			if (!Directory.Exists(taskChangesComparingDataFolder))
				Directory.CreateDirectory(taskChangesComparingDataFolder);

			return taskChangesComparingDataFolder;
		}

		public static List<string> GetAllFileNamesTaskChangesComparingDataFolder()
		{
			ValidateBaseFolder(DataComparingConfig.Current.DataComparingFolder);

			var taskChangesComparingDataFolder = $"{DataComparingConfig.Current.DataComparingFolder}\\Протоколы изменений";
			if (!Directory.Exists(taskChangesComparingDataFolder))
				return new List<string>();

			var files = Directory.GetFiles(taskChangesComparingDataFolder, "*.xlsx", SearchOption.TopDirectoryOnly).ToList();
			files.AddRange(Directory.GetFiles(taskChangesComparingDataFolder, "*.xls", SearchOption.TopDirectoryOnly)
				.ToList());

			return files;
		}

		public static Stream GetTaskChangesComparingData(string fileName)
		{
			var fileFolder = GetTaskChangesComparingDataFolder();
			var fullFileName = Path.Combine(fileFolder, fileName);

			if (!File.Exists(fullFileName))
			{
				throw new Exception("Файл физически отсутствует на сервере");
			}
			_log.ForContext("FullFileName", fullFileName).Information("Запрос файла {FileName}", fullFileName);

			return File.OpenRead(fullFileName);
		}

		public static void SaveTaskChangesComparingData(Stream stream, OMTask task)
		{
			var fileFolder = GetTaskChangesComparingDataFolder();
			var fileName = GetTaskChangesComparingFileName(task);
			var fullFileName = Path.Combine(fileFolder, fileName);
			_log.ForContext("TaskId", task.Id)
				.ForContext("FileFolder", fileFolder).Information("Сохранение файла {FileName}", fileName);

			using var fs = File.Create(fullFileName);
			fs.Seek(0, SeekOrigin.Begin);
			stream.CopyTo(fs);
		}

		public static void SaveTaskChangesResult(Stream stream, string fileName)
		{
			var fileFolder = GetTaskChangesResultDataFolder();
			var fullFileName = Path.Combine(fileFolder, fileName);
			_log.ForContext("FileFolder", fileFolder).Information("Сохранение файла {FileName}", fileName);

			using var fs = File.Create(fullFileName);
			fs.Seek(0, SeekOrigin.Begin);
			stream.CopyTo(fs);
		}

		public static void MoveComparingDataToResult(string fileName)
		{
			var sourceFileFolder = GetTaskChangesComparingDataFolder();
			var resultFileFolder = GetTaskChangesResultDataFolder();
			var sourceFullFileName = Path.Combine(sourceFileFolder, fileName);
			var resultFullFileName = Path.Combine(resultFileFolder, fileName);
			_log.ForContext("SourceFileFolder", sourceFileFolder)
				.ForContext("ResultFileFolder", resultFileFolder)
				.Information("Перенос файла {FileName} в результат", fileName);

			if (File.Exists(resultFullFileName))
				File.Delete(resultFullFileName);

			File.Move(sourceFullFileName, resultFullFileName);
		}

		private static string GetTaskChangesComparingFileName(OMTask task)
		{
			if (!task.EstimationDate.HasValue)
				throw new Exception($"Не заполнена Дата оценки для задания на оценку {task.Id}");

			var document = OMInstance.Where(x => x.Id == task.DocumentId).SelectAll().ExecuteFirstOrDefault();
			if (document == null)
				throw new Exception($"Не найден документ для задания на оценку {task.Id}");
			if (!document.ApproveDate.HasValue)
				throw new Exception($"Не заполнена Дата выпуска документа для задания на оценку {task.Id}");

			return FileInfo.GetFileName(task, document);
		}

		private static void ValidateBaseFolder(string dataComparingFolder)
		{
			if (string.IsNullOrEmpty(dataComparingFolder))
			{
				throw new Exception("Не настроен путь для дикректории сравнения данных");
			}

			if (!Directory.Exists(dataComparingFolder))
				throw new Exception($"Отсутствует дикректория сравнения данных по указанному пути:{dataComparingFolder}");
		}
	}
}
