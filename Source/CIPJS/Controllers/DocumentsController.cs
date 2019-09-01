using CIPJS.DAL.Documents;
using CIPJS.Models.Documents;
using Core.ErrorManagment;
using Core.UI.Registers.Controllers;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Directory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.Controllers
{
    public class DocumentsController : BaseController
    {
        private DocumentsService _docService;

        public DocumentsController()
        {
            _docService = new DocumentsService();
        }

        [HttpGet]
        public IActionResult Documents(long? id, long reestrId, ContractType? contractType = null)
        {
            return View(new DocumentObjDto { ObjId = id, ReestrId = reestrId, ContractType = contractType });
        }

        [HttpGet]
        public IActionResult DocumentsUpload(long? id)
        {
            return View(id);
        }

        [HttpPost]
        [RequestSizeLimit(50000000)]
        public ActionResult DocumentsUpload(long? documentId, string uid, List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                return ErrorResponse("На сервер не передано ни одного файла");
            }
            if (files.GroupBy(x => x.FileName).Count() != files.Count)
            {
                return ErrorResponse("На сервер переданы дублирующие файлы");
            }
            if (files.Count > 1)
            {
                return ErrorResponse("К документу прикрепляется только один файл");
            }
            if (files[0].Length == 0)
            {
                return ErrorResponse("Пустой файл");
            }

            long fileStorageId = _docService.UploadFiles(documentId, files);
            return Json(new {fileStorageId, documentId, uid});
        }

        [HttpGet]
        public FileResult ShowFile(long documentId)
        {
            DocumentFileDto file = _docService.GetFile(documentId);
            return File(file.StreamFile, "application/octet-stream", file.Name);
        }

        [HttpGet]
        public FileResult GetFileFromStorage(long fileStorageId)
        {
            DocumentFileDto file = _docService.GetFileFromStorage(fileStorageId);
            return File(file.StreamFile, "application/octet-stream", file.Name);
        }

        public ActionResult DocumentRead([DataSourceRequest] DataSourceRequest request, long objId, long reestrId, ContractType? type = null)
        {
           return Content(JsonConvert.SerializeObject(_docService.GetDocumentData(objId, reestrId, type).Select(x => DocumentDto.OMMap(x))), "application/json");
        }

        [HttpPost]
        public ActionResult DocumentCreate([Bind(Prefix = "models")]string documentsJson, long objId, long reestrId)
        {
            // траблы с постоянной пустой строкой в конце
            if (documentsJson.Contains("\"DocTrans_Code\":null"))
            {
                documentsJson = documentsJson.Replace("\"DocTrans_Code\":null", "\"DocTrans_Code\":0");
            }
            if (documentsJson.Contains("\"DocTypeM_Code\":null"))
            {
                documentsJson = documentsJson.Replace("\"DocTypeM_Code\":null", "\"DocTypeM_Code\":0");
            }
            var results = new List<DocumentDto>();
            List<DocumentDto> documents = JsonConvert.DeserializeObject<List<DocumentDto>>(documentsJson, new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });

            if (documents != null && ModelState.IsValid)
            {
                foreach (var doc in documents)
                {
                    // одну строку надо исключить, которая последняя
                    if (doc.DocTypeId != null)
                    {
                        doc.ObjId = objId;
                        doc.ReestrId = reestrId;
                        results.Add(DocumentDto.OMMap(_docService.CreatetDocument(DocumentDto.OMMap(doc))));
                    }
                }
            }

            return Content(JsonConvert.SerializeObject(results), "application/json");
        }

        [HttpPost]
        public ActionResult DocumentUpdate([Bind(Prefix = "models")]string documentsJson)
        {
            var results = new List<DocumentDto>();
            List<DocumentDto> documents = JsonConvert.DeserializeObject<List<DocumentDto>>(documentsJson, new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
            if (documents != null && ModelState.IsValid)
            {
                foreach (var doc in documents)
                {
                    results.Add(DocumentDto.OMMap(_docService.UpdateDocument(DocumentDto.OMMap(doc))));
                }
            }

            return Content(JsonConvert.SerializeObject(results), "application/json");
        }

        [HttpPost]
        public ActionResult DocumentDestroy([Bind(Prefix = "models")]string documentsJson)
        {
            List<DocumentDto> documents = JsonConvert.DeserializeObject<List<DocumentDto>>(documentsJson);
            if (documents.Any())
            {
                foreach (var doc in documents)
                {
                    _docService.DestroyDocument(DocumentDto.OMMap(doc));
                }
            }

            return Content(JsonConvert.SerializeObject(documents), "application/json");
        }

        [HttpGet]
        public ViewResult BaseDocumentEdit(long? id, string contractType)
        {
            return View("~/Views/Documents/BaseDocument/Edit.cshtml", BaseDocumentEditDto.OMMap(_docService.GetOrCreateBaseDocument(id, contractType)));
        }

        [HttpPost]
        public ContentResult BaseDocumentEdit(BaseDocumentEditDto baseDocument)
        {
            try
            {
                _docService.UpdateBaseDocument(BaseDocumentEditDto.OMMap(baseDocument));
                return EmptyResponse();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }
    }
}