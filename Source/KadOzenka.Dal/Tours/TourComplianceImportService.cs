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

			long idFile = SaveFileToStorage(fileStream, fileDto.FileName);
			fileStream.Seek(0, SeekOrigin.Begin);
			var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
			var mainWorkSheet = excelFile.Worksheets[0];

			AllRows = mainWorkSheet.Rows.Count;
			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 10
			};
			object locked = new object();

			var maxColumns = mainWorkSheet.CalculateMaxUsedColumns();
			var columnNames = new List<string>();
			for (var i = 0; i < maxColumns; i++)
			{
				columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
			}

			mainWorkSheet.Rows[0].Cells[maxColumns].SetValue("Результат сохранения");
			var dataRows = mainWorkSheet.Rows.Where(x => x.Index > 0);

			Parallel.ForEach(dataRows, options, row =>
			{
				try
				{
					var code = mainWorkSheet.Rows[row.Index]
						.Cells[columnNames.IndexOf(fileDto.CodeColumnName)];
					var group = mainWorkSheet.Rows[row.Index]
						.Cells[columnNames.IndexOf(fileDto.GroupColumnName)];
					ExcelCell typeRoom = null;
					if (fileDto.RoomTypeColumnName.IsNotEmpty())
					{
						typeRoom = mainWorkSheet.Rows[row.Index]
							.Cells[columnNames.IndexOf(fileDto.RoomTypeColumnName)];
					}

					if(code.Value == null || group.Value == null)
					{
						throw new Exception("Нет обязательных параметров");
					}

					if (!decimal.TryParse(group.Value?.ToString().Replace('.',','), out var d))
					{
						throw new Exception("Неверное значение группы");
					}

					KoTypeOfRoom correctTypeOfRoom = KoTypeOfRoom.None;
					if (typeRoom != null && typeRoom.Value !=null && int.TryParse(typeRoom.Value?.ToString(), out var val))
					{
						switch ((KoTypeOfRoom)val)
						{
							case KoTypeOfRoom.Residential: correctTypeOfRoom = KoTypeOfRoom.Residential; break;
							case KoTypeOfRoom.NotResidential: correctTypeOfRoom = KoTypeOfRoom.NotResidential; break;
						}
					} else if (typeRoom != null)
					{
						switch (typeRoom.Value.ToString())
						{
							case "Жилое": correctTypeOfRoom = KoTypeOfRoom.Residential; break;
							case "Нежилое": correctTypeOfRoom = KoTypeOfRoom.NotResidential; break;
						}
					}

					if (OMComplianceGuide.Where(x => x.Code == code.StringValue && x.SubGroup == group.StringValue
					                                                            && x.TourId == tourId &&
					                                                            x.TypeRoom_Code == correctTypeOfRoom && x.TypeProperty_Code == objectType)
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
						TourId = tourId
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
							mainWorkSheet.Rows[row.Index].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
						}

					}
				}
			});

			MemoryStream stream;
			stream = new MemoryStream();
			excelFile.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			long idResFile = SaveFileToStorage(stream, fileDto.FileName + "_Result");
			SendImportResultNotification(fileDto.FileName, idResFile, idFile);
		}


		public static long SaveFileToStorage(Stream file, string fileSavedName)
		{
			DateTime currentTime = DateTime.Now;

		
			var import = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = currentTime,
				Status_Code = ImportStatus.Added,
				DataFileName = fileSavedName,
				MainRegisterId = MainRegisterId,
				RegisterViewId = RegisterViewId
			};
			import.Save();
			FileStorageManager.Save(file, DataImporterCommon.FileStorageName, import.DateCreated, import.Id.ToString());

			return import.Id;
		}

		private void SendImportResultNotification(string fileName, long idFileRes, long idFile)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers =
					new MessageAddressersDto { UserIds = new long[] { SRDSession.GetCurrentUserId().GetValueOrDefault() } },
				Subject = $"Результат загрузки таблицы соответствия от ({DateTime.Now.GetString()})",
				Message = $@"Загрузка файла ""{fileName}"" была завершена.
<a href=""/Tour/DownloadImportedFile?idFile={idFileRes}"">Скачать результат</a>
<a href=""/Tour/DownloadImportedFile?idFile={idFile}"">Скачать исходный файл</a>",
				IsUrgent = true,
				IsEmail = true
			});
		}
	}
}