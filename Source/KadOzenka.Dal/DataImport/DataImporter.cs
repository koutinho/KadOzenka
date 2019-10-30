using Core.Register;
using Core.Register.QuerySubsystem;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;

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

			List<string> columnNames = new List<string>();
			for (int i = 0; i < columns.Count; i++)
			{
				columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
			}

			Parallel.ForEach(mainWorkSheet.Rows, options, row =>
			{
				try
				{
					if (row.Index != 0) //все, кроме заголовков
					{
						// Найти ИД объекта по ключевым полям
												
						List<QSCondition> conditions = new List<QSCondition>();
					
						foreach (var keyColumn in columns.Where(x => x.IsKey))
						{
							int index = columnNames.IndexOf(keyColumn.ColumnName);
							object rowValue = row.Cells[index].Value.ToString();

							QSCondition con1 = new QSConditionSimple
							{
								ConditionType = QSConditionType.Equal,
								LeftOperand = new QSColumnSimple((int)keyColumn.AttributrId),
								RightOperand = new QSColumnConstant(rowValue)
							};
							
							conditions.Add(con1);
						}

						QSQuery query = new QSQuery
						{
							MainRegisterID = mainRegisterId,
							Condition = new QSConditionGroup
							{
								Type = QSConditionGroupType.And,
								Conditions = conditions
							}
						};

						DataTable dt = query.ExecuteQuery();

						if (dt.Rows.Count > 0)
						{
							long objectId = dt.Rows[0]["ID"].ParseToLong();
													   							 						  
							RegisterObject registerObject = new RegisterObject((int)mainRegisterId, (int)objectId);

							foreach (var column in columns.Where(y => !y.IsKey))
							{
								int cell = columnNames.IndexOf(column.ColumnName);
								object value = mainWorkSheet.Rows[row.Index].Cells[cell].Value;

								registerObject.SetAttributeValue((int)column.AttributrId, value);
								RegisterStorage.Save(registerObject);
							}
						}
					}
				}
				catch (Exception ex)
				{
					
				}
			});
		}
	}
}
