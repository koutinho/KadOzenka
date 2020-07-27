using System;
using System.Collections.Generic;
using System.Linq;
using Core.SRD;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using Core.Register.Enums;
using Microsoft.AspNetCore.Http;
using Core.Shared.Extensions;
using KadOzenka.Web.Models.KoBase;

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
                    Message = x.Value.Errors.Select(e =>
                    {
                        if (e.ErrorMessage == "The value '' is invalid.")
                        {
                            return $"{e.ErrorMessage} Поле {x.Key}";
                        }
                        return e.ErrorMessage;
                    })
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

        protected FileGeneralInfo GetFileFromSession(string fileName, RegistersExportType fileType)
        {
            var fileContent = HttpContext.Session.Get(fileName);
            if (fileContent == null)
                return null;

            HttpContext.Session.Remove(fileName);
            StringExtensions.GetFileExtension(fileType, out var fileExtenstion, out var contentType);

            return new FileGeneralInfo
            {
                FileContent = fileContent,
                FileExtension = fileExtenstion,
                ContentType = contentType
            };
        }

        protected string GetContentTypeByExtension(string fileExtension)
        {
	        switch (fileExtension)
	        {
                case "xlsx":
	                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "xls":
	                return "application/xls";
                case "xml":
	                return "application/xml";
                case "zip":
	                return "application/zip";
                case "rar":
	                return "application/octet-stream";
                case "docx":
	                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                default:
	                throw new Exception($"Неподдерживаемый тип файла: {fileExtension}");
	        }
        }
    }
}
