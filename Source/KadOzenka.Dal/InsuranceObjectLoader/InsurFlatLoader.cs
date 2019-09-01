//doc:InsurFlatLoader.cs.xml

#region Used Namespaces
using CIPJS.DAL.Building;
using Core.Shared.Extensions;
using Core.Register.LongProcessManagment;
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
using ObjectModel.Core.LongProcess;
using System.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using Core.Shared.Exceptions;
#endregion


namespace CIPJS.DAL.InsuranceObjectLoader
{
    /// <summary>
    /// .
    /// See <see cref=""/> to .
    /// <seealso cref=""/>
    /// </summary>
    /// <remarks>
    /// .
    /// </remarks>
    /// <example>
    /// 
    /// <code>
    /// 
    /// </code>
    /// </example>
    public class InsurFlatLoader : ILongProcess
    {
        /// <value></value>
        public const string TableLog = "import_log_insur_flat_b";
        /// <value></value>
        public const string TableСhangeLog = "";

        /// <value></value>
        readonly int MaxRowNum = ConfigurationManager.AppSettings["InsurFlatPackageSize"].IsNullOrEmpty() ?
            10 : ConfigurationManager.AppSettings["InsurFlatPackageSize"].ParseToInt();

        /// <value></value>
        public const int ThreadsCount = 10;

        public bool IsInitialLoad = false;

        private DateTime _startExportDate;

        /// <typeparam name="LoadType">.</typeparam>
        public enum LoadType
        {
            Init,
            Update,
            UpdateSingle
        }

        /// <typeparam name="ImportStatus">.</typeparam>
        public enum ImportStatus
        {
            /// <summary>Зарезервировано таской для дальнейшего импорта</summary>
            ReservedByTask = -1,
            /// <summary>Импорт удачно выполнен</summary>
            Success = 0,
            /// <summary>Импорт произошел с ошибкой</summary>
            Error = 1
        }

        /// <summary> </summary>
        /// <returns> </returns>
        /// <exception cref="System.OverflowException"> </exception>
        /// <param name="a">.</param>
        /// <param name="b">.</param>
        /// <typeparam name="T">.</typeparam>
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            _startExportDate = DateTime.Now;

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

            if (IsInitialLoad)
            {
                InitialLoad(cancellationToken);
            }
            else
            {
                LoadPerFlat(processType, processQueue, cancellationToken);
            }
        }

        private void LoadPerFlat(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            List<ImportObjectFlat> previousModels = new List<ImportObjectFlat>();
            List<ImportObjectFlat> currentModels = GetFlatObjects();

            int errorCount = 0;
            long processedCount = 0;

            if (processQueue != null)
            {
                processQueue.StartDate = DateTime.Now;
                processQueue.Status = (long)OMQueueStatus.Running;
                processQueue.Save();
            }
            string stage = "1 этапа";
            while (true)
            {
                //Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Получено объектов: {currentModels.Count} (newEhdFinished: {newEhdFinished}, existingBtiFinished: {existingBtiFinished}); Импортировано = {processedCount}; Ошибки = {errorCount};");
                // Выходим, если не осталось моделей и пройдены все этапы.
                if (!currentModels.Any() && newEhdFinished && newBtiFinished && existingBtiChangedFinished && existingBtiFinished)
                {
                    break;
                }

                if (processQueue != null)
                {
                    processQueue.Message = $"Обработка {stage} {DateTime.Now}  Импортировано = {processedCount}";
                    processQueue.Save();
                }

                Stopwatch sw = Stopwatch.StartNew();

                ParallelOptions options = new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                    MaxDegreeOfParallelism = ThreadsCount
                };

                Parallel.ForEach(currentModels, options, x =>
                {
                    try
                    {
                        bool isNew;

                        OMFlat insurFlat = ImportModelFlat(x.InsurBuilding, x.FlatEhd, x.FlatBti, out isNew);

                        x.InsurFlat = insurFlat;
                        // Добавляем проверку на некорректный номер квартиры "д.33" и т.п.
                        string errorMessage = null;
                        if (insurFlat?.Kvnom != null && insurFlat.Kvnom.Contains("д."))
                        {
                            errorMessage = $"ЖП: {insurFlat.EmpId} некорректный номер кв: {insurFlat.Kvnom}";
                        }
                        LogRecordFlat(x, errorMessage, null, isNew);
                        processedCount++;
                    }
                    catch (Exception ex)
                    {
                        var errorId = ErrorManager.LogError(ex);
                        LogRecordFlat(x, ex.Message, errorId);
                        if (processQueue != null)
                        {
                            processQueue.Message += $"\n При обработке возникла ошибка! Сообщение: {ex.Message}";
                            processQueue.ErrorId = errorId;
                            processQueue.Status = (long)OMQueueStatus.Faulted;
                            processQueue.Save();
                        }
                        errorCount++;
                        throw;
                    }

                    //Console.Write($"\r[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Импортировано = {processedCount}; Ошибки = {errorCount};");
                });
                sw.Stop();

                //Console.WriteLine($"\n[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Обработка пакета: {sw.Elapsed.Ticks / 10000} мсек; Импортировано = {processedCount}; Ошибки = {errorCount};");
                previousModels = currentModels;
                currentModels = GetFlatObjects();

                // Определение этапа.
                stage = existingBtiChangedFinished ? $"3.2 этапа" :
                                          newBtiFinished ? $"3.1 этапа" :
                                          newEhdFinished ? $"2 этапа" : $"1 этапа";

                // Выявление цикла.
                if (previousModels.Any())
                {
                    var isCycle = previousModels.Exists(x => currentModels.Exists(y => y.LogId == x.LogId && y.LogId != null));
                    if (isCycle)
                    {
                        if (processQueue != null)
                        {
                            processQueue.Message += $"\n При обработке {stage} возникло зацикливание!";
                            processQueue.Save();
                        }
                        throw new Exception($"Зацикливание {stage}");
                    }
                }
            }

