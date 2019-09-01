using CIPJS.DAL.FileStorage;
using Core.ErrorManagment;
using Core.FastDBF;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;

namespace CIPJS.DAL.SK
{
    public class SkImportLoadProcess : ILongProcess
    {
        private readonly Encoding _OEMEncoding = Encoding.GetEncoding(new CultureInfo("ru-RU").TextInfo.OEMCodePage);
        private readonly int _packageSize = 10000;
		
        private OMQueue _omQueue;

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            _omQueue = processQueue;
            long fileId = processQueue.ObjectId.Value;
            OMFileStorage omFileStorage = OMFileStorage.Where(x => x.Id == fileId).SelectAll().Execute().FirstOrDefault();

            if (omFileStorage == null) throw new Exception($"Не удалось определить файл по идентификатору {fileId}");

            string fullFileName = Path.Combine(FileStorageService.FileStorageFolder, omFileStorage.Id.ToString());

            if (!File.Exists(fullFileName)) throw new Exception($"Файл отстуствует в каталоге загрузки (FileStorageFolder)");

            string fileName = omFileStorage.Filename.ToLower();

            SetOMQueuePercent(1);

            try
            {
                if (fileName == "policy.dbf")
                {
                    DataTable dataTable = ReadDbfFile(File.OpenRead(fullFileName), omFileStorage.Filename);
                    ImportPolicy(dataTable, omFileStorage.PeriodRegDate, fileId);
                }
                else if (fileName == "policy_d.dbf")
                {
                    DataTable dataTable = ReadDbfFile(File.OpenRead(fullFileName), omFileStorage.Filename);
                    ImportPolicyD(dataTable, omFileStorage.PeriodRegDate, fileId);
                }
                else if (fileName == "svd.dbf")
                {
                    DataTable dataTable = ReadDbfFile(File.OpenRead(fullFileName), omFileStorage.Filename);
                    ImportSvd(dataTable, omFileStorage.PeriodRegDate, fileId);
                }
                else if (fileName == "pl.dbf")
                {
                    ImportPl(omFileStorage.Filename, File.OpenRead(fullFileName), omFileStorage.PeriodRegDate, fileId);
                }
                else if (fileName == "pl_no.dbf")
                {
                    ImportPlNo(omFileStorage.Filename, File.OpenRead(fullFileName), omFileStorage.PeriodRegDate, fileId);
                }
                else if (fileName == "comm.dbf")
                {
                    ImportComm(omFileStorage.Filename, File.OpenRead(fullFileName), omFileStorage.PeriodRegDate, fileId);
                }
                else if (fileName == "comm_dop.dbf")
                {
                    ImportCommDop(omFileStorage.Filename, File.OpenRead(fullFileName), omFileStorage.PeriodRegDate, fileId);
                }
                else if (fileName == "pay_comm.dbf")
                {
                    ImportPayComm(omFileStorage.Filename, File.OpenRead(fullFileName), omFileStorage.PeriodRegDate, fileId);
                }
                else if (fileName == "pl_comm.dbf")
                {
                    ImportPlComm(omFileStorage.Filename, File.OpenRead(fullFileName), omFileStorage.PeriodRegDate, fileId);
                }
                else if (fileName == "pl_comm_no.dbf")
                {
                    ImportPlCommNo(omFileStorage.Filename, File.OpenRead(fullFileName), omFileStorage.PeriodRegDate, fileId);
                }
                else
                {
                    throw new Exception($"Для файла {omFileStorage.Filename} нет обработчика");
                }
            }
            catch
            {
                omFileStorage.Destroy();
                throw;
            }
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null) { }

        public bool Test() { return true; }

