using CIPJS.DAL.Invoice;
using CIPJS.DAL.InvoiceSvod;
using CIPJS.DAL.ReestrPay;
using CIPJS.Models.Invoice;
using Core.Shared.Misc;
using Core.SRD;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Models.CoreUi;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace CIPJS.Controllers
{
    public class InvoiceSvodController : BaseController
    {
        private readonly InvoiceSvodService _invoiceSvodService;
        private readonly ReestrPayService _reestrPayService;
        private readonly InvoiceService _invoiceService;

        public InvoiceSvodController(InvoiceSvodService invoiceSvodService, ReestrPayService reestrPayService, InvoiceService invoiceService)
        {
            _invoiceSvodService = invoiceSvodService;
            _reestrPayService = reestrPayService;
            _invoiceService = invoiceService;
        }

        public ActionResult Agreed(long invoiceSvodId, ReestrPayType type)
        {
            ViewBag.ReestrPayType = type;

            try
            {
                string errorMessage = string.Empty;

                List<OMInvoice> invoiceList = _invoiceSvodService.GetInvoices(invoiceSvodId);

                if (invoiceList.Where(x => x.Status_Code != InvoiceStatus.Formed).Any())
                    errorMessage = "Внимание! Согласовать можно только счета, находящиеся в статусе \"Создан\"!";
                else if (invoiceList.Where(x => !x.SumOpl.HasValue || x.SumOpl == 0).Any())
                    errorMessage = "Внимание! Согласовать можно только счета, сумма которых не ноль и не пусто!";

                // CIPJS-917: Не отражается информация по договору при согласовании счетов на выплату части премии по ОИ
                if (type == ReestrPayType.ReturnBonusOI)
                {
                    ViewBag.InvoiceSvod = OMInvoiceSvod.Where(x => x.EmpId == invoiceSvodId).SelectAll().Execute();
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ViewBag.ErrorMessage = errorMessage;
                    return View("~/Views/Invoice/AgreedList.cshtml");
                }
                else
                    return View("~/Views/Invoice/AgreedList.cshtml", invoiceList);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Invoice/AgreedList.cshtml");
            }
        }

        public ActionResult FormReestrPay(long invoiceSvodId, ReestrPayType type)
        {
            List<long?> invoiceIds = _invoiceSvodService.GetInvoices(invoiceSvodId).Select(x => (long?)x.EmpId).ToList();

            ViewBag.ReestrPayType = type;

            try
            {
                return View("~/Views/ReestrPay/Form.cshtml", _reestrPayService.GetInvoices(invoiceIds, type));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("~/Views/ReestrPay/Form.cshtml");
            }
        }

        public ActionResult ChangeStatusToError(long invoiceSvodId)
        {
            try
            {
                List<OMInvoice> omInvoices = _invoiceSvodService.GetInvoices(invoiceSvodId);

                return View("~/Views/Invoice/ChangeStatusToError.cshtml", new ChangeStatusToErrorDto
                {
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

                return View("~/Views/Invoice/ChangeStatusToError.cshtml");
            }
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(long invoiceSvodId)
        {
            var model = new ModalDialogDetails();
                var errorMessage = string.Empty;

            OMInvoiceSvod svod = OMInvoiceSvod.Where(x => x.EmpId == invoiceSvodId).Select(x => x.Status_Code).ExecuteFirstOrDefault();

            if (svod == null)
            {
                throw new Exception($"Не удалось определить сводный реестр счетов по идентификатору {invoiceSvodId}");
            }

            if (svod.Status_Code != InvoiceStatus.Formed && svod.Status_Code != InvoiceStatus.ErrorInDetails)
            {
                model.Message = "Внимание! Удалить можно только счета, находящиеся в статусах \"Создан\" или \"Ошибка в реквизитах\"!";
                model.Icon = ModalDialogDetails.IconType.Warning;
                model.Buttons = ModalDialogDetails.ButtonType.Ok;
            }
            else
                model.Message = "Вы уверены, что хотите удалить сводный реестр счетов?";

            return View("~/Views/Shared/ModalDialogDetails.cshtml", model);
        }

        [HttpPost]
        public ActionResult Delete(long invoiceSvodId)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, false, true);
            
            try
            {
                OMInvoiceSvod svod = OMInvoiceSvod.Where(x => x.EmpId == invoiceSvodId).Select(x => x.Status_Code).ExecuteFirstOrDefault();

                if (svod == null)
                {
                    throw new Exception($"Не удалось определить сводный реестр счетов по идентификатору {invoiceSvodId}");
                }

                using(TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    List<long> invoiceIds = _invoiceSvodService.GetInvoices(invoiceSvodId).Select(x => x.EmpId).ToList();

                    _invoiceService.Delete(invoiceIds);

                    svod.Destroy();

                    ts.Complete();
                }

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
    }
}
