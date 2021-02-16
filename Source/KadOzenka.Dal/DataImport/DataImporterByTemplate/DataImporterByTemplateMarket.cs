using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using ObjectModel.Core.Shared;
using ObjectModel.Market;

namespace KadOzenka.Dal.DataImport.DataImporterByTemplate
{
	public class DataImporterByTemplateMarket : DataImporterByTemplate
	{
		public DataImporterByTemplateMarket() : base(OMCoreObject.GetRegisterId())
		{
		}

		protected override DataImportStatus Process(List<DataExportColumn> columns, ExcelWorksheet mainWorkSheet, ParallelOptions options, int lastUsedRowIndex,
			int maxColumns, List<string> columnNames, int mainRegisterId, long? documentId)
		{
			var success = true;
			var errorCount = 0;
			var handledObjects = new Dictionary<int, RegisterObject>();
			object locked = new object();
			Parallel.ForEach(mainWorkSheet.Rows, options, row =>
			{
				try
				{
					if (row.Index != 0 && row.Index <= lastUsedRowIndex) //все, кроме заголовков и пустых строк в конце страницы
					{
						// Найти ИД объекта по ключевым полям					
						List<QSCondition> conditions = new List<QSCondition>();
						long objectId = -1;
						if (columns.Any(x => x.IsKey))
						{
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
							if (dt.Rows.Count >= 1) objectId = dt.Rows[0]["ID"].ParseToLong();
						}
						RegisterObject registerObject = new RegisterObject((int)mainRegisterId, (int)objectId);
						foreach (var column in columns.Where(y => !y.IsKey))
						{
							int cell = columnNames.IndexOf(column.ColumnName);
							object value = mainWorkSheet.Rows[row.Index].Cells[cell].Value;
							var attributeData = RegisterCache.GetAttributeData((int)column.AttributrId);
							int referenceItemId = -1;
							if (attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
							{
								OMReference reference = OMReference.Where(x => x.ReferenceId == attributeData.ReferenceId).ExecuteFirstOrDefault();
								OMReferenceItem item = OMReferenceItem.Where(x => x.ReferenceId == attributeData.ReferenceId && x.Value == (string)value).ExecuteFirstOrDefault();
								var valueStr = value != null ? value.ToString().Trim() : string.Empty;
								if (item == null && !string.IsNullOrEmpty(valueStr))
								{
									throw new Exception($"Некорректное значение в ячейке ({row}, {cell + 1}) для справочника: '{value.ToString()}'");
								}
								if (item != null) referenceItemId = (int)item.ItemId;
							}
							switch (attributeData.Type)
							{
								case RegisterAttributeType.INTEGER:
									value = value.ParseToLongNullable();
									break;
								case RegisterAttributeType.DECIMAL:
									value = value.ParseToDecimalNullable();
									break;
								case RegisterAttributeType.BOOLEAN:
									value = value.ParseToBooleanNullable();
									break;
								case RegisterAttributeType.STRING:
									value = value == null ? "" : value.ToString();
									break;
								case RegisterAttributeType.DATE:
									value = value.ParseToDateTimeNullable();
									break;
							}
							registerObject.SetAttributeValue((int)column.AttributrId, value, referenceItemId);

							if (registerObject.AttributesValues[(int)column.AttributrId].Value == null &&
								!attributeData.IsNullable)
							{
								throw new Exception(
									$"Значение атрибута {attributeData.Name} (ячейка {cell}) не может быть пустым");
							}
						}

						if (objectId == -1)
						{
							var allRegisterAttributes = RegisterCache.RegisterAttributes.Values.Where(p => p.RegisterId == (int)mainRegisterId && !p.IsPrimaryKey);
							var missedRequiredAttributeNames = new List<string>();
							foreach (var registerAttribute in allRegisterAttributes)
							{

								if (!registerAttribute.IsNullable && !registerObject.AttributesValues.ContainsKey(registerAttribute.Id))
								{
									missedRequiredAttributeNames.Add(registerAttribute.Name);
								}
							}

							if (missedRequiredAttributeNames.Count > 0)
							{
								throw new Exception(
									$"Не переданы значения для ненулевых атрибутов: {string.Join(", ", missedRequiredAttributeNames)}");
							}
						}

						lock (locked)
						{
							handledObjects.Add(row.Index, registerObject);
						}
						mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Успешно");
						if (objectId == -1)
							mainWorkSheet.Rows[row.Index].Cells[maxColumns + 1].SetValue("Объект создан");
						lock (locked)
						{
							for (int i = 0; i < maxColumns; i++)
							{
								mainWorkSheet.Rows[row.Index].Cells[i].Style.FillPattern
									.SetSolid(SpreadsheetColor.FromArgb(200, 255, 200));
							}
						}
					}
				}
				catch (Exception ex)
				{
					long errorId = ErrorManager.LogError(ex);
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"{ex.Message} (подробно в журнале №{errorId})");
					lock (locked)
					{
						for (int i = 0; i < maxColumns; i++)
						{
							mainWorkSheet.Rows[row.Index].Cells[i].Style.FillPattern
								.SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
						}
					}

					lock (locked)
					{
						errorCount++;
					}
				}
			});

			if (errorCount > 0)
			{
				success = false;
			}
			else
			{
				try
				{
					using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
					{
						for (var i = 1; i <= lastUsedRowIndex; i++)
						{
							try
							{
								var registerObject = handledObjects[i];
								RegisterStorage.Save(registerObject);
							}
							catch (Exception ex)
							{
								long errorId = ErrorManager.LogError(ex);
								mainWorkSheet.Rows[i].Cells[maxColumns]
									.SetValue($"{ex.Message} (подробно в журнале №{errorId})");
								mainWorkSheet.Rows[i].Cells[maxColumns + 1].SetValue(string.Empty);
								for (int j = 0; j < maxColumns; j++)
								{
									mainWorkSheet.Rows[i].Cells[j].Style.FillPattern
										.SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
								}

								throw;
							}
						}

						ts.Complete();
					}
				}
				catch (Exception ex)
				{
					success = false;
				}
			}

			return success 
				? DataImportStatus.Success 
				: DataImportStatus.Failed;
		}
	}
}
