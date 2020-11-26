using System.IO;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport.Dto;
using ObjectModel.Common;

namespace KadOzenka.Dal.DataImport.DataImportKoFactory.Interface
{
	public interface IDataImportKo
	{
		Stream ImportDataFromExcelByUnitStatus(ExcelFile excelFile, ImportDataFromExcelDto settings, OMImportDataLog import);
		Stream ImportDataFromExcelByTaskFilter(ExcelFile excelFile, ImportDataFromExcelDto settings, OMImportDataLog import);
	}
}