using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Web.Controllers;

namespace KadOzenka.Web.Models.ManagementDecisionSupport
{
	public class StatisticalDataModel : IValidatableObject
	{
		public bool IsForBackground => ReportType != null && StatisticalDataService.ReportsViaLongLongProcess.ContainsKey(ReportType);
		public bool IsWithAdditionalConfiguration => ReportType != null && ReportsWithAdditionalConfiguration.ContainsKey(ReportType);

		public long? TourId { get; set; }

		public long? SecondTourId { get; set; }

		public long[] TaskFilter { get; set; }

		[Required(ErrorMessage = "Выберете тип отчета")]
		public long? ReportType { get; set; }

		public DateTime? PricingFactorsCompositionFinalUniformReportActualizationDate { get; set; }
		public DateTime? PricingFactorsCompositionFinalNonuniformActualizationDate { get; set; }


		public readonly Dictionary<long?, string> ReportsWithAdditionalConfiguration =
			new Dictionary<long?, string>
			{
				{(long) StatisticalDataType.PricingFactorsCompositionForPreviousTours, nameof(ManagementDecisionSupportController.PreviousToursReportConfiguration)},
				{(long) StatisticalDataType.CadastralCostDeterminationResults, nameof(ManagementDecisionSupportController.CadastralCostDeterminationResultsConfiguration)},
				{(long) StatisticalDataType.ResultsByKRForParcels, nameof(ManagementDecisionSupportController.ResultsByCadastralDistrictForZuConfiguration)},
				{(long) StatisticalDataType.ResultsByKRForBuildings, nameof(ManagementDecisionSupportController.ResultsByCadastralDistrictForBuildingsConfiguration)},
				{(long) StatisticalDataType.ResultsByKRForConstructions, nameof(ManagementDecisionSupportController.ResultsByCadastralDistrictForConstructionsConfiguration)},
				{(long) StatisticalDataType.ResultsByKRForUncompletedBuildings, nameof(ManagementDecisionSupportController.ResultsByCadastralDistrictForUncompletedBuildingsConfiguration)},
				{(long) StatisticalDataType.ResultsByKRForPlacements, nameof(ManagementDecisionSupportController.ResultsByCadastralDistrictForPlacementsConfiguration)}
			};

		private readonly List<long?> _reportsEnabledWithoutTasks = 
            new List<long?>
            {
	            (long)StatisticalDataType.PricingFactorsCompositionForPreviousTours,
	            (long)StatisticalDataType.AdditionalFormsMarketDataInfo,
	            (long)StatisticalDataType.OnTheNumberOfObjectsByZoneAndSubgroupsOks,
	            (long)StatisticalDataType.OnTheNumberOfObjectsByZoneAndSubgroupsZu,
			};

        private readonly List<long?> _reportsEnabledWithoutTour =
	        new List<long?> { (long)StatisticalDataType.AdditionalFormsMarketDataInfo };

        private readonly List<long?> _reportsRequiredSecondTour =
	        new List<long?> { 
		        (long)StatisticalDataType.OnTheNumberOfObjectsByZoneAndSubgroupsOks, 
		        (long)StatisticalDataType.OnTheNumberOfObjectsByZoneAndSubgroupsZu, };


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

            if (_reportsRequiredSecondTour.Contains(ReportType) && !SecondTourId.HasValue)
            {
	            errors.Add(new ValidationResult("Выберете тур 2"));
            }

			return errors;
        }
    }
}
