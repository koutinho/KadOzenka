using System.Collections.Generic;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadOzenka.Web.Models.ManagementDecisionSupport
{
    public class PreviousToursConfigurationModel
    {
        public List<SelectListItem> AvailableTours { get; set; }
        public long[] SelectedTasks{ get; set; }

        public PreviousToursConfigurationModel()
        {
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
