using ObjectModel.Directory;

namespace KadOzenka.Dal.Groups.Dto
{
	public class GroupAlgorithmInfoDto
	{
		public long Id { get; set; }
		public string Number { get; set; }
		public string Name { get; set; }
		public KoGroupAlgoritm GroupAlgoritm { get; set; }
		public long? ParentId { get; set; }
		public KoGroupAlgoritm? ParentGroupAlgoritm { get; set; }
	}
}
