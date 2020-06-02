using System.Collections.Generic;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;

namespace KadOzenka.Dal.FastReports.StatisticalData.KRSummaryResults
{
	public abstract class KRSummaryResultsReport : StatisticalDataReport
	{
		protected readonly GbuObjectService GbuObjectService;
		protected readonly KRSummaryResultsService SummaryResultsService;

		protected KRSummaryResultsReport()
		{
			GbuObjectService = new GbuObjectService();
			SummaryResultsService = new KRSummaryResultsService(GbuObjectService);
		}

		protected void InitialiseGbuAttributesFilterValue(FilterValue typeOfUseAttributeFilterValue)
		{
			if (typeOfUseAttributeFilterValue != null)
			{
				typeOfUseAttributeFilterValue.ReportParameters = new List<ReportParameter>();
				var attributes = GbuObjectService.GetGbuAttributes();
				typeOfUseAttributeFilterValue.ReportParameters.Add(new ReportParameter
					{ Value = string.Empty, Key = string.Empty });
				typeOfUseAttributeFilterValue.ReportParameters.AddRange(attributes.Select(x => new ReportParameter
					{ Value = $"{x.Name} ({x.ParentRegister?.RegisterDescription})", Key = $"key:{x.Id}" })
				);
			}
		}
	}
}
