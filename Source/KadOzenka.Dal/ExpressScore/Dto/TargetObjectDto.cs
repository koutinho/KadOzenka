using System.Collections.Generic;

namespace KadOzenka.Dal.ExpressScore.Dto
{
    public class TargetObjectDto
    {
        public long UnitId { get; set; }
        public List<AttributePure> Attributes { get; set; }

        public TargetObjectDto(long targetObjectId, List<AttributePure> attributes)
        {
            UnitId = targetObjectId;
            Attributes = attributes;
        }
    }

    public class AttributePure
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
