using System.Collections.Generic;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities
{
    public class PreviousToursReportInfo
    {
        public string Title { get; set; }
        public List<string> ColumnTitles { get; set; }
        public List<PreviousTourReportItem> Items { get; set; }
        public List<OMTour> Tours { get; set; }
        public List<FactorsService.PricingFactor> PricingFactors { get; set; }

        public PreviousToursReportInfo()
        {
            Items = new List<PreviousTourReportItem>();
            Tours = new List<OMTour>();
            PricingFactors = new List<FactorsService.PricingFactor>();
        }
    }
}
