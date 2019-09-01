using CIPJS.DAL.StrahNach;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Misc;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;

namespace CIPJS.DAL.Fsp
{
    public class FspOplKodoplUpdateProcess : ILongProcess
    {
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            DateTime periodRegDate = new DateTime(2018, 1, 1);

            FspService fspService = new FspService();
            StrahNachService strahNachService = new StrahNachService();
            Dictionary<DateTime, OMTariff> periodTariffs = new Dictionary<DateTime, OMTariff>();
            DateTime actualPeriod = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime nextPeriod = new DateTime(periodRegDate.Year, periodRegDate.Month, 1);

            while (actualPeriod > nextPeriod)
            {
                OMTariff tariff = OMTariff.Where(x => x.DateBegin <= nextPeriod)
                    .OrderByDescending(x => x.DateBegin)
                    .SetPackageIndex(0)
                    .SetPackageSize(1)
                    .SelectAll()
                    .ExecuteFirstOrDefault();
                periodTariffs.Add(nextPeriod, tariff);
                nextPeriod = nextPeriod.AddMonths(1);
            }

            List<OMFsp> fspList = OMFsp.Where(x => x.OplKodpl == null || x.OplKodpl == 0)
                .Select(x => x.OplKodpl)
                .Select(x => x.Kodpl)
                .Execute();

            foreach(OMFsp fsp in fspList)
            {
                try
                {
                    using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                    {
                        string kodPl = fsp.Kodpl;
                        long? districtId = null;
                        DateTime? oplPeriodRegDate = null;
                        decimal? opl = null;
                        decimal? sum = null;
                        long? flatTypeId = null;
                        decimal? fopl = null;

                        //удаляем текущие контрольные начисления
                        List<OMInputNach> strahNachList = OMInputNach.Where(x => x.FspId == fsp.EmpId && x.TypeSource_Code == InsuranceSourceType.Gbu).Execute();
                        foreach (OMInputNach strahNach in strahNachList)
                        {
                            strahNach.Destroy();
                        }

                        OMInputNach inputNach = OMInputNach.Where(x => x.FspId == fsp.EmpId && x.TypeSource_Code == InsuranceSourceType.Mfc
                            && ((x.Opl != null && x.Opl != 0) || (x.Fopl != null && x.Fopl != 0) || (x.SumNach != null && x.SumNach != 0)))
                            .Select(x => x.DistrictId)
                            .Select(x => x.PeriodRegDate)
                            .Select(x => x.Opl)
                            .Select(x => x.SumNach)
                            .Select(x => x.FlatTypeId)
                            .Select(x => x.Fopl)
                            .ExecuteFirstOrDefault();

                        if (inputNach != null)
                        {
                            districtId = inputNach.DistrictId;
                            oplPeriodRegDate = inputNach.PeriodRegDate;
                            opl = inputNach.Opl;
                            sum = inputNach.SumNach;
                            flatTypeId = inputNach.FlatTypeId;
                            fopl = inputNach.Fopl;
                        }
                        else
                        {
                            OMInputPlat inputPlat = OMInputPlat.Where(x => x.FspId == fsp.EmpId && x.TypeSource_Code == InsuranceSourceType.Mfc)
                                .Select(x => x.DistrictId)
                                .Select(x => x.PeriodRegDate)
                                .Select(x => x.Opl)
                                .Select(x => x.SumOpl)
                                .Select(x => x.FlatTypeId)
                                .ExecuteFirstOrDefault();

                            if (inputPlat != null)
                            {
                                districtId = inputPlat.DistrictId;
                                oplPeriodRegDate = inputPlat.PeriodRegDate;
                                opl = inputPlat.Opl;
                                sum = inputPlat.SumOpl;
                                flatTypeId = inputPlat.FlatTypeId;
                            }
                        }

                        fsp.OplKodpl = fspService.GetOplKodpl(districtId, oplPeriodRegDate, kodPl, opl, sum, flatTypeId, fopl);
                        fsp.Save();

                        //создаем новые контрольные начисления
                        foreach (KeyValuePair<DateTime, OMTariff> periodTariff in periodTariffs)
                        {
                            fspService.CreateGbuInGbuCache(fsp.EmpId, periodTariff.Key, fsp, periodTariff.Value);
                        }

                        fspService.AccountFsp(fsp.EmpId, periodRegDate);
                        fspService.CalcBalanceSumFromPeriod(fsp.EmpId, periodRegDate);

                        ts.Complete();
                    }
                }
                catch(Exception ex)
                {
                    ErrorManager.LogError(ex);
                }
            }
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
        }

        public bool Test()
        {
            return true;
        }
    }
}
