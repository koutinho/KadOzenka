using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.OutliersChecking.Dto;
using MarketPlaceBusiness.Common;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Directory.Common;
using ObjectModel.Market;
using Serilog;
using EnumExtensions = Core.Shared.Extensions.EnumExtensions;

namespace KadOzenka.Dal.OutliersChecking
{
	public class OutliersCheckingSettingsService
	{
		private readonly ILogger _log = Log.ForContext<OutliersCheckingSettingsService>();

		public List<OutliersCheckingSettingDto> GetOutliersCheckingSettings()
		{
			var settings = OMCoefficientsOutliersChecking.Where(x => true)
				.SelectAll()
				.Execute();

			return settings.Select(x => new OutliersCheckingSettingDto
			{
				Id = x.Id,
				Zone = x.Zone,
				District = x.District_Code,
				Region = x.Region_Code,
				MinDeltaCoef = x.MinDeltaCoef,
				MaxDeltaCoef = x.MaxDeltaCoef
			}).OrderBy(x => x.Zone).ThenBy(x => x.District).ThenBy(x => x.Region).ToList();
		}

		public void UpdateOutliersCheckingSettings(OutliersCheckingSettingDto dto)
		{
			var setting = OMCoefficientsOutliersChecking
				.Where(x => x.Zone == dto.Zone && x.District_Code == dto.District && x.Region_Code == dto.Region)
				.SelectAll().ExecuteFirstOrDefault();
			if (setting == null)
			{
				throw new Exception($"Не найдена запись для 'Зона {dto.Zone}_{dto.District.GetShortTitle()}_{dto.Region.GetEnumDescription()}'");
			}

			setting.MinDeltaCoef = dto.MinDeltaCoef;
			setting.MaxDeltaCoef = dto.MaxDeltaCoef;
			setting.Save();
		}

		public long ImportOutliersCheckingSettingsFromExcel(Stream stream, OutliersCheckingSettingImportFromExcelDto settingsDto)
		{
			_log.Information("Старт импорта значений коэффициентов для процедуры проверки на вылеты. Настройки: {Settings}", JsonConvert.SerializeObject(settingsDto));
			var import = DataImporterCommon.CreateDataFileImport(stream, settingsDto.FileName, Consts.RegisterId, "MarketObjects");
			try
			{
				if (settingsDto.DeleteOldValues)
				{
					var settings = OMCoefficientsOutliersChecking.Where(x => true)
						.SelectAll()
						.Execute();
					foreach (var omCoefficientsOutliersChecking in settings)
					{
						omCoefficientsOutliersChecking.Destroy();
					}
					_log.ForContext("DeletedSettingsCount", settings.Count).Debug("Выполнено удаление прежних настроек коэффициентов");
				}

				import.Status_Code = ImportStatus.Running;
				import.DateStarted = DateTime.Now;
				import.Save();

				var resFileStream = ImportReferenceItemsFromExcel(stream, settingsDto);

				_log.Debug("Сохранение результата");
				DataImporterCommon.SaveResultFile(import, resFileStream);
				import.Status_Code = ImportStatus.Completed;
				import.Save();

				_log.Debug("Отправка сообщения пользователю");
				DataImporterCommon.SendResultNotification(import,
					$"Результат загрузки настроек коэффициентов для проверки на вылеты от ({DateTime.Now.GetString()})");
			}
			catch (Exception ex)
			{
				long errorId = ErrorManager.LogError(ex);
				import.Status_Code = ImportStatus.Faulted;
				import.DateFinished = DateTime.Now;
				import.ResultMessage = $"{ex.Message}{($" (журнал № {errorId})")}";
				import.Save();

				_log.ForContext("ErrorId", errorId).Error(ex, "Импорт значений коэффициентов для процедуры проверки на вылеты завершен с ошибкой");

				throw;
			}

			_log.ForContext("ImportId", import.Id)
				.Information("Импорт значений коэффициентов для процедуры проверки на вылеты завершен");

			return import.Id;
		}

		private Stream ImportReferenceItemsFromExcel(Stream fileStream, OutliersCheckingSettingImportFromExcelDto settingsDto)
		{
			_log.Debug("Выполнение импорта значений из файла");
			fileStream.Seek(0, SeekOrigin.Begin);
			var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
			var mainWorkSheet = excelFile.Worksheets[0];

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
					columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
			}

			mainWorkSheet.Rows[0].Cells[maxColumns].SetValue("Результат сохранения");
			var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
			var dataRows = mainWorkSheet.Rows.Where(x => x.Index > 0 && x.Index <= lastUsedRowIndex);

			var existedValues = OMCoefficientsOutliersChecking.Where(x => true)
				.SelectAll()
				.Execute();

