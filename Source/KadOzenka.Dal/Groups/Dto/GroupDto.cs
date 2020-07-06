using KadOzenka.Dal.Groups.Dto.Consts;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Groups.Dto
{
    public class GroupDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public long? GroupingAlgorithmId { get; set; }
        public KoGroupAlgoritm GroupAlgorithmCode { get; set; }
        public long? ParentGroupId { get; set; }
        public long? RatingTourId { get; set; }
        public GroupType GroupType { get; set; }
    }
}
