﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using Core.Register.Enums;
using Microsoft.AspNetCore.Http;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Models.KoBase;
using Kendo.Mvc.UI;
using ObjectModel.Core.TD;

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
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

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

        protected List<PartialDocument> GetDocumentsForPartialView()
        {
            return OMInstance.Where(x => x)
	            .Select(x => new
	            {
		            x.RegNumber,
		            x.Description
	            })
	            .OrderBy(x => x.RegNumber)
                .Execute()
                .Select(x => new PartialDocument
                {
                    Text = $"{x.RegNumber} {x.Description}",
                    Value = x.Id
                }).ToList();
        }

        protected IEnumerable<DropDownTreeItemModel> GetGbuAttributesTree()
        {
	        return new GbuObjectService().GetGbuAttributesTree()
		        .Select(x => new DropDownTreeItemModel
		        {
			        Value = Guid.NewGuid().ToString(),
			        Text = x.Text,
			        Items = x.Items.Select(y => new DropDownTreeItemModel
			        {
				        Value = y.Value,
				        Text = y.Text
			        }).ToList()
		        }).AsEnumerable();
        }
    }
}
