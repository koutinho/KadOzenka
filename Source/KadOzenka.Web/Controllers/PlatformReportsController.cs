using System;
using Core.Register.Enums;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.Controllers;
using KadOzenka.Dal.LongProcess.Reports;
using Microsoft.AspNetCore.Mvc;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.GeneralReports;
using ObjectModel.Common;
using ObjectModel.Core.Reports;
using Platform.Reports;

namespace KadOzenka.Web.Controllers
{
	public class PlatformReportsController : KoBaseController
	{
		[HttpGet]
		[SRDFunction(Tag = "")]
		public IActionResult ReportFileCard(long savedReportId)
		{
			var platformReport = OMSavedReport.Where(x => x.Id == savedReportId).Select(x => new
			{
				x.UserId,
				x.CreateDate,
				x.EndDate,
				x.Title,
				x.Status,
				x.FileType
			}).ExecuteFirstOrDefault();
			if (platformReport == null)
				throw new Exception($"Не найден платформенный отчет с ИД '{savedReportId}'");

			var fileLocation = ReportStorage.GetFileLocation(platformReport.Id, platformReport.FileType);
			if (!System.IO.File.Exists(fileLocation))
			{
				fileLocation = fileLocation.Replace(".pdf", ".zip");
			}

			var model = new ReportFileGeneralInfoModel
			{
				User = SRDCache.Users[(int)platformReport.UserId].FullName,
				CreationDate = platformReport.CreateDate,
				FinishDate = platformReport.EndDate,
				FileName = platformReport.Title,
				Status = ((RegistersExportStatus)platformReport.Status.GetValueOrDefault()).GetEnumDescription(),
				FileSize = CalculateFileSize(fileLocation)
			};

			return View("~/Views/GeneralReports/ReportFileCard.cshtml", model);
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