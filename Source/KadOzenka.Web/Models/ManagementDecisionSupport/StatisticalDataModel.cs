using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Web.Models.ManagementDecisionSupport
{
	public class StatisticalDataModel : IValidatableObject
	{
		/// <summary>
		/// Тур
		/// </summary>
		public long? TourId { get; set; }

		/// <summary>
		/// Список заданий на оценку
		/// </summary>
		public long[] TaskFilter { get; set; }

		/// <summary>
		/// Тип отчета
		/// </summary>
		[Required(ErrorMessage = "Выберете тип отчета")]
		public long? ReportType { get; set; }


        private readonly List<long?> _reportsEnabledWithoutTasks = 
            new List<long?>
            {
	            (long)StatisticalDataType.PricingFactorsCompositionForPreviousTours,
	            (long)StatisticalDataType.AdditionalFormsMarketDataInfo
			};

        private readonly List<long?> _reportsEnabledWithoutTour =
	        new List<long?> { (long)StatisticalDataType.AdditionalFormsMarketDataInfo };


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
	        var errors = new List<ValidationResult>();
            if (!_reportsEnabledWithoutTasks.Contains(ReportType) && (TaskFilter == null || TaskFilter.Length == 0))
            {
                errors.Add(new ValidationResult("Выберете задания на оценку"));
            }

            if (!_reportsEnabledWithoutTour.Contains(ReportType) && !TourId.HasValue)
            {
	            errors.Add(new ValidationResult("Выберете тур"));
            }

			return errors;
        }
    }
}
