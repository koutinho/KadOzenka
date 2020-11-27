using System.Collections.Generic;

namespace KadOzenka.Dal.Groups.Dto
{
	public class TourGroupsInfo
	{
		public List<GroupTreeDto> OksGroups { get; set; }
		public List<GroupTreeDto> OksSubGroups { get; set; }
		public List<GroupTreeDto> ZuGroups { get; set; }
		public List<GroupTreeDto> ZuSubGroups { get; set; }
	}
}
