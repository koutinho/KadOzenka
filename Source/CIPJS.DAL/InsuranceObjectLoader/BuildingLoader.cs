using CIPJS.DAL.Building;
using Core.Shared.Extensions;
using ObjectModel.Bti;
using ObjectModel.Directory;
using ObjectModel.Insur;
using ObjectModel.Ehd;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Threading;
using System.Transactions;
using Core.Shared.Misc;
using Core.ErrorManagment;
using Core.Register;
using Newtonsoft.Json;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Configuration;
using Core.Shared.Exceptions;
using Core.Register.QuerySubsystem;
using ObjectModel.ImportLog;
using Core.SRD;
using CIPJS.DAL.Bti.Import;

namespace CIPJS.DAL.InsuranceObjectLoader
{
    public class BuildingLoader : ILongProcess
    {
        private static int _errorCount;
        private static int _processedCount;


        public BuildingLoader()
        {
        }

        readonly int MaxRowNum = ConfigurationManager.AppSettings["InsurBuildingPackageSize"].IsNullOrEmpty() ?
            1000 : ConfigurationManager.AppSettings["InsurBuildingPackageSize"].ParseToInt();

        readonly string ExeactKadNum = ConfigurationManager.AppSettings["InsurBuildingExeactKadNum"];

        const int ThreadsCount = 10;

        private DateTime _downloadDate { get; set; }

        static List<long> _sostCodeDubl;

