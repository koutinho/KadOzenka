using Core.Register;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KadOzenka.Dal.DataImport
{
	public class DataImporter
	{
		public static void ImportDataFromExcel(int mainRegisterId, ExcelFile excelFile, List<DataExportColumn> columns)
		{
			var mainWorkSheet = excelFile.Worksheets[0];

			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 10
			};

			Parallel.ForEach(mainWorkSheet.Rows, options, x =>
			{
				try
				{
					// Найти ИД объекта по ключевым полям
					long objectId = 0;

					RegisterObject registerObject = new RegisterObject((int)mainRegisterId, (int)objectId);

					foreach(var column in columns.Where(y => !y.IsKey))
					{
						object value = null;
						registerObject.SetAttributeValue((int)column.AttributrId, value);

						RegisterStorage.Save(registerObject);
					}
				}
				catch (Exception ex)
				{
					
				}
			});
		}
	}
}
