using KadOzenka.Dal.LongProcess.Reports;
using Microsoft.AspNetCore.Mvc;
using KadOzenka.Web.Attributes;

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
		    var reportInfo = CustomReportsService.GerReportInfo(reportId);

		    return File(reportInfo.Stream, GetContentTypeByExtension(reportInfo.FileExtension), reportInfo.FullFileName);
	    }
	}
}