        static List<long> SostCodeDubl
        {
            get
            {
                if (_sostCodeDubl == null)
                {
                    string commandText = String.Format(@"
                        select t.itemid
                        from core_reference_item t
                        where t.referenceid = 65 and
                                t.code in ('1', '7', '10', '11', '12', '17', '19', '20', '21', '41', '43', '42');");
                    DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
                    DataTable sostCodeDataTable = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

                    _sostCodeDubl = sostCodeDataTable.AsEnumerable().Select(m => m.Field<long>("itemid")).ToList();
                }

                return _sostCodeDubl;
            }
        }

        public enum LoadType
        {
            Init,
            Update,
            UpdateSingle
        }

        public enum ImportStatus
        {
            /// <summary>
            /// Зарезервировано таской для дальнейшего импорта 
            /// </summary>
            ReservedByTask = -1,

            /// <summary>
            /// Импорт удачно выполнен
            /// </summary>
            Success = 0,

            /// <summary>
            /// Импорт произошел с ошибкой
            /// </summary>
            Error = 1
        }

        class ImportObject
        {
            /// <summary>
            /// ИД журнала
            /// </summary>
            public OMInsurBuildingLog Log { get; set; }

            public long ErrorId { get; set; }
            public string ErrorMessage { get; set; }

            public ObjectModel.Bti.OMBtiBuilding BtiMainBuilding { get; set; }
            public ObjectModel.Bti.OMADDRESS BtiAddress { get; set; }
            public List<OMLinkBuildBti> LinkBuildBti { get; set; }

            public OMBuildParcel EhdBuildParcel { get; set; }
            public OMLocation EhdLocation { get; set; }
            public OMRegister EhdRegister { get; set; }
            public OMEgrp EhdEgrp { get; set; }
            public OMOldNumber OldNumber { get; set; }

            public ObjectModel.Insur.OMBuilding InsurBuilding { get; set; }
        }

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            if (processQueue != null)
            {
                processQueue.Status = (long)OMQueueStatus.Running;
                processQueue.StartDate = DateTime.Now;
                processQueue.Save();
            }
            // Проверка на наличие в ЕГРН дублей по кадастровому номеру.
            // Наличие дублей по КН может привести к бесконечному зацикливанию.
            DbCommand commandCheck = DBMngr.Realty.GetSqlStringCommand("select t.object_id, count(1) as cnt from ehd_build_parcel_q t where coalesce(t.actual_ehd,0) = 0 group by t.object_id having count(1) > 1");
            DataTable dt = DBMngr.Realty.ExecuteDataSet(commandCheck).Tables[0];

            if (dt.Rows.Count > 0)
            {
                var allDoubles = dt.Rows.OfType<DataRow>().Select(dr => $"{dr.Field<string>("object_id")} ({dr.Field<long>("cnt")})");
                throw ExceptionInitializer.Create("Найдены дубли по Кадастровому номеру в источнике ЕГРН, продолжение формирования объектов страхования невозможно",
                    String.Join(";", allDoubles.Take(100)) + (allDoubles.Count() > 100 ? "..." : ""));
            }

            // Проверка на наличие в БТИ дублей по УНОМ.
            // Наличие дублей по УНОМ может привести к бесконечному зацикливанию.
            commandCheck = DBMngr.Realty.GetSqlStringCommand("select t.unom, count(1) as cnt from bti_building_q t where t.actual = 1 group by t.unom having count(1) > 1");
            dt = DBMngr.Realty.ExecuteDataSet(commandCheck).Tables[0];

            if (dt.Rows.Count > 0)
            {
                var allDoubles = dt.Rows.OfType<DataRow>().Select(dr => $"{dr.Field<long>("unom")} ({dr.Field<long>("cnt")})");
                throw ExceptionInitializer.Create("Найдены дубли по УНОМ в источнике БТИ, продолжение формирования объектов страхования невозможно",
                    String.Join(";", allDoubles.Take(100)) + (allDoubles.Count() > 100 ? "..." : ""));
            }

            _downloadDate = DateTime.Now;

            List<ImportObject> currentModels = GetObjects();


            ParallelOptions options = new ParallelOptions()
            {
                CancellationToken = cancellationToken,
                MaxDegreeOfParallelism = ThreadsCount
            };

            while (true)
            {
                Console.Write("\rПолучено объектов: {0}; Импортировано = {1} из них с ошибкой {2}", currentModels.Count, _processedCount, _errorCount);

                if (!currentModels.Any())
                {
                    break;
                }

                Parallel.ForEach(currentModels, options, x =>
                {
                    try
                    {
                        ImportModel(x);
                        LogRecord(x);
                        Console.Write("\rПолучено объектов: {0}; Импортировано = {1} из них с ошибкой {2}", currentModels.Count, (++_processedCount), _errorCount);
                    }
                    catch (Exception ex)
                    {
                        LogRecord(x, ex.Message, ErrorManager.LogError(ex));
                        Console.Write("\rПолучено объектов: {0}; Импортировано = {1} из них с ошибкой {2}", currentModels.Count, _processedCount, (++_errorCount));
                    }
                });

                currentModels = GetObjects();
            }

            // Заполнение адресов
            DbCommand command = DBMngr.Realty.GetSqlStringCommand("select public.fias_fill_insur_buildings_address()");
            DBMngr.Realty.ExecuteNonQuery(command);

            if(processQueue != null)
            {
                processQueue.Status = (long)OMQueueStatus.Completed;
                processQueue.EndDate = DateTime.Now;
                processQueue.Save();
            }
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null) { }

        public bool Test() { return true; }

        private void WriteRowsInLog(List<ImportObject> models, int taskId = -1)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                foreach (ImportObject model in models)
                {
                    model.Log.Save();
                }

