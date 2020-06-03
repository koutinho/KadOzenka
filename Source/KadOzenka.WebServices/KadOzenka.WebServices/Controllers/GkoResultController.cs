using System;
using System.Net;
using KadOzenka.WebServices.Services;
using KadOzenka.WebServices.Services.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KadOzenka.WebServices
{
	/// <summary>
	/// Controller for get records from journal
	/// </summary>
	[Route("journal")]
	[ApiController]
	public class GkoResultController : Controller
	{
		private JournalService _journalService;
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="journalService"></param>
		public GkoResultController(JournalService journalService)
		{
			_journalService = journalService;
		}

		/// <summary>
		/// Get record without confirm date
		/// </summary>
		/// <response code="200">OK</response>
		/// <response code="404">Очередь сообщений пуста</response>
		[SwaggerResponse(statusCode: 200, type: typeof(KoResultMessage), description: "OK")]
		[SwaggerResponse(statusCode: (int)HttpStatusCode.NotFound, description: "Очередь сообщений пуста")]
		[Route("read")]
		[HttpGet]
		public IActionResult Read()
		{
			var res = _journalService.ReadLastRecord();

			if (res == null) return NotFound();

			return Ok(res);
		}

		/// <summary>
		/// Confirm record by guid
		/// </summary>
		/// <param name="guidRecord">Guid record</param>
		/// <response code="200">OK</response>
		/// <response code="404">NotFound</response>
		[SwaggerResponse(statusCode: 200, type: typeof(OkResult), description: "OK")]
		[SwaggerResponse(statusCode: (int)HttpStatusCode.NotFound, type: typeof(OkResult), description: "OK")]
		[HttpPost("confirm")]
		public IActionResult Confirm([FromBody]ConfirmMessage guidRecord)
		{
			var res = _journalService.Confirm(guidRecord.Guid);
			if (!res)
			{
				return NotFound();
			}

			return Ok();
		}

		/// <summary>
		/// Get Report by guid
		/// </summary>
		/// <param name="guidRecord">Guid record</param>
		[HttpGet("DownloadReport/{guidRecord}")]
		public FileResult DownloadReport(Guid guidRecord)
		{
			var res = _journalService.GetFileReport(guidRecord);
			if (res == null)
			{
				throw new Exception("Не удалось выгрузить файл");
			}

			return File(res.Stream, res.ContentType, res.FileName);
		}
	}
}
