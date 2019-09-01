using CIPJS.DAL.Documents;
using CIPJS.DAL.Fsp;
using Core.Numerator;
using Core.RefLib;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Newtonsoft.Json;
using ObjectModel.Bti;
using ObjectModel.Core.SRD;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using CIPJS.DAL.Comment;
using Core.SRD.DAL;

namespace CIPJS.DAL.DamageAnalysis
{
    public class DamageAnalysisService
    {
        private FspService _fspService;

        private readonly string _nomDocPattern = @"^(\d*-?)(ЖП|ОИ)(\d{5})(\/\d{2})(-\d*)?$";

        public DamageAnalysisService()
        {
            _fspService = new FspService();
        }

        /// <summary>
        /// Получить список дел для расчета суммы ущерба по идентификатору объекта
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>список дел</returns>
        public List<OMDamage> Get(long? id)
        {
            if (id.HasValue)
                return OMDamage.Where(x => x.ObjId == id && x.ObjReestrId == OMFlat.GetRegisterId())
                   .SelectAll()
                   .Select(x => x.ParentInsuranceOrganization.FullName)
                   .Execute();

            return null;
        }

        public DamageAnalysisCardDto Get(long objId, ContractType type, DateTime? damageDate,
            CausesOfDamageGP? causesOfDamageGp, CausesOfDamageOI? causesOfDamageOI)
        {
            return List(objId, type, damageDate, causesOfDamageGp, causesOfDamageOI).FirstOrDefault();
        }

        public List<DamageAnalysisCardDto> List(long objId, ContractType type, DateTime? damageDate,
            CausesOfDamageGP? causesOfDamageGp, CausesOfDamageOI? causesOfDamageOI)
        {

            if (type == ContractType.None)
            {
                throw new Exception("Передан неподдерживаемый тип дела по ущербу");
            }

            if (!damageDate.HasValue)
            {
                throw new Exception("Не удалось определить дату СС");
            }

            QSQuery<OMDamage> query = OMDamage.Where(x => x.ObjId == objId
                && x.TypeDoc_Code == type && x.DamageData == damageDate.Value);

            if (causesOfDamageGp.HasValue && type == ContractType.Dwelling)
            {
                if (causesOfDamageGp.Value == CausesOfDamageGP.None)
                {
                    query.And(x => (x.DamageReasonGP_Code == null
                     || x.DamageReasonGP_Code == causesOfDamageGp.Value));
                }
                else
                {
                    query.And(x => x.DamageReasonGP_Code == causesOfDamageGp.Value);
                }
            }
            else if (causesOfDamageOI.HasValue && type == ContractType.CommonOwnership)
            {
                if (causesOfDamageOI.Value == CausesOfDamageOI.None)
                {
                    query.And(x => (x.DamageReasonOI_Code == null
                     || x.DamageReasonOI_Code == causesOfDamageOI.Value));
                }
                else
                {
                    query.And(x => x.DamageReasonOI_Code == causesOfDamageOI.Value);
                }
            }

            return query.OrderBy(x => x.DateInput)
                .Select(x => x.NomDoc)
                .Select(x => x.DateFill1)
                .Execute()
                .Select(x => new DamageAnalysisCardDto
                {
                    Id = x.EmpId,
                    NomDoc = x.NomDoc,
                    CalculatePersonDate = x.DateFill1
                }).ToList();
        }

        /// <summary>
        /// Получить количество дел для расчета суммы ущерба по идентификатору объекта
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Количество</returns>
        public int Count(long? id)
        {
            if (id.HasValue)
                return OMDamage.Where(x => x.ObjId == id && x.ObjReestrId == OMFlat.GetRegisterId())
                    .GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt();

            return 0;
        }

        /// <summary>
        /// Получение информации по делу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DamageAnalysisCardDto GetDamageAnalysisCardDto(long? id, ContractType type, bool isReadOnly)
        {
            if (id.HasValue)
            {
                OMDamage damage = OMDamage.Where(x => x.EmpId == id).SelectAll().Select(x => x.ParentInsuranceOrganization.FullName).Execute().FirstOrDefault();
                if (damage == null)
                {
                    throw new Exception(string.Format("Не найден объект OMDamage = {0}", id));
                }
                DamageAnalysisCardDto model = new DamageAnalysisCardDto
                {
                    IsReadOnly = isReadOnly,
                    Id = damage.EmpId,
                    UserId = damage.UserId.GetValueOrDefault(),
                    TypeCode = damage.TypeDoc_Code,

                    NomDoc = damage.NomDoc,
                    NomDate = damage.NomDate,
                    StatusDoc = damage.DamageAmountStatus_Code,

                    InsurCompanyId = damage.InsurOrgId,
                    InsurCompanyName = damage.ParentInsuranceOrganization?.FullName,
                    StrahPlat = damage.StrahPlat,
                    InsurNom = damage.InsurNom,
                    InsurDate = damage.InsurDate,
                    DamageData = damage.DamageData,
                    DateInputGBY = damage.DateInputGBY,
                    DateDocLastGBY = damage.DateDocLastGBY,
                    DateDocAddGBY = damage.DateDocAddGBY,
                    Note = damage.Note,
                    SumDamage = damage.SumDamage,
                    SumDamageBase = damage.SumDamageBase,
                    CalculDamage = damage.CalculDamage,
                    CausesOfDamageGP = damage.DamageReasonGP_Code,
                    SubReasonCausesOfDamage = damage.SubreasonDamageReason_Code,
                    RefinementSubReasonCOD = damage.RefinementSubreason_Code,
                    CausesOfDamageOI = damage.DamageReasonOI_Code,
                    Franchise = damage.Franchise.GetValueOrDefault(5000m),
                    FlagSlygebka = damage.FlagSlygebka,
                    CalcNote = damage.CalcNote,
                    FlagZakluchReissue = damage.FlagZakluchReissue.GetValueOrDefault(),
                    Fault = damage.Fault,

                    ObjId = damage.ObjId,

                    Contracts = new List<DamageAnalysisContractDto>(),
                    InsurOrgPayToDto = GetInsurOrgPayTo(damage.EmpId),

                    EstimatedValue = damage.EstimatedValue,
                    EstimatedValueDifferent = damage.EstimatedValueDifferent,

                    StoveType = damage.TypeCooker_Code,
                    FloorType = damage.TypeFloor_Code,

                    CalculatePersonId = damage.AgreementId1,
                    CalculatePersonDate = damage.DateFill1,
                    InspectorPersonId = damage.AgreementId2,
                    InspectorPersonDate = damage.DateFill2,
                    InspectorPersonCheckId = damage.ControlUserId,
                    InspectorPersonCheckDate = damage.DateControl,
                    PrimaryMatchingPersonId = damage.MainAgreementId,
                    PrimaryMatchingDate = damage.DateFillMain,

                    CanSendToCheck = damage.DamageAmountStatus_Code == StatusDamageAmount.Created
                        || damage.DamageAmountStatus_Code == StatusDamageAmount.DamageAmountCoincides
                        || damage.DamageAmountStatus_Code == StatusDamageAmount.DamageAmountDiscrepancies,
                    //CIPJS-426 нет, если дело в статусе "Создано", "Расчет ущерба совпадает с данными СК", "Расхождения со СК в расчете ущерба"
                    CanChecked = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_DAMAGE_FLAT_CHECK)
                        && damage.DamageAmountStatus_Code == StatusDamageAmount.SendToCheck,
                    CanAgreed = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_DAMAGE_FLAT_AGREED)
                        && damage.DamageAmountStatus_Code == StatusDamageAmount.Checked
                };

                model.PartInsur = damage.PartInsur;
                model.PartTown = damage.PartTown;
                model.TownPartSum = Math.Round((damage.PartTown ?? 0) * (damage.SumDamage ?? 0) / 100, 2, MidpointRounding.AwayFromZero);

                switch (damage.TypeDoc_Code)
                {
                    case ContractType.Dwelling:
                        DamageAnalysisAdditionalDataDto additionalData = damage.AdditionalData.IsNotEmpty() ?
                            JsonConvert.DeserializeObject<DamageAnalysisAdditionalDataDto>(damage.AdditionalData) :
                            null;
                        GetObjectInfoFromDwelling(model/*, true*/, additionalData);

                        //CIPJS-412 определяем идентификатор платежного документа
                        model.PaidDocTypeId = OMDocBaseType.Where(x => x.DocumentBase == "Сведения, подтверждающие оплату страхового взноса"
                            && x.Type == "ЖП")
                            .ExecuteFirstOrDefault()?.Id;
                        model.InspectionActDocTypeId = OMDocBaseType.Where(x => x.DocumentBase == "Акт осмотра СК").ExecuteFirstOrDefault()?.Id;
                        break;
                    case ContractType.CommonOwnership:
                        GetObjectInfoFromCommonOwnership(model/*, true*/);
                        break;
                }

                if( model.Contracts.FirstOrDefault() != null 
                    && model.Contracts.FirstOrDefault().AllPropertyId != null
                    && damage.AllPropertyId != model.Contracts.FirstOrDefault().AllPropertyId)
                {
                    damage.AllPropertyId = model.Contracts.FirstOrDefault().AllPropertyId;
                    damage.Save();
                }



