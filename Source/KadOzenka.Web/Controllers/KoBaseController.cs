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
using Core.Register.RegisterEntities;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Web.Attributes;

namespace KadOzenka.Web.Controllers
{
    public class KoBaseController : BaseController
	{
        protected IGbuObjectService GbuObjectService { get; set; }
        public IRegisterCacheWrapper RegisterCacheWrapper { get; set; }

		public KoBaseController(IGbuObjectService gbuObjectService, IRegisterCacheWrapper registerCacheWrapper)
        {
	        GbuObjectService = gbuObjectService;
	        RegisterCacheWrapper = registerCacheWrapper;
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

        protected List<DropDownTreeItemModel> GetGbuAttributesTree(List<RegisterAttributeType> availableTypes = null)
        {
	        var gbuRegisterIds = GbuObjectService.GetGbuRegistersIds();

			return RegisterCacheWrapper.GetRegistersCache().Values.Where(x => gbuRegisterIds.Contains(x.Id))
		        .Select(register =>
		        {
			        Func<RegisterAttribute, bool> attributeSearchExpression;
			        if (availableTypes == null)
			        {
				        attributeSearchExpression = attribute =>
					        attribute.RegisterId == register.Id && attribute.IsDeleted == false;
			        }
			        else
			        {
				        attributeSearchExpression = attribute =>
					        attribute.RegisterId == register.Id && attribute.IsDeleted == false &&
					        availableTypes.Contains(attribute.Type);
			        }

			        return new DropDownTreeItemModel
			        {
				        Value = Guid.NewGuid().ToString(),
				        Text = register.Description,
				        Items = RegisterCacheWrapper.GetRegisterAttributesCache().Values
					        .Where(attributeSearchExpression)
					        .Select(attribute => new DropDownTreeItemModel
					        {
						        Text = attribute.Name,
						        Value = attribute.Id.ToString()
					        }).ToList()
			        };
		        }).Where(x => x.Items.Count > 0).ToList();
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
	}
}
