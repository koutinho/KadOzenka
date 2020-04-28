using System.Collections.Generic;
using KadOzenka.Dal.ExpressScore.Dto;

namespace KadOzenka.Web.Models.ExpressScore
{
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
		public CostFactorsDto CostFactors { get; set; }
	}
}