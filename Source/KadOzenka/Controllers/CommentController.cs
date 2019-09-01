using CIPJS.DAL.Comment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIPJS.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _service;

        public CommentController(CommentService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Index(long? reestrId, long? objectId, bool isReadOnly)
        {
            CommentListDto model = _service.Get(reestrId, objectId, isReadOnly);
            return PartialView("Index", model);
        }

        [HttpGet]
        public ActionResult GetComment(long? reestrId, long? id)
        {
            CommentDto model = _service.GetComment(reestrId, id);
            return PartialView("Details", model);
        }

        [HttpPost]
        public void Index(string json)
        {
            CommentListDto model = JsonConvert.DeserializeObject<CommentListDto>(json, new JsonSerializerSettings
            {
                Culture = new System.Globalization.CultureInfo("ru-RU"),
                DateFormatString = "dd.MM.yyyy HH:mm:ss"
            });
            _service.Save(model);
        }

        [HttpPost]
        public void Delete(long? id)
        {
            _service.Delete(id);
        }
    }
}