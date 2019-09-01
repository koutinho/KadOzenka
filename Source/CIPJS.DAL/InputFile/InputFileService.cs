using CIPJS.DAL.Fsp;
using CIPJS.DAL.StrahNach;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
namespace CIPJS.DAL.InputFile
{
    public class InputFileService
    {
        private readonly FspService _fspService;
        private readonly StrahNachService _strahNachService;

        private readonly object lockObject = new object();

        public InputFileService()
        {
            _fspService = new FspService();
            _strahNachService = new StrahNachService();
        }

        public void StartIdentifyProcess(OMInputFile inputFile, bool needProcess = false)
        {
            if (inputFile == null)
            {
                throw new Exception("Не передан обязательный параметр файл загрузки");
            }

            if (inputFile.TypeFile_Code != TypeFile.Strah)
            {
                throw new Exception("Передан некорректный тип файла. Процесс идентифкации поддерживает только файлы зачислений.");
            }

            if (needProcess && inputFile.Status_Code != UFKFileProcessingStatus.Loaded)
            {
                throw new Exception("Обработать файл возможно только в статусе \"Загружен\"");
            }
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                OMFilePlatIdentifyLog log = new OMFilePlatIdentifyLog();
                log.InputFileId = inputFile.EmpId;
                log.NeedProcess = needProcess;
                log.Status_Code = IdentifyPlatStatus.Prepare;
                log.Save();

                LongProcessManager.AddTaskToQueue("InputFilePlatIdentifyProcess", OMFilePlatIdentifyLog.GetRegisterId(), log.EmpId);

                inputFile.Status_Code = UFKFileProcessingStatus.LinkedBankInProcess;
                inputFile.Save();

                ts.Complete();
            }
        }

        /// <summary>
        /// Запускает процесс обработки
        /// </summary>
        /// <param name="inputFileId">Идентификатор файла загрузки</param>
        public void StartProcess(OMInputFile inputFile)
        {
            if (inputFile == null)
            {
                throw new Exception("Не удалось определить файл загрузки");
            }

            if (inputFile.Status_Code != UFKFileProcessingStatus.Loaded && inputFile.Status_Code != UFKFileProcessingStatus.LinkedBankCompletely && inputFile.Status_Code != UFKFileProcessingStatus.LinkedBankPartially)
            {
                throw new Exception("Обработать файл возможно только в статусе \"Загружен\", \"Полностью связан с банком\", \"Частично связан с банком\"");
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                OMFileProcessLog processLog = new OMFileProcessLog();
                processLog.InputFileId = inputFile.EmpId;
                processLog.Status_Code = FileProcessStatus.Prepare;
                processLog.UserId = SRDSession.GetCurrentUserId();
                processLog.Save();

                LongProcessManager.AddTaskToQueue("InputFileProcess", OMFileProcessLog.GetRegisterId(), processLog.EmpId);

                inputFile.Status_Code = UFKFileProcessingStatus.InProcess;
                inputFile.Save();

                ts.Complete();
            }
        }

        /// <summary>
        /// Обработка данных файла загрузки
        /// 1. Процесс идентификации каждого зачисления (поиск ФСП, на котором это зачисление должно быть отражено)
        /// 2. Учет начисления/зачисления на ФСП
        /// </summary>
        /// <param name="inputFileProcessLogId">Идентификатор лога процесса загрузки</param>
        public void Process(long inputFileProcessLogId, OMQueue processQueue)
        {
            OMFileProcessLog processLog = OMFileProcessLog.Where(x => x.EmpId == inputFileProcessLogId).SelectAll().ExecuteFirstOrDefault();

            if (processLog == null)
            {
                throw new Exception($"Не удалось определить журнал загрузки по идентификатору {inputFileProcessLogId}");
            }

            if (!processLog.InputFileId.HasValue)
            {
                throw new Exception($"Не заполнен идентификатор загружаемого файла");
            }

            OMInputFile inputFile = OMInputFile.Where(x => x.EmpId == processLog.InputFileId.Value).SelectAll().Execute().FirstOrDefault();

            if (inputFile == null)
            {
                throw new Exception("Обработка прервана. Не удалось определить файл загрузки по идентификатору");
            }

            if (inputFile.Status_Code != UFKFileProcessingStatus.InProcess)
            {
                throw new Exception("Обработка прервана. Файл должен находиться в статусе \"Обрабатывается\"");
            }

            if (!inputFile.PeriodRegDate.HasValue)
            {
                throw new Exception("Обработка прервана. Не указан период учета для файла загрузки");
            }

            UFKFileProcessingStatus processStatus = UFKFileProcessingStatus.ProcessedCompletely;

            switch (inputFile.TypeFile_Code)
            {
                case TypeFile.Nach:
                    ProcessNach(processLog.InputFileId.Value, inputFile.PeriodRegDate.Value, processLog, processQueue);
                    processStatus = IsNachFileProcessedCompletely(processLog.InputFileId.Value) ? UFKFileProcessingStatus.ProcessedCompletely : UFKFileProcessingStatus.ProcessedPartially;
                    break;
                case TypeFile.Strah:
                    ProcessStrah(processLog.InputFileId.Value, inputFile.PeriodRegDate.Value, processLog);
                    processStatus = IsStrahFileProcessedCompletely(processLog.InputFileId.Value) ? UFKFileProcessingStatus.ProcessedCompletely : UFKFileProcessingStatus.ProcessedPartially;
                    break;
                    //default:
                    //    throw new Exception("Обработка прервана. Неподдерживаемый тип файла.");
            }

            inputFile.Status_Code = processStatus;
            inputFile.Save();
        }

