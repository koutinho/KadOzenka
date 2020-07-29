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

        [HttpPost]
        public JsonResult DocumentCard(DocumentModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var dto = model.ToEntity(model);
            DocumentService.UpdateDocument(dto);

            return new JsonResult(Ok());
        }

        #endregion


        #region Support Methods


        #endregion
    }
}
