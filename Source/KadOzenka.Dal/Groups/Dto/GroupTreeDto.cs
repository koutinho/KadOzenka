using KadOzenka.Dal.Groups.Dto.Consts;

namespace KadOzenka.Dal.Groups.Dto
{
	public class GroupTreeDto
	{
		public long? Id { get; set; }
		public long? ParentId { get; set; }
		public string GroupName { get; set; }
		public long? TourId { get; set; }
        public GroupType GroupType { get; set; }
	}
}