        /// <summary>
        /// Обработка данных файла загрузки для начислений
        /// </summary>
        /// <param name="inputFileId">Идентификатор файла загрузки</param>
        /// <param name="periodRegDate">Период учета</param>
        private void ProcessNach(long inputFileId, DateTime periodRegDate, OMFileProcessLog processLog, OMQueue processQueue)
        {
            if (processLog == null)
            {
                throw new Exception($"Обработка невозможна. Не удалось определить журнал загрузки");
            }

            processLog.TotalCount = OMInputNach
                    .Where(x => x.LinkIdFile == inputFileId
                        && (x.StatusIdentif_Code == StatusIdentifikacii.None
                        || x.StatusIdentif_Code == StatusIdentifikacii.NotIdentified)).GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToLong();

            long processedCount = 0;
            long errorCount = 0;
            long objectErrorCount = 0;
            long processedFspCont = 0;
            long totalFspCount = 0;
            processLog.Status_Code = FileProcessStatus.CreateBindFsp;
            processLog.StartDate = DateTime.Now;
            processLog.Save();

            /* Получение из БД всех UNOM'ов */
            HashSet<long?> uniqueUnoms = new HashSet<long?>(OMInputNach.Where(x => x.LinkIdFile == inputFileId)
                    .Select(x => x.Unom)
                    .SetDistinct()
                    .Execute()
                    .Where(x => x.Unom.HasValue)
                    .Select(x => x.Unom));

            /* Получение из БД всех Building'ов соответствующих перечислению uniqueUnoms */
            Dictionary<long, OMBuilding> cacheBuildings = uniqueUnoms.Count > 0 ?
                OMBuilding.Where(w => uniqueUnoms.Contains(w.Unom))
                    .Select(s => s.Unom)
                    .Select(s => s.DistrictId)
                    .Execute()
                    .ToDictionary(key => key.EmpId) :
                new Dictionary<long, OMBuilding>();

            List<long> excludedNachIds = new List<long>();

            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 10
            };

            ConcurrentBag<string> errors = new ConcurrentBag<string>();

            Stopwatch sw = Stopwatch.StartNew();

