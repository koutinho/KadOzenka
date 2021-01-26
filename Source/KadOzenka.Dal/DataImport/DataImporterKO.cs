using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport.Dto;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Tours;
using ObjectModel.Common;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport
{
    public static class DataImporterKO
    {
	    private static readonly long _rowCountForBackgroundLoading = 1000;
		private static TourFactorService _tourFactorService { get; set; }

        private static TourFactorService TourFactorService => _tourFactorService ?? (_tourFactorService = new TourFactorService());

        /// <summary>
        /// Импорт значений меток по фактору и группе из Excel
        /// groupId - Идентификатор группы
        /// factorId - Идентификатор фактора
        /// deleteOld - Признак удаления старых данных
        /// </summary>
        public static Stream ImportDataMarkerFromExcel(ExcelFile excelFile, string registerViewId, int mainRegisterId, 
	        long groupId, long factorId, bool deleteOld)
        {
			var import = CreateDataFileImport(excelFile, registerViewId, mainRegisterId);
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

				mainWorkSheet.Rows[0].Cells[maxColumns].SetValue($"Результат сохранения");
		        mainWorkSheet.Rows[0].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All,
			        SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

		        var objs = new ModelFactorsService().GetMarks(groupId, factorId);
		        if (deleteOld)
		        {
			        Parallel.ForEach(objs, options, obj => { obj.Destroy(); });
			        objs.Clear();
		        }
		        var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
				Parallel.ForEach(mainWorkSheet.Rows, options, row =>
		        {
			        try
			        {
						if (row.Index != 0 && row.Index <= lastUsedRowIndex) //все, кроме заголовков и пустых строк в конце страницы
						{
					        string value = mainWorkSheet.Rows[row.Index].Cells[0].Value.ParseToString();
					        string metka = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString()?.TrimEnd('0');

					        if (!string.IsNullOrWhiteSpace(metka) && !metka.TryParseToDecimal(out _))
					        {
						        AddErrorToExcel(mainWorkSheet, row.Index, maxColumns, "Метку нельзя привести к числовому типу. Значение не сохранено.");
								return;
					        }

					        var metkaNumber = metka.ParseToDecimalNullable();
					        if (metkaNumber.GetValueOrDefault() == 0)
					        {
						        AddErrorToExcel(mainWorkSheet, row.Index, maxColumns, "Нельзя сохранить пустое или нулевое значение метки.");
						        return;
							}
					        if (string.IsNullOrWhiteSpace(value))
					        {
						        AddErrorToExcel(mainWorkSheet, row.Index, maxColumns, "Значение не может быть пустым.");
						        return;
					        }

							var existObject = objs.Find(x => x.GroupId == groupId && x.ValueFactor.ToUpper() == value.ToUpper());
					        bool newobj = false;
					        if (existObject == null)
					        {
						        existObject = new ObjectModel.KO.OMMarkCatalog
						        {
							        Id = -1,
							        FactorId = factorId,
									GroupId = groupId,
									ValueFactor = value.ToUpper(),
							        MetkaFactor = metkaNumber
								};
						        existObject.Save();
						        newobj = true;
					        }
					        else
					        {
						        existObject.MetkaFactor = metkaNumber;
						        existObject.Save();
					        }

					        if (newobj)
					        {
						        try
						        {
							        lock (locked)
							        {
								        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Новый объект");
								        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern
									        .SetSolid(SpreadsheetColor.FromName(ColorName.LightGreen));
								        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders
									        .SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
										        LineStyle.Thin);
							        }
						        }
						        catch
						        {

						        }
					        }
					        else
					        {
						        try
						        {
							        lock (locked)
							        {
								        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Обновлено");
								        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern
									        .SetSolid(SpreadsheetColor.FromName(ColorName.Yellow));
								        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders
									        .SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
										        LineStyle.Thin);
							        }
						        }
						        catch
						        {

						        }
					        }
				        }
			        }
			        catch (Exception ex)
			        {
				        long errorId = ErrorManager.LogError(ex);
				        lock (locked)
				        {
					        AddErrorToExcel(mainWorkSheet, row.Index, maxColumns, $"{ex.Message} (подробно в журнале №{errorId})");
				        }
			        }
		        });
		        
		        excelFile.Save(streamResult, SaveOptions.XlsxDefault);
		        streamResult.Seek(0, SeekOrigin.Begin);
		        SaveResultFile(import, streamResult);
	        }
	        catch (Exception ex)
	        {
		        LogError(ex, import);
		        throw;
	        }

	        return streamResult;
        }

        private static void AddErrorToExcel(ExcelWorksheet sheet, int rowIndex, int columnIndex, string message)
        {
	        sheet.Rows[rowIndex].Cells[columnIndex].SetValue(message);
	        sheet.Rows[rowIndex].Cells[columnIndex].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.Red));
	        sheet.Rows[rowIndex].Cells[columnIndex].Style.Borders.SetBorders(MultipleBorders.All,
		        SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
		}

        /// <summary>
        /// Импорт группы из Excel
        /// </summary>
		public static long ImportDataFromExcel(Stream stream, ImportDataFromExcelDto settings)
        {
	        var import = CreateDataFileImport(stream, settings);
	        stream.Seek(0, SeekOrigin.Begin);
	        var excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());

	        new ImportKoFactoryCreator().ExecuteImport(excelFile, settings, import);

	        return import.Id;
        }

        public static bool UseLongProcessForImportDataGroup(Stream fileStream)
        {
	        var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
	        var mainWorkSheet = excelFile.Worksheets[0];
	        return mainWorkSheet.Rows.Count > _rowCountForBackgroundLoading;
		}

        public static OMImportDataLog CreateDataFileImport(Stream stream, ImportDataFromExcelDto settings)
        {
	        stream.Seek(0, SeekOrigin.Begin);
	        var excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
	        excelFile.DocumentProperties.Custom["FileName"] = settings.FileName;

	        return CreateDataFileImport(excelFile, settings.RegisterViewId, settings.MainRegisterId);
        }

		#region Support Methods

		public static void SaveGroupAsObjectAttribute(long? attributeId, long? objectId, long? taskId, string numberGroup)
        {
            if(attributeId == null || objectId == null)
                return;

            var task = OMTask.Where(x => x.Id == taskId).Select(x => x.DocumentId).ExecuteFirstOrDefault();

            new GbuObjectAttribute
            {
                ObjectId = objectId.Value,
                AttributeId = attributeId.Value,
                StringValue = numberGroup,
                S = DateTime.Now,
                Ot = DateTime.Now,
                ChangeDocId = task == null ? -1 : task.DocumentId ?? -1,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now
            }.Save();
        }

		private static OMImportDataLog CreateDataFileImport(ExcelFile excelFile, string registerViewId, int mainRegisterId)
        {
	        var fileName = excelFile.DocumentProperties.Custom["FileName"].ToString();
	        MemoryStream str = new MemoryStream();
	        excelFile.Save(str, SaveOptions.XlsxDefault);
	        str.Seek(0, SeekOrigin.Begin);

	        var import = new OMImportDataLog()
	        {
		        UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
		        Status_Code = ObjectModel.Directory.Common.ImportStatus.Added,
		        DataFileTitle = DataImporterCommon.GetDataFileTitle(fileName),
		        FileExtension = DataImporterCommon.GetFileExtension(fileName),
		        DateCreated = DateTime.Now,
		        RegisterViewId = registerViewId,
		        MainRegisterId = mainRegisterId
	        };
	        import.Save();

	        import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
	        FileStorageManager.Save(str, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
	        import.Save();

	        return import;
        }

		public static void SaveResultFile(OMImportDataLog import, MemoryStream streamResult)
        {
	        import.ResultFileTitle = DataImporterCommon.GetFileResultTitleFromDataTitle(import);
	        import.ResultFileName = DataImporterCommon.GetStorageResultFileName(import.Id);
	        import.DateFinished = DateTime.Now;
	        FileStorageManager.Save(streamResult, DataImporterCommon.FileStorageName, import.DateFinished.Value, import.ResultFileName);
	        import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
	        import.Save();
        }

        public static void LogError(Exception ex, OMImportDataLog import)
        {
	        long errorId = ErrorManager.LogError(ex);
	        import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
	        import.DateFinished = DateTime.Now;
	        import.ResultMessage = $"{ex.Message}{($" (журнал № {errorId})")}";
	        import.Save();
        }

        #endregion
    }
}

