using System.Collections.Generic;

namespace KadOzenka.Web.Models.ExpressScore
{
	public class CostFactor
	{
		public string Name { get; set; }
		public decimal? Coefficient { get; set; }
		public int AttributeId { get; set; }
		public int DictionaryId { get; set; }

	}
	public class SettingsExpressScoreViewModel
	{
		public int TourId { get; set; }

		/// <summary>
		/// ид реестра в которм хранятся данные для оценки
		/// </summary>
		public int FactorRegisterId { get; set; }

		/// <summary>
		/// Список факторов для экспресс оценки
		/// </summary>
		public List<CostFactor> CostFactors { get; set; }
	}
}