            if (processQueue != null)
            {
                processQueue.Message += $"\n[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Импортировано = {processedCount}; " +
                    $"Выполнение 1 этапа: {newEhdFinished} " +
                    $"Выполнение 2 этапа: {newBtiFinished} " +
                    $"Выполнение 3.1 этапа: {existingBtiChangedFinished} " +
                    $"Выполнение 3.2 этапа: {existingBtiFinished} ";
                processQueue.EndDate = DateTime.Now;
                processQueue.Status = (long)OMQueueStatus.Completed;
                processQueue.Save();
            }
        }

        /// <summary>
        /// Инициирующая загрузка первый раз на пустой БД.
        /// </summary>
        /// <param name="cancellationToken"></param>
        private void InitialLoad(CancellationToken cancellationToken)
        {
            List<ImportObjectBuilding> currentModels = GetInitialObjects();

            int errorCount = 0;
            int processedCount = 0;

            int flatEhdCount = 0;
            int flatBtiNotLinkCount = 0;
            int flatBtiCount = 0;

            Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Импортировано = {processedCount}; Ошибки = {errorCount}; Квартиры ЕГРН: {flatEhdCount}; Квартиры БТИ без связи: {flatBtiNotLinkCount}");

            while (true)
            {
                if (!currentModels.Any())
                {
                    break;
                }

                Stopwatch sw = Stopwatch.StartNew();

                WriteRowsInLogInitial(currentModels);

                Task<List<ImportObjectBuilding>> taskNextModels =
                    Task.Factory.StartNew<List<ImportObjectBuilding>>(() =>
                    {
                        return GetInitialObjects();
                    }, cancellationToken);

                ParallelOptions options = new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                    MaxDegreeOfParallelism = ThreadsCount
                };

                Parallel.ForEach(currentModels, options, x =>
                {
                    try
                    {
                        flatBtiCount += x.FlatsBti.Count;

                        ImportInitialModel(x);

                        LogRecordInitial(x);
                        processedCount++;
                        flatEhdCount += x.FlatsEhd.Count;
                        flatBtiNotLinkCount += x.FlatsBti.Count;
                    }
                    catch (Exception ex)
                    {
                        LogRecordInitial(x, ex.Message, ErrorManager.LogError(ex));
                        errorCount++;
                    }

                    Console.Write($"\r[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Импортировано = {processedCount}; Ошибки = {errorCount}; Квартиры ЕГРН: {flatEhdCount}; Квартиры БТИ без связи: {flatBtiNotLinkCount}; Квартиры БТИ всего: {flatBtiCount}");
                });

                sw.Stop();

                Console.WriteLine($"\n[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Обработка пакета: {sw.Elapsed.Ticks / 10000} мсек; Импортировано = {processedCount}; Ошибки = {errorCount}; Квартиры ЕГРН: {flatEhdCount}; Квартиры БТИ без связи: {flatBtiNotLinkCount}; Квартиры БТИ всего: {flatBtiCount}");

                sw.Restart();

                currentModels = taskNextModels.Result;

                sw.Stop();

                Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Получение нового пакета пакета: {sw.Elapsed.Ticks / 10000} мсек; Импортировано = {processedCount}; Ошибки = {errorCount}; Квартиры ЕГРН: {flatEhdCount}; Квартиры БТИ без связи: {flatBtiNotLinkCount}; Квартиры БТИ всего: {flatBtiCount}");
            }
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null) { }

        public bool Test() { return true; }

        private static void WriteRowsInLogInitial(List<ImportObjectBuilding> models, int taskId = -1)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                foreach (ImportObjectBuilding model in models)
                {
                    string ehdBuildingId = model.EhdBuilding == null ? "null" : model.EhdBuilding.EmpId.ToString();
                    string btiBuildingId = model.BtiMainBuilding == null ? "null" : model.BtiMainBuilding.EmpId.ToString();
                    string insurBuildingId = model.InsurBuilding == null ? "NULL" : model.InsurBuilding.EmpId.ToString();

                    string commandText = $@"INSERT INTO {TableLog} (id, INSUR_BUILDING_ID, ehd_parcel_id, bti_building_id, IS_ERROR, DATE_LOADED) 
										 VALUES({model.Id}, {insurBuildingId}, {ehdBuildingId}, {btiBuildingId}, {(int)ImportStatus.ReservedByTask}, {CrossDBSQL.ToDate(DateTime.Now)})";

                    DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
                    DBMngr.Realty.ExecuteNonQuery(command);
                }

                ts.Complete();
            }
        }

        static void LogRecordInitial(ImportObjectBuilding model, string errorMessage = null, int? errorId = null)
        {
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Required))
            {
                ImportStatus isError = errorMessage.IsNotEmpty() ? ImportStatus.Error : ImportStatus.Success;
                string message = (errorMessage ?? "");
                string strErrorId = errorId.HasValue ? errorId.Value.ToString() : "NULL";



                string commandText = $"UPDATE {TableLog} SET IS_ERROR={(int)isError}, ERROR_MESSAGE='{message.Replace("'", "''")}', ERROR_ID={ strErrorId} " +
                                     $"WHERE ID={model.Id}";

                DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
                DBMngr.Realty.ExecuteNonQuery(command);

                ts.Complete();
            }
        }

        private void LogRecordFlat(ImportObjectFlat importModel, string errorMessage = null, int? errorId = null, bool? isNew = null)
        {
            string dateImport = CrossDBSQL.ToDate(DateTime.Now);

            int isError = errorMessage.IsNotEmpty() ? 1 : 0;
            string message = (errorMessage ?? "");
            string strErrorId = errorId.HasValue ? errorId.Value.ToString() : "NULL";

            string insurFlatIdStr = importModel?.InsurFlat != null && importModel.InsurFlat.EmpId > 0 ? importModel.InsurFlat.EmpId.ToString() : "NULL";

            string insurBuildingIdStr = importModel?.InsurFlat?.LinkObjectMkd != null && importModel.InsurFlat.LinkObjectMkd > 0 ? importModel.InsurFlat.LinkObjectMkd.ToString() : "NULL";

            string ehdParcelIdStr = importModel?.FlatEhd?.Flat?.EmpId != null ? importModel.FlatEhd.Flat.EmpId.ToString() : "NULL";

            string updateDateEhd = importModel?.FlatEhd?.Flat?.UpdateDateEHD != null ?
                CrossDBSQL.ToDate(importModel.FlatEhd.Flat.UpdateDateEHD.Value) :
                "NULL";

            string cadNom = importModel.FlatEhd?.Flat?.ObjectId != null && importModel.FlatEhd.Flat.ObjectId.IsNotEmpty() ? importModel.FlatEhd.Flat.ObjectId.ToString() : "";

            string btiFlatIdStr = importModel?.FlatBti?.Flat?.EmpId != null ? importModel.FlatBti.Flat.EmpId.ToString() : "NULL";

            string updateDateBti = importModel?.FlatBti?.Flat?.UpdateDate != null ?
                CrossDBSQL.ToDate(importModel.FlatBti.Flat.UpdateDate.Value) :
                "NULL";

            string kvnom = importModel?.FlatBti?.Flat?.Kvnom != null ? importModel.FlatBti.Flat.Kvnom.ToString() : "";

            string isNewStr = isNew != null ? isNew.ParseToInt().ToString() : "NULL";

            /*
			id BIGINT NOT NULL,
			ehd_parcel_id BIGINT,
			bti_flat_id BIGINT,
			insur_flat_id BIGINT,
			date_loaded TIMESTAMP WITHOUT TIME ZONE,
			error_message VARCHAR,
			error_id BIGINT,
			is_error INTEGER,

			update_date_ehd TIMESTAMP WITHOUT TIME ZONE,
			update_date_bti TIMESTAMP WITHOUT TIME ZONE,

			cad_num VARCHAR(50),
			kvnom VARCHAR(50),
			error_attempts_count BIGINT,
			*/
            if (importModel.LogId > 0)
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
					where id = {importModel.LogId}";

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
        }

        public class ImportObjectFlat
        {
            /// <summary>
            /// ИД журнала
            /// </summary>
            public long? LogId { get; set; }

            public long ErrorId { get; set; }
            public string ErrorMessage { get; set; }

            /// <summary>
            /// Объект страхования МКД
            /// </summary>
            public ObjectModel.Insur.OMBuilding InsurBuilding { get; set; }

            /// <summary>
            /// Здание БТИ
            /// </summary>
            public ObjectModel.Bti.OMBtiBuilding BtiMainBuilding { get; set; }
            public ObjectModel.Bti.OMADDRESS BtiAddress { get; set; }
            public ObjectModel.Bti.OMFloor BtiFloor { get; set; }
            public ObjectModel.Bti.OMPremase BtiFlat { get; set; }

            /// <summary>
            /// Здание ЕГРН
            /// </summary>
            public OMBuildParcel EhdBuilding { get; set; }
            public OMLocation EhdBuildingLocation { get; set; }
            public OMRegister EhdBuildingRegister { get; set; }


            public OMFlat InsurFlat { get; set; }

            public ImportObjectFlatEhd FlatEhd { get; set; }

            public ImportObjectFlatBti FlatBti { get; set; }
        }


        public class ImportObjectBuilding
        {
            /// <summary>
            /// ИД журнала
            /// </summary>
            public long Id { get; set; }

            public long ErrorId { get; set; }
            public string ErrorMessage { get; set; }

            /// <summary>
            /// Объект страхования МКД
            /// </summary>
            public ObjectModel.Insur.OMBuilding InsurBuilding { get; set; }

            /// <summary>
            /// Здание БТИ
            /// </summary>
            public ObjectModel.Bti.OMBtiBuilding BtiMainBuilding { get; set; }
            public ObjectModel.Bti.OMADDRESS BtiAddress { get; set; }
            public ObjectModel.Bti.OMFloor BtiFloor { get; set; }
            public ObjectModel.Bti.OMPremase BtiFlat { get; set; }

            /// <summary>
            /// Здание ЕГРН
            /// </summary>
            public OMBuildParcel EhdBuilding { get; set; }
            public OMLocation EhdBuildingLocation { get; set; }
            public OMRegister EhdBuildingRegister { get; set; }

            /// <summary>
            /// Квартиры ЕГРН
            /// </summary>
            public List<ImportObjectFlatEhd> FlatsEhd { get; set; }

            /// <summary>
            /// Квартиры БТИ
            /// </summary>
            public List<ImportObjectFlatBti> FlatsBti { get; set; }
        }

        public class ImportObjectFlatEhd
        {
            public string KadNumOks { get; set; }

            public OMBuildParcel Flat { get; set; }
            public OMRegister FlatRegister { get; set; }
            public OMEgrp FlatEgrp { get; set; }
            public OMLocation FlatLocation { get; set; }
        }

        /// <typeparam name="ImportObjectBti">.</typeparam>
        public class ImportObjectFlatBti
        {
            public long BuildingId { get; set; }
            public OMFloor Floor { get; set; }
            public OMPremase Flat { get; set; }
        }

        bool newEhdFinished = false;
        bool newBtiFinished = false;
        bool existingBtiFinished = false;
        bool existingBtiChangedFinished = false;

        public List<ImportObjectFlat> GetFlatObjects()
        {
            string commandText;
            DbCommand command;
            DataTable dt;

            // Новые объекты ЕГРН
            if (!newEhdFinished)
            {
                // В данном случае идет INNER JOIN с insur_building_q, 
                // так как нас не интересуют квартиры, которые не связаны с сформированными объектами страхования МКД.
                // CIPJS-785: Доработать процедуру загрузки/обновления объектов – ЖП (в части идентификации квартир)
                commandText = $@"
select 
      flat.EMP_ID as ehd_flat_id, 
      ER.EMP_ID as ehd_flat_register_id, 
      EE.emp_id as ehd_flat_egrp_id, 
      l.emp_id as ehd_flat_location_id, 
      ER.CADASTRAL_NUMBER_OKS as CADASTRAL_NUMBER_OKS,
      ib.emp_id as insur_building_id
  from ehd_register_q ER
  join ehd_build_parcel_q flat
      on flat.emp_id = ER.building_parcel_id
      and flat.type = 'Помещение'
  left join ehd_egrp_q EE
on 
      -- 1
      ((ER.assftp1 = 'Квартира' or ER.assftp_cd is null) 
      and ER.cadastral_number_flat is null
      and ER.assftp_cd = 'Жилое помещение') and
      -- 2      
      ((((lower(EE.objt_cd) like 'квартира/комната' and EE.objecttp_cd like '%Помещение%' 
      and lower(EE.aparttp_cd) = 'квартира') and EE.aparttp_cd is not null) 
      or 
      --3 
      (EE.aparttp_cd is null and lower(EE.purposetp_cd) = 'квартира' and EE.purposetp_cd is not null) 
      or 
      -- 4
      (EE.aparttp_cd is null and EE.purposetp_cd is null and lower(EE.name) like '%ква%'))
      and
	  EE.num_cadnum = flat.object_id 
      and coalesce(EE.actual_id, 0) = 0)
  left join ehd_location_q L
      on L.building_parcel_id = flat.emp_id
  join insur_building_q ib 
      on ib.actual = 1 and ib.cadastr_num = ER.CADASTRAL_NUMBER_OKS
  where 
      ER.assftp_cd = 'Жилое помещение' AND
      ER.assftp1 = 'Квартира' AND
      coalesce(flat.actual_ehd, 0) = 0 AND
      ER.CADASTRAL_NUMBER_OKS is not null AND
      not exists (select 1 from import_log_insur_flat lg where lg.ehd_parcel_id = flat.emp_id and (lg.is_error = 0 or lg.is_error = 1 and lg.date_loaded < {CrossDBSQL.ToDate(_startExportDate)}))
limit {MaxRowNum}";
                command = DBMngr.Realty.GetSqlStringCommand(commandText);
                dt = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    return BuildFlatModelsEhd(dt);
                }
            }

            newEhdFinished = true;

            // 2. Загрузка новых объектов БТИ. 
            // Эти объекты не связаны с ЕГРН, так как при загрузке ЕГРН идет поиск соответствия и загрузка объекта в источнике БТИ.
            // В данном случае идет INNER JOIN с insur_building_q, 
            // так как нас не интересуют квартиры, которые не связаны с сформированными объектами страхования МКД.
            if (!newBtiFinished)
            {
                commandText = $@"
select 
      p.emp_id as bti_flat_id, 
      f.emp_id as bti_floor_id, 
      f.building_id as bti_building_id,
	  ib.emp_id as insur_building_id
  from bti_premase p
  join bti_floor_q f 
      on f.emp_id = p.floor_id 
      and f.actual = 1
  join insur_building_q ib
      on ib.actual = 1 and ib.link_bti_fsks = f.building_id
  where
      p.class_name = 'Жилые помещения' and
      p.type_name = 'Квартира' and
      p.bit0 = 0 and
      not exists(select 1 from import_log_insur_flat l where l.bti_flat_id = p.emp_id)
limit {MaxRowNum}";

                command = DBMngr.Realty.GetSqlStringCommand(commandText);
                dt = DBMngr.Realty.ExecuteDataSet(command).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    return BuildFlatModelsNewBti(dt);
                }
            }

            newBtiFinished = true;

            // 3. Обновляем существующих объектов БТИ
            if (!existingBtiFinished)
            {
                string existingBticommandText = $@"
select 
      p.emp_id as bti_flat_id, 
      f.emp_id as bti_floor_id, 
      f.building_id as bti_building_id,

      lg.id as log_id,
      lg.insur_flat_id,
	  lg.insur_building_id,

      lg.ehd_parcel_id as ehd_flat_id,
      ER.EMP_ID as ehd_flat_register_id, 
      EE.emp_id as ehd_flat_egrp_id, 
      l.emp_id as ehd_flat_location_id, 
      ER.CADASTRAL_NUMBER_OKS as CADASTRAL_NUMBER_OKS
  from bti_premase p
  join bti_floor_q f 
      on f.emp_id = p.floor_id 
      and f.actual = 1
  join import_log_insur_flat lg
      on lg.bti_flat_id = p.emp_id
  left join ehd_register_q ER
      on ER.building_parcel_id = lg.ehd_parcel_id
  left join ehd_egrp_q EE
      on EE.num_cadnum = lg.cad_num
      and coalesce(EE.actual_id, 0) = 0
  left join ehd_location_q L
      on L.building_parcel_id = lg.ehd_parcel_id
  where [FILTER]
limit {MaxRowNum}";

                if (!existingBtiChangedFinished)
                {
                    commandText = existingBticommandText.Replace("[FILTER]", "lg.is_error = 0 and lg.update_date_bti < p.update_date");
                    command = DBMngr.Realty.GetSqlStringCommand(commandText);
                    dt = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        return BuildFlatModelsExistingBti(dt);
                    }
                }

                existingBtiChangedFinished = true;

                commandText = existingBticommandText.Replace("[FILTER]", $"lg.is_error = 1 and lg.date_loaded < {CrossDBSQL.ToDate(_startExportDate)}");

                command = DBMngr.Realty.GetSqlStringCommand(commandText);
                dt = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    return BuildFlatModelsExistingBti(dt);
                }
            }

            existingBtiFinished = true;

            return new List<ImportObjectFlat>();
        }

        /// <summary>
        /// Формирование моделей для импорта для объектов новых ЕГРН. 
        /// Так как при обновлении данных в источнике ЕГРН появляется новая запись, то случая обновления данных ЕГРН нет.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<ImportObjectFlat> BuildFlatModelsEhd(DataTable dt)
        {
            /*
			 flat.EMP_ID as ehd_flat_id, 
			 ER.EMP_ID as ehd_flat_register_id, 
			 EE.emp_id as ehd_flat_egrp_id, 
			 l.emp_id as ehd_flat_location_id, 
			 ER.CADASTRAL_NUMBER_OKS as CADASTRAL_NUMBER_OKS,
			 ib.emp_id as insur_building_id
			*/
            List<ImportObjectFlat> importObjects = new List<ImportObjectFlat>();

            var flatIds = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<long>("ehd_flat_id")).ToList();
            Dictionary<long, OMBuildParcel> flats = OMBuildParcel.Where(x => flatIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x);

            var registerIds = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<long>("ehd_flat_register_id")).ToList();
            Dictionary<long, OMRegister> registers = OMRegister.Where(x => registerIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x);

            var egrpIds = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<long?>("ehd_flat_egrp_id")).ToList().Where(x => x.HasValue).Select(x => x.Value).ToList();
            Dictionary<long, OMEgrp> egrps = egrpIds.Count > 0 ?
                OMEgrp.Where(x => egrpIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x) :
                new Dictionary<long, OMEgrp>();

            var locationIds = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<long?>("ehd_flat_location_id")).ToList().Where(x => x.HasValue).Select(x => x.Value).ToList();
            Dictionary<long, OMLocation> locations = locationIds.Count > 0 ?
                OMLocation.Where(x => locationIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x) :
                new Dictionary<long, OMLocation>();

            var insurBuildingIds = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<long?>("insur_building_id")).ToList().Where(x => x.HasValue).Select(x => x.Value).ToList();
            var insurBuildings = OMBuilding.Where(x => insurBuildingIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x);

            foreach (DataRow rowFlatEhd in dt.Rows)
            {
                long flatId = rowFlatEhd["ehd_flat_id"].ParseToLong();
                long flatRegisterId = rowFlatEhd["ehd_flat_register_id"].ParseToLong();
                long? flatEgrpId = rowFlatEhd["ehd_flat_egrp_id"].ParseToLongNullable();
                long? flatLocationId = rowFlatEhd["ehd_flat_location_id"].ParseToLongNullable();

                ImportObjectFlatEhd ehdFlat = new ImportObjectFlatEhd
                {
                    KadNumOks = rowFlatEhd["CADASTRAL_NUMBER_OKS"].ToString(),

                    Flat = flats[flatId],
                    FlatRegister = registers[flatRegisterId],
                    FlatEgrp = flatEgrpId == null ? null : egrps[flatEgrpId.Value],
                    FlatLocation = flatLocationId == null ? null : locations[flatLocationId.Value],
                };

                ImportObjectFlat importObject = new ImportObjectFlat
                {
                    FlatEhd = ehdFlat
                };

                importObjects.Add(importObject);

                long? insurBuildingId = rowFlatEhd["insur_building_id"].ParseToLongNullable();

                if (insurBuildingId.HasValue)
                {
                    // Поиск существующего лога
                    DbCommand command = DBMngr.Realty.GetSqlStringCommand($"select t.id from import_log_insur_flat t where t.insur_building_id = {insurBuildingId.Value} and t.cad_num = '{ehdFlat.Flat.ObjectId}'");
                    DataTable dtLog = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

                    if (dtLog.Rows.Count > 0)
                    {
                        importObject.LogId = dtLog.Rows[0]["id"].ParseToLong();
                    }

                    // Поиск объекта страхования МКД
                    importObject.InsurBuilding = insurBuildings[insurBuildingId.Value];
                }

                if (importObject.InsurBuilding == null)
                {
                    continue;
                }

                // Поиск БТИ
                if (importObject.InsurBuilding.LinkBtiFsks == null)
                {
                    continue;
                }

                if (ehdFlat.FlatLocation == null)
                {
                    continue;
                }

                ObjectModel.Bti.OMPremase btiFlat = null;

                //CIPJS-754 cначала сопоставляемся по кадастровому номеру, если не смогли сопоставится -сопоставляется по номеру квартиры (сейчас реализовано только по номеру квартиры)
                if (ehdFlat.Flat != null && ehdFlat.Flat.ObjectId.IsNotEmpty())
                {
                    btiFlat = OMPremase.Where(x => x.ParentFloor.BuildingId == importObject.InsurBuilding.LinkBtiFsks && x.Kadastr == ehdFlat.Flat.ObjectId)
                        .SelectAll()
                        .Select(x => x.ParentFloor.FloorNumber)
                        .ExecuteFirstOrDefault();
                }

                if (btiFlat == null && ehdFlat.FlatLocation.Apartment.IsNotEmpty())
                {
                    string kvnom = ehdFlat.FlatLocation.Apartment.Replace("Квартира", "").Trim();

                    btiFlat = OMPremase.Where(x => x.ParentFloor.BuildingId == importObject.InsurBuilding.LinkBtiFsks && x.Kvnom == kvnom)
                        .SelectAll()
                        .Select(x => x.ParentFloor.FloorNumber)
                        .ExecuteFirstOrDefault();
                }

                if (btiFlat != null)
                {
                    ImportObjectFlatBti btiFlatImportObject = new ImportObjectFlatBti
                    {
                        BuildingId = importObject.InsurBuilding.LinkBtiFsks.Value,
                        Floor = btiFlat.ParentFloor,
                        Flat = btiFlat
                    };

                    importObject.FlatBti = btiFlatImportObject;
                }
            }

            return importObjects;
        }

        /// <summary>
        /// Формирование моделей для импорта для объектов БТИ, которые уже загружены, связаны с ЕГРН и были изменены в источнике БТИ.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<ImportObjectFlat> BuildFlatModelsExistingBti(DataTable dt)
        {
            /*
			p.emp_id as bti_flat_id, 
			f.emp_id as bti_floor_id, 
			f.building_id as bti_building_id,

			lg.id as log_id,
			lg.insur_flat_id,
			lg.insur_building_id,

			l.ehd_parcel_id as ehd_flat_id,
			ER.EMP_ID as ehd_flat_register_id, 
			EE.emp_id as ehd_flat_egrp_id, 
			l.emp_id as ehd_flat_location_id, 

			ER.CADASTRAL_NUMBER_OKS as CADASTRAL_NUMBER_OKS
			 */
            List<ImportObjectFlat> importObjects = new List<ImportObjectFlat>();

            var ehdFlatIds = dt.Rows.OfType<DataRow>().Select(dr => dr["ehd_flat_id"]?.ParseToLongNullable() ?? null).ToList();
            Dictionary<long, OMBuildParcel> ehdFlats = OMBuildParcel.Where(x => ehdFlatIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x);

            var ehdRegisterIds = dt.Rows.OfType<DataRow>().Select(dr => dr["ehd_flat_register_id"]?.ParseToLongNullable() ?? null).ToList();
            Dictionary<long, OMRegister> ehdRegisters = OMRegister.Where(x => ehdRegisterIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x);

            var ehdEgrpIds = dt.Rows.OfType<DataRow>().Select(dr => dr["ehd_flat_egrp_id"]?.ParseToLongNullable() ?? null).ToList().Where(x => x.HasValue).Select(x => x.Value).ToList();
            Dictionary<long, OMEgrp> ehdEgrps = ehdEgrpIds.Count > 0 ?
                OMEgrp.Where(x => ehdEgrpIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x) :
                new Dictionary<long, OMEgrp>();

            var ehdLocationIds = dt.Rows.OfType<DataRow>().Select(dr => dr["ehd_flat_location_id"]?.ParseToLongNullable() ?? null).ToList().Where(x => x.HasValue).Select(x => x.Value).ToList();
            Dictionary<long, OMLocation> ehdLocations = ehdLocationIds.Count > 0 ?
                OMLocation.Where(x => ehdLocationIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x) :
                new Dictionary<long, OMLocation>();

            var btiFlatIds = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<long?>("bti_flat_id")).ToList().Where(x => x.HasValue).Select(x => x.Value).ToList();
            var btiFlats = OMPremase.Where(x => btiFlatIds.Contains(x.EmpId))
                    .SelectAll()
                    .Select(x => x.ParentFloor.FloorNumber)
                    .Execute().ToDictionary(x => x.EmpId, x => x);

            var insurBuildingIds = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<long?>("insur_building_id")).ToList().Where(x => x.HasValue).Select(x => x.Value).ToList();
            var insurBuildings = OMBuilding.Where(x => insurBuildingIds.Contains(x.EmpId))
                    .SelectAll()
                    .Execute().ToDictionary(x => x.EmpId, x => x);

            foreach (DataRow rowFlatBti in dt.Rows)
            {
                long? flatId = rowFlatBti["ehd_flat_id"]?.ParseToLongNullable();
                long? flatRegisterId = rowFlatBti["ehd_flat_register_id"]?.ParseToLongNullable();
                long? flatEgrpId = rowFlatBti["ehd_flat_egrp_id"].ParseToLongNullable();
                long? flatLocationId = rowFlatBti["ehd_flat_location_id"].ParseToLongNullable();

                ImportObjectFlatEhd ehdFlat = new ImportObjectFlatEhd
                {
                    KadNumOks = rowFlatBti["CADASTRAL_NUMBER_OKS"].ToString(),

                    Flat = flatId != null ? ehdFlats[flatId.Value] : null,
                    FlatRegister = flatRegisterId != null ? ehdRegisters[flatRegisterId.Value] : null,
                    FlatEgrp = flatEgrpId != null ? ehdEgrps[flatEgrpId.Value] : null,
                    FlatLocation = flatLocationId != null ? ehdLocations[flatLocationId.Value] : null
                };

                ImportObjectFlat importObject = new ImportObjectFlat
                {
                    LogId = rowFlatBti["log_id"].ParseToLong(),
                    FlatEhd = ehdFlat
                };

                //string kvnom = ehdFlat.FlatLocation.Apartment.Replace("Квартира", "").Trim();


                if (rowFlatBti["insur_building_id"] == DBNull.Value && rowFlatBti["insur_building_id"].ParseToLong() == 0)
                {
                    throw ExceptionInitializer.Create("Получен пустой ИД insur_building_id", $"log_id: {rowFlatBti["log_id"].ParseToLong()}, flatId: {flatId}, flatRegisterId: {flatRegisterId}, bti_flat_id: {rowFlatBti["bti_flat_id"].ParseToLong()}");
                }

                // Определение здания.
                importObject.InsurBuilding = insurBuildings.ContainsKey(rowFlatBti["insur_building_id"].ParseToLong()) ? insurBuildings[rowFlatBti["insur_building_id"].ParseToLong()] : null;

                // Если здание не найдено.
                if (importObject.InsurBuilding == null)
                {
                    throw ExceptionInitializer.Create("insur_building_id не найден в insurBuildings", $"log_id: {rowFlatBti["log_id"]?.ParseToLong()}, flatId: {flatId}, flatRegisterId: {flatRegisterId}, bti_flat_id: {rowFlatBti["bti_flat_id"]?.ParseToLong()}");
                }

                long btiBuildingId = rowFlatBti["bti_building_id"].ParseToLong();

                if (btiBuildingId == 0)
                {
                    throw ExceptionInitializer.Create("Получен пустой btiBuildingId", $"log_id: {rowFlatBti["log_id"].ParseToLong()}, flatId: {flatId}, flatRegisterId: {flatRegisterId}, bti_flat_id: {rowFlatBti["bti_flat_id"].ParseToLong()}");
                }

                ObjectModel.Bti.OMPremase btiFlat = rowFlatBti["bti_flat_id"] != null ? btiFlats[rowFlatBti["bti_flat_id"].ParseToLong()] : null;

                ImportObjectFlatBti btiFlatImportObject = new ImportObjectFlatBti
                {
                    BuildingId = btiBuildingId,
                    Floor = btiFlat?.ParentFloor,
                    Flat = btiFlat
                };

                importObject.FlatBti = btiFlatImportObject;

                importObjects.Add(importObject);
            }

            return importObjects;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<ImportObjectFlat> BuildFlatModelsNewBti(DataTable dt)
        {
            /*
			p.emp_id as bti_flat_id, 
			f.emp_id as bti_floor_id, 
			f.building_id as bti_building_id,
			ib.emp_id as insur_building_id
			*/
            List<ImportObjectFlat> importObjects = new List<ImportObjectFlat>();

            var btiFlatIds = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<long?>("bti_flat_id")).ToList().Where(x => x.HasValue).Select(x => x.Value).ToList();
            var btiFlats = OMPremase.Where(x => btiFlatIds.Contains(x.EmpId))
                    .SelectAll()
                    .Select(x => x.ParentFloor.FloorNumber)
                    .Execute().ToDictionary(x => x.EmpId, x => x);

            var insurBuildingIds = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<long?>("insur_building_id")).ToList().Where(x => x.HasValue).Select(x => x.Value).ToList();
            var insurBuildings = OMBuilding.Where(x => insurBuildingIds.Contains(x.EmpId))
                    .SelectAll()
                    .Execute().ToDictionary(x => x.EmpId, x => x);

            foreach (DataRow rowFlatBti in dt.Rows)
            {
                ImportObjectFlat importObject = new ImportObjectFlat
                {
                    FlatEhd = null
                };

                importObject.InsurBuilding = insurBuildings[rowFlatBti["insur_building_id"].ParseToLong()];

                ObjectModel.Bti.OMPremase btiFlat = btiFlats[rowFlatBti["bti_flat_id"].ParseToLong()];

                ImportObjectFlatBti btiFlatImportObject = new ImportObjectFlatBti
                {
                    BuildingId = importObject.InsurBuilding.LinkBtiFsks.Value,
                    Floor = btiFlat.ParentFloor,
                    Flat = btiFlat
                };

                importObject.FlatBti = btiFlatImportObject;

                importObjects.Add(importObject);
            }

            return importObjects;
        }

        /// <summary>
        /// Получение объектов по зданиям для инициирующей загрузки квартир на основе зданий
        /// </summary>
        /// <param name="exactBuildingId">ИД МКД, который нужно обновить</param>
        /// <returns></returns>
        public List<ImportObjectBuilding> GetInitialObjects(string exactBuildingId = "", string exactEhdFlatId = "", string exactBtiFlatId = "")
        {
            // Получаем список зданий
            string commandText;
            if (exactBuildingId.IsNotEmpty())
            {
                commandText = $@"select b.emp_id as insur_building_id, null as log_id
						   from insur_building_q b
						 WHERE b.actual = 1 and b.emp_id = {exactBuildingId}
						 LIMIT {MaxRowNum}";
            }
            else
            {
                commandText = $@"select b.emp_id as insur_building_id, l.id as log_id
						   from insur_building_q b
						   left join {TableLog}
								l on l.insur_building_id = b.emp_id
						 WHERE b.actual = 1 and (l.id is null)
						 LIMIT {MaxRowNum}";
            }

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
            DataTable dtInsurBuildings = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

            List<string> ehdList = new List<string>();
            List<string> insurList = new List<string>();

            List<ImportObjectBuilding> importObjects = new List<ImportObjectBuilding>();

            foreach (DataRow row in dtInsurBuildings.Rows)
            {
                long insurBuildingId = row["insur_building_id"].ParseToLong();

                long logId;

                if (row["log_id"] != DBNull.Value)
                {
                    logId = row["log_id"].ParseToLong();
                }
                else
                {
                    logId = CrossDBSQL.GetNextValFromSequence("REG_OBJECT_SEQ");
                }

                ImportObjectBuilding importObject = new ImportObjectBuilding
                {
                    Id = logId,
                };

                importObjects.Add(importObject);

                importObject.InsurBuilding = OMBuilding.Where(x => x.EmpId == insurBuildingId).SelectAll().Execute().FirstOrDefault();

                if (importObject.InsurBuilding.LinkEgrnBild != null)
                {
                    importObject.EhdBuilding = OMBuildParcel.Where(x => x.EmpId == importObject.InsurBuilding.LinkEgrnBild).SelectAll().Execute().FirstOrDefault();

                    if (importObject.EhdBuilding != null)
                    {
                        importObject.EhdBuildingRegister = OMRegister.Where(x => x.BuildingParcelId == importObject.EhdBuilding.BuildingParcelId).SelectAll().Execute().FirstOrDefault();
                        importObject.EhdBuildingLocation = OMLocation.Where(x => x.BuildingParcelId == importObject.EhdBuilding.BuildingParcelId).SelectAll().Execute().FirstOrDefault();
                    }
                }

                if (importObject.InsurBuilding.LinkBuildBti != null)
                {
                    importObject.BtiMainBuilding = ObjectModel.Bti.OMBtiBuilding.Where(x => x.EmpId == importObject.InsurBuilding.LinkBtiFsks).SelectAll().Execute().FirstOrDefault();
                }

                importObject.FlatsEhd = new List<ImportObjectFlatEhd>();
                importObject.FlatsBti = new List<ImportObjectFlatBti>();
            }

            // Получение квартир

            // Квартиры ЕГРН
            List<string> buildingsEhdKadNumbers = importObjects.Where(x => x.EhdBuilding != null && x.EhdBuilding.ObjectId != null).Select(x => "'" + x.EhdBuilding.ObjectId + "'").ToList();

            if (buildingsEhdKadNumbers.Count > 0)
            {
                string andEhdFlatFilter = exactEhdFlatId.IsNotEmpty() ? $" and flat.emp_id = {exactEhdFlatId}" : "";

                commandText = string.Format($@"
                        select 
                            flat.EMP_ID as flat_id, 
                            ER.EMP_ID as flat_register_id, 
                            EE.emp_id as flat_egrp_id, 
                            l.emp_id as flat_location_id, 
                            ER.CADASTRAL_NUMBER_OKS as CADASTRAL_NUMBER_OKS
                        from ehd_register_q ER
                        join ehd_build_parcel_q flat
                            on flat.emp_id = ER.building_parcel_id
                            and flat.type = 'Помещение'
                        left join ehd_egrp_q EE
                            on EE.num_cadnum = flat.object_id 
                            and coalesce(EE.actual_id, 0) = 0
						left join ehd_location_q L
                            on L.building_parcel_id = flat.emp_id
                        where 
                            ER.assftp_cd = 'Жилое помещение' AND
                            ER.assftp1 = 'Квартира' AND
							coalesce(flat.actual_ehd, 0) = 0 AND
							ER.CADASTRAL_NUMBER_OKS IN ({String.Join(",", buildingsEhdKadNumbers)})
							{andEhdFlatFilter}");

                DbCommand commandGetEhdFlatList = DBMngr.Realty.GetSqlStringCommand(commandText);
                DataTable dtEhdFlatList = DBMngr.Realty.ExecuteDataSet(commandGetEhdFlatList).Tables[0];

                if (dtEhdFlatList.Rows.Count > 0)
                {
                    var flatIds = dtEhdFlatList.Rows.OfType<DataRow>().Select(dr => dr.Field<long>("flat_id")).ToList();
                    Dictionary<long, OMBuildParcel> flats = OMBuildParcel.Where(x => flatIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x);

                    var registerIds = dtEhdFlatList.Rows.OfType<DataRow>().Select(dr => dr.Field<long>("flat_register_id")).ToList();
                    Dictionary<long, OMRegister> registers = OMRegister.Where(x => registerIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x);

                    var egrpIds = dtEhdFlatList.Rows.OfType<DataRow>().Select(dr => dr.Field<long?>("flat_egrp_id")).ToList().Where(x => x.HasValue).Select(x => x.Value).ToList();
                    Dictionary<long, OMEgrp> egrps = egrpIds.Count > 0 ?
                        OMEgrp.Where(x => egrpIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x) :
                        new Dictionary<long, OMEgrp>();

                    var locationIds = dtEhdFlatList.Rows.OfType<DataRow>().Select(dr => dr.Field<long?>("flat_location_id")).ToList().Where(x => x.HasValue).Select(x => x.Value).ToList();
                    Dictionary<long, OMLocation> locations = locationIds.Count > 0 ?
                        OMLocation.Where(x => locationIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x) :
                        new Dictionary<long, OMLocation>();

                    foreach (DataRow rowFlatEhd in dtEhdFlatList.Rows)
                    {
                        long flatId = rowFlatEhd["flat_id"].ParseToLong();
                        long flatRegisterId = rowFlatEhd["flat_register_id"].ParseToLong();
                        long? flatEgrpId = rowFlatEhd["flat_egrp_id"].ParseToLongNullable();
                        long? flatLocationId = rowFlatEhd["flat_location_id"].ParseToLongNullable();

                        ImportObjectFlatEhd ehdFlat = new ImportObjectFlatEhd
                        {
                            KadNumOks = rowFlatEhd["CADASTRAL_NUMBER_OKS"].ToString(),

                            Flat = flats[flatId],
                            FlatRegister = registers[flatRegisterId],
                            FlatEgrp = flatEgrpId == null ? null : egrps[flatEgrpId.Value],
                            FlatLocation = flatLocationId == null ? null : locations[flatLocationId.Value],
                        };

                        ImportObjectBuilding importObject = importObjects.FirstOrDefault(x => x.EhdBuilding != null && x.EhdBuilding.ObjectId == ehdFlat.KadNumOks);

                        importObject.FlatsEhd.Add(ehdFlat);
                    }
                }
            }

            List<long> buildingBtiIds = importObjects.Where(x => x.BtiMainBuilding != null).Select(x => x.BtiMainBuilding.EmpId).ToList();

            if (buildingBtiIds.Count > 0)
            {
                string andBtiPremaseFilter = exactBtiFlatId.IsNotEmpty() ? $" and p.emp_id = {exactBtiFlatId}" : "";

                // Квартиры БТИ
                commandText = $@"
                        select 
							p.emp_id as flat_id, 
							f.emp_id as floor_id, 
							f.building_id as building_id
                        from bti_premase p
                        join bti_floor_q f 
                            on f.emp_id = p.floor_id 
                            and f.actual = 1
                        where
                            p.class_name = 'Жилые помещения' and
                            p.type_name = 'Квартира' and
							p.bit0 = 0 and
							f.building_id IN ({String.Join(",", buildingBtiIds)})
							{andBtiPremaseFilter}";

                DbCommand commandGetBtiFlatList = DBMngr.Realty.GetSqlStringCommand(commandText);
                DataTable dtBtiFlatList = DBMngr.Realty.ExecuteDataSet(commandGetBtiFlatList).Tables[0];

                var floorIds = dtBtiFlatList.Rows.OfType<DataRow>().Select(dr => dr.Field<long>("floor_id")).ToList().Distinct().ToList();
                Dictionary<long, OMFloor> floors = OMFloor.Where(x => floorIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x);

                var btiFlatIds = dtBtiFlatList.Rows.OfType<DataRow>().Select(dr => dr.Field<long>("flat_id")).ToList();
                Dictionary<long, OMPremase> btiFlats = OMPremase.Where(x => btiFlatIds.Contains(x.EmpId)).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x);

                foreach (DataRow rowFlatBti in dtBtiFlatList.Rows)
                {
                    long floorId = rowFlatBti["floor_id"].ParseToLong();
                    long flatId = rowFlatBti["flat_id"].ParseToLong();

                    ImportObjectFlatBti btiFlat = new ImportObjectFlatBti
                    {
                        BuildingId = rowFlatBti["building_id"].ParseToLong(),

                        Flat = btiFlats[flatId],
                        Floor = floors[floorId],
                    };

                    ImportObjectBuilding importObject = importObjects.FirstOrDefault(x => x.BtiMainBuilding != null && x.BtiMainBuilding.EmpId == btiFlat.BuildingId);

                    importObject.FlatsBti.Add(btiFlat);
                }
            }

            return importObjects;
        }

        /// <summary>
        /// Загрузки модели для всего здания, которая используется при инициирующей загрузке
        /// </summary>
        /// <param name="importModel"></param>
        public void ImportInitialModel(ImportObjectBuilding importModel)
        {
            Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] Загрузка начало: EHD: {importModel.FlatsEhd.Count}; BTI: {importModel.FlatsBti.Count}");

            bool isNew;

            // Загрузка квартир ЕГРН
            foreach (var flatEhd in importModel.FlatsEhd)
            {
                // Поиск соответствия в БТИ
                var flatBti = importModel.FlatsBti.FirstOrDefault(x => x.Flat.Kadastr == flatEhd.Flat.ObjectId
                        || flatEhd.FlatLocation != null && flatEhd.FlatLocation.Apartment != null && (flatEhd.FlatLocation.Apartment.Replace("Квартира", "").Trim() == x.Flat.Kvnom));

                if (flatBti != null)
                {
                    importModel.FlatsBti.Remove(flatBti);
                }

                ImportModelFlat(importModel.InsurBuilding, flatEhd, flatBti, out isNew);
            }

            Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] Перед загрузкой БТИ: EHD: {importModel.FlatsEhd.Count}; BTI: {importModel.FlatsBti.Count}");

            // Загрузка квартир БТИ
            foreach (var flatBti in importModel.FlatsBti)
            {
                ImportModelFlat(importModel.InsurBuilding, null, flatBti, out isNew);
            }
        }

        private OMFlat ImportModelFlat(ObjectModel.Insur.OMBuilding insurBuilding, ImportObjectFlatEhd flatEhd, ImportObjectFlatBti flatBti, out bool isNew)
        {
            ObjectModel.Insur.OMFlat existingFlat = null;

            if (flatEhd != null && flatEhd?.Flat != null)
            {
                existingFlat = OMFlat.Where(x => x.LinkInsurEgrn == flatEhd.Flat.EmpId).Select(x => x.AttributeSource).Execute().FirstOrDefault();

                // Так как EmpId для ЕГРН меняется, то необходимо проверить по кадастровому номеру
                if (existingFlat == null && flatEhd.Flat != null && flatEhd.Flat?.ObjectId != null && flatEhd.Flat.ObjectId.IsNotEmpty())
                {
                    existingFlat = OMFlat.Where(x => x.LinkObjectMkd == insurBuilding.EmpId && x.CadastrNum == flatEhd.Flat.ObjectId).Select(x => x.AttributeSource).Execute().FirstOrDefault();
                }

                //CIPJS-754 пытаемся найти квартиру по kvnom
                if (existingFlat == null && insurBuilding != null && flatEhd?.FlatLocation != null && flatEhd?.FlatLocation?.Apartment != null && flatEhd.FlatLocation.Apartment.IsNotEmpty())
                {
                    string kvnom = flatEhd.FlatLocation.Apartment.Replace("Квартира", "").Trim();

                    existingFlat = OMFlat.Where(x => x.LinkObjectMkd == insurBuilding.EmpId && x.Kvnom == kvnom).Select(x => x.AttributeSource).Execute().FirstOrDefault();
                }
            }

            if (existingFlat == null && flatBti != null && flatBti?.Flat != null)
            {
                existingFlat = OMFlat.Where(x => x.LinkBtiFlat == flatBti.Flat.EmpId).Select(x => x.AttributeSource).Execute().FirstOrDefault();

                if (existingFlat == null && flatBti.Flat != null && flatBti?.Flat?.Kadastr != null && flatBti.Flat.Kadastr.IsNotEmpty())
                {
                    existingFlat = OMFlat.Where(x => x.LinkObjectMkd == insurBuilding.EmpId && x.CadastrNum == flatBti.Flat.Kadastr).Select(x => x.AttributeSource).Execute().FirstOrDefault();
                }

                //CIPJS-754 пытаемся найти квартиру по kvnom
                if (existingFlat == null && flatBti.Flat != null && flatBti?.Flat?.Kvnom != null && flatBti.Flat.Kvnom.IsNotEmpty())
                {
                    string kvnom = flatBti.Flat.Kvnom.Replace("Квартира", "").Trim();

                    existingFlat = OMFlat.Where(x => x.LinkObjectMkd == insurBuilding.EmpId && x.Kvnom == kvnom).Select(x => x.AttributeSource).Execute().FirstOrDefault();
                }
            }

            Dictionary<long, Object> objects = new Dictionary<long, object>();

            objects.Add(OMBuilding.GetRegisterId(), insurBuilding);

            if (flatBti != null)
            {
                objects.Add(OMPremase.GetRegisterId(), flatBti?.Flat);
                objects.Add(OMFloor.GetRegisterId(), flatBti?.Floor);
            }
            else
            {
                objects.Add(OMPremase.GetRegisterId(), null);
                objects.Add(OMFloor.GetRegisterId(), null);
            }

            if (flatEhd != null)
            {
                objects.Add(OMBuildParcel.GetRegisterId(), flatEhd?.Flat);
                objects.Add(OMLocation.GetRegisterId(), flatEhd?.FlatLocation);
                objects.Add(OMRegister.GetRegisterId(), flatEhd?.FlatRegister);
                objects.Add(OMEgrp.GetRegisterId(), flatEhd?.FlatEgrp);
            }
            else
            {
                objects.Add(OMBuildParcel.GetRegisterId(), null);
                objects.Add(OMLocation.GetRegisterId(), null);
                objects.Add(OMRegister.GetRegisterId(), null);
                objects.Add(OMEgrp.GetRegisterId(), null);
            }

            string mapLogAttribute;

            var flat = BuildingService.Map<ObjectModel.Insur.OMFlat>("InsurObjectMapFlat", ObjectModel.Insur.OMFlat.GetRegisterId(), objects, existingFlat != null ? existingFlat?.AttributeSource : null, out mapLogAttribute);

            isNew = true;

            if (existingFlat != null)
            {
                flat.EmpId = existingFlat.EmpId;
                isNew = false;
            }

            if (flatEhd != null)
            {
                flat.LinkInsurEgrn = flatEhd?.Flat?.EmpId;
            }

            if (flatBti != null)
            {
                flat.LinkBtiFlat = flatBti?.Flat?.EmpId;
            }

            flat.GuidFiasMkd = insurBuilding?.GuidFiasMkd;

            flat.LinkObjectMkd = insurBuilding?.EmpId;

            flat.AttributeSource = mapLogAttribute;

            flat.FlagInsur = insurBuilding?.FlagInsur;

            // Дополнительная обработка
            if (flat.PropertyChangedList.Contains("Kvnom"))
            {
                flat.Kvnom = flat?.Kvnom?.Replace("Квартира", "")?.Trim();
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                flat.Save();
                ts.Complete();
            }

            return flat;
        }
    }
}