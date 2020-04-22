using ObjectModel.Directory;

namespace KadOzenka.Dal.Groups.Dto
{
    public class GroupDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? GroupingAlgorithmId { get; set; }
        public KoGroupAlgoritm GroupAlgorithmCode { get; set; }
        public long? ParentGroupId { get; set; }
        public long? RatingTourId { get; set; }
    }
}
