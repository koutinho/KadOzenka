using CIPJS.DAL.Bti.Import.Enum;
using Core.Diagnostics;
using Core.ErrorManagment;
using Core.Register;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.Td;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Bti;
using ObjectModel.Directory;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace CIPJS.DAL.Bti.Import
{
    public class Importer
    {
        private const string Unom = "UNOM";
        private const string Unad = "UNAD";

        private static ConfigBtiObjectsImport ConfigBtiObjectsImport
        {
            get
            {
                return Core.ConfigParam.Configuration.GetParam<ConfigBtiObjectsImport>("BtiObjectsImportConditions", "\\Bti\\");
            }
        }

        public static string BtiFads
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_Fads;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_Fads)");
                }

                return value;
            }
        }

        public static string BtiDiapKv
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_DiapKv;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_DiapKv)");
                }

                return value;
            }
        }

        private string BtiFkmn
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_Fkmn;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_Fkmn)");
                }

                return value;
            }
        }

        private string BtiFkva
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_Fkva;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_Fkva)");
                }

                return value;
            }
        }

        private string BtiFsks
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_Fsks;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_Fsks)");
                }

                return value;
            }
        }

        private string BtiEts
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_Ets;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_Ets)");
                }

                return value;
            }
        }

        private string BtiLog
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_Log;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_Log)");
                }

                return value;
            }
        }

        private string BtiFlatLog
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_flat_Log;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_flat_Log)");
                }

                return value;
            }
        }

        private string BtiAddressLog
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_address_Log;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_address_Log)");
                }

                return value;
            }
        }

        private string BtiFadsLog
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_fads_Log;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_fads_Log)");
                }

                return value;
            }
        }


        public static string FkunTableName
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_Fkun;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_Fkun)");
                }

                return value;
            }
        }

        public static string KlsTableName
        {
            get
            {
                string value = ConfigBtiObjectsImport.import_bti_daily_Kls;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (import_bti_daily_Kls)");
                }

                return value;
            }
        }

        public static string RefAddrStreetTableName
        {
            get
            {
                string value = ConfigBtiObjectsImport.ref_addr_street_table;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (ref_addr_street_table)");
                }

                return value;
            }
        }
        public static string RefAddrDistrictTableName
        {
            get
            {
                string value = ConfigBtiObjectsImport.ref_addr_district_table;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (ref_addr_district_table)");
                }

                return value;
            }
        }

        public static string RefAddrOkrugTableName
        {
            get
            {
                string value = ConfigBtiObjectsImport.ref_addr_okrug_table;

                if (value.IsNullOrEmpty())
                {
                    throw new Exception("Не удалось определить настройку для импорта БТИ (ref_addr_okrug_table)");
                }

                return value;
            }
        }

        private int MaxDegreeOfParallelism
        {
            get
            {
                int value = ConfigBtiObjectsImport.MaxDegreeOfParallelism;

                if (value == 0)
                {
                    return 4;
                }

                return value.ParseToInt();
            }
        }

        private ReferenceCache _referenceCache;
        private CancellationToken _cancelToken;
        private int _tdInstanceID;

        private DateTime _startDate = DateTime.Now;

        private int _processedCount = 0;
        private int _errorCount = 0;

        private bool _needAddrSync;
        private string _exactObjectUnom;

        private bool _newFlatExists = true;
        private Dictionary<long, OMPremase> _existingPremisesDict;
        private Dictionary<long, long> _budingUnomEmpIdDict;

        private HashSet<long> _existingAddresses;


        private LoadType LoadType =>
            string.IsNullOrEmpty(_exactObjectUnom) ?
                LoadType.Update :
                LoadType.UpdateSingle;

        public Importer(bool needAddrSync, CancellationToken cancelToken, string exactObjectUnom = null)
        {
            _needAddrSync = needAddrSync;
            _cancelToken = cancelToken;
            _exactObjectUnom = exactObjectUnom;
        }

        public void Import()
        {
            //_tdInstanceID = DocProcessor.CreateDefaultTD();
            _cancelToken.ThrowIfCancellationRequested();

            // Создаем набор изменений, чтобы не было ошибки при параллельном выполнении
            //RegisterStorage.GetOrCreateChangeSet(_tdInstanceID, null);
            //_cancelToken.ThrowIfCancellationRequested();

            if (_needAddrSync)
            {
                SyncAddresses();
            }
            else
            {
                Console.WriteLine("Пропущена синхронизация адресов");
            }

            //инициализация кэша без поддержки многопоточности
            InitReferenceCache();

            ImportBuildings();

            ImportAddresses();

            ImportFads();

            ImportFlats();

            ImportDiapKv();
        }

        private void SyncAddresses()
        {
            Logger logger = new Logger("Bti.DailyImport", "SyncAddresses", SRDSession.GetCurrentUserId());

            SynchronizationAddresses addr = new SynchronizationAddresses();
            addr.Merge();

            logger.TraceFinal("Закончена синхронизация адресов");
        }

        public void InitReferenceCache()
        {
            _referenceCache = new ReferenceCache();
        }

        private void ImportBuildings()
        {
            List<DataRow> currentModels = GetBuildingsForUpdate();

            while (true)
            {
                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss} получено записей зданий: {1}", DateTime.Now, currentModels.Count);

                if (currentModels.Count == 0)
                {
                    break;
                }

                // Сохранение в журнал должно идти до начала выборки объектов, чтобы объекты не попадали в выборку повторно
                WriteRowsInLog(currentModels, -1);

                _cancelToken.ThrowIfCancellationRequested();

                ParallelOptions options = new ParallelOptions
                {
                    CancellationToken = _cancelToken,
                    MaxDegreeOfParallelism = MaxDegreeOfParallelism
                };
                Parallel.ForEach(currentModels, options, LoadBuilding);

                currentModels = GetBuildingsForUpdate();
            }
        }

        private void ImportAddresses()
        {
            this._existingAddresses = new HashSet<long>();
            while (true)
            {
                List<DataRow> currentModels = GetAddressesForUpdate();
                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss} получено записей адресов: {1}", DateTime.Now, currentModels.Count);
                if (currentModels.Count == 0)
                    break;

                _cancelToken.ThrowIfCancellationRequested();
                if (LoadType == LoadType.UpdateSingle)
                {
                    foreach (var row in currentModels)
                        LoadAddress(row);
                    return;
                }
                else
                {
                    Parallel.ForEach(
                        currentModels,
                        new ParallelOptions
                        {
                            CancellationToken = _cancelToken,
                            MaxDegreeOfParallelism = MaxDegreeOfParallelism
                        },
                        LoadAddress);
                }
            }
        }


        private void ImportFads()
        {
            // Раскомментировать только при дебаге
            // InitReferenceCache();

            this._existingAddresses = new HashSet<long>();
            while (true)
            {
                List<DataRow> currentModels = GetFadsForUpdate();
                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss} получено записей адресов (FADS): {1}", DateTime.Now, currentModels.Count);
                if (currentModels.Count == 0)
                    break;

                _cancelToken.ThrowIfCancellationRequested();
                if (LoadType == LoadType.UpdateSingle)
                {
                    foreach (var row in currentModels)
                        LoadFads(row);
                    return;
                }
                else
                {
                    Parallel.ForEach(
                        currentModels,
                        new ParallelOptions
                        {
                            CancellationToken = _cancelToken,
                            MaxDegreeOfParallelism = MaxDegreeOfParallelism
                        },
                        LoadFads);
                }
            }
        }

        /// <summary>
        /// Импорт IMPORT_BTI_FADS -> bti.Fads 
        /// </summary>
        /// <param name="row"></param>
        private void LoadFads(DataRow row)
        {

            Logger logger = new Logger("Bti.DailyImport", "LoadFads", SRDSession.GetCurrentUserId());
            DateTime dateFrom = row["DATEEDIT"] != DBNull.Value ? row["DATEEDIT"].ParseToDateTime() : DateTime.Now;
            try
            {
                OMFads fad = CreateFad(row);
                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    fad.Save(tdInstanceID: _tdInstanceID, fromDate: dateFrom);
                    logger.TraceFinal("Загружены адреса объекта", $"Идентификатор объекта (BTI_FADS_Q.EMP_ID): {fad.EmpId}");
                    ts.Complete();
                }
                LogRecordFads(row, fad);
            }
            catch (Exception ex)
            {
                LogRecordFads(row, null, ex.Message, ErrorManager.LogError(ex));
                _errorCount++;
            }


        }

        private void LogRecordFads(DataRow fadRow, OMFads fad, string errorMessage = null, int? errorId = null)
        {
            if (LoadType == LoadType.UpdateSingle) return;


            Database db = CipjsDbManager.Dgi;

            string selectCmd = $"select count(1) as rows_cnt from {BtiFadsLog} l where l.unom = {fadRow["UNOM"]} and l.unad = {(fadRow["UNAD"] == DBNull.Value ? 0 : fadRow["UNAD"])}";

            DbCommand commandSelect = CipjsDbManager.Dgi.GetSqlStringCommand(selectCmd);
            DataTable dt = CipjsDbManager.Dgi.ExecuteDataSet(commandSelect).Tables[0];
            int count = dt.Rows[0]["rows_cnt"].ParseToInt();

            string cmdText;

            if (count == 0)
            {
                cmdText = $"INSERT INTO {BtiFadsLog} (OBJ_ID,UNOM,UNAD,DATEEDIT,IS_NEW,CIPJS_FADS_ID,IS_ERROR,MESSAGE,ERROR_ID,IMPORT_DATE) VALUES (:OBJ_ID,:UNOM,:UNAD,:DATEEDIT,:IS_NEW,:CIPJS_FADS_ID,:IS_ERROR,:MESSAGE,:ERROR_ID,:IMPORT_DATE)";
            }
            else
            {
                cmdText = $"update {BtiFadsLog} set OBJ_ID = :OBJ_ID, UNOM = :UNOM, UNAD = :UNAD, DATEEDIT = :DATEEDIT, IS_NEW = :IS_NEW, CIPJS_FADS_ID = :CIPJS_FADS_ID, IS_ERROR = :IS_ERROR, MESSAGE = :MESSAGE, ERROR_ID = :ERROR_ID, IMPORT_DATE = :IMPORT_DATE WHERE unom = {fadRow["UNOM"]} and unad = {(fadRow["UNAD"] == DBNull.Value ? 0 : fadRow["UNAD"])}";
            }

            DbCommand command = db.GetSqlStringCommand(cmdText);
            db.AddInParameter(command, "OBJ_ID", DbType.Int64, fadRow["OBJ_ID"]);
            db.AddInParameter(command, "UNOM", DbType.String, fadRow["UNOM"]);
            db.AddInParameter(command, "UNAD", DbType.Int32, fadRow["UNAD"] == DBNull.Value ? (object)0 : fadRow["UNAD"]);
            db.AddInParameter(command, "DATEEDIT", DbType.DateTime, fadRow["DATEEDIT"]);
            db.AddInParameter(command, "IS_NEW", DbType.Int32, fad == null ? DBNull.Value : this._existingAddresses.Contains(fad.EmpId) ? (object)0 : (object)1);
            db.AddInParameter(command, "CIPJS_FADS_ID", DbType.Int64, fad == null ? DBNull.Value : (object)fad.EmpId);
            db.AddInParameter(command, "IS_ERROR", DbType.Int32, errorMessage == null ? (int)ImportStatus.Success : (int)ImportStatus.Error);
            db.AddInParameter(command, "MESSAGE", DbType.String, errorMessage == null ? DBNull.Value : errorMessage as object);
            db.AddInParameter(command, "ERROR_ID", DbType.Int32, errorId.HasValue ? errorId.Value as object : DBNull.Value);
            db.AddInParameter(command, "IMPORT_DATE", DbType.DateTime, DateTime.Now);
            db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Создание записи bti_fads_q.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private OMFads CreateFad(DataRow row)
        {
            OMFads fad = new OMFads();

            // 25004400	Дата изменения
            fad.DateEdit = row["DATEEDIT"]?.ParseToDateTimeNullable() ?? DateTime.Now;
            // 25000200	ID объекта недвижимости
            fad.ObjId = row["OBJ_ID"]?.ParseToLongNullable();

            // 25000300 Тип объекта недвижимости
            ReferenceCacheItem reference = GetReferenceCacheItemById(row, "OBJ_TYPE");
            long? referenceCode;
            string referenceValue;
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.ObjType_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            fad.ObjType = referenceValue;
            // 25000400	Полное юридическое написание адреса или описания местоположения
            fad.Adres = row["ADRES"]?.ParseToString();
            // 25000500	Порядковый номер адреса для объекта
            fad.Unad = row["UNAD"]?.ParseToLongNullable();

            // 25000600 Субъект РФ
            reference = GetReferenceCacheItemById(row, "P1");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.P1_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            fad.P1 = referenceValue;

            // 25000700	Поселение
            reference = GetReferenceCacheItemById(row, "P3");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.P3_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            fad.P3 = referenceValue;

            // 25000800	Город
            reference = GetReferenceCacheItemById(row, "P4");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.P4_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            fad.P4 = referenceValue;

            // 25000900	Муниципальный округ
            reference = GetReferenceCacheItemById(row, "P5");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.P5_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            fad.P5 = referenceValue;

            // 25001000	Населённый пункт
            reference = GetReferenceCacheItemById(row, "P6");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.P6_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            fad.P6 = referenceValue;

            // 25001100   Улица, элемент планировочной структуры
            var codeGivc = row["P7"]?.ParseToString();
            StreetCacheItem streetCacheItem = _referenceCache.GetStreetCacheItem(codeGivc);
            fad.P7_Code = streetCacheItem != null ? streetCacheItem.Id : null;
            fad.P7 = streetCacheItem != null ? streetCacheItem.Name : null;


            // 25001200	Дополнительный адресообразующий элемент
            reference = GetReferenceCacheItemById(row, "P90");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.P90_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            fad.P90 = referenceValue;

            // 25001300	Уточнение дополнительного адресообразующего элемента
            reference = GetReferenceCacheItemById(row, "P91");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.P91_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            fad.P91 = referenceValue;

            // 25001500	Номер дома/владения/участка
            fad.L1Value = row["L1_VALUE"]?.ParseToString();
            if (string.IsNullOrEmpty(fad.L1Value))
            {
                reference = GetReferenceCacheItemById(row, "L1_TYPE");
                ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
                fad.L1Type_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
                fad.L1Type = referenceValue;
            }

            // 25001700	Номер корпуса
            fad.L2Value = row["L2_VALUE"]?.ParseToString();
            if (string.IsNullOrEmpty(fad.L2Value))
            {
                reference = GetReferenceCacheItemById(row, "L2_TYPE");
                ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
                fad.L2Type_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
                fad.L2Type = referenceValue;
            }

            // 25001900	Номер строения/сооружения
            fad.L3Value = row["L3_VALUE"]?.ParseToString();
            if (string.IsNullOrEmpty(fad.L3Value))
            {
                reference = GetReferenceCacheItemById(row, "L3_TYPE");
                ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
                fad.L3Type_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
                fad.L3Type = referenceValue;
            }

            // 25002100	Номер помещения
            fad.L4Value = row["L4_VALUE"]?.ParseToString();
            if (string.IsNullOrEmpty(fad.L4Value))
            {
                reference = GetReferenceCacheItemById(row, "L4_TYPE");
                ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
                fad.L4Type_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
                fad.L4Type = referenceValue;
            }

            // 25002200	Административный округ
            reference = GetReferenceCacheItemById(row, "ADM_AREA");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.AdmArea_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            fad.AdmArea = referenceValue;

            // 25002300	Муниципальный округ/поселение
            var district = row["DISTRICT"]?.ParseToLongNullable();
            reference = _referenceCache.GetReferenceItemById(45, district) ?? _referenceCache.GetReferenceItemById(532, district);
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.District_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            fad.District = referenceValue;

            // 25002400	Регистрационный номер в Адресном реестре
            fad.Nreg = row["NREG"]?.ParseToLongNullable();
            // 25002500	Дата регистрации в Адресном реестре
            fad.Dreg = row["DREG"]?.ParseToDateTimeNullable();
            // 25002600	Кадастровый номер объекта недвижимости
            fad.KadN = row["KAD_N"]?.ParseToString();
            // 25002600	Кадастровый номер земельного участка
            fad.KadZu = row["KAD_ZU"]?.ParseToString();

            // 25002800	Документ-основание регистрационных действий
            reference = GetReferenceCacheItemById(row, "TDOC");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.TDoc_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            fad.TDoc = referenceValue;

            // 25002900	№ документа о регистрации адреса
            fad.Ndoc = row["NDOC"]?.ParseToString();
            // 25003000	Дата документа о регистрации адреса
            fad.Ddoc = row["DDOC"]?.ParseToDateTimeNullable();
            // 25003100	UNOM объекта (только для строения, сооружения, ЗУ, ОНС)
            fad.Unom = row["UNOM"]?.ParseToLongNullable();

            if (fad.Unom != null)
            {
                var btiBuilding = OMBtiBuilding.Where(x => x.Unom == fad.Unom).ExecuteFirstOrDefault();
                fad.BtiBuildingId = btiBuilding?.EmpId;
            }

            // 25003200	Тип адреса
            reference = GetReferenceCacheItemById(row, "ADR_TYPE");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.AdrType_Code = referenceValue.IsNotEmpty() ? (AddressStatus)referenceCode : 0;
            fad.AdrType = referenceValue;

            // 25003200	Тип адреса
            reference = GetReferenceCacheItemById(row, "SOSTAD");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.Sostad_Code = referenceValue.IsNotEmpty() ? referenceCode : 0;
            fad.Sostad = referenceValue;
            // 25003400	Код КЛАДР для адресообразующиего элемента нижнего уровня
            fad.Kladr = row["KLADR"]?.ParseToString();
            // 25003500	Код (GUID) ФИАС для адреса
            fad.Nfias = row["N_FIAS"]?.ParseToString();
            // 25003600	Дата начала действия адреса в ФИАС
            fad.Dfias = row["D_FIAS"]?.ParseToDateTimeNullable();
            // 25003200	Тип адреса
            reference = GetReferenceCacheItemById(row, "VID");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.Vid_Code = referenceValue.IsNotEmpty() ? referenceCode : 0;
            fad.Vid = referenceValue;
            // 25003800	Признак "главного адреса" объекта
            fad.MainAdr = row["MAIN_ADR"]?.ParseToBooleanNullable();
            // 25003900	Комментарий к адресу
            fad.Commnt = row["COMMNT"]?.ParseToString();
            // 25004000	Статус адреса
            reference = GetReferenceCacheItemById(row, "STATUS");
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            fad.FadStatus_Code = referenceValue.IsNotEmpty() ? referenceCode : 0;
            fad.FadStatus = referenceValue;
            // 25004100	Признак "строительного адреса" объекта
            fad.BuildingAddress = row["BUILDING_ADDRESS"]?.ParseToBooleanNullable();

            // 25004200	Адм. округ
            string aoCode = row["AO"]?.ParseToString();
            OkrugCacheItem okrugCacheItem = _referenceCache.GetOkrugCacheItem(aoCode);
            fad.AO_Code = okrugCacheItem != null ? okrugCacheItem.Id : null;
            fad.AO = okrugCacheItem != null ? okrugCacheItem.Name : null;

            // 25004300	Муниципальный округ
            string mrCode = row["MR"]?.ParseToString();
            DistrictCacheItem districtCacheItem = _referenceCache.GetDistrictCacheItem(mrCode);
            fad.MR_Code = districtCacheItem != null ? districtCacheItem.Id : null;
            fad.MR = districtCacheItem != null ? districtCacheItem.Name : null;

            // 25004500	EAID
            fad.EaId = row["EAID"]?.ParseToLongNullable();

            return fad;
        }

        private void ImportFlats()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            List<DataRow> currentModels = GetFlatsForUpdate();

            while (true)
            {
                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss} получено записей квартир: {1}", DateTime.Now, currentModels.Count);

                if (currentModels.Count == 0)
                {
                    break;
                }

                _cancelToken.ThrowIfCancellationRequested();

                ParallelOptions options = new ParallelOptions
                {
                    CancellationToken = _cancelToken,
                    MaxDegreeOfParallelism = MaxDegreeOfParallelism
                };

                List<long?> existingPremisesIds = currentModels.Select(x => x["OBJ_ID"].ParseToLongNullable()).ToList();

                // Получение существующих квартир
                _existingPremisesDict = new Dictionary<long, OMPremase>();

                foreach (var premase in OMPremase.Where(x => existingPremisesIds.Contains(x.IdInSource))
                                            .Select(x => x.IdInSource)
                                            .Select(x => x.UpdateDate)
                                            .Execute())
                {
                    // TODO: разобраться с дублями в БД по IdInSource
                    if (!premase.IdInSource.HasValue || _existingPremisesDict.ContainsKey(premase.IdInSource.Value))
                    {
                        continue;
                    }

                    _existingPremisesDict.Add(premase.IdInSource.Value, premase);
                }

                List<long?> existingUnoms = currentModels.Select(x => x["UNOM"].ParseToLongNullable()).ToList();

                // Получение ИД домов
                _budingUnomEmpIdDict = new Dictionary<long, long>();


                foreach (var building in OMBtiBuilding.Where(x => existingUnoms.Contains(x.Unom)).Select(x => x.Unom).Execute())
                {
                    // TODO: разобраться с дублями в БД по Unom
                    if (!building.Unom.HasValue || _budingUnomEmpIdDict.ContainsKey(building.Unom.Value))
                    {
                        continue;
                    }

                    _budingUnomEmpIdDict.Add(building.Unom.Value, building.EmpId);
                }

                Parallel.ForEach(currentModels, options, LoadFlat);

                currentModels = GetFlatsForUpdate();
            }
        }

        /// <summary>
        /// 1. select max(download_date) from r_alt_bulding_q
        /// 2. select * from sks where update_date > max(Download_date)
        /// </summary>
        /// <returns></returns>
        private List<DataRow> GetBuildingsForUpdate()
        {
            string filter = String.Join("AND", ConfigBtiObjectsImport.Conditions.Select(x => $"nvl({x.Column}, -1) IN (" + String.Join(",", x.AllowedValues.Select(y => $"'{y}'")) + ")"));

            string cmdText = $@"select distinct sks.obj_id, nvl(sks.dateedit,sks.dtsost) as dateedit, sks.unom as new_unom, sks.KL, sks.OPL, sks.PROC, 
    sks.GDPROC, sks.NAZ, sks.MST, sks.ET_PDZ, sks.GDPOSTR, sks.ET, sks.SOST, sks.DTSOST, sks.PAMARC, sks.AVARZD, sks.DTAVARZD, sks.SAMOVOL, sks.OPL_G, sks.PDVPL_N,
    sks.NARPL, sks.KAT, sks.SER, sks.GDPEREOB, sks.GDKAPREM, sks.OPL_N, sks.KAP, sks.ET_MIN, sks.BTI, sks.KOMM, sks.OBJ_TYPE, sks.KROVPL, sks.LFPQ, sks.LFGPQ,
    sks.LFGQ, sks.PMQ_G, sks.KMQ_G, sks.KWQ, sks.PRKOR, sks.HPL, sks.ELEQ, sks.GAZQ, sks.BPL, sks.LPL, sks.PEREKR, sks.KROV, sks.OTSKORP,
    (select fads.kad_n from {BtiFads} fads where fads.unom = sks.unom and fads.main_adr = 1 and rownum = 1) KAD_N
    from {BtiFsks} sks
    left join {BtiLog} l on l.bti_id = sks.obj_id
    where {filter} and (l.bti_id is null or (l.is_error = 1 or l.dateedit < sks.dateedit) and l.is_error <> {(int)ImportStatus.ReservedByTask}  and l.import_date < {CrossDBSQL.ToDate(_startDate, CrossDBSQL.Providers.PrvOracle)}) and rownum <= 1000";

            if (LoadType == LoadType.UpdateSingle)
            {
                cmdText += string.Format(" and sks.unom in ({0})", _exactObjectUnom);
            }

            DataTable dt;

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                DbCommand selectCommand = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                dt = CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0];
            }

            return dt.AsEnumerable().ToList();
        }

        /// <summary>
        /// Получение DiapKv по ObjId из БТИ.
        /// </summary>
        /// <param name="objId"> OBJ_ID БТИ </param>
        /// <returns></returns>
        private DataRow GetBuildingDiapKv(long? objId)
        {
            string cmdText =
               $@"SELECT dkv.OBJ_ID, dkv.OBJ_TYPE, dkv.UNOM, dkv.N1, dkv.I1, dkv.N2, dkv.I2, dkv.Q, dkv.DUP, dkv.RIM FROM {BtiDiapKv} dkv WHERE dkv.OBJ_ID = {objId} AND ROWNUM = 1";

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                DbCommand selectCommand = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                return CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0].AsEnumerable().FirstOrDefault();
            }

        }

        /// <summary>
        /// Получение всех DiapKv из БТИ.
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        private List<DataRow> GetDiapKvs()
        {
            string cmdText =
               $@"SELECT dkv.OBJ_ID, dkv.OBJ_TYPE, dkv.UNOM, dkv.N1, dkv.I1, dkv.N2, dkv.I2, dkv.Q, dkv.DUP, dkv.RIM FROM {BtiDiapKv} dkv";

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                DbCommand selectCommand = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                return CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0].AsEnumerable().ToList();
            }

        }

        /// <summary>
        /// Получение DiapKv из Бти по Unom.
        /// </summary>
        /// <param name="unom"></param>
        /// <returns></returns>
        public DataRow GetDiapKvByUnom(long? unom)
        {
            string cmdText =
              $@"SELECT dkv.OBJ_ID, dkv.OBJ_TYPE, dkv.UNOM, dkv.N1, dkv.I1, dkv.N2, dkv.I2, dkv.Q, dkv.DUP, dkv.RIM FROM {BtiDiapKv} dkv WHERE dkv.UNOM = {unom}";

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                DbCommand selectCommand = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                return CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0].AsEnumerable().FirstOrDefault();
            }
        }

        public List<DataRow> GetAddressesForUpdate()
        {
            string filter = String.Join("AND", ConfigBtiObjectsImport.Conditions.Select(x => $"nvl(sks.{x.Column}, -1) IN (" + String.Join(",", x.AllowedValues.Select(y => $"'{y}'")) + ")"));

            string cmdText =
$@"select
    ibf.OBJ_ID, ibf.OBJ_TYPE, ibf.ADRES, ibf.UNAD, ibf.P1, ibf.P3, ibf.P4, ibf.P5, ibf.P6, ibf.P7, ibf.P90, 
    ibf.P91, ibf.L1_TYPE, ibf.L1_VALUE, ibf.L2_TYPE, ibf.L2_VALUE, ibf.L3_TYPE, ibf.L3_VALUE, ibf.L4_TYPE, 
    ibf.L4_VALUE, ibf.ADM_AREA, ibf.DISTRICT, ibf.NREG, ibf.DREG, ibf.KAD_N, ibf.KAD_ZU, ibf.TDOC, 
    ibf.NDOC, ibf.DDOC, ibf.UNOM, ibf.ADR_TYPE, ibf.SOSTAD, ibf.KLADR, ibf.N_FIAS, ibf.D_FIAS, ibf.VID, 
    ibf.MAIN_ADR, ibf.COMMNT, ibf.STATUS, ibf.BUILDING_ADDRESS, ibf.AO, ibf.MR, ibf.DATEEDIT, ibf.EAID
from {BtiFads} ibf
join {BtiFsks} sks
  on sks.unom = ibf.unom
left join {BtiAddressLog} l on l.unom = ibf.unom and l.unad = ibf.unad
where rownum <= 1000 and ibf.OBJ_TYPE = 27602
and {filter} AND (l.obj_id is null
    or (l.is_error <> {(int)ImportStatus.ReservedByTask}
        and (l.is_error = 1 or l.dateedit < ibf.dateedit)
        and l.import_date < {CrossDBSQL.ToDate(_startDate, CrossDBSQL.Providers.PrvOracle)}
    )
)";
            if (LoadType == LoadType.UpdateSingle)
                cmdText += $" and ibf.UNOM = {this._exactObjectUnom}";

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                DbCommand selectCommand = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                return CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0].AsEnumerable().ToList();
            }
        }

        public List<DataRow> GetFadsForUpdate()
        {
            string filter = String.Join("AND", ConfigBtiObjectsImport.Conditions.Select(x => $"nvl(sks.{x.Column}, -1) IN (" + String.Join(",", x.AllowedValues.Select(y => $"'{y}'")) + ")"));

            string cmdText =
$@"select
    ibf.OBJ_ID, ibf.OBJ_TYPE, ibf.ADRES, ibf.UNAD, ibf.P1, ibf.P3, ibf.P4, ibf.P5, ibf.P6, ibf.P7, ibf.P90, 
    ibf.P91, ibf.L1_TYPE, ibf.L1_VALUE, ibf.L2_TYPE, ibf.L2_VALUE, ibf.L3_TYPE, ibf.L3_VALUE, ibf.L4_TYPE, 
    ibf.L4_VALUE, ibf.ADM_AREA, ibf.DISTRICT, ibf.NREG, ibf.DREG, ibf.KAD_N, ibf.KAD_ZU, ibf.TDOC, 
    ibf.NDOC, ibf.DDOC, ibf.UNOM, ibf.ADR_TYPE, ibf.SOSTAD, ibf.KLADR, ibf.N_FIAS, ibf.D_FIAS, ibf.VID, 
    ibf.MAIN_ADR, ibf.COMMNT, ibf.STATUS, ibf.BUILDING_ADDRESS, ibf.AO, ibf.MR, ibf.DATEEDIT, ibf.EAID
from {BtiFads} ibf
join {BtiFsks} sks
  on sks.unom = ibf.unom
left join {BtiFadsLog} l on l.unom = ibf.unom and l.unad = ibf.unad
where rownum <= 1000 and ibf.OBJ_TYPE = 27602
and {filter} AND (l.obj_id is null
    or (l.is_error <> {(int)ImportStatus.ReservedByTask}
        and (l.is_error = 1 or l.dateedit < ibf.dateedit)
        and l.import_date < {CrossDBSQL.ToDate(_startDate, CrossDBSQL.Providers.PrvOracle)}
    )
)";
            if (LoadType == LoadType.UpdateSingle)
                cmdText += $" and ibf.UNOM = {this._exactObjectUnom}";

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                DbCommand selectCommand = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                return CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0].AsEnumerable().ToList();
            }
        }

        private List<DataRow> GetFlatsForUpdate()
        {
            string filter = String.Join("AND", ConfigBtiObjectsImport.Conditions.Select(x => $"nvl(sks.{x.Column}, -1) IN (" + String.Join(",", x.AllowedValues.Select(y => $"'{y}'")) + ")"));

            string columns = @"fkva.UNOM, fkva.OBJ_ID, fkva.KAD_N, fkva.ET, fkva.TET, fkva.KV, fkva.RIM, fkva.UNKV, fkva.DTINV,
fkva.OPL, fkva.GPL, fkva.PPL, fkva.KVNOM, fkva.KVI, fkva.KL KLA, fkva.TP, fkva.KMQ, fkva.DTKORR, fkva.UNOM, fkva.NSEK, fkva.OBJ_TYPE, fkva.ZPL, fkva.BIT0, 
fkva.TRES, fkva.SRES, fkva.DRES, fkva.NRES, fkva.AR_DTVV, fkva.AR_DT, fkva.AR_OSN, fkva.AR_DTSN, fkva.AR_OSNSN";

            DataTable dt;
            string cmdText;

            // Получаем список новых квартир
            if (_newFlatExists)
            {
                cmdText = $@"select distinct NULL as LOG_ID, {columns}
					from {BtiFkva} fkva
					   join {BtiFsks} sks on fkva.UNOM = sks.UNOM
					where {filter} and not exists (select 1 from {BtiFlatLog} l where l.bti_flat_id = fkva.obj_id) and rownum <= 1000";

                if (LoadType == LoadType.UpdateSingle)
                {
                    cmdText += string.Format(" and sks.unom in ({0})", _exactObjectUnom);
                }

                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
                {
                    DbCommand selectCommand = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                    dt = CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    return dt.AsEnumerable().ToList();
                }
            }

            _newFlatExists = false;

            // Получаем список обновленных квартир и квартир загруженных с ошибками
            cmdText = $@"select distinct l.BTI_FLAT_ID as LOG_ID, {columns}
					from {BtiFkva} fkva
					   join {BtiFsks} sks on fkva.UNOM = sks.UNOM
					   join {BtiFlatLog} l on l.bti_flat_id = fkva.obj_id
					where {filter} and ((l.is_error = 1 or l.dateedit < fkva.DTKORR) and l.import_date < {CrossDBSQL.ToDate(_startDate, CrossDBSQL.Providers.PrvOracle)}) and rownum <= 1000";

            if (LoadType == LoadType.UpdateSingle)
            {
                cmdText += string.Format(" and sks.unom in ({0})", _exactObjectUnom);
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                DbCommand selectCommand = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                dt = CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0];
            }

            return dt.AsEnumerable().ToList();
        }

        private void LoadBuilding(DataRow buildingRow)
        {
            try
            {
                OMBtiBuilding building = null;

                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    Logger logger = new Logger("Bti.DailyImport", "LoadBuilding", SRDSession.GetCurrentUserId());

                    DateTime dateFrom = buildingRow["dateedit"] != DBNull.Value ? buildingRow["dateedit"].ParseToDateTime() : DateTime.Now;

                    building = CreateBuilding(buildingRow, dateFrom);
                    building.Save(tdInstanceID: _tdInstanceID, fromDate: dateFrom);

                    // CIPJS-862: Импорт bti_data.diapkv и выведение показателей в интерфейс и реестры.
                    LoadDiapKv(buildingRow, building.EmpId);

                    logger.TraceFinal("Загружен объект", string.Format("Идентификатор объекта (R_ALT_BUILDING_Q.EMP_ID): {0}", building.EmpId));


                    ts.Complete();
                }

                LogRecord(buildingRow, building);
            }
            catch (Exception ex)
            {
                LogRecord(buildingRow, null, ex.Message, ErrorManager.LogError(ex));
                _errorCount++;
            }
            finally
            {
                _processedCount++;
            }
        }

        /// <summary>
        /// CIPJS-862: Импорт bti_data.diapkv и выведение показателей в интерфейс и реестры.
        /// Загрузка или обновления записи OMDiapKv.
        /// </summary>
        /// <param name="buildingRow"></param>
        /// <param name="builingId"> OMBtiBuilding emp_id </param>
        private void LoadDiapKv(DataRow buildingRow, long? builingId)
        {
            long? btiBuildingId = buildingRow["OBJ_ID"]?.ParseToLong();
            if (btiBuildingId == null)
            {
                throw new ArgumentNullException("Неопределенный OBJ_ID");
            }
            if (btiBuildingId != null && builingId != null)
            {
                OMDiapKv diapKv = CreateOrUpdateDiapKv(btiBuildingId, builingId);
            }
        }

        /// <summary>
        /// Создание или обновление записи OMDiapKv.
        /// </summary>
        /// <param name="btiBuildingId"> OBJ_ID БТИ </param>
        /// <param name="buildingId"> OMBtiBuilding emp_id </param>
        /// <returns></returns>
        public OMDiapKv CreateOrUpdateDiapKv(long? btiBuildingId, long? buildingId)
        {
            // Проверка на пустое значение здания.
            if (buildingId == null)
            {
                return null;
            }
            // Получение diapKv из БТИ.
            DataRow diapKvRow = GetBuildingDiapKv(btiBuildingId);
            if (diapKvRow == null)
            {
                return null;
            }

            // Получение diapKv.
            OMDiapKv diapKv = OMDiapKv.Where(x => x.ObjId == btiBuildingId && x.SpecialActual == 1).SelectAll().ExecuteFirstOrDefault();

            // Если нет, то создаем и привязываем к OBJ_ID БТИ. 
            if (diapKv == null)
            {
                diapKv = new OMDiapKv();
                diapKv.ObjId = btiBuildingId;
            }

            // Инициализация полей.
            // 26000300	Тип
            ReferenceCacheItem reference = GetReferenceCacheItemById(diapKvRow, "OBJ_TYPE");
            long? referenceCode;
            string referenceValue;
            ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
            var objType_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
            var objType = referenceValue;

            // 26000400	Уникальный идентификатор здания
            var unom = diapKvRow["UNOM"]?.ParseToLongNullable();
            // 26000500	N1
            var N1 = diapKvRow["N1"]?.ParseToLongNullable();
            // 26000600	Минимальный номер помещения
            var I1 = diapKvRow["I1"]?.ParseToLongNullable();
            // 26000700	N2
            var N2 = diapKvRow["N2"]?.ParseToLongNullable();
            // 26000800	Максимальный номер помещения
            var I2 = diapKvRow["I2"]?.ParseToString();
            // 26000900	Количество помещений в диапазоне
            var Q = diapKvRow["Q"]?.ParseToLongNullable();
            // 26001000	dup
            var DUP = diapKvRow["DUP"]?.ParseToLongNullable();
            // 26001100	rim
            var RIM = diapKvRow["RIM"]?.ParseToString();

            // Проверяем на изменения или отсутствие значений,
            // положительно - обновляем.
            if (diapKv.ObjType_Code != objType_Code ||
               diapKv.Unom != unom ||
               diapKv.N1 != N1 ||
               diapKv.I1 != I1 ||
               diapKv.N2 != N2 ||
               diapKv.I2 != I2 ||
               diapKv.Q != Q ||
               diapKv.Dup != DUP ||
               diapKv.Rim != RIM ||
               diapKv.EmpId != buildingId)
            {
                diapKv.EmpId = buildingId;
                diapKv.ObjType = objType;
                diapKv.ObjType_Code = objType_Code;
                diapKv.Unom = unom;
                diapKv.N1 = N1;
                diapKv.I1 = I1;
                diapKv.N2 = N2;
                diapKv.I2 = I2;
                diapKv.Q = Q;
                diapKv.Dup = DUP;
                diapKv.Rim = RIM;
            }

            diapKv.Save();

            return diapKv;
        }

        /// <summary>
        /// Инициализация OMDiapKv на основе БТИ. 
        /// Загрузка новых и обновление старых данных.
        /// </summary>
        public void ImportDiapKv()
        {
            Logger logger = new Logger("Bti.DailyImport", "InitialDiapKvLoad", SRDSession.GetCurrentUserId());
            try
            {
                // Получение diapKv из Bti.
                List<DataRow> diapKvRows = GetDiapKvs();
                if (diapKvRows == null || diapKvRows.Count == 0)
                {
                    throw new Exception($"Не найдены записи в IMPORT_BTI_DIAPKV");
                }
                logger.Trace($"Найдено записей IMPORT_BTI_DIAPKV в кол-ве: {diapKvRows.Count}");

                _cancelToken.ThrowIfCancellationRequested();

                ParallelOptions options = new ParallelOptions
                {
                    CancellationToken = _cancelToken,
                    MaxDegreeOfParallelism = MaxDegreeOfParallelism
                };

                Parallel.ForEach(diapKvRows, options, diapKvRow =>
                {
                    using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
                    {
                        // 26000400	Уникальный идентификатор здания.
                        var unom = diapKvRow["UNOM"]?.ParseToLongNullable();

                        if (unom == null)
                        {
                            return;
                        }
                        // OBJ_ID БТИ.
                        var btiBuildingId = diapKvRow["OBJ_ID"]?.ParseToLongNullable();

                        // Ищем здание для связи с diapkv.
                        OMBtiBuilding building = OMBtiBuilding.Where(x => x.Unom == unom && x.SpecialActual == 1).ExecuteFirstOrDefault();
                        if (building == null)
                        {
                            return;
                        }

                        // Ищем существующее diapKv.
                        var diapKv = OMDiapKv.Where(x => x.EmpId == building.EmpId && x.ObjId == btiBuildingId).SelectAll().ExecuteFirstOrDefault();

                        if (diapKv == null)
                        {
                            diapKv = new OMDiapKv();
                        }

                        // Инициализация полей.
                        // 26000300	Тип
                        ReferenceCacheItem reference = GetReferenceCacheItemById(diapKvRow, "OBJ_TYPE");
                        long? referenceCode;
                        string referenceValue;
                        ExtractReferenceCacheItemCodeValue(reference, out referenceCode, out referenceValue);
                        var objType_Code = referenceValue.IsNotEmpty() ? referenceCode : null;
                        var objType = referenceValue;


                        // 26000500	N1
                        var N1 = diapKvRow["N1"]?.ParseToLongNullable();
                        // 26000600	Минимальный номер помещения
                        var I1 = diapKvRow["I1"]?.ParseToLongNullable();
                        // 26000700	N2
                        var N2 = diapKvRow["N2"]?.ParseToLongNullable();
                        // 26000800	Максимальный номер помещения
                        var I2 = diapKvRow["I2"]?.ParseToString();
                        // 26000900	Количество помещений в диапазоне
                        var Q = diapKvRow["Q"]?.ParseToLongNullable();
                        // 26001000	dup
                        var DUP = diapKvRow["DUP"]?.ParseToLongNullable();
                        // 26001100	rim
                        var RIM = diapKvRow["RIM"]?.ParseToString();

                        // Если не происходило изменений останавливаем.
                        if (diapKv != null && diapKv.I1 == I1 && diapKv.I2 == I2 && diapKv.ObjType_Code == objType_Code)
                        {
                            return;
                        }


                        diapKv.EmpId = building?.EmpId;
                        diapKv.ObjId = btiBuildingId;
                        diapKv.ObjType = objType;
                        diapKv.ObjType_Code = objType_Code;
                        diapKv.Unom = unom;
                        diapKv.N1 = N1;
                        diapKv.I1 = I1;
                        diapKv.N2 = N2;
                        diapKv.I2 = I2;
                        diapKv.Q = Q;
                        diapKv.Dup = DUP;
                        diapKv.Rim = RIM;

                        diapKv.Save();
                        ts.Complete();
                    }

                });

                logger.TraceFinal("Объекты DiapKv загружены.");
            }
            catch (Exception ex)
            {
                logger.TraceFinal($"Ошибка: {ex.Message} StackTrace: {ex.StackTrace}");
            }
        }

        private void LoadAddress(DataRow addressRow)
        {
            Logger logger = new Logger("Bti.DailyImport", "LoadAddress", SRDSession.GetCurrentUserId());
            DateTime dateFrom = addressRow["DATEEDIT"] != DBNull.Value ? addressRow["DATEEDIT"].ParseToDateTime() : DateTime.Now;
            try
            {
                OMADDRESS address = CreateAddress(addressRow);
                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    address.Save(tdInstanceID: _tdInstanceID, fromDate: dateFrom);
                    //создаем связку адрес - объект
                    OMBtiBuilding oMAltBuilding = OMBtiBuilding.Where(x => x.Unom == addressRow["UNOM"].ParseToLong() && x.Source_Code == InsuranceSourceType.Bti).ExecuteFirstOrDefault();
                    OMADDRLINK oMAddrlink = CreateAddrlink(addressRow, address, oMAltBuilding);
                    if (oMAddrlink != null)
                        oMAddrlink.Save(tdInstanceID: _tdInstanceID, fromDate: dateFrom);
                    else if (oMAltBuilding == null)
                        logger.Trace("Не найдено здание с unom = " + addressRow["UNOM"]);
                    logger.TraceFinal("Загружены адреса объекта", $"Идентификатор объекта (BTI_ADDRESS_Q.EMP_ID): {address.EmpId}");
                    ts.Complete();
                }
                LogRecordAddress(addressRow, address);
            }
            catch (Exception ex)
            {
                LogRecordAddress(addressRow, null, ex.Message, ErrorManager.LogError(ex));
                _errorCount++;
            }
        }

        private void LoadFlat(DataRow flatRow)
        {
            try
            {
                OMPremase flat = null;

                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    Logger logger = new Logger("Bti.DailyImport", "LoadFlat", SRDSession.GetCurrentUserId());

                    DateTime dateFrom = flatRow["DTKORR"] != DBNull.Value ? flatRow["DTKORR"].ParseToDateTime() : DateTime.Now;

                    long unom = flatRow["UNOM"].ParseToLong();

                    long altBuildingId;
                    if (!_budingUnomEmpIdDict.TryGetValue(unom, out altBuildingId))
                    {
                        throw new Exception("Не найдено здание с UNOM: " + unom);
                    }

                    flat = CreatePremase(flatRow, _existingPremisesDict, dateFrom, altBuildingId);

                    logger.TraceFinal("Загружена квартира", string.Format("Идентификатор объекта (FLAT.EMP_ID): {0}", flat.EmpId));

                    ts.Complete();
                }

                LogRecordFlat(flatRow, flat);
            }
            catch (Exception ex)
            {
                LogRecordFlat(flatRow, null, ex.Message, ErrorManager.LogError(ex));
                _errorCount++;
            }
            finally
            {
                _processedCount++;
            }
        }

        private void WriteRowsInLog(IEnumerable<DataRow> models, int taskId)
        {
            string connectionString = CipjsDbManager.Dgi.ConnectionString;
            OracleConnection conn = new OracleConnection(connectionString);
            try
            {
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction();

                foreach (DataRow model in models)
                {
                    string strUnom = model["new_unom"] != null && model["new_unom"] != DBNull.Value ? model["new_unom"].ToString() : "NULL";

                    long? buildingId = null;
                    long isNew = 1;

                    if (model.Table.Columns.Contains("emp_id")
                        && model["emp_id"] != DBNull.Value)
                    {
                        buildingId = model["emp_id"].ParseToLong();
                        isNew = 0;
                    }

                    string dateEdit = CrossDBSQL.ToDate(model["dateedit"].ParseToDateTime(), CrossDBSQL.Providers.PrvOracle);

                    string buildingIdStr = buildingId.HasValue ? buildingId.Value.ToString() : "NULL";

                    string selectCmd = $"select count(1) as rows_cnt from {BtiLog} where BTI_ID = {model["obj_id"].ParseToLong()}";

                    DbCommand commandSelect = CipjsDbManager.Dgi.GetSqlStringCommand(selectCmd);
                    commandSelect.Transaction = transaction;

                    DataTable dt = CipjsDbManager.Dgi.ExecuteDataSet(commandSelect).Tables[0];
                    int count = dt.Rows[0]["rows_cnt"].ParseToInt();

                    if (count == 0)
                    {
                        string cmdText = $"INSERT INTO {BtiLog} (BTI_ID, NUM_CADNUM, UNOM, IS_NEW, ALT_BUILDING_ID, DATEEDIT, IS_ERROR, TASK_ID) " +
                            $"VALUES({model["obj_id"].ParseToLong()},'',{strUnom},{isNew},{buildingIdStr},{dateEdit},{(int)ImportStatus.ReservedByTask},{taskId})";

                        DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                        command.Transaction = transaction;

                        CipjsDbManager.Dgi.ExecuteNonQuery(command);
                    }
                    else
                    {
                        string updateCmdText = $"update {BtiLog} set NUM_CADNUM = '', UNOM = {strUnom}, IS_NEW = {isNew}, ALT_BUILDING_ID = {buildingIdStr}, " +
                            $"DATEEDIT = {dateEdit}, IS_ERROR = {(int)ImportStatus.ReservedByTask}, TASK_ID = {taskId} where BTI_ID = {model["obj_id"].ParseToLong()}";

                        DbCommand commandUpd = CipjsDbManager.Dgi.GetSqlStringCommand(updateCmdText);
                        commandUpd.Transaction = transaction;

                        CipjsDbManager.Dgi.ExecuteNonQuery(commandUpd);
                    }
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

        private void LogRecord(DataRow btiRow, OMBtiBuilding building, string errorMessage = null, int? errorId = null)
        {
            if (LoadType == LoadType.UpdateSingle) return;

            string dateImport = CrossDBSQL.ToDate(DateTime.Now, CrossDBSQL.Providers.PrvOracle);

            ImportStatus isError = errorMessage.IsNotEmpty() ? ImportStatus.Error : ImportStatus.Success;
            string message = (errorMessage ?? "");
            string strErrorId = errorId.HasValue ? errorId.Value.ToString() : "NULL";
            string cmdText =
                string.Format(@"UPDATE {0} SET IS_ERROR={1}, MESSAGE='{2}', ERROR_ID={3}, IMPORT_DATE={4} {5} WHERE BTI_ID={6}",
                BtiLog, (int)isError, message.Replace("'", "''"), strErrorId, dateImport, building != null ? ", ALT_BUILDING_ID = " + building.EmpId : "", btiRow["obj_id"].ParseToLong());

            DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
            CipjsDbManager.Dgi.ExecuteNonQuery(command);
        }

        private void LogRecordAddress(DataRow addressRow, OMADDRESS address, string errorMessage = null, int? errorId = null)
        {
            if (LoadType == LoadType.UpdateSingle) return;


            Database db = CipjsDbManager.Dgi;

            string selectCmd = $"select count(1) as rows_cnt from {BtiAddressLog} l where l.unom = {addressRow["UNOM"]} and l.unad = {(addressRow["UNAD"] == DBNull.Value ? 0 : addressRow["UNAD"])}";

            DbCommand commandSelect = CipjsDbManager.Dgi.GetSqlStringCommand(selectCmd);
            DataTable dt = CipjsDbManager.Dgi.ExecuteDataSet(commandSelect).Tables[0];
            int count = dt.Rows[0]["rows_cnt"].ParseToInt();

            string cmdText;

            if (count == 0)
            {
                cmdText = $"INSERT INTO {BtiAddressLog} (OBJ_ID,UNOM,UNAD,DATEEDIT,IS_NEW,BTI_ADDRESS_ID,IS_ERROR,MESSAGE,ERROR_ID,IMPORT_DATE) VALUES (:OBJ_ID,:UNOM,:UNAD,:DATEEDIT,:IS_NEW,:BTI_ADDRESS_ID,:IS_ERROR,:MESSAGE,:ERROR_ID,:IMPORT_DATE)";
            }
            else
            {
                cmdText = $"update {BtiAddressLog} set OBJ_ID = :OBJ_ID, UNOM = :UNOM, UNAD = :UNAD, DATEEDIT = :DATEEDIT, IS_NEW = :IS_NEW, BTI_ADDRESS_ID = :BTI_ADDRESS_ID, IS_ERROR = :IS_ERROR, MESSAGE = :MESSAGE, ERROR_ID = :ERROR_ID, IMPORT_DATE = :IMPORT_DATE WHERE unom = {addressRow["UNOM"]} and unad = {(addressRow["UNAD"] == DBNull.Value ? 0 : addressRow["UNAD"])}";
            }

            DbCommand command = db.GetSqlStringCommand(cmdText);
            db.AddInParameter(command, "OBJ_ID", DbType.Int64, addressRow["OBJ_ID"]);
            db.AddInParameter(command, "UNOM", DbType.String, addressRow["UNOM"]);
            db.AddInParameter(command, "UNAD", DbType.Int32, addressRow["UNAD"] == DBNull.Value ? (object)0 : addressRow["UNAD"]);
            db.AddInParameter(command, "DATEEDIT", DbType.DateTime, addressRow["DATEEDIT"]);
            db.AddInParameter(command, "IS_NEW", DbType.Int32, address == null ? DBNull.Value : this._existingAddresses.Contains(address.EmpId) ? (object)0 : (object)1);
            db.AddInParameter(command, "BTI_ADDRESS_ID", DbType.Int64, address == null ? DBNull.Value : (object)address.EmpId);
            db.AddInParameter(command, "IS_ERROR", DbType.Int32, errorMessage == null ? (int)ImportStatus.Success : (int)ImportStatus.Error);
            db.AddInParameter(command, "MESSAGE", DbType.String, errorMessage == null ? DBNull.Value : errorMessage as object);
            db.AddInParameter(command, "ERROR_ID", DbType.Int32, errorId.HasValue ? errorId.Value as object : DBNull.Value);
            db.AddInParameter(command, "IMPORT_DATE", DbType.DateTime, DateTime.Now);
            db.ExecuteNonQuery(command);
        }

        private void LogRecordFlat(DataRow flatRow, OMPremase flat, string errorMessage = null, int? errorId = null)
        {
            if (LoadType == LoadType.UpdateSingle) return;

            string dateImport = CrossDBSQL.ToDate(DateTime.Now, CrossDBSQL.Providers.PrvOracle);

            int isError = errorMessage.IsNotEmpty() ? 1 : 0;
            string message = (errorMessage ?? "");
            string strErrorId = errorId.HasValue ? errorId.Value.ToString() : "NULL";

            string insurFlatIdStr = flat != null && flat.EmpId > 0 ? flat.EmpId.ToString() : "NULL";

            long objId = flatRow["obj_id"].ParseToLong();

            int isNew = _existingPremisesDict.ContainsKey(objId) ? 0 : 1;

            string dateEdit = flatRow["DTKORR"] != DBNull.Value ?
                CrossDBSQL.ToDate(flatRow["DTKORR"].ParseToDateTime(), CrossDBSQL.Providers.PrvOracle) :
                "NULL";

            /*
			BTI_FLAT_ID
			UNOM
			UNKV
			IS_NEW
			INSUR_FLAT_ID
			DATEEDIT
			IS_ERROR
			MESSAGE
			ERROR_ID
			IMPORT_DATE
			*/
            if (flatRow["LOG_ID"].ParseToLong() > 0)
            {

                string updateCmdText = $@"update {BtiFlatLog} set 
						UNOM = {flatRow["UNOM"]}, 
						UNKV = '{flatRow["UNKV"]}',
						IS_NEW = {isNew}, 
						INSUR_FLAT_ID = {insurFlatIdStr}, 
						DATEEDIT = {dateEdit}, 
						IS_ERROR = {isError},
						MESSAGE = '{message}',
						ERROR_ID = {strErrorId},
						IMPORT_DATE = {dateImport} 
					where BTI_FLAT_ID = {objId}";

                DbCommand commandUpd = CipjsDbManager.Dgi.GetSqlStringCommand(updateCmdText);
                CipjsDbManager.Dgi.ExecuteNonQuery(commandUpd);
            }
            else
            {
                string cmdText = $@"INSERT INTO {BtiFlatLog} 
					(
						BTI_FLAT_ID,
						UNOM,
						UNKV,
						IS_NEW,
						INSUR_FLAT_ID,
						DATEEDIT,
						IS_ERROR,
						MESSAGE,
						ERROR_ID,
						IMPORT_DATE) 
					VALUES(
						{objId},
						{flatRow["UNOM"]}, 
						'{flatRow["UNKV"]}',
						{isNew}, 
						{insurFlatIdStr}, 
						{dateEdit}, 
						{isError},
						'{message}',
						{strErrorId},
						{dateImport} 
					)";

                DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(cmdText);
                CipjsDbManager.Dgi.ExecuteNonQuery(command);
            }
        }

        #region Здания (251 - OMAltBuilding)
        //маппим вручную по ..\Inecas.RSM\Main\Source\LoaderLibrary\XML\Здания.xml
        ////////////////////////////////////////////////////////////////////////////////////////////
        //Не найдены в таблице FSKS:
        //1. TP Тип строения (Кл.84)
        //2. KAD_O Код кадастрового округа по данным Москомзема
        //3. KAD_RN Код кадастрового района по данным Москомзема (в данном случае просто меняется обозначение)
        //4. LIT Признак литеры
        //5. KVRTL Номер квартала БТИ
        //6. NOMD Номер дела
        //7. DTPOBS Дата последнего обследования
        //8. STOS Стоимость (строительная)
        //9. GPL Площадь жилая
        //10. COKPL_N Площадь нежилых цокольных этажей
        //11. PPL Площадь с летними
        //12. GDNADSTR Год надстройки
        //13. GDPRISTR Год пристройки
        private OMBtiBuilding CreateBuilding(DataRow row, DateTime dateFrom)
        {
            //UNOM
            long? unom = row["new_unom"] != DBNull.Value ? (long?)row["new_unom"].ParseToLong() : null;

            //устанавливаем id, если нет, то -1 создает новый
            long buildingId = -1;

            // Получить buildingId для текущей записи в базе-приемнике
            OMBtiBuilding existingBuilding =
                OMBtiBuilding.Where(x => x.Unom == unom && x.Source_Code == InsuranceSourceType.Bti).Execute().FirstOrDefault();

            if (existingBuilding != null)
            {
                buildingId = existingBuilding.EmpId;

                DbCommand selectCommand =
                    DBMngr.Realty.GetSqlStringCommand(
                        String.Format("SELECT S_ from BTI_BUILDING_Q WHERE EMP_ID = {0} AND S_ > {1} ",
                        buildingId, CrossDBSQL.ToDate(dateFrom)));
                DataTable dt = DBMngr.Realty.ExecuteDataSet(selectCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    throw new Exception(String.Format("Существует более поздняя запись от {0: dd.MM.yyyy HH:mm:ss}",
                        dt.Rows[0]["S_"].ParseToDateTime()));
                }
            }

            //Кадастровый номер
            string kadN = row["KAD_N"] != DBNull.Value ? row["KAD_N"].ToString() : null;

            //Класс строения
            ReferenceCacheItem klReference = GetReferenceCacheItem(row, "KL");
            long? klCode = null;
            string klValue = null;
            ExtractReferenceCacheItemCodeValue(klReference, out klCode, out klValue);
            string className = klValue != null ? klValue : string.Empty;
            BuildingClass classNameCode = klCode.HasValue ? (BuildingClass)klCode.Value : (BuildingClass)0L;

            //Процент износа
            long? actProc = row["PROC"] != DBNull.Value ? (long?)row["PROC"].ParseToLong() : null;

            //Год установки процента износа
            long? gdProc = row["GDPROC"] != DBNull.Value ? (long?)row["GDPROC"].ParseToLong() : null;

            //Назначение
            ReferenceCacheItem purposeReference = GetReferenceCacheItem(row, "NAZ");
            long? nazCode = null;
            string naz = null;
            ExtractReferenceCacheItemCodeValue(purposeReference, out nazCode, out naz);

            //Материал стен
            ReferenceCacheItem wallMaterialReference = GetReferenceCacheItem(row, "MST");
            long? mstCode;
            string mst;
            ExtractReferenceCacheItemCodeValue(wallMaterialReference, out mstCode, out mst);

            //Этажность подземная
            long? etPdz = row["ET_PDZ"] != DBNull.Value ? (long?)row["ET_PDZ"].ParseToLong() : null;

            //Площадь общая
            decimal? opl = row["OPL"] != DBNull.Value ? (decimal?)row["OPL"].ParseToDecimal() : null;

            //Год постройки
            long? gdPostr = row["GDPOSTR"] != DBNull.Value ? (long?)row["GDPOSTR"].ParseToLong() : null;

            //Этажность максимальная
            long? et = row["ET"] != DBNull.Value ? (long?)row["ET"].ParseToLong() : null;

            //Состояние строения
            ReferenceCacheItem stateNameReference = GetReferenceCacheItem(row, "SOST");
            long? sostCode;
            string sost;
            ExtractReferenceCacheItemCodeValue(stateNameReference, out sostCode, out sost);

            //Дата состояния
            DateTime? dtSost = null;
            if (row["DTSOST"] != DBNull.Value)
            {
                DateTime tempStateDate;
                if (row["DTSOST"].TryParseToDateTime(out tempStateDate))
                {
                    dtSost = tempStateDate;
                }
            }

            //Признак постройки до 1917 г.
            bool gdDo1917 = gdPostr.HasValue && gdPostr.Value <= 1917;

            //Признак Памятник архитектуры
            bool? pamarc = ParseBtiBoolean(row["PAMARC"]);

            //Признак Аварийное
            bool? avarzd = ParseBtiBoolean(row["AVARZD"]);

            //Дата решения об аварийности здания
            DateTime? dtAvarzd = null;
            if (row["DTAVARZD"] != DBNull.Value)
            {
                DateTime tempEmergencyDecisionDate;
                if (row["DTAVARZD"].TryParseToDateTime(out tempEmergencyDecisionDate))
                {
                    dtAvarzd = tempEmergencyDecisionDate;
                }
            }

            //Признак Самовольное возведение
            bool? samovol = ParseBtiBoolean(row["SAMOVOL"]);

            //Площадь общая жилых помещений
            decimal? OplG = row["OPL_G"] != DBNull.Value ? (decimal?)row["OPL_G"].ParseToDecimal() : null;

            //Площадь нежилых подвалов
            decimal? pdvplN = row["PDVPL_N"] != DBNull.Value ? (decimal?)row["PDVPL_N"].ParseToDecimal() : null;

            //Площадь застройки
            decimal? narpl = row["NARPL"] != DBNull.Value ? (decimal?)row["NARPL"].ParseToDecimal() : null;

            //Серия проекта
            ReferenceCacheItem projectSeriesNameReference = GetReferenceCacheItem(row, "SER");
            long? serCode;
            string ser;
            ExtractReferenceCacheItemCodeValue(projectSeriesNameReference, out serCode, out ser);

            //Год капитального ремонта
            long? gdKaprem = row["GDKAPREM"] != DBNull.Value ? (long?)row["GDKAPREM"].ParseToLong() : null;

            //Площадь общая с летними жилых помещений
            decimal? oplN = row["OPL_N"] != DBNull.Value ? (decimal?)row["OPL_N"].ParseToDecimal() : null;

            //Капитальность
            long? kap = row["KAP"] != DBNull.Value ? (long?)row["KAP"].ParseToLong() : null;

            //Этажность минимальная
            long? etMin = row["ET_MIN"] != DBNull.Value ? (long?)row["ET_MIN"].ParseToLong() : null;

            //Комментарий
            string komm = row["KOMM"] != DBNull.Value ? row["KOMM"].ToString() : null;

            //Тип строения
            ReferenceCacheItem typeTextNameReference = GetReferenceCacheItem(row, "KAT");
            long? katCode;
            string kat;
            ExtractReferenceCacheItemCodeValue(typeTextNameReference, out katCode, out kat);

            //Площадь кровли
            decimal? krovpl = row["KROVPL"] != DBNull.Value ? (decimal?)row["KROVPL"].ParseToDecimal() : null;

            //Количество пассажирских лифтов
            long? lfpq = row["LFPQ"] != DBNull.Value ? (long?)row["LFPQ"].ParseToLong() : null;

            //Количество грузопассажирских лифтов
            long? lfgpq = row["LFGPQ"] != DBNull.Value ? (long?)row["LFGPQ"].ParseToLong() : null;

            //Количество грузовых лифтов
            long? lfgq = row["LFGQ"] != DBNull.Value ? (long?)row["LFGQ"].ParseToLong() : null;

            //Количество жилых помещений
            long? pmqG = row["PMQ_G"] != DBNull.Value ? (long?)row["PMQ_G"].ParseToLong() : null;

            //Количество комнат в жилых помещениях
            long? kmqG = row["KMQ_G"] != DBNull.Value ? (long?)row["KMQ_G"].ParseToLong() : null;

            //Количество квартир
            long? kwq = row["KWQ"] != DBNull.Value ? (long?)row["KWQ"].ParseToLong() : null;

            //Отметка. 1 - считать как корпус, 0 - нет
            long? prkor = row["PRKOR"] != DBNull.Value ? (long?)row["PRKOR"].ParseToLong() : null;

            //Площадь холодных помещений
            decimal? hpl = row["HPL"] != DBNull.Value ? (decimal?)row["HPL"].ParseToDecimal() : null;

            //Количество электрических плит
            long? eleq = row["ELEQ"] != DBNull.Value ? (long?)row["ELEQ"].ParseToLong() : null;

            //Количество газовых плит
            long? gazq = row["GAZQ"] != DBNull.Value ? (long?)row["GAZQ"].ParseToLong() : null;

            //Площадь балконов
            decimal? bpl = row["BPL"] != DBNull.Value ? (decimal?)row["BPL"].ParseToDecimal() : null;

            //Площадь лоджий
            decimal? lpl = row["LPL"] != DBNull.Value ? (decimal?)row["LPL"].ParseToDecimal() : null;

            //Тип здания
            ReferenceCacheItem objTypeReference = GetReferenceCacheItemById(row, "OBJ_TYPE");
            long? objTypeCode;
            string objTypeValue;
            ExtractReferenceCacheItemCodeValue(objTypeReference, out objTypeCode, out objTypeValue);

            //Материал перекрытий
            ReferenceCacheItem perekrReference = GetReferenceCacheItem(row, "PEREKR");
            long? perekrCode;
            string perekrValue;
            ExtractReferenceCacheItemCodeValue(perekrReference, out perekrCode, out perekrValue);

            //Материал кровли
            ReferenceCacheItem krovReference = GetReferenceCacheItem(row, "KROV");
            long? krovCode;
            string krovValue;
            ExtractReferenceCacheItemCodeValue(krovReference, out krovCode, out krovValue);

            //Состояние отселения корпуса
            ReferenceCacheItem otskorpReference = GetReferenceCacheItem(row, "OTSKORP");
            long? otskorpCode;
            string otskorpValue;
            ExtractReferenceCacheItemCodeValue(otskorpReference, out otskorpCode, out otskorpValue);

            OMBtiBuilding building = new OMBtiBuilding
            {
                EmpId = buildingId,
                Unom = unom,
                KadN = kadN,
                Kl_Code = classNameCode,
                Kl = className,
                ActProc = actProc,
                GdProc = gdProc,
                Naz_Code = nazCode.HasValue ? (Purpose)nazCode.Value : Purpose.None,
                Naz = naz,
                Mst_Code = mstCode,
                Mst = mst,
                EtPdz = etPdz,
                Opl = opl,
                GdPostr = gdPostr,
                Et = et,
                Sost_Code = sostCode.HasValue ? (StructureStatus)sostCode : StructureStatus.None,
                Sost = sost,
                DtSost = dtSost,
                GdDo1917 = gdDo1917,
                Pamarc = pamarc,
                Avarzd = avarzd,
                DtAvarzd = dtAvarzd,
                Samovol = samovol,
                OplG = OplG,
                PdvplN = pdvplN,
                Narpl = narpl,
                Ser_Code = serCode,
                Ser = ser,
                GdKaprem = gdKaprem,
                OplN = oplN,
                Kap = kap,
                EtMin = etMin,
                Komm = komm,
                Kat_Code = katCode,
                Kat = kat,
                ObjType_Code = objTypeCode,
                ObjType = objTypeValue,
                Krovpl = krovpl,
                Lfpq = lfpq,
                Lfgpq = lfgpq,
                Lfgq = lfgq,
                PmqG = pmqG,
                KmqG = kmqG,
                Kwq = kwq,
                Prkor = prkor,
                Hpl = hpl,
                Eleq = eleq,
                Gazq = gazq,
                Bpl = bpl,
                Lpl = lpl,
                Krov_Code = krovCode,
                Krov = krovValue,
                Perekr_Code = perekrCode,
                Perekr = perekrValue,
                Otskorp_Code = otskorpCode,
                Otskorp = otskorpValue,
                //константы
                DownloadDate = dateFrom,
                Source_Code = InsuranceSourceType.Bti
            };

            return building;
        }
        #endregion

        /// <summary>
        /// Загрузка связанных объектов в следующем порядке: этажи, помещения, комнаты
        /// </summary>
        private void CreateBuildingSibling(DataRow btiBuildingData, OMBtiBuilding building, DateTime dateFrom)
        {
            Logger logger = new Logger("Bti.DailyImport", "CreateBuildingSibling", SRDSession.GetCurrentUserId());

            if (btiBuildingData["OBJ_ID"] == DBNull.Value)
            {
                throw new Exception("Не заполнено поле OBJ_ID");
            }

            //идентификатор из БТИ (нужен для выгрузки этажей из ETS)
            long btiBuildingId = btiBuildingData["OBJ_ID"].ParseToLong();
            if (btiBuildingId == 0)
            {
                throw new Exception("Не корректный идентификатор здания БТИ: " + btiBuildingData["OBJ_ID"]);
            }

            Dictionary<string, long> floorCache = new Dictionary<string, long>();

            //загружаем этажи из ETS, если есть OBJ_ID
            LoadFloors(btiBuildingId, building.EmpId, dateFrom, floorCache);

            DataTable objectPremisesTable = GetBtiPremises(building.Unom.Value);

            Dictionary<long, Tuple<long, long?>> premiseIds = new Dictionary<long, Tuple<long, long?>>();

            // Поиск существующего помещения (квартиры)
            List<OMPremase> existingPremises =
                OMPremase.Where(
                    x => x.ParentFloor.BuildingId == building.EmpId && x.IdInSource != null)
                    .Select(x => x.IdInSource)
                    .Select(x => x.UpdateDate)
                    .Select(x => x.FloorId)
                    .Select(x => x.Unkv)
                    .Execute();

            LoadPremises(building.EmpId, dateFrom, objectPremisesTable, premiseIds, floorCache, existingPremises);

            DataTable objectRoomsTable = GetBtiRooms(building.Unom.Value);

            // Поиск существующей комнаты
            List<OMRooms> existingRooms =
                OMRooms.Where(
                    x => x.ParentPremase.ParentFloor.BuildingId == building.EmpId && x.IdInSource != null)
                    .Select(x => x.IdInSource)
                    .Select(x => x.ParentPremase.IdInSource)
                    .Select(x => x.UpdateDate)
                    .Execute();

            LoadRooms(premiseIds, dateFrom, objectRoomsTable, existingRooms);

            logger.TraceFinal("Загружены связанные со здание объекты(комнаты, помещения, этажи)",
                string.Format("Идентификатор объекта (R_ALT_BUILDING_Q.EMP_ID): {0}; помещений: {1}; комнат: {2}",
                    building.EmpId, objectPremisesTable.Rows.Count, objectRoomsTable.Rows.Count));
        }

        /// <summary>
        /// Загрузка этажей из таблицы ETS (грузим все, не проверяем - есть этаж или нет, т.к. предварительно удалили этажи для этого здания)
        /// </summary>
        ///////////////////////////////////////////////////////////////////
        //Не найдено в ETS:
        //1. 25300600 Площадь_пп (BTI_NAME oplo)
        private void LoadFloors(long rsmBuildingId, long btiBuildingId, DateTime dateFrom, Dictionary<string, long> floorCache)
        {
            DataTable objectFloorsTable;
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                DbCommand selectCommand = CipjsDbManager.Dgi.GetSqlStringCommand(String.Format(@"SELECT ets.OBJ_ID, ets.ET, ets.TET FROM {1} ets WHERE ets.OBJ_ID = {0}", btiBuildingId, BtiEts));
                objectFloorsTable = CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0];
            }

            foreach (DataRow row in objectFloorsTable.Rows)
            {
                long? et = row["ET"] != DBNull.Value ? (long?)row["ET"].ParseToLong() : null;

                //25300300 Тип этажа
                ReferenceCacheItem typeNameReference = GetReferenceCacheItem(row, "TET");
                long? typeNameCode;
                string typeNameValue;
                ExtractReferenceCacheItemCodeValue(typeNameReference, out typeNameCode, out typeNameValue);

                OMFloor oMAltFloor = CreateFloor(rsmBuildingId, et, typeNameCode, typeNameValue, dateFrom);

                string floorKey = string.Format("{0}_{1}", oMAltFloor.FloorNumber, oMAltFloor.TypeName_Code);

                if (!floorCache.ContainsKey(floorKey))
                {
                    floorCache.Add(floorKey, oMAltFloor.EmpId);
                }
            }
        }

        private OMFloor CreateFloor(long? buildingId, long? et, long? typeNameCode, string typeNameValue, DateTime dateFrom)
        {
            OMFloor oMAltFloor =
                OMFloor.Where(
                    x => x.BuildingId == buildingId && x.FloorNumber == et && x.TypeName_Code == typeNameCode)
                    .Select(x => x.FloorNumber)
                    .Select(x => x.TypeName_Code)
                    .Execute()
                    .FirstOrDefault();

            if (oMAltFloor != null)
            {
                return oMAltFloor;
            }

            oMAltFloor = new OMFloor();

            oMAltFloor.BuildingId = buildingId;

            oMAltFloor.TypeName_Code = typeNameCode;
            oMAltFloor.TypeName = typeNameValue;

            oMAltFloor.IsUndeground = GetUNDEGROUND(typeNameCode);

            //25300400 Номер этажа
            oMAltFloor.FloorNumber = et;

            //25300500 Номер этажа п/п
            oMAltFloor.FloorNumberPp = et;

            //25300800 Номер_этажа_пп
            oMAltFloor.NumberPp = et;

            oMAltFloor.Save(tdInstanceID: _tdInstanceID, fromDate: dateFrom);

            return oMAltFloor;
        }

        private DataTable GetBtiPremises(long unom)
        {
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                DbCommand selectCommand =
                CipjsDbManager.Dgi.GetSqlStringCommand(
                    String.Format(@"SELECT fkva.OBJ_ID, fkva.KAD_N, fkva.ET, fkva.TET, fkva.KV, fkva.RIM, fkva.UNKV, fkva.DTINV,
fkva.OPL, fkva.GPL, fkva.PPL, fkva.KVNOM, fkva.KVI, fkva.KL KLA, fkva.TP, fkva.KMQ, fkva.DTKORR, fkva.UNOM, fkva.NSEK, fkva.OBJ_TYPE, fkva.ZPL
FROM {1} fkva WHERE fkva.UNOM = {0} and fkva.bit0 = 0", unom, BtiFkva));
                DataTable objectPremisesTable = CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0];
                return objectPremisesTable;
            }
        }

        private OMPremase CreatePremase(DataRow flatRow, Dictionary<long, OMPremase> existingPremisesDict, DateTime dateFrom, long rsmBuildingId)
        {
            long idInSource = flatRow["OBJ_ID"].ParseToLong();

            DateTime? updateDate = flatRow["DTKORR"] != DBNull.Value
                ? (DateTime?)flatRow["DTKORR"].ParseToDateTime()
                : null;

            // Поиск существующего помещения (квартиры)
            OMPremase oMAltPremise;
            if (existingPremisesDict.TryGetValue(idInSource, out oMAltPremise))
            {
                if (updateDate != null && oMAltPremise.UpdateDate != null &&
                    updateDate <= oMAltPremise.UpdateDate)
                {
                    return oMAltPremise;
                }
            }
            else
            {
                oMAltPremise = new OMPremase { IdInSource = idInSource };
            }

            oMAltPremise.UpdateDate = updateDate;

            oMAltPremise.Kadastr = flatRow["KAD_N"].ToString();

            //25404100	Тип последнего решения по помещению
            ReferenceCacheItem tresReference = GetReferenceCacheItem(flatRow, "TRES");
            long? tresCode;
            string tresValue;
            ExtractReferenceCacheItemCodeValue(tresReference, out tresCode, out tresValue);
            oMAltPremise.Tres_Code = (SolutionTypes)tresCode;
            oMAltPremise.Tres = tresValue;

            //25404200	Содержание последнего решения
            ReferenceCacheItem sresReference = GetReferenceCacheItem(flatRow, "SRES");
            long? sresCode;
            string sresValue;
            ExtractReferenceCacheItemCodeValue(sresReference, out sresCode, out sresValue);
            oMAltPremise.Sres_Code = (SolutionContent)sresCode;
            oMAltPremise.Sres = sresValue;

            //25404300	Дата вынесения последнего решения
            oMAltPremise.Dres = flatRow["DRES"]?.ParseToDateTimeNullable();

            //25404400	№ последнего решения
            oMAltPremise.Nres = flatRow["NRES"]?.ParseToString();

            //25404500	Последняя дата ввода информации об обременении помещения
            oMAltPremise.ArDtvv = flatRow["AR_DTVV"]?.ParseToDateTimeNullable();

            //25404600	Дата регистрации последнего обременения
            oMAltPremise.ArDt = flatRow["AR_DT"]?.ParseToDateTimeNullable();

            //25404700	Тип обременения
            ReferenceCacheItem arOsnReference = GetReferenceCacheItem(flatRow, "AR_OSN");
            long? arOsnCode;
            string arOsnValue;
            ExtractReferenceCacheItemCodeValue(arOsnReference, out arOsnCode, out arOsnValue);
            oMAltPremise.ArOsn_Code = (BurdenStatus)arOsnCode;
            oMAltPremise.ArOsn = arOsnValue;

            //25404800	Дата снятия последнего обременения
            oMAltPremise.ArDtsn = flatRow["AR_DTSN"]?.ParseToDateTimeNullable();

            //25404900	Тип снятия обременения
            var arOsnNk = _referenceCache.SqlDictionaryMapping.GetValue("AR_OSN")?.ParseToLong();
            ReferenceCacheItem arOsnsnReference = _referenceCache.GetReferenceItem(arOsnNk ?? 0, flatRow["AR_OSNSN"]?.ParseToString());
            long ? arOsnsnCode;
            string arOsnsnValue;
            ExtractReferenceCacheItemCodeValue(arOsnsnReference, out arOsnsnCode, out arOsnsnValue);
            oMAltPremise.ArOsnsn_Code = (BurdenStatus)arOsnsnCode;
            oMAltPremise.ArOsnsn = arOsnsnValue;

            //25400300 Этаж (FLOOR_ID)
            long? premiseFloorNumber = flatRow["ET"] != DBNull.Value ? (long?)flatRow["ET"].ParseToLong() : null;

            //25300300 Тип этажа
            ReferenceCacheItem floorTypeNameReference = GetReferenceCacheItem(flatRow, "TET");
            long? floorTypeNameCode;
            string floorTypeNameValue;
            ExtractReferenceCacheItemCodeValue(floorTypeNameReference, out floorTypeNameCode, out floorTypeNameValue);
            oMAltPremise.Tet_Code = floorTypeNameCode;
            oMAltPremise.Tet = floorTypeNameValue;

            if (premiseFloorNumber.HasValue && floorTypeNameCode.HasValue)
            {
                OMFloor oMAltFloor = OMFloor.Where(x => x.BuildingId == rsmBuildingId && x.FloorNumber == premiseFloorNumber).ExecuteFirstOrDefault();

                //если не нашли этаж помещения среди добавленных, создаем
                if (oMAltFloor != null)
                {
                    oMAltPremise.FloorId = oMAltFloor.EmpId;
                }
                else
                {
                    oMAltFloor = CreateFloor(rsmBuildingId, premiseFloorNumber, floorTypeNameCode, floorTypeNameValue, dateFrom);

                    oMAltPremise.FloorId = oMAltFloor.EmpId;
                }
            }

            //25400700 Уникальный номер в здании
            oMAltPremise.Unkv = flatRow["UNKV"] != DBNull.Value ? (long?)flatRow["UNKV"].ParseToLong() : null;

            //25403800 UNOM
            oMAltPremise.Unom = flatRow["UNOM"] != DBNull.Value ? (long?)flatRow["UNOM"].ParseToLong() : null;

            //25400400 Дата обследования
            oMAltPremise.InspectionDate = null;
            if (flatRow["DTINV"] != DBNull.Value)
            {
                DateTime tempInspectionDate;
                if (flatRow["DTINV"].TryParseToDateTime(out tempInspectionDate))
                {
                    oMAltPremise.InspectionDate = tempInspectionDate;
                }
            }

            decimal? opl = flatRow["OPL"] != DBNull.Value ? (decimal?)flatRow["OPL"].ParseToDecimal() : null;
            oMAltPremise.TotalArea = opl;

            oMAltPremise.LivingArea = flatRow["GPL"] != DBNull.Value ? (decimal?)flatRow["GPL"].ParseToDecimal() : null;
            oMAltPremise.TotalAreaWithSummer = flatRow["PPL"] != DBNull.Value ? (decimal?)flatRow["PPL"].ParseToDecimal() : null;

            //25401700 Номер_помещения_пп
            oMAltPremise.Kvnom = flatRow["KVNOM"] != null && flatRow["KVNOM"] != DBNull.Value ?
                flatRow["KVNOM"].ToString() : null;

            //25400800 Класс помещения
            ReferenceCacheItem classNameReference = GetReferenceCacheItem(flatRow, "KLA");
            long? classNameCode;
            string classNameValue;
            ExtractReferenceCacheItemCodeValue(classNameReference, out classNameCode, out classNameValue);
            oMAltPremise.ClassName_Code = classNameCode;
            oMAltPremise.ClassName = classNameValue;

            //25400900 Тип помещения
            ReferenceCacheItem typeNameReference = GetReferenceCacheItem(flatRow, "TP");
            long? typeNameCode;
            string typeNameValue;
            ExtractReferenceCacheItemCodeValue(typeNameReference, out typeNameCode, out typeNameValue);
            oMAltPremise.TypeName_Code = typeNameCode.HasValue ? (PremisesTypes)typeNameCode.Value : PremisesTypes.None;
            oMAltPremise.TypeName = typeNameValue;

            //25403400 Количество жилых комнат
            oMAltPremise.RoomsCount = flatRow["KMQ"] != DBNull.Value ? (long?)flatRow["KMQ"].ParseToLong() : null;

            //25403700 Тип здания
            ReferenceCacheItem objTypeReference = GetReferenceCacheItemById(flatRow, "OBJ_TYPE");
            long? objTypeCode;
            string objTypeValue;
            ExtractReferenceCacheItemCodeValue(objTypeReference, out objTypeCode, out objTypeValue);
            oMAltPremise.ObjType_Code = objTypeCode;
            oMAltPremise.ObjType = objTypeValue;

            //25401800 Номер секции
            oMAltPremise.SectionNumber = flatRow["NSEK"] != DBNull.Value ? (long?)flatRow["NSEK"].ParseToLong() : null;

            //25403900 Не входящие в общую площадь
            oMAltPremise.Zpl = flatRow["ZPL"] != DBNull.Value ? (decimal?)flatRow["ZPL"].ParseToDecimal() : null;

            oMAltPremise.Bit0 = flatRow["BIT0"].ParseToBoolean();

            oMAltPremise.Save(tdInstanceID: _tdInstanceID, fromDate: dateFrom);

            return oMAltPremise;
        }

        /// <summary>
        /// Загрузка помещений из таблицы FKVA по UNOM (грузим все, не проверяем - есть помещение или нет, т.к. предварительно удалили помещения для этого здания)
        /// </summary>
        private void LoadPremises(long rsmBuildingId, DateTime dateFrom, DataTable objectPremisesTable, Dictionary<long, Tuple<long, long?>> premiseIds, Dictionary<string, long> floorCache, List<OMPremase> existingPremises = null)
        {
            foreach (DataRow row in objectPremisesTable.Rows)
            {
                long idInSource = row["OBJ_ID"].ParseToLong();
                DateTime? updateDate = row["DTKORR"] != DBNull.Value
                    ? (DateTime?)row["DTKORR"].ParseToDateTime()
                    : null;

                // Поиск существующего помещения (квартиры)
                OMPremase oMAltPremise = null;
                if (existingPremises != null && existingPremises.Count > 0)
                {
                    oMAltPremise = existingPremises.FirstOrDefault(x => x.IdInSource == idInSource);

                    if (updateDate != null && oMAltPremise != null && oMAltPremise.UpdateDate != null &&
                        updateDate <= oMAltPremise.UpdateDate)
                    {
                        premiseIds.Add(oMAltPremise.Unkv.Value, Tuple.Create(oMAltPremise.EmpId, oMAltPremise.FloorId));
                        continue;
                    }
                }

                oMAltPremise = oMAltPremise ?? new OMPremase { IdInSource = idInSource };

                oMAltPremise.UpdateDate = updateDate;

                oMAltPremise.Kadastr = row["KAD_N"].ToString();

                //25400300 Этаж (FLOOR_ID)
                long? premiseFloorNumber = row["ET"] != DBNull.Value ? (long?)row["ET"].ParseToLong() : null;

                //25300300 Тип этажа
                ReferenceCacheItem floorTypeNameReference = GetReferenceCacheItem(row, "TET");
                long? floorTypeNameCode;
                string floorTypeNameValue;
                ExtractReferenceCacheItemCodeValue(floorTypeNameReference, out floorTypeNameCode, out floorTypeNameValue);
                oMAltPremise.Tet_Code = floorTypeNameCode;
                oMAltPremise.Tet = floorTypeNameValue;

                if (premiseFloorNumber.HasValue && floorTypeNameCode.HasValue)
                {
                    long? floorId;
                    string floorKey = string.Format("{0}_{1}", premiseFloorNumber, floorTypeNameCode.Value);

                    //если не нашли этаж помещения среди добавленных, создаем
                    if (floorCache.ContainsKey(floorKey))
                    {
                        floorId = floorCache[floorKey];
                        oMAltPremise.FloorId = floorId;
                    }
                    else
                    {
                        OMFloor oMAltFloor = CreateFloor(rsmBuildingId, premiseFloorNumber, floorTypeNameCode, floorTypeNameValue, dateFrom);

                        floorId = oMAltFloor.EmpId;
                    }

                    oMAltPremise.FloorId = floorId;
                }

                //25400700 Уникальный номер в здании
                oMAltPremise.Unkv = row["UNKV"] != DBNull.Value ? (long?)row["UNKV"].ParseToLong() : null;

                //25403800 UNOM
                oMAltPremise.Unom = row["UNOM"] != DBNull.Value ? (long?)row["UNOM"].ParseToLong() : null;

                //25400400 Дата обследования
                oMAltPremise.InspectionDate = null;
                if (row["DTINV"] != DBNull.Value)
                {
                    DateTime tempInspectionDate;
                    if (row["DTINV"].TryParseToDateTime(out tempInspectionDate))
                    {
                        oMAltPremise.InspectionDate = tempInspectionDate;
                    }
                }

                decimal? opl = row["OPL"] != DBNull.Value ? (decimal?)row["OPL"].ParseToDecimal() : null;
                oMAltPremise.TotalArea = opl;

                oMAltPremise.LivingArea = row["GPL"] != DBNull.Value ? (decimal?)row["GPL"].ParseToDecimal() : null;
                oMAltPremise.TotalAreaWithSummer = row["PPL"] != DBNull.Value ? (decimal?)row["PPL"].ParseToDecimal() : null;

                //25401700 Номер_помещения_пп
                oMAltPremise.Kvnom = row["KVNOM"] != null && row["KVNOM"] != DBNull.Value ?
                    row["KVNOM"].ToString() : null;

                //25400800 Класс помещения
                ReferenceCacheItem classNameReference = GetReferenceCacheItem(row, "KLA");
                long? classNameCode;
                string classNameValue;
                ExtractReferenceCacheItemCodeValue(classNameReference, out classNameCode, out classNameValue);
                oMAltPremise.ClassName_Code = classNameCode;
                oMAltPremise.ClassName = classNameValue;

                //25400900 Тип помещения
                ReferenceCacheItem typeNameReference = GetReferenceCacheItem(row, "TP");
                long? typeNameCode;
                string typeNameValue;
                ExtractReferenceCacheItemCodeValue(typeNameReference, out typeNameCode, out typeNameValue);
                oMAltPremise.TypeName_Code = typeNameCode.HasValue ? (PremisesTypes)typeNameCode.Value : PremisesTypes.None;
                oMAltPremise.TypeName = typeNameValue;

                //25403400 Количество жилых комнат
                oMAltPremise.RoomsCount = row["KMQ"] != DBNull.Value ? (long?)row["KMQ"].ParseToLong() : null;

                //25403700 Тип здания
                ReferenceCacheItem objTypeReference = GetReferenceCacheItemById(row, "OBJ_TYPE");
                long? objTypeCode;
                string objTypeValue;
                ExtractReferenceCacheItemCodeValue(objTypeReference, out objTypeCode, out objTypeValue);
                oMAltPremise.ObjType_Code = objTypeCode;
                oMAltPremise.ObjType = objTypeValue;

                //25401800 Номер секции
                oMAltPremise.SectionNumber = row["NSEK"] != DBNull.Value ? (long?)row["NSEK"].ParseToLong() : null;

                //25403900 Не входящие в общую площадь
                oMAltPremise.Zpl = row["ZPL"] != DBNull.Value ? (decimal?)row["ZPL"].ParseToDecimal() : null;

                oMAltPremise.Bit0 = row["BIT0"].ParseToBoolean();

                oMAltPremise.Save(tdInstanceID: _tdInstanceID, fromDate: dateFrom);

                premiseIds.Add(oMAltPremise.Unkv.Value, Tuple.Create(oMAltPremise.EmpId, oMAltPremise.FloorId));
            }
        }

        private DataTable GetBtiRooms(long unom)
        {
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                DbCommand selectCommand =
                CipjsDbManager.Dgi.GetSqlStringCommand(
                    String.Format(@"SELECT fkmn.OBJ_ID, fkmn.KAD_N, fkmn.UNOM, fkmn.UNKV, fkmn.PLOV, fkmn.NZ, fkmn.VYS, fkmn.PL,
fkmn.NPP, fkmn.KMI, fkmn.DATEEDIT
FROM {1} fkmn WHERE fkmn.UNOM = {0} and fkmn.bit0 = 0", unom, BtiFkmn));
                DataTable objectRoomsTable = CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0];
                return objectRoomsTable;
            }
        }

        /// <summary>
        /// Загрузка помещений из таблицы FKMN по UNOM + UNKV помещения (грузим все, не проверяем - есть комната или нет, т.к. предварительно удалили комната для этого здания)
        /// </summary>
        /// <param name="premiseIds">Таблица соответствия ИД помещения/этажа БТИ - РСМ (заполняется при загрузке помещений)</param>
        /// <param name="dateFrom">Дата "С"</param>
        /// <param name="btiRooms">Данные из БТИ</param>
        /// <param name="existingRooms">Существующие комнаты в РСМ</param>
        //////////////////////////////////////////////////////////////
        //Не найдено в таблице FKMN:
        //1. 25700600 Тип площади комнаты ТP
        private void LoadRooms(Dictionary<long, Tuple<long, long?>> premiseIds, DateTime dateFrom, DataTable btiRooms, List<OMRooms> existingRooms = null)
        {
            foreach (DataRow row in btiRooms.Rows)
            {
                long? unkv = row["UNKV"] != DBNull.Value ? (long?)row["UNKV"].ParseToLong() : null;

                if (!unkv.HasValue) continue;

                long idInSource = row["OBJ_ID"].ParseToLong();
                DateTime? updateDate = row["DATEEDIT"] != DBNull.Value
                    ? (DateTime?)row["DATEEDIT"].ParseToDateTime()
                    : null;

                // Поиск существующей комнаты
                OMRooms oMAltRoom = null;
                if (existingRooms != null && existingRooms.Count > 0)
                {
                    oMAltRoom = existingRooms.FirstOrDefault(x => x.IdInSource == idInSource);

                    if (updateDate != null && oMAltRoom != null && oMAltRoom.UpdateDate != null &&
                        updateDate <= oMAltRoom.UpdateDate)
                    {
                        continue;
                    }
                }

                oMAltRoom = oMAltRoom ?? new OMRooms { IdInSource = idInSource };

                oMAltRoom.UpdateDate = updateDate;

                oMAltRoom.PremiseId = premiseIds[unkv.Value].Item1;
                oMAltRoom.FloorId = premiseIds[unkv.Value].Item2;

                oMAltRoom.KadastrNumber = row["KAD_N"].ToString();

                //25700500 Вид площади комнаты
                ReferenceCacheItem areaKindReferenceItem = GetAREA_KIND_Attribute(row);
                long? areaKindCode;
                string areaKindValue;
                ExtractReferenceCacheItemCodeValue(areaKindReferenceItem, out areaKindCode, out areaKindValue);
                oMAltRoom.AreaKindName_Code = areaKindCode;
                oMAltRoom.AreaKindName = areaKindValue;

                //25700300 Назначение комнаты
                ReferenceCacheItem purposeNameReferenceItem = GetReferenceCacheItem(row, "NZ");
                long? purposeNameCode;
                string purposeNameValue;
                ExtractReferenceCacheItemCodeValue(purposeNameReferenceItem, out purposeNameCode, out purposeNameValue);
                oMAltRoom.PurposeName_Code = purposeNameCode;
                oMAltRoom.PurposeName = purposeNameValue;

                //25700500 Вид площади комнаты
                ReferenceCacheItem areaKindNameReferenceItem = GetReferenceCacheItem(row, "PLOV");
                long? areaKindNameCode;
                string areaKindNameValue;
                ExtractReferenceCacheItemCodeValue(areaKindNameReferenceItem, out areaKindNameCode, out areaKindNameValue);
                oMAltRoom.AreaKindName_Code = areaKindNameCode;
                oMAltRoom.AreaKindName = areaKindNameValue;

                //25700700 Высота
                oMAltRoom.Height = row["VYS"] != DBNull.Value ? (long?)row["VYS"].ParseToLong() : null;

                decimal? pl = row["PL"] != DBNull.Value ? (decimal?)row["PL"].ParseToDecimal() : null;
                //25701000 Площадь комнаты
                oMAltRoom.Area = pl;
                //25701700 Площадь_пп
                oMAltRoom.AreaPp = pl;

                long? npp = row["NPP"] != DBNull.Value ? (long?)row["NPP"].ParseToLong() : null;
                //25701100 Номер п/п
                oMAltRoom.NumberPp = npp;
                //25701800 Номер_комнаты_пп
                oMAltRoom.NumberRoomPp = npp.ToString();

                //25701400 Номер для документов 
                oMAltRoom.DocumentNumber = row["KMI"] != null && row["KMI"] != DBNull.Value ?
                    row["KMI"].ToString() : null;

                oMAltRoom.Save(tdInstanceID: _tdInstanceID, fromDate: dateFrom);
            }
        }

        private void LoadAddresses(OMBtiBuilding oMAltBuilding, DateTime dateFrom)
        {
            //загружаем адрес, только если есть unom
            if (oMAltBuilding == null || !oMAltBuilding.Unom.HasValue)
            {
                return;
            }

            Logger logger = new Logger("Bti.DailyImport", "LoadAddresses", SRDSession.GetCurrentUserId());

            DataTable fads;
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                DbCommand selectCommand = CipjsDbManager.Dgi.GetSqlStringCommand(String.Format(@"select ibf.OBJ_ID, ibf.OBJ_TYPE, ibf.ADRES, ibf.UNAD, ibf.P1, ibf.P3, ibf.P4, ibf.P5, ibf.P6, ibf.P7, ibf.P90, 
ibf.P91, ibf.L1_TYPE, ibf.L1_VALUE, ibf.L2_TYPE, ibf.L2_VALUE, ibf.L3_TYPE, ibf.L3_VALUE, ibf.L4_TYPE, 
ibf.L4_VALUE, ibf.ADM_AREA, ibf.DISTRICT, ibf.NREG, ibf.DREG, ibf.KAD_N, ibf.KAD_ZU, ibf.TDOC, 
ibf.NDOC, ibf.DDOC, ibf.UNOM, ibf.ADR_TYPE, ibf.SOSTAD, ibf.KLADR, ibf.N_FIAS, ibf.D_FIAS, ibf.VID, 
ibf.MAIN_ADR, ibf.COMMNT, ibf.STATUS, ibf.BUILDING_ADDRESS, ibf.AO, ibf.MR, ibf.DATEEDIT, ibf.EAID
from {0} ibf
where ibf.UNOM = {1}", BtiFads, oMAltBuilding.Unom.Value));
                fads = CipjsDbManager.Dgi.ExecuteDataSet(selectCommand).Tables[0];
            }

            foreach (DataRow row in fads.Rows)
            {
                OMADDRESS omAddress = CreateAddress(row);
                omAddress.Save(tdInstanceID: _tdInstanceID, fromDate: dateFrom);

                //создаем связку адрес - объект
                OMADDRLINK oMAddrlink = CreateAddrlink(row, omAddress, oMAltBuilding);
                oMAddrlink.Save(tdInstanceID: _tdInstanceID, fromDate: dateFrom);
            }

            logger.TraceFinal("Загружены адреса объекта", string.Format("Идентификатор объекта (R_ALT_BUILDING_Q.EMP_ID): {0}", oMAltBuilding.EmpId));
        }




        #region Адреса (50 - OMAddress)
        //маппим вручную по ..\Inecas.RSM\Main\Source\LoaderLibrary\XML\Адреса.xml
        ////////////////////////////////////////////////////////////////////////////////////////////
        //Не найдены в таблице FADS:
        //1. LIT Литера
        private OMADDRESS CreateAddress(DataRow row)
        {
            string idinds = GetID_IN_DS(row);

            OMADDRESS oMAddress = OMADDRESS.Where(x => x.IdInDs == idinds).Execute().FirstOrDefault();

            if (oMAddress == null)
                oMAddress = OMADDRESS.CreateEmpty();
            else
                this._existingAddresses.Add(oMAddress.EmpId);

            string codeGivc = row["P7"] != null && row["P7"] != DBNull.Value
                ? Convert.ToString(row["P7"])
                : null;


            string nFias = row["N_FIAS"] != null && row["N_FIAS"] != DBNull.Value ? row["N_FIAS"].ToString().ToUpper() : null;


            //5001000 Субъект РФ - константа - Москва
            oMAddress.SubjectRfName_Code = 2L;
            oMAddress.SubjectRfName = "город Москва";

            //5000800 идентификатор в источнике данных
            oMAddress.IdInDs = idinds;

            //5000900 источник данных
            oMAddress.DataSourceName_Code = InsuranceSourceType.Bti;

            //5003600 Код ФИАС
            oMAddress.CodeFias = nFias;

            //5001600 идентификатор улицы
            StreetCacheItem streetCacheItem = _referenceCache.GetStreetCacheItem(codeGivc);
            oMAddress.StreetName_Code = streetCacheItem != null ? streetCacheItem.Id : null;
            oMAddress.StreetName = streetCacheItem != null ? streetCacheItem.Name : null;

            //50001200 идентификатор района
            string mrCode = row["MR"] != DBNull.Value ? row["MR"].ToString() : null;
            DistrictCacheItem districtCacheItem = _referenceCache.GetDistrictCacheItem(mrCode);
            oMAddress.DistrictId = districtCacheItem != null ? districtCacheItem.Id : null;

            //50001100 идентификатор округа
            string aoCode = row["AO"] != DBNull.Value ? row["AO"].ToString() : null;
            OkrugCacheItem okrugCacheItem = _referenceCache.GetOkrugCacheItem(aoCode);
            oMAddress.OkrugId = okrugCacheItem != null ? okrugCacheItem.Id : null;

            //5001800 Номер участка
            oMAddress.PlotNumber = row["KAD_ZU"] != DBNull.Value ? row["KAD_ZU"].ToString() : null;

            //5001900 Дом
            oMAddress.HouseNumber = row["L1_VALUE"] != DBNull.Value ? row["L1_VALUE"].ToString() : null;
            if (oMAddress.HouseNumber.IsNotEmpty())
            {
                //5001700 Тип владения
                ReferenceCacheItem propertyTypeNameReference = GetReferenceCacheItemById(row, "L1_TYPE");
                long? propertyTypeNameCode;
                string propertyTypeNameValue;
                ExtractReferenceCacheItemCodeValue(propertyTypeNameReference, out propertyTypeNameCode, out propertyTypeNameValue);
                //Если LN_VALUE не заполнен, то обнулять (NULL) соответствующий номер и тип (значение и код)
                oMAddress.PropertyTypeName_Code = propertyTypeNameValue.IsNotEmpty() ? propertyTypeNameCode : null;
                oMAddress.PropertyTypeName = propertyTypeNameValue;
            }

            //5002000 Корпус
            oMAddress.KorpusNumber = row["L2_VALUE"] != DBNull.Value ? row["L2_VALUE"].ToString() : null;
            if (oMAddress.KorpusNumber.IsNotEmpty())
            {
                //5002900 Тип корпуса
                ReferenceCacheItem typeCorpusReference = GetReferenceCacheItemById(row, "L2_TYPE");
                long? typeCorpusCode;
                string typeCorpusValue;
                ExtractReferenceCacheItemCodeValue(typeCorpusReference, out typeCorpusCode, out typeCorpusValue);
                //Если LN_VALUE не заполнен, то обнулять (NULL) соответствующий номер и тип (значение и код)
                oMAddress.TypeCorpus_Code = typeCorpusValue.IsNotEmpty() ? typeCorpusCode : null;
                oMAddress.TypeCorpus = typeCorpusValue;
            }

            //5002200 Строение
            oMAddress.StructureNumber = row["L3_VALUE"] != DBNull.Value ? row["L3_VALUE"].ToString() : null;
            if (oMAddress.StructureNumber.IsNotEmpty())
            {
                //5002100 Тип сооружения (адр)
                ReferenceCacheItem structureTypeNameReference = GetReferenceCacheItemById(row, "L3_TYPE");
                long? structureTypeCode;
                string structureTypeNameValue;
                ExtractReferenceCacheItemCodeValue(structureTypeNameReference, out structureTypeCode, out structureTypeNameValue);
                //Если LN_VALUE не заполнен, то обнулять (NULL) соответствующий номер и тип (значение и код)
                oMAddress.StructureTypeName_Code = structureTypeNameValue.IsNotEmpty() ? structureTypeCode : null;
                oMAddress.StructureTypeName = structureTypeNameValue;
            }

            //5000200, 5000300, 5000400, 5000500, 5000700, 5000600 различные наименования
            string name = MakeAddress(oMAddress, row);
            oMAddress.FullName = name;
            oMAddress.ShortName = name;
            oMAddress.NameForSort = name;
            oMAddress.MainName = name;
            oMAddress.FullNamePrint = name;
            oMAddress.MainNamePrint = name;

            return oMAddress;
        }
        #endregion

        #region Адресный список (52 - OMAddrlink)
        //маппим вручную по ..\Inecas.RSM\Main\Source\LoaderLibrary\XML\АдресныйСписок.xml
        ////////////////////////////////////////////////////////////////////////////////////////////
        private OMADDRLINK CreateAddrlink(DataRow row, OMADDRESS omAddress, OMBtiBuilding oMAltBuilding)
        {
            if (oMAltBuilding == null)
                return null;

            OMADDRLINK oMAddrlink = OMADDRLINK.Where(x => x.AddressId == omAddress.EmpId && x.BuildingId == oMAltBuilding.EmpId)
                .Execute()
                .FirstOrDefault();
            if (oMAddrlink == null)
                oMAddrlink = OMADDRLINK.CreateEmpty();

            //5200600 Адрес
            oMAddrlink.AddressId = omAddress.EmpId;

            //5200700 ID объекта
            oMAddrlink.BuildingId = oMAltBuilding.EmpId;

            //5201400 Номер реестра объекта, для нас всегда 251
            oMAddrlink.RegisterObjectNumber = 251L;

            //5201600 Источник данных
            oMAddrlink.TextSource_Code = InsuranceSourceType.Bti;

            //5200101 Состояние адреса - С.А.
            ReferenceCacheItem stateReference = GetReferenceCacheItemById(row, "SOSTAD");
            long? stateCode;
            string stateValue;
            ExtractReferenceCacheItemCodeValue(stateReference, out stateCode, out stateValue);
            if (stateValue.IsNullOrEmpty() || stateValue == "-")
            {
                oMAddrlink.AddressStateName_Code = null;
                oMAddrlink.AddressStateName = null;
            }
            else
            {
                oMAddrlink.AddressStateName_Code = stateCode;
                oMAddrlink.AddressStateName = stateValue;
            }

            //5200200 Статус адреса STATUS
            ReferenceCacheItem statusReference = GetReferenceCacheItemById(row, "ADR_TYPE");
            long? statusCode;
            string statusValue;
            ExtractReferenceCacheItemCodeValue(statusReference, out statusCode, out statusValue);
            oMAddrlink.AddressStatusName_Code = statusCode.HasValue ? (AddressStatus)statusCode : (AddressStatus)0L;
            oMAddrlink.AddressStatusName = statusValue != null ? statusValue : string.Empty;

            //TODO: В БТИ Основной адрес заменен на Официальный. Чтобы выборки РСМ не поломались пока делаем замену. В дальнейшем нужно это доработать.
            if (oMAddrlink.AddressStatusName_Code == AddressStatus.AlternativeCurrent)
            {
                oMAddrlink.AddressStatusName_Code = AddressStatus.Main;
            }

            if (row["MAIN_ADR"].ParseToLong() == 1)
            {
                oMAddrlink.AddressStatusName_Code = AddressStatus.Main;
            }

            //5200800 Регистрационный номер в адресном реестре
            oMAddrlink.RegNumber = row["NREG"] != DBNull.Value ? (long?)row["NREG"].ParseToLong() : null;

            //5200900 Дата регистрации адреса
            oMAddrlink.RegDate = null;
            if (row["DREG"] != DBNull.Value)
            {
                DateTime tempRegDate;
                if (row["DREG"].TryParseToDateTime(out tempRegDate))
                {
                    oMAddrlink.RegDate = tempRegDate;
                }
            }

            //5201000 Тип документа-основания (рег)
            ReferenceCacheItem regDocTypeNameReference = GetReferenceCacheItemById(row, "TDOC");
            long? regDocTypeNameCode;
            string regDocTypeNameValue;
            ExtractReferenceCacheItemCodeValue(regDocTypeNameReference, out regDocTypeNameCode, out regDocTypeNameValue);
            oMAddrlink.RegDocTypeName_Code = regDocTypeNameCode;
            oMAddrlink.RegDocTypeName = regDocTypeNameValue;

            //5201100 Номер документа-основания (рег)
            oMAddrlink.RegDocNumber = row["NDOC"] != null && row["NDOC"] != DBNull.Value ?
                row["NDOC"].ToString() : null;

            //5201200 Дата документа-основания (рег)
            oMAddrlink.RegDocDate = null;
            if (row["DDOC"] != DBNull.Value)
            {
                DateTime tempRegDocDate;
                if (row["DDOC"].TryParseToDateTime(out tempRegDocDate))
                {
                    oMAddrlink.RegDocDate = tempRegDocDate;
                }
            }

            //5200500 UNAD
            oMAddrlink.Unad = row["UNAD"] != DBNull.Value ? (long?)row["UNAD"].ParseToLong() : null;

            return oMAddrlink;
        }
        #endregion

        #region Вспомогательные методы
        /// <summary>
        /// Получает код справочника из РСМ по соответсию с кодом БТИ
        /// </summary>
        /// <param name="row">Строка</param>
        /// <param name="rowIndex">Столбец</param>
        /// <param name="nk">Номер классификатора</param>
        /// <param name="kod">Код классификатора</param>
        private ReferenceCacheItem GetReferenceCacheItem(DataRow row, string rowIndex)
        {
            if (row[rowIndex] == DBNull.Value || row[rowIndex] == null)
                return null;

            int? nk = _referenceCache.SqlDictionaryMapping.GetValue(rowIndex);

            if (!nk.HasValue)
            {
                throw new ConfigurationErrorsException(String.Format("Не удалось найти соответсвие \"поле запроса - номер классификатора\": {0}", rowIndex));
            }

            string klKod = row[rowIndex].ToString();
            return _referenceCache.GetReferenceItem(nk.Value, klKod);
        }

        /// <summary>
        /// Получает код справочника из РСМ по соответсию с идентификатором БТИИ
        /// </summary>
        /// <param name="row">Строка</param>
        /// <param name="rowIndex">Столбец</param>
        /// <param name="nk">Номер классификатора</param>
        /// <param name="kod">Код классификатора</param>
        private ReferenceCacheItem GetReferenceCacheItemById(DataRow row, string rowIndex)
        {
            if (row[rowIndex] == DBNull.Value || row[rowIndex] == null)
                return null;

            int? nk = _referenceCache.SqlDictionaryMapping.GetValue(rowIndex);

            if (!nk.HasValue)
            {
                throw new ConfigurationErrorsException(String.Format("Не удалось найти соответсвие \"поле запроса - номер классификатора\": {0}", rowIndex));
            }

            long btiId = row[rowIndex].ParseToLong();
            return _referenceCache.GetReferenceItemById(nk.Value, btiId);
        }

        private void ExtractReferenceCacheItemCodeValue(ReferenceCacheItem referenceCacheItem, out long? code, out string value)
        {
            long? tempCode = null;
            string tempValue = null;

            if (referenceCacheItem != null)
            {
                tempCode = referenceCacheItem.ItemId;
                tempValue = referenceCacheItem.Value;
            }

            code = tempCode;
            value = tempValue;
        }

        /// <summary>
        /// у них хначения из классификатора да-1/нет-2, у нас bool
        /// так и преобразуем 1-true, 2-false
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool ParseBtiBoolean(object value)
        {
            if (value == DBNull.Value || value == null)
                return false;

            int intValue = Convert.ToInt32(value);

            return intValue == 1;
        }

        /// <summary>
        /// Получаем идентификатор в РСМ (ID_IN_DS) UNOM + UNAD
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetID_IN_DS(DataRow row)
        {
            var unad = Convert.ToString(row[Unad]);
            var unom = Convert.ToString(row[Unom]);
            return unom + " " + unad;
        }

        private string MakeAddress(OMADDRESS oMAddress, DataRow row)
        {
            if (oMAddress.StreetName.IsNullOrEmpty() && row["ADRES"] != DBNull.Value)
            {
                return row["ADRES"].ToString();
            }

            List<string> addressFacets = new List<string>
            {
                "г. Москва",
                oMAddress.StreetName
            };

            if (oMAddress.HouseNumber.IsNotEmpty())
            {
                //возможно вместо "дом" нужно брать значение классификатора 122 из L1_TYPE
                addressFacets.Add(string.Format("{0} {1}", oMAddress.PropertyTypeName != null ? oMAddress.PropertyTypeName.ToLower() : null, oMAddress.HouseNumber));
            }

            if (oMAddress.KorpusNumber.IsNotEmpty())
            {
                //возможно вместо "корпус" нужно брать значение классификатора 563 из L2_TYPE
                addressFacets.Add(string.Format("{0} {1}", oMAddress.TypeCorpus != null ? oMAddress.TypeCorpus.ToLower() : null, oMAddress.KorpusNumber));
            }

            if (oMAddress.StructureNumber.IsNotEmpty())
            {
                //возможно вместо "строение" нужно брать значение классификатора 123 из L3_TYPE
                addressFacets.Add(string.Format("{0} {1}", oMAddress.StructureTypeName != null ? oMAddress.StructureTypeName.ToLower() : null, oMAddress.StructureNumber));
            }

            return String.Join(", ", addressFacets);
        }

        /// <summary>
        /// вычисляем признак подземный по типу
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool? GetUNDEGROUND(long? typeNameCode)
        {
            /*
             * 
            1	подвал
            2	технический этаж
            3	техподвал
            4	цоколь
            5	чердак
            6	антресоль
            7	мансарда
            8	мезонин
            9	светелка
            10	полуподвал
            11	надстроенный этаж
            12	антресоль подвала
            13	антресоль цокольного этажа
            14	чердачная надстройка
            15	техподполье
            16	подвал под двором
            17	подземный этаж
            18	кровля
            19	подпол
            20	ярус
            21	антресоль чердака
            22	эксплуатируемая кровля
             * 
         */
            if (!typeNameCode.HasValue)
            {
                return null;
            }

            long tet = typeNameCode.Value;

            return (tet == 1 || tet == 4 || tet == 10 || tet == 15 || tet == 16 || tet == 17 || tet == 19);
        }

        #region Номер помещения
        private string GetNumber(DataRow row)
        {
            var obj = row["RIM"];
            if (obj.IsNullOrDbNull())
            {
                return Convert.ToString(row["KV"]);
            }
            var str = Convert.ToString(obj);
            int tmp;
            if (!int.TryParse(str, out tmp) || tmp == 0)
            {
                return Convert.ToString(row["KV"]);
            }
            obj = row["KV"];
            if (obj.IsNullOrDbNull())
            {
                return null;
            }
            str = Convert.ToString(obj);
            if (int.TryParse(str, out tmp))
            {
                return ArabicToRomanAsAdobe(tmp);
            }
            return str;
        }
        private int[] Arabics = { 1, 5, 10, 50, 100, 1000 };
        private char[] Romans = { 'I', 'V', 'X', 'L', 'C', 'M' };
        // {0,0,0,2,2,4} - 1990 == MCMXC
        // {0,0,0,0,0,0} - 1990 == MXM
        // {0,0,0,2,4,5} - 1990 == MCCCCCCCCCLXL
        private int[] Subs = { 0, 0, 0, 2, 2, 4 };
        System.Collections.Concurrent.ConcurrentDictionary<int, string> cache = new System.Collections.Concurrent.ConcurrentDictionary<int, string>();
        public string ArabicToRomanAsAdobe(int arabic)
        {
            string res;
            if (cache.TryGetValue(arabic, out res))
                return res;
            StringBuilder retVal = new StringBuilder();
            while (arabic > 0)
            {
                for (int i = 5; i >= 0; i--)
                {
                    if (arabic >= Arabics[i])
                    {
                        retVal.Append(Romans[i]);
                        arabic -= Arabics[i];
                        break;
                    }
                    bool flag = false;
                    for (int j = Subs[i]; j < i; j++)
                    {
                        // 5 != VX а 50 != LC
                        if (Arabics[j] == Arabics[i] - Arabics[j])
                            continue;
                        if (arabic >= Arabics[i] - Arabics[j])
                        {
                            retVal.Append(Romans[j]);
                            retVal.Append(Romans[i]);
                            arabic -= Arabics[i] - Arabics[j];
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                        break;
                }
            }
            res = retVal.ToString();
            cache.TryAdd(arabic, res);
            return res;
        }
        #endregion

        #region Вид площади комнаты
        #endregion
        private ReferenceCacheItem GetAREA_KIND_Attribute(DataRow row)
        {
            var data = row["PLOV"];
            if (data == null || data == DBNull.Value)
                return null;
            var PLOV = Convert.ToInt32(data);
            var tpl = GetCharacteristicsArea(PLOV);
            if (tpl == null)
                return null;
            return new ReferenceCacheItem()
            {
                Code = tpl.Item1.ToString(),
                Value = tpl.Item2
            };
        }

        /*
1943    0	за итогом
1944    1	основная
1945    2	вспомогательная
1946    3	лоджия
1947    4	балкон
1948    5	холодные
         */
        /*
         Характеристика площади (0:5)за/ит;осн;всп;лод;балкон;пр.холодные помещения
         */
        /// <summary>
        /// жесткий хардкод)
        /// </summary>
        /// <param name="ca"></param>
        /// <returns></returns>
        private Tuple<int, string> GetCharacteristicsArea(int ca)
        {
            switch (ca)
            {
                case 0:
                    return Tuple.Create(1943, "за итогом");
                case 1:
                    return Tuple.Create(1944, "основная");
                case 2:
                    return Tuple.Create(1945, "вспомогательная");
                case 3:
                    return Tuple.Create(1946, "лоджия");
                case 4:
                    return Tuple.Create(1947, "балкон");
                case 5:
                    return Tuple.Create(1948, "холодные");
                default:
                    return null;
            }
        }
        #endregion
    }
}
