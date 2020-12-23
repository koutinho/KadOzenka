using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Core.Shared.Extensions;
using Ionic.Zip;
using KadOzenka.Dal.DataComparing.Configs;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Tasks;
using ObjectModel.KO;
using Serilog;
using File = System.IO.File;

namespace KadOzenka.Dal.DataComparing.StorageManagers
{
	public class CadastralCostDataComparingStorageManager
	{
		private static readonly ILogger _log = Log.ForContext<CadastralCostDataComparingStorageManager>();

		public static List<string> GetTaskFoldersToProcess()
		{
			var allTaskFolders = Directory.GetDirectories(GetBaseFolder(), "*", SearchOption.TopDirectoryOnly).ToList();
			var taskFoldersToProcess = new List<string>();
			foreach (var taskDirectory in allTaskFolders)
			{
				var rsmFolder = Path.Combine(taskDirectory, CadastralCostDataComparingConfig.Current.TaskRmsFolder);
				var pkkoFolder = Path.Combine(taskDirectory, CadastralCostDataComparingConfig.Current.TaskPkkoFolder);
				if (Directory.Exists(rsmFolder) && Directory.GetFiles(rsmFolder).IsNotEmpty()
				                                && Directory.Exists(pkkoFolder) &&
				                                Directory.GetFiles(pkkoFolder).IsNotEmpty())
				{
					taskFoldersToProcess.Add(Path.GetFileName(taskDirectory.TrimEnd(Path.DirectorySeparatorChar)));
				}
			}

			return taskFoldersToProcess;
		}

		public static List<string> GetTaskCostFiles(string taskFolder, string subfolder)
		{
			var folder = GetBaseFolder(taskFolder, subfolder);
			return Directory.GetFiles(folder, "Task_*COST_*.xml", SearchOption.TopDirectoryOnly).ToList();
		}

		public static List<string> GetTaskCostFilesFromResult(string taskFolder, SystemType systemType)
		{
			var fileFolder = GetBaseFolder(taskFolder, CadastralCostDataComparingConfig.Current.TaskResultFolder);
			var suffix = systemType == SystemType.Rsm 
				? CadastralCostDataComparingConfig.Current.RsmSuffix 
				: CadastralCostDataComparingConfig.Current.PkkoSuffix;

			return Directory.GetFiles(fileFolder, $"Task_*COST_*{suffix}.xml", SearchOption.TopDirectoryOnly).ToList();
		}

		public static List<string> GetTaskFDFiles(string taskFolder, string subfolder)
		{
			var folder = GetBaseFolder(taskFolder, subfolder);
			return Directory.GetFiles(folder, "Task_*FD_State_Cadastral_Valuation_*.xml", SearchOption.TopDirectoryOnly).ToList();
		}

		public static Stream GetComparingDataFile(string fullFileName)
		{
			if (!File.Exists(fullFileName))
			{
				throw new Exception("Файл физически отсутствует на сервере");
			}
			_log.ForContext("FullFileName", fullFileName).Information("Запрос файла {FileName}", fullFileName);

			return File.OpenRead(fullFileName);
		}

		public static FileStream GetResultFile(OMTask task, bool downloadFD = false)
		{
			var fileFolder = GetBaseFolder(CadastralCostDataComparingConfig.Current.GetTaskFolderName(task), CadastralCostDataComparingConfig.Current.TaskResultFolder);
			var fileName = downloadFD
				? CadastralCostDataComparingConfig.Current.TaskFDResultFile
				: CadastralCostDataComparingConfig.Current.TaskResultFile;
			var fullFileName = Path.Combine(fileFolder, fileName);

			if (!File.Exists(fullFileName))
			{
				throw new Exception("Файл физически отсутствует на сервере");
			}
			_log.ForContext("FullFileName", fullFileName).Information("Запрос файла {FileName}", fullFileName);

			return File.OpenRead(fullFileName);
		}

		public static string GetTaskRsmFolderFullPath(OMTask task)
		{
			return GetBaseFolder(CadastralCostDataComparingConfig.Current.GetTaskFolderName(task), CadastralCostDataComparingConfig.Current.TaskRmsFolder);
		}

