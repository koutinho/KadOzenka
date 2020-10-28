namespace KadOzenka.Dal.Groups.Dto
{
	public class GroupFactorDto
	{
		public long Id { get; set; }
		public long GroupId { get; set; }
		public long FactorId { get; set; }
		public string FactorName { get; set; }
		public bool SignMarket { get; set; }
	}
}
