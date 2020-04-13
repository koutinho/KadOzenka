using System;
using System.Collections.Generic;
using System.Linq;
using Core.SRD;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Common;
using ObjectModel.Directory.Common;

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

        protected JsonResult SaveTemplate(string nameTemplate, DataFormStorege formType, string serializeData)
        {
	        if (string.IsNullOrEmpty(nameTemplate))
	        {
		        return Json(new { Error = "Сохранение не выполнено. Имя шаблона обязательное поле" });
	        }

	        try
	        {
		        new OMDataFormStorage()
		        {
			        UserId = SRDSession.GetCurrentUserId().Value,
			        FormType_Code = formType,
			        Data = serializeData,
			        TemplateName = nameTemplate,

		        }.Save();
	        }
	        catch (Exception e)
	        {
		        return Json(new { Error = $"Сохранение не выполнено. Подробности в журнале ошибок. Ошибка: {e.Message}" });
	        }

	        return Json(new { success = true });
        }

        protected JsonResult SendErrorMessage(string errorMessage)
        {
	        return Json(new
	        {
				Errors = new List<object>
				{
					new {
						Control = 0,
						Message = errorMessage
					}
				}
	        });

        }

	}
}
