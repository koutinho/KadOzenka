using KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.DataInfo;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.ReportItem
{
	public class ReportGko2018
	{
		public string EvaluationSubgroup2018 { get; set; }
		public decimal? Upks2018 { get; set; }
		public decimal? CadastralCost2018 { get; set; }
		public string CadastralQuartal2018 { get; set; }

		public void FillData(Gko2018Info gko2018Info)
		{
			EvaluationSubgroup2018 = gko2018Info.EvaluationSubgroup2018;
			Upks2018 = gko2018Info.Upks2018;
			CadastralCost2018 = gko2018Info.CadastralCost2018;
			CadastralQuartal2018 = gko2018Info.CadastralQuartal2018;
		}
	}
}