                if (damage.LinkBaseDelo.HasValue)
                {
                    OMDamage baseDamage = OMDamage.Where(x => x.EmpId == damage.LinkBaseDelo.Value)
                        .Select(x => x.NomDoc)
                        .ExecuteFirstOrDefault();

                    if (baseDamage != null)
                    {
                        model.BaseDamageExists = true;
                        model.BaseDamageId = baseDamage.EmpId;
                        model.BaseDamageNomDoc = baseDamage.NomDoc;
                    }
                }
                else if (damage.BaseDelo.IsNotEmpty())
                {
                    model.BaseDamageExists = true;
                    model.BaseDamageNomDoc = damage.BaseDelo;
                }

                if (model.CalculatePersonId.HasValue)
                {
                    OMUser user = OMUser.Where(x => x.Id == model.CalculatePersonId).Select(x => x.FullName).Select(x => x.Position).Execute().FirstOrDefault();
                    if (user != null)
                    {
                        model.CalculatePersonFIO = user.FullName;
                        model.CalculatePersonPost = user.Position;
                    }
                }

                if (model.InspectorPersonCheckId.HasValue)
                {
                    OMUser user = OMUser.Where(x => x.Id == model.InspectorPersonCheckId).Select(x => x.FullName).Select(x => x.Position).Execute().FirstOrDefault();
                    if (user != null)
                    {
                        model.InspectorPersonCheckFIO = user.FullName;
                        model.InspectorPersonCheckPost = user.Position;
                    }
                }

                if (model.InspectorPersonId.HasValue)
                {
                    OMUser user = OMUser.Where(x => x.Id == model.InspectorPersonId).Select(x => x.FullName).Select(x => x.Position).Execute().FirstOrDefault();
                    if (user != null)
                    {
                        model.InspectorPersonFIO = user.FullName;
                        model.InspectorPersonPost = user.Position;
                    }
                }

                if (model.PrimaryMatchingPersonId.HasValue)
                {
                    OMUser user = OMUser.Where(x => x.Id == model.PrimaryMatchingPersonId).Select(x => x.FullName).Select(x => x.Position).Execute().FirstOrDefault();
                    if (user != null)
                    {
                        model.PrimaryMatchingFIO = user.FullName;
                        model.PrimaryMatchingPost = user.Position;
                    }
                }

                if (damage.TypeBuild_Code != BuildingType.None)
                {
                    OMTypeBuldingFloorLink link = OMTypeBuldingFloorLink
                        .Where(x => x.TypeBuilding_Code == damage.TypeBuild_Code)
                        .Select(x => x.TypeBuildingStructure_Code)
                        .Select(x => x.TypeFloors_Code)
                        .Execute()
                        .FirstOrDefault();
                    if (link != null)
                    {
                        model.BuildingStructure = link.TypeBuildingStructure_Code;
                        model.Floors = link.TypeFloors_Code;
                    }
                }

                model.HasOtherDamages = damage.NomDoc.IsNotEmpty() && Regex.IsMatch(damage.NomDoc, @"-\d*$");

                //CIPJS-342 После нажатия на кнопку "Передано на проверку" дело блокируется для того кто его создал
                model.CanEdit = !model.InspectorPersonCheckId.HasValue
                    || !((model.StatusDoc == StatusDamageAmount.SendToCheck
                            || model.StatusDoc == StatusDamageAmount.Checked
                            || model.StatusDoc == StatusDamageAmount.Agreed)
                        && model.InspectorPersonCheckId.Value == SRDSession.GetCurrentUserId());

                return model;
            }

