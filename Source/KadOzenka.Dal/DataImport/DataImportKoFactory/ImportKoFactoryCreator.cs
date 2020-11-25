using System;
using Core.ErrorManagment;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport.DataImportKoFactory;
using KadOzenka.Dal.DataImport.DataImportKoFactory.Interface;
using KadOzenka.Dal.DataImport.Dto;
using KadOzenka.Dal.Tours;
using ObjectModel.Common;

namespace KadOzenka.Dal.DataImport
{
	public class ImportKoFactoryCreator
	{
		private IDataImportKo _importer;

		public void ExecuteImport(ExcelFile excelFile, ImportDataFromExcelDto settings, OMImportDataLog import)
		{
			CreateImporter(settings.LoadType);
			try
			{
				if (settings.IsUnitStatusUsed)
				{
					_importer.ImportDataFromExcelByUnitStatus(excelFile, settings, import);
				}
				else
				{
					_importer.ImportDataFromExcelByTaskFilter(excelFile, settings, import);
				}
			}
			catch (Exception e)
			{
				ErrorManager.LogError("При импорте данных произошла ошибка ", settings.LoadType.ToString());
				Console.WriteLine(e);
				throw;
			}

		}

		private void CreateImporter(LoadType loadType)
		{
			switch (loadType)
			{
				case LoadType.Group: _importer = new ImportGroupNumber(new TourFactorService()); break;
				case LoadType.PreCost: _importer = new ImportPreCost(); break;
				case LoadType.EndCost: _importer = new ImportEndCost(); break;
				default: throw new NotImplementedException("Не удалось получить исполнителя, не указан тип загрузки");
			}

		}
	}
}