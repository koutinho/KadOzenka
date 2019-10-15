using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.Shared;
using ObjectModel.Core.TD;
using ObjectModel.Gbu;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;

namespace DebugApplication.ObjectReplicationExcel
{
    public class ObjectReplicationExcelProcess
    {
		public static string BaseDirectory
		{
			get
			{
				return ConfigurationManager.AppSettings["ObjectReplicationExcelBaseFolder"];
			}
		}

		private static Dictionary<long, OMInstance> _documents = new Dictionary<long, OMInstance>();

		private static Dictionary<long, OMReferenceItem> _refItems = new Dictionary<long, OMReferenceItem>();

		public static void StartImport()
		{
			if (!Directory.Exists(BaseDirectory)) throw new Exception($"Директория не существует: {BaseDirectory}");

			LoadDocuments();

			LoadObjects();

			LoadReference();

			LoadAttributes();
		}

		private static void LoadDocuments()
		{
			var dt = GetData($"{BaseDirectory}tbDocument.xlsx");

			foreach (DataRow row in dt.Rows)
			{
				OMInstance document = new OMInstance
				{
					Id = row["id_document"].ParseToLong(),
					RegNumber = row["num_document"].ToString(),
					CreateDate = row["date_document"].ParseToDateTime(),
					ApproveDate = row["date_document"].ParseToDateTime(),
					Description = row["name_document"].ToString(),
				};
				
				document.Save();

				_documents.Add(document.Id, document);
			}
		}

		private static void LoadObjects()
		{
			var dt = GetData($"{BaseDirectory}tbObject.xlsx");

			foreach(DataRow row in dt.Rows)
			{
				OMMainObject gbuObject = new OMMainObject
				{
					Id = row["id_object"].ParseToLong(),
					CadastralNumber = row["kn_object"].ToString(),
					ObjectType_Code = ObjectModel.Directory.PropertyTypes.Building,
					IsActive = row["status_object"].ParseToBoolean()
				};

				gbuObject.Save();
			}
		}

		private static void LoadReference()
		{
			var dt = GetData($"{BaseDirectory}DICTIONARYRECORD.xlsx");

			foreach (DataRow row in dt.Rows)
			{
				OMReferenceItem refItem = new OMReferenceItem
				{
					ItemId = row["ID_RECORD"].ParseToLong(),
					Value = row["VAL_RECORD"].ToString()
				};

				//document.Save();

				_refItems.Add(refItem.ItemId, refItem);
			}
		}

		private static void LoadAttributes()
		{
			var dt = GetData($"{BaseDirectory}tbFactorDateValue.xlsx");

			foreach (DataRow row in dt.Rows)
			{
				var attributeValue = GetAttributeValueCommon(row);
				attributeValue.DtValue = row["value"].ParseToDateTime();

				attributeValue.Save();
			}

			dt = GetData($"{BaseDirectory}tbFactorDoubleValue.xlsx");

			foreach (DataRow row in dt.Rows)
			{
				var attributeValue = GetAttributeValueCommon(row);
				attributeValue.NumValue = row["value"].ParseToDecimal();
				
				attributeValue.Save();
			}

			dt = GetData($"{BaseDirectory}tbFactorTextValue.xlsx");

			foreach (DataRow row in dt.Rows)
			{
				var attributeValue = GetAttributeValueCommon(row);
				attributeValue.StringValue = row["value"].ToString();

				attributeValue.Save();
			}

			dt = GetData($"{BaseDirectory}tbFactorLinkValue.xlsx");

			foreach (DataRow row in dt.Rows)
			{
				var attributeValue = GetAttributeValueCommon(row);
				attributeValue.RefItemId = row["value"].ParseToLong();
				attributeValue.StringValue = _refItems[attributeValue.RefItemId].Value;

				attributeValue.Save();
			}
		}

		private static GbuObjectAttribute GetAttributeValueCommon(DataRow row)
		{
			var attributeValue = new GbuObjectAttribute
			{
				Id = row["id_value"].ParseToInt(),
				AttributeId = row["id_factor"].ParseToInt(),
				ObjectId = row["id_object"].ParseToInt(),
				ChangeDocId = row["id_document"].ParseToInt(),
				S = row["date_value"].ParseToDateTime(),
				ChangeUserId = row["id_user"].ParseToInt(),
				ChangeDate = row["date_user"].ParseToDateTime(),
			};

			attributeValue.Ot = _documents[attributeValue.ChangeDocId].CreateDate;

			return attributeValue;
		}

		private static DataTable GetData(string filePath)
		{
			ExcelFile excelFile = ExcelFile.Load(filePath, new XlsxLoadOptions());

			ExcelWorksheet ws = excelFile.Worksheets[0];

			DataTable dt = new DataTable();

			ExcelRow headerRow = ws.Rows[0];
			for (int i = 0; i < headerRow.AllocatedCells.Count; i++)
			{
				dt.Columns.Add(headerRow.Cells[i].Value != null ? headerRow.Cells[i].Value.ToString() : i.ToString());
			}

			ExtractToDataTableOptions options = new ExtractToDataTableOptions(1, 0, ws.Rows.Count - 1)
			{
				ExtractDataOptions = ExtractDataOptions.SkipEmptyRows
			};

			options.ExcelCellToDataTableCellConverting += (sender, e) =>
			{
				if (e.IsDataTableValueValid) return;
				// GemBox.Spreadsheet doesn't automatically convert numbers to strings in ExtractToDataTable() method because of culture issues; 
				// someone would expect the number 12.4 as "12.4" and someone else as "12,4".
				e.DataTableValue = e.ExcelCellValue == null ? null : e.ExcelCellValue.ToString();
				e.Action = ExtractDataEventAction.Continue;
			};

			ws.ExtractToDataTable(dt, options);

			return dt;
		}
	}
}
