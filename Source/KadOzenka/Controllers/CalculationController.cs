using CIPJS.DAL.Calculation;
using CIPJS.DAL.Dictionaries;
using CIPJS.Models.Calculation;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.SRD.DAL;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.Models.CoreUi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.UI.Registers.CoreUI.Registers;

namespace CIPJS.Controllers
{
    [Authorize]
    public class CalculationController : BaseController
    {
        private readonly CalculationService _calculationService = new CalculationService();

        public bool ReadOnly
        {
            get
            {
                if (Request.Query.ContainsKey("ReadOnly"))
                {
                    var readOnly = Request.Query["ReadOnly"].FirstOrDefault().ParseToBoolean();
                    if (!readOnly)
                    {
                        readOnly = !(SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_DAMAGE_WRITE));
                    }
                    return readOnly;
                }

                return true;
            }
        }

        [HttpGet]
        public ActionResult ComonData(long? calculationId)
        {
            CalculationDetails model = null;
            if (calculationId.HasValue)
            {
                OMParamCalculation calculation = _calculationService.GetById(calculationId);
                model = CalculationDetails.OMMap(calculation);
                model.IsReadOnly = ReadOnly;
            }
            else
            {
                System.Collections.Generic.Dictionary<long, decimal> baseTariff = DictionaryService.GetBaseTariff();
                model = new CalculationDetails()
                {
                    Id = -1,
                    CreatedUserId = SRDSession.Current.UserID,
                    CreatedUserName = SRDSession.Current.User.FullName,
                    PartCompensation = DictionaryService.GetPartCompensationIncuranceCompany(DateTime.Now),
                    BasicRate1 = baseTariff[1],
                    BasicRate2 = baseTariff[2],
                    BasicRate3 = baseTariff[3],
                    CoefActualCost = DictionaryService.GetActualCostRatio(DateTime.Now),
                    AgreementId = -1,
                    RequestDate = DateTime.Now,
                    FlagOkrugl = true
                };
            }

            return View(model);
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm()
        {
            var model = new ModalDialogDetails();
            if (RegistersVariables.CurrentList != null && RegistersVariables.CurrentList.Count > 0)
            {
                List<long> ids = RegistersVariables.CurrentList.ToList();
                List<OMParamCalculation> calculations = OMParamCalculation
                    .Where(l => ids.Contains(l.EmpId))
                    .Select(x => x.PackageNum)
                    .Execute();
            
                if (calculations.Count > 0)
                {
                    model.Message = $"Вы действительно хотите удалить расчеты ({calculations.Count}): {string.Join(", ", calculations.Select(x => x.PackageNum))}?";
                }
                else
                {
                    model.Message = "Такого расчета не существует";
                    model.Icon = ModalDialogDetails.IconType.Warning;
                    model.Buttons = ModalDialogDetails.ButtonType.Ok;
                }
            }
            else
            {
                model.Message = "Не отмечены расчеты для удаления";
                model.Icon = ModalDialogDetails.IconType.Warning;
                model.Buttons = ModalDialogDetails.ButtonType.Ok;
            }
            return View("~/Views/Shared/ModalDialogDetails.cshtml", model);
        }

        [HttpPost]
        public ActionResult Delete()
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_CALC_DELETE, true, false, true);

            List<long> ids = RegistersVariables.CurrentList != null && RegistersVariables.CurrentList.Count > 0
                ? RegistersVariables.CurrentList.ToList()
                : throw new Exception("Не отмечены расчеты для удаления");
            try
            {
                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
                {
                    foreach (long id in ids)
                    {
                        _calculationService.DeleteCalculation(id);
                        SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_CALC_DELETE, true, "Расчет удален", OMParamCalculation.GetRegisterId(), id);
                    }
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

            return Json(new
            {
                type = "Success",
                message = "Расчеты успешно удалены",
                reload = true
            });
        }