            while (true)
            {
                QSQuery<OMInputNach> inputNachQuery = OMInputNach
                    .Where(x => x.LinkIdFile == inputFileId
                        && (x.StatusIdentif_Code == StatusIdentifikacii.None
                        || x.StatusIdentif_Code == StatusIdentifikacii.NotIdentified))
                    .OrderBy(x => x.EmpId)
                    .SetPackageSize(1000)
                    .Select(x => x.FspId)
                    .Select(x => x.Kodpl)
                    .Select(x => x.PeriodRegDate)
                    .Select(x => x.Ls)
                    .Select(x => x.Opl)
                    .Select(x => x.Unom)
                    .Select(x => x.Kvnom)
                    .Select(x => x.FlatStatusId)
                    .Select(x => x.FlatTypeId)
                    .Select(x => x.DistrictId)
                    .Select(x => x.SumNach)
                    .Select(x => x.Fopl);

                if (excludedNachIds.Count > 0)
                {
                    inputNachQuery.And(x => !excludedNachIds.Contains(x.EmpId));
                }

                List<OMInputNach> inputNachList = inputNachQuery.Execute();

                if (inputNachList.Count == 0)
                {
                    break;
                }

                Parallel.ForEach(inputNachList, options, inputNach =>
                {
                    if (!inputNach.FspId.HasValue)
                    {
                        List<string> setErrors;
                        _fspService.SetNachFsp(inputNach, out setErrors, cacheBuildings);
                        if (setErrors != null && setErrors.Count > 0)
                        {
                            //CIPJS-674 закладываемся на том, что может быть только два варианта ошибок:
                            //1. ФСП не был создан
                            //2. ФСП создан, но объект не проставлен
                            if (inputNach.FspId.HasValue)
                            {
                                Interlocked.Increment(ref objectErrorCount);
                            }
                            else
                            {
                                excludedNachIds.Add(inputNach.EmpId);
                                Interlocked.Increment(ref errorCount);
                            }

                            foreach (string error in setErrors)
                            {
                                errors.Add(error);
                            }
                        }
                    }

                    if (inputNach.FspId.HasValue)
                    {
                        Interlocked.Increment(ref totalFspCount);
                        inputNach.StatusIdentif_Code = StatusIdentifikacii.Identified;
                        inputNach.LoadStatus_Code = LoadStatus.Processed;
                        inputNach.Save();
                    }

                    Interlocked.Increment(ref processedCount);
                    if (processedCount % 100 == 0)
                    {
                        lock (lockObject)
                        {
                            // Сокращаем количество сохранений в БД для оптимизации
                            processLog.Save();

                            processLog.ProcessedCount = processedCount;
                            processLog.ObjectErrorCount = objectErrorCount;
                            processLog.ErrorCount = errorCount;
                            processLog.TotalFspCount = totalFspCount;

                            processQueue.Message = $"timingCreateFspCount: {_fspService.timingCreateFspCount};\n" +
                                $"timingCreateFspFindFspObject: {_fspService.timingCreateFspFindFspObject};\n" +
                                $"timingCreateFspFindFlat: {_fspService.timingCreateFspFindFlat};\n" +
                                $"timingCreateFspCreateFlat: {_fspService.timingCreateFspCreateFlat};\n" +
                                $"timingCreateFspGetFspNumber: {_fspService.timingCreateFspGetFspNumber};\n";
                            processQueue.Save();
                        }
                    }
                });
            }

            sw.Stop();
            processQueue.Message = $"Первый этап: {sw.Elapsed.Ticks}\n";
            processQueue.Save();
            sw.Restart();

            processLog.ProcessedCount = processedCount;
            processLog.ObjectErrorCount = objectErrorCount;
            processLog.ErrorCount = errorCount;
            processLog.TotalFspCount = totalFspCount;
            processLog.ErrorLog = string.Join(Environment.NewLine, errors.Select((x, index) => $"{index + 1}. {x}"));
            processLog.Status_Code = FileProcessStatus.RecalcFsp;
            processLog.Save();

            int package = 0;

            // CIPJS-914: Получаем все ФСП связанные с данным пакетом через начисления. 
            var accountFspList = _fspService.GetNachFspOfPackage(inputFileId);

            while (true)
            {
                List<long> fspIdPackage = accountFspList.Skip(1000 * package).Take(1000).ToList();
                if (fspIdPackage.Count == 0)
                {
                    break;
                }

                /* Извлекаем из БД список тарифов */
                OMTariff tariff = OMTariff.Where(x => x.DateBegin <= periodRegDate)
                                 .OrderByDescending(x => x.DateBegin)
                                 .SetPackageIndex(0)
                                 .SetPackageSize(1)
                                 .SelectAll()
                                 .ExecuteFirstOrDefault();

                string message = processQueue.Message;

                // Выполняется процедура «Учет начисления/ зачисления на ФСП»
                // Так как accountFspList содержит только уникальные fspId, то обработку можно запускать параллельно
                Parallel.ForEach(fspIdPackage, options, fspId =>
                {
                    OMInputNach strahNach = null;
                    List<OMBalance> balances = null;
                    OMBalance balance = null;
                    List<OMInputPlat> inputPlats = null;
                    List<OMBalance> calcBalanceList = null;

                    _fspService.AccountFsp(fspId, periodRegDate, balance, inputPlats, strahNach, balances, tariff: tariff);

                    //CIPJS-69 6)Запуск процедуры «Перестроение остатков  и оборотов» 
                    //для самого СТАРОГО периода по PERIOD_REG_DATE для всех обрабатываемых операций по начислению и зачислению
                    _fspService.CalcBalanceSumFromPeriod(fspId, periodRegDate, calcBalanceList);

                    Interlocked.Increment(ref processedFspCont);

                    if (processedFspCont % 100 == 0)
                    {
                        lock (lockObject)
                        {
                            processQueue.Message = message + $"Второй этап: {sw.Elapsed.Ticks}; циклов: {processedFspCont}\n";
                            processQueue.Save();


                            processLog.ProcessedFspCont = processedFspCont;
                            // Сокращаем количество сохранений в БД для оптимизации
                            processLog.Save();
                        }
                    }
                });

                package++;
            }

