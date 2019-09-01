using CIPJS.DAL.Fsp;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Transactions;

namespace CIPJS.DAL.SK
{
    public class SkProcessLoadProcess : ILongProcess
    {
        private OMQueue _omQueue;

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            _omQueue = processQueue;
            long id = processQueue.ObjectId.Value;
            OMInputFile omInputFile = OMInputFile.Where(x => x.EmpId == id)
                .Select(x => x.PeriodRegDate)
                .Select(x => x.TypeFile)
                .Select(x => x.TypeFile_Code)
                .Select(x => x.Status_Code)
                .Execute().FirstOrDefault();

            if (omInputFile == null) throw new Exception($"Запись в реестре загрузки файлов не найдена (ИД={id})");
            if (omInputFile.Status_Code != UFKFileProcessingStatus.Loaded) throw new Exception($"У записи в реестре загрузки файлов (ИД={id}) статус должен быть \"Загружен\"");

            omInputFile.Status_Code = UFKFileProcessingStatus.InProcess;
            omInputFile.Save();

            try
            {
                if (omInputFile.TypeFile_Code == TypeFile.Policy)
                {
                    ImportPolicy(omInputFile);
                }
                else if (omInputFile.TypeFile_Code == TypeFile.Certificate)
                {
                    ImportSvd(omInputFile);
                }
                else if (omInputFile.TypeFile_Code == TypeFile.InsuranceContractConcluded)
                {
                    ImportComm(omInputFile);
                }
                else if (omInputFile.TypeFile_Code == TypeFile.PaymentReceived)
                {
                    ImportPayComm(omInputFile);
                }
                else if (omInputFile.TypeFile_Code == TypeFile.InsurancePaymentsUnderContracts)
                {
                    ImportPlComm(omInputFile);
                }
                else if (omInputFile.TypeFile_Code == TypeFile.DeclaredUnsettledInsuranceEvents)
                {
                    ImportPlCommNo(omInputFile);
                }
                else if (omInputFile.TypeFile_Code == TypeFile.InsurancePayments)
                {
                    ImportPl(omInputFile);
                }
                else if (omInputFile.TypeFile_Code == TypeFile.InsurancePaymentsRefusal)
                {
                    ImportPlNo(omInputFile);
                }
                else
                {
                    throw new Exception($"Для файла {omInputFile.TypeFile} нет обработчика");
                }

                omInputFile.Status_Code = UFKFileProcessingStatus.ProcessedCompletely;
                omInputFile.Save();
            }
            catch
            {
                omInputFile.Status_Code = UFKFileProcessingStatus.ImportError;
                omInputFile.Save();
                throw;
            }
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null) { }

        public bool Test() { return true; }

        public string SetContracts()
        {
            int rowCount = 0;
            List<OMInputPlat> omInputPlats = OMInputPlat.Where(x => x.TypeSource_Code == InsuranceSourceType.Sk
                && x.TypeDoc_Code == ContractType.CommonOwnership && x.LinkAllPropertyId == null)
                .Select(x => x.Ndog)
                .Select(x => x.Ndogdat)
                .Select(x => x.Unom)
                .Execute();

            foreach (OMInputPlat omInputPlat in omInputPlats)
            {
                OMAllProperty omAllProperty = OMAllProperty.Where(x => x.Ndogdat == omInputPlat.Ndogdat
                    && (x.Ndog.ToUpper() == omInputPlat.Ndog.ReplaceCharactersWithRussian()
                    || x.Ndog.ToUpper() == omInputPlat.Ndog.ReplaceCharactersWithEnglish()))
                    .ExecuteFirstOrDefault();

                if (omAllProperty == null)
                {
                    omAllProperty = OMAllProperty.Where(x => x.Unom == omInputPlat.Unom
                        && (x.Ndog.ToUpper() == omInputPlat.Ndog.ReplaceCharactersWithRussian()
                        || x.Ndog.ToUpper() == omInputPlat.Ndog.ReplaceCharactersWithEnglish()))
                        .ExecuteFirstOrDefault();
                }

                if (omAllProperty != null)
                {
                    omInputPlat.LinkAllPropertyId = omAllProperty.EmpId;
                    omInputPlat.Save();
                    rowCount++;
                }
            }

            return $"Всего кол-во платежей без связи = {omInputPlats.Count}. Кол-во установленных связей = {rowCount}";
        }

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

