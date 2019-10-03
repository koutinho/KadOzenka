using Core.RefLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Text;

namespace KadOzenka.Dal.RefLibImpl
{
	[Serializable]
	[Export(typeof(IReferenceExecutor))]
	public class GroupExecutor : IReferenceExecutor
	{
		public DataTable CreateItemList(int referenceId, RefParameters parameters, string additonalParams = "")
		{
			//List<ReportType> fmReportTypes = ReportsFactory.GetReportTypes();

			DataTable dtReportTypes = new DataTable();

			//dtReportTypes.Columns.Add("ItemID", typeof(int));
			//dtReportTypes.Columns.Add("Value");
			//dtReportTypes.Columns.Add("SName");

			//foreach (ReportType reportType in fmReportTypes.OrderBy(x => x.Title))
			//{
			//	DataRow dataRow = dtReportTypes.NewRow();
			//	dataRow["ItemID"] = reportType.Code;
			//	dataRow["Value"] = reportType.Title;
			//	dataRow["SName"] = reportType.Title;
			//	dtReportTypes.Rows.Add(dataRow);
			//}

			return dtReportTypes;
		}

		#region Obsolete

		public int AddItem(int referenceId, string itemValue, string itemCode, string shortTitle = "", int parentId = 0, int parentReferenceId = 0)
		{
			throw new NotImplementedException();
		}

		public bool DestroyItem(int itemCode, bool unDestroy = false)
		{
			throw new NotImplementedException();
		}

		public DataTable GetDataFromString(int referenceId, string value)
		{
			throw new NotImplementedException();
		}

		public string GetItemCode(int referenceId, int itemId)
		{
			throw new NotImplementedException();
		}

		public int GetItemCount(int referenceId, string strFilter, string fk = "")
		{
			throw new NotImplementedException();
		}

		public int GetItemId(int referenceId, string itemValue)
		{
			throw new NotImplementedException();
		}

		public int GetItemParent(int referenceId, int itemId, string valueEx = "")
		{
			throw new NotImplementedException();
		}

		public string GetItemShortTitle(int referenceId, int itemId)
		{
			throw new NotImplementedException();
		}

		public string GetItemValue(int referenceId, int itemId)
		{
			throw new NotImplementedException();
		}

		public DataTable GetParentDataTable(int referenceId, int itemId, string strValueEx)
		{
			throw new NotImplementedException();
		}

		public bool IsItemCode(int refernceId, int code)
		{
			throw new NotImplementedException();
		}

		public bool IsItemCountToMatch(int referenceId, string filter)
		{
			throw new NotImplementedException();
		}

		public bool IsItemId(int refernceId, int itemId)
		{
			throw new NotImplementedException();
		}

		public bool IsItemValue(int refernceId, object value)
		{
			throw new NotImplementedException();
		}

		public int ParentLevel(int refernceId, int parentLevel)
		{
			throw new NotImplementedException();
		}

		public bool UpdateItem(int itemId, int referenceId, string itemValue, string itemCode, string shortTitle = "")
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
