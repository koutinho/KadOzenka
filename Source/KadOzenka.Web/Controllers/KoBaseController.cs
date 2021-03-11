using System;
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
using Core.Main.FileStorages;
using System.IO;
using Core.Register;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Web.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadOzenka.Web.Controllers
{
    public class KoBaseController : BaseController
	{
        protected IGbuObjectService GbuObjectService { get; set; }

        public KoBaseController()
        {
	        GbuObjectService = new GbuObjectService();
        }


        [HttpGet]
		[SRDFunction(Tag = "")]
		public IActionResult DownloadExcelFileFromSessionByName(string fileName)
		{
			var fileInfo = GetFileFromSession(fileName, RegistersExportType.Xlsx);
			if (fileInfo == null)
				return new EmptyResult();

			return File(fileInfo.FileContent, fileInfo.ContentType, $"{fileName}, {DateTime.Now}.{fileInfo.FileExtension}");
		}

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
	        if (!string.IsNullOrEmpty(fileExtension) && fileExtension.StartsWith("."))
		        fileExtension = fileExtension.Remove(0, 1);

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
                case "txt":
	                return "text/plain";
                case "csv":
	                return "text/csv";
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

        protected List<DropDownTreeItemModel> GetGbuAttributesTree()
        {
	        return GetGbuAttributesTreeInternal()
                .Select(x => new DropDownTreeItemModel
		        {
			        Value = Guid.NewGuid().ToString(),
			        Text = x.Text,
			        Items = x.Items.Select(y => new DropDownTreeItemModel
			        {
				        Value = y.Value,
				        Text = y.Text
			        }).ToList()
		        }).ToList();
        }

        protected FileSize CalculateFileSize(string storageKey, DateTime fileDateOnServer, string fileName)
        {
	        var fileLocation = FileStorageManager.GetPathForFileFolder(storageKey, fileDateOnServer);
	        fileLocation = Path.Combine(fileLocation, fileName);

            return CalculateFileSize(fileLocation);
        }

        protected FileSize CalculateFileSize(string fileLocation)
        {
	        var fileSize = new FileSize();
	        if (!string.IsNullOrEmpty(fileLocation) && System.IO.File.Exists(fileLocation))
	        {
		        var resultFileSize = new FileInfo(fileLocation).Length;
		        fileSize.Kb = (float)(resultFileSize / 1024m);
	        }
	        return fileSize;
        }


        #region Support Methods

        private List<GbuAttributesTreeDto> GetGbuAttributesTreeInternal()
        {
	        var gbuRegisterIds = GbuObjectService.GetGbuRegistersIds();
	        return RegisterCache.Registers.Values.Where(x => gbuRegisterIds.Contains(x.Id)).Select(x => new GbuAttributesTreeDto
	        {
		        Text = x.Description,
		        Value = x.Id.ToString(),
		        Items = RegisterCache.RegisterAttributes.Values.Where(y => y.RegisterId == x.Id && y.IsDeleted == false)
			        .Select(y => new SelectListItem
			        {
				        Text = y.Name,
				        Value = y.Id.ToString()
			        }).ToList()
	        }).ToList();
        }

        #endregion


        #region Entities

        public class GbuAttributesTreeDto
        {
	        public string Text { get; set; }
	        public string Value { get; set; }

	        public List<SelectListItem> Items { get; set; }
        }

        #endregion
    }
}