        private void ImportPolicy(OMInputFile omInputFile)
        {
            int fspCreated = 0, fspTaken = 0, createFspFailed = 0, takeFspFailed = 0;
            FspService fspService = new FspService();
            decimal? fee = ConfigurationManager.AppSettings["FEE"]?.ParseToDecimal();
            decimal? opl = ConfigurationManager.AppSettings["OPL"]?.ParseToDecimal();
            long? flatTypeId = ConfigurationManager.AppSettings["PRKOM"]?.ParseToLong();
            List<OMPolicySvd> omPolicySvds = OMPolicySvd.Where(x => x.LinkIdFile == omInputFile.EmpId).SelectAll().Execute();

            for (int i = 0; i < omPolicySvds.Count; i++)
            {
                OMPolicySvd omPolicySvd = omPolicySvds[i];
                long fspId;

                try
                {
                    using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                    {
                        try
                        {
                            OMFlat omFlat = OMFlat.Where(x => x.ParentBuilding.Unom == omPolicySvd.Unom && x.Kvnom == omPolicySvd.Kvnom)
                                .ExecuteFirstOrDefault();

                            fspId = new OMFsp
                            {
                                FspNumber = fspService.GetFspNumber(omPolicySvd.Npol, omPolicySvd.Unom, omPolicySvd.Kvnom),
                                FspType_Code = FSPType.Polis,
                                ContractId = omPolicySvd.EmpId,
                                IdReestrContr = OMPolicySvd.GetRegisterId(),
                                ObjId = omFlat?.EmpId,
                                ObjReestrId = OMFlat.GetRegisterId(),
                                Ls = omPolicySvd.Ls,
                                DateOpen = omPolicySvd.Dat ?? DateTime.Now
                            }.Save();
                            omPolicySvd.FspId = fspId;
                            omPolicySvd.Save();
                            fspCreated++;
                        }
                        catch
                        {
                            createFspFailed++;
                            throw;
                        }

                        try
                        {
                            new OMInputPlat
                            {
                                LinkIdFile = omInputFile.EmpId,
                                PolicySvdId = omPolicySvd.EmpId,
                                FspId = fspId,
                                StatusIdentif_Code = StatusIdentifikacii.Identified,
                                PeriodRegDate = omInputFile.PeriodRegDate,
                                Unom = omPolicySvd.Unom,
                                Nom = omPolicySvd.Kvnom,
                                Ls = omPolicySvd.Ls,
                                SumNach = omPolicySvd.Ss,
                                SumOpl = omPolicySvd.Soplvz,
                                Fee = fee,
                                Opl = opl,
                                FlatTypeId = flatTypeId,
                                TypeDoc_Code = ContractType.Dwelling,
                                TypeSource_Code = InsuranceSourceType.Sk
                            }.Save();
                            fspService.AccountFsp(fspId, omInputFile.PeriodRegDate.Value);
                            fspService.CalcBalanceSumFromPeriod(fspId, omInputFile.PeriodRegDate.Value);
                            fspTaken++;
                        }
                        catch
                        {
                            takeFspFailed++;
                            throw;
                        }

                        ts.Complete();
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }

                SetOMQueuePercent(i * 100 / omPolicySvds.Count);
            }

            string message = $"Записей в файле POLICY.DBF, всего: { omPolicySvds.Count }\r\n" +
                $"Записей обработано (созданы ФСП для полисов): {fspCreated}\r\n" +
                $"Записей обработано (оплаты полисов учтены на ФСП): {fspTaken}\r\n" +
                $"Записей не обработано (не удалось создать ФСП): {createFspFailed}\r\n" +
                $"Записей не  обработано(не удалось учесть оплаты полисов на ФСП): {takeFspFailed}";

            SetOMQueueMessage(message);
        }

        private void ImportSvd(OMInputFile omInputFile)
        {
            int successCount = 0, errorCount = 0;
            FspService fspService = new FspService();
            List<OMPolicySvd> omPolicySvds = OMPolicySvd.Where(x => x.LinkIdFile == omInputFile.EmpId).SelectAll().Execute();

            for (int i = 0; i < omPolicySvds.Count; i++)
            {
                OMPolicySvd omPolicySvd = omPolicySvds[i];

                try
                {
                    using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                    {
                        if (omPolicySvd.PlatId == 0 || omPolicySvd.PlatId == 1 || omPolicySvd.PlatId == 2)
                        {
                            string fspNumber = omPolicySvd.Npol;
                            int firstMinus = fspNumber.IndexOf('-');

                            if (firstMinus != -1) fspNumber = fspNumber.Substring(0, firstMinus);

                            if (fspNumber.IsNotEmpty())
                            {
                                OMFsp omFsp = OMFsp.Where(x => x.FspNumber == omPolicySvd.Npol).ExecuteFirstOrDefault();

                                if (omFsp != null)
                                {
                                    omFsp.ContractId = omPolicySvd.EmpId;
                                    omFsp.IdReestrContr = OMPolicySvd.GetRegisterId();
                                    omFsp.Save();
                                }
                            }

                            //OMFlat omFlat = OMFlat.Where(x => x.ParentBuilding.Unom == omPolicySvd.Unom && x.Kvnom == omPolicySvd.Kvnom)
                            //    .ExecuteFirstOrDefault();

                            //if (omFlat != null)
                            //{
                            //    omFsp.ObjId = omFlat.EmpId;
                            //    omFsp.ObjReestrId = OMFlat.GetRegisterId();
                            //    omFsp.Save();
                            //}
                            //omFsp = new OMFsp
                            //{
                            //    FspType_Code = FSPType.Svidetelstvo,
                            //    IdReestrContr = OMPolicySvd.GetRegisterId(),
                            //    FspNumber = fspService.GetFspNumber(omPolicySvd.Npol, omPolicySvd.Unom, omPolicySvd.Kvnom),
                            //    Ls = omPolicySvd.Ls,
                            //    ContractId = omPolicySvd.EmpId,
                            //    DateOpen = omPolicySvd.Dat ?? DateTime.Now,
                            //    ObjId = omFlat?.EmpId,
                            //    ObjReestrId = OMFlat.GetRegisterId()
                            //};

                            //if ((omPolicySvd.Soplvz ?? 0) != 0)
                            //{
                            //    new OMInputPlat
                            //    {
                            //        LinkIdFile = omInputFile.EmpId,
                            //        PolicySvdId = omPolicySvd.EmpId,
                            //        FspId = omFsp.EmpId,
                            //        StatusIdentif_Code = StatusIdentifikacii.Identified,
                            //        PeriodRegDate = omInputFile.PeriodRegDate,
                            //        Unom = omPolicySvd.Unom,
                            //        Nom = omPolicySvd.Kvnom,
                            //        Kodpl = omPolicySvd.Kodpl?.ToString(),
                            //        Ls = omPolicySvd.Ls,
                            //        SumNach = (omPolicySvd.Opl ?? 0m) * (omPolicySvd.Vznos ?? 0m),
                            //        SumOpl = omPolicySvd.Soplvz,
                            //        TypeDoc_Code = ContractType.Dwelling,
                            //        TypeSource_Code = InsuranceSourceType.Sk
                            //    }.Save();
                            //}

                            //fspService.AccountFsp(omFsp.EmpId, omInputFile.PeriodRegDate.Value);
                            //fspService.CalcBalanceSumFromPeriod(omFsp.EmpId, omInputFile.PeriodRegDate.Value);

                        }
                        else if (omPolicySvd.PlatId == 3 || omPolicySvd.PlatId == 4 || omPolicySvd.PlatId == 5)
                        {
                            if (omPolicySvd.Kodpl.HasValue)
                            {
                                OMFsp omFsp = OMFsp.Where(x => x.Kodpl == omPolicySvd.Kodpl.ToString()).ExecuteFirstOrDefault();

                                if (omFsp != null)
                                {
                                    omFsp.ContractId = omPolicySvd.EmpId;
                                    omFsp.IdReestrContr = OMPolicySvd.GetRegisterId();
                                    omFsp.Save();
                                }
                            }
                        }

                        ts.Complete();
                        successCount++;
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                    errorCount++;
                }

                SetOMQueuePercent(i * 100 / omPolicySvds.Count);
            }

            SetOMQueueMessage($"Обработано строк: {successCount}. Количество ошибок: {errorCount}");
        }

        private void ImportComm(OMInputFile omInputFile)
        {
            int linkBuildingCount = 0, linkCalcCount = 0, setPartCount = 0;
            List<OMAllProperty> omAllProperties = OMAllProperty.Where(x => x.LinkIdFile == omInputFile.EmpId).SelectAll().Execute();

            for (int i = 0; i < omAllProperties.Count; i++)
            {
                OMAllProperty omAllProperty = omAllProperties[i];

                try
                {
                    using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                    {
                        omAllProperty.ObjId = OMBuilding.Where(x => x.Unom == omAllProperty.Unom).ExecuteFirstOrDefault()?.EmpId;

                        if (!omAllProperty.ObjId.HasValue) continue;

                        OMAgreementProject omAgreementProject = OMAgreementProject
                            .Where(x => x.ParentParamCalculation.ObjId == omAllProperty.ObjId
                                && x.SizeBonusMkd == omAllProperty.RasPripay
                                && (x.ProgectNum.ToUpper() == omAllProperty.Ndog.ReplaceCharactersWithRussian()
                                || x.ProgectNum.ToUpper() == omAllProperty.Ndog.ReplaceCharactersWithEnglish()))
                            .Select(x => x.PartMoscow)
                            .Select(x => x.ParentParamCalculation.EmpId)
                            .ExecuteFirstOrDefault();

                        if (omAgreementProject != null)
                        {
                            OMParamCalculation omParamCalculation = omAgreementProject.ParentParamCalculation;
                            omParamCalculation.ContractId = omAllProperty.EmpId;
                            omParamCalculation.Save();

                            if ((omAllProperty.PartCity ?? 0) == 0)
                            {
                                omAllProperty.PartCity = omAgreementProject.PartMoscow;
                            }

                            if (omAllProperty.PartCity.HasValue) setPartCount++;
                            linkCalcCount++;
                        }

                        omAllProperty.Save();

                        linkBuildingCount++;
                        ts.Complete();
                    }

                    using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                    {

                        #region Связка договора с платежами.

                        List<OMInputPlat> omInputPlats = OMInputPlat.Where(x => (x.Ndogdat == omAllProperty.Ndogdat || x.Unom == omAllProperty.Unom)
                                                                                   && (x.LinkAllPropertyId != omAllProperty.EmpId || x.LinkAllPropertyId == null)
                                                                                   && (x.Ndog.ToUpper() == omAllProperty.Ndog.ReplaceCharactersWithRussian()
                                                                                   || x.Ndog.ToUpper() == omAllProperty.Ndog.ReplaceCharactersWithEnglish()))
                                                                                   .Select(x => x.LinkAllPropertyId)
                                                                                   .Execute();

                        if (omInputPlats != null && omInputPlats.Any())
                        {
                            foreach (OMInputPlat omInputPlat in omInputPlats)
                            {
                                omInputPlat.LinkAllPropertyId = omAllProperty.EmpId;
                                omInputPlat.Save();
                            }
                        }
                        // Если не находим, запускаем массовую связку.
                        else
                        {
                            SetContracts();
                        }

                        #endregion

                        ts.Complete();
                    }

                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }
                finally
                {
                    SetOMQueuePercent(i * 100 / omAllProperties.Count);
                }
            }

            string message = $"Записей в файле COMM.DBF, всего: { omAllProperties.Count }\r\n" +
                $"Записей обработано (договора страхования по ОИ связаны с объектами): {linkBuildingCount}\r\n" +
                $"Записей обработано (договора связаны по ОИ связаны с расчетами): {linkCalcCount}\r\n" +
                $"Записей обработано (в договорах страхования заполнена доля города): {setPartCount}\r\n" +
                $"Записей не обработано (договора страхования по ОИ не связаны с объектами): {omAllProperties.Count - linkBuildingCount}\r\n" +
                $"Записей не обработано (договора связаны по ОИ не связаны с расчетами): {omAllProperties.Count - linkCalcCount}\r\n" +
                $"Записей не обработано (в договорах страхования не заполнена доля города): {omAllProperties.Count - setPartCount}";

            SetOMQueueMessage(message);
        }

        private void ImportPayComm(OMInputFile omInputFile)
        {
            int linkCount = 0;
            List<OMInputPlat> omInputPlats = OMInputPlat.Where(x => x.LinkIdFile == omInputFile.EmpId)
                .SelectAll()
                .Execute();

            for (int i = 0; i < omInputPlats.Count; i++)
            {
                OMInputPlat omInputPlat = omInputPlats[i];

                try
                {
                    OMAllProperty omAllProperty = OMAllProperty.Where(x => x.Ndogdat == omInputPlat.Ndogdat
                        && (x.Ndog.ToUpper() == omInputPlat.Ndog.ReplaceCharactersWithRussian()
                        || x.Ndog.ToUpper() == omInputPlat.Ndog.ReplaceCharactersWithEnglish()))
                        .Select(x => x.Ndog)
                        .ExecuteFirstOrDefault();

                    if (omAllProperty == null)
                    {
                        omAllProperty = OMAllProperty.Where(x => x.Unom == omInputPlat.Unom
                            && (x.Ndog.ToUpper() == omInputPlat.Ndog.ReplaceCharactersWithRussian()
                            || x.Ndog.ToUpper() == omInputPlat.Ndog.ReplaceCharactersWithEnglish()))
                            .ExecuteFirstOrDefault();
                    }

                    if (omAllProperty != null)
                    {
                        List<OMInputPlat> otherOMInputPlats = OMInputPlat.Where(x => x.LinkAllPropertyId == omAllProperty.EmpId
                            && x.EmpId != omInputPlat.EmpId
                            && x.SumOpl == omInputPlat.SumOpl
                            && x.PmtDate == omInputPlat.PmtDate).Execute();

                        if (otherOMInputPlats.Any())
                        {
                            throw new Exception($"Платеж уже существует в системе, привязанный к договору {omAllProperty.Ndog}");
                        }

                        omInputPlat.LinkAllPropertyId = omAllProperty.EmpId;
                        omInputPlat.Save();
                        linkCount++;
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }

                SetOMQueuePercent(i * 100 / omInputPlats.Count);
            }

            string message = $"Записей в файле PAY_COMM, всего: {omInputPlats.Count}\r\n" +
                $"Записей обработано  (платежи по ОИ связаны с договорами): {linkCount}\r\n" +
                $"Записей не обработано (платежи по ОИ не удалось связать с договорами): {omInputPlats.Count - linkCount}\r\n";

            SetOMQueueMessage(message);
        }

        private void ImportPlComm(OMInputFile omInputFile)
        {
            int linkBuildingCount = 0, linkDamageCount = 0;
            List<OMPayTo> omPayTos = OMPayTo.Where(x => x.LinkIdFile == omInputFile.EmpId)
                .Select(x => x.Ndoc).Select(x => x.Ndogdat).Execute();

            for (int i = 0; i < omPayTos.Count; i++)
            {
                OMPayTo omPayTo = omPayTos[i];

                try
                {
                    OMDamage omDamage = OMDamage.Where(x => x.SumDamage == omPayTo.Sl && x.ObjReestrId == OMBuilding.GetRegisterId())
                        .Select(x => x.ObjId).ExecuteFirstOrDefault();

                    if (omDamage != null)
                    {
                        omPayTo.LinkDamageId = omDamage.EmpId;
                        omPayTo.ObjId = omDamage.ObjId;
                        omPayTo.ObjReestrId = OMBuilding.GetRegisterId();

                        linkDamageCount++;
                        if (omPayTo.ObjId.HasValue) linkBuildingCount++;
                    }

                    omPayTo.ContractId = OMAllProperty.Where(x => x.Ndog == omPayTo.Ndoc && x.Ndogdat == omPayTo.Ndogdat).ExecuteFirstOrDefault()?.EmpId;
                    omPayTo.Save();
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }

                SetOMQueuePercent(i * 100 / omPayTos.Count);
            }

            string message = $"Записей в файле PL_COMM.DBF, всего: {omPayTos.Count}\r\n" +
                $"Записей обработано (выплаты связаны с объектами): {linkBuildingCount}\r\n" +
                $"Записей обработано (выплаты связаны с делами по расчету ущерба в ОИ): {linkDamageCount}\r\n" +
                $"Записей не обработано (не удалось связать с объектами): {omPayTos.Count - linkBuildingCount}\r\n" +
                $"Записей не обработано (не удалось связать выплаты с делами по расчету ущерба в ОИ): {omPayTos.Count - linkDamageCount}";

            SetOMQueueMessage(message);
        }

        private void ImportPlCommNo(OMInputFile omInputFile)
        {
            int linkBuildingCount = 0, linkContractCount = 0;
            List<OMNoPay> omNoPays = OMNoPay.Where(x => x.LinkIdFile == omInputFile.EmpId)
                .Select(x => x.Ndog)
                .Select(x => x.Ndogdat)
                .Select(x => x.Unom)
                .Execute();

            for (int i = 0; i < omNoPays.Count; i++)
            {
                try
                {
                    omNoPays[i].ContractId = OMAllProperty.Where(x => x.Ndog == omNoPays[i].Ndog && x.Ndogdat == omNoPays[i].Ndogdat).ExecuteFirstOrDefault()?.EmpId;
                    omNoPays[i].ObjId = OMBuilding.Where(x => x.Unom == omNoPays[i].Unom).ExecuteFirstOrDefault()?.EmpId;
                    omNoPays[i].ObjReestrId = OMBuilding.GetRegisterId();
                    omNoPays[i].Save();

                    if (omNoPays[i].ContractId.HasValue) linkContractCount++;
                    if (omNoPays[i].ObjId.HasValue) linkBuildingCount++;
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }

                SetOMQueuePercent(i * 100 / omNoPays.Count);
            }

            string message = $"Записей в файле PL_COMM_NO.DBF, всего: {omNoPays.Count}\r\n" +
                $"Записей обработано (отказы в выплатах по ОИ связаны с объектами): {linkBuildingCount}\r\n" +
                $"Записей обработано (отказы в выплатах по ОИ связаны с договорами страхования): {linkContractCount}\r\n" +
                $"Записей не обработано (не удалось связать отказы в выплатах по ОИ с объектами): {omNoPays.Count - linkBuildingCount}\r\n" +
                $"Записей не обработано (не удалось связать отказы в выплатах по ОИ с договорами страхования): {omNoPays.Count - linkContractCount}";

            SetOMQueueMessage(message);
        }

        private void ImportPl(OMInputFile omInputFile)
        {
            int linkFlatCount = 0, linkDamageCount = 0;
            List<OMPayTo> omPayTos = OMPayTo.Where(x => x.LinkIdFile == omInputFile.EmpId).SelectAll().Execute();

            for (int i = 0; i < omPayTos.Count; i++)
            {
                OMPayTo omPayTo = omPayTos[i];

                try
                {
                    omPayTo.ContractId = OMPolicySvd.Where(x => x.Npol == omPayTo.Npol).ExecuteFirstOrDefault()?.EmpId;
                    omPayTo.ObjId = OMFlat.Where(x => x.Unom == omPayTo.Unom && x.Kvnom == omPayTo.Nom).ExecuteFirstOrDefault()?.EmpId;
                    omPayTo.ObjReestrId = OMFlat.GetRegisterId();
                    omPayTo.LinkDamageId = OMDamage.Where(x => x.SumDamage == omPayTo.Sl && x.ObjReestrId == OMFlat.GetRegisterId()).ExecuteFirstOrDefault()?.EmpId;
                    omPayTo.Save();

                    if (omPayTo.ObjId.HasValue) linkFlatCount++;
                    if (omPayTo.LinkDamageId.HasValue) linkDamageCount++;
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }

                SetOMQueuePercent(i * 100 / omPayTos.Count);
            }

            string message = $"Записей в файле PL.DBF, всего: { omPayTos.Count }\r\n" +
                $"Записей обработано (выплаты связаны с объектами): {linkFlatCount}\r\n" +
                $"Записей обработано (выплаты связаны с делами по расчету ущерба в ЖП): {linkDamageCount}\r\n" +
                $"Записей не обработано (не удалось связать с объектами): {omPayTos.Count - linkFlatCount}\r\n" +
                $"Записей не обработано (не удалось связать выплаты с делами по расчету ущерба в ЖП): {omPayTos.Count - linkDamageCount}";

            SetOMQueueMessage(message);
        }

        private void ImportPlNo(OMInputFile omInputFile)
        {
            int linkFlatCount = 0, linkPolicyCount = 0;
            List<OMNoPay> omNoPays = OMNoPay.Where(x => x.LinkIdFile == omInputFile.EmpId).Select(x => x.Npol).Execute();

            for (int i = 0; i < omNoPays.Count; i++)
            {
                try
                {
                    OMPolicySvd omPolicySvd = OMPolicySvd.Where(x => x.Npol == omNoPays[i].Npol).ExecuteFirstOrDefault();

                    if (omPolicySvd != null)
                    {
                        OMFsp omFsp = OMFsp.Where(x => x.ContractId == omPolicySvd.EmpId && x.IdReestrContr == OMPolicySvd.GetRegisterId())
                            .Select(x => x.ObjId).ExecuteFirstOrDefault();

                        omNoPays[i].ContractId = omPolicySvd.EmpId;
                        omNoPays[i].ObjId = omFsp?.ObjId;
                        omNoPays[i].ObjReestrId = OMFlat.GetRegisterId();
                        omNoPays[i].Save();

                        linkPolicyCount++;
                        if (omNoPays[i].ObjId.HasValue) linkFlatCount++;
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }

                SetOMQueuePercent(i * 100 / omNoPays.Count);
            }

            string message = $"Записей в файле PL_NO.DBF, всего: { omNoPays.Count }\r\n" +
                $"Записей обработано (отказы в выплатах связаны с объектами): {linkFlatCount}\r\n" +
                $"Записей обработано (отказы в выплатах связаны с договорами страхования): {linkPolicyCount}\r\n" +
                $"Записей не обработано (не удалось связать отказы в выплатах с объектами): {omNoPays.Count - linkFlatCount}\r\n" +
                $"Записей не обработано (не удалось связать отказы в выплатах с договорами страхования): {omNoPays.Count - linkPolicyCount}";

            SetOMQueueMessage(message);
        }
    }
}