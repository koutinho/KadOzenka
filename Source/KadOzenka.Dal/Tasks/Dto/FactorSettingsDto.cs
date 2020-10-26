using ObjectModel.Directory.KO;

namespace KadOzenka.Dal.Tasks.Dto
{
	public class FactorSettingsDto
	{
		public long Id { get; set; }
		public string FactorName { get; set; }
		public FactorInheritance FactorInheritance { get; set; }
		public string Source { get; set; }
		public string CorrectFactorName { get; set; }
	}
}
