using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using SpdIntegration;


namespace RsmCloudService.Web.Controllers
{
    public class SpdController : BaseController
	{
		public IActionResult OpenSpdRequest(long appId)
		{
			if (appId > 0)
			{
				return Redirect(SpdSettings.Current.SpdRequestUrl + appId);
			}
			return Ok("Отсутствует связанная заявка в СПД");
		}
	}
}
