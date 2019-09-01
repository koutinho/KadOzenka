using System.Collections.Generic;

namespace CIPJS.DAL.Fsp
{
    public class ManyFspFlatLinkDto
    {
        public IReadOnlyList<long> FspIds { get; set; }

        public long Unom { get; set; }

        public string Comment { get; set; }
    }
}
