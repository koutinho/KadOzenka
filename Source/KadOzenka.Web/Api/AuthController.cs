using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Platform.Authorization;
using Platform.Web.Models.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace KadOzenka.Web.Api
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		[SwaggerResponse(statusCode: 200, type: typeof(AuthRequestDto), description: "OK")]
		[SwaggerResponse(statusCode: (int)HttpStatusCode.BadRequest, description: "Авторизация не удалась")]
		[Route("login")]
		[HttpPost]
		public async Task<ActionResult> Login(LoginDto model)
		{
			return Ok(new AuthRequestDto());
		}

		[HttpGet]
		public string Test()
		{
			return "123";
		}

	}
}