            sw.Stop();
            processQueue.Message += $"Второй этап: {sw.Elapsed.Ticks}";
            processQueue.Save();

            processLog.ProcessedFspCont = processedFspCont;
            processLog.EndDate = DateTime.Now;
            processLog.Status_Code = FileProcessStatus.Finished;
            processLog.Save();
        }

        /// <summary>
        /// Обработка данных файла загрузки для зачислений
        /// </summary>
        /// <param name="inputFileId">Идентификатор файла загрузки</param>
        /// <param name="periodRegDate">Период учета</param>
        private void ProcessStrah(long inputFileId, DateTime periodRegDate, OMFileProcessLog processLog)
        {
            processLog.TotalCount = OMInputNach
                    .Where(x => x.LinkIdFile == inputFileId
                        && x.StatusIdentif_Code == StatusIdentifikacii.PartiallyIdentified).GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToLong();
            processLog.ProcessedCount = 0;
            processLog.ErrorCount = 0;
            processLog.ObjectErrorCount = 0;
            processLog.ProcessedFspCont = 0;
            processLog.TotalFspCount = 0;
            processLog.Status_Code = FileProcessStatus.CreateBindFsp;
            processLog.StartDate = DateTime.Now;
            processLog.Save();

            List<long> excludedPlatIds = new List<long>();
            List<string> errors = new List<string>();

            while (true)
            {
                //CIPJS-241 Проверить еще раз АЛГОРИТМ - обрабатываем только зачисления в статусе "Частично-идентифицировано".
                QSQuery<OMInputPlat> inputPlatQuery = OMInputPlat
                    .Where(x => x.LinkIdFile == inputFileId
                        && x.StatusIdentif_Code == StatusIdentifikacii.PartiallyIdentified)
                    .OrderBy(x => x.EmpId)
                    .SetPackageSize(1000)
                    .SelectAll();

                if (excludedPlatIds.Count > 0)
                {
                    inputPlatQuery.And(x => !excludedPlatIds.Contains(x.EmpId));
                }

                List<OMInputPlat> inputPlatList = inputPlatQuery.Execute();

                if (inputPlatList.Count == 0)
                {
                    break;
                }

                foreach (OMInputPlat inputPlat in inputPlatList)
                {
                    //Процесс идентификации каждого зачисления (поиск ФСП, на котором это зачисление должно быть отражено)
                    if (!inputPlat.FspId.HasValue)
                    {
                        List<string> setErrors;
                        _fspService.SetPlatFsp(inputPlat, out setErrors);
                        if (setErrors != null && setErrors.Count > 0)
                        {
                            //CIPJS-674 закладываемся на том, что может быть только два варианта ошибок:
                            //1. ФСП не был создан
                            //2. ФСП создан, но объект не проставлен
                            if (inputPlat.FspId.HasValue)
                            {
                                processLog.ObjectErrorCount++;
                            }
                            else
                            {
                                excludedPlatIds.Add(inputPlat.EmpId);
                                processLog.ErrorCount++;
                            }
                            errors.AddRange(setErrors);
                        }
                    }

                    if (inputPlat.FspId.HasValue)
                    {
                        processLog.TotalFspCount++;
                        inputPlat.StatusIdentif_Code = StatusIdentifikacii.Identified;
                        inputPlat.LoadStatus_Code = LoadStatus.Processed;
                        inputPlat.Save();
                    }

                    processLog.ProcessedCount++;
                    processLog.Save();
                }
            }

            processLog.ErrorLog += string.Join(Environment.NewLine, errors.Select((x, index) => $"{index + 1}. {x}"));
            processLog.Status_Code = FileProcessStatus.RecalcFsp;
            processLog.Save();

            // CIPJS-914: Получаем все ФСП связанные с данным пакетом через зачисления. 
            var accountFspList = _fspService.GetStrahFspOfPackage(inputFileId);

            //Выполняется процедура «Учет начисления/ зачисления на ФСП»
            foreach (long fspId in accountFspList)
            {
                _fspService.AccountFsp(fspId, periodRegDate);

                //CIPJS-69 6)Запуск процедуры «Перестроение остатков  и оборотов» 
                //для самого СТАРОГО периода по PERIOD_REG_DATE для всех обрабатываемых операций по начислению и зачислению
                _fspService.CalcBalanceSumFromPeriod(fspId, periodRegDate);

                processLog.ProcessedFspCont++;
                processLog.Save();
            }

            processLog.EndDate = DateTime.Now;
            processLog.Status_Code = FileProcessStatus.Finished;
            processLog.Save();
        }

        /// <summary>
        /// Удаление файла загрузки
        /// </summary>
        /// <param name="inputFileId">Идентификатор файла загрузки</param>
        public void Delete(long inputFileId)
        {
            OMInputFile inputFile = OMInputFile.Where(x => x.EmpId == inputFileId).SelectAll().Execute().FirstOrDefault();

            if (inputFile == null)
            {
                throw new Exception("Не удалось определить файл загрузки по идентификатору");
            }

            if (inputFile.Status_Code != UFKFileProcessingStatus.Loaded)
            {
                throw new Exception("Удалить файл возможно только в статусе \"Загружен\"");
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                switch (inputFile.TypeFile_Code)
                {
                    case TypeFile.Nach:
                        DeleteNach(inputFileId);
                        break;
                    case TypeFile.Strah:
                        DeleteStrah(inputFileId);
                        break;
                    default:
                        throw new Exception("Попытка удалить неподдерживаемый тип файла");
                }

                inputFile.Destroy();
                ts.Complete();
            }
        }

        private void DeleteNach(long inputFileId)
        {
            List<OMInputNach> inputNachList = OMInputNach.Where(x => x.LinkIdFile == inputFileId).Execute();

            foreach (OMInputNach inputNach in inputNachList)
            {
                inputNach.Destroy();
            }
        }

        private void DeleteStrah(long inputFileId)
        {
            List<OMInputPlat> inputPlatList = OMInputPlat.Where(x => x.LinkIdFile == inputFileId).Execute();

            foreach (OMInputPlat inputPlat in inputPlatList)
            {
                inputPlat.Destroy();
            }
        }

        public bool IsStrahFileIdentifiedcCompletely(long inputFileId)
        {
            return OMInputPlat.Where(x => x.LinkIdFile == inputFileId && x.SumOpl > 0 && (x.StatusIdentif_Code == StatusIdentifikacii.None
                    || x.StatusIdentif_Code == StatusIdentifikacii.NotIdentified)).GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt() == 0;
        }

        public bool IsStrahFileProcessedCompletely(long inputFileId)
        {
            return OMInputPlat.Where(x => x.LinkIdFile == inputFileId && x.SumOpl > 0 && (x.StatusIdentif_Code == StatusIdentifikacii.None
                    || x.StatusIdentif_Code == StatusIdentifikacii.NotIdentified
                    || x.StatusIdentif_Code == StatusIdentifikacii.PartiallyIdentified)).GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt() == 0;
        }

        public bool IsNachFileProcessedCompletely(long inputFileId)
        {
            return OMInputNach.Where(x => x.LinkIdFile == inputFileId && x.SumNach > 0 && (x.StatusIdentif_Code == StatusIdentifikacii.None
                    || x.StatusIdentif_Code == StatusIdentifikacii.NotIdentified)).GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt() == 0;
        }

        /// <summary>
        /// CIPJS-831 Обновление статистических данных мфц.
        /// </summary>
        /// <param name="inputFilePackageIds"></param>
        /// <returns></returns>
        public bool UpdateMfcDataStatistics(long[] inputFilePackageIds)
        {

            ParallelOptions parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 10
            };
            Parallel.ForEach(inputFilePackageIds, parallelOptions, id =>
            {
                OMInputFilePackage inputFilePackage = OMInputFilePackage
                            .Where(x => x.Id == id)
                            .Select(x => x.Id)
                            .Select(x => x.PeriodRegDate)
                            .Select(x => x.OkrugId)
                            .ExecuteFirstOrDefault();

                if (inputFilePackage != null)
                {
                    if (inputFilePackage.PeriodRegDate.HasValue && inputFilePackage?.OkrugId != null)
                    {

                        string sql = $@"SELECT public.fill_input_file_package(
	                                    {inputFilePackage.Id},	
	                                    {DBMngr.Realty.FormatDate(inputFilePackage.PeriodRegDate.Value)}
                                        );";

                        DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
                        DBMngr.Realty.ExecuteNonQuery(command);

                        sql = $@"
with post as (
	select sp.kod_post||'' cod_post,
    	row_number() over (order by sp.date_start desc, sp.status_new desc, sp.emp_id desc) rn
    from insur_okrug io
    join insur_strah_post sp on sp.link_company = io.insurance_company_id
	where io.id = {inputFilePackage.OkrugId} and sp.date_start <= {DBMngr.Realty.FormatDate(inputFilePackage.PeriodRegDate.Value)}
),
bank as (
    select count(1) count_str, sum(case when ibp.flag_vozvr = 1 then 0 - ibp.sum_by_code else ibp.sum_by_code end) opl,
     iif.district_id, isb.cod_post
    from insur_district ds
    join insur_input_file iif on iif.district_id = ds.id and iif.period_reg_date = {DBMngr.Realty.FormatDate(inputFilePackage.PeriodRegDate.Value)}
    join insur_svod_bank isb on isb.link_id_file = iif.emp_id
    join insur_bank_plat ibp on ibp.link_id_file = iif.emp_id
    where ds.okrug_id = {inputFilePackage.OkrugId}
    group by iif.district_id, isb.cod_post
),
mfc_plats as (
    select count(1) count_str, sum(iip.sum_opl) opl, iif.district_id, iip.status_identif_code
    from insur_district ds
    join insur_input_file iif on iif.district_id = ds.id and iif.period_reg_date = {DBMngr.Realty.FormatDate(inputFilePackage.PeriodRegDate.Value)}
    join insur_input_plat iip on iip.link_id_file = iif.emp_id
    where ds.okrug_id = {inputFilePackage.OkrugId}
    group by iif.district_id, iip.status_identif_code
)
select insurd.id as ""DistrictId"",
insurd.name as ""DistrictName"",
insurd.code as ""DistrictCode"",
(select sum(mfc.count_str) from mfc_plats mfc where mfc.district_id = insurd.id) as ""CountStrMfc"",
(select sum(mfc.opl) from mfc_plats mfc where mfc.district_id = insurd.id) as ""SumOpl"",
(select sum(mfc.count_str) from mfc_plats mfc where mfc.district_id = insurd.id and mfc.status_identif_code = {(long)StatusIdentifikacii.NotConfirmedByBank}) as ""CountStrMfcError"",
(select sum(mfc.opl) from mfc_plats mfc where mfc.district_id = insurd.id and mfc.status_identif_code = {(long)StatusIdentifikacii.NotConfirmedByBank}) as ""SumOplError"",
(select sum(bnk.count_str) from bank bnk where bnk.district_id = insurd.id) as ""CountStrSvodBank"",
(select sum(bnk.opl) from bank bnk where bnk.district_id = insurd.id) as ""SumOplSvodBank""
from insur_district insurd
left
join post post1 on post1.rn = 1
left
join post post2 on post2.rn = 2
where insurd.okrug_id = {inputFilePackage.OkrugId}
                group by insurd.id, insurd.name, insurd.code, post1.cod_post, post2.cod_post
                order by insurd.code
";

                        command = DBMngr.Realty.GetSqlStringCommand(sql);
                        DataTable dt = DBMngr.Realty.ExecuteDataSet(command).Tables[0];
                        
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                long countStrMfc = row["CountStrMfc"].ParseToLong();
                                long countStrSvodBank = row["CountStrSvodBank"].ParseToLong();
                                long countStrMfcError = row["CountStrMfcError"].ParseToLong();
                                decimal? sumOpl = row["SumOpl"] != DBNull.Value ? (decimal?)row["SumOpl"].ParseToDecimal() : null;
                                decimal? sumOplSvodBank = row["SumOplSvodBank"] != DBNull.Value ? (decimal?)row["SumOplSvodBank"].ParseToDecimal() : null;
                                decimal? sumOplError = row["SumOplError"] != DBNull.Value ? (decimal?)row["SumOplError"].ParseToDecimal() : null;
                                var CountStrDifference = Math.Abs(countStrMfc - countStrSvodBank - countStrMfcError);
                                var SumOplDifference = Math.Abs((sumOpl ?? 0) - (sumOplSvodBank ?? 0) - (sumOplError ?? 0));

                                if(countStrMfcError > 0 || sumOplError > 0 || CountStrDifference > 0 || SumOplDifference > 0)
                                {
                                    inputFilePackage.PackageProcessedCompletely = null;
                                    inputFilePackage.Save();
                                    break;
                                }
                            }
                        }

                    }
                }
            });
            return true;
        }
    }
}
