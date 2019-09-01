using System;
using System.Collections.Generic;

namespace CIPJS.Models.Tenements
{
    public class ConsolidatedDataValueDto
    {
        public string Value { get; set; }

        public DateTime? PeriodRegDate { get; set; }

        public int? AttributeId { get; set; }

        public long? ObjectId { get; set; }

        public int ReestrId { get; set; }

        public bool HaveHistory { get; set; }

        public List<ConsolidatedDataHistoryValueDto> HistoryValues { get; set; }
    }
}