using System.IO;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Modeling;

namespace KadOzenka.Dal.DataExport
{
    public static class DataExporterKO
    {
	    public static Stream ExportMarkerListToExcel(long groupId, long factorId)
        {
            ExcelFile excelTemplate = new ExcelFile();

            var mainWorkSheet = excelTemplate.Worksheets.Add("Метки");

            DataExportCommon.AddRow(mainWorkSheet, 0, new object[] { "Значение фактора", "Метка" });

            var row = 1;
            var markers = new ModelFactorsService().GetMarks(groupId, factorId);

            foreach (var marker in markers)
            {
                var factor = marker.ValueFactor == null ? string.Empty : marker.ValueFactor;
                var metka = marker.MetkaFactor == null ? string.Empty : marker.MetkaFactor.ParseToString();

                DataExportCommon.AddRow(mainWorkSheet, row, new object[] { factor, metka });
                row++;
            }

            MemoryStream stream = new MemoryStream();
            excelTemplate.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

	    public static Stream ExportMarkerListToExcel(long dictionaryId)
	    {
		    var excelTemplate = new ExcelFile();
		    var mainWorkSheet = excelTemplate.Worksheets.Add("Метки");

		    DataExportCommon.AddRow(mainWorkSheet, 0, new object[] { "Значение фактора", "Метка" });

		    var row = 1;
		    var markers = new ModelDictionaryService().GetDictionaryValues(dictionaryId);

		    foreach (var marker in markers)
		    {
			    var factor = marker.Value == null ? string.Empty : marker.Value;
			    var metka = marker.CalculationValue == null ? string.Empty : marker.CalculationValue.ParseToString();

			    DataExportCommon.AddRow(mainWorkSheet, row, new object[] { factor, metka });
			    row++;
		    }

		    var stream = new MemoryStream();
		    excelTemplate.Save(stream, SaveOptions.XlsxDefault);
		    stream.Seek(0, SeekOrigin.Begin);
		    return stream;
	    }
    }
}