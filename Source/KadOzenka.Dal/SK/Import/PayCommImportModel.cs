using System;

namespace CIPJS.DAL.SK
{
    public class PayCommImportModel
    {
        public long? Unom { get; set; }

        public DateTime? PayDat { get; set; }

        public decimal? Pripay { get; set; }

        public long? Kod { get; set; }

        public string Ndog { get; set; }

        public DateTime? NdogDat { get; set; }

        public string Ndops { get; set; }

        public string PayNumber { get; set; }
    }
}