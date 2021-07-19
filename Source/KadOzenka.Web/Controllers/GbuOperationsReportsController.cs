using CommonSdks;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.GbuObject;
using Microsoft.AspNetCore.Mvc;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.GeneralReports;

namespace KadOzenka.Web.Controllers
{
	public class GbuOperationsReportsController : KoBaseController
	{
		public IGbuReportService GbuReportService { get; set; }

		public GbuOperationsReportsController(IGbuReportService gbuReportService, IRegisterCacheWrapper registerCacheWrapper,
			IGbuObjectService gbuObjectService)
			: base(gbuObjectService, registerCacheWrapper)
		{
			GbuReportService = gbuReportService;
		}


		[SRDFunction(Tag = "")]
		public FileResult Download(long reportId)
		{
			var reportInfo = GbuReportService.GetFile(reportId);

			return File(reportInfo.Stream, GetContentTypeByExtension(GbuReportService.DefaultExtension), reportInfo.FullFileName);
		}

		[HttpGet]
		[SRDFunction(Tag = "")]
		public IActionResult ReportFileCard(long reportId)
		{
			var report = GbuReportService.GetFileInfo(reportId);

			var model = new FileGeneralInfoModel
			{
				User = SRDCache.Users[(int)report.UserId].FullName,
				CreationDate = report.CreationDate,
				FinishDate = report.FinishDate,
				FileName = report.FileName,
				Status = report.Status_Code.GetEnumDescription(),
				FileSize = CalculateFileSize(GbuReportService.FileStorageKey, report.DateOnServer, report.FileNameOnServer)
			};

			return View("~/Views/GeneralReports/ReportFileCard.cshtml", model);
		}
	}
}