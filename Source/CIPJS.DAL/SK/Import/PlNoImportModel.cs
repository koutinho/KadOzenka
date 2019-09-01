using System;

namespace CIPJS.DAL.SK
{
    public class PlNoImportModel
    {
        public long? Kod { get; set; }

        public long? Aok { get; set; }

        public string Npol { get; set; }

        public DateTime? NpolDate { get; set; }

        public DateTime? EventDat { get; set; }

        public DateTime? AppDat { get; set; }

        public string Reject { get; set; }

        public string ReNumber { get; set; }

        public DateTime? ReDat { get; set; }
    }
}