        [HttpPost]
        public ContentResult Edit(CalculationDetails data)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_CALC_WRITE, true, false, true);

            if (!ModelState.IsValid)
            {
                return JsonResponse(new
                {
                    Errors = ModelState.Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new
                        {
                            Control = x.Key,
                            Message = string.Join("\n", x.Value.Errors.Select(e => e.ErrorMessage))
                        })
                });
            }
            else
            {
                bool isNew = false;

                OMParamCalculation model = CalculationDetails.OMMap(data);

                if (!this._calculationService.CanUpdate(model, out string error))
                    return ErrorResponse(error);
                
                if (model.EmpId == -1)
                {
                    isNew = true;
                    model.Status_Code = CalculationStatus.Created;
                    model.CreatedDate = DateTime.Now;
                    model.AgreementId1 = SRDSession.GetCurrentUserId();
                    model.DateFill1 = DateTime.Now;
                }

                //var oldParamInfo = OMParamCalculation.Where(x => x.EmpId == data.Id).SelectAll().Execute().FirstOrDefault();
                //if (oldParamInfo != null)
                //{
                //    if (data.ApprovalDate != oldParamInfo.ApprovalDate)
                //    {
                //        var userId = SRDSession.GetCurrentUserId();
                //        if (data.ApprovalUserId != userId)
                //        {
                //            model.ApprovalUserId = userId;
                //            model.Status_Code = CalculationStatus.Agreed;
                //        }
                //    }
                //}
                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    long id = _calculationService.Save(model);
                    SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_CALC, true, isNew ? "Создан расчет параметров объекта общего имущества" : "Изменен расчет параметров объекта общего имущества", OMParamCalculation.GetRegisterId(), model.EmpId);

                    //Сохранение проекта договора
                    OMAgreementProject agreementProjectModel = new OMAgreementProject
                    {
                        EmpId = data.AgreementId,
                        CalculationId = id,
                        GotDate = data.AgreementGotDate,
                        GotUserId = data.AgreementGotUserId,
                        ApprovalDate = data.AgreementApprovalDate,
                        ApprovalUserId = data.AgreementApprovalUserId,
                        Note = data.AgreementNote,
                        CommentSpravka = data.AgreementCommentSpravka,
                        ResumeSpravka = data.AgreementResumeSpravka,
                        PartMoscow = data.AgreementPartMoscow,
                        ProgectNum = data.ProgectNum
                    };
                    long agreementProjectId = agreementProjectModel.Save();
                    SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_CALC, true, "Создан проект договора страхования", OMAgreementProject.GetRegisterId(), agreementProjectId);

                    //CIPJS-153 Все что доступно в разделе "МКД" для изменения должно сохраняться при сохранении расчета в INSUR_BUILDING
                    if (model.ObjId.HasValue)
                    {
                        OMBuilding building = OMBuilding.Where(x => x.EmpId == model.ObjId.Value)
                            .Select(x => x.Lfpq)
                            .Select(x => x.Lfgpq)
                            .Select(x => x.OkrugId)
                            .Execute()
                            .FirstOrDefault();

                        if (building != null)
                        {
                            building.Lfpq = data.PassengerElevators;
                            building.Lfgpq = data.CargoElevators;
                            building.Epl = data.Epl;
                            building.Pizn = data.Pizn;
                            building.Save();
                            SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_CALC, true, "Создан объект страхования МКД", OMBuilding.GetRegisterId(), building.EmpId);
                        }

                        // CIPJS-859 Доработать создание нового расчета ( пробивать сразу okrug_id)
                        model.OkrugId = building?.OkrugId;
                        model.Save();
                    }

                    if (data.Id == 0 || data.Id == -1)
                    {
                        List<OMDocBaseType> docType = null;
                        string[] docs = new string[]
                        {
                        "Справка о лифтах",
                        "Технический паспорт",
                        "Проект полиса",
                        "Заявление о страховании"
                        };
                        docType = OMDocBaseType.Where(x => docs.Contains(x.DocumentBase) && x.Type == "ОИ").Select(x => x.DocumentBase).OrderBy(x => x.DocumentBase).Execute();
                        if (docType != null)
                        {
                            foreach (OMDocBaseType elem in docType)
                            {
                                OMDocuments doc = OMDocuments.CreateEmpty();
                                doc.ObjId = id;
                                doc.ReestrId = OMParamCalculation.GetRegisterId();
                                doc.DocTypeId = elem.Id;
                                doc.DocIsHave = false;
                                doc.DocTypeM_Code = TypeDocBaseCase.None;
                                doc.Save();
                            }

                        }
                    }

                    ts.Complete();

                    ModelState.Clear();

                    return JsonResponse(new { id, agreementProjectId });
                }
            }
        }

        [HttpPost]
        public IActionResult CheckEstimatedInsuranceStartDate(long? id, DateTime? date)
        {
            if(id == null || date == null)
            {
                return BadRequest();
            }
            OMParamCalculation calculation = _calculationService.GetById(id);
            var allProperties = OMAllProperty.Where(x => x.ObjId == calculation.ObjId && x.Ndogdat != null).Select(x => x.Ndogdat).Execute();
            bool exist = allProperties.Exists(x => x.Ndogdat.Value.AddYears(1).AddDays(-1) >= date);
            return Ok(exist);
        }

        [HttpPost]
        public ContentResult Agreed(long calculationId)
        {
            try
            {
                return JsonResponse(_calculationService.Agreed(calculationId));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult ProjectReceived(long agreementId)
        {
            try
            {
                return JsonResponse(_calculationService.ProjectReceived(agreementId));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult ProjectAgreed(long agreementId)
        {
            try
            {
                return JsonResponse(_calculationService.ProjectAgreed(agreementId));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        public ActionResult Print(long id)
        {
            return Content("Нет Данных");
        }

        public ActionResult AgreementProjectCategoriesList(long? calculationId)
        {
            List<AgreementProjectCategory> agreementProjectCategoryList = new List<AgreementProjectCategory>();
            Dictionary<long, decimal> baseTariff = DictionaryService.GetBaseTariff();
            if (calculationId > 0)
            {
                OMParamCalculation calculation = _calculationService.GetById(calculationId);

                if (calculation != null)
                {
                    OMAgreementProject project = _calculationService.GetAgreementProjectsByCalculationId(calculation.EmpId);

                    agreementProjectCategoryList.Add(new AgreementProjectCategory
                    {
                        Type = 1,
                        Name = "Конструктивные элементы и помещения общего пользования",
                        TotalCost = calculation.TotalCost1,
                        DesignCost = calculation.DesignCost1,
                        Excluded = !(project?.Kat1 ?? false),
                        Baserate = baseTariff[1],
                        Annualprem = calculation.AnnualBonus1
                    });

                    agreementProjectCategoryList.Add(new AgreementProjectCategory
                    {
                        Type = 2,
                        Name = "Внеквартирное инженерное оборудование",
                        TotalCost = calculation.TotalCost2,
                        DesignCost = calculation.DesignCost2,
                        Excluded = !(project?.Kat2 ?? false),
                        Baserate = baseTariff[2],
                        Annualprem = calculation.AnnualBonus2
                    });

                    agreementProjectCategoryList.Add(new AgreementProjectCategory
                    {
                        Type = 3,
                        Name = "Лифтовое оборудование",
                        TotalCost = calculation.TotalCost3,
                        DesignCost = calculation.DesignCost3,
                        Excluded = !(project?.Kat3 ?? false),
                        Baserate = baseTariff[3],
                        Annualprem = calculation.AnnualBonus3
                    });

                    decimal? sizeBonusMkd = null;

                    if (project != null)
                    {
                        if (project.SizeBonusMkd.HasValue)
                        {
                            sizeBonusMkd = project.SizeBonusMkd.Value;
                        }
                        else
                        {
                            if (calculation.AnnualBonus1.HasValue && project.Kat1.HasValue && project.Kat1.Value)
                            {
                                sizeBonusMkd = (sizeBonusMkd ?? 0) + calculation.AnnualBonus1.Value;
                            }
                            if (calculation.AnnualBonus2.HasValue && project.Kat2.HasValue && project.Kat2.Value)
                            {
                                sizeBonusMkd = (sizeBonusMkd ?? 0) + calculation.AnnualBonus2.Value;
                            }
                            if (calculation.AnnualBonus3.HasValue && project.Kat3.HasValue && project.Kat3.Value)
                            {
                                sizeBonusMkd = (sizeBonusMkd ?? 0) + calculation.AnnualBonus3.Value;
                            }
                        }
                    }

                    agreementProjectCategoryList.Add(new AgreementProjectCategory
                    {
                        Type = 0,
                        Name = "Годовая премия по дому в целом",
                        Annualprem = sizeBonusMkd.HasValue ? (decimal?)Math.Round(sizeBonusMkd.Value, 2, MidpointRounding.AwayFromZero) : null
                    });
                }
            }

            return Content(JsonConvert.SerializeObject(agreementProjectCategoryList), "application/json");
        }

        public ContentResult UpdateAgreementProjectCategory(long agreementId, int type, bool include)
        {
            try
            {
                _calculationService.UpdateAgreementProjectCategory(agreementId, type, include);

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult UpdateAgreementSizeBonusMkd(long agreementId, decimal? sizeBonusMkd)
        {
            try
            {
                _calculationService.UpdateAgreementSizeBonusMkd(agreementId, sizeBonusMkd);

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult LinkToContract(long? id)
        {
            CalculationContractLinkDto model = _calculationService.GetLink(id);
            return View(model);
        }

        [HttpPost]
        public void LinkToContract(string jsonLink)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_CALC_WRITE, true, false, true);

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                CalculationContractLinkDto model = JsonConvert.DeserializeObject<CalculationContractLinkDto>(jsonLink, new JsonSerializerSettings
                {
                    Culture = new System.Globalization.CultureInfo("ru-RU"),
                    DateFormatString = "dd.MM.yyyy HH:mm:ss"
                });
                _calculationService.SaveLink(model);

                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_COMMPROP_CALC_WRITE, true, $"Для расчета задана связь с договором(id={model.ContractId})", OMParamCalculation.GetRegisterId(), model.CalculationId);

                ts.Complete();
            }
        }

        [HttpPost]
        public ActionResult GetNumber(long objId)
        {
            return JsonResponse(_calculationService.GetNumber(objId));
        }

        [HttpPost]
        public IActionResult CheckProgectNum(string number, long? id)
        {
            number = number.Trim();

            if (number.IsNullOrEmpty()) return EmptyResponse();

            List<OMAgreementProject> omAgreementProjects = OMAgreementProject.Where(x => x.ProgectNum == number).Select(x => x.CalculationId).Execute();

            if (omAgreementProjects.Any(x => x.CalculationId != id)) return JsonResponse(1);

            return EmptyResponse();
        }

        public ActionResult RequestNumberAutocomplete(string searchString)
        {
            return Json(_calculationService.RequestNumberAutocomplete(searchString));
        }

        [HttpPost]
        [Consumes("application/json")]
        public ContentResult Calculate([FromBody]CalculationValuesInDto calculationValuesInDto)
        {
            try
            {
                return JsonResponse(_calculationService.Calculate(calculationValuesInDto));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }
    }
}