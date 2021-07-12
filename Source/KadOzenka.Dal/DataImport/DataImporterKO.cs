using Core.ErrorManagment;
using Core.Main.FileStorages;
using GemBox.Spreadsheet;
using System;
using System.IO;
using Core.SRD;
using KadOzenka.Dal.DataImport.Dto;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Tours;
using ObjectModel.Common;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport
{
    public static class DataImporterKO
    {
	    private static readonly long _rowCountForBackgroundLoading = 1000;
		private static TourFactorService _tourFactorService { get; set; }

        private static TourFactorService TourFactorService => _tourFactorService ?? (_tourFactorService = new TourFactorService());


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

