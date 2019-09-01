using CIPJS.DAL.InputPlat;
using CIPJS.Models.InputPlat;
using Core.ErrorManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.SRD.DAL;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace CIPJS.Controllers
{
    public class InputPlatController : BaseController
    {
        private readonly InputPlatService _inputPlatService;

        public InputPlatController(InputPlatService inputPlatService)
        {
            _inputPlatService = inputPlatService;
        }

        [HttpGet]
        public IActionResult GetById(long id)
        {
            InputPlatDto bankPlat = InputPlatDto.OMMap(_inputPlatService.Get(id));
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

            List<long> inputPlatIds = RegistersVariables.CurrentList?.ToList() ?? new List<long>();

            List<InputPlatDto> inputPlatList = new List<InputPlatDto>();

            foreach (long inputPlatId in inputPlatIds)
            {
                inputPlatList.Add(InputPlatDto.OMMap(_inputPlatService.Get(inputPlatId)));
            }

            return Content(JsonConvert.SerializeObject(inputPlatList), "application/json");
        }

        [HttpPost]
        public ContentResult Update(InputPlatDto inputPlatDto)
        {
            try
            {
                List<OMChangesLog> changesLog = new List<OMChangesLog>();
                OMInputPlat inputPlat = InputPlatDto.OMMap(inputPlatDto, changesLog);

                using(TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    inputPlatDto.Id = inputPlat.Save();

                    foreach(OMChangesLog change in changesLog)
                    {
                        change.Save();
                    }

                    ts.Complete();
                }

                return JsonResponse(inputPlatDto);
            }
            catch(Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult LinkPlat(long inputPlatId)
        {
            SetUniqueSessionKey();

            if (CurrentUniqueSessionKey.IsNullOrEmpty())
            {
                throw new Exception("Не передан уникальный ключ сессии");
            }

            ViewBag.CurrentUniqueSessionKey = CurrentUniqueSessionKey;

            OMInputPlat inputPlat = OMInputPlat.Where(x => x.EmpId == inputPlatId)
                .SelectAll()
                .ExecuteFirstOrDefault();

            if (inputPlat == null)
            {
                throw new Exception($"Не удалось определить зачисление с идентификатором {inputPlatId}");
            }

            return View(LinkPlatDto.OMMap(inputPlat));
        }

        [HttpPost]
        public ContentResult LinkPlat(long inputPlatId, long bankPlatId, string reason, bool rewrite = false)
        {
            try
            {
                _inputPlatService.LinkPlat(inputPlatId, bankPlatId, reason, rewrite);
                return EmptyResponse();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult NotConfirmedByBank(long inputPlatId, string reason)
        {
            try
            {
                _inputPlatService.NotConfirmedByBank(inputPlatId, reason);
                return EmptyResponse();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        public ActionResult LinkToContract(long id)
        {
            List<long> ids = RegistersVariables.CurrentList?.ToList() ?? new List<long>();

            if (!ids.Any()) ids.Add(id);

            return View(new LinkToContractDto
            {
                InputPlatIds = ids,
                ExistsLinks = _inputPlatService.CheckHasLinkToContract(ids)
            });
        }

        [HttpPost]
        public ActionResult LinkToContract(LinkToContractDto model)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_PAY, true, false, true);

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                _inputPlatService.SetLinkToContract(model.InputPlatIds, model.ContractId);

                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_PAY, true, "Связаны платежи с договором", OMAllProperty.GetRegisterId(), model.ContractId);

                ts.Complete();
            }

            return EmptyResponse();
        }
    }
}