            return CreateEmptyCardModel(type);
        }

        /// <summary>
        /// Get info for delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DamageAnalysisCardDto GetShortCard(long? id)
        {
            if (id.HasValue)
            {
                OMDamage damage = OMDamage.Where(x => x.EmpId == id).SelectAll().Select(x => x.ParentInsuranceOrganization.FullName).Execute().FirstOrDefault();
                if (damage == null)
                {
                    throw new Exception(string.Format("Не найден объект OMDamage = {0}", id));
                }
                DamageAnalysisCardDto model = new DamageAnalysisCardDto
                {
                    Id = damage.EmpId,
                    NomDoc = damage.NomDoc,
                    NomDate = damage.NomDate
                };

                return model;
            }

            return null;
        }

        /// <summary>
        /// Сохранение дела
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DamageAnalysisCardDto SaveDamageAnalysisCard(DamageAnalysisCardDto model, bool generateNewNumber = false)
        {
            OMDamage damage;

            BuildingType buildingType = BuildingType.None;
            if (model.BuildingStructure != TypeBuildingStructure.None && model.Floors != TypeFloors.None)
            {
                OMTypeBuldingFloorLink link = OMTypeBuldingFloorLink
                    .Where(x => x.TypeFloors_Code == model.Floors && x.TypeBuildingStructure_Code == model.BuildingStructure)
                    .Select(x => x.TypeBuilding_Code)
                    .Execute()
                    .FirstOrDefault();
                if (link != null)
                {
                    buildingType = link.TypeBuilding_Code;
                }
            }

            if (model.Id.HasValue && model.Id > 0)
            {
                damage = OMDamage.Where(x => x.EmpId == model.Id).SelectAll().Execute().FirstOrDefault();
            }
            else
            {
                if (model.NomDoc.StartsWith("000000"))
                {
                    throw new Exception("Некорректный номер дела");
                }
                damage = OMDamage.CreateEmpty();
                damage.TypeDoc_Code = model.TypeCode;
                damage.DamageAmountStatus_Code = StatusDamageAmount.Created;
                damage.AgreementId1 = SRDSession.GetCurrentUserId();
                damage.ObjReestrId = model.TypeCode == ContractType.Dwelling ? 317 : model.TypeCode == ContractType.CommonOwnership ? 316 : 0;
            }

            if (!model.ObjId.HasValue)
            {
                throw new Exception("Невозможно сохранить дело, т.к. не был выбран объект");
            }

            if ((model.Contracts == null && model.ObjId.HasValue) || !model.Area.HasValue)
            {
                DamageAnalysisAdditionalDataDto additionalData = damage.EmpId > 0 &&
                    damage.TypeDoc_Code == ContractType.Dwelling && damage.AdditionalData.IsNotEmpty() ?
                    JsonConvert.DeserializeObject<DamageAnalysisAdditionalDataDto>(damage.AdditionalData) :
                    null;
                model = GetObjInfo(model, additionalData: additionalData);
            }

            bool estimatedValueDifferent = false;
            if (model.Contracts != null && model.Contracts.Count > 0m && (!damage.EstimatedValue.HasValue || model.RecalcEstimatedValue))
            {
                model.EstimatedValue = CalcEstimatedValue(model.DamageData, model.Area, model.Contracts, out estimatedValueDifferent);
            }

            //CIPJS-585 измененяем номер, если был изменен объект
            if (model.ObjId != damage.ObjId)
            {
                long? baseDamageId = null;
                model.NomDoc = GetNewNomDoc(model.TypeCode, model.ObjId.Value, model.DamageData, out baseDamageId, model.CausesOfDamageGP, model.CausesOfDamageOI, generateNewNumber, damage.NomDoc);
                damage.LinkBaseDelo = baseDamageId;
            }

            CommentListDto commentList = new CommentListDto
            {
                Comments = new List<CommentDto>(),
                IsCommentReadOnly = true,
                CommentObjectId = damage.EmpId,
                CommentReestrId = OMDamage.GetRegisterId()
            };
            if (!model.Note.IsNullOrEmpty() && model.Note != damage.Note)
            {
                damage.Note = model.Note;
                commentList.Comments.Add(new CommentDto { Comment = model.Note + " (Примечание в разделе \"Документы от СК\")" });
            }
            if (!model.CalcNote.IsNullOrEmpty() && model.CalcNote != damage.CalcNote)
            {
                damage.CalcNote = model.CalcNote;
                commentList.Comments.Add(new CommentDto { Comment = model.CalcNote + " (Примечание в разделе \"Расчет ущерба\")" });
            }
            new CommentService().Save(commentList);

            if (damage.UserId == null)
                damage.UserId = model.UserId;

            damage.NomDoc = model.NomDoc;
            damage.NomDate = model.NomDate;
            damage.InsurOrgId = model.InsurCompanyId;
            damage.StrahPlat = model.StrahPlat;
            damage.PartInsur = model.PartInsur;
            damage.Franchise = model.Franchise;
            damage.PartTown = model.PartTown;
            damage.InsurNom = model.InsurNom;
            damage.InsurDate = model.InsurDate;
            damage.DamageData = model.DamageData;
            damage.DateInputGBY = model.DateInputGBY;
            damage.DateDocLastGBY = model.DateDocLastGBY;
            damage.DateDocAddGBY = model.DateDocAddGBY;
            damage.SumDamage = model.SumDamage;
            damage.SumDamageBase = model.SumDamageBase;
            damage.DamageReasonGP_Code = model.CausesOfDamageGP;
            damage.SubreasonDamageReason_Code = model.SubReasonCausesOfDamage;
            damage.RefinementSubreason_Code = model.RefinementSubReasonCOD;
            damage.DamageReasonOI_Code = model.CausesOfDamageOI;
            damage.DateFill1 = model.CalculatePersonDate ?? DateTime.Now;
            damage.Fault = model.Fault;

            damage.ObjId = model.ObjId;

            bool flagRecalc = false;
            if (damage.EstimatedValue != model.EstimatedValue)
            {
                flagRecalc = true;
            }
            if (damage.TypeBuild_Code != buildingType)
            {
                flagRecalc = true;
            }
            if (damage.TypeCooker_Code != model.StoveType)
            {
                flagRecalc = true;
            }
            if (damage.TypeFloor_Code != model.FloorType)
            {
                flagRecalc = true;
            }
            damage.EstimatedValue = model.EstimatedValue;
            damage.EstimatedValueDifferent = model.EstimatedValueDifferent = estimatedValueDifferent;
            damage.TypeBuild_Code = buildingType;
            damage.TypeCooker_Code = model.StoveType;
            damage.TypeFloor_Code = model.FloorType;
            damage.FlagSlygebka = model.FlagSlygebka;
            damage.FlagZakluchReissue = model.FlagZakluchReissue;


            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                if (flagRecalc)
                {
                    List<OMDamageAmount> oMDamageAmounts = OMDamageAmount.Where(x => x.DamageId == damage.EmpId).SelectAll().Execute();
                    foreach (var elem in ReferencesCommon.GetItems<ElementsOfConstructions>(addEmptyValue: false).OrderBy(x => x.ItemId))
                    {
                        var damageAmount = oMDamageAmounts.FirstOrDefault(x => x.ElementConstruction_Code == (ElementsOfConstructions)elem.ItemId);
                        if (damageAmount == null)
                        {
                            damageAmount = new OMDamageAmount
                            {
                                DamageId = damage.EmpId,
                                ElementConstruction_Code = (ElementsOfConstructions)elem.ItemId
                            };
                            damageAmount.Save();
                            //SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_DAMAGE, true, "Создан расчет ущерба по элементам конструкции", OMDamageAmount.GetRegisterId(), damageAmount.EmpId);

                        }
                        if (damage.TypeCooker_Code != StoveType.None && damage.TypeBuild_Code != BuildingType.None && damage.TypeFloor_Code != FloorMaterial.None)
                        {
                            OMIntegrateIndicatorsReplecmentCost indicator = OMIntegrateIndicatorsReplecmentCost.Where(x => x.ElementsConstructions_Code == (ElementsOfConstructions)elem.ItemId &&
                            x.FloorMaterial_Code == damage.TypeFloor_Code && x.BuildingType_Code == damage.TypeBuild_Code && x.StoveType_Code == damage.TypeCooker_Code)
                                                                                .Select(x => x.CostValue)
                                                                                .Select(x => x.HasChilds)
                                                                                .Select(x => x.ElementsConstructions)
                                                                                .Execute()
                                                                                .FirstOrDefault();
                            if (indicator != null && !indicator.HasChilds.Value)
                            {
                                damageAmount.ProportionReplacementCost = indicator.CostValue;
                                damageAmount.DamageAmount = CalcDamageAmount(damageAmount.MaterialDamage, damageAmount.Correction, damageAmount.ProportionReplacementCost, damageAmount.ProportionDamagedArea, damage.EstimatedValue);
                                damageAmount.Save();
                                //SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_DAMAGE, true, "Изменен расчет ущерба по элементам конструкции", OMDamageAmount.GetRegisterId(), damageAmount.EmpId);
                            }
                        }
                    }
                }

                if (!model.Id.HasValue || model.Id == -1)
                {
                    CreateDocsForDamage(damage.EmpId, damage.TypeDoc_Code);
                }

                model.Id = damage.Save();
                string resultDesc = generateNewNumber ? "Создано дело по ущербу" : "Обновлено дело по ущербу ";
                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_DAMAGE, true, resultDesc, OMDamage.GetRegisterId(), damage.EmpId);

                ts.Complete();

                return model;
            }
        }

        /// <summary>
        /// Получение информации об объекте страхования
        /// </summary>
        /// <param name="model">Данные по делу</param>
        /// <returns></returns>
        public DamageAnalysisCardDto GetObjInfo(DamageAnalysisCardDto model, DamageAnalysisAdditionalDataDto additionalData = null)
        {
            switch (model.TypeCode)
            {
                case ContractType.Dwelling:
                    GetObjectInfoFromDwelling(model, additionalData);
                    break;
                case ContractType.CommonOwnership:
                    GetObjectInfoFromCommonOwnership(model);
                    break;
            }

            return model;
        }

        /// <summary>
        /// Проверка статуса расчета дела, совпадает или нет
        /// </summary>
        /// <param name="damageId"></param>
        public StatusDamageAmount CheckStatus(long? damageId)
        {
            OMDamage damage = OMDamage.Where(x => x.EmpId == damageId)
                .Select(x => x.TypeDoc)
                .Select(x => x.TypeDoc_Code)
                .Select(x => x.SumDamage)
                .Select(x => x.SumDamageBase)
                .Select(x => x.EstimatedValue)
                .Select(x => x.DamageAmountStatus_Code)
                .Select(x => x.DateFill1)
                .Select(x => x.DateFill2)
                .Select(x => x.DateFillMain)
                .Execute().FirstOrDefault();
            if (damage == null || !damage.DateFill1.HasValue)
            {
                return StatusDamageAmount.None;
            }

            if (damage.TypeDoc_Code == ContractType.CommonOwnership)
            {
                return damage.DamageAmountStatus_Code;
            }

            if (damage.DamageAmountStatus_Code != StatusDamageAmount.None
                && damage.DamageAmountStatus_Code != StatusDamageAmount.Created
                && damage.DamageAmountStatus_Code != StatusDamageAmount.DamageAmountCoincides
                && damage.DamageAmountStatus_Code != StatusDamageAmount.DamageAmountDiscrepancies)
            {
                return damage.DamageAmountStatus_Code;
            }

            damage.CalculDamage = OMDamageAmount.Where(x => x.DamageId == damageId).Select(x => x.DamageAmount).Execute().Sum(x => x.DamageAmount);
            if ((damage.EstimatedValue == null || damage.CalculDamage == null || !damage.SumDamageBase.HasValue) && damage.DateFill1.HasValue)
            {
                damage.DamageAmountStatus_Code = StatusDamageAmount.Created;
            }
            else if (Math.Abs(damage.SumDamageBase.Value - damage.CalculDamage.Value) > (decimal)0.1)
            {
                damage.DamageAmountStatus_Code = StatusDamageAmount.DamageAmountDiscrepancies;
            }
            else
            {
                damage.DamageAmountStatus_Code = StatusDamageAmount.DamageAmountCoincides;
            }
            damage.Save();
            return damage.DamageAmountStatus_Code;
        }

        /// <summary>
        /// Перевод дела в статус "Проверено"
        /// </summary>
        /// <param name="damageId"></param>
        public void CheckedDamageAnalysis(long? damageId)
        {
            OMDamage damage = OMDamage.Where(x => x.EmpId == damageId).SelectAll(false).Execute().FirstOrDefault();
            if (damage == null)
            {
                throw new Exception(string.Format("Не найден объект OMDamage = {0}", damageId));
            }

            damage.DamageAmountStatus_Code = StatusDamageAmount.Checked;
            damage.AgreementId2 = SRDSession.GetCurrentUserId();
            damage.DateFill2 = DateTime.Now;

            damage.Save();
        }

        /// <summary>
        /// Перевод дела в статус "Согласовано"
        /// </summary>
        /// <param name="damageId"></param>
        public void AgreedDamageAnalysis(long? damageId, bool isAgreeInvoices)
        {
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                OMDamage omDamage = OMDamage.Where(x => x.EmpId == damageId).ExecuteFirstOrDefault();

                if (omDamage == null) throw new Exception($"Не найден объект OMDamage = {damageId}");

                omDamage.DamageAmountStatus_Code = StatusDamageAmount.Agreed;
                omDamage.MainAgreementId = SRDSession.GetCurrentUserId();
                omDamage.DateFillMain = DateTime.Now;
                omDamage.Save();

                if (isAgreeInvoices)
                {
                    // CIPJS-899: п4. переводить счет в статус "Согласован" только из статуса "Создан"
                    List<OMInvoice> omInvoices = OMInvoice.Where(x => x.LinkDamage == damageId && x.Status_Code == InvoiceStatus.Formed)
                        .Select(x => x.Status)
                        .Select(x => x.Status_Code)
                        .Select(x => x.DateAgree)
                        .Select(x => x.UserAgreeId)
                        .Execute();

                    foreach (OMInvoice omInvoice in omInvoices)
                    {
                        omInvoice.DateAgree = DateTime.Now;
                        omInvoice.UserAgreeId = SRDSession.GetCurrentUserId();
                        //CIPJS-434 При процессе согласования счетов ( этот функционал может вызываться как с карточки, так и по кнопке "Согласовать счета" в представлениях со счетами.
                        //Счет в статусе "Отказано в выплате" переводить не в статус "Согласовано", а в статус ""Отказано в выплате / Согласован"
                        if (omInvoice.Status_Code == InvoiceStatus.Denied)
                        {
                            omInvoice.Status_Code = InvoiceStatus.DeniedAgreed;
                        }
                        else if(omInvoice.Status_Code == InvoiceStatus.Formed)
                        {
                            omInvoice.Status_Code = InvoiceStatus.Agreed;
                        }
                        omInvoice.Save();
                    }
                }

                ts.Complete();
            }
        }

        public void SendToCheckDamageAnalysis(long? damageId)
        {
            OMDamage damage = OMDamage.Where(x => x.EmpId == damageId).SelectAll(false).Execute().FirstOrDefault();
            if (damage == null)
            {
                throw new Exception(string.Format("Не найден объект OMDamage = {0}", damageId));
            }

            damage.DamageAmountStatus_Code = StatusDamageAmount.SendToCheck;
            damage.ControlUserId = SRDSession.GetCurrentUserId();
            damage.DateControl = DateTime.Now;

            damage.Save();
        }

        public List<OMDamage> GetDamagesByIds(List<long> ids)
        {
            if (ids == null || !ids.Any()) return new List<OMDamage>();

            return OMDamage.Where(x => ids.Contains(x.EmpId)).Select(x => x.DamageAmountStatus_Code).Execute();
        }

        /// <summary>
        /// Получение списка подпричин для причины
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public List<SubReasonCausesOfDamage> GetSubreasonOfDamage(CausesOfDamageGP damage)
        {
            List<SubReasonCausesOfDamage> list = OMLinkCausesSubreasonLP
                .Where(x => x.CausesOfDamage_Code == damage)
                .Select(x => x.SubresonCausesOfDamage_Code)
                .Execute()
                .Select(x => x.SubresonCausesOfDamage_Code)
                .ToList();

            list.Add(SubReasonCausesOfDamage.None);

            return list;
        }

        /// <summary>
        /// Получение списка уточнений подпричины
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="subreason"></param>
        /// <returns></returns>
        public List<RefinementSubReasonCOD> GetRefinementSubreason(CausesOfDamageGP damage, SubReasonCausesOfDamage subreason)
        {
            List<RefinementSubReasonCOD> list = OMLinkCausesSubreasonLP
                .Where(x => x.CausesOfDamage_Code == damage && x.SubresonCausesOfDamage_Code == subreason)
                .Select(x => x.RefinementSubreason_Code)
                .Execute()
                .Select(x => x.RefinementSubreason_Code)
                .ToList();

            list.Add(RefinementSubReasonCOD.None);

            return list;
        }

        /// <summary>
        /// Получение списка 
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public List<TypeFloors> GetFloors(TypeBuildingStructure buildingStructure)
        {
            List<TypeFloors> list = OMTypeBuldingFloorLink
                .Where(x => x.TypeBuildingStructure_Code == buildingStructure)
                .Select(x => x.TypeFloors_Code)
                .Execute()
                .Select(x => x.TypeFloors_Code)
                .ToList();

            list.Add(TypeFloors.None);

            return list;
        }

        /// <summary>
        /// Удаление дела
        /// </summary>
        /// <param name="id"></param>
        public void Delete(long? id)
        {
            if (id.HasValue)
            {
                OMDamage damageAmount = OMDamage.Where(x => x.EmpId == id).SelectAll(false).Execute().FirstOrDefault();
                if (damageAmount != null)
                {
                    damageAmount.Destroy();
                }
            }
        }

        public void UpdateContractIsPaid(long damageId, long fspId, bool isPaid)
        {
            OMDamage damage = OMDamage.Where(x => x.EmpId == damageId).Select(x => x.AdditionalData).ExecuteFirstOrDefault();

            if (damage == null)
            {
                throw new Exception($"Не удалось определить дело по идентификатору {damageId}");
            }

            DamageAnalysisAdditionalDataDto additionalData = damage.AdditionalData.IsNotEmpty() ?
                    JsonConvert.DeserializeObject<DamageAnalysisAdditionalDataDto>(damage.AdditionalData) :
                    new DamageAnalysisAdditionalDataDto();

            if (additionalData.Contracts == null)
            {
                additionalData.Contracts = new List<DamageAnalysisAdditionalContractInfoDto>();
            }

            DamageAnalysisAdditionalContractInfoDto contractInfo = additionalData.Contracts.FirstOrDefault(x => x.FspId == fspId);

            if (contractInfo == null)
            {
                contractInfo = new DamageAnalysisAdditionalContractInfoDto
                {
                    FspId = fspId
                };
                additionalData.Contracts.Add(contractInfo);
            }

            contractInfo.IsPaid = isPaid;

            damage.AdditionalData = JsonConvert.SerializeObject(additionalData);
            damage.Save();
        }

        public decimal? UpdateContractInsurCost(long damageId, long fspId, long livingPremiseInsurCostId)
        {
            OMLivingPremiseInsurCost livingPremiseCost = OMLivingPremiseInsurCost
                .Where(x => x.Id == livingPremiseInsurCostId)
                .SelectAll()
                .ExecuteFirstOrDefault();

            if (livingPremiseCost == null)
            {
                throw new Exception($"Не удалось определить страховоую стоимость по идентификатору {livingPremiseInsurCostId}");
            }

            OMDamage damage = OMDamage.Where(x => x.EmpId == damageId).Select(x => x.AdditionalData).ExecuteFirstOrDefault();

            if (damage == null)
            {
                throw new Exception($"Не удалось определить дело по идентификатору {damageId}");
            }

            DamageAnalysisAdditionalDataDto additionalData = damage.AdditionalData.IsNotEmpty() ?
                    JsonConvert.DeserializeObject<DamageAnalysisAdditionalDataDto>(damage.AdditionalData) :
                    new DamageAnalysisAdditionalDataDto();

            if (additionalData.Contracts == null)
            {
                additionalData.Contracts = new List<DamageAnalysisAdditionalContractInfoDto>();
            }

            DamageAnalysisAdditionalContractInfoDto contractInfo = additionalData.Contracts.FirstOrDefault(x => x.FspId == fspId);

            if (contractInfo == null)
            {
                contractInfo = new DamageAnalysisAdditionalContractInfoDto
                {
                    FspId = fspId
                };
                additionalData.Contracts.Add(contractInfo);
            }

            contractInfo.InsurCost = livingPremiseCost.Value;

            damage.AdditionalData = JsonConvert.SerializeObject(additionalData);
            damage.Save();

            return livingPremiseCost.Value;
        }

        public void ReturnToRevision(long id)
        {
            OMDamage omDamage = OMDamage.Where(x => x.EmpId == id)
                .Select(x => x.SumDamage)
                .Select(x => x.CalculDamage)
                .ExecuteFirstOrDefault();

            if (omDamage == null) throw new Exception($"Сущность не найдена ИД={id}");

            if (Math.Abs((omDamage.SumDamage ?? 0) - (omDamage.CalculDamage ?? 0)) > (decimal)0.1)
            {
                omDamage.DamageAmountStatus_Code = StatusDamageAmount.DamageAmountDiscrepancies;
            }
            else
            {
                omDamage.DamageAmountStatus_Code = StatusDamageAmount.DamageAmountCoincides;
            }

            omDamage.Save();
        }

        public void ReturnToCheck(long id)
        {
            OMDamage omDamage = OMDamage.Where(x => x.EmpId == id)
                .Select(x => x.SumDamage)
                .Select(x => x.CalculDamage)
                .ExecuteFirstOrDefault();

            if (omDamage == null) throw new Exception($"Сущность не найдена ИД={id}");

            omDamage.DamageAmountStatus_Code = StatusDamageAmount.SendToCheck;

            omDamage.Save();
        }

        public List<DamageAnalysisContractDto> GetDamageContracts(long damageId, decimal? area = null)
        {
            OMDamage damage = OMDamage.Where(x => x.EmpId == damageId)
                .Select(x => x.ObjId)
                .Select(x => x.DamageData)
                .Select(x => x.NomDate)
                .Select(x => x.PartInsur)
                .Select(x => x.ObjId)
                .Select(x => x.TypeDoc)
                .Select(x => x.TypeDoc_Code)
                .Select(x => x.AdditionalData)
                .ExecuteFirstOrDefault();

            if (damage == null)
            {
                throw new Exception($"Не найдено дело по ущербу с идентификатором {damageId}");
            }

            if (!damage.ObjId.HasValue)
            {
                return new List<DamageAnalysisContractDto>();
            }

            if (!area.HasValue)
            {
                switch (damage.TypeDoc_Code)
                {
                    case ContractType.Dwelling:
                        area = OMFlat.Where(x => x.EmpId == damage.ObjId).Select(x => x.Fopl).Execute().FirstOrDefault()?.Fopl;
                        break;
                    case ContractType.CommonOwnership:
                        area = OMBuilding.Where(x => x.EmpId == damage.ObjId).Select(x => x.Opl).ExecuteFirstOrDefault()?.Opl;
                        break;
                }
            }

            switch (damage.TypeDoc_Code)
            {
                case ContractType.Dwelling:
                    DamageAnalysisAdditionalDataDto additionalData = damage.AdditionalData.IsNotEmpty() ?
                        JsonConvert.DeserializeObject<DamageAnalysisAdditionalDataDto>(damage.AdditionalData) :
                        null;
                    return GetContractsFromDwelling(damage.ObjId.Value, damage.DamageData, damage.NomDate, damage.PartInsur, area, additionalData);
                case ContractType.CommonOwnership:
                    return GetContractsFromCommonOwnership(damage.ObjId.Value, damage.DamageData,damage.EmpId);
                default:
                    return new List<DamageAnalysisContractDto>();
            }
        }

        /// <summary>
        /// Копирует дело по ущербу
        /// </summary>
        /// <param name="damageId">Идентификатор для копирования</param>
        /// <param name="damageDate">Дата ущерба  для копирования</param>
        public void Copy(long damageId, DateTime? damageDate,
            CausesOfDamageGP? causesOfDamageGp, CausesOfDamageOI? causesOfDamageOI,
            bool generateNewNumber = false)
        {
            DocumentsService documentsService = new DocumentsService();

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                OMDamage damage = OMDamage.Where(x => x.EmpId == damageId).SelectAll().ExecuteFirstOrDefault();

                if (damage == null)
                {
                    throw new Exception($"Не удалось определить дело по ущербу для копирования по идентификатору {damageId}");
                }

                if (!damage.ObjId.HasValue)
                {
                    throw new Exception($"Не удалось определить объект для дела по ущербу с идентификатором {damageId}");
                }

                //CIPJS-422 Связь с объектом такая же
                //DAMAGE_DATE c формы подтверждения о копировании дела
                //Алгоритм формирования номера дела: Если дата страхового события нового дела = дате события,в деле, которое выбрано для создании копии
                //то номер нового дела = Номер дела, выбранного для копии - 1(или 2, 3, 4, по порядку)
                //Иначе просто стандартный нумератор дела - следующее по порядку
                //Дата дела в ГБУ -текущая дата
                //Сразу формируется таблица для расчета ущерба и в нем заполнены все атрибуты для заполнения таблицы
                OMDamage damageCopy = new OMDamage();
                damageCopy.DateInput = DateTime.Now;
                damageCopy.DamageData = damageDate;
                damageCopy.TypeDoc_Code = damage.TypeDoc_Code;
                damageCopy.ObjReestrId = damage.ObjReestrId;
                damageCopy.ObjId = damage.ObjId;
                damageCopy.InsurOrgId = damage.InsurOrgId;
                damageCopy.PartInsur = damage.PartInsur;
                damageCopy.PartTown = damage.PartTown;
                damageCopy.NomDate = damage.NomDate;

                damageCopy.TypeBuild_Code = damage.TypeBuild_Code;
                damageCopy.TypeFloor_Code = damage.TypeFloor_Code;
                damageCopy.TypeCooker_Code = damage.TypeCooker_Code;
                damageCopy.EstimatedValue = damage.EstimatedValue;
                damageCopy.FlagSlygebka = damage.FlagSlygebka;
                damageCopy.CalcNote = damage.CalcNote;

                damageCopy.DamageAmountStatus_Code = StatusDamageAmount.Created;
                damageCopy.DateFill1 = DateTime.Now;
                damageCopy.AgreementId1 = SRDSession.GetCurrentUserId();

                if (causesOfDamageGp.HasValue)
                {
                    damageCopy.DamageReasonGP_Code = causesOfDamageGp.Value;
                }

                if (causesOfDamageOI.HasValue)
                {
                    damageCopy.DamageReasonOI_Code = causesOfDamageOI.Value;
                }

                long? baseDamageId = null;
                damageCopy.NomDoc = GetNewNomDoc(damageCopy.TypeDoc_Code, damageCopy.ObjId.Value, damageCopy.DamageData, out baseDamageId, damageCopy.DamageReasonGP_Code, damageCopy.DamageReasonOI_Code, generateNewNumber);
                damageCopy.LinkBaseDelo = damage.EmpId;

                int result = damageCopy.Save();
                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_DAMAGE, true, $"Скопировано дело по расчету суммы ущерба", OMDamage.GetRegisterId(), damageCopy.EmpId);

                CreateDocsForDamage(damageCopy.EmpId, damageCopy.TypeDoc_Code);

                //CIPJS-529 все комментарии, которые сохраняются по всей карточке
                //Копировать в блок Комментарии отдельными строками
                List<OMComment> damageComments = OMComment.Where(x => x.Id == damage.EmpId)
                      .SelectAll()
                      .Execute();
                foreach (OMComment damageComment in damageComments)
                {
                    new OMComment
                    {
                        LinkObjectId = damageCopy.EmpId,
                        LinkReestrId = damageComment.LinkReestrId,
                        DateCreate = damageComment.DateCreate,
                        Comment = damageComment.Comment,
                        UserId = damageComment.UserId
                    }.Save();
                }

                ts.Complete();
            }
        }

        public List<long> GetInvoiceIds(long damageId)
        {
            List<OMInvoice> oMInvoices = OMInvoice.Where(x => x.LinkDamage == damageId && x.Status_Code != InvoiceStatus.ErrorInDetails).SelectAll().Execute();
            List<OMInvoice> oMInvoiceWithoutDenied = oMInvoices.Where(x => x.Status_Code == InvoiceStatus.Denied || x.Status_Code == InvoiceStatus.DeniedAgreed).OrderBy(x => x.EmpId).ToList();
            List<OMInvoice> oMInvoiceFull = oMInvoices.Where(x => x.Status_Code != InvoiceStatus.DeniedAgreed && x.Status_Code != InvoiceStatus.Denied).OrderBy(x => x.EmpId).ToList();
            oMInvoiceFull.AddRange(oMInvoiceWithoutDenied);
            return oMInvoiceFull.Select(x => x.EmpId).ToList();
        }

        /// <summary>
        /// Получить список начислений МФЦ для помещения
        /// </summary>
        /// <param name="flatId">идентификатор помещения</param>
        /// <returns>список начислений</returns>
        public List<OMInputNach> GetOMInputNachesByFlat(long flatId)
        {
            List<OMInputNach> omInputNachs = OMInputNach.Where(x => x.ParentFsp.ObjId == flatId
                && x.ParentFsp.ObjReestrId == OMFlat.GetRegisterId()
                && x.TypeSource_Code == InsuranceSourceType.Mfc)
                .Select(x => x.PeriodRegDate)
                .Select(x => x.ParentFlatType.Name)
                .Select(x => x.ParentFlatStatus.Name)
                .Select(x => x.Kolgp)
                .Select(x => x.Fopl)
                .Select(x => x.Opl)
                .Select(x => x.ParentDistrict.ParentOkrug.ShortName)
                .Select(x => x.ParentDistrict.Name)
                .Select(x => x.Unom)
                .Select(x => x.Nom)
                .Select(x => x.AdresT)
                .Execute();

            DateTime? maxPeriod = omInputNachs.Max(x => x.PeriodRegDate);

            return omInputNachs.Where(x => x.PeriodRegDate == maxPeriod).ToList();
        }

        public bool CheckStrahPlat(decimal? sumDamage, decimal? partInsur, decimal? strahPlat)
        {
            if (sumDamage.HasValue && partInsur.HasValue && strahPlat.HasValue)
            {
                return Math.Round(sumDamage.Value * partInsur.Value / 100, 2, MidpointRounding.AwayFromZero) == strahPlat.Value;
            }

            return true;
        }

        public decimal? CalcTownPartSum(decimal? sumDamage, decimal? partTown)
        {
            if (sumDamage.HasValue && partTown.HasValue)
            {
                return Math.Round(sumDamage.Value * partTown.Value / 100, 2, MidpointRounding.AwayFromZero);
            }

            return null;
        }

        /// <summary>
        /// Поиск дубликатов.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckDublicate(DamageAnalysisCardDto model)
        {
            // CIPJS-821 поиск похожих дел с Дата СС и адрес
            List<OMDamage> damages = OMDamage.Where(x => (x.ShortAddress == model.Address || x.ObjId == model.ObjId) &&
                                                           x.DamageData == model.DamageData &&
                                                           x.EmpId != model.Id).Select(x => x.EmpId).Execute();
            return damages.Count > 0;
        }

        public decimal? CalcDamageAmount(decimal? materialDamage, decimal? correction,
            decimal? proportionReplacementCost, decimal? proportionDamagedArea, decimal? estimatedValue)
        {
            if (materialDamage.HasValue && proportionReplacementCost.HasValue &&
                proportionDamagedArea.HasValue && estimatedValue.HasValue)
            {
                return Math.Round(materialDamage.Value * (correction ?? 1) * proportionReplacementCost.Value *
                    proportionDamagedArea.Value * estimatedValue.Value * 0.000001m, 2, MidpointRounding.AwayFromZero);
            }

            return null;
        }

        public List<DamageAnalysisCardDto> GetByNomDoc(string nomDoc)
        {
            return OMDamage.Where(x => x.NomDoc.ToLower().Contains(nomDoc.ToLower())).SelectAll()
                .Select(x => x.NomDoc)
                .Execute().Select(x => new DamageAnalysisCardDto
                {
                    Id = x.EmpId,
                    NomDoc = x.NomDoc
                }).ToList();
        }

        public SetBaseDamageDto SetBaseDamage(long damageId, long? baseDamageId, string baseDamageNomDoc, bool isSetValueType)
        {
            SetBaseDamageDto setBaseDamageDto = new SetBaseDamageDto
            {
                DamageId = damageId,
                BaseDamageId = baseDamageId,
                BaseDamageNomDoc = baseDamageNomDoc
            };

            OMDamage damage = OMDamage.Where(x => x.EmpId == damageId)
                .SelectAll()
                .ExecuteFirstOrDefault();

            if (damage == null)
            {
                throw new Exception($"Не удалось определить дело по ущербу с идентификатором {damageId}");
            }

            if (isSetValueType)
            {
                if (!baseDamageId.HasValue && baseDamageNomDoc.IsNullOrEmpty())
                {
                    throw new Exception($"Не удалось установить значение базового дела, т.к. не было заполнено наименование базовго дела");
                }

                OMDamage baseDamage = null;

                if (baseDamageId.HasValue)
                {
                    baseDamage = OMDamage.Where(x => x.EmpId == baseDamageId.Value).Select(x => x.NomDoc).ExecuteFirstOrDefault();

                    if (baseDamage == null)
                    {
                        throw new Exception($"Не удалось определить базовое дело по ущербу с идентификатором {baseDamageId.Value}");
                    }

                    damage.LinkBaseDelo = baseDamage.EmpId;
                }

                //CIPJS-644 Так же в номер дела по ущербу , с которым работаем пользователь, 
                //добавить префикс -1 ( только префикс)
                damage.NomDoc = $"{damage.NomDoc}-1";
                damage.LinkBaseDelo = baseDamage != null ? (long?)baseDamage.EmpId : null;
                damage.BaseDelo = baseDamage != null ? baseDamage.NomDoc : baseDamageNomDoc;
            }
            else
            {
                Match match = Regex.Match(damage.NomDoc, _nomDocPattern);
                if (match.Success)
                {
                    damage.NomDoc = $"{match.Groups[1].Value}{match.Groups[2].Value}{match.Groups[3].Value}{match.Groups[4].Value}";
                }
                damage.LinkBaseDelo = null;
                damage.BaseDelo = null;
            }

            damage.Save();

            setBaseDamageDto.BaseDamageNomDoc = damage.BaseDelo;
            setBaseDamageDto.DamageNomDoc = damage.NomDoc;
            return setBaseDamageDto;
        }
        #region private methods
        /// <summary>
        /// Создание нового экземпляра
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private DamageAnalysisCardDto CreateEmptyCardModel(ContractType type)
        {
            return new DamageAnalysisCardDto
            {
                Id = -1,
                TypeCode = type,
                NomDate = DateTime.Now,
                Contracts = new List<DamageAnalysisContractDto>(),
                InsurOrgPayToDto = new List<DamageAnalysisPayToDto>(),
                StatusDoc = StatusDamageAmount.None,
                BuildingStructure = TypeBuildingStructure.None,
                Floors = TypeFloors.None,
                StoveType = StoveType.None,
                FloorType = FloorMaterial.None,

                CausesOfDamageGP = CausesOfDamageGP.None,
                SubReasonCausesOfDamage = SubReasonCausesOfDamage.None,
                RefinementSubReasonCOD = RefinementSubReasonCOD.None,
                CausesOfDamageOI = CausesOfDamageOI.None,
                CanEdit = true,
                PartInsur = (type == ContractType.CommonOwnership ? 75 : (decimal?)null),
                CalculatePersonDate = DateTime.Now
            };
        }

        private string GetNewNomDoc(ContractType contractType,
            long objId,
            DateTime? damageDate,
            out long? baseDamageId,
            CausesOfDamageGP causesOfDamageGP = CausesOfDamageGP.None,
            CausesOfDamageOI causesOfDamageOI = CausesOfDamageOI.None,
            bool generateNewNumber = false,
            string currentNomDoc = null)
        {
            OMBuilding building = null;
            OMFlat flat = null;
            baseDamageId = null;

            if (contractType == ContractType.CommonOwnership)
            {
                building = GetObjectFromCommonOwnership(objId);
            }
            else
            {
                flat = GetObjectFromDwelling(objId);
                building = flat?.ParentBuilding;
            }

            if (!generateNewNumber && damageDate.HasValue)
            {
                List<DamageAnalysisCardDto> sameObjectDamageList = null;

                if (contractType == ContractType.CommonOwnership && building != null)
                {
                    sameObjectDamageList = List(building.EmpId, contractType, damageDate, causesOfDamageGP, causesOfDamageOI);
                }
                else if (contractType == ContractType.Dwelling && flat != null)
                {
                    sameObjectDamageList = List(flat.EmpId, contractType, damageDate, causesOfDamageGP, causesOfDamageOI);
                }

                if (sameObjectDamageList != null && sameObjectDamageList.Count > 0)
                {
                    DamageAnalysisCardDto originalDoc = sameObjectDamageList.OrderBy(x => x.CalculatePersonDate).FirstOrDefault();
                    baseDamageId = originalDoc.Id;
                    string originalNumDoc = originalDoc.NomDoc;

                    if (originalNumDoc.IsNotEmpty())
                    {
                        Match match = new Regex(_nomDocPattern).Match(originalNumDoc);
                        if (match.Success)
                        {
                            return $"{match.Groups[1].Value}{match.Groups[2].Value}{match.Groups[3].Value}{match.Groups[4].Value}-{sameObjectDamageList.Count}";
                        }
                        //если совпадения с форматом номера нет, то берем полностью
                        else
                        {
                            return $"{originalNumDoc}-{sameObjectDamageList.Count}";
                        }
                    }
                    else
                    {
                        return $"{GetDefaultNomDoc(contractType, building)}-{sameObjectDamageList.Count}";
                    }
                }
            }

            return GetDefaultNomDoc(contractType, building, currentNomDoc);
        }

        private string GetDefaultNomDoc(ContractType contractType, OMBuilding building, string currentNomDoc = null)
        {
            OMBtiDistrict district = null;
            if (building != null)
            {
                district = building.ParentBtiDistrict ?? OMBtiDistrict.Where(x => x.Id == building.DistrictId).Select(x => x.CodeGivc).Execute().FirstOrDefault();
            }

            string mainNumber = null;

            if (currentNomDoc.IsNotEmpty())
            {
                Match match = Regex.Match(currentNomDoc, _nomDocPattern);

                if (match.Success)
                {
                    mainNumber = $"{match.Groups[2].Value}{match.Groups[3].Value}{match.Groups[4].Value}";
                }
            }

            if (mainNumber.IsNullOrEmpty())
            {
                mainNumber = Regex.Match(string.Format("00000000{0}{1}/{2}",
                   contractType == ContractType.CommonOwnership ? "ОИ" : contractType == ContractType.Dwelling ? "ЖП" : "",
                   contractType == ContractType.CommonOwnership ? Generator.GetRegNumber("", 3132) : contractType == ContractType.Dwelling ? Generator.GetRegNumber("", 3131) : "00000",
                   DateTime.Now.ToString("yy")), @"\w{2}\d{5}\/\d{2}").Value;
            }

            string nomDoc = $"{district?.CodeGivc ?? "0000"}-{mainNumber}";

            if (nomDoc.StartsWith("000000"))
            {
                string subNum = Regex.Match(nomDoc, @"\w{2}\d{5}\/\d{2}").Value;
                nomDoc = string.Format("10000000{0}", subNum);
            }

            return nomDoc;
        }

        /// <summary>
        /// Заполнение данных по делу, если тип дела - жилое помещение
        /// </summary>
        /// <param name="model"></param>
        private void GetObjectInfoFromDwelling(DamageAnalysisCardDto model, DamageAnalysisAdditionalDataDto additionalData = null)
        {
            if (!model.ObjId.HasValue)
            {
                return;
            }

            OMFlat flat = GetObjectFromDwelling(model.ObjId.Value);

            if (flat is null) return;

            model.Okrug = flat.ParentBuilding?.ParentBtiOkrug?.Name;
            model.District = flat.ParentBuilding?.ParentBtiDistrict?.Name;

            model.UNOM = flat.Unom?.ToString() ?? "";
            model.FlatNumber = flat.Kvnom;
            model.Area = flat.Fopl;
            model.Address = flat.ParentBuilding?.ParentAddress?.FullAddress ?? "";

            if (!model.PartInsur.HasValue || !model.PartTown.HasValue)
            {
                OMShareResponsibilityICCity shareResponsibilityICCity = GetShareResponsibilityFromDwelling(model.ObjId.Value, model.DamageData, model.NomDate);

                if (shareResponsibilityICCity != null)
                {
                    model.PartInsur = shareResponsibilityICCity.ICShare;
                    model.PartTown = shareResponsibilityICCity.CityShare;
                }
            }

            model.Contracts = GetContractsFromDwelling(model.ObjId.Value, model.DamageData, model.NomDate, model.PartInsur, model.Area, additionalData);

            if (flat != null)
            {
                OMInputNach inputNach = GetOMInputNachesByFlat(flat.EmpId).FirstOrDefault();
                model.FlatTypeName = inputNach?.ParentFlatType?.Name;
                model.FlatStatus = inputNach?.ParentFlatStatus?.Name;
            }

            if (!model.EstimatedValue.HasValue)
            {
                bool estimatedValueDifferent = false;
                model.EstimatedValue = CalcEstimatedValue(model.DamageData, model.Area, model.Contracts, out estimatedValueDifferent);
                model.EstimatedValueDifferent = estimatedValueDifferent;
            }
        }

        private OMFlat GetObjectFromDwelling(long objectId)
        {
            OMFlat flat = OMFlat.Where(x => x.EmpId == objectId)
                .Select(x => x.Fopl)
                .Select(x => x.TypeFlat_Code)
                .Select(x => x.Unom)
                .Select(x => x.Kvnom)
                .Select(x => x.LinkObjectMkd)
                .Select(x => x.ParentBuilding.OkrugId)
                .Select(x => x.ParentBuilding.DistrictId)
                .Select(x => x.ParentBuilding.ParentBtiDistrict.CodeGivc)
                .Select(x => x.ParentBuilding.ParentBtiDistrict.Name)
                .Select(x => x.ParentBuilding.ParentBtiOkrug.CodeGivc)
                .Select(x => x.ParentBuilding.ParentBtiOkrug.Name)
                .Select(x => x.ParentBuilding.ParentAddress.FullAddress)
                .Execute()
                .FirstOrDefault();

            if (flat == null)
            {
                return null;
            }

            if (flat.LinkObjectMkd.HasValue && flat.ParentBuilding == null)
            {
                flat.ParentBuilding = OMBuilding.Where(x => x.EmpId == flat.LinkObjectMkd.Value)
                    .Select(x => x.OkrugId)
                    .Select(x => x.DistrictId)
                    .Select(x => x.ParentBtiDistrict.CodeGivc)
                    .Select(x => x.ParentBtiDistrict.Name)
                    .Select(x => x.ParentBtiOkrug.CodeGivc)
                    .Select(x => x.ParentBtiOkrug.Name)
                    .Execute()
                    .FirstOrDefault();
            }

            return flat;
        }

        /// <summary>
        /// Заполнение данных по делу, если тип дела - жилое помещение
        /// </summary>
        /// <param name="model"></param>
        private void GetObjectInfoFromCommonOwnership(DamageAnalysisCardDto model/*, bool notChangeNom*/)
        {
            if (!model.ObjId.HasValue)
            {
                return;
            }
            OMBuilding building = GetObjectFromCommonOwnership(model.ObjId.Value);
            if (building == null)
            {
                return;
            }

            model.UNOM = building.Unom?.ToString() ?? "";
            model.CadastrNumber = building.CadasrNum?.ToString() ?? "";
            model.Okrug = building.ParentBtiOkrug?.Name;
            model.District = building?.ParentBtiDistrict?.Name;
            model.Address = building.ParentAddress?.FullAddress ?? "";

            if (!model.PartInsur.HasValue || !model.PartTown.HasValue)
            {
                model.PartInsur = 75m;
                model.PartTown = 25m;
            }

            model.Contracts = GetContractsFromCommonOwnership(model.ObjId.Value, model.DamageData);
        }

        private OMBuilding GetObjectFromCommonOwnership(long objectId)
        {
            return OMBuilding.Where(x => x.EmpId == objectId)
                .Select(x => x.Unom)
                .Select(x => x.CadasrNum)
                .Select(x => x.DistrictId)
                .Select(x => x.OkrugId)
                .Select(x => x.ParentBtiDistrict.CodeGivc)
                .Select(x => x.ParentBtiDistrict.Name)
                .Select(x => x.ParentBtiOkrug.CodeGivc)
                .Select(x => x.ParentBtiOkrug.Name)
                .Select(x => x.ParentAddress.FullAddress)
                .Execute()
                .FirstOrDefault();
        }

        /// <summary>
        /// Получение списка договоров по выбранному объекту
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="damageDate"></param>
        /// <param name="shareIC"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        private List<DamageAnalysisContractDto> GetContractsFromDwelling(long objId, DateTime? damageDate, DateTime? nomDate, decimal? shareIC, decimal? area, DamageAnalysisAdditionalDataDto additionalData = null)
        {
            List<DamageAnalysisContractDto> list = new List<DamageAnalysisContractDto>();

            List<OMFsp> fsps = OMFsp.Where(x => x.ObjId == objId && x.ObjReestrId == 317).SelectAll().Execute();

            foreach (OMFsp fsp in fsps)
            {
                string livingPremiseInsurCostCondition = GetLivingPremiseInsurCostCondition(fsp.FspType_Code);
                DamageAnalysisContractDto newData = null;
                switch (fsp.FspType_Code)
                {
                    case FSPType.EPD:
                        newData = new DamageAnalysisContractDto
                        {
                            FspId = fsp.EmpId
                        };
                        newData.ContractType = "ЕПД";
                        newData.ContractNumber = fsp.Kodpl.ParseToString();
                        newData.AreaByContract = fsp.OplKodpl;
                        newData.DateBegin = fsp.DateOpen?.ToString("dd.MM.yyyy") ?? "";

                        if (damageDate.HasValue)
                        {
                            OMPolicySvd policy = _fspService.GetPolicyByDate(fsp.EmpId, damageDate.Value);

                            if (policy != null)
                            {
                                newData.ContractNumber = $"{fsp.Kodpl}{(policy.Npol.IsNotEmpty() ? $" / {policy.Npol}" : string.Empty)}";
                                livingPremiseInsurCostCondition = GetLivingPremiseInsurCostCondition(fsp.FspType_Code, policy.Pralt_Code);
                            }
                        }

                        break;
                    case FSPType.Polis:
                    case FSPType.Svidetelstvo:
                        if (damageDate.HasValue)
                        {
                            OMPolicySvd policy = _fspService.GetPolicyByDate(fsp.EmpId, damageDate.Value);

                            if (policy != null)
                            {
                                newData = new DamageAnalysisContractDto
                                {
                                    FspId = fsp.EmpId
                                };
                                newData.ContractType = fsp.FspType_Code == FSPType.Polis ? "Полис" : "Свидетельство";
                                newData.ContractNumber = $"{(fsp.Kodpl.IsNotEmpty() ? fsp.Kodpl : string.Empty)}{(fsp.Kodpl.IsNotEmpty() && policy.Npol.IsNotEmpty() ? " / " : string.Empty)}{(policy.Npol.IsNotEmpty() ? policy.Npol : string.Empty)}";
                                newData.DateBegin = policy.Dat?.ToString("dd.MM.yyyy") ?? "";
                                newData.DateEnd = policy.Dat.HasValue ? policy.Dat.Value.AddYears(1).AddDays(-1).ToString("dd.MM.yyyy") : "";
                                newData.AreaByContract = policy.Opl;

                                livingPremiseInsurCostCondition = GetLivingPremiseInsurCostCondition(fsp.FspType_Code, policy.Pralt_Code);
                            }
                        }
                        break;
                }

                if (newData != null)
                {
                    newData.PartInRoom = area.HasValue && newData.AreaByContract.HasValue ? Math.Round((newData.AreaByContract / area).Value, 2) : (decimal?)null;

                    if (damageDate.HasValue)
                    {
                        // получаем дату - 31 декабря предыдущего года и от нее пытаемся найти нужный год
                        DateTime startYearDate = new DateTime(damageDate.Value.Year - 1, 12, 31);

                        decimal? costValue = additionalData?.Contracts?.FirstOrDefault(x => x.FspId == newData.FspId.Value)?.InsurCost;

                        decimal? defaultCostValue = OMLivingPremiseInsurCost
                            .Where(x => x.Condition == livingPremiseInsurCostCondition && x.DateBegin >= startYearDate)
                            .OrderBy(x => x.DateBegin)
                            .Select(x => x.Value)
                            .Execute()
                            .FirstOrDefault()?.Value;

                        if (costValue == null)
                        {
                            costValue = defaultCostValue;
                        }

                        newData.InsurCostChanged = costValue != defaultCostValue;

                        if (costValue.HasValue)
                        {
                            newData.InsurCost = costValue.Value;
                            newData.CalcCost = costValue.Value * newData.AreaByContract;
                            if (newData.CalcCost.HasValue)
                            {
                                newData.CalcCost = Math.Round((costValue.Value * newData.AreaByContract).Value, 2);
                            }
                            newData.InsurSum = shareIC / 100 * newData.CalcCost;
                            if (newData.InsurSum.HasValue)
                            {
                                newData.InsurSum = Math.Round((shareIC / 100 * newData.CalcCost).Value, 2);
                            }
                        }
                        else
                        {
                            newData.InsurCost = null;
                            newData.CalcCost = null;
                            newData.InsurSum = null;
                        }

                        DateTime startMonthDate = new DateTime(damageDate.Value.Year, damageDate.Value.Month, 1);
                        OMBalance balance = OMBalance.Where(x => x.FspId == fsp.EmpId && x.PeriodRegDate == startMonthDate).Select(x => x.FlagInsur).Execute().FirstOrDefault();
                        newData.IsInsured = balance == null ? false : balance.FlagInsur.HasValue ? balance.FlagInsur.Value : false;
                    }

                    newData.Url = string.Format("/ObjectCard?ObjId={0}&RegisterViewId=FspEpd&isVertical=true&useMasterPage=true", fsp.EmpId);

                    //если мы определили что период застрахован , то СРАЗУ АВТОМАТОМ  " Период оплачен =ПЛЮС , а вот если НЕ застрахован то тогда МИНУС
                    newData.IsPaid = newData.IsInsured || (newData.FspId.HasValue && additionalData != null && additionalData.Contracts != null &&
                        additionalData.Contracts.Any(x => x.FspId == newData.FspId.Value && x.IsPaid));

                    list.Add(newData);
                }
            }

            return list;
        }

        /// <summary>
        /// Получение списка договоров по выбранному объекту
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="damageDate"></param>
        /// <returns></returns>
        private List<DamageAnalysisContractDto> GetContractsFromCommonOwnership(long objId, DateTime? damageDate, long? damageId = null)
        {
            List<DamageAnalysisContractDto> list = new List<DamageAnalysisContractDto>();

            List<OMAllProperty> properties = OMAllProperty
                .Where(x => x.ObjId == objId)
                .Select(x => x.Ndogdat)
                .Select(x => x.Ndog)
                .Select(x => x.Paysign)
                .Select(x => x.RasPripay)
                .Select(x => x.FspId)
                .Execute();
            foreach (OMAllProperty prop in properties)
            {
                if (prop != null && prop.Ndogdat.HasValue && prop.Ndogdat <= damageDate && prop.Ndogdat.Value.AddYears(1) >= damageDate)
                {
                    decimal? sumOpl = OMInputPlat.Where(x => x.LinkAllPropertyId == prop.EmpId).Select(x => x.SumOpl).Execute()?.Sum(x => x.SumOpl);
                    var newData = new DamageAnalysisContractDto
                    {
                        FspId = prop.FspId,
                        AllPropertyId = prop.EmpId,
                        ContractType = "Свидетельство",
                        ContractNumber = prop.Ndog,
                        Paysign = prop.Paysign,
                        DateBegin = prop.Ndogdat.HasValue ? prop.Ndogdat.Value.ToString("dd.MM.yyyy") : "",
                        DateEnd = prop.Ndogdat.HasValue ? prop.Ndogdat.Value.AddYears(1).AddDays(-1).ToString("dd.MM.yyyy") : "",
                        IsInsured = prop.RasPripay.HasValue && sumOpl.HasValue && prop.RasPripay <= sumOpl,
                        IsPaid = prop.RasPripay.HasValue && sumOpl.HasValue && prop.RasPripay <= sumOpl,
                        Url = $"/ObjectCard?ObjId={prop.EmpId}&RegisterViewId=Contracts&isVertical=true&useMasterPage=true",
                        DamageId = damageId
                    };
                    list.Add(newData);
                }
            }

            return list;
        }

        /// <summary>
        /// Получение списка о выплатах СК
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<DamageAnalysisPayToDto> GetInsurOrgPayTo(long? id)
        {
            List<DamageAnalysisPayToDto> list = new List<DamageAnalysisPayToDto>();

            if (id.HasValue)
            {
                List<OMPayTo> pays = OMPayTo.Where(x => x.LinkDamageId == id).SelectAll().Execute();
                foreach (var pay in pays)
                {
                    DamageAnalysisPayToDto elem = new DamageAnalysisPayToDto
                    {
                        FactNumber = pay.Factnumber,
                        FactDate = pay.Factdate,
                        SpFact = pay.SpFact,
                        SpNo = pay.SpNo
                    };

                    list.Add(elem);
                }
            }

            return list;
        }

        private void CreateDocsForDamage(long damageId, ContractType type)
        {
            List<OMDocBaseType> docType = null;
            string[] docs = null;
            switch (type)
            {
                case ContractType.Dwelling:
                    docs = new string[]
                    {
                            "Сопроводительный документ",
                            "Заявление в ГЦИПиЖС о повреждении ЖП",
                            "Страховой акт",
                            "Заявление в СК о повреждении застрахованного ЖП",
                            "Акт осмотра СК",
                            "Таблица расчета ущерба",
                            "Акт УК",
                            "Сведения, подтверждающие оплату страхового взноса",
                            "Правоустанавливающий документ на ЖП",
                            "Паспорт РФ",
                            "Банковские реквизиты",
                            "Страховой полис/свидетельство ЖП"
                    };
                    docType = OMDocBaseType.Where(x => docs.Contains(x.DocumentBase) && x.Type == "ЖП").Select(x => x.DocumentBase).OrderBy(x => x.DocumentBase).Execute();
                    break;
                case ContractType.CommonOwnership:
                    docs = new string[]
                    {
                            "Сопроводительный документ (опись предоставляемых документов)",
                            "Заявление в ГЦИП и ЖС о повреждении застрахованного жил. помещения",
                            "Страховой акт",
                            "Заявление в СК о повреждении застрахованного жил. помещения",
                            "Полис добровольного страховании объектов общего имущества",
                            "Платежный документ, подтверждающий уплату страховой премии (один или несколько)",
                            "Банковские реквизиты страхователя",
                            "Акт осмотра объекта общего имущества",
                            "Акт Управляющей компании (компетентного орг-ции)",
                            "Свидетельство о внесении в ЕГР юридических лиц",
                            "Свидетельство о постановке на учет в налоговую орг-цию"
                    };
                    docType = OMDocBaseType.Where(x => docs.Contains(x.DocumentBase) && x.Type == "ОИ").Select(x => x.DocumentBase).OrderBy(x => x.DocumentBase).Execute();
                    break;
            }
            if (docType != null)
            {
                using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
                {
                    foreach (OMDocBaseType elem in docType)
                    {
                        OMDocuments doc = OMDocuments.CreateEmpty();
                        doc.ObjId = damageId;
                        doc.ReestrId = OMDamage.GetRegisterId();
                        doc.DocTypeId = elem.Id;
                        doc.DocIsHave = false;
                        doc.DocTypeM_Code = TypeDocBaseCase.None;
                        doc.Save();
                    }
                    ts.Complete();
                }
            }
        }

        private decimal? CalcEstimatedValue(DateTime? damageDate, decimal? area, List<DamageAnalysisContractDto> contracts, out bool estimatedValueDifferent)
        {
            estimatedValueDifferent = false;

            if (!damageDate.HasValue || !area.HasValue || area.Value == 0)
            {
                return 0;
            }

            decimal? insurCostValue = null;
            string livingPremiseInsurCostCondition = GetLivingPremiseInsurCostCondition();

            if (contracts != null
                && contracts.Count > 0
                && contracts.Any(x => x.FspId.HasValue && x.IsPaid))
            {
                DamageAnalysisContractDto paidContract = contracts.FirstOrDefault(x => x.FspId.HasValue && x.IsPaid);
                OMFsp fsp = OMFsp.Where(x => x.EmpId == paidContract.FspId.Value)
                    .Select(x => x.FspType)
                    .Select(x => x.FspType_Code)
                    .Select(x => x.ContractId)
                    .ExecuteFirstOrDefault();

                if (fsp != null)
                {
                    if (fsp.FspType_Code == FSPType.Polis
                        || fsp.FspType_Code == FSPType.Svidetelstvo)
                    {
                        OMPolicySvd policy = OMPolicySvd.Where(x => x.EmpId == fsp.ContractId)
                            .Select(x => x.Pralt)
                            .Select(x => x.Pralt_Code)
                            .Execute()
                            .FirstOrDefault();

                        if (policy != null)
                        {
                            livingPremiseInsurCostCondition = GetLivingPremiseInsurCostCondition(fsp.FspType_Code, policy.Pralt_Code);
                        }
                        else
                        {
                            livingPremiseInsurCostCondition = GetLivingPremiseInsurCostCondition(fsp.FspType_Code);
                        }
                    }
                    else
                    {
                        livingPremiseInsurCostCondition = GetLivingPremiseInsurCostCondition(fsp.FspType_Code);
                    }
                }

                insurCostValue = paidContract.InsurCost;
            }

            // получаем дату - 31 декабря предыдущего года и от нее пытаемся найти нужный год
            DateTime startYearDate = new DateTime(damageDate.Value.Year - 1, 12, 31);

            OMLivingPremiseInsurCost currentlivingPremiseInsurCost = OMLivingPremiseInsurCost
                        .Where(x => x.Condition == livingPremiseInsurCostCondition && x.DateBegin >= startYearDate)
                        .OrderBy(x => x.DateBegin)
                        .Select(x => x.Value)
                        .Execute()
                        .FirstOrDefault();

            if (insurCostValue == null)
            {
                insurCostValue = currentlivingPremiseInsurCost.Value;
            }

            if (insurCostValue != currentlivingPremiseInsurCost.Value)
            {
                estimatedValueDifferent = true;
            }

            return (insurCostValue ?? 0) * area.Value;
        }

        private string GetLivingPremiseInsurCostCondition(FSPType fspType = FSPType.None, InsuranceTerms insuranceTerms = InsuranceTerms.None)
        {
            if (fspType == FSPType.Polis && insuranceTerms == InsuranceTerms.Special)
            {
                return "ЖП_Индивид";
            }

            return "ЖП_Все";
        }

        private OMShareResponsibilityICCity GetShareResponsibilityFromDwelling(long objId, DateTime? damageDate, DateTime? nomDate)
        {
            DateTime dateBegin = nomDate ?? DateTime.Now;

            List<OMFsp> fsps = OMFsp.Where(x => x.ObjId == objId && x.ObjReestrId == 317).SelectAll().Execute();

            OMPolicySvd policy = null;
            if (damageDate.HasValue)
            {
                foreach (OMFsp fsp in fsps)
                {
                    policy = _fspService.GetPolicyByDate(fsp.EmpId, damageDate.Value);

                    if (policy != null)
                    {
                        break;
                    }
                }
            }

            if (policy != null && policy.Pralt_Code == InsuranceTerms.Special)
            {
                return OMShareResponsibilityICCity.Where(x => x.Type == "ЖП_Индивид" && x.DateBegin <= dateBegin)
                       .Select(x => x.ICShare).Select(x => x.CityShare).OrderByDescending(x => x.DateBegin).ExecuteFirstOrDefault();
            }
            else
            {
                return OMShareResponsibilityICCity.Where(x => x.Type == "ЖП_Все" && x.DateBegin <= dateBegin)
                       .Select(x => x.ICShare).Select(x => x.CityShare).OrderByDescending(x => x.DateBegin).ExecuteFirstOrDefault();
            }
        }
        #endregion
    }
}