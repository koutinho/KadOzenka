using System;

namespace CIPJS.Models.ReestrPay
{
    public class ChangeDateDto
    {
        public long PayId { get; set; }

        public DateTime? Date { get; set; }
    }
}