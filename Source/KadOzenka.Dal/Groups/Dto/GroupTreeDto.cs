using System.Collections.Generic;
using KadOzenka.Dal.Groups.Dto.Consts;

namespace KadOzenka.Dal.Groups.Dto
{
	public class GroupTreeDto
	{
		public long? Id { get; set; }
		public long? ParentId { get; set; }
		public string GroupName { get; set; }
        public string CombinedNumber { get; set; }
        public int? Number { get; set; }
        public long? TourId { get; set; }
        public bool CheckModelFactorsValues { get; set; }
        public GroupType GroupType { get; set; }
        public List<GroupTreeDto> Items { get; set; }
	}
}