		public static void SaveResultFile(string taskFolder, Stream fileStream, bool isFDResult = false)
		{
			var fileFolder = GetBaseFolder(taskFolder, CadastralCostDataComparingConfig.Current.TaskResultFolder);
			var fileName = isFDResult
				? CadastralCostDataComparingConfig.Current.TaskFDResultFile
				: CadastralCostDataComparingConfig.Current.TaskResultFile;
			var fullFileName = Path.Combine(fileFolder, fileName);
			_log.ForContext("FileFolder", fileFolder)
				.Information("Сохранение файла '{FileName}' для '{TaskFolder}'", CadastralCostDataComparingConfig.Current.TaskResultFile, taskFolder);

			using var fs = File.Create(fullFileName);
			fs.Seek(0, SeekOrigin.Begin);
			fileStream.CopyTo(fs);
		}

		public static void RemovePreviousCostFilesFromResult(string taskFolder)
		{
			var fileFolder = GetBaseFolder(taskFolder, CadastralCostDataComparingConfig.Current.TaskResultFolder);
			var files = Directory.GetFiles(fileFolder, "Task_*COST_*.xml", SearchOption.TopDirectoryOnly).ToList();
			foreach (var file in files)
			{
				File.Delete(file);
			}
		}

		public static void RemovePreviousFDFilesFromResult(string taskFolder)
		{
			var fileFolder = GetBaseFolder(taskFolder, CadastralCostDataComparingConfig.Current.TaskResultFolder);
			var files = Directory.GetFiles(fileFolder, "Task_*FD_State_Cadastral_Valuation_*.xml", SearchOption.TopDirectoryOnly).ToList();
			foreach (var file in files)
			{
				File.Delete(file);
			}
		}

		public static bool ContainsResultFdFile(OMTask task)
		{
			var fileFolder = GetBaseFolder(CadastralCostDataComparingConfig.Current.GetTaskFolderName(task), CadastralCostDataComparingConfig.Current.TaskResultFolder);
			var fullFileName = Path.Combine(fileFolder, CadastralCostDataComparingConfig.Current.TaskFDResultFile);

			return File.Exists(fullFileName);
		}

		public static bool AreCostPkkoFilesUploaded(OMTask task)
		{
			var fileFolder = GetBaseFolder(CadastralCostDataComparingConfig.Current.GetTaskFolderName(task), CadastralCostDataComparingConfig.Current.TaskPkkoFolder);
			var fileNames = Directory.GetFiles(fileFolder, "Task_*COST_*.xml", SearchOption.TopDirectoryOnly).ToList();

			return fileNames.Count > 0;
		}

		public static bool AreFdPkkoFilesUploaded(OMTask task)
		{
			var fileFolder = GetBaseFolder(CadastralCostDataComparingConfig.Current.GetTaskFolderName(task), CadastralCostDataComparingConfig.Current.TaskPkkoFolder);
			var fileNames = Directory.GetFiles(fileFolder, "Task_*FD_State_Cadastral_Valuation_*.xml", SearchOption.TopDirectoryOnly).ToList();

			return fileNames.Count > 0;
		}

		public static GbuReportService.ReportFile GetTaskPkkoFiles(OMTask task, bool loadCostFiles = true, bool loadFdFiles = true)
		{
			if (!loadCostFiles && !loadFdFiles)
				throw new Exception("Не указан тип выгружаемых файлов (Cost, FD)");

			GbuReportService.ReportFile reportFile = new GbuReportService.ReportFile();
			var taskFolder = CadastralCostDataComparingConfig.Current.GetTaskFolderName(task);

			var files = new List<string>();
			if (loadCostFiles)
				files.AddRange(GetTaskCostFiles(taskFolder, CadastralCostDataComparingConfig.Current.TaskPkkoFolder));
			if (loadFdFiles)
				files.AddRange(GetTaskFDFiles(taskFolder, CadastralCostDataComparingConfig.Current.TaskPkkoFolder));

			_log.ForContext("FileNames", files)
				.Debug("Добавление в архив ПККО фалов для {TaskFolder}({TaskId})", taskFolder, task.Id);
			using (ZipFile zipFile = new ZipFile())
			{
				zipFile.AlternateEncoding = Encoding.UTF8;
				zipFile.AlternateEncodingUsage = ZipOption.AsNecessary;

				foreach (var file in files)
				{
					_log.Verbose("Добавление файла {FileName} в архив", file);
					zipFile.AddFile(file, string.Empty);
				}

				var stream = new MemoryStream();
				zipFile.Save(stream);
				stream.Seek(0, SeekOrigin.Begin);
				reportFile.FileStream = stream;
				reportFile.FileName = loadCostFiles && loadFdFiles
					? $"Данные ПККО {taskFolder}.zip"
					: $"Данные ПККО {taskFolder} ({(loadCostFiles ? "COST" : "")}{(loadFdFiles ? "FD" : "")} файлы).zip";
			}

			return reportFile;
		}

