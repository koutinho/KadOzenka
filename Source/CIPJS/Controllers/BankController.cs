using CIPJS.DAL.Bank;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CIPJS.Controllers
{
    public class BankController : BaseController
    {
        private readonly BankService _bankService;

        public BankController(BankService bankService)
        {
            _bankService = bankService;
        }

        public IActionResult GetById(long? id)
        {
            BankDto model = _bankService.Get(id);
            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        [HttpGet]
        public IActionResult BankCard(long? objectId)
        {
            return View(_bankService.Get(objectId));
        }

        [HttpGet]
        public IActionResult DeleteBank(long objectId)
        {
            return View(_bankService.Get(objectId));
        }

        [HttpPost]
        public IActionResult Delete(long objectId)
        {
            _bankService.Delete(objectId);
            return EmptyResponse();
        }

        [HttpPost]
        public IActionResult BankCard(BankDto model)
        {
            _bankService.Save(model);

            return EmptyResponse();
        }

        public IActionResult GetBankByBik(string bic)
        {
            List<BankDto> models = _bankService.GetByBic(bic);

            return Content(JsonConvert.SerializeObject(models), "application/json");
        }
    }
}