                ts.Complete();
            }
        }

        private void LogRecord(ImportObject model, string errorMessage = null, int? errorId = null)
        {
            ImportStatus status = errorMessage.IsNotEmpty() ? ImportStatus.Error : ImportStatus.Success;

            List<long> existingLogIds = new List<long>();

            if (model.InsurBuilding != null && model.InsurBuilding.EmpId != -1)
            {
                model.Log.InsurBuildingId = model.InsurBuilding.EmpId;
            }

            // Получение существующих журналов
            if (model.Log.InsurBuildingId != null)
            {
                existingLogIds.AddRange(OMInsurBuildingLog.Where(x => x.InsurBuildingId == model.Log.InsurBuildingId).Execute().Select(x => x.Id));
            }

            if (model.Log.EhdParcelId != null)
            {
                existingLogIds.AddRange(OMInsurBuildingLog.Where(x => x.EhdParcelId == model.Log.EhdParcelId).Execute().Select(x => x.Id));
            }

            if (model.Log.BtiBuildingId != null)
            {
                existingLogIds.AddRange(OMInsurBuildingLog.Where(x => x.BtiBuildingId == model.Log.BtiBuildingId).Execute().Select(x => x.Id));
            }

            existingLogIds = existingLogIds.Distinct().ToList();

            if (existingLogIds.Count > 0)
            {
                model.Log.Id = existingLogIds[0];

                model.Log.ErrorAttemptsCount = OMInsurBuildingLog.Where(x => x.Id == model.Log.Id).Select(x => x.ErrorAttemptsCount).ExecuteFirstOrDefault().ErrorAttemptsCount;
            }

            model.Log.DateLoaded = DateTime.Now;
            model.Log.IsError = (int)status;
            model.Log.ErrorMessage = errorMessage;
            model.Log.ErrorId = errorId;

            if (status == ImportStatus.Error)
            {
                if (model.Log.ErrorAttemptsCount == null)
                {
                    model.Log.ErrorAttemptsCount = 0;
                }
                else
                {
                    model.Log.ErrorAttemptsCount++;
                }
            }
            else
            {
                model.Log.ErrorAttemptsCount = null;
            }

            model.Log.Save();

            // Удаляем дубли журналов
            if (existingLogIds.Count > 1)
            {
                for (int i = 1; i < existingLogIds.Count; i++)
                {
                    OMInsurBuildingLog.ODestroy((int)existingLogIds[i]);
                }
            }

            Core.Diagnostics.DiagnosticsManager.Trace("BuildingLoader", "LogRecord", model.Log.Id.ToString(), $"model.Log.Id: {model.Log.Id}\nmodel.Log.IsError: {model.Log.IsError}\nmodel.Log.DateLoaded: {model.Log.DateLoaded};", SRDSession.GetCurrentUserId());
        }

        private bool _ehdObjectsFinished = false;

        private List<ImportObject> GetObjects()
        {
            List<ImportObject> importObjects = new List<ImportObject>();

            // Сначала грузим объекты ЕГРН
            if (!_ehdObjectsFinished)
            {
                importObjects = GetObjectsEhd();

                if (importObjects.Count > 0)
                {
                    return importObjects;
                }
            }

            _ehdObjectsFinished = true;

            // Затем грзим объекты БТИ не связанные с ЕГРН
            importObjects = GetObjectsBtiNotLinked();

            return importObjects;
        }

        private List<ImportObject> GetObjectsEhd()
        {
            string exactKadNumFilter = ExeactKadNum.IsNotEmpty() ? " AND parcel.object_id = '" + ExeactKadNum + "'" : "";

            string commandText = $@"
                SELECT parcel.emp_id, l.id as log_id
                FROM EHD_BUILD_PARCEL_Q parcel
                join EHD_REGISTER_Q register 
                    on register.building_parcel_id = parcel.emp_id 
                left join import_log_insur_building l on l.ehd_parcel_id = parcel.EMP_ID
				left join BTI_BUILDING_Q bti on l.bti_building_id = bti.EMP_ID and bti.actual = 1
                where register.state_id in (1216001, 1216003) AND coalesce(parcel.actual_ehd, 0) = 0
                    AND (parcel.assignation_code = 'Многоквартирный дом' or parcel.assignation_code = 'Жилой дом' and (select count(1) from ehd_register_q flat_r where flat_r.CADASTRAL_NUMBER_OKS = parcel.object_id) > 1)
                    AND (l.id is null or l.date_loaded < '{_downloadDate:yyyy-MM-dd HH:mm:ss}' and (l.is_error <> 0 or l.update_date_ehd < parcel.update_date_ehd or l.update_date_bti < bti.download_date))
					{exactKadNumFilter}
				LIMIT {MaxRowNum}";

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
            DataTable dataTable = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

            List<ImportObject> importObjects = new List<ImportObject>();

            foreach (DataRow row in dataTable.Rows)
            {
                ImportObject importObject = new ImportObject
                {
                    LinkBuildBti = new List<OMLinkBuildBti>()
                };

                // Получение данные ЕГРН
                importObject.EhdBuildParcel = OMBuildParcel.Where(x => x.EmpId == row["emp_id"].ParseToLong()).SelectAll().Execute().FirstOrDefault();

                if (importObject.EhdBuildParcel == null)
                {
                    importObject.ErrorId = -1;
                    importObject.ErrorMessage = "Не найден объект ЕГАС с ИД: " + row["emp_id"].ParseToLong();

                    importObjects.Add(importObject);

                    continue;
                }

                importObject.EhdRegister = OMRegister.Where(x => x.BuildingParcelId == importObject.EhdBuildParcel.BuildingParcelId).SelectAll().Execute().FirstOrDefault();
                importObject.EhdLocation = OMLocation.Where(x => x.BuildingParcelId == importObject.EhdBuildParcel.BuildingParcelId).SelectAll().Execute().FirstOrDefault();
                importObject.EhdEgrp = OMEgrp.Where(x => x.NumCadnum == importObject.EhdBuildParcel.ObjectId).SelectAll().Execute().FirstOrDefault();

                if (importObject.EhdRegister != null)
                {
                    importObject.OldNumber = OMOldNumber.Where(x => x.RegisterId == importObject.EhdRegister.EmpId &&
                        x.Type == "Условный номер" &&
                        x.Number.RegexpLike("^[0-9]+$")).SelectAll().ExecuteFirstOrDefault();
                }

                // Получение данных БТИ
                importObject.BtiMainBuilding = GetBtiBuildingsByKadN(importObject.EhdBuildParcel.ObjectId, importObject.LinkBuildBti);

                // Если не нашли через Кадастровый номер, ищем через логи.
                if (importObject.BtiMainBuilding == null)
                {
                    importObject.BtiMainBuilding = GetBtiBuildingsFromLogByEhd(importObject.EhdBuildParcel, importObject.LinkBuildBti);
                }

                if (importObject.BtiMainBuilding == null && importObject.EhdLocation != null)
                {
                    // TODO: Объединение по коду ФИАС пока закрываем, так как могут быть несколько объектов ЕГРН, связанных с одним БТИ.
                    // Из-за этого алгоритм сваливается в бесконечный цикл.
                    // Продумать как избежать сваливания в бесконечный цикл при нескольких ЕГРН, связанных с одним БТИ.
                    // Проанализировать вероятность сваливания в бесконечный цикл для нескольких БТИ, связанных с одним ЕГРН.
                    //importObject.BtiMainBuilding = GetBtiBuildingsByCodeFias(importObject);
                }

                if (importObject.BtiMainBuilding != null)
                {
                    importObject.BtiAddress = OMADDRESS.Where(x => x.ADDRLINK[0].BuildingId == importObject.BtiMainBuilding.EmpId && x.ADDRLINK[0].AddressStatusName_Code == AddressStatus.Main).SelectAll().Execute().FirstOrDefault();
                }

                // Создание журнала
                importObject.Log = new OMInsurBuildingLog
                {
                    ErrorAttemptsCount = 0
                };

                importObject.Log.IsError = (int)ImportStatus.ReservedByTask;
                importObject.Log.CadNum = importObject.EhdBuildParcel != null ? importObject.EhdBuildParcel.ObjectId : "";
                importObject.Log.Unom = importObject.BtiMainBuilding != null ? importObject.BtiMainBuilding.Unom : null;

                FillLog(importObject.Log, importObject);

                importObjects.Add(importObject);
            }


            return importObjects;
        }

        private void FillLog(OMInsurBuildingLog log, ImportObject importObject)
        {
            if (importObject.EhdBuildParcel != null)
            {
                importObject.Log.EhdParcelId = importObject.EhdBuildParcel.EmpId;
                importObject.Log.UpdateDateEhd = importObject.EhdBuildParcel.UpdateDateEHD;
                importObject.Log.CadNum = importObject.EhdBuildParcel.ObjectId;
            }

            if (importObject.BtiMainBuilding != null)
            {
                importObject.Log.BtiBuildingId = importObject.BtiMainBuilding.EmpId;
                importObject.Log.UpdateDateBti = importObject.BtiMainBuilding.DownloadDate;
                importObject.Log.Unom = importObject.BtiMainBuilding.Unom;
            }
        }

        private List<ImportObject> GetObjectsBtiNotLinked()
        {
            string exactKadNumFilter = ExeactKadNum.IsNotEmpty() ? " AND b.KAD_N = '" + ExeactKadNum + "'" : "";

            string commandText = $@"
                SELECT b.emp_id, l.id as Log_ID, l.update_date_bti, l.is_error, l.date_loaded
                FROM BTI_BUILDING_Q b
                LEFT JOIN import_log_insur_building L on l.bti_building_id = b.EMP_ID
                where b.actual = 1 AND 
                   (l.id is null OR l.date_loaded < '{_downloadDate:yyyy-MM-dd HH:mm:ss}' AND (l.is_error <> 0 OR l.update_date_bti < b.download_date)) and l.EHD_PARCEL_ID is null 
				   {exactKadNumFilter}
				LIMIT {MaxRowNum}";

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
            DataTable dataTable = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

            string firstRowData = "";

            if (dataTable.Rows.Count > 0)
            {
                firstRowData = $"emp_id:{dataTable.Rows[0]["emp_id"]}; Log_ID:{dataTable.Rows[0]["Log_ID"]}; update_date_bti:{dataTable.Rows[0]["update_date_bti"]}; is_error:{dataTable.Rows[0]["is_error"]}; date_loaded:{dataTable.Rows[0]["date_loaded"]}; ";
            }

            Core.Diagnostics.DiagnosticsManager.Trace("BuildingLoader", "GetObjectsBtiNotLinked", "", $"SQL: {commandText}\nRows.Count: {dataTable.Rows.Count}\nRow[0]: {firstRowData}", SRDSession.GetCurrentUserId());

            List<ImportObject> importObjects = new List<ImportObject>();

            foreach (DataRow row in dataTable.Rows)
            {
                ImportObject importObject = new ImportObject
                {
                    LinkBuildBti = new List<OMLinkBuildBti>()
                };

                importObject.BtiMainBuilding = ObjectModel.Bti.OMBtiBuilding.Where(x => x.EmpId == row["emp_id"].ParseToLong()).SelectAll().Execute().FirstOrDefault();

                if (importObject.BtiMainBuilding == null)
                {
                    importObject.ErrorId = -1;
                    importObject.ErrorMessage = "Не найден объект БТИ с ИД: " + row["emp_id"].ParseToLong();

                    importObjects.Add(importObject);

                    continue;
                }

                importObject.LinkBuildBti.Add(new OMLinkBuildBti
                {
                    IdBtiFsks = importObject.BtiMainBuilding.EmpId,
                    FlagDublUnom = false
                });

                importObject.BtiAddress = OMADDRESS.Where(x => x.ADDRLINK[0].BuildingId == importObject.BtiMainBuilding.EmpId && x.ADDRLINK[0].AddressStatusName_Code == AddressStatus.Main).SelectAll().Execute().FirstOrDefault();

                // Создание журнала
                importObject.Log = new OMInsurBuildingLog
                {
                    ErrorAttemptsCount = 0
                };

                importObject.Log.IsError = (int)ImportStatus.ReservedByTask;

                importObject.Log.CadNum = importObject.EhdBuildParcel != null ? importObject.EhdBuildParcel.ObjectId : "";
                importObject.Log.Unom = importObject.BtiMainBuilding != null ? importObject.BtiMainBuilding.Unom : null;

                FillLog(importObject.Log, importObject);

                importObjects.Add(importObject);
            }

            return importObjects;
        }

        ObjectModel.Bti.OMBtiBuilding GetBtiBuildingsByKadN(string kadN, List<OMLinkBuildBti> btiLinks)
        {
            if (kadN.IsNullOrEmpty() || !kadN.Contains(":"))
            {
                return null;
            }

            List<ObjectModel.Bti.OMBtiBuilding> btiBuildings = ObjectModel.Bti.OMBtiBuilding.Where(x => x.KadN == kadN).SelectAll().Execute();

            return GetDublUnom(btiBuildings, btiLinks);
        }

        /// <summary>
        /// Ищем через логи ссылку на БТИ.
        /// </summary>
        /// <param name="kadN"></param>
        /// <param name="btiLinks"></param>
        /// <returns></returns>
        ObjectModel.Bti.OMBtiBuilding GetBtiBuildingsFromLogByEhd(OMBuildParcel ehd, List<OMLinkBuildBti> btiLinks)
        {
            if (ehd == null)
            {
                return null;
            }

            var insurBuildingExists = ObjectModel.Insur.OMBuilding.Where(x => x.LinkEgrnBild == ehd.EmpId).Select(x => x.EmpId).Execute().FirstOrDefault();

            if (insurBuildingExists == null && ehd.ObjectId.IsNotEmpty())
            {
                insurBuildingExists = ObjectModel.Insur.OMBuilding.Where(x => x.CadasrNum == ehd.ObjectId).Select(x => x.EmpId).Execute().FirstOrDefault();
            }

            if (insurBuildingExists != null)
            {
                OMInsurBuildingLog log = OMInsurBuildingLog.Where(x => x.InsurBuildingId == insurBuildingExists.EmpId).Select(x => x.BtiBuildingId).ExecuteFirstOrDefault();

                if(log != null && log?.BtiBuildingId != null)
                {
                    var btiMainBuilding = OMBtiBuilding.Where(x => x.EmpId == log.BtiBuildingId && x.SpecialActual == 1).SelectAll().ExecuteFirstOrDefault();
                    return btiMainBuilding;
                }

            }

            return null;    
        }



        ObjectModel.Bti.OMBtiBuilding GetBtiBuildingsByOldNumber(long oldNumber, List<OMLinkBuildBti> btiLinks)
        {
            if (oldNumber <= 0)
            {
                return null;
            }

            List<ObjectModel.Bti.OMBtiBuilding> btiBuildings = ObjectModel.Bti.OMBtiBuilding.Where(x => x.Unom == oldNumber).SelectAll().Execute();

            return GetDublUnom(btiBuildings, btiLinks);
        }

        ObjectModel.Bti.OMBtiBuilding GetBtiBuildingsByCodeFias(ImportObject importObject)
        {
            if (importObject.EhdEgrp == null || importObject.EhdEgrp.AddrStrCd.IsNullOrEmpty() || importObject.EhdEgrp.AddrStrCd.Length < 5
                || (importObject.EhdEgrp.AddrLevel1Num.IsNotEmpty() && importObject.EhdEgrp.AddrLevel2Num.IsNotEmpty() && importObject.EhdEgrp.AddrLevel3Num.IsNotEmpty()))
            {
                return null;
            }

            string filter = String.Empty;

            if (importObject.EhdEgrp.AddrLevel1Num.IsNotEmpty())
            {
                filter += $" and upper(a.HOUSE_NUMBER) = upper('{importObject.EhdEgrp.AddrLevel1Num.Replace("'", "''")}') ";
            }
            else
            {
                filter += $" and a.HOUSE_NUMBER is null ";
            }

            if (importObject.EhdEgrp.AddrLevel2Num.IsNotEmpty())
            {
                filter += $" and upper(a.KORPUS_NUMBER) = upper('{importObject.EhdEgrp.AddrLevel2Num.Replace("'", "''")}') ";
            }
            else
            {
                filter += $" and a.KORPUS_NUMBER is null ";
            }

            if (importObject.EhdEgrp.AddrLevel3Num.IsNotEmpty())
            {
                filter += $" and upper(a.STRUCTURE_NUMBER) = upper('{importObject.EhdEgrp.AddrLevel3Num.Replace("'", "''")}') ";
            }
            else
            {
                filter += $" and a.STRUCTURE_NUMBER is null ";
            }

            string sql = $@"select distinct l.building_id
