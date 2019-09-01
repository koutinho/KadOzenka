using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Ehd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using ObjectModel.Bti;
using System.Text;

namespace CIPJS.DAL.Egas
{
    public class EgasImportLoadProcess : ILongProcess
    {
        private List<string> logger = new List<string>();

        private static ConfigEhdObjectsImport ImportConfig
        {
            get
            {
                return Core.ConfigParam.Configuration.GetParam<ConfigEhdObjectsImport>("EhdObjectsImportConditions", "Ehd");
            }
        }

        private int ThreadsCount
        {
            get
            {
                if (!ImportConfig.ThreadsCount.HasValue)
                {
                    return 10;
                }

                return ImportConfig.ThreadsCount.Value;
            }
        }

        OMQueue ProcessQueue;


        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            this.ProcessQueue = processQueue;

            DbCommand command;
            try
            {
                if (!ImportConfig.BuildingParcelSkip)
                {
                    this.Log($"Запуск формирования журнала на перенос объектов {ImportConfig.BuildingParcelLog}");

                    command = CipjsDbManager.Dgi.GetSqlStringCommand($@"
insert into {ImportConfig.BuildingParcelLog}
SELECT b.Id, b.global_id, b.update_date, -1 as is_error, null, null, null, sysdate, null, null
  FROM {ImportConfig.BuildingParcel} b
 where b.actual is null and not exists(select 1 from {ImportConfig.BuildingParcelLog} l where l.id = b.id)");
                    CipjsDbManager.Dgi.ExecuteNonQuery(command);

                    this.Log($"Завершено формирование журнала на перенос объектов {ImportConfig.BuildingParcelLog}");
                }

                if (!ImportConfig.RegisterSkip)
                {
                    this.Log($"Запуск формирования журнала на перенос объектов {ImportConfig.RegisterLog}");

                    command = CipjsDbManager.Dgi.GetSqlStringCommand($@"
insert into {ImportConfig.RegisterLog}
SELECT r.Id, r.global_id, b.update_date, -1 as is_error, null, null, null, sysdate, null
  FROM {ImportConfig.Register} r
  join {ImportConfig.BuildingParcel} b
    on b.id = r.building_parcel_id
 where b.actual is null and not exists(select 1 from {ImportConfig.RegisterLog} l where l.id = r.id)");
                    CipjsDbManager.Dgi.ExecuteNonQuery(command);

                    this.Log($"Завершено формирование журнала на перенос объектов {ImportConfig.RegisterLog}");
                }

                if (!ImportConfig.LocationSkip)
                {
                    this.Log($"Запуск формирования журнала на перенос объектов {ImportConfig.LocationLog}");

                    command = CipjsDbManager.Dgi.GetSqlStringCommand($@"
insert into {ImportConfig.LocationLog}
SELECT L.Id, l.global_id, b.update_date, -1 as is_error, null, null, null, sysdate, null
  FROM {ImportConfig.Location} L
  join {ImportConfig.BuildingParcel} b
    on b.id = l.building_parcel_id
 where b.actual is null and not exists(select 1 from {ImportConfig.LocationLog} lg where lg.id = l.id)");
                    CipjsDbManager.Dgi.ExecuteNonQuery(command);

                    this.Log($"Завершено формирование журнала на перенос объектов {ImportConfig.LocationLog}");
                }

                if (!ImportConfig.EgrpSkip)
                {
                    this.Log($"Запуск формирования журнала на перенос объектов {ImportConfig.EgrpLog}");

                    command = CipjsDbManager.Dgi.GetSqlStringCommand($@"
insert into {ImportConfig.EgrpLog}
SELECT E.id, e.global_id, e.update_date, -1 as is_error, null, null, null, sysdate, null, null
FROM {ImportConfig.Egrp} E
where e.actual is null and not exists(select 1 from {ImportConfig.EgrpLog} l where l.id = e.id)");
                    CipjsDbManager.Dgi.ExecuteNonQuery(command);

                    this.Log($"Завершено формирование журнала на перенос объектов {ImportConfig.EgrpLog}");
                }

                if (!ImportConfig.RightSkip)
                {
                    this.Log($"Запуск формирования журнала на перенос объектов {ImportConfig.RightLog}");

                    command = CipjsDbManager.Dgi.GetSqlStringCommand($@"
insert into {ImportConfig.RightLog}
SELECT R.ID, r.global_id, e.update_date, -1 as is_error, null, null, null, sysdate, null, R.EGRP_ID
FROM {ImportConfig.Right} R
JOIN {ImportConfig.Egrp} E ON E.ID=R.EGRP_ID
where e.actual is null and not exists(select 1 from {ImportConfig.RightLog} l where l.id = r.id)");
                    CipjsDbManager.Dgi.ExecuteNonQuery(command);

                    this.Log($"Завершено формирование журнала на перенос объектов {ImportConfig.RightLog}");
                }

                if (!ImportConfig.OldNumbersSkip)
                {
                    this.Log($"Запуск формирования журнала на перенос объектов {ImportConfig.OldNumbersLog}");

                    command = CipjsDbManager.Dgi.GetSqlStringCommand($@"
insert into {ImportConfig.OldNumbersLog}
select n.id, n.global_id, p.update_date, -1 as is_error, null, null, null, sysdate, null
  from {ImportConfig.OldNumbers} n
  left join {ImportConfig.Register} r
    on r.id = n.register_id
  join {ImportConfig.BuildingParcel} p
    on r.building_parcel_id = p.id
 where p.actual is null and not exists(select 1 from {ImportConfig.OldNumbersLog} l where l.id = n.id)");
                    CipjsDbManager.Dgi.ExecuteNonQuery(command);

                    this.Log($"Завершено формирование журнала на перенос объектов {ImportConfig.OldNumbersLog}");
                }

                /*======================================================================================*/

                if (!ImportConfig.FloorsSkip)
                {
                    this.Log($"Запуск формирования журнала на перенос объектов {ImportConfig.FloorsLog}");

                    command = CipjsDbManager.Dgi.GetSqlStringCommand($@"
insert into {ImportConfig.FloorsLog} 
SELECT f.ID, f.global_id, p.update_date, -1 as is_error, null, null, null, sysdate, null 
FROM {ImportConfig.Floors} f 
     INNER JOIN {ImportConfig.BuildingParcel} p on p.id = f.building_parcel_id 
WHERE p.actual is null and not exists(SELECT 1 FROM {ImportConfig.FloorsLog} l WHERE l.id = f.id)");
                    CipjsDbManager.Dgi.ExecuteNonQuery(command);

                    this.Log($"Завершено формирование журнала на перенос объектов {ImportConfig.FloorsLog}");
                }

                if (!ImportConfig.ElementsConstructSkip)
                {
                    this.Log($"Запуск формирования журнала на перенос объектов {ImportConfig.ElementsConstructLog}");

                    command = CipjsDbManager.Dgi.GetSqlStringCommand($@"
insert into {ImportConfig.ElementsConstructLog}
SELECT f.ID, f.global_id, p.update_date, -1 as is_error, null, null, null, sysdate, null
FROM {ImportConfig.ElementsConstruct} f
     INNER JOIN {ImportConfig.BuildingParcel} p on p.id = f.building_parcel_id
WHERE p.actual is null and not exists(SELECT 1 FROM {ImportConfig.ElementsConstructLog} l WHERE l.id = f.id)");
                    CipjsDbManager.Dgi.ExecuteNonQuery(command);

                    this.Log($"Завершено формирование журнала на перенос объектов {ImportConfig.ElementsConstructLog}");
                }

                if (!ImportConfig.ExploitationCharSkip)
                {
                    this.Log($"Запуск формирования журнала на перенос объектов {ImportConfig.ExploitationCharLog}");

                    command = CipjsDbManager.Dgi.GetSqlStringCommand($@"
insert into {ImportConfig.ExploitationCharLog}
SELECT f.ID, f.global_id, p.update_date, -1 as is_error, null, null, null, sysdate, null
FROM {ImportConfig.ExploitationChar} f
     INNER JOIN {ImportConfig.BuildingParcel} p on p.id = f.building_parcel_id
WHERE p.actual is null and not exists(SELECT 1 FROM {ImportConfig.ExploitationCharLog} l WHERE l.id = f.id)");
                    CipjsDbManager.Dgi.ExecuteNonQuery(command);

                    this.Log($"Завершено формирование журнала на перенос объектов {ImportConfig.ExploitationCharLog}");
                }

                /*======================================================================================*/

                if (!ImportConfig.BuildingParcelSkip) ImportCommon(ImportConfig.BuildingParcelLog, GetBuildingParcelTable, SaveBuildParcel);
                if (!ImportConfig.RegisterSkip) ImportCommon(ImportConfig.RegisterLog, GetRegisterTable, SaveRegister);
                if (!ImportConfig.LocationSkip) ImportCommon(ImportConfig.LocationLog, GetLocationTable, SaveLocation);
                if (!ImportConfig.EgrpSkip) ImportCommon(ImportConfig.EgrpLog, GetEgrpTable, SaveEgrp);
                if (!ImportConfig.RightSkip) ImportCommon(ImportConfig.RightLog, GetRightTable, SaveRight);
                if (!ImportConfig.OldNumbersSkip) ImportCommon(ImportConfig.OldNumbersLog, GetOldNumbersTable, SaveOldNumbers);

                if (!ImportConfig.FloorsSkip) ImportCommon(ImportConfig.FloorsLog, GetFloorsTable, SaveFloors);
                if (!ImportConfig.ElementsConstructSkip) ImportCommon(ImportConfig.ElementsConstructLog, GetElementsConstructTable, SaveElementsConstruct);
                if (!ImportConfig.ExploitationCharSkip) ImportCommon(ImportConfig.ExploitationCharLog, GetExploitationCharTable, SaveExploitationChar);

                // Проставление поля Actual для архивных записей
                SetActual(ImportConfig.BuildingParcel, ImportConfig.BuildingParcelLog, "ehd_build_parcel_q", "actual_ehd");
                SetActual(ImportConfig.Egrp, ImportConfig.EgrpLog, "ehd_egrp_q", "actual_id");
            }
            catch (Exception exception)
            {
                int errorId = ErrorManager.LogError(exception);
                this.LogError(null, exception, errorId);
                if (ProcessQueue != null)
                {
                    ProcessQueue.ErrorId = errorId;
                    ProcessQueue.Status = (long)OMQueueStatus.Faulted;
                    ProcessQueue.Save();
                }
                throw;
            }
            finally
            {
                Log();
            }

            if (ProcessQueue != null)
            {
                ProcessQueue.Status = (long)OMQueueStatus.Completed;
                ProcessQueue.Save();
            }
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
            Log(string.Format("errorId:{0}, objectId:{1}, Exception: {2}", errorId ?? 0, objectId ?? 0, ex.Message));
        }

        private void Log(string message = null, bool replaceLast = false)
        {
            if (message != null)
            {
                if (replaceLast && logger.Count > 0)
                {
                    logger[logger.Count - 1] = message;
                }
                else
                {
                    logger.Add($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: {message}");
                }
            }

            ProcessQueue.Log = String.Join("\n\r", logger);
            ProcessQueue.Save();
        }

        public bool Test() { return true; }

        #region Common

        private delegate DataTable GetDataTable();

        private delegate void SaveObject(DataRow row);

        private int _packageNumber;

        private HashSet<long> _objectIds;

        private void InsertLogs(string logTable, DataTable models)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["SomeDB"];
            string connectionString = settings.ConnectionString;

            OracleConnection conn = new OracleConnection(connectionString);

            try
            {
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction();

                foreach (DataRow model in models.Rows)
                {
                    string globalId = model["global_id"] != null && model["global_id"] != DBNull.Value ? model["global_id"].ToString() : "NULL";

                    string updateDate = model.Table.Columns.Contains("update_date") ? CrossDBSQL.ToDate(model["update_date"].ParseToDateTime(), CrossDBSQL.Providers.PrvOracle) : "NULL";

                    string cmdText = $"INSERT INTO {logTable} (ID, GLOBAL_ID, UPDATE_DATE) " +
                            $"VALUES({model["ID"].ParseToLong()},{globalId},{updateDate})";

                    DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                    command.Transaction = transaction;

                    CipjsDbManager.Dgi.ExecuteNonQuery(command);
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void UpdateLogs(string logTable, DataTable models)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["SomeDB"];
            string connectionString = settings.ConnectionString;

            OracleConnection conn = new OracleConnection(connectionString);

            try
            {
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction();

                foreach (DataRow model in models.Rows)
                {
                    string cmdText = $"UPDATE {logTable} SET IS_ERROR=-2 WHERE ID={model["id"].ParseToLong()}";

                    DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                    command.Transaction = transaction;

                    CipjsDbManager.Dgi.ExecuteNonQuery(command);
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void InsertLog(string logTable, DataRow model, long? errorId = null, string errorMessage = null)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["SomeDB"];
            string connectionString = settings.ConnectionString;

            OracleConnection conn = new OracleConnection(connectionString);

            try
            {
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction();

                string globalId = model["global_id"] != null && model["global_id"] != DBNull.Value ? model["global_id"].ToString() : "NULL";

                string updateDate = model.Table.Columns.Contains("update_date") ? CrossDBSQL.ToDate(model["update_date"].ParseToDateTime(), CrossDBSQL.Providers.PrvOracle) : "NULL";


                string dateImport = CrossDBSQL.ToDate(DateTime.Now, CrossDBSQL.Providers.PrvOracle);

                int isError = errorMessage.IsNotEmpty() ? 1 : 0;

                string message = (errorMessage ?? "");
                string strErrorId = errorId.HasValue ? errorId.Value.ToString() : "NULL";


                string cmdText = $"INSERT INTO {logTable} (ID, GLOBAL_ID, UPDATE_DATE, IS_ERROR, MESSAGE, ERROR_ID, IMPORT_DATE) " +
                        $"VALUES({model["ID"].ParseToLong()}, {globalId}, {updateDate}, {isError}, '{message.Replace("'", "''")}', {strErrorId}, {dateImport})";

                DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                command.Transaction = transaction;

                CipjsDbManager.Dgi.ExecuteNonQuery(command);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void UpdateLog(string logTable, DataRow row, long? errorId = null, string errorMessage = null)
        {
            string dateImport = CrossDBSQL.ToDate(DateTime.Now, CrossDBSQL.Providers.PrvOracle);

            int isError = errorMessage.IsNotEmpty() ? 1 : 0;

            string message = (errorMessage ?? "");
            string strErrorId = errorId.HasValue ? errorId.Value.ToString() : "NULL";

            string cmdText = $"UPDATE {logTable} SET IS_ERROR={isError}, MESSAGE='{message.Replace("'", "''")}', ERROR_ID={strErrorId}, IMPORT_DATE={dateImport} WHERE ID={row["id"].ParseToLong()}";

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
            CipjsDbManager.Dgi.ExecuteNonQuery(command);
        }

        private void ImportCommon(string logTable, GetDataTable getData, SaveObject saveObjectMethod)
        {
            this.Log($"Запуск импорта объектов {logTable}");

            _packageNumber = 0;
            _objectIds = null;

            int successRowCount = 0;
            int failRowCount = 0;

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            DataTable currentModels = getData();

            while (true)
            {
                if (currentModels.Rows.Count == 0)
                {
                    break;
                }

                // Сохранение в журнал должно идти до начала выборки объектов, чтобы объекты не попадали в выборку повторно
                //UpdateLogs(logTable, currentModels);

                //Task<DataTable> taskNextModels =
                //	Task.Factory.StartNew<DataTable>(() =>
                //	{
                //		return getData();
                //	}, cancelTokenSource.Token);

                ParallelOptions options = new ParallelOptions()
                {
                    CancellationToken = cancelTokenSource.Token,
                    MaxDegreeOfParallelism = ThreadsCount
                };

                Parallel.ForEach(currentModels.AsEnumerable(), options, x =>
                {
                    try
                    {
                        saveObjectMethod(x);

                        UpdateLog(logTable, x);

                        //InsertLog(logTable, x);

                        successRowCount++;
                    }
                    catch (Exception ex)
                    {
                        long errorId = ErrorManager.LogError(ex);

                        UpdateLog(logTable, x, errorId, $"При импорте {logTable} произошла ошибка: {ex.Message} (журнал №{errorId})");

                        //InsertLog(logTable, x, errorId, $"При импорте {logTable} произошла ошибка: {ex.Message} (журнал №{errorId})");

                        failRowCount++;
                    }
                });

                this.Log($"{logTable}: Импортировано {successRowCount}. Не удалось импортировать {failRowCount}.", replaceLast: true);

                //currentModels = taskNextModels.Result;
                currentModels = getData();
            }

            this.Log($"Завершен импорт объектов {logTable}: Импортировано {successRowCount}. Не удалось импортировать {failRowCount}");
        }

        private void SetActual(string tableName, string tableLogName, string quantTableName, string actualColumnName)
        {
            this.Log($"Запуск установки признака Actual {tableName}");

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand($"select t.id, t.actual, t.global_id from {tableName} t join {tableLogName} l on l.id = t.id where t.actual is not null and l.ACTUAL_SET is null");

            int count = 0;

            using (var reader = CipjsDbManager.Dgi.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    long id = reader.GetInt64(0);
                    long actual = reader.GetInt64(1);
                    long? globalId = reader.GetValue(2).ParseToLongNullable();

                    string globalIdStr = globalId.HasValue ? globalId.Value.ToString() : "NULL";

                    DbCommand updateCommand = DBMngr.Realty.GetSqlStringCommand($"update {quantTableName} set {actualColumnName} = {actual}, global_Id = {globalIdStr} where emp_id = {id}");
                    DBMngr.Realty.ExecuteNonQuery(updateCommand);

                    DbCommand updateLogCommand = CipjsDbManager.Dgi.GetSqlStringCommand($"update {tableLogName} set actual_set = 1 where id = {id}");
                    CipjsDbManager.Dgi.ExecuteNonQuery(updateLogCommand);

                    count++;
                }
            }

            this.Log($"Завершена установка признака Actual {tableName}. Обновлено объектов: {count}");
        }

        #endregion

        #region BuildingParcel

        private DataTable GetBuildingParcelTable()
        {
            string commandText =
$@"SELECT B.ID, 
    B.GLOBAL_ID, 
    B.NAME, 
    B.ASSIGNATION_CODE, 
    B.AREA, 
    DBMS_LOB.substr(B.NOTES, 4000) AS NOTES, 
    B.DEGREE_READINESS, 
    B.ACTUAL, 
    B.UPDATE_DATE, 
    B.TYPE, 
    B.SUBBUILDINGS, 
    B.OBJECT_ID, 
    B.PACKAGE_ID, 
    B.ACTUAL_ON_DATE,
    ROWNUM AS QS_ROWNUM
FROM {ImportConfig.BuildingParcel} B join {ImportConfig.BuildingParcelLog} l on l.id = b.id WHERE l.is_error = -1 and rownum <= {ImportConfig.PackageSize}";

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(commandText);

            return CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0];
        }

        private void SaveBuildParcel(DataRow row)
        {
            if (row == null) return;

            OMBuildParcel omBuildParcel = new OMBuildParcel
            {
                EmpId = row["ID"].ParseToLong(),
                BuildingParcelId = row["ID"].ParseToLong(),
                AssignationName_Code = (BuildingPurposeRosreestr)EnumExtensions.GetEnumByDescription<BuildingPurposeRosreestr>(row["ASSIGNATION_CODE"]?.ToString())
            };

            omBuildParcel.GlobalId = row["GLOBAL_ID"]?.ParseToLong();
            omBuildParcel.Name = row["NAME"].ToString();
            omBuildParcel.AssignationCode = row["ASSIGNATION_CODE"]?.ToString();
            omBuildParcel.Area = row["AREA"]?.ParseToDecimal();
            omBuildParcel.Notes = row["NOTES"]?.ToString();
            omBuildParcel.AssignationName = row["ASSIGNATION_CODE"]?.ToString();
            omBuildParcel.DegreeReadiness = row["DEGREE_READINESS"]?.ParseToDecimal();
            omBuildParcel.ActualEHD = row["ACTUAL"]?.ParseToLong();
            omBuildParcel.UpdateDateEHD = row["UPDATE_DATE"]?.ParseToDateTime();
            omBuildParcel.Type = row["TYPE"]?.ToString();
            omBuildParcel.Subbuildings = row["SUBBUILDINGS"]?.ToString();
            omBuildParcel.ObjectId = row["OBJECT_ID"]?.ToString();
            omBuildParcel.PackageId = row["PACKAGE_ID"]?.ParseToLong();
            omBuildParcel.ActualOnDate = row["ACTUAL_ON_DATE"]?.ParseToDateTime();
            omBuildParcel.LoadDate = DateTime.Now;

            omBuildParcel.Save();
        }

        #endregion

        #region Register

        private DataTable GetRegisterTable()
        {
            string commandText = $@"SELECT R.* FROM {ImportConfig.Register} R join {ImportConfig.RegisterLog} l on l.id = r.id where l.is_error = -1 and rownum <= {ImportConfig.PackageSize}";

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(commandText);

            return CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0];
        }

        private void SaveRegister(DataRow row)
        {
            if (row == null) return;

            OMRegister omRegister = new OMRegister
            {
                EmpId = row["ID"].ParseToLong(),
                BuildingParcelId = row["BUILDING_PARCEL_ID"].ParseToLong()
            };

            omRegister.GlobalId = row["GLOBAL_ID"]?.ParseToLong();
            omRegister.CadastralNumberParent = row["CADASTRAL_NUMBER_PARENT"]?.ToString();
            omRegister.CadastralNumber = row["CADASTRAL_NUMBER"]?.ToString();
            omRegister.DateCreated = row["DATE_CREATED"]?.ParseToDateTime();
            omRegister.DateRemoved = row["DATE_REMOVED"]?.ParseToDateTime();
            omRegister.State_Code = (State)EnumExtensions.GetEnumByDescription<State>(row["STATE"]?.ToString());
            omRegister.Method = row["METHOD"]?.ToString();
            omRegister.CadastralNumberOks = row["CADASTRAL_NUMBER_OKS"]?.ToString();
            omRegister.CadastralNumberKk = row["CADASTRAL_NUMBER_KK"]?.ToString();
            omRegister.CadastralNumberFlat = row["CADASTRAL_NUMBER_FLAT"]?.ToString();
            omRegister.TotalAss = row["TOTALASS"]?.ToString();
            omRegister.Assftp1_Code = (Assftp1)EnumExtensions.GetEnumByDescription<Assftp1>(row["ASSFTP1"]?.ToString());
            omRegister.AssftpCd_Code = (Assftp_cd)EnumExtensions.GetEnumByDescription<Assftp_cd>(row["ASSFTP_CD"]?.ToString());
            omRegister.LoadDate = DateTime.Now;

            omRegister.Save();
        }

        #endregion

        #region Location

        private DataTable GetLocationTable()
        {
            string commandText = $@"SELECT L.* FROM {ImportConfig.Location} L join {ImportConfig.LocationLog} log on log.id = l.id where log.is_error = -1 and rownum <= {ImportConfig.PackageSize}";

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(commandText);

            return CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0];
        }

        private void SaveLocation(DataRow row)
        {
            if (row == null) return;

            OMLocation omLocation = new OMLocation
            {
                EmpId = row["ID"].ParseToLong(),
                LocationEhdId = row["ID"].ParseToLong()
            };

            omLocation.ParcelId = row["PARCEL_ID"]?.ParseToLong();
            omLocation.PersonId = row["PERSON_ID"]?.ParseToLong();
            omLocation.OrganizationId = row["ORGANIZATION_ID"]?.ParseToLong();
            omLocation.BuildingParcelId = row["BUILDING_PARCEL_ID"]?.ParseToLong();
            omLocation.GlobalId = row["GLOBAL_ID"]?.ParseToLong();
            omLocation.Placed = row["PLACED"]?.ToString();
            omLocation.InBounds = row["IN_BOUNDS"]?.ToString();
            omLocation.CodeOkato = row["CODE_OKATO"]?.ToString();
            omLocation.CodeKladr = row["CODE_KLADR"]?.ToString();
            omLocation.PostalCode = row["POSTAL_CODE"]?.ToString();
            omLocation.Region = row["REGION"]?.ToString();
            omLocation.District = row["DISTRICT"]?.ToString();
            omLocation.City = row["CITY"]?.ToString();
            omLocation.UrbanDistrict = row["URBAN_DISTRICT"]?.ToString();
            omLocation.SovietVillage = row["SOVIET_VILLAGE"]?.ToString();
            omLocation.Locality = row["LOCALITY"]?.ToString();
            omLocation.Street = row["STREET"]?.ToString();
            omLocation.Level1 = row["LEVEL1"]?.ToString();
            omLocation.Level2 = row["LEVEL2"]?.ToString();
            omLocation.Level3 = row["LEVEL3"]?.ToString();
            omLocation.Apartment = row["APARTMENT"]?.ToString();
            omLocation.FullAddress = row["FULL_ADDRESS"]?.ToString();
            omLocation.AddressTotal = row["ADDRESS_TOTAL"]?.ToString();
            omLocation.Other = row["OTHER"]?.ToString();
            omLocation.LoadDate = DateTime.Now;

            omLocation.Save();
        }

        #endregion

        #region Egrp

        private DataTable GetEgrpTable()
        {
            string commandText =
$@"SELECT E.*, S.AREA AS SPACE_AREA
FROM {ImportConfig.Egrp} E
LEFT JOIN {ImportConfig.Space} S ON S.EGRP_ID=E.ID AND S.AREATP_CD='Общая площадь'
JOIN {ImportConfig.EgrpLog} l on l.id = e.id
where l.is_error = -1 and rownum <= {ImportConfig.PackageSize}";

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(commandText);

            return CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0];
        }

        private void SaveEgrp(DataRow row)
        {
            if (row == null) return;

            OMEgrp omEgrp = new OMEgrp
            {
                EmpId = row["ID"].ParseToLong(),
                EhdEgrnId = row["ID"].ParseToLong()
            };

            omEgrp.Area = row["SPACE_AREA"]?.ParseToDecimal();
            omEgrp.GlobalId = row["GLOBAL_ID"]?.ParseToLong();
            omEgrp.ObjtCd = row["OBJT_CD"]?.ToString();
            omEgrp.ObjecttpCd = row["OBJECTTP_CD"]?.ToString();
            omEgrp.RegtpCd = row["REGTP_CD"]?.ToString();
            omEgrp.DisttpCd = row["DISTTP_CD"]?.ToString();
            omEgrp.CitytpCd = row["CITYTP_CD"]?.ToString();
            omEgrp.LoctpCd = row["LOCTP_CD"]?.ToString();
            omEgrp.StrtpCd = row["STRTP_CD"]?.ToString();
            omEgrp.Level1tpCd = row["LEVEL1TP_CD"]?.ToString();
            omEgrp.Level2tpCd = row["LEVEL2TP_CD"]?.ToString();
            omEgrp.Level3tpCd = row["LEVEL3TP_CD"]?.ToString();
            omEgrp.AparttpCd = row["APARTTP_CD"]?.ToString();
            omEgrp.PurposetpCd = row["PURPOSETP_CD"]?.ToString();
            omEgrp.ObjectstCd = row["OBJECTST_CD"]?.ToString();
            omEgrp.ActstCd = row["ACTST_CD"]?.ToString();
            omEgrp.FaktCd = row["FAKT_CD"]?.ToString();
            omEgrp.BydocCd = row["BYDOC_CD"]?.ToString();
            omEgrp.GroundcatCd = row["GROUNDCAT_CD"]?.ToString();
            omEgrp.Purpose = row["PURPOSE"]?.ToString();
            omEgrp.Invnum = row["INVNUM"]?.ToString();
            omEgrp.Literbti = row["LITERBTI"]?.ToString();
            omEgrp.AddrRefmark = row["ADDR_REFMARK"]?.ToString();
            omEgrp.AddrId = row["ADDR_ID"]?.ToString();
            omEgrp.AddrCdcountry = row["ADDR_CDCOUNTRY"]?.ToString();
            omEgrp.AddrCdokato = row["ADDR_CDOKATO"]?.ToString();
            omEgrp.AddrPostcd = row["ADDR_POSTCD"]?.ToString();
            omEgrp.AddrDistName = row["ADDR_DIST_NAME"]?.ToString();
            omEgrp.AddrDistCd = row["ADDR_DIST_CD"]?.ToString();
            omEgrp.AddrCityName = row["ADDR_CITY_NAME"]?.ToString();
            omEgrp.AddrCityCd = row["ADDR_CITY_CD"]?.ToString();
            omEgrp.AddrLocName = row["ADDR_LOC_NAME"]?.ToString();
            omEgrp.AddrLocCd = row["ADDR_LOC_CD"]?.ToString();
            omEgrp.AddrStrName = row["ADDR_STR_NAME"]?.ToString();
            omEgrp.AddrStrCd = row["ADDR_STR_CD"]?.ToString();
            omEgrp.AddrLevel1Num = row["ADDR_LEVEL1_NUM"]?.ToString();
            omEgrp.AddrLevel2Num = row["ADDR_LEVEL2_NUM"]?.ToString();
            omEgrp.AddrLevel3Num = row["ADDR_LEVEL3_NUM"]?.ToString();
            omEgrp.AddrApart = row["ADDR_APART"]?.ToString();
            omEgrp.AddrOther = row["ADDR_OTHER"]?.ToString();
            omEgrp.AddrNote = row["ADDR_NOTE"]?.ToString();
            omEgrp.NumCadnum = row["NUM_CADNUM"]?.ToString();
            omEgrp.NumCondnum = row["NUM_CONDNUM"]?.ToString();
            omEgrp.Name = row["NAME"]?.ToString();
            omEgrp.FloorGr = row["FLOOR_GR"]?.ToString();
            omEgrp.FloorUnd = row["FLOOR_UND"]?.ToString();
            omEgrp.TecharHeight = row["TECHAR_HEIGHT"]?.ParseToDecimal();
            omEgrp.TecharLenght = row["TECHAR_LENGHT"]?.ParseToDecimal();
            omEgrp.TecharVol = row["TECHAR_VOL"]?.ParseToDecimal();
            omEgrp.NumFloor = row["NUM_FLOOR"]?.ToString();
            omEgrp.NumFlat = row["NUM_FLAT"]?.ToString();
            omEgrp.Regdt = row["REGDT"]?.ParseToDateTime();
            omEgrp.Brkdt = row["BRKDT"]?.ParseToDateTime();
            omEgrp.Mdfdt = row["MDFDT"]?.ParseToDateTime();
            omEgrp.Updt = row["UPDT"]?.ParseToDateTime();
            omEgrp.ActDt = row["ACT_DT"]?.ParseToDateTime();
            omEgrp.ObjectId = row["OBJECT_ID"]?.ToString();
            omEgrp.UpdateDate = row["UPDATE_DATE"]?.ParseToDateTime();
            omEgrp.ActualId = row["ACTUAL"]?.ParseToLong();
            omEgrp.ActualOnDate = row["ACTUAL_ON_DATE"]?.ParseToDateTime();
            omEgrp.AddressTotal = row["ADDRESS_TOTAL"]?.ToString();
            omEgrp.Json = row["JSON"]?.ToString();
            omEgrp.LoadDate = DateTime.Now;

            omEgrp.Save();
        }

        #endregion

        #region Right

        private DataTable GetRightTable()
        {
            string commandText = $@"SELECT R.* FROM {ImportConfig.Right} R join {ImportConfig.RightLog} l on l.id = r.id where l.is_error = -1 and rownum <= {ImportConfig.PackageSize}";

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(commandText);

            return CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0];
        }

        private void SaveRight(DataRow row)
        {
            if (row == null) return;

            OMRight omRight = new OMRight
            {
                EmpId = row["ID"].ParseToLong(),
                EhdRightId = row["ID"].ParseToLong()
            };

            //omRight.EgrpId = omEgrp.EmpId;
            omRight.GlobalId = row["GLOBAL_ID"]?.ParseToLong();
            omRight.EhdRightId = row["ID"].ParseToLong();
            omRight.Mdfdt = row["MDFDT"]?.ParseToDateTime();
            omRight.ObjectId = row["OBJECT_ID"]?.ToString();
            omRight.EgrpId = row["EGRP_ID"]?.ParseToLong();
            omRight.RegCloseRegdt = row["REG_CLOSE_REGDT"]?.ParseToDateTime();
            omRight.RegCloseRegnum = row["REG_CLOSE_REGNUM"]?.ToString();
            omRight.RegOpenRegdt = row["REG_OPEN_REGDT"]?.ParseToDateTime();
            omRight.RegOpenRegnum = row["REG_OPEN_REGNUM"]?.ToString();
            omRight.RightstCd = row["RIGHTST_CD"]?.ToString();
            omRight.RighttpCd_Code = (VidPravaRasshirennoyChasti)EnumExtensions.GetEnumByDescription<VidPravaRasshirennoyChasti>(row["RIGHTTP_CD"]?.ToString());
            omRight.RightKey = row["RIGHT_KEY"]?.ToString();
            omRight.SharecomflatDen = row["SHARECOMFLAT_DEN"]?.ParseToDecimal();
            omRight.SharecomflatNum = row["SHARECOMFLAT_NUM"]?.ParseToDecimal();
            omRight.SharecomflatText = row["SHARECOMFLAT_TEXT"]?.ToString();
            omRight.SharecomDen = row["SHARECOM_DEN"]?.ParseToDecimal();
            omRight.SharecomNum = row["SHARECOM_NUM"]?.ParseToDecimal();
            omRight.SharecomText = row["SHARECOM_TEXT"]?.ToString();
            omRight.ShareDen = row["SHARE_DEN"]?.ParseToDecimal();
            omRight.ShareNum = row["SHARE_NUM"]?.ParseToDecimal();
            omRight.ShareText = row["SHARE_TEXT"]?.ToString();
            omRight.TpName = row["TP_NAME"]?.ToString();
            omRight.LoadDate = DateTime.Now;

            omRight.Save();
        }

        #endregion

        #region OldNumbers

        private DataTable GetOldNumbersTable()
        {
            string commandText = $@"SELECT N.* FROM {ImportConfig.OldNumbers} N join {ImportConfig.OldNumbersLog} l on l.id = N.id where l.is_error = -1 and rownum <= {ImportConfig.PackageSize}";

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(commandText);

            return CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0];
        }

        private void SaveOldNumbers(DataRow row)
        {
            if (row == null) return;

            OMOldNumber omOldNumber = new OMOldNumber
            {
                Id = row["ID"].ParseToLong()
            };

            omOldNumber.GlobalId = row["GLOBAL_ID"].ParseToLongNullable();
            omOldNumber.Type = row["TYPE"].ToString();
            omOldNumber.Number = row["NUMBER"].ToString();
            omOldNumber.Date = row["DATE"].ParseToDateTimeNullable();
            omOldNumber.Organ = row["ORGAN"].ToString();
            omOldNumber.RegisterId = row["REGISTER_ID"].ParseToLong();
            omOldNumber.LoadDate = DateTime.Now;

            omOldNumber.Save();
        }

        #endregion



        #region Floors

        private DataTable GetFloorsTable()
        {
            string commandText = $@"SELECT L.* FROM {ImportConfig.Floors} L join {ImportConfig.FloorsLog} log on log.id = l.id where log.is_error = -1 and rownum <= {ImportConfig.PackageSize}";

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(commandText);

            return CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0];
        }

        private void SaveFloors(DataRow row)
        {
            if (row == null) return;

            OMFloors omFloors = new OMFloors
            {
                Id = row["ID"].ParseToInt(),
                BuildingParcelId = row["BUILDING_PARCEL_ID"]?.ParseToInt(),
                GlobalId = row["GLOBAL_ID"]?.ParseToLong(),
                Floors = row["FLOORS"]?.ParseToString(),
                UndergroundFloors = row["UNDERGROUND_FLOORS"]?.ParseToString()
            };

            omFloors.Save();
        }

        #endregion

        #region ElementsConstruct

        private DataTable GetElementsConstructTable()
        {
            string commandText = $@"SELECT L.* FROM {ImportConfig.ElementsConstruct} L join {ImportConfig.ElementsConstructLog} log on log.id = l.id where log.is_error = -1 and rownum <= {ImportConfig.PackageSize}";

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(commandText);

            return CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0];
        }

        private void SaveElementsConstruct(DataRow row)
        {
            if (row == null) return;

            ObjectModel.Ehd.Elements.OMConstruct omConstruct = new ObjectModel.Ehd.Elements.OMConstruct
            {
                Id = row["ID"].ParseToInt(),
                BuildingParcelId = row["BUILDING_PARCEL_ID"]?.ParseToInt(),
                GlobalId = row["GLOBAL_ID"]?.ParseToLong(),
                Wall = row["WALL"]?.ParseToString()
            };

            omConstruct.Save();
        }

        #endregion

        #region ExploitationChar

        private DataTable GetExploitationCharTable()
        {
            string commandText = $@"SELECT L.* FROM {ImportConfig.ExploitationChar} L join {ImportConfig.ExploitationCharLog} log on log.id = l.id where log.is_error = -1 and rownum <= {ImportConfig.PackageSize}";

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(commandText);

            return CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0];
        }

        private void SaveExploitationChar(DataRow row)
        {
            if (row == null) return;

            ObjectModel.Ehd.Exploitation.OMChar omChar = new ObjectModel.Ehd.Exploitation.OMChar
            {
                Id = row["ID"].ParseToInt(),
                BuildingParcelId = row["BUILDING_PARCEL_ID"]?.ParseToInt(),
                GlobalId = row["GLOBAL_ID"]?.ParseToLong(),
                YearBuilt = row["YEAR_BUILT"]?.ParseToString(),
                YearUsed = row["YEAR_USED"]?.ParseToString()
            };

            omChar.Save();
        }

        #endregion
    }
}