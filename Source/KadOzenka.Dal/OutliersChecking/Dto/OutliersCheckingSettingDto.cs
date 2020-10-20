using ObjectModel.Directory;

namespace KadOzenka.Dal.OutliersChecking.Dto
{
	public class OutliersCheckingSettingDto
	{
		public long Id { get; set; }
		public long Zone { get; set; }
		public Hunteds District { get; set; }
		public Districts Region { get; set; }
		public decimal? MinDeltaCoef { get; set; }
		public decimal? MaxDeltaCoef { get; set; }
	}
}
