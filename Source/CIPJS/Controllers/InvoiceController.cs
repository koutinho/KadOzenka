using CIPJS.DAL.DamageAnalysis;
using CIPJS.DAL.Invoice;
using CIPJS.DAL.ReestrPay;
using CIPJS.Models.Invoice;
using CIPJS.Models.ReestrPay;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.SRD.DAL;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Models.CoreUi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace CIPJS.Controllers
{
    public class InvoiceController : BaseController
    {
        private readonly InvoiceService _invoiceService;
        private readonly ReestrPayService _reestrPayService;
        private readonly DamageAnalysisService _damageAnalysisService;

        public InvoiceController(InvoiceService invoiceService, ReestrPayService reestrPayService, DamageAnalysisService damageAnalysisService)
        {
            _reestrPayService = reestrPayService;
            _invoiceService = invoiceService;
            _damageAnalysisService = damageAnalysisService;
        }

        [HttpGet]
        public ActionResult Index(long? damageId, long? allPropertyId, bool isReadOnly)
        {
            InvoiceListDto model = _invoiceService.Get(damageId, allPropertyId, isReadOnly);
            return PartialView("Index", model);
        }

        [HttpGet]
        public ActionResult PartialInvoiceDetailsByDamageFsp(long? damageId, long? fspId, bool readOnly = false)
        {
            if (!fspId.HasValue)
            {
                return new EmptyResult();
            }

            InvoiceDto model = _invoiceService.GetByDamageFsp(damageId, fspId.Value, true);

            model.ReadOnly = readOnly;

            return PartialView("Details", model);
        }

        [HttpGet]
        public ActionResult GetInvoice(long? id, long? damageId, long? allPropertyId, decimal? sum, long? fspId, decimal partDog)
        {
            InvoiceDto model = _invoiceService.GetInvoice(id, damageId, allPropertyId, sum, fspId, partDog);
            return PartialView("Details", model);
        }

        [HttpPost]
        [Consumes("application/json")]
        public ContentResult Index([FromBody]InvoiceListDto model)
        {
            try
            {
                List<string> validationErrors = _invoiceService.Validate(model);

                if (validationErrors.Count == 0)
                {
                    _invoiceService.Save(model);
                }
                else
                {
                    return ErrorResponse(validationErrors);
                }

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public void Agreed(long? id)
        {
            _invoiceService.Agreed(id);
        }

        [HttpGet]
        public ActionResult AgreedList(ReestrPayType type)
        {
            List<long?> ids = RegistersVariables.CurrentList?.Select(x => (long?)x).ToList() ?? new List<long?>();
            ViewBag.ReestrPayType = type;

            try
            {
                var errorMessage = string.Empty;

                List<OMInvoice> invoiceList = _reestrPayService.GetInvoices(ids);

                if (invoiceList.Where(x => x.Status_Code != InvoiceStatus.Formed).Any())
                    errorMessage = "Внимание! Согласовать можно только счета, находящиеся в статусе \"Создан\"!";
                else if (invoiceList.Where(x => !x.SumOpl.HasValue || x.SumOpl == 0).Any())
                    errorMessage = "Внимание! Согласовать можно только счета, сумма которых не ноль и не пусто!";

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ViewBag.ErrorMessage = errorMessage;
                    return View();
                }
                else
                    return View(invoiceList);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult AgreedList([FromBody]FormDto model)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL_DAMAGE_FLAT_AGREEDLIST, true, false, true);

            if (model == null || model.Ids == null || !model.Ids.Any()) return ErrorResponse("На сервер пришла пустая модель");

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                _invoiceService.Agreed(model.Ids);

                foreach(long id in model.Ids)
                    SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Статус счета изменен на 'Согласован'", OMInvoice.GetRegisterId(), id);

                ts.Complete();

                return EmptyResponse();
            }
        }

        [HttpPost]
        public ContentResult CheckInvoice(long? damageId, long? allPropertyId)
        {
            return Content(JsonConvert.SerializeObject(_invoiceService.CheckInvoice(damageId, allPropertyId)), "application/json");
        }

        [HttpPost]
        public void Delete(long? id)
        {
            _invoiceService.Delete(id);
        }

        [HttpGet]
        [ActionName("DeleteList")]
        public ActionResult DeleteListConfirm()
        {
            List<long?> ids = RegistersVariables.CurrentList?.Select(x => (long?)x).ToList() ?? new List<long?>();
            var model = new ModalDialogDetails();

            if (ids.IsNotEmpty())
            {
                var errorMessage = string.Empty;

                List<OMInvoice> invoiceList = _reestrPayService.GetInvoices(ids);

                if (invoiceList.Any(x => x.Status_Code != InvoiceStatus.Formed && x.Status_Code == InvoiceStatus.ErrorInDetails))
                {
                    model.Message = "Внимание! Удалить можно только счета, находящиеся в статусах \"Создан\" или \"Ошибка в реквизитах\"!";
                    model.Icon = ModalDialogDetails.IconType.Warning;
                    model.Buttons = ModalDialogDetails.ButtonType.Ok;
                }
                else
                    model.Message = "Вы уверены, что хотите удалить выбранные счета?";
            }
            else
            {
                model.Message = "Не выбрано ни одного счета";
                model.Icon = ModalDialogDetails.IconType.Warning;
                model.Buttons = ModalDialogDetails.ButtonType.Ok;
            }

            return View("~/Views/Shared/ModalDialogDetails.cshtml", model);
        }

        [HttpPost]
        public ActionResult DeleteList()
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, false, true);

            List<long> ids = RegistersVariables.CurrentList?.Select(x => x).ToList();

            try
            {
                _invoiceService.Delete(ids);

                return Json(new
                {
                    type = "Success",
                    message = "Счета успешно удалены",
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

        public ActionResult ChangeStatusToError()
        {
            List<long?> ids = RegistersVariables.CurrentList?.Select(x => (long?)x).ToList() ?? new List<long?>();

            // CIPJS-899
            ViewBag.IsInvoicePaysGP = RegistersVariables.CurrentRegisterViewId == "InvoicePaysGP";

            try
            {
                if (ids == null || !ids.Any() || ids.Count > 1) throw new Exception("Необходимо выбрать одну строку");

                List<OMInvoice> omInvoices = OMInvoice.Where(x => ids.Contains(x.EmpId)).SelectAll().Execute();
                OMInvoice invoice = omInvoices.FirstOrDefault();
                if(invoice == null)
                {
                    throw new Exception("Не найдены выбранные счета!");
                }
                return View(new ChangeStatusToErrorDto
                {
                    SubjectName = invoice.SubjectName,
                    SubjectId = invoice.SubjectId,
                    NumInvoice = invoice.NumInvoice,
                    DateInvoice = invoice.dateInvoice,
                    Phone = invoice.Phone,
                    Inn = invoice.Inn,
                    Kpp = invoice.Kpp,
                    InnBank = invoice.InnBank,
                    KppBank = invoice.KppBank,
                    BicBank = invoice.BicBank,
                    BankName = invoice.BankName,
                    KorAcc = invoice.KorAcc,
                    RachAcc = invoice.RachAcc,
                    Invoices = omInvoices.Select(x => new InvoiceItemDto
                    {
                        Id = x.EmpId,
                        SubjectName = x.SubjectName,
                        SumOpl = x.SumOpl
                       
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View();
            }
        }

        [HttpPost]
        public ActionResult ChangeStatusToError(ChangeStatusToErrorDto model)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, false, true);

            if (model == null || model.Invoices.IsEmpty()) return EmptyResponse();

            List<long> ids = model.Invoices.Select(x => x.Id).ToList();
            List<OMInvoice> omInvoices = OMInvoice.Where(x => ids.Contains(x.EmpId))
                .SelectAll()
                .Select(x => x.ParentDamage.EmpId)
                .Select(x => x.ParentAllProperty.EmpId)
                .Execute();

            Dictionary<long, long> newSvodIds = new Dictionary<long, long>();
            List<long> invoiceSvodIds = omInvoices.Where(x => x.LinkInvoiceSvod.HasValue).Select(x => x.LinkInvoiceSvod.Value).ToList();

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                //CIPJS-694 статус в INSUR_INVIOICE изменяет и статус записи в INSUR_INVOICE_SVOD
                if (invoiceSvodIds.Count > 0)
                {
                    List<OMInvoiceSvod> svodList = OMInvoiceSvod.Where(x => invoiceSvodIds.Contains(x.EmpId))
                        .SelectAll()
                        .Execute();

                    foreach (OMInvoiceSvod svod in svodList)
                    {
                        svod.Status_Code = InvoiceStatus.ErrorInDetails;
                        svod.Save();

                        long newSvodId = new OMInvoiceSvod
                        {
                            SubjectId = svod.SubjectId,
                            SubjectName = svod.SubjectName,
                            NumInvoice = svod.NumInvoice,
                            DateInvoice = svod.DateInvoice,
                            InvoiceVsego = svod.InvoiceVsego,
                            SumSvod = svod.SumSvod,
                            Status_Code = InvoiceStatus.Formed
                        }.Save();

                        newSvodIds.Add(svod.EmpId, newSvodId);
                    }
                }

                foreach (OMInvoice omInvoice in omInvoices)
                {
                    omInvoice.Status_Code = InvoiceStatus.ErrorInDetails;
                    omInvoice.Save();

                    if (omInvoice.ParentDamage != null)
                    {
                        // Изменен функционал задачей  CIPJS-899 
                        //CIPJS-434 нужно реализовать, чтобы дело по расчету ущерба в ЖП/ОИ ( ТОЛЬКО если кнопку нажали для представления со счетами по ущербу) 
                        //переводилось в статус "Передано на проверку"
                        //omInvoice.ParentDamage.DamageAmountStatus_Code = StatusDamageAmount.SendToCheck;
                        //omInvoice.ParentDamage.Save();

                        // CIPJS-899: Исправить проблемы для функционала по кнопке "Изменить статус счета-ошибка в реквизитах"
                        _damageAnalysisService.ReturnToRevision(omInvoice.ParentDamage.EmpId);

                        SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменено дело по расчету суммы ущерба", OMInvoice.GetRegisterId(), omInvoice.ParentDamage.EmpId);
                    }

                    if (omInvoice.ParentAllProperty != null)
                    {
                        omInvoice.ParentAllProperty.Status_Code = ContractStatus.Created;
                        omInvoice.ParentAllProperty.Save();
                        SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен договор", OMAllProperty.GetRegisterId(), omInvoice.ParentAllProperty.EmpId);
                    }

                    // Простовляем флаг "Перевыпуск заключения" 
                    OMDamage damage = OMDamage.Where(x => x.EmpId == omInvoice.LinkDamage && x.SpecialActual == 1).ExecuteFirstOrDefault();
                    if(damage != null)
                    {
                        damage.FlagZakluchReissue = true;
                        damage.Save();
                    }

                    int invoiceId = new OMInvoice
                    {
                        BankId = model.BankId,
                        BankName = model.BankName,
                        BicBank = model.BicBank,
                        Comment = omInvoice.Comment,
                        DataInput = DateTime.Now,
                        DateAgree = null    ,
                        dateInvoice = model.DateInvoice,
                        Inn = model.Inn,
                        InnBank = model.InnBank,
                        KorAcc = model.KorAcc,
                        Kpp = model.Kpp,
                        KppBank = model.KppBank,
                        LinkAllProperty = omInvoice.LinkAllProperty,
                        LinkDamage = omInvoice.LinkDamage,
                        LinkFsp = omInvoice.LinkFsp,
                        NoteNoPayId = omInvoice.NoteNoPayId,
                        NumCard = omInvoice.NumCard,
                        NumInvoice = model.NumInvoice,
                        Phone = model.Phone,
                        RachAcc = model.RachAcc,
                        Status_Code = InvoiceStatus.Formed,
                        SubjectId = model.SubjectId,
                        SubjectName = model.SubjectName,
                        SumOpl = omInvoice.SumOpl,
                        UserId = SRDSession.GetCurrentUserId(),
                        SvidPolycNum = omInvoice.SvidPolycNum,
                        LinkInvoiceSvod = omInvoice.LinkInvoiceSvod.HasValue && newSvodIds.ContainsKey(omInvoice.LinkInvoiceSvod.Value) ? (long?)newSvodIds[omInvoice.LinkInvoiceSvod.Value] : null,
                        Sort = omInvoice.Sort,
                        DataZakluchenia = DateTime.Now,
                        InsurSymmaOtchet = omInvoice.InsurSymmaOtchet,
                        PartDog = omInvoice.PartDog,
                        SvdPolyceDate = omInvoice.SvdPolyceDate,
                        UserAgreeId = null,
                        UserAgree = null
                    }.Save();
                    SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменен счет", OMInvoice.GetRegisterId(), invoiceId);
                }

                ts.Complete();
            }

            return EmptyResponse();
        }

        public PartialViewResult Summaries(long? damageId, long? allPropertyId)
        {
            return PartialView(_invoiceService.GetSummaries(damageId, allPropertyId));
        }
        
        [HttpPost]
        [Consumes("application/json")]
        public ContentResult UpdateInvoiceSums([FromBody]UpdateInvoiceSumsDto updateInvoiceSums)
        {
            try
            {
                return JsonResponse(_invoiceService.UpdateInvoiceSums(updateInvoiceSums.InvoiceList));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ContentResult CalcUndestributedAmount([FromBody]CalcUndestributedAmountDto calcUndestributedAmountDto)
        {
            try
            {
                return JsonResponse(_invoiceService.CalcUndestributedAmount(calcUndestributedAmountDto?.PartTownSum, calcUndestributedAmountDto?.InvoiceSums));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }
    }
}