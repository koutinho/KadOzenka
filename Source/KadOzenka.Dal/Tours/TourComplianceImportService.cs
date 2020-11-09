using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Messages;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Tours.Dto;
using ObjectModel.Common;
using ObjectModel.Directory;
using ObjectModel.Directory.Common;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tours
{
	public class TourComplianceImportService
	{
		public int AllRows { get; private set; } = 1;
		public int CurrentRow { get; private set; }

		private static readonly int MainRegisterId = OMComplianceGuide.GetRegisterId();
		private static readonly string RegisterViewId = "KoComplianceGuide";

		public void ImportComplianceFromFile(Stream fileStream, ImportFileComplianceDto fileDto, PropertyTypes objectType, long tourId)
		{
			if (tourId == 0)
			{
				throw new Exception("Не задан ид тура");
			}

			var import = CreateDataFileImport(fileStream, fileDto.FileName);
			try
			{
				import.Status_Code = ImportStatus.Running;
				import.DateStarted = DateTime.Now;
				import.Save();

				fileStream.Seek(0, SeekOrigin.Begin);
				var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
				var mainWorkSheet = excelFile.Worksheets[0];

				var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
				AllRows = lastUsedRowIndex + 1;
				CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
				ParallelOptions options = new ParallelOptions
				{
					CancellationToken = cancelTokenSource.Token,
					MaxDegreeOfParallelism = 10
				};
				object locked = new object();

				int maxColumns = DataExportCommon.GetLastUsedColumnIndex(mainWorkSheet) + 1;
				var columnNames = new List<string>();
				for (var i = 0; i < maxColumns; i++)
				{
					if (mainWorkSheet.Rows[0].Cells[i].Value != null)
						columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value?.ToString());
				}

				mainWorkSheet.Rows[0].Cells[maxColumns].SetValue("Результат сохранения");
				
				var dataRows = mainWorkSheet.Rows.Where(x => x.Index > 0 && x.Index <= lastUsedRowIndex);

				Parallel.ForEach(dataRows, options, row =>
				{
					try
					{
						var code = mainWorkSheet.Rows[row.Index]
							.Cells[columnNames.IndexOf(fileDto.CodeColumnName)];
						var group = mainWorkSheet.Rows[row.Index]
							.Cells[columnNames.IndexOf(fileDto.GroupColumnName)];
						ExcelCell territoryType = null;
						if (!fileDto.TerritoryTypeColumnName.IsNullOrEmpty())
						{
							territoryType = mainWorkSheet.Rows[row.Index]
								.Cells[columnNames.IndexOf(fileDto.TerritoryTypeColumnName)];
						}

						ExcelCell typeRoom = null;
						if (fileDto.RoomTypeColumnName.IsNotEmpty())
						{
							typeRoom = mainWorkSheet.Rows[row.Index]
								.Cells[columnNames.IndexOf(fileDto.RoomTypeColumnName)];
						}

						if (code.Value == null || group.Value == null)
						{
							throw new Exception("Нет обязательных параметров");
						}

						if (!decimal.TryParse(group.Value?.ToString().Replace('.', ','), out var d))
						{
							throw new Exception("Неверное значение группы");
						}

						KoTypeOfRoom correctTypeOfRoom = KoTypeOfRoom.None;
						if (typeRoom != null && typeRoom.Value != null &&
						    int.TryParse(typeRoom.Value?.ToString(), out var val))
						{
							switch ((KoTypeOfRoom) val)
							{
								case KoTypeOfRoom.Residential:
									correctTypeOfRoom = KoTypeOfRoom.Residential;
									break;
								case KoTypeOfRoom.NotResidential:
									correctTypeOfRoom = KoTypeOfRoom.NotResidential;
									break;
							}
						}
						else if (typeRoom != null)
						{
							switch (typeRoom.Value?.ToString())
							{
								case "Жилое":
									correctTypeOfRoom = KoTypeOfRoom.Residential;
									break;
								case "Нежилое":
									correctTypeOfRoom = KoTypeOfRoom.NotResidential;
									break;
							}
						}

						if (OMComplianceGuide.Where(x => x.Code == code.StringValue && x.SubGroup == group.StringValue
						                                                            && x.TourId == tourId &&
						                                                            x.TypeRoom_Code ==
						                                                            correctTypeOfRoom &&
						                                                            x.TypeProperty_Code == objectType)
							.ExecuteFirstOrDefault() != null)
						{
							lock (locked)
							{
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Значение уже существует");
							}

							return;
						}

						new OMComplianceGuide
						{
							Code = code.StringValue,
							SubGroup = group.StringValue.Replace(',', '.'),
							TypeRoom_Code = correctTypeOfRoom,
							TypeProperty_Code = objectType,
							TourId = tourId,
							TerritoryType = territoryType?.Value != null ? territoryType.StringValue : ""
						}.Save();

						lock (locked)
						{
							mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Значение сохранено");
						}

					}
					catch (Exception e)
					{
						lock (locked)
						{
							mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"Ошибка: {e.Message}");
							for (int i = 0; i < maxColumns; i++)
							{
								mainWorkSheet.Rows[row.Index].Cells[i].Style.FillPattern
									.SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
							}

						}
					}
				});

				MemoryStream stream;
				stream = new MemoryStream();
				excelFile.Save(stream, SaveOptions.XlsxDefault);
				stream.Seek(0, SeekOrigin.Begin);

				SaveResultFile(import, stream);
				SendImportResultNotification(import.DataFileTitle, import.Id);
			}
			catch (Exception ex)
			{
				long errorId = ErrorManager.LogError(ex);
				import.Status_Code = ImportStatus.Faulted;
				import.DateFinished = DateTime.Now;
				import.ResultMessage = $"{ex.Message}{($" (журнал № {errorId})")}";
				import.Save();

				throw;
			}
		}

		private static OMImportDataLog CreateDataFileImport(Stream fileStream, string fileSavedName)
		{
			var import = new OMImportDataLog()
			{
				UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
				Status_Code = ImportStatus.Added,
				DataFileTitle = DataImporterCommon.GetDataFileTitle(fileSavedName),
				FileExtension = DataImporterCommon.GetFileExtension(fileSavedName),
				DateCreated = DateTime.Now,
				RegisterViewId = RegisterViewId,
				MainRegisterId = MainRegisterId
			};
			import.Save();

			import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
			FileStorageManager.Save(fileStream, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
			import.Save();

			return import;
		}

		private static void SaveResultFile(OMImportDataLog import, MemoryStream streamResult)
		{
			import.ResultFileTitle = DataImporterCommon.GetFileResultTitleFromDataTitle(import);
			import.ResultFileName = DataImporterCommon.GetStorageResultFileName(import.Id);
			import.DateFinished = DateTime.Now;
			FileStorageManager.Save(streamResult, DataImporterCommon.FileStorageName, import.DateFinished.Value, import.ResultFileName);
			import.Status_Code = ImportStatus.Completed;
			import.Save();
		}

		private void SendImportResultNotification(string fileName, long importId)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers =
					new MessageAddressersDto { UserIds = new long[] { SRDSession.GetCurrentUserId().GetValueOrDefault() } },
				Subject = $"Результат загрузки таблицы соответствия от ({DateTime.Now.GetString()})",
				Message = $@"Загрузка файла ""{fileName}"" была завершена.
<a href=""/DataImport/DownloadImportResultFile?importId={importId}"">Скачать результат</a>
<a href=""/DataImport/DownloadImportDataFile?importId={importId}"">Скачать исходный файл</a>",
				IsUrgent = true,
				IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
			});
		}
	}
}