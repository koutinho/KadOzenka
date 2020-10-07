using System.ComponentModel.DataAnnotations;
using System.Net;
using KadOzenka.WebServices.Exceptions;
using KadOzenka.WebServices.Services.ModelDto;
using KadOzenka.WebServices.Services.Rsm;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KadOzenka.WebServices.Controllers
{
	/// <summary>
	/// Controller for get records from journal
	/// </summary>
	[Route("rsm")]
	[ApiController]
	public class RSMController : Controller
	{
		private RsmService _rsmService;
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="rsmService"></param>
		public RSMController(RsmService rsmService)
		{
			_rsmService = rsmService;
		}

		/// <summary>
		/// Returns result of evaluation
		/// </summary>
		/// <remarks>
		/// Retrieves result of evaluation by Cadastral Number and Tour Year.
		/// </remarks>
		/// <returns>Result of evaluation</returns>
		[ApiExceptionFilter]
		[SwaggerResponse((int)HttpStatusCode.OK, type: typeof(RsmEvaluationResultMessage), description: "OK")]
		[SwaggerResponse((int)HttpStatusCode.NotFound)]
		[Route("evaluation_results")]
		[HttpGet]
		//Для тестирования
		//public IActionResult GetEvaluationResults(string cadastralNumber = "77:07:0005007:13763", ushort tourYear = 2018)
		public IActionResult GetEvaluationResults([Required] string cadastralNumber, [Required] ushort tourYear)
		{
			if (string.IsNullOrWhiteSpace(cadastralNumber))
				throw new BadRequestException("Не передан Кадастовый номер");
			if (tourYear == 0)
				throw new BadRequestException("Не передан год оценки");

			var result = _rsmService.GetEvaluationResults(cadastralNumber, tourYear);
			if (result == null)
				return NotFound();

			var message = new RsmEvaluationResultMessage
			{
				GroupNumber = result.GroupNumber,
				SubGroupNumber = result.SubGroupNumber,
				Upks = result.Upks,
				CadastralCost = result.CadastralCost
			};

			return Ok(message);
		}
	}
}
