using Core.Shared.Extensions;
using Core.Shared.Misc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Insur;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Transactions;

namespace CIPJS.DAL.ImportLogInsurFlat
{
    /// <summary>
    /// Сервис для работы с сущностью import_log_insur_flat.
    /// </summary>
    public class ImportLogInsurFlatService
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public ImportLogInsurFlatService()
        {

        }

        /// <summary>
        /// Добавление лога import_log_insur_flat для ЖП.
        /// </summary>
        /// <param name="log"></param>
        /// <param name="flat"></param>
        public void FillInsurFlatBuildingLog(OMFlat flat)
        {
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
            {
                if (flat == null)
                    return;

                string logIdCmdText = $@"select id from import_log_insur_flat where insur_flat_id = {flat.EmpId}";
                DbCommand commandLogId = DBMngr.Realty.GetSqlStringCommand(logIdCmdText);
                var result = DBMngr.Realty.ExecuteDataSet(commandLogId).Tables[0].AsEnumerable().FirstOrDefault();

                var logId = result?["id"]?.ParseToLongNullable();
                string dateImport = CrossDBSQL.ToDate(DateTime.Now);
                int isError = 0;
                string message = "";
                string strErrorId = "NULL";
                string insurFlatIdStr = flat != null && flat.EmpId > 0 ? flat.EmpId.ToString() : "NULL";
                string insurBuildingIdStr = flat != null && flat.LinkObjectMkd > 0 ? flat.LinkObjectMkd.ToString() : "NULL";
                string ehdParcelIdStr = flat.LinkInsurEgrn != null ? flat.LinkInsurEgrn.ToString() : "NULL";
                string updateDateEhd = flat.LinkInsurEgrn != null ? dateImport : "NULL";
                string cadNom = flat.CadastrNum ?? "";
                string btiFlatIdStr = flat.LinkBtiFlat != null ? flat.LinkBtiFlat.ToString() : "NULL";
                string updateDateBti = flat.LinkBtiFlat != null ? dateImport : "NULL";
                string kvnom = flat.Kvnom ?? "";
                string isNewStr = "0";

                if (logId != null && logId > 0)
                {
                    string updateCmdText = $@"update import_log_insur_flat set 
						ehd_parcel_id = {ehdParcelIdStr}, 
						bti_flat_id = {btiFlatIdStr},
						insur_flat_id = {insurFlatIdStr},
                        insur_building_id = {insurBuildingIdStr},
						date_loaded = {dateImport},
						error_message = '{message}',
						ERROR_ID = {strErrorId},
						IS_ERROR = {isError},

						update_date_ehd = {updateDateEhd},
						update_date_bti = {updateDateBti},
						cad_num = '{cadNom}',
						kvnom = '{kvnom}',

						is_new = {isNewStr},

						error_attempts_count = {"0"}
					where id = {logId}";

                    DbCommand commandUpd = DBMngr.Realty.GetSqlStringCommand(updateCmdText);
                    DBMngr.Realty.ExecuteNonQuery(commandUpd);
                }
                else
                {
                    long id = CrossDBSQL.GetNextValFromSequence("REG_OBJECT_SEQ");

                    string cmdText = $@"INSERT INTO import_log_insur_flat
					(
						id,
						ehd_parcel_id, 
						bti_flat_id,
						insur_flat_id,
                        insur_building_id,
						date_loaded,
						error_message,
						ERROR_ID,
						IS_ERROR,
						update_date_ehd,
						update_date_bti,
						cad_num,
						kvnom,
						is_new,
						error_attempts_count) 
					VALUES(
						{id},
						{ehdParcelIdStr}, 
						{btiFlatIdStr},
						{insurFlatIdStr},
						{insurBuildingIdStr},
						{dateImport},
						'{message}',
						{strErrorId},
						{isError},

						{updateDateEhd},
						{updateDateBti},
						'{cadNom}',
						'{kvnom}',

						{isNewStr},

						{"0"}
					)";

                    DbCommand command = DBMngr.Realty.GetSqlStringCommand(cmdText);
                    DBMngr.Realty.ExecuteNonQuery(command);
                }

                ts.Complete();
            }
        }

        /// <summary>
        /// Удаление  лога import_log_insur_flat для ЖП.
        /// </summary>
        /// <param name="flat"></param>
        public void RemoveInsurFlatBuildingLog(OMFlat flat)
        {
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
            {
                if (flat == null)
                    return;

                string logIdCmdText = $@"select id from import_log_insur_flat where insur_flat_id = {flat.EmpId}";
                DbCommand commandLogId = DBMngr.Realty.GetSqlStringCommand(logIdCmdText);
                var result = DBMngr.Realty.ExecuteDataSet(commandLogId).Tables[0].AsEnumerable().FirstOrDefault();
                var logId = result?["id"]?.ParseToLongNullable();
                if (logId != null)
                {
                    string removeCmd = $@"DELETE FROM import_log_insur_flat WHERE id = {logId}";
                    DbCommand command = DBMngr.Realty.GetSqlStringCommand(removeCmd);
                    DBMngr.Realty.ExecuteNonQuery(command);
                }

                ts.Complete();
            }

        }
    }
}
