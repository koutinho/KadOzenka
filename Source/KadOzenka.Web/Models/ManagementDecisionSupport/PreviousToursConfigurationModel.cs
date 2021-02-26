using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.LongProcess.InputParameters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadOzenka.Web.Models.ManagementDecisionSupport
{
    public class PreviousToursConfigurationModel : IValidatableObject
    {
	    public List<SelectListItem> AvailableTours { get; set; }

	    [Display(Name = "Задания на оценку")]
        public long[] SelectedTasks{ get; set; }

        [Display(Name = "Группа")]
        public long? GroupId { get; set; }

        public PreviousToursConfigurationModel()
        {
	        AvailableTours = new List<SelectListItem>();
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
	        if (SelectedTasks == null || SelectedTasks.Length == 0)
		        yield return new ValidationResult("Не выбраны задания на оценку");

	        if (GroupId.GetValueOrDefault() == 0)
		        yield return new ValidationResult("Не выбрана группа");
        }

        public PreviousToursReportInputParameters MapToInputParameters()
        {
            return new PreviousToursReportInputParameters
            {
	            GroupId = GroupId.GetValueOrDefault(),
	            TaskIds = SelectedTasks.ToList()
            };
        }
    }
}
