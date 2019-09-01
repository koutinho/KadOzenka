using CIPJS.DAL.InsuranceOrganization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using ObjectModel.Insur;

namespace CIPJS.Controllers
{
    public class InsuranceOrganizationController : Controller
    {
        private readonly InsuranceOrganizationService service = new InsuranceOrganizationService();

        public IActionResult GetById(long? id)
        {
            InsuranceOrganizationDto model = service.Get(id);
            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        public ActionResult GetByName(string name)
        {
            List<InsuranceOrganizationDto> models = service.GetByName(name);
            return Content(JsonConvert.SerializeObject(models), "application/json");
        }

        public ActionResult LinkStrahPost()
        {
            return View();
        }

        public ActionResult GetStrahPostLinks()
        {
            var list = OMLinkStrahPost
                .Where(x => x.Id)
                .SetJoinType(QSJoinType.Inner)
                .SelectAll()
                .Select(x => x.ParentInsuranceOrganization.FullName)
                .SetOrder(new List<QSOrder> {
                    new QSOrder { Column = OMInsuranceOrganization.GetColumn(x => x.FullName) },
                    new QSOrder { Column = OMLinkStrahPost.GetColumn(x => x.Id) }
                })
                .Execute()
                .Select(LinkStrahPostDto.FromOMObject);
            return Content(JsonConvert.SerializeObject(list), "application/json");
        }

        public ActionResult InsertStrahPostLinks(LinkStrahPostDto dto)
        {
            dto.Id = this.service.InsertLinkStrahPost(dto);
            return Content(JsonConvert.SerializeObject(dto), "application/json");
        }

        public ActionResult UpdateStrahPostLinks(LinkStrahPostDto dto)
        {
            this.service.UpdateLinkStrahPost(dto);
            return new EmptyResult();
        }

        public ActionResult DeleteStrahPostLinks(LinkStrahPostDto dto)
        {
            this.service.DeleteLinkStrahPost(dto);
            return new EmptyResult();
        }
    }
}