namespace KadOzenka.Dal.Tours.Dto
{
	public class GroupTreeDto
	{
		public long? Id { get; set; }
		public long? ParentId { get; set; }
		public string GroupName { get; set; }
		public long? TourId { get; set; }
	}
}
