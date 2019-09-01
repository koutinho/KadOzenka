using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.MV;
using System.Data.Common;

namespace CIPJS.DAL.Mv
{
    public class MvService
    {

        public void UpdateMvRefreshList(OMRefreshList mView)
        {
            if (!string.IsNullOrEmpty(mView?.MvName))
            {
                string sql = $@"REFRESH MATERIALIZED VIEW {mView.MvName};";
                DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
                DBMngr.Realty.ExecuteNonQuery(command);
            }

        }
    }
}
