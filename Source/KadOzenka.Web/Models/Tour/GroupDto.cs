namespace KadOzenka.Web.Models.Tour
{
	public class GroupDto
	{
		public long? Id { get; set; }
		public long? RatingTourId { get; set; }

		public string ObjType { get; set; }
		public long? ParentGroupId { get; set; }

		public long? GroupingMechanismId { get; set; }
		public string Name { get; set; }
	}
}
