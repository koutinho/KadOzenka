using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadOzenka.Web.Models.ManagementDecisionSupport
{
    public class PreviousToursConfigurationModel : IValidatableObject
    {
        public bool IsInBackground { get; set; }
        public List<SelectListItem> AvailableTours { get; set; }
        public long[] SelectedTasks{ get; set; }
        public long? GroupId { get; set; }

        public PreviousToursConfigurationModel()
        {
            IsInBackground = true;
            AvailableTours = new List<SelectListItem>();
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (IsInBackground)
                ValidateForBackground(errors);
            else
                ValidateForRealTime(errors);

            return errors;
        }

        public StatisticalDataModel Map()
        {
            return new StatisticalDataModel
            {
                ReportType = (long)StatisticalDataType.PricingFactorsCompositionForPreviousTours,
                TaskFilter = SelectedTasks
            };
        }


        #region Support Methods

        private void ValidateForBackground(List<ValidationResult> errors)
        {
            ValidateSelectedTasks(errors);

            if (GroupId == null || GroupId == 0)
                errors.Add(new ValidationResult("Не выбрана группа."));
        }

        private void ValidateForRealTime(List<ValidationResult> errors)
        {
            ValidateSelectedTasks(errors);
        }

        private void ValidateSelectedTasks(List<ValidationResult> errors)
        {
            if (SelectedTasks == null || SelectedTasks.Length == 0)
                errors.Add(new ValidationResult("Не выбраны задания на оценку."));
        }

        #endregion
    }
}
