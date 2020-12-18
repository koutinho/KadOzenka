using System.Collections.Generic;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using ObjectModel.Directory;
using ObjectModel.Gbu;

namespace KadOzenka.BlFrontEnd.GbuTest
{
	public static class ChangeTypeToCadastralQuarter
	{
		public static void Perform(string filePath)
		{
			//Загружаем файл и считываем из 1 столбца значения кадастровых номеров объектов
			ExcelFile excelFile = ExcelFile.Load(filePath, new XlsxLoadOptions());
			var mainWorkSheet = excelFile.Worksheets[0];
			var cadastralNumbers = new List<string>();
			var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
			for (var i = 1; i <= lastUsedRowIndex; i++)
			{
				var val = mainWorkSheet.Rows[i].Cells[1].StringValue;
				cadastralNumbers.Add(val);
			}

			if (!cadastralNumbers.IsEmpty())
			{
				//Ищем гбу объекты по кадастровым номерам из файла
				var gbuObjects = OMMainObject.Where(x => cadastralNumbers.Contains(x.CadastralNumber))
					.SelectAll().Execute();
				//Для найденных объектов исправляем тип на Кадастровый квартал
				foreach (var gbuObject in gbuObjects)
				{
					gbuObject.ObjectType_Code = PropertyTypes.CadastralQuartal;
					gbuObject.Save();
				}
			}
		}
	}
}