from bti_address_q a 
join fias_house h on h.houseguid = lower(a.code_fias)
join fias_addrobj o on o.aoguid = h.aoguid
join bti_addrlink_q l on l.actual = 1 and l.address_id = a.emp_id
where a.actual = 1
and o.plaincode = '{importObject.EhdEgrp.AddrStrCd.Truncate(15)}' {filter} ";

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
            DataTable dt = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

            List<long> buildingIds = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<long>("building_id")).ToList();

            if (buildingIds.Count == 0)
            {
                return null;
            }

            List<ObjectModel.Bti.OMBtiBuilding> btiBuildings = ObjectModel.Bti.OMBtiBuilding.Where(x => buildingIds.Contains(x.EmpId)).SelectAll().Execute();

            return GetDublUnom(btiBuildings, importObject.LinkBuildBti);
        }

        ObjectModel.Bti.OMBtiBuilding GetDublUnom(List<ObjectModel.Bti.OMBtiBuilding> btiBuildings, List<OMLinkBuildBti> btiLinks)
        {
            if (btiBuildings.Count == 0)
            {
                return null;
            }

            if (btiBuildings.Count == 1)
            {
                btiLinks.Add(new OMLinkBuildBti
                {
                    IdBtiFsks = btiBuildings[0].EmpId,
                    FlagDublUnom = true
                });

                return btiBuildings[0];
            }

            // Проставляем признак дублирования
            btiBuildings.ForEach(x =>
            {
                OMLinkBuildBti btiLink = new OMLinkBuildBti
                {
                    IdBtiFsks = x.EmpId,
                    FlagDublUnom = x.Avarzd == true || SostCodeDubl.Contains((long)x.Sost_Code) || x.Kl_Code != BuildingClass.Living || x.Unom < 0 || x.Unom == 9999999,
                    ParentBtiBuilding = x
                };

                btiLinks.Add(btiLink);
            });

            // Берем первый попавшийся с признаком Не дубль
            ObjectModel.Bti.OMBtiBuilding btiMainBuilding = btiLinks.Where(x => x.FlagDublUnom == false).Select(x => x.ParentBtiBuilding).FirstOrDefault();

            // Или любой, если все дубли
            if (btiMainBuilding == null)
            {
                btiMainBuilding = btiLinks[0].ParentBtiBuilding;
            }

            return btiMainBuilding;
        }

        void ImportModel(ImportObject importModel)
        {
            ObjectModel.Insur.OMBuilding insurBuildingExists = null;

            if (importModel.EhdBuildParcel != null)
            {
                insurBuildingExists = ObjectModel.Insur.OMBuilding.Where(x => x.LinkEgrnBild == importModel.EhdBuildParcel.EmpId).Select(x => x.AttributeSource).Execute().FirstOrDefault();

                if (insurBuildingExists == null && importModel.EhdBuildParcel.ObjectId.IsNotEmpty())
                {
                    insurBuildingExists = ObjectModel.Insur.OMBuilding.Where(x => x.CadasrNum == importModel.EhdBuildParcel.ObjectId).Select(x => x.AttributeSource).Execute().FirstOrDefault();
                }
            }
            if (insurBuildingExists == null)
            {
                if (importModel.BtiMainBuilding != null)
                {
                    insurBuildingExists = ObjectModel.Insur.OMBuilding.Where(x => x.LinkBtiFsks == importModel.BtiMainBuilding.EmpId).Select(x => x.AttributeSource).Execute().FirstOrDefault();

                    if (insurBuildingExists == null && importModel.BtiMainBuilding.Unom != null)
                    {
                        insurBuildingExists = ObjectModel.Insur.OMBuilding.Where(x => x.Unom == importModel.BtiMainBuilding.Unom).Select(x => x.AttributeSource).Execute().FirstOrDefault();
                    }
                }
            }

            string mapLogAttribute;

            Dictionary<long, Object> objects = new Dictionary<long, object>();

            objects.Add(OMADDRESS.GetRegisterId(), importModel.BtiAddress);
            objects.Add(ObjectModel.Bti.OMBtiBuilding.GetRegisterId(), importModel.BtiMainBuilding);

            objects.Add(ObjectModel.Ehd.OMBuildParcel.GetRegisterId(), importModel.EhdBuildParcel);
            objects.Add(ObjectModel.Ehd.OMLocation.GetRegisterId(), importModel.EhdLocation);
            objects.Add(ObjectModel.Ehd.OMRegister.GetRegisterId(), importModel.EhdRegister);
            objects.Add(ObjectModel.Ehd.OMOldNumber.GetRegisterId(), importModel.OldNumber);

            var insurBuilding = BuildingService.Map<ObjectModel.Insur.OMBuilding>("InsurObjectMapBuilding", ObjectModel.Insur.OMBuilding.GetRegisterId(), objects, insurBuildingExists != null ? insurBuildingExists.AttributeSource : null, out mapLogAttribute);

            importModel.InsurBuilding = insurBuilding;

            insurBuilding.LinkEgrnBild = importModel.EhdBuildParcel != null ? importModel.EhdBuildParcel.EmpId : (long?)null;
            insurBuilding.LinkBtiFsks = importModel.BtiMainBuilding != null ? importModel.BtiMainBuilding.EmpId : (long?)null;

            insurBuilding.AttributeSource = mapLogAttribute;

            if (insurBuildingExists != null)
            {
                insurBuilding.EmpId = insurBuildingExists.EmpId;
            }

            long? egrnBuildingId = importModel.EhdBuildParcel != null ? importModel.EhdBuildParcel.EmpId : (long?)null;

            insurBuilding.FlagInsurCalculated = BuildingService.CalculateFlagInsur(egrnBuildingId, importModel.BtiMainBuilding);

            // Если здание не было, проставляем flagInsur.
            if(insurBuildingExists == null)
            {
                insurBuilding.FlagInsur = insurBuilding.FlagInsurCalculated;
            }

            // Если есть расхождения со значением "Назначение объекта" БТИ, то явно проставляем значение.
            if(importModel?.BtiMainBuilding?.Naz_Code != null && insurBuilding?.PurposeName_Code != importModel?.BtiMainBuilding?.Naz_Code)
            {
                insurBuilding.PurposeName = importModel?.BtiMainBuilding?.Naz;
                insurBuilding.PurposeName_Code = importModel.BtiMainBuilding.Naz_Code;
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                insurBuilding.Save();

                Console.WriteLine($"Сохранен объект: {insurBuilding.EmpId}, связи с БТИ: {importModel.LinkBuildBti.Count}");

                BuildingService.CopyFlatFlagInsurFromBuilding(insurBuilding.EmpId, insurBuilding.FlagInsur);

                List<OMLinkBuildBti> existsLinksBti = OMLinkBuildBti.Where(x => x.IdInsurBuild == insurBuilding.EmpId).Select(x => x.IdBtiFsks).Execute();

                // Сохраняем новые, обновляем старые
                foreach (OMLinkBuildBti linkBti in importModel.LinkBuildBti)
                {
                    OMLinkBuildBti existsLinkBti = existsLinksBti.FirstOrDefault(x => x.IdBtiFsks == linkBti.IdBtiFsks);

                    if (existsLinkBti != null)
                    {
                        linkBti.EmpId = existsLinkBti.EmpId;
                    }

                    linkBti.IdInsurBuild = insurBuilding.EmpId;
                    linkBti.Save();
                }

                // Удаляем старые ссылки
                // TODO: как удалять: физически или отдельным признаком?

                ts.Complete();
            }
        }
    }
}
