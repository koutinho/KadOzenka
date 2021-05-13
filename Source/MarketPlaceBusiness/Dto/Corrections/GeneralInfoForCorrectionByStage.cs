using ObjectModel.Directory;

namespace MarketPlaceBusiness.Dto.Corrections
{
	public class GeneralInfoForCorrectionByStage
	{
		public string CadastralNumber { get; set; }
		public MarketSegment Segment { get; set; }
		public decimal? Price { get; set; }
	}
}
