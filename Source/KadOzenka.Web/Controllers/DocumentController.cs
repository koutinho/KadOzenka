using KadOzenka.Dal.Documents;
using KadOzenka.Web.Models.Document;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Core.TD;

namespace KadOzenka.Web.Controllers
{
    //TODO add SRD
	public class DocumentController : KoBaseController
	{
        public DocumentService DocumentService { get; set; }

        public DocumentController(DocumentService documentService)
        {
            DocumentService = documentService;
        }

        #region Document Card

		[HttpGet]
        public ActionResult DocumentCard(long documentId)
		{
            var document = DocumentService.GetDocumentById(documentId);

            var model = DocumentModel.ToModel(document);

            return View(model);
		}

        [HttpGet]
        public ActionResult AddDocument()
        {
            var model = new DocumentModel();

            return View(model);
        }

        [HttpPost]
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

        [HttpDelete]
        public JsonResult DeleteDocument(long documentId)
        {
            DocumentService.DeleteDocument(documentId);

            return new JsonResult(Ok());
        }

        #endregion


        #region Support Methods


        #endregion
    }
}
