using System.Collections.Generic;

namespace CIPJS.Models.Fsp
{
    public class LinkManyToFlatViewModel
    {
        public ICollection<long> FspIds { get; set; }

        public long? CurrentBuildingId { get; set; }

        public string CurrentUnom { get; set; }

        public string CurrentAddress { get; set; }

        public long? AltBuildingId { get; set; }

        public string AltUnom { get; set; }

        public string AltAddress { get; set; }

        public string Comment { get; set; }

        public bool ShowUnomUpdateHistory { get; set; }
    }
}
