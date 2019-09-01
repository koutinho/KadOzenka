using System;

namespace CIPJS.Models.Tenements
{
    public class ConsolidatedDataHistoryValueDto
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Value { get; set; }

        public string ChangeUser { get; set; }
    }
}