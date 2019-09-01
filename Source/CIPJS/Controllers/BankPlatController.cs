using CIPJS.DAL.BankPlat;
using CIPJS.Models.BankPlat;
using Core.Shared.Extensions;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.Controllers
{
    public class BankPlatController : BaseController
    {
        private BankPlatService _bankPlatService;

        public BankPlatController()
        {
            _bankPlatService = new BankPlatService();
        }

        [HttpGet]
        public IActionResult GetById(long id)
        {
            BankPlatDto bankPlat = BankPlatDto.OMMap(_bankPlatService.Get(id));
            return Content(JsonConvert.SerializeObject(bankPlat), "application/json");
        }

        [HttpGet]
        public ContentResult GetCurrentList()
        {
            SetUniqueSessionKey();

            if (CurrentUniqueSessionKey.IsNullOrEmpty())
            {
                throw new Exception("Не передан уникальный ключ сессии");
            }

            List<long> bankPlatIds = RegistersVariables.CurrentList?.ToList() ?? new List<long>();

            List<BankPlatDto> bankPlatList = new List<BankPlatDto>();

            foreach(long bankPlatId in bankPlatIds)
            {
                bankPlatList.Add(BankPlatDto.OMMap(_bankPlatService.Get(bankPlatId)));
            }

            return Content(JsonConvert.SerializeObject(bankPlatList), "application/json");
        }

        [HttpGet]
        public ActionResult LinkPlat(long bankPlatId)
        {
            SetUniqueSessionKey();

            if (CurrentUniqueSessionKey.IsNullOrEmpty())
            {
                throw new Exception("Не передан уникальный ключ сессии");
            }

            ViewBag.CurrentUniqueSessionKey = CurrentUniqueSessionKey;

            OMBankPlat bankPlat = OMBankPlat.Where(x => x.EmpId == bankPlatId)
                .SelectAll()
                .ExecuteFirstOrDefault();

            if (bankPlat == null)
            {
                throw new Exception($"Не удалось определить банковскую строку с идентификатором {bankPlatId}");
            }

            return View(LinkBankPlatDto.OMMap(bankPlat));
        }
    }
}
