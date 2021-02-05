using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.Register;
using ObjectModel.Core.TD;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.DataImport.DataImporterByTemplate
{
	public class DataImporterByTemplateGbu : DataImporterByTemplate
	{
		public DataImporterByTemplateGbu() : base(OMMainObject.GetRegisterId())
		{
		}

		protected override DataImportStatus Process(List<DataExportColumn> columns, ExcelWorksheet mainWorkSheet, ParallelOptions options, int lastUsedRowIndex,
			int maxColumns, List<string> columnNames, int mainRegisterId, long? documentId)
		{
			object locked = new object();
			var successLoadedObjectsCount = 0;
			OMInstance doc = OMInstance.Where(x => x.Id == documentId).SelectAll().Execute().FirstOrDefault();
			var docDate = doc.ApproveDate;

			Parallel.ForEach(mainWorkSheet.Rows, options, row =>
			{
				try
				{
					if (row.Index != 0 && row.Index <= lastUsedRowIndex) //все, кроме заголовков и пустых строк в конце страницы
					{
						long objectId = -1;
						var isNewObject = false;

						//ключ - кадастровый номер, колонка №2						
						string cadastralNumber = row.Cells[1].Value?.ToString();
						if (string.IsNullOrEmpty(cadastralNumber))
							throw new Exception("Не указан кадастровый номер");

						OMMainObject mainObject = OMMainObject.Where(x => x.CadastralNumber == cadastralNumber)
							.Select(x => x.Id).ExecuteFirstOrDefault();
						if (mainObject == null)
						{
							mainObject = new OMMainObject
							{
								CadastralNumber = cadastralNumber,
								ObjectType_Code = ObjectModel.Directory.PropertyTypes.Building
							};
							mainObject.Save();
							isNewObject = true;
						}
						objectId = mainObject.Id;

						List<long> loadColumnIds = columns.Where(y => !y.IsKey).Select(x => x.AttributrId).ToList();

						var registers = OMAttribute.Where(x => loadColumnIds.Contains(x.Id))
							.Select(x => x.RegisterId)
							.Execute();

						var registerGroups = registers.GroupBy(x => x.RegisterId, y => y.Id).ToList();

						foreach (var group in registerGroups)
						{
							List<long> attributeIds = group.Select(x => x).ToList();

							foreach (long attribute in attributeIds)
							{
								var gbuObjectAttribute = new GbuObjectAttribute
								{
									ObjectId = objectId,
									AttributeId = attribute,
									S = docDate ?? DateTime.Now, // установка даты актуальности по дате документа
									Ot = docDate ?? DateTime.Now,
									ChangeDocId = documentId ?? -1
								};
								DataExportColumn column = columns.Where(x => x.AttributrId == attribute).First();

								int cell = columnNames.IndexOf(column.ColumnName);
								object value = mainWorkSheet.Rows[row.Index].Cells[cell].Value;

								var attributeData = RegisterCache.GetAttributeData((int)column.AttributrId);
								switch (attributeData.Type)
								{
									case RegisterAttributeType.INTEGER:
										gbuObjectAttribute.NumValue = value.ParseToLongNullable();
										break;
									case RegisterAttributeType.DECIMAL:
										gbuObjectAttribute.NumValue = value.ParseToDecimalNullable();
										break;
									case RegisterAttributeType.BOOLEAN:
										gbuObjectAttribute.NumValue = value.ParseToBoolean() ? 1 : 0;
										break;
									case RegisterAttributeType.STRING:
										gbuObjectAttribute.StringValue = value == null ? "" : value.ToString();
										break;
									case RegisterAttributeType.DATE:
										gbuObjectAttribute.DtValue = value.ParseToDateTimeNullable();
										break;
									default:
										gbuObjectAttribute.StringValue = value == null ? "" : value.ToString();
										break;
								}

								GbuObjectService.SaveAttributeValueWithCheck(gbuObjectAttribute);
							}

							mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Успешно");
							if (isNewObject)
								mainWorkSheet.Rows[row.Index].Cells[maxColumns + 1].SetValue("Объект создан");
						}

						lock (locked)
						{
							successLoadedObjectsCount++;
						}
					}
				}
				catch (Exception ex)
				{
					if (ex.Message != "Не указан кадастровый номер")
					{
						long errorId = ErrorManager.LogError(ex);
						mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"{ex.Message} (подробно в журнале №{errorId})");
					}
					else
					{
						mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"{ex.Message}");
					}
				}
			});

			return successLoadedObjectsCount > 0
				? successLoadedObjectsCount == lastUsedRowIndex
					? DataImportStatus.Success
					: DataImportStatus.PartiallyLoaded
				: DataImportStatus.Failed;
		}
	}
}
