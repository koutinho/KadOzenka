using Core.RefLib;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Common;

namespace CIPJS.DAL.RefLibImpl
{
    [Serializable]
    [Export(typeof(IReferenceExecutor))]
    public class SKExecutor : IReferenceExecutor
    {
        public DataTable CreateItemList(int referenceId, RefParameters parameters, string additonalParams = "")
        {
            DbCommand selectCommand = DBMngr.Realty.GetSqlStringCommand(@"select t.id, t.full_name, t.code, t.short_name as VALUE " +
                $@"from insur_insurance_organization t WHERE 1=1 {(additonalParams.IsNotEmpty() ? $" and {additonalParams}" : string.Empty)} order by t.short_name"); //
            DataSet ds = DBMngr.Realty.ExecuteDataSet(selectCommand);
            return ds.Tables[0];
        }

        public int GetItemCount(int referenceId, string strFilter, string fk = "")
        {
            DataTable dt = CreateItemList(referenceId, null, strFilter);
            return dt.Rows.Count;
        }

        public int GetItemParent(int referenceId, int itemId, string valueEx = "")
        {
            throw new NotImplementedException();
        }

        public int GetItemId(int referenceId, string itemValue)
        {
            DataTable dt = CreateItemList(referenceId, null);
            DataRow[] rows = dt.Select("Value = '" + itemValue + "'");
            if (rows.Length > 0)
                return int.Parse(rows[0]["ItemID"].ToString());
            else
                return 0;
        }

        public string GetItemCode(int referenceId, int itemId)
        {
            DataTable dt = CreateItemList(referenceId, null);
            DataRow[] rows = dt.Select("ItemID = " + itemId);
            if (rows.Length > 0)
                return rows[0]["Code"].ToString();
            else
                return null;
        }

        public string GetItemValue(int referenceId, int itemId)
        {
            DataTable dt = CreateItemList(referenceId, null);
            DataRow[] rows = dt.Select("ItemID = " + itemId);
            if (rows.Length > 0)
                return rows[0]["Value"].ToString();
            else
                return null;
        }

        public string GetItemShortTitle(int referenceId, int itemId)
        {
            throw new NotImplementedException();
        }

        public bool IsItemCountToMatch(int referenceId, string filter)
        {
            throw new NotImplementedException();
        }

        public bool DestroyItem(int itemCode, bool unDestroy)
        {
            throw new NotImplementedException();
        }

        public int AddItem(int refernceId, string itemValue, string itemCode, string shortTitle = "", int parentId = 0, int parentRefenceId = 0)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(int itemId, int refernceId, string itemValue, string itemCode, string shortTitle = "")
        {
            throw new NotImplementedException();
        }

        public bool IsItemId(int refernceId, int itemId)
        {
            throw new NotImplementedException();
        }

        public bool IsItemCode(int refernceId, int code)
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

        public DataTable GetParentDataTable(int referenceId, int itemId, string strValueEx)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataFromString(int referenceId, string value)
        {
            throw new NotImplementedException();
        }
    }
}
