using System.Linq;
using Core.Register.Enums;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.Controllers;
using KadOzenka.Web.Models.DataImporterLayout;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Common;

namespace KadOzenka.Web.Controllers
{
	public class DataImporterLayoutController : BaseController
	{
		[HttpGet]
		public ActionResult MainData(long importId)
		{
			var import = OMImportFromTemplates
				.Where(x => x.Id == importId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var userName = import != null ? SRDCache.Users[(int)import.UserId]?.FullName : string.Empty;
			var status = import != null ? (RegistersExportStatus)import.Status : (RegistersExportStatus?) null;

			return View(DataImporterLayoutDto.OMMap(import, userName, status));
		}

		[HttpGet]
		public ActionResult Download(long importId)
		{
			return EmptyResponse();
		}

		[HttpGet]
		public ActionResult ImportReStart(long importId)
		{
			return EmptyResponse();
		}
	}
}
