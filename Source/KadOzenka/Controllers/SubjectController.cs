using CIPJS.DAL.Subject;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.Models.CoreUi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Directory;
using System;
using System.Collections.Generic;

namespace CIPJS.Controllers
{
    public class SubjectController : BaseController
    {
        private SubjectService service = new SubjectService();

        public IActionResult GetById(long? id)
        {
            SubjectDto model = service.Get(id);
            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        [HttpGet]
        public ActionResult SubjectCard(long? objectId, bool IsFisical)
        {
            SubjectDto model = service.Get(objectId);
            ViewBag.IsFisical = IsFisical;
            if (objectId == null || objectId == -1)
            {
                model.TypeSubjectCode = IsFisical ? SubjectType.Individual : SubjectType.ManagementCompany;
            }
            return View(model);
        }

        [HttpPost]
        public void SubjectCard(SubjectDto model)
        {
            service.Save(model);
        }

        public ActionResult GetSubjectByName(string name, SubjectType? subjectType = null)
        {
            List<SubjectDto> models = service.GetByName(name, subjectType);
            return Content(JsonConvert.SerializeObject(models), "application/json");
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(long id)
        {
            ModalDialogDetails model = new ModalDialogDetails();

            try
            {
                service.CheckDelete(id, out string subjectName);
                model.Message = $"Внимание, Вы подтверждаете удаление субъекта {subjectName}?";
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
                model.Icon = ModalDialogDetails.IconType.Warning;
                model.Buttons = ModalDialogDetails.ButtonType.Ok;
            }

            return View("~/Views/Shared/ModalDialogDetails.cshtml", model);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                service.Delete(id);

                return Json(new
                {
                    type = "Success",
                    message = "Субъект успешно удален",
                    reload = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    type = "Error",
                    message = ex.Message
                });
            }
        }
    }
}