		public static void AddNewPkkoCostFiles(OMTask task, DisposableList<Stream> streamList)
		{
			var taskFolder = CadastralCostDataComparingConfig.Current.GetTaskFolderName(task);
			var fileFolder = GetBaseFolder(taskFolder, CadastralCostDataComparingConfig.Current.TaskPkkoFolder);
			var files = Directory.GetFiles(fileFolder, "Task_*COST_*.xml", SearchOption.TopDirectoryOnly).ToList();
			_log.ForContext("FileNames", files)
				.Debug("Получена информация о предыдущих ПККО COST файлах для {TaskFolder}({TaskId}) для их удаления", taskFolder, task.Id);
			foreach (var file in files)
			{
				_log.Verbose("Удаление файла '{FileName}'", file);
				File.Delete(file);
			}

			var i = 0;
			foreach (var stream in streamList)
			{
				var fileName = $"Task_{task.Id}_COST_{i}.xml";

				_log.ForContext("FileFolder", fileFolder)
					.Verbose("Сохранение файла '{FileName}'", fileName);
				using var fs = File.Create(Path.Combine(fileFolder, fileName));
				fs.Seek(0, SeekOrigin.Begin);
				stream.CopyTo(fs);

				i++;
			}
		}

		public static void AddNewPkkoFdFiles(OMTask task, DisposableList<Stream> streamList)
		{
			var taskFolder = CadastralCostDataComparingConfig.Current.GetTaskFolderName(task);
			var fileFolder = GetBaseFolder(taskFolder, CadastralCostDataComparingConfig.Current.TaskPkkoFolder);
			var files = Directory.GetFiles(fileFolder, "Task_*FD_State_Cadastral_Valuation_*.xml", SearchOption.TopDirectoryOnly).ToList();
			_log.ForContext("FileNames", files)
				.Debug("Получена информация о предыдущих ПККО FD файлах для {TaskFolder}({TaskId}) для их удаления", taskFolder, task.Id);
			foreach (var file in files)
			{
				_log.Verbose("Удаление файла '{FileName}'", file);
				File.Delete(file);
			}

			var i = 0;
			foreach (var stream in streamList)
			{
				var fileName = $"Task_{task.Id}_FD_State_Cadastral_Valuation_{i}.xml";

				_log.ForContext("FileFolder", fileFolder)
					.Information("Сохранение файла '{FileName}'", fileName);
				using var fs = File.Create(Path.Combine(fileFolder, fileName));
				fs.Seek(0, SeekOrigin.Begin);
				stream.CopyTo(fs);

				i++;
			}
		}

		public static void MoveFileToResultFolder(Files.File file, string taskFolder)
		{
			var sourceFileFolder = GetBaseFolder(taskFolder,
				file.SystemType == SystemType.Rsm
					? CadastralCostDataComparingConfig.Current.TaskRmsFolder
					: CadastralCostDataComparingConfig.Current.TaskPkkoFolder);
			var resultFileFolder = GetBaseFolder(taskFolder, CadastralCostDataComparingConfig.Current.TaskResultFolder);

			var sourceFullFileName = Path.Combine(sourceFileFolder, file.Name);
			var resultFullFileName = Path.Combine(resultFileFolder, CadastralCostDataComparingConfig.Current.GetFileNameForResultFolder(file.Name, file.SystemType));
			_log.ForContext("SourceFileFolder", sourceFileFolder)
				.ForContext("ResultFileFolder", resultFileFolder)
				.Information("Перенос файла {FileName} в результат", file.Name);

			if (File.Exists(resultFullFileName))
				File.Delete(resultFullFileName);

			File.Move(sourceFullFileName, resultFullFileName);
		}

		private static string GetBaseFolder(params string[] paths)
		{
			if (string.IsNullOrEmpty(CadastralCostDataComparingConfig.Current.CadastralCostComparingFolder))
			{
				throw new Exception("Не настроен путь для дикректории сравнения данных");
			}

			if (!Directory.Exists(CadastralCostDataComparingConfig.Current.CadastralCostComparingFolder))
				Directory.CreateDirectory(CadastralCostDataComparingConfig.Current.CadastralCostComparingFolder);

			var dataFolder = CadastralCostDataComparingConfig.Current.CadastralCostComparingFolder;
			foreach (var path in paths)
			{
				dataFolder = Path.Combine(dataFolder, path);
			}

			if (!Directory.Exists(dataFolder))
				Directory.CreateDirectory(dataFolder);

			return dataFolder;
		}
	}
}
