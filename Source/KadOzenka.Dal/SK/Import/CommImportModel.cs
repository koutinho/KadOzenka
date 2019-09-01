using System;

namespace CIPJS.DAL.SK
{
    public class CommImportModel
    {
        public long? Kod { get; set; }

        public long? Aok { get; set; }

        public long? Unom { get; set; }

        public long? OrgId { get; set; }

        public string OrgType { get; set; }

        public string Name { get; set; }

        public string Ndog { get; set; }

        public DateTime? NdogDat { get; set; }

        public decimal? St1 { get; set; }

        public decimal? St2 { get; set; }

        public decimal? St3 { get; set; }

        public decimal? Ss1 { get; set; }

        public decimal? Ss2 { get; set; }

        public decimal? Ss3 { get; set; }

        public decimal? Part { get; set; }

        public decimal? PartCity { get; set; }

        public string Paysign { get; set; }

        public decimal? RasPripay { get; set; }

        public string Inn { get; set; }
    }
}