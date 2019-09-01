using CIPJS.DAL.DamageAnalysis;
using CIPJS.DAL.FileStorage;
using CIPJS.DAL.ReestrPay;
using CIPJS.Models.ReestrPay;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.SRD.DAL;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Models.CoreUi;
using Microsoft.AspNetCore.Http;
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
    public class ReestrPayController : BaseController
    {
        private readonly ReestrPayService _reestrPayService;
        private readonly DamageAnalysisService _damageAnalysisService;
        private readonly FileStorageService _fileStorageService;

        public ReestrPayController(ReestrPayService reestrPayService, 
            DamageAnalysisService damageAnalysisService,
            FileStorageService fileStorageService)
        {
            _reestrPayService = reestrPayService;
            _damageAnalysisService = damageAnalysisService;
            _fileStorageService = fileStorageService;
        }

        public ActionResult Form(ReestrPayType type)
        {
            List<long?> ids = RegistersVariables.CurrentList?.Select(x => (long?)x).ToList() ?? new List<long?>();

            ViewBag.ReestrPayType = type;

            try
            {
                return View(_reestrPayService.GetInvoices(ids, type));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View();
            }
        }

        [HttpGet]

        public ActionResult FormSvod(ReestrPayType type)
        {
            List<long?> ids = RegistersVariables.CurrentList?.Select(x => (long?)x).ToList() ?? new List<long?>();

            ViewBag.ReestrPayType = type;

            try
            {
                return View(_reestrPayService.GetInvoiceSvods(ids, type));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View();
            }
        }

        [HttpPost]
        public ActionResult Form([FromBody]FormDto model)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, false, true);

            if (model == null || model.Ids == null || !model.Ids.Any()) return ErrorResponse("На сервер пришла пустая модель");

            OMReestrPay reestrPay = _reestrPayService.Form(model.Ids, model.Type);

            if (reestrPay != null)
            {
                return JsonResponse(reestrPay);
            }
            else
            {
                return ErrorResponse("Не удалось сформировать реестр оплат");
            }
        }

        [HttpPost]
        public ActionResult FormSvod([FromBody]FormDto model)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, false, true);

            if (model == null || model.Ids == null || !model.Ids.Any()) return ErrorResponse("На сервер пришла пустая модель");

            OMReestrPay reestrPay = _reestrPayService.FormSvod(model.Ids, model.Type);

            if (reestrPay != null)
            {
                return JsonResponse(reestrPay);
            }
            else
            {
                return ErrorResponse("Не удалось сформировать реестр оплат");
            }
        }

        public ActionResult ChangeStatus(long id)
        {
            List<long> ids = RegistersVariables.CurrentList?.ToList() ?? new List<long>();

            if (!ids.Any()) ids.Add(id);

            List<OMReestrPay> omReestrPays = _reestrPayService.GetReestrPaysByIds(ids);

            if (!omReestrPays.Any())
            {
                ViewBag.ErrorMessage = "Ни одного реестра оплат не найдено";
                return View();
            }
            if (omReestrPays.GroupBy(x => x.Status_Code).Count() > 1)
            {
                ViewBag.ErrorMessage = "Вы выбрали реестры оплат в различных статусах - операция по изменению невозможна";
                return View();
            }
            if (omReestrPays.First().Status_Code == ReestrPayStatus.TransferredPayment)
            {
                ViewBag.ErrorMessage = "Изменить статус можно только для записей в статусе не равном Оплачен";
                return View();
            }

            ChangeStatusDto model = new ChangeStatusDto
            {
                Ids = ids,
                Date = omReestrPays.Count == 1 ? omReestrPays.First().Date : null,
                Status = omReestrPays.First().Status_Code == ReestrPayStatus.Formed ? ReestrPayStatus.TransferredDGI :
                    (omReestrPays.First().Status_Code == ReestrPayStatus.TransferredDGI ? ReestrPayStatus.ApprovedDGI : 
                    (omReestrPays.First().Status_Code == ReestrPayStatus.ApprovedDGI ? ReestrPayStatus.TransferredPayment : 
                    ReestrPayStatus.None))
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeStatus(ChangeStatusDto dto)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, false, true);
            _reestrPayService.ChangeStatus(dto);
            return EmptyResponse();
        }

        public ActionResult DownloadFileDGI(long id)
        {
            OMFileStorage omFileStorage = _reestrPayService.GetFileDGI(id);

            if (omFileStorage == null) throw new Exception("Файл не найден");

            return File(_fileStorageService.GetFileStream(omFileStorage), "application/octet-stream", omFileStorage.Filename);
        }

        public ActionResult DownloadFilePay(long id)
        {
            OMFileStorage omFileStorage = _reestrPayService.GetFilePay(id);

            if (omFileStorage == null) return ErrorResponse("Файл не найден");

            return File(_fileStorageService.GetFileStream(omFileStorage), "application/octet-stream", omFileStorage.Filename);
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(long? payId)
        {
            var model = new ModalDialogDetails();
            OMReestrPay reestrPay = null;
            if (payId.HasValue)
            {
                reestrPay = OMReestrPay.Where(x => x.EmpId == payId)
                    .SelectAll()
                    .Execute()
                    .FirstOrDefault();
            }

            if (reestrPay != null)
            {
                int invoicesCount = OMInvoice.Where(x => x.LinkReestrPay == payId).GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt();

                model.Message = $"Внимание! Вы подтверждаете, что ранее сформированный реестр № {reestrPay.Num} от {reestrPay.Date:dd.MM.yyyy} " +
                    $"в статусе «{reestrPay.Status_Code.GetEnumDescription()}» должен быть расформирован и все счета, в количестве {invoicesCount} шт. связанные с эти реестром, " +
                    "будут переведены с статус «Счет согласован»?";
            }
            else
            {
                model.Message = "Реестра оплат с переданным идентификатором не существует";
                model.Icon = ModalDialogDetails.IconType.Warning;
                model.Buttons = ModalDialogDetails.ButtonType.Ok;
            }

            return View("~/Views/Shared/ModalDialogDetails.cshtml", model);
        }

        [HttpPost]
        public ContentResult Delete(long? payId)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, false, true);

            if (payId.HasValue)
            {
                try
                {
                    _reestrPayService.Delete(payId.Value);
                }
                catch (Exception e)
                {
                    return Content(JsonConvert.SerializeObject(new
                    {
                        type = "Error",
                        message = e.Message
                    }), "application/json");
                }
            }

            return Content(JsonConvert.SerializeObject(new
            {
                type = "Success",
                message = "Расчет успешно расформирован",
                reload = true
            }), "application/json");
        }

        [HttpGet]
        [ActionName("DeletePaysReturn")]
        public ActionResult DeletePaysReturnGet(long? payId)
        {
            return DeleteConfirm(payId);
        }

        [HttpPost]
        public ContentResult DeletePaysReturn(long? payId)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, false, true);

            if (payId.HasValue)
            {
                try
                {
                    _reestrPayService.DeletePayReturn(payId.Value);
                }
                catch (Exception e)
                {
                    return Content(JsonConvert.SerializeObject(new
                    {
                        type = "Error",
                        message = e.Message
                    }), "application/json");
                }
            }

            return Content(JsonConvert.SerializeObject(new
            {
                type = "Success",
                message = "Расчет успешно расформирован",
                reload = true
            }), "application/json");
        }

        [HttpGet]
        [ActionName("ChangeDate")]
        public ActionResult ChangeDateConfirm(long payId)
        {
            OMReestrPay reestrPay = OMReestrPay.Where(x => x.EmpId == payId)
                    .SelectAll()
                    .Execute()
                    .FirstOrDefault();

            if (reestrPay == null)
            {
                throw new Exception($"Не удалось определить реестр оплат по идентификатору {payId}");
            }

            return View(new ChangeDateDto
            {
                PayId = reestrPay.EmpId,
                Date = reestrPay.Date
            });
        }

        [HttpPost]
        public ContentResult ChangeDate(long payId, DateTime? date)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, false, true);

            try
            {
                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
                {
                    _reestrPayService.ChangeDate(payId, date);

                    SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Изменена оплата в системе ОПС", OMReestrPay.GetRegisterId(), payId);

                    ts.Complete();

                    return EmptyResponse();
                }
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("DeleteList")]
        public ActionResult DeleteListConfirm()
        {
            ModalDialogDetails model = new ModalDialogDetails();
            List<long?> ids = RegistersVariables.CurrentList?.Select(x => (long?)x).ToList() ?? new List<long?>();

            if (ids.IsNotEmpty())
            {
                List<OMReestrPay> omReestrPays = OMReestrPay.Where(x => ids.Contains(x.EmpId))
                .Select(x => x.Invoice[0].EmpId).Execute();

                if (omReestrPays.Any(x => x.Invoice.Any()))
                {
                    model.Message = "Внимание! Невозможно удалить Реестр, в который включены счета";
                    model.Icon = ModalDialogDetails.IconType.Warning;
                    model.Buttons = ModalDialogDetails.ButtonType.Ok;
                }
                else
                {
                    model.Message = "Внимание! Проведена проверка, выбранные реестры не содержат счета, удаление возможно. Вы подтверждаете удаление?";
                }
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

            List<long?> ids = RegistersVariables.CurrentList?.Select(x => (long?)x).ToList() ?? new List<long?>();
            ModalDialogDetails model = new ModalDialogDetails();

            if (ids.IsNotEmpty())
            {
                try
                {
                    using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                    {
                        ids.ForEach(x =>
                        {
                            OMReestrPay.ODestroy((int)x.Value);

                            SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_ROPL, true, "Удалена оплата в системе ОПС", OMReestrPay.GetRegisterId(), (int)x.Value);
                        });
                        ts.Complete();
                    }
                }
                catch (Exception e)
                {
                    return Json(new
                    {
                        type = "Error",
                        message = e.Message
                    });
                }
            }

            return Json(new
            {
                type = "Success",
                message = "Счета успешно удалены",
                reload = true
            });
        }
    }
}