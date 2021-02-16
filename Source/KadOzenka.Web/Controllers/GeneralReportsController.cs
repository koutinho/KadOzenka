using Microsoft.AspNetCore.Mvc;
using KadOzenka.Web.Attributes;
using ObjectModel.Common;

namespace KadOzenka.Web.Controllers
{
	public class GeneralReportsController : KoBaseController
	{
		private string CustomReportsControllerName => "CustomReports";
		private string PlatformReportsControllerName => "PlatformReports";


		[SRDFunction(Tag = "")]
		public RedirectToActionResult Download(long reportId)
		{
			return CheckIsPlatformReport(reportId)
				? RedirectToAction("DownloadSavedReport", "Report", new { savedReportId = reportId })
				: RedirectToAction(nameof(CustomReportsController.Download), CustomReportsControllerName, new { reportId });
		}

		[HttpGet]
		[SRDFunction(Tag = "")]
		public RedirectToActionResult ReportFileCard(long reportId)
		{
			return CheckIsPlatformReport(reportId)
				? RedirectToAction(nameof(PlatformReportsController.ReportFileCard), PlatformReportsControllerName, new { savedReportId = reportId })
				: RedirectToAction(nameof(CustomReportsController.ReportFileCard), CustomReportsControllerName, new { reportId });
		}


		#region Support Methods

		private bool CheckIsPlatformReport(long reportId)
		{
			var isPlatformReport = OMAllReportsInSystemView.Where(x => x.Id == reportId).Select(x => x.IsPlatformReport)
				.ExecuteFirstOrDefault()?.IsPlatformReport;

			return isPlatformReport.GetValueOrDefault();
		}

		#endregion
	}
}