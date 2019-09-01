using CIPJS.DAL.Flat;
using CIPJS.DAL.Fsp;
using CIPJS.DAL.InputPlat;
using CIPJS.Models.Fsp;
using Core.ErrorManagment;
using Core.Shared.Misc;
using Core.SRD;
using Core.SRD.DAL;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.UI.Registers.Models.CoreUi;

namespace CIPJS.Controllers
{
    public class FspController : BaseController
    {
        private readonly FspService _fspService;
        private readonly InputPlatService _inputPlatService;
        private readonly FlatService _flatService;

        public FspController(FspService fspService, InputPlatService inputPlatService, FlatService flatService)
        {
            _fspService = fspService;
            _inputPlatService = inputPlatService;
            _flatService = flatService;
        }

        [HttpGet]
        public ActionResult Create(long? objId, long? insuranceOrganizationId = null)
        {
            if (!objId.HasValue)
            {
                throw new Exception("Не передан обязательный параметр идентификатор объекта");
            }

            return View(new FspCreateDto
            {
                FspType = FSPType.EPD,
                ObjId = objId.Value,
                InsuranceOrganizationId = insuranceOrganizationId
            });
        }

        [HttpPost]
        [Consumes("application/json")]
        public ContentResult Create([FromBody]FspCreateDto fspCreateDto)
        {
            try
            {
                // При передаче данных из view в dto приходят даты в utc формате.
                // CIPJS-955: Исправить ошибки при создании при нового договора - неверно сохраняет Дату С , уменьшает на один день
                fspCreateDto.DateBegin = fspCreateDto.DateBegin.HasValue ? fspCreateDto.DateBegin.Value.ToLocalTime() : fspCreateDto.DateBegin;
                fspCreateDto.DateEnd = fspCreateDto.DateEnd.HasValue ? fspCreateDto.DateEnd.Value.ToLocalTime() : fspCreateDto.DateEnd;
                DocumentType documentType;
                List<string> errors;
                long? existPolicyId;
                OMFsp fsp = _fspService.Create(fspCreateDto.FspType,
                    fspCreateDto.ObjId, fspCreateDto.Kodpl, fspCreateDto.Npol, fspCreateDto.OplKodpl,
                    fspCreateDto.DateBegin, fspCreateDto.DateEnd, fspCreateDto.Pralt, fspCreateDto.InsuranceOrganizationId,
                    fspCreateDto.Ss, fspCreateDto.Soplvz,
                    out documentType, out errors, out existPolicyId);

                if (fsp == null)
                {
                    if (errors.Count > 0)
                    {
                        return ErrorResponse(errors);
                    }

                    if (existPolicyId.HasValue)
                    {
                        return JsonResponse(new
                        {
                            documentType = (long)documentType,
                            existPolicySvdUrl = Url.Action("Index", "RegistersView",
                            new
                            {
                                RegisterId = documentType == DocumentType.Polis ? 
                                    "SkPolicySvdPolis" : "SkPolicySvdCertificate",
                                Transition = "1",
                                qs = $"(309000100equal'{existPolicyId}')"
                            })
                        });
                    }

                    return ErrorResponse("Не удалось создать договор");
                }
                else
                {
                    return JsonResponse(fsp.EmpId);
                }
            }
            catch(Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        public ActionResult PartialDetails(long? id)
        {
            OMFsp fsp = _fspService.GetById(id, false, true);

            //var fspService = new FspService();
            //fspService.CalcBalanceSumFromPeriod(105061416, DateTime.Parse("01.04.2019"));

            var model = FspDetails.OMMap(fsp);

            return PartialView("Details", model);
        }

        public ActionResult Details(long? id)
        {
            OMFsp fsp = _fspService.GetById(id, false, true);

            if (fsp != null && fsp.ObjId.HasValue)
            {
                OMFlat flat = _flatService.Get(fsp.ObjId);

                if(flat != null)
                    return Redirect(Url.Content($"~/Tenements/LivingSpace/{flat.EmpId}#AFSP{fsp.EmpId}"));
            }

            var model = FspDetails.OMMap(fsp);

            return View(model);
        }

        /// <summary>
        /// Получить количество ФСП по идентификатору помещения
        /// </summary>
        /// <param name="id">Идентификатор помещения</param>
        /// <returns>количество ФСП</returns>
        public int Count(long? id)
        {
            return _fspService.CountByFlatId(id);
        }

        [HttpPost]
        public ContentResult GetById(long id)
        {
            OMFsp fsp = _fspService.GetById(id);
            return Content(JsonConvert.SerializeObject(fsp), "application/json");
        }

        public ActionResult GetPlats([DataSourceRequest] DataSourceRequest request, int fspId)
        {
            List<FspInputPlatDetails> models = _inputPlatService.GetByFsp(fspId)
                .OrderByDescending(x => x.PeriodRegDate)
                .Select((x, y) => FspInputPlatDetails.OMMap(x, y))
                .ToList();

            return Content(JsonConvert.SerializeObject(models), "application/json");
        }

        public ActionResult BindAndAccount(long fspId, long inputObjectId, long inputRegisterId)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_PAY, true, false, true);

            List<long> ids = RegistersVariables.CurrentList?.ToList() ?? new List<long>();
            if (ids.Count == 0)
            {
                ids.Add(inputObjectId);
            }

            List<string> errors = new List<string>();

            try
            {
                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
                {
                    if (inputRegisterId == OMInputNach.GetRegisterId())
                    {
                        List<OMInputNach> inputNachList = OMInputNach
                            .Where(x => ids.Contains(x.EmpId))
                            .SelectAll()
                            .Execute();

                        _fspService.BindAndAccount(fspId, inputNachList);
                    }
                    else if (inputRegisterId == OMInputPlat.GetRegisterId())
                    {
                        List<OMInputPlat> inputPlatList = OMInputPlat
                               .Where(x => ids.Contains(x.EmpId))
                               .SelectAll()
                               .Execute();

                        _fspService.BindAndAccount(fspId, inputPlatList);
                    }
                    else
                    {
                        errors.Add("Переданы идентификаторы неподдерживаемого реестра для учета начислений/зачислений на ФСП");
                    }

                    if (errors.Count == 0)
                    {
                        SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_PAY, true, "Привязано зачисление", inputRegisterId, inputObjectId);

                        ts.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }

            return View(errors);
        }

        public ActionResult UnbindAndAccount(long inputObjectId, long inputRegisterId)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_PAY, true, false, true);

