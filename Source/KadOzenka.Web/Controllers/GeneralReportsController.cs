using System;
using CommonSdks;
using CommonSdks.PlatformWrappers;
using Core.Register.Enums;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.GbuObject;
using Microsoft.AspNetCore.Mvc;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.GeneralReports;
using ObjectModel.Core.Reports;
using Platform.Reports;

namespace KadOzenka.Web.Controllers
{
	public class GeneralReportsController : KoBaseController
	{
		public GeneralReportsController(IGbuObjectService gbuObjectService, IRegisterCacheWrapper registerCacheWrapper)
			: base(gbuObjectService, registerCacheWrapper)
		{

		}


		[HttpGet]
		[SRDFunction(Tag = "")]
		public IActionResult ReportFileCard(long reportId)
		{
			var platformReport = OMSavedReport.Where(x => x.Id == reportId).Select(x => new
			{
				x.UserId,
				x.CreateDate,
				x.EndDate,
				x.Title,
				x.Status,
				x.FileType
			}).ExecuteFirstOrDefault();
			if (platformReport == null)
				throw new Exception($"Не найден платформенный отчет с ИД '{reportId}'");

			var fileLocation = ReportStorage.GetFileLocation(platformReport.Id, platformReport.FileType);
			if (!System.IO.File.Exists(fileLocation))
			{
				fileLocation = fileLocation.Replace(".pdf", ".zip");
			}

			var model = new FileGeneralInfoModel
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
	}
}