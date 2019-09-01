using System;

namespace CIPJS.Models.Tenements
{
    public class HistoryModelDto
    {
        public string Value { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string UserName { get; set; }
    }
}