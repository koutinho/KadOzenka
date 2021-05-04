using MarketPlaceBusiness;

namespace KadOzenka.Dal.Correction
{
	public class CorrectionBaseService
	{
		public IMarketObjectsForCorrectionsService MarketObjectsService { get; }
		public CorrectionSettingsService CorrectionSettingsService { get; protected set; }


		public CorrectionBaseService()
		{
			CorrectionSettingsService = new CorrectionSettingsService();
			MarketObjectsService = new MarketObjectsForCorrectionsService();
		}
	}
}
