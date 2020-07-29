using KadOzenka.Dal.Documents;
using Microsoft.AspNetCore.Mvc;

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
            return View();
		}

        #endregion


        #region Support Methods


        #endregion
    }
}
