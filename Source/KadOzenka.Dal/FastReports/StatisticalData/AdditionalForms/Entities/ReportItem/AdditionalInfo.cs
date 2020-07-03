using KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.DataInfo;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.ReportItem
{
	public class AdditionalInfo
	{
		public decimal? MinUpksByCadastralQuartal { get; set; }
		public decimal? AverageUpksByCadastralQuartal { get; set; }
		public decimal? MaxUpksByCadastralQuartal { get; set; }
		public decimal? MinUpksByZone { get; set; }
		public decimal? AverageUpksByZone { get; set; }
		public decimal? MaxUpksByZone { get; set; }

		public void FillCadastralQuartalInfo(CalcInfo calcInfo)
		{
			MinUpksByCadastralQuartal = calcInfo.MinUpks;
			MaxUpksByCadastralQuartal = calcInfo.MaxUpks;
			AverageUpksByCadastralQuartal = calcInfo.AverageUpks;
		}

		public void FillZoneInfo(CalcInfo calcInfo)
		{
			MinUpksByZone = calcInfo.MinUpks;
			MaxUpksByZone = calcInfo.MaxUpks;
			AverageUpksByZone = calcInfo.AverageUpks;
		}
	}
}