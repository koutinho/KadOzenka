using CIPJS.DAL.DamageAnalysis;
using CIPJS.Models.DamageAnalysis;
using CIPJS.Models.Tenements;
using Core.ErrorManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.SRD.DAL;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace CIPJS.Controllers
{
    public class DamageAnalysisController : BaseController
    {
        private readonly DamageAnalysisService service = new DamageAnalysisService();

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
        public IActionResult DamageAnalysisCard(long? id, string type)
        {
            ViewBag.UniqueSessionKey = HttpContextHelper.HttpContext.Request.Query["UniqueSessionKey"];
            return View(service.GetDamageAnalysisCardDto(id, GetContractType(type), ReadOnly));
        }

        [HttpPost]
        public JsonResult DamageAnalysisCard(string jsonModel,
            bool generateNewNumber = false)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_DAMAGE, true, false, true);

            if (!ModelState.IsValid)
            {
                return Json(new
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
                DamageAnalysisCardDto model = JsonConvert.DeserializeObject<DamageAnalysisCardDto>(jsonModel, new JsonSerializerSettings
                {
                    Culture = new System.Globalization.CultureInfo("ru-RU"),
                    DateFormatString = "dd.MM.yyyy HH:mm:ss",
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });
                if (model.UserId == 0)
                    model.UserId = SRDSession.Current.User.ID;

                ModelState.Clear();

                DamageAnalysisCardDto result = service.SaveDamageAnalysisCard(model, generateNewNumber);

                return Json(result);
            }
        }

        /// <summary>
        /// Поиск дубликата по дате сс и адресу.
        /// </summary>
        /// <param name="damageId"></param>
        /// <param name="damageData"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        public bool CheckDamageCardDublicate(string request)
        {
            DamageAnalysisCardDto model = JsonConvert.DeserializeObject<DamageAnalysisCardDto>(request, new JsonSerializerSettings
            {
                Culture = new System.Globalization.CultureInfo("ru-RU"),
                DateFormatString = "dd.MM.yyyy HH:mm:ss",
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });

            var response = service.CheckDublicate(model);
            return response;
        }

        [HttpGet]
        public IActionResult EditCard(DamageAnalysisCardDto model)
        {
            model = service.GetDamageAnalysisCardDto(model.Id, model.TypeCode, false);
            return View("~/Views/DamageAnalysis/DamageAnalysisCard.cshtml", model);
        }

        [HttpGet]
        public ContentResult Get(long? objId, ContractType type, DateTime? damageDate,
            CausesOfDamageGP? causesOfDamageGp, CausesOfDamageOI? causesOfDamageOI)
        {
            try
            {
                if (!objId.HasValue)
                {
                    throw new Exception("Не передан обязательный параметр идентификатор объекта");
                }

                return JsonResponse(service.Get(objId.Value, type, damageDate, causesOfDamageGp, causesOfDamageOI));
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GetObjInfoByObjId(long? id, long? objId, ContractType type, DateTime? damageDate, decimal? partInsur, decimal? partTown,
            DateTime? nomDate, CausesOfDamageGP? causesOfDamageGp, CausesOfDamageOI? causesOfDamageOI)
        {
            DamageAnalysisCardDto model = new DamageAnalysisCardDto
            {
                Id = id,
                NomDate = nomDate,
                ObjId = objId,
                TypeCode = type,
                DamageData = damageDate,
                PartInsur = partInsur,
                PartTown = partTown,
                CausesOfDamageGP = causesOfDamageGp.HasValue ? causesOfDamageGp.Value : CausesOfDamageGP.None,
                CausesOfDamageOI = causesOfDamageOI.HasValue ? causesOfDamageOI.Value : CausesOfDamageOI.None
            };
            model = service.GetObjInfo(model);
            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        [HttpPost]
        public void CheckedDamageAnalysis(long? id)
        {
            service.CheckedDamageAnalysis(id);
        }

        [HttpPost]
        public IActionResult CheckObjectAndDate([FromBody]CheckObjectAndDateDto model)
        {
            long reestrId = model.Type == ContractType.CommonOwnership ? OMBuilding.GetRegisterId() : OMFlat.GetRegisterId();
            QSQuery<OMDamage> qsQuery = OMDamage.Where(x => x.ObjId == model.ObjId && x.DamageData == model.Date && x.ObjReestrId == reestrId);

            if (model.Id.HasValue) qsQuery = qsQuery.And(x => x.EmpId != model.Id.Value);

            OMDamage omDamage = qsQuery.ExecuteFirstOrDefault();

            if (omDamage != null) return JsonResponse(1);

            return EmptyResponse();
        }

        public IActionResult OtherDamages_Read(CheckObjectAndDateDto model)
        {
            long reestrId = model.Type == ContractType.CommonOwnership ? OMBuilding.GetRegisterId() : OMFlat.GetRegisterId();
            QSQuery<OMDamage> qsQuery = OMDamage.Where(x => x.ObjId == model.ObjId && x.ObjReestrId == reestrId);

            if (model.Id.HasValue) qsQuery = qsQuery.And(x => x.EmpId != model.Id.Value);

            qsQuery = qsQuery
                .Select(x => x.NomDoc)
                .Select(x => x.NomDate)
                .Select(x => x.ParentInsuranceOrganization.FullName)
                .Select(x => x.DamageData)
                .Select(x => x.SumDamage)
                .Select(x => x.DamageReasonGP);

            List<LivingSpaceDamageDto> models = qsQuery.Execute().Select(x => LivingSpaceDamageDto.Map(x)).OrderByDescending(x => x.CaseDate).ToList();

            return Content(JsonConvert.SerializeObject(models), "application/json");
        }

        public IActionResult OtherDamagesDwelling_Read(CheckObjectAndDateDto model)
        {
            QSQuery q = new QSQuery(OMDamage.GetRegisterId())
            {
                Columns = new List<QSColumn>
                {
                    OMDamage.GetColumn(x => x.EmpId, "DamageId"),
                    OMDamage.GetColumn(x => x.DamageData, "DamageDate"),
                    OMDamage.GetColumn(x => x.SumDamage, "DamageAmount"),
                    OMDamage.GetColumn(x => x.DamageReasonGP, "DamageReason"),
                    OMDamage.GetColumn(x => x.NomDoc, "CaseNumber"),
                    OMInvoice.GetColumn(x => x.DataZakluchenia, "DataZakluchenia"),
                    OMInvoice.GetColumn(x => x.SubjectName, "SubjectName"),
                    OMAllProperty.GetColumn(x => x.Ndog, "Ndog"),
                    OMInsuranceOrganization.GetColumn(x => x.ShortName, "InsuranceCompany"),
                    OMFsp.GetColumn(x => x.Kodpl, "Kodpl")
                },
                ManualJoin = true,
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        JoinType = QSJoinType.Inner,
                        RegisterId = OMInsuranceOrganization.GetRegisterId(),
                        JoinCondition = new QSConditionSimple(
                            OMDamage.GetColumn(x => x.InsurOrgId),
                            QSConditionType.Equal,
                            OMInsuranceOrganization.GetColumn(x => x.Id))
                    },
                    new QSJoin
                    {
                        JoinType = QSJoinType.Inner,
                        RegisterId = OMInvoice.GetRegisterId(),
                        JoinCondition = new QSConditionSimple(
                            OMDamage.GetColumn(x => x.EmpId),
                            QSConditionType.Equal,
                            OMInvoice.GetColumn(x => x.LinkDamage))
                    },
                    new QSJoin
                    {
                        JoinType = QSJoinType.Inner,
                        RegisterId = OMFsp.GetRegisterId(),
                        JoinCondition = new QSConditionSimple(
                            OMInvoice.GetColumn(x => x.LinkFsp),
                            QSConditionType.Equal,
                            OMFsp.GetColumn(x => x.EmpId))
                    },
                    new QSJoin
                    {
                        JoinType = QSJoinType.Left,
                        RegisterId = OMAllProperty.GetRegisterId(),
                        JoinCondition = new QSConditionSimple(
                            OMInvoice.GetColumn(x => x.LinkAllProperty),
                            QSConditionType.Equal,
                            OMAllProperty.GetColumn(x => x.EmpId))
                    }
                },
                Condition = new QSConditionGroup
                {
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMDamage.GetColumn(x => x.ObjId), QSConditionType.Equal, new QSColumnConstant(model.ObjId)),
                        new QSConditionSimple(OMDamage.GetColumn(x => x.ObjReestrId), QSConditionType.Equal, new QSColumnConstant(OMFlat.GetRegisterId())),
                        new QSConditionSimple(OMFsp.GetColumn(x => x.SpecialActual), QSConditionType.Equal, new QSColumnConstant(1)),
                        new QSConditionSimple(OMDamage.GetColumn(x => x.EmpId), QSConditionType.NotEqual, new QSColumnConstant(model.Id.GetValueOrDefault()))
                    }
                },
                OrderBy = new List<QSOrder>
                {
                    new QSOrder
                    {
                        Order = QSOrderType.DESC,
                        Column = OMDamage.GetColumn(x => x.DamageData)
                    }
                }

            };
            return Content(JsonConvert.SerializeObject(q.ExecuteQuery()), "application/json");
        }

        [HttpPost]
        public void AgreedDamageAnalysis(long? id, bool isAgreeInvoices)
        {
            service.AgreedDamageAnalysis(id, isAgreeInvoices);
        }

        [HttpPost]
        public void SendToCheckDamageAnalysis(long? id)
        {
            service.SendToCheckDamageAnalysis(id);
        }

        /// <summary>
        /// Получить количество дел для расчета суммы ущерба по идентификатору объекта
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>список дел</returns>
        [HttpPost]
        public int Count(long? id)
        {
            return service.Count(id);
        }

        /// <summary>
        /// Получить ID дел по счету для формирования Заключения по идентификатору объекта
        /// </summary>
        /// <returns>список дел</returns>
        [HttpGet]
        public ActionResult CCCReport(long? reportTypeId, long? reportObjectId, long? reportRegisterId, string uniqueSessionKey)
        {
            //устанавливаем ключ сессии
            SetUniqueSessionKey(uniqueSessionKey);
            RegistersVariables.CurrentRegisterViewConfig = RegisterCommonConfiguration.GetRegisterViewConfiguration(reportObjectId.ToString());

            if (reportObjectId.HasValue)
            {
                List<long> currentList = service.GetInvoiceIds(reportObjectId.Value);
				
				if (currentList.Count == 0)
                    currentList.Add(0);

                RegistersVariables.CurrentList = new HashSet<long>(currentList);
                return RedirectToAction("Viewer", "Report", new
                {
                    ReportTypeId = reportTypeId,
                    ReportObjectId = reportObjectId,
                    ReportRegisterId = OMInvoice.GetRegisterId(),
                    UniqueSessionKey = uniqueSessionKey
                });
            }

            return EmptyResponse();
        }

        /// <summary>
        /// Получить ID дел по счету для формирования Заключения по идентификатору объекта
        /// </summary>
        /// <returns>список дел</returns>
        [HttpGet]
        public ActionResult CCCSavedReport(long? objectId, string uniqueSessionKey, bool canEdit = true)
        {
            //устанавливаем ключ сессии
            SetUniqueSessionKey(uniqueSessionKey);

            if (objectId.HasValue)
            {
                OMDamage damage = OMDamage
                    .Where(x => x.EmpId == objectId.Value)
                    .Select(x => x.TypeDoc)
                    .Select(x => x.TypeDoc_Code)
                    .ExecuteFirstOrDefault();

                if (damage == null)
                    return EmptyResponse();

                List<long> currentList = this.service.GetInvoiceIds(damage.EmpId);

                if (currentList.Count == 0)
                    currentList.Add(0);

                return RedirectToAction("Index", "RegistersView", new
                {
                    RegisterId = "FinanceSavedReport",
                    Transition = "1",
                    Pageable = "false",
                    Scrollable = "false",
                    ReportRegisterId = OMInvoice.GetRegisterId(),
                    ReportObjectId = objectId.Value,
                    qs = $"(80900600equal'{OMInvoice.GetRegisterId()}'and80900700in'{string.Join(',', currentList)}')",
                    UniqueSessionKey = uniqueSessionKey,
                    DamageAnalysisType = damage.TypeDoc_Code,
                    partialview = 1,
                    CanEdit = canEdit
                });
            }

            return EmptyResponse();
        }


        /// <summary>
        /// CIPJS-852: Добавить новую кнопку на карточку "Расчет ущерба" - возврат дела в работу после согласования
        /// Смена статуса счетов связанных с делом на "Создано"
        /// CIPJS-864: Доработка по задаче CIPJS-852
        /// </summary>
        [HttpGet]
        public IActionResult SetDamageInvoiceCreatedStatus(long? damageId)
        {
            if (damageId.HasValue)
            {
                var invoiceList = OMInvoice.Where(x => x.LinkDamage == damageId).Execute();
              
                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    foreach(var invoice in invoiceList)
                    {
                        invoice.Status_Code = InvoiceStatus.Formed;
                        invoice.UserAgreeId = null;
                        invoice.DateAgree = null;
                        invoice.NoteNoPayId = null;
                        invoice.Save();
                    }

                    var damage = OMDamage.Where(x => x.EmpId == damageId).ExecuteFirstOrDefault();
                    if (damage == null)
                        throw new Exception($"Дело {damageId} не найдено!");
                    // Очистить кто и когда передал на проверку дело.
                    damage.ControlUserId = null;
                    damage.DateControl = null;
                    // Очистить кто и когда проверил дело.
                    damage.AgreementId2 = null;
                    damage.DateFill2 = null;
                    // Очистить кто и когда согласовал дело.
                    damage.MainAgreementId = null;
                    damage.DateFillMain = null;
                    damage.Save();

                    ts.Complete();
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Проверка статуса расчета дела
        /// </summary>
        /// <param name="damageId"></param>
        [HttpPost]
        public JsonResult CheckStatus(long? damageId)
        {
            return Json(service.CheckStatus(damageId));
        }

        /// <summary>
        /// Получение списка подпричин для причины
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSubreasonOfDamage(CausesOfDamageGP damage, SubReasonCausesOfDamage subreason)
        {
            List<SubReasonCausesOfDamage> list = service.GetSubreasonOfDamage(damage);
            var selectedList = ComboBoxHelper.GetSelectList(typeof(SubReasonCausesOfDamage), filterValues: list.Select(x => (long)x).ToArray());
            if (list.Contains(subreason))
            {
                selectedList.FirstOrDefault(x => x.Value == ((long)subreason).ToString()).Selected = true;
            }
            else
            {
                selectedList.FirstOrDefault(x => x.Value == "0").Selected = true;
            }
            SelectListItem noneItem = selectedList.FirstOrDefault(x => x.Text == "Значение отсутствует");
            if (noneItem != null) noneItem.Text = "-";
            return Json(selectedList);
        }

        /// <summary>
        /// Получение списка уточнений подпричины
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRefinementSubreason(CausesOfDamageGP damage, SubReasonCausesOfDamage subreason, RefinementSubReasonCOD refinement)
        {
            List<RefinementSubReasonCOD> list = service.GetRefinementSubreason(damage, subreason);
            var selectedList = ComboBoxHelper.GetSelectList(typeof(RefinementSubReasonCOD), filterValues: list.Select(x => (long)x).ToArray());
            if (list.Contains(refinement))
            {
                selectedList.FirstOrDefault(x => x.Value == ((long)refinement).ToString()).Selected = true;
            }
            else
            {
                selectedList.FirstOrDefault(x => x.Value == "0").Selected = true;
            }
            SelectListItem noneItem = selectedList.FirstOrDefault(x => x.Text == "Значение отсутствует");
            if (noneItem != null) noneItem.Text = "-";
            return Json(selectedList);
        }

        /// <summary>
        /// Получение списка 
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFloors(TypeBuildingStructure buildingStructure, TypeFloors floors)
        {
            List<TypeFloors> list = service.GetFloors(buildingStructure);
            var selectedList = ComboBoxHelper.GetSelectList(typeof(TypeFloors), filterValues: list.Select(x => (long)x).ToArray());
            //CIPJS-501 Над таблицей "Расчет ущерба" требуется когда выбрано "Кирпичное здание" автоматом ничего не подкидывать в след строку " Этажность здания".
            //При этом когда пользователь открывает "Этажность здания" последние две строки выбора разместить в самом верху"
            if (buildingStructure == TypeBuildingStructure.Brick)
            {
                int emptyItemIndex = selectedList.FindIndex(x => x.Value == TypeFloors.None.ToString("D"));
                int buildingVariableReinforcedIndex = selectedList.FindIndex(x => x.Value == TypeFloors.BuildingsVariableReinforced.ToString("D"));
                int buildingsVariableWoodIndex = selectedList.FindIndex(x => x.Value == TypeFloors.BuildingsVariableWood.ToString("D"));

                if (buildingsVariableWoodIndex > -1)
                {
                    SelectListItem buildingsVariableWoodItem = selectedList.First(x => x.Value == TypeFloors.BuildingsVariableWood.ToString("D"));
                    selectedList.Remove(buildingsVariableWoodItem);
                    selectedList.Insert(emptyItemIndex > -1 ? emptyItemIndex + 1 : 0, buildingsVariableWoodItem);
                }

                if (buildingVariableReinforcedIndex > -1)
                {
                    SelectListItem buildingsVariableReinforcedItem = selectedList.First(x => x.Value == TypeFloors.BuildingsVariableReinforced.ToString("D"));
                    selectedList.Remove(buildingsVariableReinforcedItem);
                    selectedList.Insert(emptyItemIndex > -1 ? emptyItemIndex + 1 : 0, buildingsVariableReinforcedItem);
                }
            }

            if (floors == TypeFloors.None && buildingStructure != TypeBuildingStructure.Brick && list.Contains(TypeFloors.BuildingsVariableReinforced))
            {
                selectedList.FirstOrDefault(x => x.Value == ((long)TypeFloors.BuildingsVariableReinforced).ToString()).Selected = true;
            }
            else if (list.Contains(floors))
            {
                selectedList.FirstOrDefault(x => x.Value == ((long)floors).ToString()).Selected = true;
            }
            else if (list.Contains(TypeFloors.BuildingsVariableReinforced))
            {
                selectedList.FirstOrDefault(x => x.Value == ((long)TypeFloors.BuildingsVariableReinforced).ToString()).Selected = true;
            }
            else
            {
                selectedList.FirstOrDefault(x => x.Value == "0").Selected = true;
            }
            SelectListItem noneItem = selectedList.FirstOrDefault(x => x.Text == "Значение отсутствует");
            if (noneItem != null) noneItem.Text = "-";
            return Json(selectedList);
        }

        [HttpGet]
        public ContentResult GetDamageContracts(long damageId)
        {
            return Content(JsonConvert.SerializeObject(service.GetDamageContracts(damageId)), "application/json");
        }



        [HttpGet]
        public ActionResult DeleteCard(long? objectId)
        {
            DamageAnalysisCardDto model = service.GetShortCard(objectId);
            ViewBag.NumAndDate = string.Format("№ {0} от {1}", model?.NomDoc, model?.NomDate?.ToShortDateString());
            return View(objectId);
        }

        [HttpPost]
        public ContentResult Delete(long? damageId)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_DAMAGE_DELETE, true, false, true);

            try
            {
                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
                {
                    service.Delete(damageId);

                    SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_DAMAGE_DELETE, true, "Удалено дело по расчету суммы ущерба", OMDamage.GetRegisterId(), damageId);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }

            return EmptyResponse();
        }

        [HttpPost]
        public ContentResult UpdateContractIsPaid(long? damageId, long? fspId, bool isPaid)
        {
            if (!damageId.HasValue)
            {
                throw new Exception("Не удалось определить идентификатор дела");
            }

            if (!fspId.HasValue)
            {
                throw new Exception("Не удалось определить идентификатор ФСП");
            }

            try
            {
                service.UpdateContractIsPaid(damageId.Value, fspId.Value, isPaid);

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult UpdateContractInsurCost(long? damageId, long? fspId, long? livingPremiseInsurCostId)
        {
            if (!damageId.HasValue)
            {
                throw new Exception("Не удалось определить идентификатор дела");
            }

            if (!fspId.HasValue)
            {
                throw new Exception("Не удалось определить идентификатор ФСП");
            }

            if (!livingPremiseInsurCostId.HasValue)
            {
                throw new Exception("Не удалось определить идентификатор страховой стоимости");
            }

            try
            {
                return JsonResponse(service.UpdateContractInsurCost(damageId.Value, fspId.Value, livingPremiseInsurCostId.Value));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult ReturnToRevision(long id)
        {
            service.ReturnToRevision(id);

            return EmptyResponse();
        }

        [HttpPost]
        public IActionResult ReturnToCheck(long id)
        {
            service.ReturnToCheck(id);

            return EmptyResponse();
        }

        public ViewResult CopyConfirm(long? damageId, string type)
        {
            if (!damageId.HasValue)
            {
                throw new Exception("Не передан обязательный параметр идентифкатор дела по ущербу");
            }

            DamageAnalysisCardDto model = service.GetDamageAnalysisCardDto(damageId, GetContractType(type), true);
            return View(CopyConfirmDto.OMMap(model));
        }

        public ContentResult Copy(long? damageId, DateTime? damageDate,
            CausesOfDamageGP? causesOfDamageGp, CausesOfDamageOI? causesOfDamageOI,
            bool generateNewNumber = false)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_DAMAGE_WRITE, true, false, true);

            try
            {
                if (!damageId.HasValue)
                {
                    throw new Exception("Не передан обязательный параметр идентифкатор дела по ущербу");
                }

                service.Copy(damageId.Value, damageDate, causesOfDamageGp, causesOfDamageOI, generateNewNumber);

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ContentResult CheckStrahPlat([FromBody]CheckStrahPlatDto checkStrahPlatDto)
        {
            try
            {
                return JsonResponse(service.CheckStrahPlat(checkStrahPlatDto?.SumDamage, checkStrahPlatDto?.PartInsur, checkStrahPlatDto?.StrahPlat));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ContentResult CalcTownPartSum([FromBody]CalcTownPartSumDto calcTownPartSumDto)
        {
            try
            {
                return JsonResponse(new
                {
                    SumDamageEqual = (calcTownPartSumDto?.SumDamage ?? 0) - (calcTownPartSumDto?.DamageAmountSum ?? 0) <= 0.1m,
                    TownPartSum = service.CalcTownPartSum(calcTownPartSumDto?.SumDamage, calcTownPartSumDto?.PartTown)
                });
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        [Consumes("application/json")]
        public ContentResult CalcDamageAmount([FromBody]CalcDamageAmountDto calcDamageAmountDto)
        {
            try
            {
                return JsonResponse(service.CalcDamageAmount(calcDamageAmountDto?.MaterialDamage, calcDamageAmountDto?.Correction, calcDamageAmountDto.ProportionReplacementCost,
                    calcDamageAmountDto.ProportionDamagedArea, calcDamageAmountDto.EstimatedValue));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        private ContractType GetContractType(string type)
        {
            switch (type)
            {
                case "CommonOwnership":
                    return ContractType.CommonOwnership;
                default:
                    return ContractType.Dwelling;
            }
        }

        public IActionResult CheckList(long id)
        {
            List<long?> ids = RegistersVariables.CurrentList?.Cast<long?>().ToList() ?? new List<long?>();

            if (!ids.Any()) ids.Add(id);

            List<OMDamage> omDamages = OMDamage.Where(x => ids.Contains(x.EmpId))
                .SelectAll()
                .Select(x => x.ParentInsuranceOrganization.ShortName)
                .Select(x => x.ParentFlat.Unom)
                .Select(x => x.ParentFlat.ParentBuilding.ParentAddress.FullAddress)
                .Select(x => x.ParentFlat.Kvnom)
                .Select(x => x.ParentUser.FullName)
                .OrderByDescending(x => x.SortNumber)
                .Execute();

            List<OMInvoice> omInvoices = OMInvoice.Where(x => ids.Contains(x.LinkDamage))
                .Select(x => x.LinkDamage)
                .Select(x => x.SubjectName)
                .Select(x => x.SumOpl)
                .Select(x => x.Status)
                .Execute();

            if (omDamages.Any(x => x.DamageAmountStatus_Code != StatusDamageAmount.SendToCheck))
            {
                ViewBag.ErrorMessage = "Установить статус \"Проверено\" можно только для дел в статусе \"Передано на проверку\"";

                return View(new List<CheckListDto>());
            }

            List<CheckListDto> models = omDamages.Select(x => CheckListDto.Map(x, omInvoices)).ToList();

            return View(models);
        }

        [HttpPost]
        public IActionResult CheckList(List<long> ids)
        {
            List<OMDamage> omDamages = OMDamage.Where(x => ids.Contains(x.EmpId)).Execute();

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                foreach (OMDamage omDamage in omDamages)
                {
                    omDamage.DamageAmountStatus_Code = StatusDamageAmount.Checked;
                    omDamage.AgreementId2 = SRDSession.GetCurrentUserId();
                    omDamage.DateFill2 = DateTime.Now;
                    omDamage.Save();
                }

                ts.Complete();
            }

            return EmptyResponse();
        }

        public IActionResult AgreeList(long id)
        {
            List<long?> ids = RegistersVariables.CurrentList?.Cast<long?>().ToList() ?? new List<long?>();

            if (!ids.Any()) ids.Add(id);

            List<OMDamage> omDamages = OMDamage.Where(x => ids.Contains(x.EmpId))
                .SelectAll()
                .Select(x => x.ParentInsuranceOrganization.ShortName)
                .Select(x => x.ParentFlat.Unom)
                .Select(x => x.ParentFlat.ParentBuilding.ParentAddress.FullAddress)
                .Select(x => x.ParentFlat.Kvnom)
                .Select(x => x.ParentUser.FullName)
                .OrderByDescending(x => x.SortNumber)
                .Execute();

            List<OMInvoice> omInvoices = OMInvoice.Where(x => ids.Contains(x.LinkDamage))
                .Select(x => x.LinkDamage)
                .Select(x => x.SubjectName)
                .Select(x => x.SumOpl)
                .Select(x => x.Status)
                .Execute();

            if (omDamages.Any(x => x.DamageAmountStatus_Code != StatusDamageAmount.Checked))
            {
                ViewBag.ErrorMessage = "Установить статус \"Согласовано\" можно только для дел в статусе \"Проверено\"";
            }

            List<CheckListDto> models = omDamages.Select(x => CheckListDto.Map(x, omInvoices)).ToList();

            return View(models);
        }

        [HttpPost]
        public IActionResult AgreeList(List<long?> ids)
        {
            List<OMDamage> omDamages = OMDamage.Where(x => ids.Contains(x.EmpId)).Execute();
            List<OMInvoice> omInvoices = OMInvoice.Where(x => ids.Contains(x.LinkDamage))
                .Select(x => x.LinkDamage)
                .Select(x => x.Status_Code)
                .Execute();

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                foreach (OMDamage omDamage in omDamages)
                {
                    omDamage.DamageAmountStatus_Code = StatusDamageAmount.Agreed;
                    omDamage.MainAgreementId = SRDSession.GetCurrentUserId();
                    omDamage.DateFillMain = DateTime.Now;
                    omDamage.Save();

                    List<OMInvoice> targetOMInvoices = omInvoices.Where(x => x.LinkDamage == omDamage.EmpId).ToList();

                    foreach (OMInvoice omInvoice in omInvoices)
                    {
                        omInvoice.DateAgree = DateTime.Now;
                        omInvoice.UserAgreeId = SRDSession.GetCurrentUserId();
                        omInvoice.Status_Code = 
                            omInvoice.Status_Code == InvoiceStatus.Denied ? InvoiceStatus.DeniedAgreed : 
                            omInvoice.Status_Code == InvoiceStatus.Formed ? InvoiceStatus.Agreed : omInvoice.Status_Code;
                        omInvoice.Save();
                    }
                }

                ts.Complete();
            }

            return EmptyResponse();
        }

        public ActionResult GetDamageByNomDoc(string nomDoc)
        {
            List<DamageAnalysisCardDto> models = service.GetByNomDoc(nomDoc);
            return Content(JsonConvert.SerializeObject(models), "application/json");
        }

        [HttpGet]
        public ActionResult SetBaseDamage(long? damageId, bool isSetValueType)
        {
            if (!damageId.HasValue)
            {
                throw new Exception("Не передан обязательный параметр (damageId)");
            }

            return View(new SetBaseDamageDto
            {
                DamageId = damageId.Value,
                IsSetValueType = isSetValueType
            });
        }

        [HttpPost]
        public ContentResult SetBaseDamage(long? damageId, long? baseDamageId, string baseDamageNomDoc, bool isSetValueType)
        {
            try
            {
                if (!damageId.HasValue)
                {
                    throw new Exception("Не передан обязательный параметр (damageId)");
                }

                return JsonResponse(service.SetBaseDamage(damageId.Value, baseDamageId, baseDamageNomDoc, isSetValueType));
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SetReissueZalkuchFlag(long damageId)
        {
            new OMDamage
            {
                EmpId = damageId,
                FlagZakluchReissue = true
            }.Save();
            return new EmptyResult();
        }
    }
}