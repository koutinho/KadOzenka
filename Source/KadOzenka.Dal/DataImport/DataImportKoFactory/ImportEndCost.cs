using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport.DataImportKoFactory.ImportKoFactoryCommon;
using KadOzenka.Dal.DataImport.DataImportKoFactory.Interface;
using KadOzenka.Dal.DataImport.Dto;
using ObjectModel.Common;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImportKoFactory
{
	public class ImportEndCost: IDataImportKo
	{
		public Stream ImportDataFromExcelByUnitStatus(ExcelFile excelFile, ImportDataFromExcelDto settings, OMImportDataLog import)
		{
			MemoryStream streamResult = new MemoryStream();
			var locked = new object();
			try
			{
				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
				import.DateStarted = DateTime.Now;
				import.Save();

				var mainWorkSheet = excelFile.Worksheets[0];

				CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
				ParallelOptions options = new ParallelOptions
				{
					CancellationToken = cancelTokenSource.Token,
					MaxDegreeOfParallelism = 10
				};


				int maxColumns = DataExportCommon.GetLastUsedColumnIndex(mainWorkSheet) + 1;
				ImportKoCommon.AddSuccessHeaderColumn(mainWorkSheet, maxColumns);


				var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
				Parallel.ForEach(mainWorkSheet.Rows, options, row =>
				{
					try
					{
						if (row.Index != 0 && row.Index <= lastUsedRowIndex) //все, кроме заголовков и пустых строк в конце страницы
						{
							string cadastralNumber = mainWorkSheet.Rows[row.Index].Cells[0].Value.ParseToString();
							string cadastralCost = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
							string upks = mainWorkSheet.Rows[row.Index].Cells[2].Value.ParseToString();
							bool setCost = false;
							bool setUpks = false;
							bool findObj = false;
							OMUnit unit = OMUnit.Where(x => x.TourId == settings.TourId.GetValueOrDefault() &&
							                                x.Status_Code == settings.UnitStatus &&
							                                x.CadastralNumber == cadastralNumber).SelectAll().ExecuteFirstOrDefault();
							if (unit != null)
							{
								findObj = true;
								if (cadastralCost.TryParseToDecimal(out var cost))
								{
									unit.CadastralCost = cost;
									setCost = true;
								}

								if (upks.TryParseToDecimal(out var upksDecimal))
								{
									unit.Upks = upksDecimal;
									setUpks = true;
								}

								if (setUpks && setCost && upksDecimal > cost)
								{
									lock (locked)
									{
										ImportKoCommon.AddErrorCell(mainWorkSheet, row.Index, maxColumns, "УПКС не может быть больше Кадастровой стоимости");
										return;
									}
								}

								if (setCost || setUpks)
								{
									unit.Save();
								}
							}

							if (setCost && setUpks)
							{
								try
								{
									lock (locked)
									{
										ImportKoCommon.AddSuccessCell(mainWorkSheet, row.Index, maxColumns, "КС (окончательная) и УПКС (окончательный) обновлены");
									}
								}
								catch
								{
									// обработка будет выше
								}
							}
							else
							{
								try
								{
									lock (locked)
									{
										string msg = !setCost & !setUpks
											? "КС (окончательная) не установлена и УПКС(окончательный) не установлен"
											: !setCost
												? "КС (окончательная) не установлена"
												: "УПКС (окончательный) не установлен";
										ImportKoCommon.AddWarningCell(mainWorkSheet, row.Index, maxColumns, !findObj
											? "Указанный объект не найден" : msg);
									}
								}
								catch
								{
									// обработка будет выше
								}

							}
						}
					}
					catch (Exception ex)
					{
						long errorId = ErrorManager.LogError(ex);
						lock (locked)
						{
							ImportKoCommon.AddErrorCell(mainWorkSheet, row.Index, maxColumns, $"{ex.Message} (подробно в журнале №{errorId})");
						}
					}
				});

				excelFile.Save(streamResult, SaveOptions.XlsxDefault);
				streamResult.Seek(0, SeekOrigin.Begin);
				DataImporterKO.SaveResultFile(import, streamResult);
			}
			catch (Exception ex)
			{
				DataImporterKO.LogError(ex, import);
				throw;
			}

			return streamResult;
		}

		public Stream ImportDataFromExcelByTaskFilter(ExcelFile excelFile, ImportDataFromExcelDto settings, OMImportDataLog import)
		{
			MemoryStream streamResult = new MemoryStream();
			var locked = new object();
			try
			{
				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
				import.DateStarted = DateTime.Now;
				import.Save();

				var mainWorkSheet = excelFile.Worksheets[0];

				CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
				ParallelOptions options = new ParallelOptions
				{
					CancellationToken = cancelTokenSource.Token,
					MaxDegreeOfParallelism = 10
				};

				int maxColumns = DataExportCommon.GetLastUsedColumnIndex(mainWorkSheet) + 1;
				ImportKoCommon.AddSuccessHeaderColumn(mainWorkSheet, maxColumns);

				var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
				Parallel.ForEach(mainWorkSheet.Rows, options, row =>
				{
					try
					{
						if (row.Index != 0 && row.Index <= lastUsedRowIndex) //все, кроме заголовков и пустых строк в конце страницы
						{
							string cadastralNumber = mainWorkSheet.Rows[row.Index].Cells[0].Value.ParseToString();
							string cadastralCost = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
							string upks = mainWorkSheet.Rows[row.Index].Cells[2].Value.ParseToString();
							bool setCost = false;
							bool setUpks = false;
							bool findObj = false;

							OMUnit unit = OMUnit.Where(ImportKoCommon.GetConditionByTaskAndKn(settings.TaskFilter, cadastralNumber)).SelectAll().ExecuteFirstOrDefault();
							if (unit != null)
							{
								findObj = true;
								if (cadastralCost.TryParseToDecimal(out var cost))
								{
									unit.CadastralCost = cost;
									setCost = true;
								}

								if (upks.TryParseToDecimal(out var upksDecimal))
								{
									unit.Upks = upksDecimal;
									setUpks = true;
								}

								if (setUpks && setCost && upksDecimal > cost)
								{
									lock (locked)
									{
										ImportKoCommon.AddErrorCell(mainWorkSheet, row.Index, maxColumns, "УПКС не может быть больше Кадастровой стоимости");
										return;
									}
								}

								if (setCost || setUpks)
								{
									unit.Save();
								}
							
							}


							if (setCost && setUpks)
							{
								try
								{
									lock (locked)
									{
										ImportKoCommon.AddSuccessCell(mainWorkSheet, row.Index, maxColumns, "КС (окончательная) и УПКС (окончательный) обновлены");
									}
								}
								catch
								{
									// обработка будет выше
								}
							}
							else
							{
								try
								{
									lock (locked)
									{
										string msg = !setCost & !setUpks
											? "КС (окончательная) не установлена и УПКС(окончательный) не установлен"
											: !setCost
												? "КС (окончательная) не установлена"
												: "УПКС (окончательный) не установлен";
										ImportKoCommon.AddWarningCell(mainWorkSheet, row.Index, maxColumns, !findObj
											? "Указанный объект не найден" : msg);
									}
								}
								catch
								{
									// обработка будет выше
								}
							}
						}
					}
					catch (Exception ex)
					{
						long errorId = ErrorManager.LogError(ex);
						lock (locked)
						{
							ImportKoCommon.AddErrorCell(mainWorkSheet, row.Index, maxColumns, $"{ex.Message} (подробно в журнале №{errorId})");
						}
					}
				});

				excelFile.Save(streamResult, SaveOptions.XlsxDefault);
				streamResult.Seek(0, SeekOrigin.Begin);
				DataImporterKO.SaveResultFile(import, streamResult);
			}
			catch (Exception ex)
			{
				DataImporterKO.LogError(ex, import);
				throw;
			}

			return streamResult;
		}
	}
}