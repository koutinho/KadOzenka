namespace KadOzenka.Dal.Correction
{
	public class CorrectionBaseService
	{
		public CorrectionSettingsService CorrectionSettingsService { get; protected set; }


		public CorrectionBaseService()
		{
			CorrectionSettingsService = new CorrectionSettingsService();
		}
	}
}
