using CIPJS.Models.InputPlat;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.Models.BankPlat
{
    public class LinkBankPlatDto
    {
        public long Id { get; set; }
        public bool CreateMode { get; set; }
        public DateTime? PeriodRegDate { get; set; }
        public DateTime? DataPp { get; set; }
        public string Kodpl { get; set; }
        public decimal? SumByCode { get; set; }
        public string NomDoc { get; set; }
        public long? FlagVozvr { get; set; }
        public DateTime? Period { get; set; }
        public long? CodDoc { get; set; }

        #region Поля начисления
        public long? NachUnom { get; set; }
        public string NachAdresT { get; set; }
        public string NachKvnom { get; set; }
        public decimal? NachOpl { get; set; }
        #endregion

        public List<InputPlatDto> InputPlats { get; set; }

        public static LinkBankPlatDto OMMap(OMBankPlat entity)
        {
            LinkBankPlatDto bankPlatDto = new LinkBankPlatDto
            {
                Id = entity.EmpId.Value,
                PeriodRegDate = entity.PeriodRegDate,
                DataPp = entity.DataPp,
                Kodpl = entity.Kodpl,
                SumByCode = entity.SumByCode,
                NomDoc = entity.NomDoc,
                CodDoc = entity.CodDoc,
                FlagVozvr = entity.FlagVozvr,
                Period = entity.Period,
                CreateMode = false
            };

            //CIPJS-473 окне создания новой записи МФЦ подкидывать и поля, которые выделены стрелками. 
            //В INSUR_INPUT_NACH ищем строку , для который PERIOD_REG_DATE =максимальный И KODLP= коду плательщика из банковской строки.
            OMInputNach inputNach = OMInputNach.Where(x => 
                x.PeriodRegDate == entity.PeriodRegDate
                && x.DistrictId == entity.DistrictId
                && x.Kodpl == entity.Kodpl)
                .Select(x => x.AdresT)
                .Select(x => x.Unom)
                .Select(x => x.Kvnom)
                .Select(x => x.Opl)
                .OrderByDescending(x => x.PeriodRegDate)
                .SetPackageIndex(0)
                .SetPackageSize(1)
                .ExecuteFirstOrDefault();

            if (inputNach != null)
            {
                bankPlatDto.NachAdresT = inputNach.AdresT;
                bankPlatDto.NachUnom = inputNach.Unom;
                bankPlatDto.NachKvnom = inputNach.Kvnom;
                bankPlatDto.NachOpl = inputNach.Opl;
            }

            List<OMInputPlat> inputPlats = OMInputPlat.Where(x => x.LinkBankId == bankPlatDto.Id).SelectAll().Execute();
            if (inputPlats.Count == 0)
            {
                bankPlatDto.CreateMode = true;
                inputPlats = OMInputPlat.Where(x => x.PeriodRegDate == bankPlatDto.PeriodRegDate
                    && x.Kodpl == bankPlatDto.Kodpl
                    && x.LinkBankId == null
                    && x.StatusIdentif_Code == StatusIdentifikacii.NotIdentified)
                    .SelectAll()
                    .Execute();
            }

            bankPlatDto.InputPlats = inputPlats.Select(x => InputPlatDto.OMMap(x)).ToList();

            return bankPlatDto;
        }
    }
}
