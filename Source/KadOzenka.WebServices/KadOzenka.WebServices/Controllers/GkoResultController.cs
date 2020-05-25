﻿using System;
using System.Linq;
using System.Net;
using KadOzenka.WebServices.Domain.Context;
using KadOzenka.WebServices.Domain.Model;
using KadOzenka.WebServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KadOzenka.WebServices
{
	[Route("journal")]
	[ApiController]
	public class GkoResultController : Controller
	{
		private JournalService _journalService;
		public GkoResultController(JournalService journalService)
		{
			_journalService = journalService;
		}

		/// <summary>
		/// Get record without confirm date
		/// </summary>
		/// <response code="200">OK</response>
		[SwaggerResponse(statusCode: 200, type: typeof(ReonJournal), description: "OK")]
		[Route("read")]
		[HttpGet]
		public IActionResult Read()
		{
			var res = _journalService.ReadLastRecord();

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
		[Route("confirm")]
		[HttpPut]
		public IActionResult Confirm(Guid guidRecord)
		{
			var res = _journalService.Confirm(guidRecord);
			if (!res)
			{
				return NotFound();
			}

			return Ok();
		}
	}
}
