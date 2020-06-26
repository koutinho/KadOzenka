using System.Collections.Generic;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadOzenka.Web.Models.ManagementDecisionSupport
{
    public class PreviousToursConfigurationModel
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

        public StatisticalDataModel Map()
        {
            return new StatisticalDataModel
            {
                ReportType = (long)StatisticalDataType.PricingFactorsCompositionForPreviousTours,
                TaskFilter = SelectedTasks
            };
        }
    }
}
