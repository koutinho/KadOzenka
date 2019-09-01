using System.Collections.Generic;

namespace CIPJS.DAL.Fsp
{
    public class SingleFspFlatLinkDto
    {
        public long FspId { get; set; }

        public IReadOnlyList<long> Flats { get; set; }
    }
}
