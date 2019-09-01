using ObjectModel.Directory;
using System.Collections.Generic;

namespace CIPJS.Models.ReestrPay
{
    public class FormDto
    {
        public List<long?> Ids { get; set; }

        public ReestrPayType Type { get; set; }
    }
}