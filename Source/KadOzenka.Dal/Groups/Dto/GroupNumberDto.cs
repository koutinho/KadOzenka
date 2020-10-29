namespace KadOzenka.Dal.Groups.Dto
{
	public class GroupNumberDto
	{
		public long? Id { get; set; }
		public string CombinedName { get; set; }
		public int? Number { get; set; }
		public int? ParentNumber { get; set; }
	}
}
