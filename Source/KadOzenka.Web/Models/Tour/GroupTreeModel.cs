namespace KadOzenka.Web.Models.Tour
{
	public class GroupTreeModel
	{
		public long? Id { get; set; }
		public long? ParentId { get; set; }
		public string GroupName { get; set; }
		public long? TourId { get; set; }
	}
}
