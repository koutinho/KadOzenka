using System.Collections.Generic;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Tours.Dto
{
    public class AttributeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<KoAttributeUsingType> UsingTypes { get; set; } = new List<KoAttributeUsingType>();
    }
}
