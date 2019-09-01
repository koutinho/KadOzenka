using System;

namespace CIPJS.DAL.SK
{
    public class PlCommImportModel
    {
        public long? Kod { get; set; }

        public long? Aok { get; set; }

        public string ComNumber { get; set; }

        public DateTime? ComDate { get; set; }

        public decimal? Sl { get; set; }

        public decimal? SpFact { get; set; }

        public decimal? SpNo { get; set; }

        public string FactNumber { get; set; }

        public DateTime? FactDate { get; set; }

        public string Name { get; set; }

        public string Ndoc { get; set; }

        public DateTime? NdogDat { get; set; }
    }
}