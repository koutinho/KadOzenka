using System;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.GbuObject;
using Microsoft.AspNetCore.Mvc;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.GeneralReports;
using KadOzenka.Web.Models.UnloadResults;

namespace KadOzenka.Web.Controllers
{
	public class UnloadResultsController : KoBaseController
	{
		public UnloadResultsController()
		{
		}


		[SRDFunction(Tag = "")]
		public FileResult DownloadResult(long resultFileId)
		{
			var fileName = UnloadResultStorageManager.GetUnloadResultFileNameOrDefault(resultFileId);
			var content = UnloadResultStorageManager.GetUnloadResultFileById(resultFileId);
			var contentType = UnloadResultStorageManager.GetUnloadResultContentType(resultFileId);
			return File(content, contentType, fileName);
		}

		[HttpGet]
		[SRDFunction(Tag = "")]
		public IActionResult UnloadResultsModal(long? resultFileId)
		{
			var model = new UnloadResultsDownloadModel();
			if (!resultFileId.HasValue)
			{
				model.FileExists = false;
			}
			else
			{
				var id = resultFileId.Value;
				var exists = UnloadResultStorageManager.CheckIfFileExists(id);
				var fileName = UnloadResultStorageManager.GetUnloadResultFileNameOrDefault(id);
				model = new UnloadResultsDownloadModel {FileExists = exists, FileName = fileName, Id = id};
			}

			return View(model);
		}
	}
}