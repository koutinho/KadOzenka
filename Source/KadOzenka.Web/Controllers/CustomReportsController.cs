using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.LongProcess.Reports;
using Microsoft.AspNetCore.Mvc;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.CustomReports;

namespace KadOzenka.Web.Controllers
{
    public class CustomReportsController : KoBaseController
    {
	    public CustomReportsService CustomReportsService { get; set; }

	    public CustomReportsController(CustomReportsService customReportsService)
	    {
		    CustomReportsService = customReportsService;
	    }


	    [SRDFunction(Tag = "")]
	    public FileResult Download(long reportId)
	    {
		    var reportInfo = CustomReportsService.GetFile(reportId);

		    return File(reportInfo.Stream, GetContentTypeByExtension(reportInfo.FileExtension), reportInfo.FullFileName);
	    }


		[HttpGet]
		[SRDFunction(Tag = "")]
		public IActionResult ReportFileCard(long reportId)
		{
			var report = CustomReportsService.GetFileInfo(reportId);

			var model = new ReportFileGeneralInfoModel
			{
				User = SRDCache.Users[(int) report.UserId].FullName,
				CreationDate = report.CreationDate,
				FinishDate = report.FinishDate,
				FileName = report.FileName,
				Status = report.Status_Code.GetEnumDescription(),
				FileSize = CalculateFileSize(CustomReportsService.FileStorageKey, report.DateOnServer, report.FileNameOnServer)
			};

			return View(model);
		}
    }
}