            List<long> ids = RegistersVariables.CurrentList?.ToList() ?? new List<long>();
            if (ids.Count == 0)
            {
                ids.Add(inputObjectId);
            }

            var errors = new List<string>();

            try
            {
                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
                {
                    if (inputRegisterId == OMInputNach.GetRegisterId())
                    {
                        List<OMInputNach> inputNachList = OMInputNach
                            .Where(x => ids.Contains(x.EmpId))
                            .SelectAll()
                            .Execute();

                        _fspService.UnbindAndAccount(inputNachList);
                    }
                    else if (inputRegisterId == OMInputPlat.GetRegisterId())
                    {
                        List<OMInputPlat> inputPlatList = OMInputPlat
                            .Where(x => ids.Contains(x.EmpId))
                            .SelectAll()
                            .Execute();

                        _fspService.UnbindAndAccount(inputPlatList);
                    }
                    else
                    {
                        errors.Add("Переданы идентификаторы неподдерживаемого реестра для отвязки и перерасчета суммы начислений/зачислений на ФСП");
                    }

                    if (errors.Count == 0)
                    {
                        SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_PAY, true, "Отвязано зачисление", inputRegisterId, inputObjectId);

                        ts.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }

            return View(errors);
        }

        public ActionResult CreateStrahNach(long inputObjectId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStrahNach(CreateStrahNachDto createStrahNachDto)
        {
            try
            {
                if (createStrahNachDto == null)
                {
                    throw new Exception("Не были переданы обязательные параметры");
                }

                List<OMInputNach> strahNachList = 
                    _fspService.CreateStrahNachByPeriods(createStrahNachDto.FspId, createStrahNachDto.DateS, createStrahNachDto.DatePo);

                return JsonResponse(strahNachList);
            }
            catch(Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult LinkToFlat()
        {
            var list = RegistersVariables.CurrentList;
            if (list == null || list.Count == 0)
                return LinkToFlatNoRecords();
            else if (list.Count == 1)
                return LinkSingleToFlat(list.First());
            else
                return LinkManyToFlat(list);
        }

        public ActionResult LinkToFlatNoRecords()
        {
            return View("~/Views/Shared/ModalDialogDetails.cshtml", new ModalDialogDetails
            {
                Action = ModalDialogDetails.ActionType.None,
                Buttons = ModalDialogDetails.ButtonType.Ok,
                Icon = ModalDialogDetails.IconType.Warning,
                Message = "Не отмечено ни одной записи"
            });
        }

        private ActionResult LinkSingleToFlat(long fspId)
        {
            bool hasLinkToFlat = _fspService.HasLinkToFlat(fspId, out var linkDescr);
            var fsp = OMFsp
                .Where(x => x.EmpId == fspId)
                .Select(x => x.FspNumber)
                .ExecuteFirstOrDefault();
            var nach = this._fspService.GetInputNachForLink(fspId);
            return View("LinkSingleToFlat", new LinkSingleToFlatViewModel
            {
                FspId = fspId,
                FspNumber = fsp.FspNumber,
                Unom = nach == null || !nach.Unom.HasValue ? string.Empty : nach.Unom.Value.ToString(),
                Kvnom = nach == null || nach.Kvnom == null ? string.Empty : nach.Kvnom,
                HasLinkToFlat = hasLinkToFlat,
                LinkToFlatDescription = linkDescr
            });
        }

        private ActionResult LinkManyToFlat(ICollection<long> fspIds)
        {
            if (this._fspService.HasLinkToFlat(fspIds))
                return View("~/Views/Shared/ModalDialogDetails.cshtml", new ModalDialogDetails
                {
                    Action = ModalDialogDetails.ActionType.None,
                    Buttons = ModalDialogDetails.ButtonType.Ok,
                    Icon = ModalDialogDetails.IconType.Warning,
                    Message = "Внимание! Среди выбранных ФСП присутствует связь с объектом, операция невозможна. Изменить ранее установленную связь с объектом возможно только в индивидуальном режиме"
                });
            else if (!this._fspService.CanMassLinkToFlat(fspIds, out var reason))
                return View("~/Views/Shared/ModalDialogDetails.cshtml", new ModalDialogDetails
                {
                    Action = ModalDialogDetails.ActionType.None,
                    Buttons = ModalDialogDetails.ButtonType.Ok,
                    Icon = ModalDialogDetails.IconType.Warning,
                    Message = reason
                });

            var fspId = fspIds.First();
            var fsp = this._fspService.GetFspForLink(fspId);
            var currentBuilding = fsp.UnomFsp.HasValue
                ? OMBuilding
                    .Where(x => x.Unom == fsp.UnomFsp)
                    .Select(x => x.ParentAddress.ShortAddress)
                    .SetPackageSize(1)
                    .ExecuteFirstOrDefault()
                : null;
            var altUnom = fsp.UnomFsp.HasValue
                ? OMUnomUpdate
                    .Where(x => x.OldUnom == fsp.UnomFsp)
                    .OrderByDescending(x => x.DateChange)
                    .SetPackageSize(1)
                    .Select(x => x.NewUnom)
                    .Select(x => x.NewLinkMkd)
                    .Select(x => x.Note)
                    .ExecuteFirstOrDefault()
                : null;
            var altBuilding = altUnom == null
                ? null
                : OMBuilding
                    .Where(x => x.EmpId == altUnom.NewLinkMkd)
                    .Select(x => x.ParentAddress.ShortAddress)
                    .SetPackageSize(1)
                    .ExecuteFirstOrDefault();

            return View("LinkManyToFlat", new LinkManyToFlatViewModel
            {
                FspIds = fspIds,
                CurrentUnom = fsp.UnomFsp?.ToString(),
                CurrentAddress = currentBuilding?.ParentAddress?.ShortAddress ?? "",
                CurrentBuildingId = currentBuilding?.EmpId,
                AltUnom = altUnom?.NewUnom.ToString(),
                AltAddress = altBuilding?.ParentAddress?.ShortAddress ?? "",
                AltBuildingId = altBuilding?.EmpId,
                Comment = altUnom?.Note ?? "",
                ShowUnomUpdateHistory = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_FSP_VIEW_UNOM_UPDATE_HISTORY)
            });
        }

        [HttpPost]
        public IActionResult LinkSingleToFlat(SingleFspFlatLinkDto model)
        {
            _fspService.LinkToFlat(model);
            return EmptyResponse();
        }

        [HttpPost]
        public IActionResult LinkManyToFlat(ManyFspFlatLinkDto model)
        {
            int result = this._fspService.MassLinkToFlat(model);
            return View("~/Views/Shared/ModalDialogDetails.cshtml", new ModalDialogDetails
            {
                Action = ModalDialogDetails.ActionType.None,
                Buttons = ModalDialogDetails.ButtonType.Ok,
                Icon = ModalDialogDetails.IconType.Warning,
                Message = $"Внимание! Удалось установить связь между ФСП и помещениями для {result} ФСП из {model.FspIds.Count}"
            });
        }

        [HttpPost]
        public IActionResult LinkToFlatConfirmMessage(SingleFspFlatLinkDto model)
        {
            return Json(_fspService.LinkToFlatConfirmMessage(model));
        }

        [HttpGet]
        public ActionResult UnlinkFlatConfirm()
        {
            var list = RegistersVariables.CurrentList;
            if (list == null || list.Count == 0)
                return LinkToFlatNoRecords();
            var fsp = OMFsp
                .Where(x => list.Contains(x.EmpId))
                .Select(x => x.FspNumber)
                .Execute();
            return View(fsp);
        }

        [HttpPost]
        public ActionResult UnlinkFlat(IReadOnlyList<long> fspIds)
        {
            this._fspService.UnlinkFlat(fspIds);
            return View("~/Views/Shared/ModalDialogDetails.cshtml", new ModalDialogDetails
            {
                Action = ModalDialogDetails.ActionType.None,
                Buttons = ModalDialogDetails.ButtonType.Ok,
                Icon = ModalDialogDetails.IconType.Ok,
                Message = "Связи с помещением удалена"
            });
        }

        [HttpGet]
        public IActionResult Recalc(long? fspId)
        {
            List<long> fspIds = RegistersVariables.CurrentList?.ToList() ?? new List<long>();
            if (fspIds.Count == 0 && fspId.HasValue)
            {
                fspIds.Add(fspId.Value);
            }

            if (fspIds.Count == 0)
            {
                throw new Exception("Не выбрано ни одного ФСП");
            }

            return View(fspIds);
        }


        [HttpPost]
        [Consumes("application/json")]
        public ContentResult Recalc([FromBody]List<long> fspIds)
        {
            try
            {
                if (fspIds.Count == 0)
                {
                    throw new Exception("Не выбрано ни одного ФСП");
                }

                DateTime currentPeriod = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                foreach (long fspId in fspIds)
                {
                    _fspService.AccountFsp(fspId, currentPeriod);

                    List<OMBalance> balances = OMBalance.Where(w => w.FspId == fspId).OrderBy(o => o.PeriodRegDate).SelectAll().Execute().ToList();

                    _fspService.CalcBalanceSumFromPeriod(fspId, currentPeriod, balances, renewStrahEnd: true);
                }

                return EmptyResponse();
            }
            catch(Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult UnomUpdateHistoryGrid()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult GetUnomUpdateHistory()
        {
            return Content(JsonConvert.SerializeObject(new UnomUpdateHistoryService().Get()), "application/json");
        }

        [HttpPost]
        public ActionResult InsertUnomUpdateHistory(UnomUpdateHistoryDto dto)
        {
            new UnomUpdateHistoryService().Insert(dto);
            return Content(JsonConvert.SerializeObject(dto), "application/json");
        }

        [HttpPost]
        public ActionResult UpdateUnomUpdateHistory(UnomUpdateHistoryDto dto)
        {
            new UnomUpdateHistoryService().Update(dto);
            return Content(JsonConvert.SerializeObject(dto), "application/json");
        }

        [HttpPost]
        public ActionResult DeleteUnomUpdateHistory(UnomUpdateHistoryDto dto)
        {
            new UnomUpdateHistoryService().Delete(dto);
            return new EmptyResult();
        }
    }
}