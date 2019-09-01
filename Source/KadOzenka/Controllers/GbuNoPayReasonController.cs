using CIPJS.DAL.GbuNoPayReason;
using Core.ErrorManagment;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace CIPJS.Controllers
{
    public class GbuNoPayReasonController : BaseController
    {
        private GbuNoPayReasonService service = new GbuNoPayReasonService();

        public IActionResult GetById(long? id)
        {
            GbuNoPayReasonDto model = service.Get(id);
            return Content(JsonConvert.SerializeObject(model), "application/json");
        }
        
        [HttpGet]
        public ViewResult Edit(long? id, string contractType)
        {
            return View(service.Get(id, contractType));
        }

        [HttpPost]
        public ContentResult Edit(GbuNoPayReasonDto gbuNoPayReason)
        {
            try
            {
                service.Update(GbuNoPayReasonDto.OMMap(gbuNoPayReason));
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