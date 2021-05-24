using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Shared.Misc;
using Core.SRD;
using Core.SRD.DAL;
using KadOzenka.Web.Api.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Platform.Web.Models.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace KadOzenka.Web.Api
{
	[Route("api/[Controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{

		[AllowAnonymous]
		[SwaggerResponse(statusCode: 200, description: "OK")]
		[SwaggerResponse(statusCode: (int)HttpStatusCode.BadRequest, type: typeof(AuthResultDto), description: "Авторизация не удалась")]
		[Route("login")]
		[HttpPost]
		public async Task<ActionResult> Login(LoginDto model)
		{
			if (ModelState.IsValid)
			{
				if (SRDSession.ValidateUser(model.UserName, model.Password, out string validatedUserName, out string errorMessage)) {
					if (SRDService.CheckNeedChangePassword(model.UserName) && SRDCache.TryGetUserBaseData(model.UserName, out SRDUserBase userBase))
					{
						return BadRequest($"Истекло время действия пароля для пользователя: {userBase.Name}");
					}
				}

				var claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, validatedUserName)
				};
				ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
				await HttpContextHelper.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
			}
			else
			{
				return BadRequest("Не верно заполнен логин или пароль");
			}

			return Ok();
		}

		[AllowAnonymous]
		[SwaggerResponse(401,  "Not Authorize")]
		[Route("NotAuthorize")]
		[HttpGet]
		public IActionResult NotAuthorize()
		{
			return Unauthorized();
		}

	}
}
