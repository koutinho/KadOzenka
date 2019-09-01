using System;

namespace CIPJS.DAL.SK
{
    public class PlCommNoImportModel
    {
        public long? Kod { get; set; }

        public long? Aok { get; set; }

        public DateTime? EventDat { get; set; }

        public DateTime? AppDat { get; set; }

        public string Reject { get; set; }

        public long? Unom { get; set; }

        public long? OrgId { get; set; }

        public string OrgType { get; set; }

        public string Name { get; set; }

        public string Ndog { get; set; }

        public DateTime? NdogDat { get; set; }

        public DateTime? PhoneDat { get; set; }

        public string InspNumber { get; set; }

        public DateTime? InspDat { get; set; }

        public string Reason { get; set; }

        public string Reasabs { get; set; }

        public string ReNumber { get; set; }

        public DateTime? ReDat { get; set; }
    }
}