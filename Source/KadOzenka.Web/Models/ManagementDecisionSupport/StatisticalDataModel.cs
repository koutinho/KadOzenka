using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.ManagementDecisionSupport
{
	public class StatisticalDataModel
	{
		/// <summary>
		/// Тур
		/// </summary>
		[Required(ErrorMessage = "Выберете тур")]
		public long? TourId { get; set; }

		/// <summary>
		/// Список заданий на оценку
		/// </summary>
		[Required(ErrorMessage = "Выберете задания на оценку")]
		public long[] TaskFilter { get; set; }

		/// <summary>
		/// Тип отчета
		/// </summary>
		[Required(ErrorMessage = "Выберете тип отчета")]
		public long? ReportType { get; set; }
	}
}
