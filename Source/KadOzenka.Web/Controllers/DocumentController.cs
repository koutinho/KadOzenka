using CommonSdks;
using CommonSdks.PlatformWrappers;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Documents;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Models.Document;
using Microsoft.AspNetCore.Mvc;
using KadOzenka.Web.Attributes;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
    public class DocumentController : KoBaseController
	{
        public DocumentService DocumentService { get; set; }

        public DocumentController(DocumentService documentService, IRegisterCacheWrapper registerCacheWrapper,
	        IGbuObjectService gbuObjectService)
	        : base(gbuObjectService, registerCacheWrapper)
        {
            DocumentService = documentService;
        }


        #region Document Card

		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DOCUMENTS)]
        public ActionResult DocumentCard(long documentId)
		{
            var document = DocumentService.GetDocumentById(documentId);

            var model = DocumentModel.ToModel(document);

            return View(model);
		}

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DOCUMENTS_EDIT)]
        public ActionResult AddDocument()
        {
            var model = new DocumentModel();

            return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.DOCUMENTS_EDIT)]
        public JsonResult EditDocument(DocumentModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var dto = model.ToEntity(model);

            if (model.Id.HasValue)
            {
                DocumentService.UpdateDocument(dto);
            }
            else
            {
                DocumentService.AddDocument(dto);
            }

            return new JsonResult(Ok());
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.DOCUMENTS_DELETE)]
        public ActionResult ConfirmDeleteDocument(long documentId)
        {
            ViewBag.DocumentId = documentId;

            return View();
        }

        [HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.DOCUMENTS_DELETE)]
        public JsonResult DeleteDocument(long documentId)
        {
            DocumentService.DeleteDocument(documentId);

            return new JsonResult(Ok());
        }

        #endregion
    }
}