			Parallel.ForEach(dataRows, options, row =>
			{
				try
				{
					var zoneCellValue = mainWorkSheet.Rows[row.Index]
						.Cells[columnNames.IndexOf(settingsDto.ZoneColumnName)];
					var districtCellValue = mainWorkSheet.Rows[row.Index]
						.Cells[columnNames.IndexOf(settingsDto.DistrictColumnName)];
					var regionCellValue = mainWorkSheet.Rows[row.Index]
						.Cells[columnNames.IndexOf(settingsDto.RegionColumnName)];
					var minCoefCellValue = mainWorkSheet.Rows[row.Index]
						.Cells[columnNames.IndexOf(settingsDto.MinCoefColumnName)];
					var maxCoefCellValue = mainWorkSheet.Rows[row.Index]
						.Cells[columnNames.IndexOf(settingsDto.MaxCoefColumnName)];

					var zone = zoneCellValue.Value.ParseToLongNullable();
					if (!zone.HasValue)
					{
						throw new Exception(
							$"Значение '{zoneCellValue.Value}' не может быть приведено к целому типу");
					}
					if (zone < 1 || zone > 5)
					{
						throw new Exception(
							$"Некорректное значение для зоны: '{zoneCellValue.Value}'");
					}

					var districtShortTitle = districtCellValue.Value.ParseToStringNullable();
					var district = Extentions.EnumExtensions.GetEnumByShortTitle<Hunteds>(districtShortTitle);
					if (district == 0)
					{
						throw new Exception(
							$"Значение '{districtCellValue.Value}' не может быть приведено к административному округу");
					}
					var districtVal = (Hunteds)district;

					var regionDescription = regionCellValue.Value.ParseToStringNullable();
					var region = EnumExtensions.GetEnumByDescription<Districts>(regionDescription);
					if (region == 0)
					{
						throw new Exception(
							$"Значение '{regionCellValue.Value}' не может быть приведено к району");
					}
					var regionVal = (Districts)region;

					decimal? minCoefValue = null;
					if (minCoefCellValue.Value != null)
					{
						if (!minCoefCellValue.Value.TryParseToDecimal(out var minCoef))
						{
							throw new Exception(
								$"Некорректное значение для коэффициента: '{minCoefCellValue.Value}'");
						}

						minCoefValue = minCoef;
					}

					decimal? maxCoefValue = null;
					if (maxCoefCellValue.Value != null)
					{
						if (!maxCoefCellValue.Value.TryParseToDecimal(out var maxCoef))
						{
							throw new Exception(
								$"Некорректное значение для коэффициента: '{maxCoefCellValue.Value}'");
						}

						maxCoefValue = maxCoef;
					}

					var existedValue = existedValues.FirstOrDefault(x =>
						x.Zone == zone && x.District_Code == districtVal && x.Region_Code == regionVal);
					if (existedValue != null)
					{
						existedValue.MinDeltaCoef = minCoefValue;
						existedValue.MaxDeltaCoef = maxCoefValue;
						existedValue.Save();

						lock (locked)
						{
							mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Значение успешно обновлено");
							_log.ForContext("Zone", existedValue.Zone)
								.ForContext("District", existedValue.District_Code.GetShortTitle())
								.ForContext("Region", existedValue.Region_Code.GetEnumDescription())
								.ForContext("MinDeltaCoef", existedValue.MinDeltaCoef)
								.ForContext("MaxDeltaCoef", existedValue.MaxDeltaCoef)
								.ForContext("RowIndex", row.Index)
								.Verbose("Значение успешно обновлено");
						}
					}
					else
					{
						var newValue = new OMCoefficientsOutliersChecking
						{
							Zone = zone.Value,
							District_Code = districtVal,
							Region_Code = regionVal,
							MinDeltaCoef = minCoefValue,
							MaxDeltaCoef = maxCoefValue
						};
						newValue.Save();

						lock (locked)
						{
							mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Значение успешно создано");
							_log.ForContext("Zone", newValue.Zone)
								.ForContext("District", newValue.District_Code.GetShortTitle())
								.ForContext("Region", newValue.Region_Code.GetEnumDescription())
								.ForContext("MinDeltaCoef", newValue.MinDeltaCoef)
								.ForContext("MaxDeltaCoef", newValue.MaxDeltaCoef)
								.ForContext("RowIndex", row.Index)
								.Verbose("Значение успешно создано");
						}
					}
				}
				catch (Exception ex)
				{
					lock (locked)
					{
						_log.Warning(ex, "Не удалось обработать данные из строки {RowIndex}: {Error}", row.Index, ex.Message);
						mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"Ошибка: {ex.Message}");
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
			_log.Debug("Выполнение импорта значений из файла завершено");

			return stream;
		}
	}
}
