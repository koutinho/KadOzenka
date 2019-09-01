using System;

namespace CIPJS.DAL.SK
{
    public class PlImportModel
    {
        public long? Kod { get; set; }

        public long? Aok { get; set; }

        public long? Unom { get; set; }

        public string Nom { get; set; }

        public string Nomi { get; set; }

        public string Npol { get; set; }

        public DateTime? NpolDate { get; set; }

        public string ComNumber { get; set; }

        public DateTime? ComDate { get; set; }

        public decimal? Sl { get; set; }

        public decimal? SpFact { get; set; }

        public decimal? SpNo { get; set; }
     
        public string FactNumber { get; set; }

        public DateTime? FactDate { get; set; }
    }
}