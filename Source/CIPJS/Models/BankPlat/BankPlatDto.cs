using ObjectModel.Insur;
using System;

namespace CIPJS.Models.BankPlat
{
    public class BankPlatDto
    {
        public long Id { get; set; }
        public DateTime? PeriodRegDate { get; set; }
        public DateTime? DataPp { get; set; }
        public string Kodpl { get; set; }
        public decimal? SumByCode { get; set; }
        public string NomDoc { get; set; }
        public long? FlagVozvr { get; set; }
        public DateTime? Period { get; set; }
        public long? CodDoc { get; set; }

        public static BankPlatDto OMMap(OMBankPlat entity)
        {
            return new BankPlatDto
            {
                Id = entity.EmpId.Value,
                PeriodRegDate = entity.PeriodRegDate,
                DataPp = entity.DataPp,
                Kodpl = entity.Kodpl,
                SumByCode = entity.SumByCode,
                NomDoc = entity.NomDoc,
                FlagVozvr = entity.FlagVozvr,
                Period = entity.Period,
                CodDoc = entity.CodDoc
            };
        }
    }
}
