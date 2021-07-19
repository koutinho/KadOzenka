using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.SRD;
using Ionic.Zip;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.LongProcess.Common;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Common;
using ObjectModel.KO;
using Serilog;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.Tasks;
using ObjectModel.Directory.Core.LongProcess;

namespace KadOzenka.Dal.LongProcess.DataComparing
{
	public class UploadTaskChangesPkkoFileLongProcess : LongProcess
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<UploadTaskChangesPkkoFileLongProcess>();
		public const string LongProcessName = "UploadTaskChangesPkkoFileLongProcess";

		private TaskService _taskService;

		public UploadTaskChangesPkkoFileLongProcess()
		{
			_taskService = new TaskService();
		}

		public static void AddImportToQueue(Stream zipFile, long taskId)
		{
			var import = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status_Code = ImportStatus.Added,
				DataFileTitle = "Файлы протоколов изменений",
				FileExtension = "zip",
				RegisterId = OMTask.GetRegisterId(),
				ObjectId = taskId,
				MainRegisterId = OMTask.GetRegisterId(),
				RegisterViewId = "Tasks"
			};
			import.Save();

			import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
			FileStorageManager.Save(zipFile, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
			import.Save();

			////TODO код для отладки
			//var processType = OMProcessType.Where(x => x.ProcessName == LongProcessName)
			//	.SelectAll().ExecuteFirstOrDefault();
			//var queue = new OMQueue { Status_Code = Status.Running, UserId = SRDSession.GetCurrentUserId(), ObjectId = import.Id };
			//new UploadTaskChangesPkkoFileLongProcess().StartProcess(processType,
			//	queue,
			//	new CancellationTokenSource().Token);
			//queue.Status_Code = Status.Completed;
			//queue.Save();

			LongProcessManager.AddTaskToQueue(LongProcessName, OMTask.GetRegisterId(), import.Id);
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Log.Information("Старт фонового процесса: {Description}.", processType.Description);

			if (!processQueue.ObjectId.HasValue)
			{
				WorkerCommon.SetMessage(processQueue, Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				return;
			}

			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();
			if (import == null)
			{
				WorkerCommon.SetMessage(processQueue, Consts.GetMessageForProcessInterruptedBecauseOfNoDataLog(processQueue.ObjectId.Value));
				WorkerCommon.SetProgress(processQueue, Consts.ProgressForProcessInterruptedBecauseOfNoDataLog);
				return;
			}

			WorkerCommon.SetProgress(processQueue, 0);
			var taskId = import.ObjectId.Value;
			var taskName = _taskService.GetTemplateForTaskName(taskId);

			try
			{
				import.Status_Code = ImportStatus.Running;
				import.DateStarted = DateTime.Now;
				import.Save();

				Log.Debug($"Формирование ПККО протокола изменений для задания на оценку {taskName}({taskId})");
				ExcelFile mainExcelFile = FormExcelFile(import);

				Log.Debug($"Cохранение ПККО протокола изменений для задания на оценку {taskName}({taskId})");
				var savedStream = new MemoryStream();
				mainExcelFile.Save(savedStream, SaveOptions.XlsxDefault);
				savedStream.Seek(0, SeekOrigin.Begin);
				_taskService.UploadDataComparingTaskChangesPkkoFile(taskId, savedStream);

				import.Status_Code = ImportStatus.Completed;
				import.DateFinished = DateTime.Now;
				import.Save();

				WorkerCommon.SetProgress(processQueue, 100);
				NotificationSender.SendNotification(processQueue, "Результат загрузки ПККО протокола изменений", $"Загрузка ПККО протокола изменений для '{taskName}': Операция успешно завершена.");
				Log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);
			}
			catch (Exception ex)
			{
				long errorId = ErrorManager.LogError(ex);
				import.Status_Code = ImportStatus.Faulted;
				import.DateFinished = DateTime.Now;
				import.ResultMessage = $"{ex.Message} (журнал № {errorId})";
				import.Save();

				NotificationSender.SendNotification(processQueue, "Результат загрузки ПККО протокола изменений", $"Загрузка ПККО протокола изменений для '{taskName}': Операция была прервана: {ex.Message} (журнал  № {errorId})");

				throw;
			}
		}

		private ExcelFile FormExcelFile(OMImportDataLog import)
		{
			ExcelFile mainExcelFile;
			var zipFileStream =
				FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
			using (var filesFromZip = ZipFile.Read(zipFileStream, new ReadOptions {Encoding = Encoding.GetEncoding("cp866")}))
			{
				if (filesFromZip.Count == 0)
					throw new Exception("Передан пустой zip-файл");

				Log.Debug($"Внутри zip архива {filesFromZip.Count} файлов.");

				var firstFile = filesFromZip.First();
				var stream = new MemoryStream();
				firstFile.Extract(stream);
				stream.Seek(0, SeekOrigin.Begin);

				mainExcelFile = ExcelFile.Load(stream, LoadOptions.XlsxDefault);
				var mainWorksheet = mainExcelFile.Worksheets[0];
				var mainCurrentRowIndex = CommonSdks.DataExportCommon.GetLastUsedRowIndex(mainWorksheet) + 1;
				Log.Debug("Создан Excel файл на основе {FileName}", firstFile.FileName);

				foreach (var file in filesFromZip.Skip(1).ToList())
				{
					Log.Debug("Добавление информации из файла {FileName}", file.FileName);
					stream = new MemoryStream();
					file.Extract(stream);
					stream.Seek(0, SeekOrigin.Begin);
					var currentExcelFile = ExcelFile.Load(stream, LoadOptions.XlsxDefault);
					var currentLastUsedRowIndex = CommonSdks.DataExportCommon.GetLastUsedRowIndex(currentExcelFile.Worksheets[0]);
					var currentLastUsedColIndex = CommonSdks.DataExportCommon.GetLastUsedColumnIndex(currentExcelFile.Worksheets[0]);
					for (var i = 1; i <= currentLastUsedRowIndex; i++)
					{
						for (var j = 0; j <= currentLastUsedColIndex; j++)
						{
							mainWorksheet.Rows[mainCurrentRowIndex].Cells[j].Value = currentExcelFile.Worksheets[0].Rows[i].Cells[j].Value;
						}

						mainCurrentRowIndex++;
					}

					Log.Debug("Обработка файла {FileName} завершена", file.FileName);
				}
			}
			Log.Debug("Общий Excel файл сформирован.");

			return mainExcelFile;
		}
	}
}
