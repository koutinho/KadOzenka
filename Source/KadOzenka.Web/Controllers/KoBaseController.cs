using System.Linq;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace KadOzenka.Web.Controllers
{
    public class KoBaseController : BaseController
	{
        protected JsonResult GenerateMessageNonValidModel()
        {
            return Json(new
            {
                Errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new
                {
                    Control = x.Key,
                    Message = string.Join("\n", x.Value.Errors.Select(e =>
                    {
                        if (e.ErrorMessage == "The value '' is invalid.")
                        {
                            return $"{e.ErrorMessage} Поле {x.Key}";
                        }

                        return e.ErrorMessage;
                    }))
                })
            });
        }
    }
}
