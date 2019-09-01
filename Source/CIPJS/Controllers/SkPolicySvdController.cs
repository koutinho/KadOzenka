using System.Collections.Generic;
using System.Linq;
using CIPJS.DAL.Fsp;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Models.CoreUi;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Insur;

namespace CIPJS.Controllers
{
    public class SkPolicySvdController : Controller
    {
        private readonly FspService _fspService;
        private readonly SkPolicySvdService _skPolicySvdService;

        public SkPolicySvdController(FspService fspService, SkPolicySvdService skPolicySvdService)
        {
            _fspService = fspService;
            _skPolicySvdService = skPolicySvdService;
        }

        public ActionResult LinkToFspOnEpd()
        {
            List<long?> ids = RegistersVariables.CurrentList?.Select(x => (long?)x).ToList() ?? new List<long?>();
            OMFsp fsp = null;
            int numbersCount = 0;

            var b = OMBuilding.Where(x => x.Unom == 470034)
                .Select(x => x.SpecialS)                           
                    .SetJoins(new List<QSJoin>                     
                    {                                              
                        new QSJoin                                 
                        {                                          
                            RegisterId = OMBuilding.GetRegisterId(),
                            ActualDate = null                      
                        }                                          
                    })
                .SelectAll().Execute();

            if (ids.IsNotEmpty())
            {
                foreach (var id in ids)
                {
                    var polis = _skPolicySvdService.Get(id);
                    if (polis != null)
                    {
                        fsp = _fspService.GetByContractId(polis.EmpId);

                        if (fsp is null && polis.Kodpl.HasValue)
                        {
                            fsp = _fspService.GetByKodpl(polis.Kodpl.Value.ParseToString());

                            if (fsp != null)
                            {
                                fsp.ContractId = polis.EmpId;
                                fsp.IdReestrContr = OMPolicySvd.GetRegisterId();
                                fsp.Save();

                                numbersCount++;
                            }
                        }
                    }
                }
            }

            var model = new ModalDialogDetails();

            if (numbersCount > 0)
            {
                model.Message = "Связь создана";
                model.Icon = ModalDialogDetails.IconType.Ok;
                model.Buttons = ModalDialogDetails.ButtonType.Ok;
                model.Action = ModalDialogDetails.ActionType.Reload; // После нажатия кнопки Ok, грид будет обновлен
            }
            else
            {
                model.Message = "Не найден счет плательщика для установления связи!";
                model.Icon = ModalDialogDetails.IconType.Warning;
                model.Buttons = ModalDialogDetails.ButtonType.Ok;
            }

            return View("~/Views/Shared/ModalDialogDetails.cshtml", model);
        }
    }
}