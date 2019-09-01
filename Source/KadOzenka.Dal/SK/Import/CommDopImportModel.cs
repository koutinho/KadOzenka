using System;

namespace CIPJS.DAL.SK
{
    public class CommDopImportModel
    {
        public long? Kod { get; set; }

        public long? Unom { get; set; }

        public string Ndog { get; set; }

        public DateTime? NdogDat { get; set; }

        public decimal? Ndops { get; set; }

        public long? St1 { get; set; }

        public long? St2 { get; set; }

        public long? St3 { get; set; }

        public long? Ss1 { get; set; }

        public long? Ss2 { get; set; }

        public long? Ss3 { get; set; }

        public decimal? Part { get; set; }

        public decimal? PartCity { get; set; }

        public decimal? Paysign { get; set; }

        public decimal? RasPripay { get; set; }
    }
}