        private void SetOMQueueMessage(string message)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Suppress))
            {
                _omQueue.Message = message;
                _omQueue.Save();
            }
        }

        private void SetOMQueuePercent(int percent)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Suppress))
            {
                _omQueue.Status = 2;
                _omQueue.Message = Math.Min(Math.Max(1, percent), 100).ToString();
                _omQueue.Save();
            }
        }

        public DataTable ReadDbfFile(Stream fileStream, string fileName, bool isCheck = false)
        {
            DataTable dataTable = new DataTable(fileName);
            DbfFile dbfFile = new DbfFile(Encoding.GetEncoding(new CultureInfo("ru-RU").TextInfo.OEMCodePage));

            dbfFile.Open(fileStream);

            DbfRecord dbfRecord = dbfFile.ReadNext();

            if (dbfRecord == null) return dataTable;

            for (int i = 0; i < dbfRecord.ColumnCount; i++)
            {
                dataTable.Columns.Add(dbfRecord.Column(i).Name);
            }

            for (; dbfRecord != null; dbfRecord = dbfFile.ReadNext())
            {
                DataRow dataRow = dataTable.NewRow();

                for (int i = 0; i < dbfRecord.ColumnCount; i++)
                {
                    dataRow[i] = dbfRecord[i];
                }

                if (dataRow.ItemArray.Any(x => x != null && x.ToString().IsNotEmpty()))
                {
                    dataTable.Rows.Add(dataRow);
                }

                if (isCheck) return dataTable;
            }

            return dataTable;
        }

        private void ImportPolicy(DataTable dataTable, DateTime? period, long fileStorageId)
        {
            int successCount = 0, existsCount = 0, errorCount = 0;
            List<string> errorMessages = new List<string>();
            List<OMOkrug> omOkrugs = OMOkrug.Where(x => 1).Select(x => x.Code).Execute();
            List<OMDistrict> omDistricts = OMDistrict.Where(x => 1).Select(x => x.Code).Execute();
            List<OMInsuranceOrganization> omInsuranceOrganizations = OMInsuranceOrganization.Where(x => 1).Select(x => x.Code).Execute();
            OMInputFile omInputFile = new OMInputFile
            {
                FileName = dataTable.TableName,
                TypeFile_Code = TypeFile.Policy,
                PeriodRegDate = period,
                TypeSource_Code = InsuranceSourceType.Sk,
                DateInput = DateTime.Now,
                CountStr = dataTable.Rows.Count,
                Status_Code = UFKFileProcessingStatus.Loaded,
                UserId = SRDSession.Current.UserID,
                FileStorageId = fileStorageId,
                InsuranceOrganizationId = dataTable.Rows.Count > 0
                    ? omInsuranceOrganizations.FirstOrDefault(x => x.Code == dataTable.Rows[0]["kod"].ParseToLong())?.Id : null
            };

            omInputFile.Save();

            for (int i = 0; i < dataTable.Rows.Count; i += _packageSize)
            {
                int maxIndex = Math.Min(i + _packageSize, dataTable.Rows.Count);
                List<string> npolList = new List<string>(_packageSize);
                List<DateTime?> datList = new List<DateTime?>(_packageSize);
                int tempExistsCount = 0;

                try
                {
                    for (int j = i; j < maxIndex; j++)
                    {
                        npolList.Add(dataTable.Rows[j]["npol"]?.ToString());
                        datList.Add(dataTable.Rows[j]["dat"]?.ParseToDateTime());
                    }

                    npolList = npolList.Distinct().ToList();
                    datList = datList.Distinct().ToList();

                    List<OMPolicySvd> omPolicySvds = npolList.Any() && datList.Any()
                        ? OMPolicySvd.Where(x => npolList.Contains(x.Npol) && datList.Contains(x.Dat)).Select(x => x.Npol).Select(x => x.Dat).Execute()
                        : new List<OMPolicySvd>();
                    StringBuilder commandText = new StringBuilder("INSERT INTO INSUR_POLICY_SVD (EMP_ID, LINK_ID_FILE, TYPE_DOC_CODE, TYPE_DOC, INSURANCE_ORGANIZATION_ID, OKRUG_ID, DISTRICT_ID, ORG_ID, ORG_TYPE, PLAT_ID, UNOM, UNKVA, NOM, NOMI, KVNOM, KOLGP, FLATSTATUS, FOPL, OPL, LS, NPOL, DAT, PRALT_CODE, PRALT, SS, KOLDS, SOPLVZ) VALUES ");
                    bool needExecute = false;

                    for (int j = i; j < maxIndex; j++)
                    {
                        DataRow dataRow = dataTable.Rows[j];
                        string npol = dataRow["npol"]?.ToString();
                        DateTime? dat = dataRow["dat"]?.ParseToDateTime();

                        if (omPolicySvds.Any(x => x.Npol == npol && x.Dat == dat))
                        {
                            tempExistsCount++;
                            continue;
                        }

                        if (needExecute) commandText = commandText.Append(",");
                        else needExecute = true;

                        OMOkrug omOkrug = omOkrugs.FirstOrDefault(x => x.Code == dataRow["aok"].ParseToLong());
                        OMDistrict omDistrict = omDistricts.FirstOrDefault(x => x.Code == dataRow["mok"].ParseToLong());
                        OMInsuranceOrganization omInsuranceOrganization = omInsuranceOrganizations.FirstOrDefault(x => x.Code == dataRow["kod"].ParseToLong());
                        long? pralt = dataRow["pralt"]?.ParseToDecimal().ParseToLong();
                        InsuranceTerms insuranceTerms = pralt == 0 ? InsuranceTerms.Common
                            : (pralt == 1 ? InsuranceTerms.Special : InsuranceTerms.None);

                        commandText = commandText.Append($@"(nextval('reg_object_seq'), {omInputFile.EmpId}, {(long)DocumentType.Polis}, '{EnumExtensions.GetEnumDescription(DocumentType.Polis)}', {(omInsuranceOrganization != null ? omInsuranceOrganization.Id.ToString() : "NULL")}, {(omOkrug != null ? omOkrug.Id.ToString() : "NULL")}, {(omDistrict != null ? omDistrict.Id.ToString() : "NULL")}, {GetDBValue(dataRow["org_id"])}, {GetDBValue(dataRow["org"])}, {GetDBValue(dataRow["plat_id"])}, {GetDBValue(dataRow["unom"])}, '{dataRow["unkva"]}', '{dataRow["nom"]}', '{dataRow["nomi"]}', '{dataRow["kvnom"]?.ToString()?.Replace(".", "")}', {GetDBValue(dataRow["kolgp"])}, {GetDBValue(dataRow["flatstatus"])}, {GetDBValue(dataRow["fopl"])}, {GetDBValue(dataRow["opl"])}, {GetDBValue(dataRow["ls"])}, '{dataRow["npol"]}', {CrossDBSQL.ToDate(dat.Value, CrossDBSQL.Providers.PrvPostgres)}, {(long)insuranceTerms}, '{EnumExtensions.GetEnumDescription(insuranceTerms)}', {GetDBValue(dataRow["ss"])}, {GetDBValue(dataRow["kolds"])}, {GetDBValue(dataRow["soplvz"])})");
                    }

                    if (needExecute)
                    {
                        DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText.ToString());
                        DBMngr.Realty.ExecuteNonQuery(command);
                    }

                    successCount += maxIndex - i - tempExistsCount;
                    existsCount += tempExistsCount;
                }
                catch (Exception ex)
                {
                    errorCount += maxIndex - i;
                    ErrorManager.LogError(ex);

                    if (!errorMessages.Any(x => x == ex.Message))
                    {
                        errorMessages.Add(ex.Message);
                    }
                }

                SetOMQueuePercent(i * 100 / dataTable.Rows.Count);
            }

            string message = $"Создано строк: {successCount}\n\r" +
                $"Уже существует в системе: {existsCount}\n\r" +
                $"Количество ошибок: {errorCount}\n\r";

            if (errorMessages.Any())
            {
                message += $"Ошибки: \n\r" +
                $"{string.Join("\n\r", errorMessages)}";
            }

            SetOMQueueMessage(message);
        }

        private void ImportPolicyD(DataTable dataTable, DateTime? period, long fileStorageId)
        {
            int successCount = 0, errorCount = 0;
            List<string> errorMessages = new List<string>();
            List<string> npolList = new List<string>(dataTable.Rows.Count);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                npolList.Add(dataTable.Rows[i]["npol"]?.ToString());
            }

            npolList = npolList.Distinct().ToList();

            List<OMPolicySvd> omPolicySvds = npolList.Any()
                ? OMPolicySvd.Where(x => npolList.Contains(x.Npol) && x.DatEnd == null).Select(x => x.Npol).Execute()
                : new List<OMPolicySvd>();
            OMInputFile omInputFile = new OMInputFile
            {
                FileName = dataTable.TableName,
                TypeFile_Code = TypeFile.TerminatedPolicy,
                PeriodRegDate = period,
                TypeSource_Code = InsuranceSourceType.Sk,
                DateInput = DateTime.Now,
                CountStr = dataTable.Rows.Count,
                Status_Code = UFKFileProcessingStatus.ProcessedCompletely,
                UserId = SRDSession.Current.UserID,
                FileStorageId = fileStorageId,
                InsuranceOrganizationId = omPolicySvds.FirstOrDefault()?.InsuranceOrganizationId
            };

            omInputFile.Save();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                try
                {
                    DataRow dataRow = dataTable.Rows[i];
                    string npol = dataRow["npol"]?.ToString();
                    OMPolicySvd omPolicySvd = omPolicySvds.FirstOrDefault(x => x.Npol == npol);

                    if (omPolicySvd == null)
                    {
                        throw new Exception($"В Реестре полисов и свидетельств INSUR_POLICY_SVD не найдена строка для Полиса = {npol}. Системе не удалось сохранить данные о расторжении");
                    }

                    omPolicySvd.DatEnd = dataRow["dat_end"]?.ParseToDateTime();
                    omPolicySvd.LinkIdFileEnd = omInputFile.EmpId;
                    omPolicySvd.Save();
                    successCount++;
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    errorCount++;

                    if (!errorMessages.Any(x => x == ex.Message))
                    {
                        errorMessages.Add(ex.Message);
                    }
                }

                SetOMQueuePercent(i * 100 / dataTable.Rows.Count);
            }

            string message = $"Обработано строк: {successCount}\n\r" +
                $"Не обработано строк: {errorCount}\n\r";

            if (errorMessages.Any())
            {
                message += $"Ошибки: \n\r" +
                $"{string.Join("\n\r", errorMessages)}";
            }

            SetOMQueueMessage(message);
        }

        private void ImportSvd(DataTable dataTable, DateTime? period, long fileStorageId)
        {
            int successCount = 0, existsCount = 0, errorCount = 0;
            List<string> errorMessages = new List<string>();
            List<OMOkrug> omOkrugs = OMOkrug.Where(x => 1).Select(x => x.Code).Execute();
            List<OMDistrict> omDistricts = OMDistrict.Where(x => 1).Select(x => x.Code).Execute();
            List<OMInsuranceOrganization> omInsuranceOrganizations = OMInsuranceOrganization.Where(x => 1).Select(x => x.Code).Execute();
            OMInputFile omInputFile = new OMInputFile
            {
                FileName = dataTable.TableName,
                TypeFile_Code = TypeFile.Certificate,
                PeriodRegDate = period,
                TypeSource_Code = InsuranceSourceType.Sk,
                DateInput = DateTime.Now,
                CountStr = dataTable.Rows.Count,
                Status_Code = UFKFileProcessingStatus.Loaded,
                UserId = SRDSession.Current.UserID,
                FileStorageId = fileStorageId,
                InsuranceOrganizationId = dataTable.Rows.Count > 0
                    ? omInsuranceOrganizations.FirstOrDefault(x => x.Code == dataTable.Rows[0]["kod"].ParseToLong())?.Id : null
            };

            omInputFile.Save();

            for (int i = 0; i < dataTable.Rows.Count; i += _packageSize)
            {
                int maxIndex = Math.Min(i + _packageSize, dataTable.Rows.Count);
                List<string> npolList = new List<string>(_packageSize);
                List<DateTime?> datList = new List<DateTime?>(_packageSize);
                int tempExistsCount = 0;

                try
                {
                    for (int j = i; j < maxIndex; j++)
                    {
                        npolList.Add(dataTable.Rows[j]["npol"]?.ToString());
                        datList.Add(dataTable.Rows[j]["dat"]?.ParseToDateTime());
                    }

                    npolList = npolList.Distinct().ToList();
                    datList = datList.Distinct().ToList();

                    List<OMPolicySvd> omPolicySvds = npolList.Any() && datList.Any()
                        ? OMPolicySvd.Where(x => npolList.Contains(x.Npol) && datList.Contains(x.Dat)).Select(x => x.Npol).Select(x => x.Dat).Execute()
                        : new List<OMPolicySvd>();
                    StringBuilder commandText = new StringBuilder("INSERT INTO INSUR_POLICY_SVD (EMP_ID, LINK_ID_FILE, TYPE_DOC_CODE, TYPE_DOC, INSURANCE_ORGANIZATION_ID, OKRUG_ID, DISTRICT_ID, ORG_ID, ORG_TYPE, PLAT_ID, UNOM, UNKVA, NOM, NOMI, KVNOM, KOLGP, FOPL, OPL, LS, NPOL, DAT, VZNOS, PR, KOLDS, SOPLVZ, KODPL) VALUES ");
                    bool needExecute = false;

                    for (int j = i; j < maxIndex; j++)
                    {
                        DataRow dataRow = dataTable.Rows[j];
                        string npol = dataRow["npol"]?.ToString();
                        DateTime? dat = dataRow["dat"]?.ParseToDateTime();

                        if (omPolicySvds.Any(x => x.Npol == npol && x.Dat == dat))
                        {
                            tempExistsCount++;
                            continue;
                        }

                        if (needExecute) commandText = commandText.Append(",");
                        else needExecute = true;

                        OMOkrug omOkrug = omOkrugs.FirstOrDefault(x => x.Code == dataRow["aok"].ParseToLong());
                        OMDistrict omDistrict = omDistricts.FirstOrDefault(x => x.Code == dataRow["mok"].ParseToLong());
                        OMInsuranceOrganization omInsuranceOrganization = omInsuranceOrganizations.FirstOrDefault(x => x.Code == dataRow["kod"].ParseToLong());

                        commandText = commandText.Append($@"(nextval('reg_object_seq'), {omInputFile.EmpId}, {(long)DocumentType.Certificate}, '{EnumExtensions.GetEnumDescription(DocumentType.Certificate)}', {(omInsuranceOrganization != null ? omInsuranceOrganization.Id.ToString() : "NULL")}, {(omOkrug != null ? omOkrug.Id.ToString() : "NULL")}, {(omDistrict != null ? omDistrict.Id.ToString() : "NULL")}, {GetDBValue(dataRow["org_id"])}, {GetDBValue(dataRow["org"])}, {GetDBValue(dataRow["plat_id"])}, {GetDBValue(dataRow["unom"])}, '{dataRow["unkva"]}', '{dataRow["nom"]}', '{dataRow["nomi"]}', '{dataRow["kvnom"]}', {GetDBValue(dataRow["kolgp"])}, {GetDBValue(dataRow["fopl"])}, {GetDBValue(dataRow["opl"])}, {GetDBValue(dataRow["ls"])}, '{dataRow["npol"]}', {CrossDBSQL.ToDate(dat.Value, CrossDBSQL.Providers.PrvPostgres)}, {GetDBValue(dataRow["vznos"])}, {GetDBValue(dataRow["pr"])}, {GetDBValue(dataRow["kolds"])}, {GetDBValue(dataRow["soplvz"])}, {GetDBValue(dataRow["kodpl"])})");
                    }

                    if (needExecute)
                    {
                        DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText.ToString());
                        DBMngr.Realty.ExecuteNonQuery(command);
                    }

                    successCount += maxIndex - i - tempExistsCount;
                    existsCount += tempExistsCount;
                }
                catch (Exception ex)
                {
                    errorCount += maxIndex - i;
                    ErrorManager.LogError(ex);

                    if (!errorMessages.Any(x => x == ex.Message))
                    {
                        errorMessages.Add(ex.Message);
                    }
                }

                SetOMQueuePercent(i * 100 / dataTable.Rows.Count);
            }

            string message = $"Создано строк: {successCount}\n\r" +
                $"Уже существует в системе: {existsCount}\n\r" +
                $"Количество ошибок: {errorCount}\n\r";

            if (errorMessages.Any())
            {
                message += $"Ошибки: \n\r" +
                $"{string.Join("\n\r", errorMessages)}";
            }

            SetOMQueueMessage(message);
        }

        public void ImportPl(string fileName, Stream fileStream, DateTime? period, long fileStorageId)
        {
            List<PlImportModel> importModels = ReadPlFile(fileStream);

            if (importModels == null || !importModels.Any()) throw new Exception($"Не удалось прочитать файл {fileName}");

            int successCount = 0, errorCount = 0;
            List<string> errorMessages = new List<string>();
            List<OMInsuranceOrganization> omInsuranceOrganizations = OMInsuranceOrganization.Where(x => 1).Select(x => x.Code).Execute();
            OMInputFile omInputFile = new OMInputFile() { EmpId = -1 };
            omInputFile.Save();

            for (int i = 0; i < importModels.Count; i++)
            {
                try
                {
                    OMInsuranceOrganization omInsuranceOrganization = omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[i].Kod);

                    new OMPayTo
                    {
                        LinkIdFile = omInputFile.EmpId,
                        IdReestrContr = OMPolicySvd.GetRegisterId(),
                        TypeDoc_Code = ContractType.Dwelling,
                        Period = period,
                        InsuranceOrganizationId = omInsuranceOrganization?.Id,
                        Aok = importModels[i].Aok,
                        Unom = importModels[i].Unom,
                        Nom = importModels[i].Nom,
                        Nomi = importModels[i].Nomi,
                        Npol = importModels[i].Npol,
                        Npoldate = importModels[i].NpolDate,
                        Comnumber = importModels[i].ComNumber,
                        Comdate = importModels[i].ComDate,
                        Sl = importModels[i].Sl,
                        SpFact = importModels[i].SpFact,
                        SpNo = importModels[i].SpNo,
                        Factnumber = importModels[i].FactNumber,
                        Factdate = importModels[i].FactDate
                    }.Save();
                    successCount++;
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    errorCount++;

                    if (!errorMessages.Any(x => x == ex.Message))
                    {
                        errorMessages.Add(ex.Message);
                    }
                }

                SetOMQueuePercent(i * 100 / importModels.Count);
            }

            omInputFile.FileName = fileName;
            omInputFile.TypeFile_Code = TypeFile.InsurancePayments;
            omInputFile.PeriodRegDate = period;
            omInputFile.TypeSource_Code = InsuranceSourceType.Sk;
            omInputFile.DateInput = DateTime.Now;
            omInputFile.CountStr = importModels.Count;
            omInputFile.Status_Code = UFKFileProcessingStatus.Loaded;
            omInputFile.UserId = SRDSession.Current.UserID;
            omInputFile.FileStorageId = fileStorageId;
            omInputFile.InsuranceOrganizationId = importModels.Any()
                ? omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[0].Kod)?.Id : null;
            omInputFile.Save();

            string message = $"Создано строк: {successCount}\n\r" +
                $"Количество ошибок: {errorCount}\n\r";

            if (errorMessages.Any())
            {
                message += $"Ошибки: \n\r" +
                $"{string.Join("\n\r", errorMessages)}";
            }

            SetOMQueueMessage(message);
        }

        public void ImportPlNo(string fileName, Stream fileStream, DateTime? period, long fileStorageId)
        {
            List<PlNoImportModel> importModels = ReadPlNoFile(fileStream);

            if (importModels == null || !importModels.Any()) throw new Exception($"Не удалось прочитать файл {fileName}");

            int successCount = 0, errorCount = 0;
            List<string> errorMessages = new List<string>();
            List<OMInsuranceOrganization> omInsuranceOrganizations = OMInsuranceOrganization.Where(x => 1).Select(x => x.Code).Execute();
            List<OMOkrug> omOkrugs = OMOkrug.Where(x => 1).Select(x => x.Code).Execute();
            OMInputFile omInputFile = new OMInputFile() { EmpId = -1 };
            omInputFile.Save();

            for (int i = 0; i < importModels.Count; i++)
            {
                try
                {
                    OMInsuranceOrganization omInsuranceOrganization = omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[i].Kod);
                    OMOkrug omOkrug = omOkrugs.FirstOrDefault(x => x.Code == importModels[i].Aok);

                    new OMNoPay
                    {
                        LinkIdFile = omInputFile.EmpId,
                        IdReestrContr = OMPolicySvd.GetRegisterId(),
                        TypeDoc_Code = ContractType.Dwelling,
                        PeriodRegDate = period,
                        InsuranceOrganizationId = omInsuranceOrganization?.Id,
                        OkrugId = omOkrug?.Id,
                        Npol = importModels[i].Npol,
                        Npoldate = importModels[i].NpolDate,
                        Eventdat = importModels[i].EventDat,
                        Appdat = importModels[i].AppDat,
                        Reject_Code = (ReasonsRefusalInsurancePaymentLivingPremise)EnumExtensions.GetEnumByCode(typeof(ReasonsRefusalInsurancePaymentLivingPremise), importModels[i].Reject),
                        Renumber = importModels[i].ReNumber,
                        Redat = importModels[i].ReDat
                    }.Save();
                    successCount++;
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    errorCount++;

                    if (!errorMessages.Any(x => x == ex.Message))
                    {
                        errorMessages.Add(ex.Message);
                    }
                }

                SetOMQueuePercent(i * 100 / importModels.Count);
            }

            omInputFile.FileName = fileName;
            omInputFile.TypeFile_Code = TypeFile.InsurancePaymentsRefusal;
            omInputFile.PeriodRegDate = period;
            omInputFile.TypeSource_Code = InsuranceSourceType.Sk;
            omInputFile.DateInput = DateTime.Now;
            omInputFile.CountStr = importModels.Count;
            omInputFile.Status_Code = UFKFileProcessingStatus.Loaded;
            omInputFile.UserId = SRDSession.Current.UserID;
            omInputFile.FileStorageId = fileStorageId;
            omInputFile.InsuranceOrganizationId = importModels.Any()
                ? omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[0].Kod)?.Id : null;
            omInputFile.Save();

            string message = $"Создано строк: {successCount}\n\r" +
                $"Количество ошибок: {errorCount}\n\r";

            if (errorMessages.Any())
            {
                message += $"Ошибки: \n\r" +
                $"{string.Join("\n\r", errorMessages)}";
            }

            SetOMQueueMessage(message);
        }

        public void ImportComm(string fileName, Stream fileStream, DateTime? period, long fileStorageId)
        {
            List<CommImportModel> importModels = ReadCommFile(fileStream);

            if (importModels == null || !importModels.Any()) throw new Exception($"Не удалось прочитать файл {fileName}");

            int successCount = 0, existsCount = 0, errorCount = 0;
            List<string> errorMessages = new List<string>();
            List<string> ndogList = importModels.Select(x => x.Ndog).Distinct().ToList();
            List<DateTime?> ndogDatList = importModels.Select(x => x.NdogDat).Distinct().ToList();
            List<OMAllProperty> omAllProperties = ndogList.Any() && ndogDatList.Any()
                ? OMAllProperty.Where(x => ndogList.Contains(x.Ndog) && ndogDatList.Contains(x.Ndogdat)).Select(x => x.Ndog).Select(x => x.Ndogdat).Execute()
                : new List<OMAllProperty>();
            List<OMOkrug> omOkrugs = OMOkrug.Where(x => 1).Select(x => x.Code).Execute();
            List<OMInsuranceOrganization> omInsuranceOrganizations = OMInsuranceOrganization.Where(x => 1).Select(x => x.Code).Execute();
            List<OMSubject> omSubjets = OMSubject.Where(x => 1).Select(x => x.KodOrg).Select(x => x.Inn).Execute();
            OMInputFile omInputFile = new OMInputFile() { EmpId = -1 };
            omInputFile.Save();

            for (int i = 0; i < importModels.Count; i++)
            {
                try
                {
                    if (importModels[i].NdogDat?.Month != period?.Month || importModels[i].NdogDat?.Year != period?.Year)
                    {
                        throw new Exception("\"Дата начала действия договора\" должна быть только того месяца, за который идет загрузка");
                    }

                    OMInsuranceOrganization omInsuranceOrganization = omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[i].Kod);
                    OMOkrug omOkrug = omOkrugs.FirstOrDefault(x => x.Code == importModels[i].Aok);
                    OMSubject omSubject = null;

                    if (importModels[i].Inn.IsNotEmpty())
                    {
                        omSubject = omSubjets.FirstOrDefault(x => x.Inn == importModels[i].Inn);
                    }

                    if (omSubject == null)
                    {
                        omSubject = omSubjets.FirstOrDefault(x => x.KodOrg == importModels[i].OrgId);
                    }
                    // CIPJS-867: Доработать загрузку файла COMM.DBF
                    // INSUR_ALL_PROPERTY.OKUG_ID = OKRUG_ID МКД
                    var mkd = OMBuilding.Where(x => x.Unom == importModels[i].Unom && x.SpecialActual == 1).Select(x => x.OkrugId).ExecuteFirstOrDefault();

                    if (!omAllProperties.Any(x => x.Ndog == importModels[i].Ndog && x.Ndogdat == importModels[i].NdogDat))
                    {
                        new OMAllProperty
                        {
                            LinkIdFile = omInputFile.EmpId,
                            InsuranceId = omInsuranceOrganization?.Id,
                            OkrugId = mkd?.OkrugId,
                            Unom = importModels[i].Unom,
                            SubjectId = omSubject?.EmpId,
                            OrgType_Code = (FormAssociationOwners)EnumExtensions.GetEnumByCode(typeof(FormAssociationOwners), importModels[i].OrgType),
                            Name = importModels[i].Name,
                            Ndog = importModels[i].Ndog,
                            Ndogdat = importModels[i].NdogDat,
                            St1 = importModels[i].St1,
                            St2 = importModels[i].St2,
                            St3 = importModels[i].St3,
                            Ss1 = importModels[i].Ss1,
                            Ss2 = importModels[i].Ss2,
                            Ss3 = importModels[i].Ss3,
                            Part = importModels[i].Part,
                            PartCity = importModels[i].PartCity,
                            Paysign_Code = (SignInstallmentPayment)EnumExtensions.GetEnumByCode(typeof(SignInstallmentPayment), importModels[i].Paysign),
                            RasPripay = importModels[i].RasPripay,
                            Status_Code = ContractStatus.Created,
                            OrgIdFile = importModels[i].OrgId,
                            Inn = importModels[i].Inn
                        }.Save();
                        successCount++;
                    }
                    else { existsCount++; }
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    errorCount++;

                    if (!errorMessages.Any(x => x == ex.Message))
                    {
                        errorMessages.Add(ex.Message);
                    }
                }

                SetOMQueuePercent(i * 100 / importModels.Count);
            }

            omInputFile.FileName = fileName;
            omInputFile.TypeFile_Code = TypeFile.InsuranceContractConcluded;
            omInputFile.PeriodRegDate = period;
            omInputFile.TypeSource_Code = InsuranceSourceType.Sk;
            omInputFile.DateInput = DateTime.Now;
            omInputFile.CountStr = importModels.Count;
            omInputFile.Status_Code = UFKFileProcessingStatus.Loaded;
            omInputFile.UserId = SRDSession.Current.UserID;
            omInputFile.FileStorageId = fileStorageId;
            omInputFile.InsuranceOrganizationId = importModels.Any()
                ? omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[0].Kod)?.Id : null;
            omInputFile.Save();

            string message = $"Создано строк: {successCount}\n\r" +
                $"Уже существует в системе: {existsCount}\n\r" +
                $"Количество ошибок: {errorCount}\n\r";

            if (errorMessages.Any())
            {
                message += $"Ошибки: \n\r" +
                $"{string.Join("\n\r", errorMessages)}";
            }

            SetOMQueueMessage(message);
        }

        public void ImportCommDop(string fileName, Stream fileStream, DateTime? period, long fileStorageId)
        {
            List<CommDopImportModel> importModels = ReadCommDopFile(fileStream);

            if (importModels == null || !importModels.Any()) throw new Exception($"Не удалось прочитать файл {fileName}");

            int successCount = 0, errorCount = 0;
            List<string> errorMessages = new List<string>();
            List<string> ndogList = importModels.Select(x => x.Ndog).Distinct().ToList();
            List<OMAllProperty> omAllProperties = ndogList.Any() ? OMAllProperty.Where(x => ndogList.Contains(x.Ndog)).Select(x => x.Ndog).Execute()
                : new List<OMAllProperty>();
            OMInputFile omInputFile = new OMInputFile() { EmpId = -1 };
            omInputFile.Save();

            for (int i = 0; i < importModels.Count; i++)
            {
                try
                {
                    new OMDopAllProperty
                    {
                        LinkIdFile = omInputFile.EmpId,
                        ContractId = omAllProperties.FirstOrDefault(x => x.Ndog == importModels[i].Ndog)?.EmpId,
                        Kod = importModels[i].Kod,
                        Unom = importModels[i].Unom,
                        Ndog = importModels[i].Ndog,
                        Ndogdat = importModels[i].NdogDat,
                        Ndops = importModels[i].Ndops,
                        St1New = importModels[i].St1,
                        St2New = importModels[i].St2,
                        St3New = importModels[i].St3,
                        Ss1New = importModels[i].Ss1,
                        Ss2New = importModels[i].Ss2,
                        Ss3New = importModels[i].Ss3,
                        PartNew = importModels[i].Part,
                        PartCityNew = importModels[i].PartCity,
                        PaysignNew = importModels[i].Paysign,
                        RasPripayNew = importModels[i].RasPripay
                    }.Save();
                    successCount++;
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    errorCount++;

                    if (!errorMessages.Any(x => x == ex.Message))
                    {
                        errorMessages.Add(ex.Message);
                    }
                }

                SetOMQueuePercent(i * 100 / importModels.Count);
            }

            omInputFile.FileName = fileName;
            omInputFile.TypeFile_Code = TypeFile.AddInsuranceContractConcluded;
            omInputFile.PeriodRegDate = period;
            omInputFile.TypeSource_Code = InsuranceSourceType.Sk;
            omInputFile.DateInput = DateTime.Now;
            omInputFile.CountStr = importModels.Count;
            omInputFile.Status_Code = UFKFileProcessingStatus.ProcessedCompletely;
            omInputFile.UserId = SRDSession.Current.UserID;
            omInputFile.FileStorageId = fileStorageId;
            omInputFile.InsuranceOrganizationId = importModels.Any()
                ? OMInsuranceOrganization.Where(x => x.Code == importModels[0].Kod).ExecuteFirstOrDefault()?.Id : null;
            omInputFile.Save();

            string message = $"Создано строк: {successCount}\n\r" +
                $"Количество ошибок: {errorCount}\n\r";

            if (errorMessages.Any())
            {
                message += $"Ошибки: \n\r" +
                $"{string.Join("\n\r", errorMessages)}";
            }

            SetOMQueueMessage(message);
        }

        public void ImportPayComm(string fileName, Stream fileStream, DateTime? period, long fileStorageId)
        {
            List<PayCommImportModel> importModels = ReadPayCommFile(fileStream);
            List<OMInsuranceOrganization> omInsuranceOrganizations = OMInsuranceOrganization.Where(x => 1).Select(x => x.Code).Execute();

            if (importModels == null || !importModels.Any()) throw new Exception($"Не удалось прочитать файл {fileName}");

            int successCount = 0, errorCount = 0;
            List<string> errorMessages = new List<string>();
            OMInputFile omInputFile = new OMInputFile() { EmpId = -1 };
            omInputFile.Save();

            for (int i = 0; i < importModels.Count; i++)
            {
                try
                {
                    if (importModels[i].PayDat?.Month != period?.Month || importModels[i].PayDat?.Year != period?.Year)
                    {
                        throw new Exception("\"Дата платежа\" должна быть только того месяца, за который идет загрузка");
                    }

                    OMInsuranceOrganization omInsuranceOrganization = omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[i].Kod);

                    new OMInputPlat
                    {
                        LinkIdFile = omInputFile.EmpId,
                        Unom = importModels[i].Unom,
                        PmtDate = importModels[i].PayDat,
                        PeriodRegDate = period,
                        SumOpl = importModels[i].Pripay,
                        StatusIdentif_Code = StatusIdentifikacii.Identified,
                        TypeSource_Code = InsuranceSourceType.Sk,
                        TypeDoc_Code = ContractType.CommonOwnership,
                        Kod = importModels[i].Kod,
                        InsuranceOrganizationId = omInsuranceOrganization?.Id,
                        Ndog = importModels[i].Ndog,
                        Ndogdat = importModels[i].NdogDat,
                        Ndops = importModels[i].Ndops,
                        Paynumber = importModels[i].PayNumber
                    }.Save();
                    successCount++;
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    errorCount++;

                    if (!errorMessages.Any(x => x == ex.Message))
                    {
                        errorMessages.Add(ex.Message);
                    }
                }

                SetOMQueuePercent(i * 100 / importModels.Count);
            }

            omInputFile.FileName = fileName;
            omInputFile.TypeFile_Code = TypeFile.PaymentReceived;
            omInputFile.PeriodRegDate = period;
            omInputFile.TypeSource_Code = InsuranceSourceType.Sk;
            omInputFile.DateInput = DateTime.Now;
            omInputFile.CountStr = importModels.Count;
            omInputFile.Status_Code = UFKFileProcessingStatus.Loaded;
            omInputFile.UserId = SRDSession.Current.UserID;
            omInputFile.FileStorageId = fileStorageId;
            omInputFile.InsuranceOrganizationId = importModels.Any()
                ? omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[0].Kod)?.Id : null;
            omInputFile.Save();


            string message = $"Создано строк: {successCount}\n\r" +
                $"Количество ошибок: {errorCount}\n\r";

            if (errorMessages.Any())
            {
                message += $"Ошибки: \n\r" +
                $"{string.Join("\n\r", errorMessages)}";
            }

            SetOMQueueMessage(message);
        }

        public void ImportPlComm(string fileName, Stream fileStream, DateTime? period, long fileStorageId)
        {
            List<PlCommImportModel> importModels = ReadPlCommFile(fileStream);

            if (importModels == null || !importModels.Any()) throw new Exception($"Не удалось прочитать файл {fileName}");

            int successCount = 0, errorCount = 0;
            List<string> errorMessages = new List<string>();
            List<OMInsuranceOrganization> omInsuranceOrganizations = OMInsuranceOrganization.Where(x => 1).Select(x => x.Code).Execute();
            List<OMSubject> omSubjects = OMSubject.Where(x => 1).Select(x => x.SubjectName).Execute();
            OMInputFile omInputFile = new OMInputFile() { EmpId = -1 };
            omInputFile.Save();

            for (int i = 0; i < importModels.Count; i++)
            {
                try
                {
                    OMInsuranceOrganization omInsuranceOrganization = omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[i].Kod);
                    OMSubject omSubject = omSubjects.FirstOrDefault(x => x.SubjectName.ToLower() == importModels[i].Name.ToLower());

                    new OMPayTo
                    {
                        LinkIdFile = omInputFile.EmpId,
                        IdReestrContr = OMAllProperty.GetRegisterId(),
                        TypeDoc_Code = ContractType.CommonOwnership,
                        Period = period,
                        InsuranceOrganizationId = omInsuranceOrganization?.Id,
                        Aok = importModels[i].Aok,
                        Comnumber = importModels[i].ComNumber,
                        Comdate = importModels[i].ComDate,
                        Sl = importModels[i].Sl,
                        SpFact = importModels[i].SpFact,
                        SpNo = importModels[i].SpNo,
                        Factnumber = importModels[i].FactNumber,
                        Factdate = importModels[i].FactDate,
                        SubjectId = omSubject?.EmpId,
                        Ndoc = importModels[i].Ndoc,
                        Ndogdat = importModels[i].NdogDat
                    }.Save();
                    successCount++;
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    errorCount++;

                    if (!errorMessages.Any(x => x == ex.Message))
                    {
                        errorMessages.Add(ex.Message);
                    }
                }

                SetOMQueuePercent(i * 100 / importModels.Count);
            }

            omInputFile.FileName = fileName;
            omInputFile.TypeFile_Code = TypeFile.InsurancePaymentsUnderContracts;
            omInputFile.PeriodRegDate = period;
            omInputFile.TypeSource_Code = InsuranceSourceType.Sk;
            omInputFile.DateInput = DateTime.Now;
            omInputFile.CountStr = importModels.Count;
            omInputFile.Status_Code = UFKFileProcessingStatus.Loaded;
            omInputFile.UserId = SRDSession.Current.UserID;
            omInputFile.FileStorageId = fileStorageId;
            omInputFile.InsuranceOrganizationId = importModels.Any()
                ? omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[0].Kod)?.Id : null;
            omInputFile.Save();


            string message = $"Создано строк: {successCount}\n\r" +
                $"Количество ошибок: {errorCount}\n\r";

            if (errorMessages.Any())
            {
                message += $"Ошибки: \n\r" +
                $"{string.Join("\n\r", errorMessages)}";
            }

            SetOMQueueMessage(message);
        }

        public void ImportPlCommNo(string fileName, Stream fileStream, DateTime? period, long fileStorageId)
        {
            List<PlCommNoImportModel> importModels = ReadPlCommNoFile(fileStream);

            if (importModels == null || !importModels.Any()) throw new Exception($"Не удалось прочитать файл {fileName}");

            int successCount = 0, errorCount = 0;
            List<string> errorMessages = new List<string>();
            List<OMInsuranceOrganization> omInsuranceOrganizations = OMInsuranceOrganization.Where(x => 1).Select(x => x.Code).Execute();
            List<OMOkrug> omOkrugs = OMOkrug.Where(x => 1).Select(x => x.Code).Execute();
            List<OMSubject> omSubjects = OMSubject.Where(x => 1).Select(x => x.KodOrg).Execute();
            OMInputFile omInputFile = new OMInputFile() { EmpId = -1 };
            omInputFile.Save();

            for (int i = 0; i < importModels.Count; i++)
            {
                try
                {
                    OMInsuranceOrganization omInsuranceOrganization = omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[i].Kod);
                    OMOkrug omOkrug = omOkrugs.FirstOrDefault(x => x.Code == importModels[i].Aok);
                    OMSubject omSubject = omSubjects.FirstOrDefault(x => x.KodOrg == importModels[i].OrgId);

                    new OMNoPay
                    {
                        LinkIdFile = omInputFile.EmpId,
                        IdReestrContr = OMAllProperty.GetRegisterId(),
                        TypeDoc_Code = ContractType.CommonOwnership,
                        PeriodRegDate = period,
                        InsuranceOrganizationId = omInsuranceOrganization?.Id,
                        OkrugId = omOkrug?.Id,
                        Eventdat = importModels[i].EventDat,
                        Appdat = importModels[i].AppDat,
                        Reject_Code = (ReasonsRefusalInsurancePaymentLivingPremise)EnumExtensions.GetEnumByCode(typeof(ReasonsRefusalInsurancePaymentLivingPremise), importModels[i].Reject),
                        Unom = importModels[i].Unom,
                        SubjectId = omSubject?.EmpId,
                        OrgType_Code = (FormAssociationOwners)EnumExtensions.GetEnumByCode(typeof(FormAssociationOwners), importModels[i].OrgType),
                        Name = importModels[i].Name,
                        Ndog = importModels[i].Ndog,
                        Ndogdat = importModels[i].NdogDat,
                        Phonedat = importModels[i].PhoneDat,
                        Inspnumber = importModels[i].InspNumber,
                        Inspdat = importModels[i].InspDat,
                        Reason_Code = (CausesOfDamageGP)EnumExtensions.GetEnumByCode(typeof(CausesOfDamageGP), importModels[i].Reason),
                        Reasabs_Code = (ReasonsAbsenceDecision)EnumExtensions.GetEnumByCode(typeof(ReasonsAbsenceDecision), importModels[i].Reasabs),
                        Renumber = importModels[i].ReNumber,
                        Redat = importModels[i].ReDat
                    }.Save();
                    successCount++;
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    errorCount++;

                    if (!errorMessages.Any(x => x == ex.Message))
                    {
                        errorMessages.Add(ex.Message);
                    }
                }

                SetOMQueuePercent(i * 100 / importModels.Count);
            }

            omInputFile.FileName = fileName;
            omInputFile.TypeFile_Code = TypeFile.DeclaredUnsettledInsuranceEvents;
            omInputFile.PeriodRegDate = period;
            omInputFile.TypeSource_Code = InsuranceSourceType.Sk;
            omInputFile.DateInput = DateTime.Now;
            omInputFile.CountStr = importModels.Count;
            omInputFile.Status_Code = UFKFileProcessingStatus.Loaded;
            omInputFile.UserId = SRDSession.Current.UserID;
            omInputFile.FileStorageId = fileStorageId;
            omInputFile.InsuranceOrganizationId = importModels.Any()
                ? omInsuranceOrganizations.FirstOrDefault(x => x.Code == importModels[0].Kod)?.Id : null;
            omInputFile.Save();

            string message = $"Создано строк: {successCount}\n\r" +
                $"Количество ошибок: {errorCount}\n\r";

            if (errorMessages.Any())
            {
                message += $"Ошибки: \n\r" +
                $"{string.Join("\n\r", errorMessages)}";
            }

            SetOMQueueMessage(message);
        }

        private object GetDBValue(object value)
        {
            if (value == null || value.ToString().IsNullOrEmpty()) return "NULL";
            return value;
        }

        public List<PolicyImportModel> ReadPolicyFile(Stream fileStream, bool isCheck = false)
        {
            List<PolicyImportModel> importModels = new List<PolicyImportModel>();
            DbfFile dbfFile = new DbfFile(_OEMEncoding);

            dbfFile.Open(fileStream);

            DbfRecord dbfRecord = dbfFile.ReadNext();

            if (dbfRecord == null) return importModels;

            for (; dbfRecord != null; dbfRecord = dbfFile.ReadNext())
            {
                if (dbfRecord.ToString().Trim().IsNullOrEmpty()) continue;

                importModels.Add(new PolicyImportModel
                {
                    Npol = dbfRecord["npol"]?.ParseToString(),
                    Dat = dbfRecord["dat"]?.ParseToDateTime(),
                    Aok = dbfRecord["aok"]?.ParseToLong(),
                    Mok = dbfRecord["mok"]?.ParseToLong(),
                    Kod = dbfRecord["kod"]?.ParseToLong(),
                    Pralt = dbfRecord["pralt"]?.ParseToString(),
                    OrgId = dbfRecord["org_id"]?.ParseToLong(),
                    OrgType = dbfRecord["org"]?.ParseToLong(),
                    PlatId = dbfRecord["plat_id"]?.ParseToLong(),
                    Unom = dbfRecord["unom"]?.ParseToLong(),
                    Unkva = dbfRecord["unkva"]?.ParseToString(),
                    Nom = dbfRecord["nom"]?.ParseToString(),
                    Nomi = dbfRecord["nomi"]?.ParseToString(),
                    Kvnom = dbfRecord["kvnom"]?.ParseToString(),
                    Kolgp = dbfRecord["kolgp"]?.ParseToLong(),
                    FlatStatus = dbfRecord["flatstatus"]?.ParseToLong(),
                    Fopl = dbfRecord["fopl"]?.ParseToDecimal(),
                    Opl = dbfRecord["opl"]?.ParseToDecimal(),
                    Ls = dbfRecord["ls"]?.ParseToLong(),
                    Ss = dbfRecord["ss"]?.ParseToDecimal(),
                    Kolds = dbfRecord["kolds"]?.ParseToLong(),
                    Soplvz = dbfRecord["soplvz"]?.ParseToDecimal()
                });

                if (isCheck) return importModels;
            }

            return importModels;
        }

        public List<PolicyDImportModel> ReadPolicyDFile(Stream fileStream, bool isCheck = false)
        {
            List<PolicyDImportModel> importModels = new List<PolicyDImportModel>();
            DbfFile dbfFile = new DbfFile(_OEMEncoding);

            dbfFile.Open(fileStream);

            DbfRecord dbfRecord = dbfFile.ReadNext();

            if (dbfRecord == null) return importModels;

            for (; dbfRecord != null; dbfRecord = dbfFile.ReadNext())
            {
                if (dbfRecord.ToString().Trim().IsNullOrEmpty()) continue;

                importModels.Add(new PolicyDImportModel
                {
                    Npol = dbfRecord["npol"]?.ParseToString(),
                    DatEnd = dbfRecord["dat_end"]?.ParseToDateTime()
                });

                if (isCheck) return importModels;
            }

            return importModels;
        }

        public List<SvdImportModel> ReadSvdFile(Stream fileStream, bool isCheck = false)
        {
            List<SvdImportModel> importModels = new List<SvdImportModel>();
            DbfFile dbfFile = new DbfFile(_OEMEncoding);

            dbfFile.Open(fileStream);

            DbfRecord dbfRecord = dbfFile.ReadNext();

            if (dbfRecord == null) return importModels;

            for (; dbfRecord != null; dbfRecord = dbfFile.ReadNext())
            {
                if (dbfRecord.ToString().Trim().IsNullOrEmpty()) continue;

                importModels.Add(new SvdImportModel
                {
                    Npol = dbfRecord["npol"]?.ParseToString(),
                    Dat = dbfRecord["dat"]?.ParseToDateTime(),
                    Aok = dbfRecord["aok"]?.ParseToLong(),
                    Mok = dbfRecord["mok"]?.ParseToLong(),
                    Kod = dbfRecord["kod"]?.ParseToLong(),
                    OrgId = dbfRecord["org_id"]?.ParseToLong(),
                    OrgType = dbfRecord["org"]?.ParseToLong(),
                    PlatId = dbfRecord["plat_id"]?.ParseToLong(),
                    Unom = dbfRecord["unom"]?.ParseToLong(),
                    Unkva = dbfRecord["unkva"]?.ParseToString(),
                    Nom = dbfRecord["nom"]?.ParseToString(),
                    Nomi = dbfRecord["nomi"]?.ParseToString(),
                    Kvnom = dbfRecord["kvnom"]?.ParseToString(),
                    Kolgp = dbfRecord["kolgp"]?.ParseToLong(),
                    Fopl = dbfRecord["fopl"]?.ParseToDecimal(),
                    Opl = dbfRecord["opl"]?.ParseToDecimal(),
                    Ls = dbfRecord["ls"]?.ParseToLong(),
                    Vznos = dbfRecord["vznos"]?.ParseToDecimal(),
                    Pr = dbfRecord["pr"]?.ParseToLong(),
                    Kolds = dbfRecord["kolds"]?.ParseToLong(),
                    Soplvz = dbfRecord["soplvz"]?.ParseToDecimal(),
                    Kodpl = dbfRecord["kodpl"]?.ParseToLong()
                });

                if (isCheck) return importModels;
            }

            return importModels;
        }

        public List<PlImportModel> ReadPlFile(Stream fileStream, bool isCheck = false)
        {
            List<PlImportModel> importModels = new List<PlImportModel>();
            DbfFile dbfFile = new DbfFile(_OEMEncoding);

            dbfFile.Open(fileStream);

            DbfRecord dbfRecord = dbfFile.ReadNext();

            if (dbfRecord == null) return importModels;

            for (; dbfRecord != null; dbfRecord = dbfFile.ReadNext())
            {
                if (dbfRecord.ToString().Trim().IsNullOrEmpty()) continue;

                importModels.Add(new PlImportModel
                {
                    Kod = dbfRecord["kod"]?.ParseToDecimal().ParseToLong(),
                    Aok = dbfRecord["aok"]?.ParseToDecimal().ParseToLong(),
                    Unom = dbfRecord["unom"]?.ParseToDecimal().ParseToLong(),
                    Nom = dbfRecord["nom"]?.ParseToString(),
                    Nomi = dbfRecord["nomi"]?.ParseToString(),
                    Npol = dbfRecord["npol"]?.ParseToString(),
                    NpolDate = dbfRecord["npoldate"]?.ParseToDateTime(),
                    ComNumber = dbfRecord["comnumber"]?.ParseToString(),
                    ComDate = dbfRecord["comdate"]?.ParseToDateTime(),
                    Sl = dbfRecord["sl"]?.ParseToDecimal(),
                    SpFact = dbfRecord["sp_fact"]?.ParseToDecimal(),
                    SpNo = dbfRecord["sp_no"]?.ParseToDecimal(),
                    FactNumber = dbfRecord["factnumber"]?.ParseToString(),
                    FactDate = dbfRecord["factdate"]?.ParseToDateTime()
                });

                if (isCheck) return importModels;
            }

            return importModels;
        }

        public List<PlNoImportModel> ReadPlNoFile(Stream fileStream, bool isCheck = false)
        {
            List<PlNoImportModel> importModels = new List<PlNoImportModel>();
            DbfFile dbfFile = new DbfFile(_OEMEncoding);

            dbfFile.Open(fileStream);

            DbfRecord dbfRecord = dbfFile.ReadNext();

            if (dbfRecord == null) return importModels;

            for (; dbfRecord != null; dbfRecord = dbfFile.ReadNext())
            {
                if (dbfRecord.ToString().Trim().IsNullOrEmpty()) continue;

                importModels.Add(new PlNoImportModel
                {
                    Kod = dbfRecord["kod"]?.ParseToDecimal().ParseToLong(),
                    Aok = dbfRecord["aok"]?.ParseToDecimal().ParseToLong(),
                    Npol = dbfRecord["npol"]?.ParseToString(),
                    NpolDate = dbfRecord["npoldate"]?.ParseToDateTime(),
                    EventDat = dbfRecord["eventdat"]?.ParseToDateTime(),
                    AppDat = dbfRecord["appdat"]?.ParseToDateTime(),
                    Reject = dbfRecord["reject"]?.ParseToString(),
                    ReNumber = dbfRecord["renumber"]?.ParseToString(),
                    ReDat = dbfRecord["redat"]?.ParseToDateTime()
                });

                if (isCheck) return importModels;
            }

            return importModels;
        }

        public List<CommImportModel> ReadCommFile(Stream fileStream, bool isCheck = false)
        {
            List<CommImportModel> importModels = new List<CommImportModel>();
            DbfFile dbfFile = new DbfFile(_OEMEncoding);

            dbfFile.Open(fileStream);

            DbfRecord dbfRecord = dbfFile.ReadNext();

            if (dbfRecord == null) return importModels;

            for (; dbfRecord != null; dbfRecord = dbfFile.ReadNext())
            {
                if (dbfRecord.ToString().Trim().IsNullOrEmpty()) continue;

                importModels.Add(new CommImportModel
                {
                    Kod = dbfRecord["kod"]?.ParseToDecimal().ParseToLong(),
                    Aok = dbfRecord["aok"]?.ParseToDecimal().ParseToLong(),
                    Unom = dbfRecord["unom"]?.ParseToDecimal().ParseToLong(),
                    OrgType = dbfRecord["org"]?.ParseToString(),
                    OrgId = dbfRecord["org_id"]?.ParseToDecimal().ParseToLong(),
                    Name = dbfRecord["name"]?.ParseToString(),
                    Ndog = dbfRecord["ndog"]?.ParseToString(),
                    NdogDat = dbfRecord["ndogdat"]?.ParseToDateTime(),
                    St1 = dbfRecord["st1"]?.ParseToDecimal(),
                    St2 = dbfRecord["st2"]?.ParseToDecimal(),
                    St3 = dbfRecord["st3"]?.ParseToDecimal(),
                    Ss1 = dbfRecord["ss1"]?.ParseToDecimal(),
                    Ss2 = dbfRecord["ss2"]?.ParseToDecimal(),
                    Ss3 = dbfRecord["ss3"]?.ParseToDecimal(),
                    Part = dbfRecord["part"]?.ParseToDecimal(),
                    PartCity = dbfRecord["part_city"]?.ParseToDecimal(),
                    Paysign = dbfRecord["paysign"]?.ParseToString(),
                    RasPripay = dbfRecord["ras_pripay"]?.ParseToDecimal()
                });

                if (dbfRecord.FindColumn("inn") != -1)
                {
                    importModels.Last().Inn = dbfRecord["inn"]?.ParseToString();
                }

                if (isCheck) return importModels;
            }

            return importModels;
        }

        public List<CommDopImportModel> ReadCommDopFile(Stream fileStream, bool isCheck = false)
        {
            List<CommDopImportModel> importModels = new List<CommDopImportModel>();
            DbfFile dbfFile = new DbfFile(_OEMEncoding);

            dbfFile.Open(fileStream);

            DbfRecord dbfRecord = dbfFile.ReadNext();

            if (dbfRecord == null) return importModels;

            for (; dbfRecord != null; dbfRecord = dbfFile.ReadNext())
            {
                if (dbfRecord.ToString().Trim().IsNullOrEmpty()) continue;

                importModels.Add(new CommDopImportModel
                {
                    Kod = dbfRecord["kod"]?.ParseToDecimal().ParseToLong(),
                    Unom = dbfRecord["unom"]?.ParseToDecimal().ParseToLong(),
                    Ndog = dbfRecord["ndog"]?.ParseToString(),
                    NdogDat = dbfRecord["ndogdat"]?.ParseToDateTime(),
                    Ndops = dbfRecord["ndops"]?.ParseToDecimal(),
                    St1 = dbfRecord["st1"]?.ParseToDecimal().ParseToLong(),
                    St2 = dbfRecord["st2"]?.ParseToDecimal().ParseToLong(),
                    St3 = dbfRecord["st3"]?.ParseToDecimal().ParseToLong(),
                    Ss1 = dbfRecord["ss1"]?.ParseToDecimal().ParseToLong(),
                    Ss2 = dbfRecord["ss2"]?.ParseToDecimal().ParseToLong(),
                    Ss3 = dbfRecord["ss3"]?.ParseToDecimal().ParseToLong(),
                    Part = dbfRecord["part"]?.ParseToDecimal(),
                    PartCity = dbfRecord["part_city"]?.ParseToDecimal(),
                    Paysign = dbfRecord["paysign"]?.ParseToDecimal(),
                    RasPripay = dbfRecord["ras_pripay"]?.ParseToDecimal()
                });

                if (isCheck) return importModels;
            }

            return importModels;
        }

        public List<PayCommImportModel> ReadPayCommFile(Stream fileStream, bool isCheck = false)
        {
            List<PayCommImportModel> importModels = new List<PayCommImportModel>();
            DbfFile dbfFile = new DbfFile(_OEMEncoding);

            dbfFile.Open(fileStream);

            DbfRecord dbfRecord = dbfFile.ReadNext();

            if (dbfRecord == null) return importModels;

            for (; dbfRecord != null; dbfRecord = dbfFile.ReadNext())
            {
                if (dbfRecord.ToString().Trim().IsNullOrEmpty()) continue;

                importModels.Add(new PayCommImportModel
                {
                    Unom = dbfRecord["unom"]?.ParseToDecimal().ParseToLong(),
                    PayDat = dbfRecord["paydat"]?.ParseToDateTime(),
                    Pripay = dbfRecord["pripay"]?.ParseToDecimal(),
                    Kod = dbfRecord["kod"]?.ParseToDecimal().ParseToLong(),
                    Ndog = dbfRecord["ndog"]?.ParseToString(),
                    NdogDat = dbfRecord["ndogdat"]?.ParseToDateTime(),
                    Ndops = dbfRecord["ndops"]?.ParseToString(),
                    PayNumber = dbfRecord["paynumber"]?.ParseToString()
                });

                if (isCheck) return importModels;
            }

            return importModels;
        }

        public List<PlCommImportModel> ReadPlCommFile(Stream fileStream, bool isCheck = false)
        {
            List<PlCommImportModel> importModels = new List<PlCommImportModel>();
            DbfFile dbfFile = new DbfFile(_OEMEncoding);

            dbfFile.Open(fileStream);

            DbfRecord dbfRecord = dbfFile.ReadNext();

            if (dbfRecord == null) return importModels;

            for (; dbfRecord != null; dbfRecord = dbfFile.ReadNext())
            {
                if (dbfRecord.ToString().Trim().IsNullOrEmpty()) continue;

                importModels.Add(new PlCommImportModel
                {
                    Kod = dbfRecord["kod"]?.ParseToDecimal().ParseToLong(),
                    Aok = dbfRecord["aok"]?.ParseToDecimal().ParseToLong(),
                    ComNumber = dbfRecord["comnumber"]?.ParseToString(),
                    ComDate = dbfRecord["comdate"]?.ParseToDateTime(),
                    Sl = dbfRecord["sl"]?.ParseToDecimal(),
                    SpFact = dbfRecord["sp_fact"]?.ParseToDecimal(),
                    SpNo = dbfRecord["sp_no"]?.ParseToDecimal(),
                    FactNumber = dbfRecord["factnumber"]?.ParseToString(),
                    FactDate = dbfRecord["factdate"]?.ParseToDateTime(),
                    Name = dbfRecord["name"]?.ParseToString(),
                    Ndoc = dbfRecord["ndoc"]?.ParseToString(),
                    NdogDat = dbfRecord["ndogdat"]?.ParseToDateTime()
                });

                if (isCheck) return importModels;
            }

            return importModels;
        }

        public List<PlCommNoImportModel> ReadPlCommNoFile(Stream fileStream, bool isCheck = false)
        {
            List<PlCommNoImportModel> importModels = new List<PlCommNoImportModel>();
            DbfFile dbfFile = new DbfFile(_OEMEncoding);

            dbfFile.Open(fileStream);

            DbfRecord dbfRecord = dbfFile.ReadNext();

            if (dbfRecord == null) return importModels;

            for (; dbfRecord != null; dbfRecord = dbfFile.ReadNext())
            {
                if (dbfRecord.ToString().Trim().IsNullOrEmpty()) continue;

                importModels.Add(new PlCommNoImportModel
                {
                    Kod = dbfRecord["kod"]?.ParseToDecimal().ParseToLong(),
                    Aok = dbfRecord["aok"]?.ParseToDecimal().ParseToLong(),
                    EventDat = dbfRecord["eventdat"]?.ParseToDateTime(),
                    AppDat = dbfRecord["appdat"]?.ParseToDateTime(),
                    Reject = dbfRecord["reject"]?.ParseToString(),
                    Unom = dbfRecord["unom"]?.ParseToDecimal().ParseToLong(),
                    OrgId = dbfRecord["org_id"]?.ParseToDecimal().ParseToLong(),
                    OrgType = dbfRecord["org"]?.ParseToString(),
                    Name = dbfRecord["name"]?.ParseToString(),
                    Ndog = dbfRecord["ndog"]?.ParseToString(),
                    NdogDat = dbfRecord["ndogdat"]?.ParseToDateTime(),
                    PhoneDat = dbfRecord["phonedat"]?.ParseToDateTime(),
                    InspNumber = dbfRecord["inspnumber"]?.ParseToString(),
                    InspDat = dbfRecord["inspdat"]?.ParseToDateTime(),
                    Reason = dbfRecord["reason"]?.ParseToString(),
                    Reasabs = dbfRecord["reasabs"]?.ParseToString(),
                    ReNumber = dbfRecord["renumber"]?.ParseToString(),
                    ReDat = dbfRecord["redat"]?.ParseToDateTime()
                });

                if (isCheck) return importModels;
            }

            return importModels;
        }
    }
}