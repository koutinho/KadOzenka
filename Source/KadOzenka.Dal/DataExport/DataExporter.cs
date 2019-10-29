using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace KadOzenka.Dal.DataExport
{
    public class DataExporter
    {
		public static ExcelFile ExportDataToExcel(int mainRegisterId, ExcelFile excelTemplate, List<DataExportColumn> columns)
		{
			// Получить значение из ключевой колонки пакетами по 1000
			int packageNum = 0;
			int packageSize = 1000;
			var mainWorkSheet = excelTemplate.Worksheets[0];
			bool isFinish = false;

			List<string> keyValues = new List<string>();

			while (true)
			{
				for(int i = packageNum * packageSize; i < (packageNum + 1) * packageSize; i++)
				{
					if(i == mainWorkSheet.Rows.Count)
					{
						isFinish = true;
						break;
					}

					keyValues.Add(mainWorkSheet.Rows[i].Cells[0].Value.ToString());
				}
				
				// Получение данных для 1000 строк
				QSQuery query = new QSQuery
				{
					MainRegisterID = mainRegisterId,
					Columns = columns.Select(x => (QSColumn)new QSColumnSimple((int)x.AttributrId)).ToList(),
					Condition = new QSConditionSimple
					{
						ConditionType = QSConditionType.In,
						LeftOperand = new QSColumnSimple((int)columns.FirstOrDefault(x => x.IsKey).AttributrId),
						RightOperand = new QSColumnConstant(keyValues)
					}
				};

				DataTable dt = query.ExecuteQuery();

				for (int i = packageNum * packageSize; i < (packageNum + 1) * packageSize; i++)
				{
					if (i == mainWorkSheet.Rows.Count)
					{
						break;
					}

					string keyValue = mainWorkSheet.Rows[i].Cells[0].Value.ToString();

					foreach (var column in columns)
					{
						DataRow row = dt.FilteringAndSortingTable($"10005400 = '{keyValue}'").Rows[0];

						// Заполнение данных в Excel
						var attributeType = RegisterCache.GetAttributeData(10007100).Type;

						switch (attributeType)
						{
							case RegisterAttributeType.INTEGER:
								mainWorkSheet.Rows[i].Cells[2].SetValue(row["10007100"].ParseToLong());
								break;
							case RegisterAttributeType.DECIMAL:
								mainWorkSheet.Rows[i].Cells[2].SetValue(row["10007100"].ParseToDouble());
								break;
							case RegisterAttributeType.BOOLEAN:
								break;
							case RegisterAttributeType.STRING:
								break;
							case RegisterAttributeType.DATE:
								break;
							default:
								throw new Exception($"Не поддерживаемый тип: {attributeType}");
						}
					}
				}

				if (isFinish) break;

				packageNum++;
			}
			
			return excelTemplate;
		}
	}
}
