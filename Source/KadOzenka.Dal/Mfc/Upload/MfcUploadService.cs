using CIPJS.DAL.FileStorage;
using CIPJS.DAL.Fsp;
using Core.ErrorManagment;
using Core.FastDBF;
using Core.Main.FileStorages;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using Npgsql;
using ObjectModel.Directory;
using ObjectModel.Insur;
using PostgreSQLCopyHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace CIPJS.DAL.Mfc.Upload
{
    public class MfcUploadService
    {
        private readonly FspService _fspService;
        private readonly FileStorageService _fileStorageService;

        public MfcUploadService()
        {
            _fspService = new FspService();
            _fileStorageService = new FileStorageService();
        }

        public static string NachFilePrefix
        {
            get
            {
                return "nach";
            }
        }
        
        public static string StrahFilePrefix
        {
            get
            {
                return "strah";
            }
        }

        private const string _oplZipRegex = @"^opl_(\d{4})-(\d{2}).*";
        private const string _zelAOOplZipRegex = @"^opl(\d{3})_(\d{4})_(\d{4})-(\d{2}).*";
        private const string _oplFileRegex = @"^P(\d{3})(\d{2})(\d{2})\.001$";
        private const string _districtFileRegexFormat = @"^rosno_{0}_(\d{{4}})_(\d{{4}}-\d{{2}}).*";
        private string _mfcDataFileRegex = string.Format(_districtFileRegexFormat, @"(\d{3})");
        private string _nachDataFileRegex = string.Format(@"^{0}(\d{{3}}).*$", NachFilePrefix);
        private string _strahDataFileRegex = string.Format(@"^{0}(\d{{3}}).*$", StrahFilePrefix);
        private string _commonNachDataFileRegex = string.Format(@"^{0}.dbf$", NachFilePrefix);
        private string _commonStrahDataFileRegex = string.Format(@"^spass_{0}.dbf$", StrahFilePrefix);

        private List<OMOkrug> _okrugCache;
        private List<OMOkrug> Okrugs
        {
            get
            {
                if (_okrugCache == null)
                {
                    _okrugCache = OMOkrug.Where(x => true).SelectAll().Execute();
                }

                return _okrugCache;
            }
        }
        
        private List<OMDistrict> _districtCache;
        private List<OMDistrict> Districts
        {
            get
            {
                if (_districtCache == null)
                {
                    _districtCache = OMDistrict.Where(x => true).SelectAll().Execute();
                }

                return _districtCache;
            }
        }

        private Dictionary<long, long> _okrugFilePackage = new Dictionary<long, long>();

        /// <summary>
        /// Директория для создания обработки файлов архива из МФЦ
        /// </summary>
        private string MfcProcessFolder
        {
            get
            {
				return FileStorageManager.GetPathForStorage("MfcProcessFolder");
			}
        }

        private string MappingConfigurationFolder
        {
            get
            {
                return @"\Mfc\Upload";
            }
        }

        private int MfcMaxDegreeOfParallelism
        {
            get
            {
                string value = ConfigurationManager.AppSettings["MfcMaxDegreeOfParallelism"];

                if (value.IsNullOrEmpty() || value.ParseToInt() == 0)
                {
                    return 4;
                }
                return value.ParseToInt();
            }
        }

        private bool MfcPlatNeedIdentify
        {
            get
            {
                string value = ConfigurationManager.AppSettings["MfcPlatNeedIdentify"];

                if (value.IsNullOrEmpty())
                {
                    return false;
                }
                return value.ParseToBoolean();
            }
        }

        private bool MfcUploadSetCriteria
        {
            get
            {
                string value = ConfigurationManager.AppSettings["MfcUploadSetCriteria"];

                if (value.IsNullOrEmpty())
                {
                    return false;
                }
                return value.ParseToBoolean();
            }
        }

        private bool MfcUploadSetCriteriaExtended
        {
            get
            {
                string value = ConfigurationManager.AppSettings["MfcUploadSetCriteriaExtended"];

                if (value.IsNullOrEmpty())
                {
                    return false;
                }
                return value.ParseToBoolean();
            }
        }

        private Dictionary<string, DataTable> _referenceCache = new Dictionary<string, DataTable>();

        /// <summary>
        /// Загрузка файлов МФЦ
        /// </summary>
        /// <param name="logFileId">Идентификатор записи файла логов</param>
        /// <returns>Список загруженных пакетов</returns>
        public List<long?> Load(long logFileId)
        {
            OMLogFile logFile = OMLogFile.Where(x => x.EmpId == logFileId)
                .SelectAll()
                .Execute()
                .FirstOrDefault();

            if (logFile == null || !logFile.FileStorageId.HasValue)
            {
                throw new Exception("Не удалось определить файл для загрузки");
            }

            MfcUploadTraceData traceData = new MfcUploadTraceData(logFile.EmpId);

            try
            {
                
                OMFileStorage file = _fileStorageService.Get(logFile.FileStorageId.Value);

                if (!file.PeriodRegDate.HasValue)
                {
                    throw new Exception("Для загружаемых файлов не указан период учета");
                }

                //распаковываем и загружаем в бд
                Load(file, traceData);

                traceData.ProcessEndDate = DateTime.Now;
                traceData.Status = MfcUploadFileStatus.Finished;
                traceData.SaveLogFile();

                return traceData.InputPackageIds;
            }
            catch (Exception ex)
            {
                traceData.Errors.Add(ex.Message);
                traceData.ProcessEndDate = DateTime.Now;
                traceData.SaveLogFile();

                throw;
            }
        }

        /// <summary>
        /// Распаковка и загрузка в БД
        /// </summary>
        /// <param name="file">Файл из хранилища</param>
        /// <param name="traceData">Лог загрузки</param>
        private void Load(OMFileStorage file, MfcUploadTraceData traceData)
        {
            if (traceData == null)
            {
                throw new Exception("Не удалось определить журнал трассировки");
            }

            traceData.Status = MfcUploadFileStatus.UnpackageFiles;
            traceData.SaveLogFile();

            ////список загружаемых файлов
            List<OMInputFile> inputFileList = new List<OMInputFile>();

            //TODO возможно рекурсивный алгоритм с распаковкой и получением файлов заменить рекурсивную распаковку и 
            //распаковываем все файлы в директорию МФЦ
            //проверяем был ли файл уже загружен и определяем список файлов, которые должны быть загружены
            ExtractRecursive(inputFileList, file, file.PeriodRegDate.Value, traceData);

            //CIPJS-280 Один поток - один файл. Количество потоков,
            //которое будет использовано для загрузки должно быть вынесено в конфигурационный файл
            List<Action> loadActions = new List<Action>();

            #region только не загруженные и оригинальные файлы
            foreach (OMInputFile inputFile in inputFileList.Where(x => !x.ParentId.HasValue && x.Status_Code == UFKFileProcessingStatus.None))
            {
                loadActions.Add(new Action(() => { LoadToDb(inputFile); }));
            }
            #endregion

            Parallel.Invoke(new ParallelOptions() { MaxDegreeOfParallelism = MfcMaxDegreeOfParallelism }, loadActions.ToArray());

            loadActions.Clear();

            #region только не загруженные дочерние файлы
            foreach (OMInputFile inputFile in inputFileList.Where(x => x.ParentId.HasValue && x.Status_Code == UFKFileProcessingStatus.None))
            {
                loadActions.Add(new Action(() => { LoadToDb(inputFile); }));
            }
            
            Parallel.Invoke(new ParallelOptions() { MaxDegreeOfParallelism = MfcMaxDegreeOfParallelism }, loadActions.ToArray());
            #endregion

            traceData.Status = MfcUploadFileStatus.DbSave;
            traceData.SaveLogFile();

            if (MfcUploadSetCriteria)
            {
                traceData.Status = MfcUploadFileStatus.CriteriaSet;
                traceData.SaveLogFile();

                List<long> nachIds = inputFileList.Where(x => x.TypeFile_Code == TypeFile.Nach 
                    && !x.CriteriaSet.HasValue || x.CriteriaSet == false).Select(x => x.EmpId).ToList();

                if (nachIds.Count > 0)
                {
                    //Устанавливаем критерии для начислений
                    SetNachCriteria(nachIds, traceData);
                }

                List<long> platIds = inputFileList.Where(x => x.TypeFile_Code == TypeFile.Strah
                    && !x.CriteriaSet.HasValue || x.CriteriaSet == false).Select(x => x.EmpId).ToList();

                if (platIds.Count > 0)
                {
                    //Устанавливаем критерии для зачислений
                    SetPlatCriteria(platIds, traceData);
                }

                foreach (OMInputFile inputFile in inputFileList
                    .Where(x => nachIds.Contains(x.EmpId) || platIds.Contains(x.EmpId)))
                {
                    //устанавливаем признак, что критерии установлены
                    inputFile.CriteriaSet = true;
                    inputFile.Save();
                }
            }

            traceData.OplFilesCount = inputFileList.Count(x => x.TypeFile_Code == TypeFile.BankPayment);
            traceData.NachFilesCount = inputFileList.Count(x => x.TypeFile_Code == TypeFile.Nach);
            traceData.PlatFileCount = inputFileList.Count(x => x.TypeFile_Code == TypeFile.Strah);
        }

        /// <summary>
        /// Рекурсивно распаковывает все файлы МФЦ
        /// </summary>
        /// <param name="inputFileList">Список загружаемых файлов</param>
        /// <param name="file">Запись файлового хранилища</param>
        /// <param name="periodRegDate">Период учета</param>
        /// <param name="traceData">Лог загрузки</param>
        private void ExtractRecursive(List<OMInputFile> inputFileList, OMFileStorage file, DateTime periodRegDate, MfcUploadTraceData traceData)
        {
            if (inputFileList == null)
            {
                throw new Exception("Не передан обязательный параметр - список загружаеммых файлов");
            }

            if (file == null)
            {
                return;
            }

            if (file.IsVirtualDirectory.HasValue
                && file.IsVirtualDirectory.Value)
            {
                List<OMFileStorage> directoryFiles = _fileStorageService.GetVirtualDirectoryFiles(file.Id);
                foreach(OMFileStorage directoryFile in directoryFiles)
                {
                    ExtractRecursive(inputFileList, directoryFile, periodRegDate, traceData);
                }
            }
            else
            {
                using (FileStream stream = _fileStorageService.GetFileStream(file))
                {
                    ExtractFile(inputFileList, stream, file.Filename, periodRegDate, traceData);
                }
            }
        }

        /// <summary>
        /// Распаковывает или копирует файлы МФЦ в директорию для загрузки
        /// </summary>
        /// <param name="inputFileList">Список загружаемых файлов</param>
        /// <param name="stream">Файл МФЦ</param>
        /// <param name="fullFileName">Полное наименование файла</param>
        /// <param name="extractPath">Путь распаковки</param>
        /// <param name="periodRegDate">Период учета</param>
        /// <param name="traceData">Лог загрузки</param>
        private void ExtractFile(List<OMInputFile> inputFileList, Stream stream, string fullFileName, DateTime periodRegDate, MfcUploadTraceData traceData)
        {
            if (inputFileList == null)
            {
                throw new Exception("Не передан обязательный параметр - список загружаеммых файлов");
            }

            try
            {
                string fileName = Path.GetFileName(fullFileName);
                string fileExtension = Path.GetExtension(fullFileName);

                switch (fileExtension)
                {
                    case ".zip":
                        ExtractArchiveData(inputFileList, stream, periodRegDate, traceData);
                        break;
                    //файл начислений или зачислений
                    case ".dbf":
                    //файл оплаты
                    case ".001":
                        InsuranceSourceType sourceType = fileExtension == ".dbf" ? InsuranceSourceType.Mfc : InsuranceSourceType.Bank;

                        //создаем файл зачисления/начисления
                        OMInputFile inputFile = CreateInputFile(sourceType, fileName, stream, periodRegDate);

                        //добавляем идентифкатор лога загрузки
                        inputFile.LogFileId = traceData.LogFileId;

                        //проверяем файлы загруженные ранее
                        OMInputFile previousMfcInputFile = sourceType == InsuranceSourceType.Mfc ? 
                            OMInputFile.Where(x => x.PeriodRegDate == inputFile.PeriodRegDate
                            && x.DistrictId == inputFile.DistrictId && x.TypeFile_Code == inputFile.TypeFile_Code && x.ParentId == null)
                            .SelectAll()
                            .ExecuteFirstOrDefault() :
                            OMInputFile.Where(x => x.PeriodRegDate == inputFile.PeriodRegDate
                            && x.DistrictId == inputFile.DistrictId && x.TypeFile_Code == inputFile.TypeFile_Code
                            && x.FileName == inputFile.FileName && x.KodPost == inputFile.KodPost).SelectAll().ExecuteFirstOrDefault();

                        if (previousMfcInputFile != null)
                        {
                            //CIPJS-313 Требуется предоставлять возможность по одну РАЙОНУ+ПЕРИОД грузить второй файл с начислениями\зачислениями МФЦ.
                            //Доработать загрузку файлов. Если в новой загрузке присутствует файл с начислениями\зачислениями МФЦ проверять
                            if (sourceType == InsuranceSourceType.Bank || (previousMfcInputFile.CountStr == inputFile.CountStr
                                && previousMfcInputFile.SumAll == inputFile.SumAll))
                            {
                                throw new Exception($"Файл {fileName}" +
                                    $" {(sourceType == InsuranceSourceType.Bank ? "банковских строк оплат" : $"{(inputFile.TypeFile_Code == TypeFile.Nach ? "начисления" : "зачисления")}")}" +
                                    $" {(sourceType == InsuranceSourceType.Bank ? "был загружен ранее" : $"считается дублем и не требует загрузки, т.к. совпадает кол-во строк и общая сумма")}");
                            }
                            else
                            {
                                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                                {
                                    inputFile.ParentId = previousMfcInputFile.EmpId;
                                    inputFile.Save();

                                    SaveStream(stream, GetMfcDataFullFileName(inputFile), true);

                                    inputFileList.Add(inputFile);

                                    ts.Complete();
                                }
                            }
                        }
                        else
                        {
                            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                            {
                                inputFile.Save();

                                SaveStream(stream, GetMfcDataFullFileName(inputFile), true);

                                inputFileList.Add(inputFile);

                                ts.Complete();
                            }
                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                if (traceData != null)
                {
                    traceData.Errors.Add(ex.Message);
                }
            }
        }

        /// <summary>
        /// Распаковывает архив данных по округу
        /// </summary>
        /// <param name="inputFileList">Список загружаемых файлов</param>
        /// <param name="stream">Архив данных по округу</param>
        /// <param name="extractPath">Путь для распаковки</param>
        private void ExtractArchiveData(List<OMInputFile> inputFileList, Stream stream, DateTime periodRegDate, MfcUploadTraceData traceData)
        {
            if (inputFileList == null)
            {
                throw new Exception("Не передан обязательный параметр - список загружаеммых файлов");
            }

            using (ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (Stream entryStream = zipArchiveEntry.Open())
                        {
                            entryStream.CopyTo(ms);
                            ExtractFile(inputFileList, ms, zipArchiveEntry.Name, periodRegDate, traceData);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Получает название файла начисления/зачисления для конечной загрузки 
        /// файлы храним на сервере в папках вида год/месяц/ID файла.dbf
        /// </summary>
        /// <param name="extractPath">Путь распаковки</param>
        /// <returns></returns>
        private string GetMfcDataFullFileName(OMInputFile inputFile)
        {
            if (inputFile == null)
            {
                throw new Exception($"Не удалось определить название файла. Передан пустой файл загрузки");
            }

            if (!inputFile.PeriodRegDate.HasValue)
            {
                throw new Exception($"Не удалось определить название файла. Для файла загрузки с идентфикатором {inputFile.EmpId} не заполнен период");
            }
            return Path.Combine(MfcProcessFolder,
                inputFile.PeriodRegDate.Value.Year.ToString(),
                inputFile.PeriodRegDate.Value.Month.ToString().PadLeft(2, '0'),
                inputFile.EmpId.ToString() + GetFileExtensionByType(inputFile.TypeFile_Code));
        }

        /// <summary>
        /// Получает расширение файла МФЦ по его типу
        /// </summary>
        /// <param name="typeFile"></param>
        /// <returns></returns>
        private string GetFileExtensionByType(TypeFile typeFile)
        {
            switch(typeFile)
            {
                case TypeFile.Strah:
                case TypeFile.Nach:
                    return ".dbf";
                case TypeFile.BankPayment:
                    return ".001";
                default:
                    throw new Exception("Не удалось определить расширение файла, передан неподдерживаемый тип");
            }
        }

        /// <summary>
        /// Получает последний загруженный период для округа
        /// </summary>
        /// <param name="okrugCode">Код округа</param>
        /// <returns>Удаляемый период с перечислением районов и округом</returns>
        public MfcDeleteOkrugPeriodDto GetLastOkrugPeriod(long okrugCode)
        {
            OMOkrug okrug = OMOkrug.Where(x => x.Code == okrugCode)
                .Select(x => x.Code)
                .Select(x => x.Name)
                .Select(x => x.ShortName)
                .Execute()
                .FirstOrDefault();

            if (okrug == null)
            {
                throw new ValidationException($"Не удалось определить округ с кодом {okrugCode}");
            }

            List<OMDistrict> districts = OMDistrict.Where(x => x.OkrugId == okrug.Id && x.Code != null)
                .Select(x => x.Code)
                .Select(x => x.Name)
                .Execute();

            List<long?> districtIds = districts.Select(x => (long?)x.Id).ToList();

            if (districtIds.Count == 0)
            {
                throw new ValidationException($"Не удалось определить коды районов для округа {okrug.ShortName}");
            }

            DateTime? periodRegDate = OMInputFile
                .Where(x => districtIds.Contains(x.DistrictId) && x.PeriodRegDate != null)
                .OrderByDescending(x => x.PeriodRegDate)
                .Select(x => x.PeriodRegDate)
                .Execute()
                .FirstOrDefault()?.PeriodRegDate;

            return new MfcDeleteOkrugPeriodDto
            {
                Okrug = okrug,
                PeriodRegDate = periodRegDate,
                Districts = districts
            };
        }
        
        /// <summary>
        /// Удаление загруженного периода для округа
        /// </summary>
        /// <param name="okrugCode">Код округа</param>
        /// <param name="periodRegDate">Период</param>
        public void Delete(long okrugCode, DateTime periodRegDate)
        {
            MfcDeleteOkrugPeriodDto lastPeriodDto = GetLastOkrugPeriod(okrugCode);

            //проверяем, что период для округа - последний
            if (lastPeriodDto.PeriodRegDate != periodRegDate)
            {
                throw new ValidationException("Удаляемый период не является последним. Удаление невозможно");
            }

            List<long?> districtIds = lastPeriodDto.Districts.Select(x => (long?)x.Id).ToList();

            if (districtIds.Count == 0)
            {
                throw new ValidationException("Не удалось определить коды районов для округа");
            }

            List<OMInputFile> inputFiles = OMInputFile
                .Where(x => x.PeriodRegDate == periodRegDate 
                    && districtIds.Contains(x.DistrictId))
                .SelectAll()
                .Execute();

            List<Tuple<long, DateTime>> fspRegDates = new List<Tuple<long, DateTime>>();

            foreach(OMInputFile inputFile in inputFiles)
            {
                try
                {
                    using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                    {
                        //Найти запись в Реестре INSUR_BANK_PLAT, для которой  значение атрибута LINK_ID_FILE = INSUR_INPUT_FILE.EMP_ID
                        List<OMBankPlat> bankPlats = OMBankPlat.Where(x => x.LinkIdFile == inputFile.EmpId).Execute();
                        foreach (OMBankPlat bankPlat in bankPlats)
                        {
                            bankPlat.Destroy();
                        }

                        //Найти запись в Реестре INSUR_SVOD_BANK, для которой  значение атрибута LINK_ID_FILE = INSUR_INPUT_FILE.EMP_ID
                        List<OMSvodBank> svodBanks = OMSvodBank.Where(x => x.LinkIdFile == inputFile.EmpId).Execute();
                        foreach (OMSvodBank svodBank in svodBanks)
                        {
                            svodBank.Destroy();
                        }

                        //Найти запись в Реестре INSUR_INPUT_NACH, для которой  значение атрибута LINK_ID_FILE = INSUR_INPUT_FILE.EMP_ID
                        List<OMInputNach> inputNaches = OMInputNach.Where(x => x.LinkIdFile == inputFile.EmpId).Execute();
                        foreach (OMInputNach inputNach in inputNaches)
                        {
                            if (inputNach.FspId.HasValue && inputNach.PeriodRegDate.HasValue)
                            {
                                Tuple<long, DateTime> nachFspRegDate = new Tuple<long, DateTime>(inputNach.FspId.Value, inputNach.PeriodRegDate.Value);
                                if (!fspRegDates.Contains(nachFspRegDate))
                                {
                                    fspRegDates.Add(nachFspRegDate);
                                }
                            }
                            inputNach.Destroy();
                        }

                        //Найти запись в Реестре INSUR_INPUT_PLAT, для которой  значение атрибута LINK_ID_FILE = INSUR_INPUT_FILE.EMP_ID
                        List<OMInputPlat> inputPlats = OMInputPlat.Where(x => x.LinkIdFile == inputFile.EmpId).Execute();
                        foreach (OMInputPlat inputPlat in inputPlats)
                        {
                            if (inputPlat.FspId.HasValue && inputPlat.PeriodRegDate.HasValue)
                            {
                                Tuple<long, DateTime> platFspRegDate = new Tuple<long, DateTime>(inputPlat.FspId.Value, inputPlat.PeriodRegDate.Value);
                                if (!fspRegDates.Contains(platFspRegDate))
                                {
                                    fspRegDates.Add(platFspRegDate);
                                }
                            }
                            inputPlat.Destroy();
                        }

                        ts.Complete();
                    }
                }
                catch(Exception ex)
                {
                    ErrorManager.LogError(ex);
                }
            }

            //Выполняем перерасчет начислений на ФСП
            foreach(Tuple<long, DateTime> fspRegDate in fspRegDates)
            {
                _fspService.AccountFsp(fspRegDate.Item1, fspRegDate.Item2);
            }
        }

        /// <summary>
        /// Создание записей реестра начислений из файла с начислениями МФЦ (.dbf)
        /// </summary>
        /// <param name="fileStream">Файл с начислениями МФЦ (.dbf)</param>
        /// <returns></returns>
        private List<OMInputNach> GetInputNachList(Stream fileStream, out long countStr, long? inputFileId = null, long? districtId = null, DateTime? periodRegDate = null, long? inputFileParentId = null)
        {
            List<OMInputNach> inputNaches = new List<OMInputNach>();

            countStr = 0;

            DbfFile dbfFile = new DbfFile(Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage));

            if (fileStream == null)
            {
                return inputNaches;
            }

            fileStream.Seek(0, SeekOrigin.Begin);
            dbfFile.Open(fileStream);

            MapConfig mapConfig = GetMapConfig(dbfFile, NachFilePrefix);

            if (mapConfig == null)
            {
                throw new Exception("Не удалось определить конфигурацию соотвествия \"Реестр начислений - файл начислений МФЦ\"");
            }

            while (true)
            {
                DbfRecord dbfRecord = dbfFile.ReadNext();

                if (dbfRecord == null)
                {
                    return inputNaches;
                }

                OMInputNach inputNach = GetInputNach(dbfRecord, mapConfig, inputFileId, districtId, periodRegDate);

                if (inputNach != null
                    //CIPJS-313 если данные не совпадают, грузим второй файл ( создаем строку в INSUR_INPUT и сохраняем информацию о втором файле , 
                    //строки грузим только те, которых нет в ранее загруженном файле , сопоставляемся по коду плательщика+ период загрузки+сумма
                    //CIPJS-416 только строки у которых сумма не равна 0
                    && (!inputFileParentId.HasValue || (inputNach.SumNach != 0 && OMInputNach.Where(x => x.LinkIdFile == inputFileParentId.Value
                                && x.Kodpl == inputNach.Kodpl
                                && x.SumNach == inputNach.SumNach)
                                .GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt() == 0)))
                {
                    inputNaches.Add(inputNach);
                }

                countStr++;
            }
        }

        /// <summary>
        /// Получает запись реестра начислений из строки dbf-файла
        /// </summary>
        /// <param name="dbfRecord">Строка dbf</param>
        /// <param name="mapConfig">Конфигурация маппинга</param>
        /// <returns></returns>
        private OMInputNach GetInputNach(DbfRecord dbfRecord, MapConfig mapConfig, long? inputFileId = null, long? districtId = null, DateTime? periodRegDate = null)
        {
            if (mapConfig == null || mapConfig.MapList == null)
            {
                return null;
            }

            OMInputNach inputNach = new OMInputNach();
            inputNach.LinkIdFile = inputFileId;
            inputNach.DistrictId = districtId;
            inputNach.PeriodRegDate = periodRegDate;
            inputNach.TypeSource_Code = InsuranceSourceType.Mfc;
            inputNach.StatusIdentif_Code = StatusIdentifikacii.None;
            inputNach.LoadStatus_Code = LoadStatus.Loaded;
            foreach (Map map in mapConfig.MapList)
            {
                if (map == null || map.Dest.IsNullOrEmpty())
                {
                    continue;
                }
                int dbfColumnIndex = dbfRecord.FindColumn(map.Src);
                if (dbfColumnIndex == -1)
                {
                    continue;
                }

                //заполняем значение из связаной таблицы
                if (map.ReferenceTable.IsNotEmpty())
                {
                    if (map.ReferenceId.IsNullOrEmpty()
                        || map.ReferenceColumn.IsNullOrEmpty())
                    {
                        continue;
                    }
                    string referenceKey = $"{map.ReferenceTable}_{map.ReferenceId}_{map.ReferenceColumn}";
                    string referenceValue = dbfRecord[dbfColumnIndex].ToString();
                    if (referenceValue.IsNullOrEmpty())
                    {
                        continue;
                    }
                    DataRow[] referenceRows = _referenceCache[referenceKey].Select($"{map.ReferenceColumn} = {referenceValue}");
                    if (referenceRows.Length > 0)
                    {
                        SetMapPropertyValue(inputNach, map.Dest, referenceRows[0][map.ReferenceId]);
                    }
                }
                else
                {
                    SetMapPropertyValue(inputNach, map.Dest, dbfRecord[dbfColumnIndex]);
                }
            }
            return inputNach;
        }

        /// <summary>
        /// Создание записей реестра зачислений из файла с зачислениями МФЦ (.dbf)
        /// </summary>
        /// <param name="fileStream">Файл с зачислениями МФЦ (.dbf)</param>
        /// <returns></returns>
        private List<OMInputPlat> GetInputPlatList(Stream fileStream, out long countStr, long? inputFileId = null, long? districtId = null, DateTime? periodRegDate = null, long? inputFileParentId = null)
        {
            List<OMInputPlat> inputPlats = new List<OMInputPlat>();

            countStr = 0;

            DbfFile dbfFile = new DbfFile(Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage));

            if (fileStream == null)
            {
                return inputPlats;
            }

            fileStream.Seek(0, SeekOrigin.Begin);
            dbfFile.Open(fileStream);

            MapConfig mapConfig = GetMapConfig(dbfFile, StrahFilePrefix);

            if (mapConfig == null)
            {
                throw new Exception("Не удалось определить конфигурацию соотвествия \"Реестр зачислений - файл начислений МФЦ\"");
            }
            
            while(true)
            {
                DbfRecord dbfRecord = dbfFile.ReadNext();

                if (dbfRecord == null)
                {
                    return inputPlats;
                }

                OMInputPlat inputPlat = GetInputPlat(dbfRecord, mapConfig, inputFileId, districtId, periodRegDate);

                // CIPJS-895: Ошибки при загрузке зачислений МФЦ ( при загрузке Старого кода )
                //if (inputPlat != null
                //    //CIPJS-313 если данные не совпадают, грузим второй файл ( создаем строку в INSUR_INPUT и сохраняем информацию о втором файле , 
                //    //строки грузим только те, которых нет в ранее загруженном файле , сопоставляемся по коду плательщика+ период загрузки+сумма
                //    && (!inputFileParentId.HasValue || OMInputPlat.Where(x => x.LinkIdFile == inputFileParentId.Value
                //                && x.Kodpl == inputPlat.Kodpl
                //                && x.SumOpl == inputPlat.SumOpl)
                //                .GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt() == 0))
                //{
                //    inputPlats.Add(inputPlat);
                //}

                if (inputPlat != null)
                {
                    inputPlats.Add(inputPlat);
                }

                countStr++;
            }
        }

        /// <summary>
        /// Получает запись реестра зачислений из строки dbf-файла
        /// </summary>
        /// <param name="dbfRecord">Строка dbf</param>
        /// <param name="mapConfig">Конфигурация маппинга</param>
        /// <returns></returns>
        private OMInputPlat GetInputPlat(DbfRecord dbfRecord, MapConfig mapConfig, long? inputFileId = null, long? districtId = null, DateTime? periodRegDate = null)
        {
            if (mapConfig == null || mapConfig.MapList == null)
            {
                return null;
            }

            OMInputPlat inputPlat = new OMInputPlat();
            inputPlat.LinkIdFile = inputFileId;
            inputPlat.DistrictId = districtId;
            inputPlat.PeriodRegDate = periodRegDate;
            inputPlat.TypeSource_Code = InsuranceSourceType.Mfc;
            inputPlat.StatusIdentif_Code = StatusIdentifikacii.None;
            inputPlat.LoadStatus_Code = LoadStatus.Loaded;
            foreach (Map map in mapConfig.MapList)
            {
                if (map == null || map.Dest.IsNullOrEmpty())
                {
                    continue;
                }
                int dbfColumnIndex = dbfRecord.FindColumn(map.Src);
                if (dbfColumnIndex == -1)
                {
                    continue;
                }

                //заполняем значение из связаной таблицы
                if (map.ReferenceTable.IsNotEmpty())
                {
                    if (map.ReferenceId.IsNullOrEmpty()
                        || map.ReferenceColumn.IsNullOrEmpty())
                    {
                        continue;
                    }
                    string referenceKey = $"{map.ReferenceTable}_{map.ReferenceId}_{map.ReferenceColumn}";
                    string referenceValue = dbfRecord[dbfColumnIndex].ToString();
                    if (referenceValue.IsNullOrEmpty())
                    {
                        continue;
                    }

                    //CIPJS-216 KOM если О , то НОЛЬ
                    if (referenceValue != null && referenceValue.ToLower() == "о")
                    {
                        referenceValue = "0";
                    }

                    DataRow[] referenceRows = _referenceCache[referenceKey].Select($"{map.ReferenceColumn} = {referenceValue}");
                    if (referenceRows.Length > 0)
                    {
                        SetMapPropertyValue(inputPlat, map.Dest, referenceRows[0][map.ReferenceId]);
                    }
                }
                else
                {
                    SetMapPropertyValue(inputPlat, map.Dest, dbfRecord[dbfColumnIndex]);
                }
            }
            return inputPlat;
        }

        private List<OMBankPlat> GetBankPlatsList(Stream fileStream, out string codPost, out string checkLine, out decimal? totalSum,
            long? inputFileId = null, long? svodBankId = null, long? districtId = null, DateTime? periodRegDate = null)
        {
            fileStream.Seek(0, SeekOrigin.Begin);
            List<OMBankPlat> result = new List<OMBankPlat>();
            checkLine = null;
            codPost = null;
            totalSum = null;

            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 1024, leaveOpen: true))
            {
                while (true)
                {
                    string line = streamReader.ReadLine();

                    if (line == null)
                    {
                        break;
                    }

                    //строку с контрольной суммой считаем последней
                    if (line.StartsWith("="))
                    {
                        checkLine = line;
                        break;
                    }

                    //длина должна соответствовать формату
                    if (line.Length < 123)
                    {
                        continue;
                    }

                    string kodPl = line.Substring(0, 10);
                    string period = $"01.{line.Substring(10, 2)}.20{line.Substring(12, 2)}";
                    string nomDoc = line.Substring(14, 2);
                    string sumAll = GetDecimalString(line.Substring(16, 7), line.Substring(23, 2));
                    string komBankAll = GetDecimalString(line.Substring(25, 5), line.Substring(30, 2));
                    string bikBank = line.Substring(32, 9).TrimStart('0');
                    string dataPp = $"{line.Substring(41, 2)}.{line.Substring(43, 2)}.20{line.Substring(45, 2)}";
                    string codDoc = line.Substring(47, 20).TrimStart('0');
                    string kodYsl = line.Substring(67, 3).TrimStart('0');
                    string kodPost = line.Substring(70, 4).TrimStart('0');
                    string sumByCode = GetDecimalString(line.Substring(74, 7), line.Substring(81, 2));
                    string docPeriod = $"01.{line.Substring(111, 2)}.20{line.Substring(113, 2)}";
                    string flagVozvr = line.Substring(115, 1);
                    string typeOpl = line.Substring(116, 1);
                    string kodYpravl = line.Substring(117, 5).TrimStart('0');
                    string flagNach = line.Substring(122, 1);

                    if (codPost.IsNullOrEmpty())
                    {
                        codPost = kodPost;
                    }

                    OMBankPlat bankPlat = new OMBankPlat();
                    bankPlat.LinkIdFile = inputFileId;
                    bankPlat.LinkSvodBank = svodBankId;
                    bankPlat.Kodpl = kodPl;
                    bankPlat.Period = DateTime.ParseExact(period, "dd.MM.yyyy", CultureInfo.CurrentCulture);
                    bankPlat.NomDoc = nomDoc;
                    bankPlat.SumAll = sumAll.ParseToDecimal();
                    bankPlat.KomBankAll = komBankAll.ParseToDecimal();
                    bankPlat.BicBank = bikBank.IsNotEmpty() ? (long?)bikBank.ParseToLong() : null;
                    bankPlat.DataPp = DateTime.ParseExact(dataPp, "dd.MM.yyyy", CultureInfo.CurrentCulture);
                    bankPlat.CodDoc = codDoc.IsNotEmpty() ? (long?)codDoc.ParseToLong() : null;
                    bankPlat.KodYsl = kodYsl.IsNotEmpty() ? (long?)kodYsl.ParseToLong() : null;
                    bankPlat.KodPost = kodPost.IsNotEmpty() ? (long?)kodPost.ParseToLong() : null;
                    bankPlat.SumByCode = sumByCode.ParseToDecimal();
                    bankPlat.DocPeriod = DateTime.ParseExact(docPeriod, "dd.MM.yyyy", CultureInfo.CurrentCulture);
                    bankPlat.FlagVozvr = flagVozvr.ParseToLong();
                    bankPlat.TypeOpl = typeOpl.ParseToLong();
                    bankPlat.KodYpravl = kodYpravl.IsNotEmpty() ? (long?)kodYpravl.ParseToLong() : null;
                    bankPlat.FlagNach = flagNach.ParseToLong();
                    bankPlat.DistrictId = districtId;
                    bankPlat.PeriodRegDate = periodRegDate;
                    result.Add(bankPlat);

                    if (totalSum.HasValue)
                    {
                        totalSum += bankPlat.SumByCode;
                    }
                    else
                    {
                        totalSum = bankPlat.SumByCode;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// /// Получает конфигурацию из всех доступных конфигураций
        /// по соответствию колонок в файле конфигурации и в загружаемом файле
        /// </summary>
        /// <param name="dbfFile">Файл с данными</param>
        /// <param name="configurationSubFolderPath">Путь до дочернего каталога с конфигурациями</param>
        /// <returns></returns>
        private MapConfig GetMapConfig(DbfFile dbfFile, string configurationSubFolderPath = null)
        {
            Dictionary<string, MapConfig> mapConfigs = Core.ConfigParam.Configuration.GetParams<MapConfig>(configurationSubFolderPath.IsNotEmpty() ?
                Path.Combine(MappingConfigurationFolder, configurationSubFolderPath) :
                MappingConfigurationFolder);

            //проверяем соответствие всех полей из конфигурации - всем колонкам из файла с данными
            //возвращаем первый валидный
            foreach(MapConfig mapConfig in mapConfigs.Values)
            {
                if (ValidateMapConfig(dbfFile, mapConfig))
                {
                    //загружаем кэш справочников
                    LoadReferenceCache(mapConfig);

                    return mapConfig;
                }
            }

            return null;
        }

        /// <summary>
        /// Проверяет соответствие конфигурации соответствия полей и колонок файла с данными
        /// Если в файле с данными присутствуют все колонки из конфигурации соотвествия полей,
        /// значит конфигурации соответствия полей валидна
        /// </summary>
        /// <param name="dbfFile">Файл с данными</param>
        /// <param name="mapConfig"></param>
        /// <returns></returns>
        private bool ValidateMapConfig(DbfFile dbfFile, MapConfig mapConfig)
        {
            //пропускаем пустые
            if (mapConfig == null || mapConfig.MapList == null || mapConfig.MapList.Count == 0)
            {
                return false;
            }

            string[] mapSrcs = mapConfig.MapList.Select(x => x.Src).ToArray();

            foreach (string mapSrc in mapSrcs)
            {
                if (dbfFile.Header.FindColumn(mapSrc) == -1)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Загружаем все справочники в память, если они указаны в конфиге для загрузки
        /// </summary>
        /// <param name="mapConfig"></param>
        private void LoadReferenceCache(MapConfig mapConfig)
        {
            if (mapConfig == null || mapConfig.MapList == null || mapConfig.MapList.Count == 0)
            {
                return;
            }

            List<Map> referenceMaps = mapConfig.MapList.Where(x =>
                x.ReferenceTable.IsNotEmpty() &&
                x.ReferenceId.IsNotEmpty() &&
                x.ReferenceColumn.IsNotEmpty()).ToList();

            foreach(Map map in referenceMaps)
            {
                string referenceKey = $"{map.ReferenceTable}_{map.ReferenceId}_{map.ReferenceColumn}";
                if (!_referenceCache.ContainsKey(referenceKey))
                {
                    _referenceCache.Add(referenceKey, GetReferencedData(map.ReferenceTable, map.ReferenceId, map.ReferenceColumn));
                }
            }
        }

        /// <summary>
        /// Получает справочник из БД
        /// </summary>
        /// <param name="referenceTable">Название таблицы БД</param>
        /// <param name="idColumn">Колонка значения</param>
        /// <param name="referenceColumn">Колонка сопоставления</param>
        /// <returns></returns>
        private DataTable GetReferencedData(string referenceTable, string idColumn, string referenceColumn)
        {
            DbCommand command = DBMngr.Realty.GetSqlStringCommand($"select {idColumn}, {referenceColumn} from {referenceTable}");
            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// Устанавливает значение свойства объекта
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="propertyName">Наименование свойства</param>
        /// <param name="value">Устанавливаемое значение</param>
        private void SetMapPropertyValue(object obj, string propertyName, object value)
        {
            if (propertyName.IsNullOrEmpty())
            {
                return;
            }

            Type objType = obj.GetType();

            PropertyInfo propertyInfo = objType.GetProperty(propertyName);
            if (propertyInfo == null)
            {
                return;
            }

            object objectValue = propertyInfo.PropertyType == value.GetType() ?
                value :
                value.ParseTo(propertyInfo.PropertyType);
            
            propertyInfo.SetValue(obj, objectValue);
        }

        /// <summary>
        /// Получение строкового представления дробного числа из двух целых чисел
        /// </summary>
        /// <param name="integerPart">Целая часть дробного числа</param>
        /// <param name="decimalPart">Дробная часть дробного числа</param>
        /// <returns></returns>
        private string GetDecimalString(string integerPart, string decimalPart)
        {
            return $"{integerPart.TrimStart('0')}{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}{decimalPart}";
        }

        /// <summary>
        /// Устанавливает значение критериев для списка начислений
        /// </summary>
        /// <param name="inputNachList">Списко начислений</param>
        /// <param name="traceData">Лог загрузки</param>
        private void SetNachCriteria(List<long> inputFileIds, MfcUploadTraceData traceData)
        {
            if (inputFileIds != null && inputFileIds.Count > 0)
            {
                traceData.Status = MfcUploadFileStatus.CriteriaSetNach1;
                traceData.SaveLogFile();
                #region Критерий №1 – расхождения в суммах начислений
                string calcSumMismatchFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Nach_CalcSumMismatch");
                DbCommand calcSumMismatchCommand = DBMngr.Realty.GetStoredProcCommand(calcSumMismatchFn, inputFileIds, 0.1);
                DBMngr.Realty.ExecuteNonQuery(calcSumMismatchCommand);
                #endregion

                if (MfcUploadSetCriteriaExtended)
                {
                    traceData.Status = MfcUploadFileStatus.CriteriaSetNach2;
                    traceData.SaveLogFile();
                    #region Критерий №2 – UNOM с разными адресами
                    string unomAddressMismatch = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Nach_UnomAddressMismatch");
                    DbCommand unomAddressMismatchCommand = DBMngr.Realty.GetStoredProcCommand(unomAddressMismatch, inputFileIds);
                    DBMngr.Realty.ExecuteNonQuery(unomAddressMismatchCommand);
                    #endregion
                }

                traceData.Status = MfcUploadFileStatus.CriteriaSetNach3;
                traceData.SaveLogFile();
                #region Критерий №3 – Подозрительные UNOM
                string suspiciousUnomFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Nach_SuspiciousUnom");
                DbCommand suspiciousUnomCommand = DBMngr.Realty.GetStoredProcCommand(suspiciousUnomFn, inputFileIds);
                DBMngr.Realty.ExecuteNonQuery(suspiciousUnomCommand);
                #endregion

                traceData.Status = MfcUploadFileStatus.CriteriaSetNach4;
                traceData.SaveLogFile();
                #region Критерий №4 - Несовпадение NOM+NOMI
                string kvnomNomMismatchFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Nach_KvnomNomMismatch");
                DbCommand kvnomNomMismatchCommand = DBMngr.Realty.GetStoredProcCommand(kvnomNomMismatchFn, inputFileIds);
                DBMngr.Realty.ExecuteNonQuery(kvnomNomMismatchCommand);
                #endregion

                if (MfcUploadSetCriteriaExtended)
                {
                    traceData.Status = MfcUploadFileStatus.CriteriaSetNach5;
                    traceData.SaveLogFile();
                    #region Критерий №5 - Более одного начисления на одного плательщика
                    string moreThanOneNachFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Nach_MoreThanOneNach");
                    DbCommand moreThanOneNachFnCommand = DBMngr.Realty.GetStoredProcCommand(moreThanOneNachFn, inputFileIds);
                    DBMngr.Realty.ExecuteNonQuery(moreThanOneNachFnCommand);
                    #endregion
                }

                traceData.Status = MfcUploadFileStatus.CriteriaSetNach6;
                traceData.SaveLogFile();
                #region Критерий №6 - Есть начисление, нет площади
                string nachWithoutOplFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Nach_NachWithoutOpl");
                DbCommand nachWithoutOplCommand = DBMngr.Realty.GetStoredProcCommand(nachWithoutOplFn, inputFileIds);
                DBMngr.Realty.ExecuteNonQuery(nachWithoutOplCommand);
                #endregion

                traceData.Status = MfcUploadFileStatus.CriteriaSetNach7;
                traceData.SaveLogFile();
                #region Критерий №7 - Площадь страхования не совпадает с площадью квартиры, для отдельных квартир
                string foplOplMismatchFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Nach_FoplOplMismatch");
                DbCommand foplOplMismatchCommand = DBMngr.Realty.GetStoredProcCommand(foplOplMismatchFn, inputFileIds);
                DBMngr.Realty.ExecuteNonQuery(foplOplMismatchCommand);
                #endregion

                traceData.Status = MfcUploadFileStatus.CriteriaSetNach8;
                traceData.SaveLogFile();
                #region Критерий #8 - В данных МФЦ квартира присутствует, а объект страхования (квартира) в Системе не найден
                string flatNotFoundFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Nach_FlatNotFound");
                DbCommand flatNotFoundCommand = DBMngr.Realty.GetStoredProcCommand(flatNotFoundFn, inputFileIds);
                DBMngr.Realty.ExecuteNonQuery(flatNotFoundCommand);
                #endregion

                traceData.Status = MfcUploadFileStatus.CriteriaSetNach9;
                traceData.SaveLogFile();
                #region Критерий #9 - В данных МФЦ неверная общая площадь квартиры
                string flatOplMismatchFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Nach_FlatOplMismatch");
                DbCommand flatOplMismatchCommand = DBMngr.Realty.GetStoredProcCommand(flatOplMismatchFn, inputFileIds);
                DBMngr.Realty.ExecuteNonQuery(flatOplMismatchCommand);
                #endregion

                traceData.Status = MfcUploadFileStatus.CriteriaSetNach10;
                traceData.SaveLogFile();
                #region Критерий #10 - В данных МФЦ неверное количество комнат в квартире
                string flatKolgpMismatchFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Nach_FlatKolgpMismatch");
                DbCommand flatKolgpMismatchCommand = DBMngr.Realty.GetStoredProcCommand(flatKolgpMismatchFn, inputFileIds);
                DBMngr.Realty.ExecuteNonQuery(flatKolgpMismatchCommand);
                #endregion
            }
        }
        /// <summary>
        /// Устанавливает значение критериев для списка начислений
        /// </summary>
        /// <param name="inputPlatList">Списко начислений</param>
        /// <param name="traceData">Лог загрузки</param>
        private void SetPlatCriteria(List<long> inputFileIds, MfcUploadTraceData traceData)
        {
            if (inputFileIds != null && inputFileIds.Count > 0)
            {
                traceData.Status = MfcUploadFileStatus.CriteriaSetPlat1;
                traceData.SaveLogFile();
                #region Критерий №1 – расхождения в суммах начислений
                string calcSumMismatchFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Plat_CalcSumMismatch");
                DbCommand calcSumMismatchCommand = DBMngr.Realty.GetStoredProcCommand(calcSumMismatchFn, inputFileIds, 0.1);
                DBMngr.Realty.ExecuteNonQuery(calcSumMismatchCommand);
                #endregion

                if (MfcUploadSetCriteriaExtended)
                {
                    traceData.Status = MfcUploadFileStatus.CriteriaSetPlat2;
                    traceData.SaveLogFile();
                    #region Критерий №2 – UNOM с разными адресами
                    string unomAddressMismatch = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Plat_UnomAddressMismatch");
                    DbCommand unomAddressMismatchCommand = DBMngr.Realty.GetStoredProcCommand(unomAddressMismatch, inputFileIds);
                    DBMngr.Realty.ExecuteNonQuery(unomAddressMismatchCommand);
                    #endregion
                }

                traceData.Status = MfcUploadFileStatus.CriteriaSetPlat3;
                traceData.SaveLogFile();
                #region Критерий №3 – Подозрительные UNOM
                string suspiciousUnomFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Plat_SuspiciousUnom");
                DbCommand suspiciousUnomCommand = DBMngr.Realty.GetStoredProcCommand(suspiciousUnomFn, inputFileIds);
                DBMngr.Realty.ExecuteNonQuery(suspiciousUnomCommand);
                #endregion

                traceData.Status = MfcUploadFileStatus.CriteriaSetPlat4;
                traceData.SaveLogFile();
                #region Критерий #8 - В данных МФЦ квартира присутствует, а объект страхования (квартира) в Системе не найден
                string flatNotFoundFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Plat_FlatNotFound");
                DbCommand flatNotFoundCommand = DBMngr.Realty.GetStoredProcCommand(flatNotFoundFn, inputFileIds);
                DBMngr.Realty.ExecuteNonQuery(flatNotFoundCommand);
                #endregion

                traceData.Status = MfcUploadFileStatus.CriteriaSetPlat5;
                traceData.SaveLogFile();
                #region Критерий #9 - В данных МФЦ неверная общая площадь квартиры
                string flatOplMismatchFn = CrossDBSQL.ResolveStoredProcedureName("PCKG_MFC.Criteria_Set_Plat_FlatOplMismatch");
                DbCommand flatOplMismatchCommand = DBMngr.Realty.GetStoredProcCommand(flatOplMismatchFn, inputFileIds);
                DBMngr.Realty.ExecuteNonQuery(flatOplMismatchCommand);
                #endregion
            }
        }
        
        private void LoadToDb(OMInputFile inputFile)
        {
            #region Проверка переданных параметров
            if (inputFile == null)
            {
                return;
            }
            if (!inputFile.PeriodRegDate.HasValue)
            {
                throw new Exception($"Импорт невозможен. Для файла загрузки с идентификатором {inputFile.EmpId} не указан период");
            }
            #endregion

            string schemaName = "public";

            using (Stream fs = File.OpenRead(GetMfcDataFullFileName(inputFile)))
            {

                switch (inputFile.TypeFile_Code)
                {
                    case TypeFile.BankPayment:
                        #region Конфигурация импорта банковских оплат
                        string bankPlatTableName = $"tmp_insur_bank_plat_{inputFile.EmpId}";
                        Dictionary<string, string> bankPlatColumns = new Dictionary<string, string>
                        {
                            { "emp_id", "bigint" },
                            { "link_id_file", "bigint" },
                            { "link_svod_bank", "bigint" },
                            { "kodpl", "varchar(255)" },
                            { "period", "timestamp" },
                            { "nom_doc", "varchar(255)" },
                            { "sum_all", "numeric" },
                            { "kom_bank_all", "numeric" },
                            { "bic_bank", "bigint" },
                            { "data_pp", "timestamp" },
                            { "cod_doc", "bigint" },
                            { "kod_ysl", "bigint" },
                            { "kod_post", "bigint" },
                            { "sum_by_code", "numeric" },
                            { "doc_period", "timestamp" },
                            { "flag_vozvr", "bigint" },
                            { "type_opl", "bigint" },
                            { "kod_ypravl", "bigint" },
                            { "flag_nach", "bigint" },
                            { "district_id", "bigint" },
                            { "period_reg_date", "timestamp" }
                        };

                        PostgreSQLCopyHelper<OMBankPlat> bankPlatCopyHelper = new PostgreSQLCopyHelper<OMBankPlat>("public", bankPlatTableName)
                            .MapBigInt("emp_id", x => x.EmpId)
                            .MapBigInt("link_id_file", x => x.LinkIdFile)
                            .MapBigInt("link_svod_bank", x => x.LinkSvodBank)
                            .MapVarchar("kodpl", x => x.Kodpl)
                            .MapTimeStamp("period", x => x.Period)
                            .MapVarchar("nom_doc", x => x.NomDoc)
                            .MapNumeric("sum_all", x => x.SumAll)
                            .MapNumeric("kom_bank_all", x => x.KomBankAll)
                            .MapBigInt("bic_bank", x => x.BicBank)
                            .MapTimeStamp("data_pp", x => x.DataPp)
                            .MapBigInt("cod_doc", x => x.CodDoc)
                            .MapBigInt("kod_ysl", x => x.KodYsl)
                            .MapBigInt("kod_post", x => x.KodPost)
                            .MapNumeric("sum_by_code", x => x.SumByCode)
                            .MapTimeStamp("doc_period", x => x.DocPeriod)
                            .MapBigInt("flag_vozvr", x => x.FlagVozvr)
                            .MapBigInt("type_opl", x => x.TypeOpl)
                            .MapBigInt("kod_ypravl", x => x.KodYpravl)
                            .MapBigInt("flag_nach", x => x.FlagNach)
                            .MapBigInt("district_id", x => x.DistrictId)
                            .MapTimeStamp("period_reg_date", x => x.PeriodRegDate);
                        #endregion

                        try
                        {
                            DropTempTable(schemaName, bankPlatTableName);
                            CreateTempTable(schemaName, bankPlatTableName, bankPlatColumns);

                            string codPost;
                            string checkLine;
                            decimal? totalSum;

                            OMSvodBank svodBank = OMSvodBank.Where(x => x.LinkIdFile == inputFile.EmpId).SelectAll().ExecuteFirstOrDefault();
                            if (svodBank == null)
                            {
                                svodBank = new OMSvodBank();
                                svodBank.LinkIdFile = inputFile.EmpId;
                                svodBank.FileName = inputFile.FileName;

                                Match match = new Regex(_oplFileRegex).Match(inputFile.FileName);
                                if (match.Success)
                                {
                                    svodBank.BankDay = new DateTime(inputFile.PeriodRegDate.Value.Year, match.Groups[2].Value.ParseToInt(), match.Groups[3].Value.ParseToInt());
                                }
                                svodBank.DistrictId = inputFile.DistrictId;
                                svodBank.Save();
                            }

                            List<OMBankPlat> bankPlatList = GetBankPlatsList(fs, out codPost, out checkLine, out totalSum,
                                inputFile.EmpId, svodBank.EmpId, inputFile.DistrictId, inputFile.PeriodRegDate);


                            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Realty"].ConnectionString))
                            {
                                connection.Open();
                                bankPlatCopyHelper.SaveAll(connection, bankPlatList);
                            }

                            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                            {
                                DbCommand bankPlatInsertCommand = DBMngr.Realty.GetSqlStringCommand($@"insert into insur_bank_plat({string.Join(",", bankPlatColumns.Select(x => x.Key))})
select nextval('reg_object_seq'),{string.Join(",", bankPlatColumns.Where(x => x.Key != "emp_id").Select(x => x.Key))}
from {bankPlatTableName}");
                                DBMngr.Realty.ExecuteNonQuery(bankPlatInsertCommand);

                                svodBank.Str = checkLine.Substring(1, 8).TrimStart('0').ParseToLong();
                                svodBank.PaySum = GetDecimalString(checkLine.Substring(9, 10), checkLine.Substring(19, 2)).ParseToDecimal();
                                svodBank.CodPost = codPost;
                                svodBank.Save();

                                inputFile.SumAll = totalSum;
                                inputFile.CountStr = bankPlatList.Count;
                                inputFile.Status_Code = UFKFileProcessingStatus.Loaded;
                                inputFile.Save();

                                ts.Complete();
                            }
                        }
                        finally
                        {
                            DropTempTable(schemaName, bankPlatTableName);
                        }
                        break;
                    case TypeFile.Nach:
                        #region Конфигурация импорта начислений
                        string nachTableName = $"tmp_insur_input_nach_{inputFile.EmpId}";
                        Dictionary<string, string> nachColumns = new Dictionary<string, string>
                        {
                            { "emp_id", "bigint" },
                            { "link_id_file", "bigint" },
                            { "fsp_id", "bigint" },
                            { "type_source", "varchar(255)" },
                            { "status_identif", "varchar(255)" },
                            { "period_reg_date", "timestamp" },
                            { "period", "timestamp" },
                            { "district_id", "bigint" },
                            { "kod", "bigint" },
                            { "unom", "bigint" },
                            { "adres_t", "varchar(2000)" },
                            { "unkva", "varchar(255)" },
                            { "nomi", "varchar(255)" },
                            { "nom", "varchar(255)" },
                            { "kvnom", "varchar(255)" },
                            { "flat_status_id", "bigint" },
                            { "flat_type_id", "bigint" },
                            { "kolgp", "bigint" },
                            { "fopl", "numeric" },
                            { "opl", "numeric" },
                            { "kodpl", "varchar(255)" },
                            { "ls", "bigint" },
                            { "sum_nach", "numeric" },
                            { "flag_unom_no", "bigint" },
                            { "fio", "varchar(255)" },
                            { "type_source_code", "bigint" },
                            { "status_identif_code", "bigint" },
                            { "load_status", "varchar(255)" },
                            { "load_status_code", "bigint" },
                            { "criteria_json", "text" }
                        };

                        PostgreSQLCopyHelper<OMInputNach> insurInputNachCopyHelper = new PostgreSQLCopyHelper<OMInputNach>("public", nachTableName)
                            .MapBigInt("emp_id", x => x.EmpId)
                            .MapBigInt("link_id_file", x => x.LinkIdFile)
                            .MapBigInt("fsp_id", x => x.FspId)
                            .MapVarchar("type_source", x => x.TypeSource)
                            .MapVarchar("status_identif", x => x.StatusIdentif)
                            .MapTimeStamp("period_reg_date", x => x.PeriodRegDate)
                            .MapBigInt("unom", x => x.Unom)
                            .MapBigInt("district_id", x => x.DistrictId)
                            .MapVarchar("adres_t", x => x.AdresT)
                            .MapVarchar("unkva", x => x.Unkva)
                            .MapVarchar("nomi", x => x.Nomi)
                            .MapVarchar("nom", x => x.Nom)
                            .MapVarchar("kvnom", x => x.Kvnom)
                            .MapVarchar("kodpl", x => x.Kodpl)
                            .MapBigInt("flat_status_id", x => x.FlatStatusId)
                            .MapBigInt("flat_type_id", x => x.FlatTypeId)
                            .MapBigInt("kolgp", x => x.Kolgp)
                            .MapNumeric("fopl", x => x.Fopl)
                            .MapNumeric("opl", x => x.Opl)
                            .MapBigInt("ls", x => x.Ls)
                            .MapNumeric("sum_nach", x => x.SumNach)
                            .MapBigInt("flag_unom_no", x => x.FlagUnomNo)
                            .MapVarchar("fio", x => x.Fio)
                            .MapBigInt("type_source_code", x => (long?)x.TypeSource_Code)
                            .MapBigInt("status_identif_code", x => (long?)x.StatusIdentif_Code)
                            .MapVarchar("load_status", x => x.LoadStatus)
                            .MapBigInt("load_status_code", x => (long?)x.LoadStatus_Code)
                            .MapText("criteria_json", x => x.CriteriaJson);
                        #endregion

                        try
                        {
                            DropTempTable(schemaName, nachTableName);
                            CreateTempTable(schemaName, nachTableName, nachColumns);

                            long countStr;
                            List<OMInputNach> inputNachList = GetInputNachList(fs, out countStr, inputFile.EmpId, inputFile.DistrictId, inputFile.PeriodRegDate, inputFile.ParentId);

                            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Realty"].ConnectionString))
                            {
                                connection.Open();
                                insurInputNachCopyHelper.SaveAll(connection, inputNachList);
                            }

                            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                            {
                                DbCommand nachInsertCommand = DBMngr.Realty.GetSqlStringCommand($@"insert into insur_input_nach({string.Join(",", nachColumns.Select(x => x.Key))})
select nextval('reg_object_seq'),{string.Join(",", nachColumns.Where(x => x.Key != "emp_id").Select(x => x.Key))}
from {nachTableName}");
                                DBMngr.Realty.ExecuteNonQuery(nachInsertCommand);

                                inputFile.CountStr = countStr;
                                inputFile.CountStrLoad = inputNachList.Count;
                                inputFile.SumAll = inputNachList.Sum(x=> x.SumNach);
                                inputFile.Status_Code = UFKFileProcessingStatus.Loaded;
                                inputFile.Save();

                                ts.Complete();
                            }
                        }
                        finally
                        {
                            DropTempTable(schemaName, nachTableName);
                        }
                        break;
                    case TypeFile.Strah:
                        #region Конфигурация импорта зачислений
                        string platTableName = $"tmp_insur_input_plat_{inputFile.EmpId}";
                        Dictionary<string, string> platColumns = new Dictionary<string, string>
                        {
                            { "emp_id", "bigint" },
                            { "link_id_file", "bigint" },
                            { "fsp_id", "bigint" },
                            { "link_bank_id", "bigint" },
                            { "unom", "bigint" },
                            { "adres", "varchar(2000)" },
                            { "nom", "varchar(255)" },
                            { "kodpl", "varchar(255)" },
                            { "ls", "bigint" },
                            { "tx_id", "varchar(255)" },
                            { "pmt_date", "timestamp" },
                            { "date_in_tofk", "timestamp" },
                            { "period", "timestamp" },
                            { "period_reg_date", "timestamp" },
                            { "sum_nach", "numeric" },
                            { "sum_opl", "numeric" },
                            { "fee", "numeric" },
                            { "opl", "numeric" },
                            { "flat_type_id", "bigint" },
                            { "fio", "varchar(255)" },
                            { "comment", "varchar(4000)" },
                            { "status_identif", "varchar(255)" },
                            { "type_source", "varchar(255)" },
                            { "type_source_code", "bigint" },
                            { "flag_unom_no", "bigint" },
                            { "type_doc", "varchar(255)" },
                            { "type_doc_code", "bigint" },
                            { "kod", "bigint" },
                            { "ndog", "varchar(255)" },
                            { "ndogdat", "timestamp without time zone" },
                            { "ndops", "varchar(255)" },
                            { "paynumber", "varchar(255)" },
                            { "status_identif_code", "bigint" },
                            { "district_id", "bigint" },
                            { "load_status", "varchar(255)" },
                            { "load_status_code", "bigint" },
                            { "criteria_json", "text" },
                            { "insurance_organization_id", "bigint"}
                        };

                        PostgreSQLCopyHelper<OMInputPlat> insurInputPlatCopyHelper = new PostgreSQLCopyHelper<OMInputPlat>("public", platTableName)
                            .MapBigInt("emp_id", x => x.EmpId)
                            .MapBigInt("link_id_file", x => x.LinkIdFile)
                            .MapBigInt("fsp_id", x => x.FspId)
                            .MapBigInt("link_bank_id", x => x.LinkBankId)
                            .MapBigInt("unom", x => x.Unom)
                            .MapVarchar("adres", x => x.Adres)
                            .MapVarchar("nom", x => x.Nom)
                            .MapVarchar("kodpl", x => x.Kodpl)
                            .MapBigInt("ls", x => x.Ls)
                            .MapVarchar("tx_id", x => x.TxId)
                            .MapTimeStamp("pmt_date", x => x.PmtDate)
                            .MapTimeStamp("date_in_tofk", x => x.DateInTofk)
                            .MapTimeStamp("period", x => x.Period)
                            .MapTimeStamp("period_reg_date", x => x.PeriodRegDate)
                            .MapNumeric("sum_nach", x => x.SumNach)
                            .MapNumeric("sum_opl", x => x.SumOpl)
                            .MapNumeric("fee", x => x.Fee)
                            .MapNumeric("opl", x => x.Opl)
                            .MapBigInt("flat_type_id", x => x.FlatTypeId)
                            .MapVarchar("fio", x => x.Fio)
                            .MapVarchar("comment", x => x.Comment)
                            .MapVarchar("status_identif", x => x.StatusIdentif)
                            .MapBigInt("type_source_code", x => (long?)x.TypeSource_Code)
                            .MapVarchar("type_source", x => x.TypeSource)
                            .MapBigInt("flag_unom_no", x => x.FlagUnomNo)
                            .MapVarchar("type_doc", x => x.TypeDoc)
                            .MapBigInt("type_doc_code", x => (long?)x.TypeDoc_Code)
                            .MapBigInt("kod", x => x.Kod)
                            .MapBigInt("insurance_organization_id", x => x.InsuranceOrganizationId)
                            .MapVarchar("ndog", x => x.Ndog)
                            .MapTimeStamp("ndogdat", x => x.Ndogdat)
                            .MapVarchar("ndops", x => x.Ndops)
                            .MapVarchar("paynumber", x => x.Paynumber)
                            .MapBigInt("status_identif_code", x => (long?)x.StatusIdentif_Code)
                            .MapBigInt("district_id", x => x.DistrictId)
                            .MapVarchar("load_status", x => x.LoadStatus)
                            .MapBigInt("load_status_code", x => (long?)x.LoadStatus_Code)
                            .MapText("criteria_json", x => x.CriteriaJson);
                        #endregion

                        try
                        {
                            DropTempTable(schemaName, platTableName);
                            CreateTempTable(schemaName, platTableName, platColumns);

                            long countStr;
                            List<OMInputPlat> inputPlatList = GetInputPlatList(fs, out countStr, inputFile.EmpId, inputFile.DistrictId, inputFile.PeriodRegDate, inputFile.ParentId);

                            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Realty"].ConnectionString))
                            {
                                connection.Open();
                                insurInputPlatCopyHelper.SaveAll(connection, inputPlatList);
                            }

                            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                            {
                                DbCommand nachInsertCommand = DBMngr.Realty.GetSqlStringCommand($@"insert into insur_input_plat({string.Join(",", platColumns.Select(x => x.Key))})
select nextval('reg_object_seq'),{string.Join(",", platColumns.Where(x => x.Key != "emp_id").Select(x => x.Key))}
from {platTableName}");
                                DBMngr.Realty.ExecuteNonQuery(nachInsertCommand);

                                inputFile.CountStr = countStr;
                                inputFile.CountStrLoad = inputPlatList.Count;
                                inputFile.SumAll = inputPlatList.Sum(x => x.SumOpl);
                                inputFile.Status_Code = UFKFileProcessingStatus.Loaded;
                                inputFile.Save();

                                ts.Complete();
                            }
                        }
                        finally
                        {
                            DropTempTable(schemaName, platTableName);
                        }
                        break;
                    default:
                        throw new Exception($"Импорт невозможен. Неподдерживаемый тип файла. ИД {inputFile.EmpId}");
                }
            }
        }

        /// <summary>
        /// Создает таблицу в базе данных
        /// </summary>
        /// <param name="schemaName">Схема (по-умолчанию public)</param>
        /// <param name="tableName">Название таблицы</param>
        /// <param name="columns">Описание колонок</param>
        private void CreateTempTable(string schemaName, string tableName, Dictionary<string, string> columns)
        {
            StringBuilder query = new StringBuilder();
            query.Append($"create table {(schemaName.IsNotEmpty() ? schemaName : "public")}.{tableName} (");
            query.AppendLine();
            foreach(KeyValuePair<string, string> column in columns)
            {
                query.AppendLine($"{column.Key} {column.Value},");
            }
            query.Remove(query.Length - 3, 3);
            query.Append(") with (oids = false);");
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(query.ToString());
            DBMngr.Realty.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Удаляет таблицу из базы данных
        /// </summary>
        /// <param name="schemaName">Схема (по-умолчанию public)</param>
        /// <param name="tableName">Название таблицы</param>
        private void DropTempTable(string schemaName, string tableName)
        {
            DbCommand command = DBMngr.Realty.GetSqlStringCommand($"drop table if exists {(schemaName.IsNotEmpty() ? schemaName : "public")}.{tableName}");
            DBMngr.Realty.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Сохранение файла из потока
        /// </summary>
        /// <param name="stream">Поток для сохранения</param>
        /// <param name="savePath">Путь для сохранения</param>
        private void SaveStream(Stream stream, string savePath, bool rewrite = false)
        {
            if (!File.Exists(savePath))
            {
                string directory = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (FileStream fileStream = File.Create(savePath))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }
            else if (rewrite)
            {
                File.Delete(savePath);

                using (FileStream fileStream = File.Create(savePath))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }

        }

        private long GetFilePackage(long okrugId, DateTime periodRegDate)
        {
            if (_okrugFilePackage.ContainsKey(okrugId))
            {
                return _okrugFilePackage[okrugId];
            }

            OMInputFilePackage filePackage = OMInputFilePackage
                .Where(x => x.OkrugId == okrugId && x.PeriodRegDate == periodRegDate)
                .Execute()
                .FirstOrDefault();

            if (filePackage == null)
            {
                filePackage = new OMInputFilePackage();
                filePackage.OkrugId = okrugId;
                filePackage.PeriodRegDate = periodRegDate;
                filePackage.CountDistrict = Districts.Count(x => x.OkrugId == okrugId);
                filePackage.Save();
            }

            _okrugFilePackage.Add(okrugId, filePackage.Id);

            return filePackage.Id;
        }

        /// <summary>
        /// Создает файл для загрузки МФЦ
        /// </summary>
        /// <param name="sourceType">Тип источника</param>
        /// <param name="fileName">Наименованеи файла</param>
        /// <param name="stream">Файл</param>
        /// <param name="periodRegDate">Дата учета</param>
        /// <returns></returns>
        private OMInputFile CreateInputFile(InsuranceSourceType sourceType, string fileName, Stream stream, DateTime periodRegDate)
        {
            switch(sourceType)
            {
                case InsuranceSourceType.Mfc:
                    return CreateMfcInputFile(fileName, stream, periodRegDate);
                case InsuranceSourceType.Bank:
                    return CreateBankPlatInputFile(fileName, stream, periodRegDate);
                default:
                    throw new Exception("Неподдерживаемый формат файла");
            }
        }

        /// <summary>
        /// Создает файл МФЦ зачисления/начисления
        /// </summary>
        /// <param name="fileName">Наименованеи файла</param>
        /// <param name="stream">Файл</param>
        /// <param name="periodRegDate">Дата учета</param>
        /// <returns></returns>
        private OMInputFile CreateMfcInputFile(string fileName, Stream stream, DateTime periodRegDate)
        {
            Match nachMatch = new Regex(_nachDataFileRegex).Match(fileName);
            Match strahMatch = new Regex(_strahDataFileRegex).Match(fileName);
            Match commonNachMatch = new Regex(_commonNachDataFileRegex).Match(fileName);
            Match commonStrahMatch = new Regex(_commonStrahDataFileRegex).Match(fileName);

            long districtCode;

            if (nachMatch.Success || commonNachMatch.Success)
            {
                long countStr;
                List<OMInputNach> inputNachList = GetInputNachList(stream, out countStr);

                if (inputNachList.Count == 0)
                {
                    throw new Exception($"Не удалось определить ни одной строки для файла начислений {fileName}");
                }

                if (nachMatch.Success)
                {
                    districtCode = nachMatch.Groups[1].Value.ParseToLong();
                }
                //CIPJS-216 Исправить проблему загрузки файлов по Зеленограду ( иное название)
                //CIPJS-275 За ноябрь не загрузились файлы с начислениями и зачислениями (в первую очередь разобраться с ДВУМЯ округами ( НОЯБРЬ)  СВАО и ТИНАО)
                else
                {
                    //CIPJS-275 определяем район по первой строчке файла .dbf, код плательщика там "должен" быть!
                    //CIPJS-275 берем первые три цифры кода плательщика для определения района
                    string kodPl = inputNachList.FirstOrDefault(x => x.Kodpl.IsNotEmpty() && x.Kodpl.Length >= 3)?.Kodpl;

                    if (kodPl == null)
                    {
                        throw new Exception($"Невозможно определить район для {fileName}, т.к. ни для одной строки не заполнен код плательщика");
                    }
                    
                    districtCode = kodPl.Substring(0, 3).ParseToLong();
                }
                
                return CreateMfcInputFile(fileName, districtCode, periodRegDate, TypeFile.Nach, countStr, inputNachList.Sum(x => x.SumNach));
            }
            else if (strahMatch.Success || commonStrahMatch.Success)
            {
                long countStr;
                List<OMInputPlat> inputPlatList = GetInputPlatList(stream, out countStr);

                if (inputPlatList.Count == 0)
                {
                    throw new Exception($"Не удалось определить ни одной строки для файла зачислений {fileName}");
                }

                if (strahMatch.Success)
                {
                    districtCode = strahMatch.Groups[1].Value.ParseToLong();
                }
                //CIPJS-216 Исправить проблему загрузки файлов по Зеленограду ( иное название)
                //CIPJS-275 За ноябрь не загрузились файлы с начислениями и зачислениями (в первую очередь разобраться с ДВУМЯ округами ( НОЯБРЬ)  СВАО и ТИНАО)
                else
                {
                    //CIPJS-275 определяем район по первой строчке файла .dbf, код плательщика там "должен" быть!
                    //CIPJS-275 берем первые три цифры кода плательщика для определения района
                    string kodPl = inputPlatList.FirstOrDefault(x => x.Kodpl.IsNotEmpty() && x.Kodpl.Length >= 3)?.Kodpl;

                    if (kodPl == null)
                    {
                        throw new Exception("Невозможно определить район, т.к. ни для одной строки не заполнен код плательщика");
                    }

                    districtCode = kodPl.Substring(0, 3).ParseToLong();
                }
                
                return CreateMfcInputFile(fileName, districtCode, periodRegDate, TypeFile.Strah, countStr, inputPlatList.Sum(x => x.SumOpl));
            }
            else
            {
                throw new Exception($"Не удалось определить формат файла для загружаемого начисления/зачисления {fileName}");
            }
        }

        /// <summary>
        /// Создает файл банковских оплат
        /// </summary>
        /// <param name="fileName">Наименованеи файла</param>
        /// <param name="stream">Файл</param>
        /// <param name="periodRegDate">Дата учета</param>
        /// <returns></returns>
        private OMInputFile CreateBankPlatInputFile(string fileName, Stream stream, DateTime periodRegDate)
        {
            Match match = new Regex(_oplFileRegex).Match(Path.GetFileName(fileName));

            if (!match.Success)
            {
                throw new Exception($"Не удалось определить формат названия для банковского файла оплат {fileName}");
            }

            OMDistrict district = Districts.FirstOrDefault(x => x.Code == match.Groups[1].Value.ParseToLong());

            if (district == null)
            {
                throw new Exception($"Не удалось определить код района для файла {fileName}");
            }

            if (!district.OkrugId.HasValue)
            {
                throw new Exception($"Не заполнен идентификатор округа для района с кодом {district.Code}");
            }

            long inputFilePackageId = GetFilePackage(district.OkrugId.Value, periodRegDate);

            //CIPJS-275 определяем код поставщика по первой строчке файла .001, код поставщика там "должен" быть!
            string codPost;
            string checkLine;
            decimal? totalSum;

            List<OMBankPlat> bankPlatList = GetBankPlatsList(stream, out codPost, out checkLine, out totalSum);

            if (bankPlatList.Count == 0)
            {
                throw new Exception($"Не удалось определить ни одной строки файла для банковского файла оплат {fileName}." +
                    $" Не возможно определить код поставщика.");
            }

            long kodPost = codPost.ParseToLong(); 

            if (kodPost == 0)
            {
                throw new Exception($"Не удалось определить код поставщика для файла {fileName}");
            }

            return new OMInputFile
            {
                FileName = fileName,
                TypeFile_Code = TypeFile.BankPayment,
                PeriodRegDate = periodRegDate,
                TypeSource_Code = InsuranceSourceType.Bank,
                DateInput = DateTime.Now,
                Status_Code = UFKFileProcessingStatus.None,
                DistrictId = district.Id,
                LinkPackage = inputFilePackageId,
                KodPost = kodPost,
                SumAll = totalSum,
                CountStr = bankPlatList.Count
            };
        }

        /// <summary>
        /// Создаем файл загрузки, заранее заполняем количество строк и сумму
        /// </summary>
        /// <param name="fileName">Наименование файла</param>
        /// <param name="districtCode">Код района</param>
        /// <param name="periodRegDate">Периоду учета</param>
        /// <param name="typeFile">Тип файла</param>
        /// <param name="countStr">Кол-во строк</param>
        /// <param name="totalSum">Общая сумма</param>
        /// <returns></returns>
        private OMInputFile CreateMfcInputFile(string fileName, long districtCode, DateTime periodRegDate, TypeFile typeFile, long? countStr, decimal? totalSum)
        {
            if (districtCode == 0)
            {
                throw new Exception($"Не удалось определить код района для файла {fileName}");
            }

            OMDistrict district = Districts.FirstOrDefault(x => x.Code == districtCode);

            if (district == null)
            {
                throw new Exception($"Не удалось определить район по коду {districtCode}");
            }

            if (!district.OkrugId.HasValue)
            {
                throw new Exception($"Не заполнен идентификатор округа для района с кодом {district.Code}");
            }

            long inputFilePackageId = GetFilePackage(district.OkrugId.Value, periodRegDate);

            return new OMInputFile
            {
                FileName = fileName,
                TypeFile_Code = typeFile,
                PeriodRegDate = periodRegDate,
                TypeSource_Code = InsuranceSourceType.Mfc,
                DateInput = DateTime.Now,
                Status_Code = UFKFileProcessingStatus.None,
                DistrictId = district.Id,
                LinkPackage = inputFilePackageId,
                CountStr = countStr,
                SumAll = totalSum
            };
        }
    }
}
