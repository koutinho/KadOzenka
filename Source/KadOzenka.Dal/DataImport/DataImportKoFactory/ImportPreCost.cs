using System;
using System.Collections.Generic;
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
	public class ImportPreCost: IDataImportKo
	{
		public ImportPreCost()
		{
		}

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
							string preCadastralCost = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
							string preUpks = mainWorkSheet.Rows[row.Index].Cells[2].Value.ParseToString();
							bool setPreCost = false;
							bool setPreUpks = false;
							bool findObj = false;
							OMUnit unit = OMUnit
								.Where(x => x.TourId == settings.TourId.GetValueOrDefault() &&
											x.Status_Code == settings.UnitStatus &&
											x.CadastralNumber == cadastralNumber).SelectAll().ExecuteFirstOrDefault();
							if (unit != null)
							{
								findObj = true;
								if (decimal.TryParse(preCadastralCost, out var preCost))
								{
									unit.CadastralCostPre = preCost;
									setPreCost = true;
								}

								if (decimal.TryParse(preUpks, out var preUpksDecimal))
								{
									unit.UpksPre = preUpksDecimal;
									setPreUpks = true;
								}
								unit.Save();
							}

							if (setPreCost && setPreUpks)
							{
								try
								{
									lock (locked)
									{
										ImportKoCommon.AddSuccessCell(mainWorkSheet, row.Index, maxColumns, "КС (предварительная) и УПКС (предварительный) обновлены");
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
										ImportKoCommon.AddWarningCell(mainWorkSheet, row.Index, maxColumns, findObj 
											? "Указанный объект не найден" : setPreCost ? "КС (предварительная) не найдена" : "УПКС (предварительный) не найден");
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

				List<OMUnit> Objs = new List<OMUnit>();
				foreach (long taskId in settings.TaskFilter)
				{
					Objs.AddRange(OMUnit.Where(x => x.TaskId == taskId).SelectAll().Execute());
				}
				var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
				Parallel.ForEach(mainWorkSheet.Rows, options, row =>
				{
					try
					{
						if (row.Index != 0 && row.Index <= lastUsedRowIndex) //все, кроме заголовков и пустых строк в конце страницы
						{
							string cadastralNumber = mainWorkSheet.Rows[row.Index].Cells[0].Value.ParseToString();
							string preCadastralCost = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
							string preUpks = mainWorkSheet.Rows[row.Index].Cells[2].Value.ParseToString();
							bool setPreCost = false;
							bool setPreUpks = false;
							bool findObj = false;

							OMUnit unit = Objs.Find(x => x.CadastralNumber == cadastralNumber);
							if (unit != null)
							{
								findObj = true;
								if (decimal.TryParse(preCadastralCost, out var preCost))
								{
									unit.CadastralCostPre = preCost;
									setPreCost = true;
								}

								if (decimal.TryParse(preUpks, out var preUpksDecimal))
								{
									unit.UpksPre = preUpksDecimal;
									setPreUpks = true;
								}
								unit.Save();
							}


							if (setPreCost && setPreUpks)
							{
								try
								{
									lock (locked)
									{
										ImportKoCommon.AddSuccessCell(mainWorkSheet, row.Index, maxColumns, "КС (предварительная) и УПКС (предварительный) обновлены");
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
										ImportKoCommon.AddWarningCell(mainWorkSheet, row.Index, maxColumns, findObj
											? "Указанный объект не найден" : setPreCost ? "КС (предварительная) не найдена" : "УПКС (предварительный) не найден");
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