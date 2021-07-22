using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommonSdks.Excel;
using KadOzenka.Dal.DataComparing.Configs;
using KadOzenka.Dal.GbuObject;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataComparing.StorageManagers
{
	public class TaskChangesDataComparingStorageManager
	{
		private static readonly ILogger _log = Log.ForContext<TaskChangesDataComparingStorageManager>();

		public static List<string> GetAllComparingDataFileNames()
		{
			var taskChangesComparingDataFolder = GetTaskChangesDataFolder(TaskChangesDataComparingConfig.Current.ComparingDataFolder);
			var files = Directory.GetFiles(taskChangesComparingDataFolder, "*.xlsx", SearchOption.TopDirectoryOnly).ToList();
			files.AddRange(Directory.GetFiles(taskChangesComparingDataFolder, "*.xls", SearchOption.TopDirectoryOnly)
				.ToList());

			return files;
		}

		public static Stream GetComparingDataFile(string fileName)
		{
			var fileFolder = GetTaskChangesDataFolder(TaskChangesDataComparingConfig.Current.ComparingDataFolder);
			var fullFileName = Path.Combine(fileFolder, fileName);

			if (!File.Exists(fullFileName))
			{
				throw new Exception("Файл физически отсутствует на сервере");
			}
			_log.ForContext("FullFileName", fullFileName).Information("Запрос файла {FileName}", fullFileName);

			return File.OpenRead(fullFileName);
		}

		public static FileStream GetResultFile(OMTask task)
		{
			var fileFolder = GetTaskChangesDataFolder(TaskChangesDataComparingConfig.Current.ResultDataFolder);
			var fileName = TaskChangesDataComparingConfig.Current.GetResultFileName(task);
			var fullFileName = Path.Combine(fileFolder, fileName);

			if (!File.Exists(fullFileName))
			{
				throw new Exception("Файл физически отсутствует на сервере");
			}
			_log.ForContext("FullFileName", fullFileName).Information("Запрос файла {FileName}", fullFileName);

			return File.OpenRead(fullFileName);
		}

		public static string GetComparingDataRsmFileFullName(OMTask task)
		{
			var fileFolder = GetTaskChangesDataFolder(TaskChangesDataComparingConfig.Current.ComparingDataFolder);
			var fileName = TaskChangesDataComparingConfig.Current.GetRsmFileName(task);
			return Path.Combine(fileFolder, fileName);
		}

		public static void SaveResultFile(Stream stream, string fileName)
		{
			var fileFolder = GetTaskChangesDataFolder(TaskChangesDataComparingConfig.Current.ResultDataFolder);
			var fullFileName = Path.Combine(fileFolder, fileName);
			_log.ForContext("FileFolder", fileFolder).Information("Сохранение файла {FileName}", fileName);

			using var fs = File.Create(fullFileName);
			fs.Seek(0, SeekOrigin.Begin);
			stream.CopyTo(fs);
		}

		public static void MoveComparingDataFileToResultFolder(string fileName)
		{
			var sourceFileFolder = GetTaskChangesDataFolder(TaskChangesDataComparingConfig.Current.ComparingDataFolder);
			var resultFileFolder = GetTaskChangesDataFolder(TaskChangesDataComparingConfig.Current.ResultDataFolder);
			var sourceFullFileName = Path.Combine(sourceFileFolder, fileName);
			var resultFullFileName = Path.Combine(resultFileFolder, fileName);
			_log.ForContext("SourceFileFolder", sourceFileFolder)
				.ForContext("ResultFileFolder", resultFileFolder)
				.Information("Перенос файла {FileName} в результат", fileName);

			if (File.Exists(resultFullFileName))
				File.Delete(resultFullFileName);

			File.Move(sourceFullFileName, resultFullFileName);
		}

		public static bool IsTaskChangesPkkoFileUploaded(OMTask task)
		{
			var fileName = TaskChangesDataComparingConfig.Current.GetPkkoFileName(task);
			var taskChangesComparingDataFolder = GetTaskChangesDataFolder(TaskChangesDataComparingConfig.Current.ComparingDataFolder);

			return File.Exists(Path.Combine(taskChangesComparingDataFolder, fileName));
		}

		public static GbuReportService.ReportFile GetTaskChangesPkkoFile(OMTask task)
		{
			var fileName = TaskChangesDataComparingConfig.Current.GetPkkoFileName(task);
			var taskChangesComparingDataFolder = GetTaskChangesDataFolder(TaskChangesDataComparingConfig.Current.ComparingDataFolder);
			var fullFileName = Path.Combine(taskChangesComparingDataFolder, $"{fileName}");

			if (!File.Exists(fullFileName))
			{
				throw new Exception("Файл физически отсутствует на сервере");
			}
			_log.ForContext("FullFileName", fullFileName).Information("Запрос файла {FileName}", fullFileName);

			MemoryStream memoryStream;
			using (var fileStream = File.OpenRead(fullFileName))
			{
				memoryStream = new MemoryStream();
				fileStream.CopyTo(memoryStream);
			}

			memoryStream.Seek(0, SeekOrigin.Begin);
			var report = new GbuReportService.ReportFile
			{
				FileName = fileName,
				FileStream = memoryStream
			};

			return report;
		}

		public static void SaveTaskChangesPkkoFile(Stream stream, OMTask task)
		{
			var fileName = TaskChangesDataComparingConfig.Current.GetPkkoFileName(task);
			var taskChangesComparingDataFolder = GetTaskChangesDataFolder(TaskChangesDataComparingConfig.Current.ComparingDataFolder);
			var fullFileName = Path.Combine(taskChangesComparingDataFolder, fileName);
			_log.ForContext("FileFolder", taskChangesComparingDataFolder).Information("Сохранение файла {FileName}", fileName);

			using var fs = File.Create(fullFileName);
			fs.Seek(0, SeekOrigin.Begin);
			stream.CopyTo(fs);
		}

		private static string GetTaskChangesDataFolder(string folder)
		{
			if (string.IsNullOrEmpty(TaskChangesDataComparingConfig.Current.TaskChangesComparingFolder))
			{
				throw new Exception("Не настроен путь для дикректории сравнения данных");
			}

			if (!Directory.Exists(TaskChangesDataComparingConfig.Current.TaskChangesComparingFolder))
				Directory.CreateDirectory(TaskChangesDataComparingConfig.Current.TaskChangesComparingFolder);

			var dataFolder = Path.Combine(TaskChangesDataComparingConfig.Current.TaskChangesComparingFolder, folder);
			if (!Directory.Exists(dataFolder))
				Directory.CreateDirectory(dataFolder);

			return dataFolder;
		}
	}
}
