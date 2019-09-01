using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace CIPJS.DAL.StrahNach
{
    /// <summary>
    /// Сервис контрольных начислений
    /// </summary>
    public class StrahNachService
    {
        /// <summary>
        /// CIPJS-861: Переработать обработку файлов МФЦ без сохранения контрольных начислений в БД.
        /// Локальное хранилище данных о контрольных зачислениях.
        /// </summary>
        //private ConcurrentDictionary<long, OMInputNach> GbuCache = new ConcurrentDictionary<long, OMInputNach>();
        private ConcurrentDictionary<long?, List<OMInputNach>> GbuCache = new ConcurrentDictionary<long?, List<OMInputNach>>();

        /// <summary>
        /// Получает контрольное начисление по ФСП и периоду из кэша
        /// </summary>
        /// <param name="fspId">Идентификатор ФСП</param>
        /// <param name="periodRegDate">Период учета</param>
        /// <returns></returns>
        public OMInputNach GetByFspIdPeriodFromGbuCache(long fspId, DateTime periodRegDate)
        {
            //return GbuCache
            //        .FirstOrDefault(x => x.Value.FspId == fspId && x.Value.PeriodRegDate == periodRegDate
            //        && x.Value.TypeSource_Code == InsuranceSourceType.Gbu
            //        && x.Value.StatusIdentif_Code == StatusIdentifikacii.Identified).Value;

            List<OMInputNach> nach;
            if (GbuCache.TryGetValue(fspId, out nach))
            {
                return nach.FirstOrDefault(x => x.FspId == fspId && x.PeriodRegDate == periodRegDate
                           && x.TypeSource_Code == InsuranceSourceType.Gbu
                           && x.StatusIdentif_Code == StatusIdentifikacii.Identified);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Создает контрольное начисление по ФСП и периоду в кэше
        /// </summary>
        /// <param name="fspId">Идентификатор ФСП</param>
        /// <param name="periodRegDate">Период учета</param>
        public OMInputNach CreateByFspIdPeriodInGbuCache(long fspId, DateTime periodRegDate, OMFsp fsp = null, OMTariff tariff = null)
        {

            if (tariff == null)
            {
                tariff = OMTariff.Where(x => x.DateBegin <= periodRegDate)
                .OrderByDescending(x => x.DateBegin)
                .SetPackageIndex(0)
                .SetPackageSize(1)
                .SelectAll()
                .Execute()
                .FirstOrDefault();
            }

            if (tariff == null)
            {
                throw new Exception($"Не удалось определить тариф для периода {periodRegDate:dd.MM.yyyy}");
            }

            if (!tariff.Value.HasValue)
            {
                throw new Exception($"Для периода {periodRegDate:dd.MM.yyyy} у тарифа не заполнено значение тарифа");
            }

            OMInputNach strahNach = SetGbuInputNachWithFspByPeriod(fspId, periodRegDate, tariff);

            if (strahNach == null)
            {
                if (fsp != null)
                {
                    strahNach = new OMInputNach
                    {
                        FspId = fsp.EmpId,
                        TypeSource_Code = InsuranceSourceType.Gbu,
                        StatusIdentif_Code = StatusIdentifikacii.Identified,
                        PeriodRegDate = periodRegDate,
                        Opl = fsp.OplKodpl,
                        Kodpl = fsp.Kodpl,
                        SumNach = fsp.OplKodpl.HasValue ? Math.Round(tariff.Value.Value * fsp.OplKodpl.Value, 2, MidpointRounding.AwayFromZero) : 0m
                    };
                }
            }
            if (strahNach != null)
            {
                var strahNachColl = GbuCache.GetOrAdd(strahNach.FspId, new List<OMInputNach>());
                strahNachColl.Add(strahNach);
            }
            
            return strahNach;
        }
        /// <summary>
        /// Установка значения контрольных начислений в зависимости от площадей периода.
        /// </summary>
        /// <param name="fspId"></param>
        /// <param name="periodRegDate"></param>
        /// <param name="tariff"></param>
        /// <returns></returns>
        private OMInputNach SetGbuInputNachWithFspByPeriod(long fspId, DateTime periodRegDate, OMTariff tariff)
        {
            var periodDate = DBMngr.Realty.FormatDate(periodRegDate);
            string sql = $@"select emp_id, opl_kodpl, kodpl from insur_fsp_q
                            where emp_id = {fspId} and date_trunc('day', s_) <= date_trunc('day', {periodDate}::date) 
                            and  date_trunc('day', {periodDate}::date) <= date_trunc('day', po_)
                            limit 1";

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
            using (var reader = DBMngr.Realty.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    var emp_id = reader.GetInt64(0);
                    var opl_kodpl = reader.GetDecimal(1);
                    var kodpl = reader.IsDBNull(2) ? null : reader.GetString(2);

                    return new OMInputNach
                    {
                        FspId = emp_id,
                        TypeSource_Code = InsuranceSourceType.Gbu,
                        StatusIdentif_Code = StatusIdentifikacii.Identified,
                        PeriodRegDate = periodRegDate,
                        Opl = opl_kodpl,
                        Kodpl = kodpl,
                        SumNach = opl_kodpl != 0 ? Math.Round(tariff.Value.Value * opl_kodpl, 2, MidpointRounding.AwayFromZero) : 0m
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// Получает контрольное начисление по ФСП и периоду
        /// </summary>
        /// <param name="fspId">Идентификатор ФСП</param>
        /// <param name="periodRegDate">Период учета</param>
        /// <returns></returns>
        public OMInputNach GetByFspIdPeriod(long fspId, DateTime periodRegDate)
        {
            return OMInputNach
                .Where(x => x.FspId == fspId && x.PeriodRegDate == periodRegDate
                    && x.TypeSource_Code == InsuranceSourceType.Gbu
                    && x.StatusIdentif_Code == StatusIdentifikacii.Identified)
                .SelectAll()
                .Execute()
                .FirstOrDefault();
        }

        /// <summary>
        /// Создает контрольное начисление по ФСП и периоду
        /// </summary>
        /// <param name="fspId">Идентификатор ФСП</param>
        /// <param name="periodRegDate">Период учета</param>
        public OMInputNach CreateByFspIdPeriod(long fspId, DateTime periodRegDate, OMFsp fsp = null, OMTariff tariff = null)
        {
            if(fsp == null)
            {
                fsp = OMFsp.Where(x => x.EmpId == fspId)
                    .Select(x => x.OplKodpl)
                    .Select(x => x.Kodpl)
                    .ExecuteFirstOrDefault();
            }
            
            if (fsp == null)
            {
                throw new Exception($"Не удалось определить ФСП по идентификатору: {fspId}");
            }

            if(tariff == null)
            {
                tariff = OMTariff.Where(x => x.DateBegin <= periodRegDate)
                .OrderByDescending(x => x.DateBegin)
                .SetPackageIndex(0)
                .SetPackageSize(1)
                .SelectAll()
                .Execute()
                .FirstOrDefault();
            }

            if (tariff == null)
            {
                throw new Exception($"Не удалось определить тариф для периода {periodRegDate:dd.MM.yyyy}");
            }

            if (!tariff.Value.HasValue)
            {
                throw new Exception($"Для периода {periodRegDate:dd.MM.yyyy} у тарифа не заполнено значение тарифа");
            }

			OMInputNach strahNach = new OMInputNach
			{
				FspId = fsp.EmpId,
				TypeSource_Code = InsuranceSourceType.Gbu,
				StatusIdentif_Code = StatusIdentifikacii.Identified,
				PeriodRegDate = periodRegDate,
				Opl = fsp.OplKodpl,
				Kodpl = fsp.Kodpl,
				SumNach = fsp.OplKodpl.HasValue ? Math.Round(tariff.Value.Value * fsp.OplKodpl.Value, 2, MidpointRounding.AwayFromZero) : 0m
			};

			strahNach.Save();

            return strahNach;
        }

        public decimal? GetSumNachByPeriodByCache(long fspId, DateTime periodRegDate, bool withFlagOpl = false)
        {
            //Отобрать в Реестре начислений INSUR_INPUT_NACH записи, для которых:
            //INSUR_INPUT_ NACH.PERIOD = Период
            //INSUR_INPUT_ NACH.FSP_ID = INSUR_BALANCE.FSP_ID
            //INSUR_INPUT_ NACH.STATUS_IDENTIF = «Идентифицирован»

            List<OMInputNach> nach;
            if (GbuCache.TryGetValue(fspId, out nach))
            {
                return nach.FirstOrDefault(x => x.FspId == fspId 
                                                && x.PeriodRegDate == periodRegDate
                                                && x.TypeSource_Code == InsuranceSourceType.Gbu
                                                && x.StatusIdentif_Code == StatusIdentifikacii.Identified)?.SumNach;
            }
            else
            {
                return 0;
            }
        }
    }
}
