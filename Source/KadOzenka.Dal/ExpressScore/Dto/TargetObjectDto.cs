using System.Collections.Generic;

namespace KadOzenka.Dal.ExpressScore.Dto
{
    public class TargetObjectDto
    {
        public long TargetObjectId { get; set; }
        public List<AttributePure> Attributes { get; set; }

        public TargetObjectDto(long targetObjectId, List<AttributePure> attributes)
        {
            TargetObjectId = targetObjectId;
            Attributes = attributes;
        }
    }

    public class AttributePure
    {
        public int? AttributeId { get; set; }
        public string AttributeValue { get; set; }
    }
}
