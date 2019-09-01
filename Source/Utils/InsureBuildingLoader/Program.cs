using CIPJS.DAL.Bti.Import;
using CIPJS.DAL.Building;
using CIPJS.DAL.Egas;
using Core.DBManagment;
using Core.ErrorManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Ehd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace CIPJS.DAL.InsuranceObjectLoader
{
    class Program
    {
        private static ConfigEhdObjectsImport ImportConfig
        {
            get
            {
                return Core.ConfigParam.Configuration.GetParam<ConfigEhdObjectsImport>("EhdObjectsImportConditions", "Ehd");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            OMProcessType processType = new OMProcessType();
            OMQueue processQueue = new OMQueue();

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			
            try
            {
                if (ConfigurationManager.AppSettings["LoadType"] == "Building")
                {
                    new BuildingLoader().StartProcess(processType, processQueue, cancelTokenSource.Token);
                }
                else if (ConfigurationManager.AppSettings["LoadType"] == "Flat")
                {
                    new InsurFlatLoader().StartProcess(processType, processQueue, cancelTokenSource.Token);
                }
                else if (ConfigurationManager.AppSettings["LoadType"] == "CorrectEgas")
                {
                    CorrectEgas();
                }
                else if (ConfigurationManager.AppSettings["LoadType"] == "CorrectBtiFlat")
                {
                    CorrectBtiFlat();
                }
                else if (ConfigurationManager.AppSettings["LoadType"] == "BtiBuildings")
                {
                    CIPJS.DAL.Bti.Import.Importer importer = new CIPJS.DAL.Bti.Import.Importer(true, cancelTokenSource.Token);
                    importer.Import();
                }
                else if (ConfigurationManager.AppSettings["LoadType"] == "FlagInsurFiller")
                {
					processQueue = OMQueue.Where(x => x.Id == 36595153).SelectAll().ExecuteFirstOrDefault();
					
					new FlagInsurFiller().StartProcess(processType, processQueue, cancelTokenSource.Token);
                }
                else if (ConfigurationManager.AppSettings["LoadType"] == "CorrectBtiAoAndRegion")
                {
                    CorrectBtiAoAndRegion();
                }
                else if (ConfigurationManager.AppSettings["LoadType"] == "UpdateInsurFlat")
                {
                    UpdateInsurFlat();
                }
                else if (ConfigurationManager.AppSettings["LoadType"] == "CalculateFlagInsur")
                {
                    CalculateFlagInsur();
                }
                else if (ConfigurationManager.AppSettings["LoadType"] == "LinkMKD")
                {
                    LinkMKD();
                }
                else if(ConfigurationManager.AppSettings["LoadType"] == "EgasImportLoad")
                {
                    processType = OMProcessType.Where(w => w.Id == 3).SelectAll().ExecuteFirstOrDefault();
                    processQueue.Status = -1;
                    new EgasImportLoadProcess().StartProcess(processType, processQueue, cancelTokenSource.Token);
                }
                else
                {
                    Console.WriteLine("Не корректная настройка LoadType: " + ConfigurationManager.AppSettings["LoadType"]);
                }
            }
            catch (Exception ex)
            {
                int errorId = ErrorManager.LogError(ex);
                Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy hh.mm.ss}] Возникла ошибка {ex.Message} (см. журнал {errorId})");
            }

            Console.WriteLine($"\n[{DateTime.Now:dd.MM.yyyy hh.mm.ss}] Import finished");
            Console.ReadLine();
            Environment.Exit(0);
        }

        #region Correct EGAS

        private int _packageNumber;

        private static DataTable GetRightTable()
        {
            string commandText = $@"SELECT R.id,r.egrp_id FROM ehd.right R join izk_rsm.cipjs_import_right l on l.id = r.id where  l.egrp_id is null and r.egrp_id is not null and rownum <= 50000";

            DbCommand command = DBMngr.SomeDB.GetSqlStringCommand(commandText);

            return DBMngr.SomeDB.ExecuteDataSet(command).Tables[0];
        }

        private static void SaveRight(DataRow row)
        {
            if (row == null) return;

            OMRight omRight = new OMRight
            {
                EmpId = row["ID"].ParseToLong(),
                EhdRightId = row["ID"].ParseToLong(),
                EgrpId = row["EGRP_ID"]?.ParseToLong()
            };

            omRight.Save();
        }

        private static void CorrectEgas()
        {
            int successRowCount = 0;
            int failRowCount = 0;

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            DataTable currentModels = GetRightTable();

            while (true)
            {
                if (currentModels.Rows.Count == 0)
                {
                    break;
                }

                Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} получено записей: {currentModels.Rows.Count};  Импортировано {successRowCount}. Не удалось импортировать {failRowCount}.");

                ParallelOptions options = new ParallelOptions()
                {
                    CancellationToken = cancelTokenSource.Token,
                    MaxDegreeOfParallelism = 10
                };

                Parallel.ForEach(currentModels.AsEnumerable(), options, x =>
                {
                    try
                    {
                        SaveRight(x);

                        UpdateLog(x);

                        successRowCount++;
                    }
                    catch (Exception ex)
                    {
                        long errorId = ErrorManager.LogError(ex);

                        UpdateLog(x, errorId, $"При импортепроизошла ошибка: {ex.Message} (журнал №{errorId})");

                        failRowCount++;
                    }
                });

                currentModels = GetRightTable();
            }
        }

        private static void UpdateLog(DataRow row, long? errorId = null, string errorMessage = null)
        {
            string dateImport = CrossDBSQL.ToDate(DateTime.Now, CrossDBSQL.Providers.PrvOracle);

            int isError = errorMessage.IsNotEmpty() ? 1 : 0;

            string message = (errorMessage ?? "");
            string strErrorId = errorId.HasValue ? errorId.Value.ToString() : "NULL";

            string cmdText = $"UPDATE izk_rsm.cipjs_import_right SET EGRP_ID={row["EGRP_ID"].ParseToLong()} WHERE ID={row["id"].ParseToLong()}";

            DbCommand command = DBMngr.SomeDB.GetSqlStringCommand(cmdText);
            DBMngr.SomeDB.ExecuteNonQuery(command);
        }

        #endregion

        #region Correct Flat

        private static DataTable GetFlats()
        {
            string commandText = $@"SELECT p.EMP_ID as FLAT_ID, p.ID_IN_SOURCE from bti_premase p WHERE p.BIT0 is null LIMIT 1000"; //AND p.floor_id in (select f.emp_id from bti_floor_q f where f.building_id = 68338723)

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }


        private static void CorrectBtiFlat()
        {
            int successRowCount = 0;
            int failRowCount = 0;

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            DataTable currentModels = GetFlats();

            while (true)
            {
                if (currentModels.Rows.Count == 0)
                {
                    break;
                }

                Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} получено записей: {currentModels.Rows.Count};  Импортировано {successRowCount}. Не удалось импортировать {failRowCount}.");

                ParallelOptions options = new ParallelOptions()
                {
                    CancellationToken = cancelTokenSource.Token,
                    MaxDegreeOfParallelism = 10
                };

                string objIds = String.Join(",", currentModels.AsEnumerable().Select(x => x["ID_IN_SOURCE"]));

                string commandText = $@"SELECT fkva.OBJ_ID, fkva.BIT0 FROM bti_data.fkva fkva WHERE fkva.OBJ_ID in ({objIds})";

                DbCommand command = CipjsDbManager.Dgi.GetSqlStringCommand(commandText);
                DataTable dtBtiFlats = CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0];

                Parallel.ForEach(currentModels.AsEnumerable(), options, row =>
                {
                    try
                    {
                        var btiRows = dtBtiFlats.Select($"OBJ_ID = {row["ID_IN_SOURCE"]}");

                        ObjectModel.Bti.OMPremase omFlat;

                        if (btiRows.Length == 0)
                        {
                            omFlat = new ObjectModel.Bti.OMPremase
                            {
                                EmpId = row["FLAT_ID"].ParseToLong(),
                                Bit0 = false
                            };

                            ErrorManager.LogError($"Нет строк БТИ", $"omFlat.EmpId = { omFlat.EmpId}, найдено 0");
                        }
                        else if (btiRows.Length > 1)
                        {
                            omFlat = new ObjectModel.Bti.OMPremase
                            {
                                EmpId = row["FLAT_ID"].ParseToLong(),
                                Bit0 = false
                            };

                            ErrorManager.LogError($"Нет строк БТИ", $"omFlat.EmpId = { omFlat.EmpId}, найдено {btiRows.Length}");
                        }
                        else
                        {
                            omFlat = new ObjectModel.Bti.OMPremase
                            {
                                EmpId = row["FLAT_ID"].ParseToLong(),
                                Bit0 = btiRows[0]["BIT0"].ParseToBoolean(),
                            };
                        }

                        omFlat.Save();

                        successRowCount++;
                    }
                    catch (Exception ex)
                    {
                        long errorId = ErrorManager.LogError(ex);

                        ObjectModel.Bti.OMPremase omFlat = new ObjectModel.Bti.OMPremase
                        {
                            EmpId = row["FLAT_ID"].ParseToLong(),
                            //ZplFill = errorId
                        };

                        omFlat.Save();

                        failRowCount++;
                    }
                });

                currentModels = GetFlats();
            }
        }

        #endregion

        #region CorrectBtiAoAndRegion

        class ImportModel
        {
            public long? OkrugId { get; set; }

            public long? DistrictId { get; set; }

            public long AddressId { get; set; }

            public long Unom { get; set; }

            public long? Unad { get; set; }
        }

        class AddressModel
        {
            public long? Mr { get; set; }

            public long? Ao { get; set; }

            public long Unom { get; set; }

            public long? Unad { get; set; }
        }

        private static void CorrectBtiAoAndRegion()
        {
            SynchronizationAddresses addr = new SynchronizationAddresses();
            addr.MergeOkrugs();
            addr.MergeDistricts();

            Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy hh.mm.ss}] Синхронизация адреса закончена");

            ReferenceCache referenceCache = new ReferenceCache();

            string sql = @"
				select a.okrug_id as OkrugId, a.district_id as DistrictId, a.emp_id as AddressId, l.unad, b.unom
				  from bti_address_q a
				  join bti_addrlink_q l
					on l.actual = 1 and l.address_id = a.emp_id
				  join bti_building_q b 
					on b.actual = 1 and b.emp_id = l.building_id
				 where a.actual = 1 and
					(a.okrug_id is null or a.district_id is null)";


            DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
            DataTable dt = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            List<ImportModel> importModels = dt.ConvertToObjectsList<ImportModel>();

            int package = 0;

            while (true)
            {
                List<ImportModel> packageObjects = importModels.Skip(1000 * package).Take(1000).ToList();

                if (packageObjects.Count == 0)
                {
                    break;
                }

                sql = $"select  a.unom, a.unad, a.mr, a.ao from {Importer.BtiFads} a where a.unom in (" + String.Join(",", packageObjects.Select(x => x.Unom)) + ")";
                command = CipjsDbManager.Dgi.GetSqlStringCommand(sql);
                dt = CipjsDbManager.Dgi.ExecuteDataSet(command).Tables[0];

                List<AddressModel> addresses = dt.ConvertToObjectsList<AddressModel>();

                ParallelOptions options = new ParallelOptions()
                {
                    CancellationToken = cancelTokenSource.Token,
                    MaxDegreeOfParallelism = 10
                };

                Parallel.ForEach(packageObjects, options, importObject =>
                {
                    AddressModel btiAddress = addresses.FirstOrDefault(x => x.Unom == importObject.Unom && x.Unad == importObject.Unad);

                    if (btiAddress == null)
                    {
                        return;
                    }

                    if (importObject.OkrugId == null && btiAddress.Ao != null || importObject.DistrictId == null && btiAddress.Mr != null)
                    {
                        //50001200 идентификатор района
                        DistrictCacheItem districtCacheItem = referenceCache.GetDistrictCacheItem(btiAddress.Mr.ToString());
                        string districtId = districtCacheItem != null ? districtCacheItem.Id.ToString() : "NULL";

                        //50001100 идентификатор округа
                        OkrugCacheItem okrugCacheItem = referenceCache.GetOkrugCacheItem(btiAddress.Ao.ToString());
                        string okrugId = okrugCacheItem != null ? okrugCacheItem.Id.ToString() : "NULL";

                        using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                        {
                            string updateSql = $"update bti_address_q set OKRUG_ID = {okrugId}, DISTRICT_ID = {districtId} where emp_id = {importObject.AddressId}";
                            var updateCommand = DBMngr.Realty.GetSqlStringCommand(updateSql);
                            DBMngr.Realty.ExecuteNonQuery(updateCommand);

                            ts.Complete();
                        }
                    }
                });

                Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy hh.mm.ss}] Пакет {package} из {importModels.Count / 1000} обработан");

                package++;
            }
        }

        #endregion

        private static void UpdateInsurFlat()
        {
            string exactBuildingId = ConfigurationManager.AppSettings["UpdateInsurFlatExactBuildingId"];
            string exactEhdFlatId = ConfigurationManager.AppSettings["UpdateInsurFlatExactEhdFlatId"];
            string exactBtiFlatId = ConfigurationManager.AppSettings["UpdateInsurFlatExactBtiFlatId"];

            var insurFlatLoader = new InsurFlatLoader();

            var objects = insurFlatLoader.GetInitialObjects(exactBuildingId, exactEhdFlatId, exactBtiFlatId);

            Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] Получено объектов: {objects.Count}");

            if (objects.Count != 1)
            {
                Console.WriteLine("Обработка прервана, так как количество объектов != 1");
                return;
            }

            insurFlatLoader.ImportInitialModel(objects[0]);

            Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] Обработка завершена");
        }

        private static void CalculateFlagInsur()
        {
            long insurBuildingId = ConfigurationManager.AppSettings["InsurBuildingId"].ParseToLong();

            ObjectModel.Insur.OMBuilding insurBuilding = ObjectModel.Insur.OMBuilding.Where(x => x.EmpId == insurBuildingId).SelectAll().ExecuteFirstOrDefault();

            ObjectModel.Bti.OMBtiBuilding btiBuilding = ObjectModel.Bti.OMBtiBuilding.Where(x => x.EmpId == insurBuilding.LinkBtiFsks).SelectAll().ExecuteFirstOrDefault();

            bool? flagInsur = BuildingService.CalculateFlagInsur(insurBuilding.LinkEgrnBild, btiBuilding);
        }

        private static void LinkMKD()
        {
            BuildingService serivce = new BuildingService();
            bool isGone = true;
            while (isGone)
            {
                Console.Write("Не проводить связывание при несовпадении ЖП? (1 - да / 0 - нет): ");
                bool checkLink = Console.ReadLine().ParseToBoolean();
                Console.Write("Посылаем запрос...");
                try
                {
                    string commandText = @"SELECT i1.emp_id, i2.emp_id
from 
(
select * 
from insur_building_q b1
where b1.link_egrn_bild is not null and b1.link_bti_fsks is null and b1.actual =1
) i1
join 
(
select * 
from insur_building_q b2
where b2.link_egrn_bild is  null and b2.link_bti_fsks is not null and b2.actual =1
) i2 on i1.cadastr_num = i2.cadastr_num";
                    DbCommand dbCommand = DBMngr.Realty.GetSqlStringCommand(commandText);
                    DataTable dataTable = DBMngr.Realty.ExecuteDataSet(dbCommand).Tables[0];

                    Console.WriteLine($" Получено {dataTable.Rows.Count} записей");
                    Console.WriteLine("Начинаем обработку...");

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        long fiasBuildingId = dataRow[0].ParseToLong();
                        long btiBuildingId = dataRow[1].ParseToLong();
                        Console.WriteLine($"Связываем МКД-ЕГРН с Id={fiasBuildingId} и МКД-БТИ с Id={btiBuildingId}");
                        try
                        {
                            serivce.LinkBuilding(fiasBuildingId, btiBuildingId, checkLink);
                            Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] МКД связаны");
                        }
                        catch (Exception ex)
                        {
                            int errorId = ErrorManager.LogError(ex);
                            Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] Ошибка при связывании МКД. {ex.Message}. (Подробно в журнале {errorId})");
                        }
                    }
                    Console.WriteLine("Закончили обработку.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] Ошибка. {ex.Message}");
                }
                //         Console.Write("Введите через запятую все МКД, которых хотите связать по парно (МКД, связанное с ЕГРН, МКД связанное с БТИ): ");
                //         string[] ids = Console.ReadLine().Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);
                //         if(ids.Length % 2 != 0)
                //         {
                //             Console.WriteLine("Количество введенных МКД должно быть четным");
                //         }
                //         else
                //         {
                //             Console.Write("Не проводить связывание при несовпадении ЖП? (1 - да / 0 - нет): ");
                //             bool checkLink = Console.ReadLine().ParseToBoolean();
                //             for (int i = 0; i < ids.Length; i += 2)
                //             {
                //                 long fiasBuildingId = ids[i].ParseToLong();
                //                 long btiBuildingId = ids[i + 1].ParseToLong();
                //                 Console.WriteLine($"Связываем МКД-ЕГРН с Id={fiasBuildingId} и МКД-БТИ с Id={btiBuildingId}");
                //                 try
                //                 {
                //                     serivce.LinkBuilding(fiasBuildingId, btiBuildingId, checkLink);
                //                     Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] МКД связаны");
                //                 }
                //                 catch (Exception ex)
                //                 {
                //int errorId = ErrorManager.LogError(ex);
                //Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] Ошибка при связывании МКД. {ex.Message}. (Подробно в журнале {errorId})");
                //                 }
                //             }
                //         }

                //Console.Write("Введите Id МКД, связанного с ЕГРН: ");
                //long fiasBuildingId = Console.ReadLine().ParseToLong();
                //Console.Write("Введите Id МКД, связанного с БТИ: ");
                //long btiBuildingId = Console.ReadLine().ParseToLong();
                //Console.Write("Не проводить связывание при несовпадении ЖП? (1 - да / 0 - нет): ");
                //bool checkLink = Console.ReadLine().ParseToBoolean();
                //Console.WriteLine($"Связываем МКД-ЕГРН с Id={fiasBuildingId} и МКД-БТИ с Id={btiBuildingId}");
                //try
                //{
                //    serivce.LinkBuilding(fiasBuildingId, btiBuildingId, checkLink);
                //    Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] МКД связаны");
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] Ошибка при связывании МКД. {ex.Message}");
                //}
                Console.Write("Продолжить работу? (1 - да/0 - нет)");
                isGone = Console.ReadLine().ParseToBoolean();
            }
            Environment.Exit(0);
        }
    }
}