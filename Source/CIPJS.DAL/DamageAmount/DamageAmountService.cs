using CIPJS.DAL.DamageAssessmentMethod;
using Core.RefLib;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.DAL.DamageAmount
{
    public class DamageAmountService
    {
        private DamageAssessmentMethodService _damService;

        public DamageAmountService()
        {
            _damService = new DamageAssessmentMethodService();
        }

        /// <summary>
        /// Получение записей для расчета ущерба дела
        /// </summary>
        /// <param name="damageId"></param>
        /// <returns></returns>
        public List<DamageAmountDto> GetDamageAmountData(long damageId)
        {
            List<DamageAmountDto> dtoList = new List<DamageAmountDto>();
            List <OMDamageAmount> oMDamageAmounts = OMDamageAmount.Where(x => x.DamageId == damageId).SelectAll().Execute();
            OMDamage damage = OMDamage.Where(x => x.EmpId == damageId)
                .Select(x => x.TypeFloor_Code)
                .Select(x => x.TypeBuild_Code)
                .Select(x => x.TypeCooker_Code)
                .Execute().FirstOrDefault();
            foreach (var elem in ReferencesCommon.GetItems<ElementsOfConstructions>(addEmptyValue: false).OrderBy(x => x.ItemId))
            {
                var damageAmount = oMDamageAmounts.FirstOrDefault(x => x.ElementConstruction_Code == (ElementsOfConstructions)elem.ItemId);
                if (damageAmount == null)
                {
                    damageAmount = new OMDamageAmount
                    {
                        DamageId = damageId,
                        ElementConstruction_Code = (ElementsOfConstructions)elem.ItemId
                    };
                    if (damage != null &&
                        damage.TypeCooker_Code != StoveType.None && damage.TypeBuild_Code != BuildingType.None && damage.TypeFloor_Code != FloorMaterial.None)
                    {
                        OMIntegrateIndicatorsReplecmentCost indicator = OMIntegrateIndicatorsReplecmentCost.Where(x => x.ElementsConstructions_Code == (ElementsOfConstructions)elem.ItemId &&
                        x.FloorMaterial_Code == damage.TypeFloor_Code && x.BuildingType_Code == damage.TypeBuild_Code && x.StoveType_Code == damage.TypeCooker_Code)
                        .Select(x => x.CostValue)
                        .Select(x => x.HasChilds)
                        .Execute()
                        .FirstOrDefault();
                        if (indicator != null && !indicator.HasChilds.Value)
                        {
                            damageAmount.ProportionReplacementCost = indicator.CostValue;
                            //CIPJS-385 «Поправочный коэффициент» , по умолчанию =1
                            damageAmount.Correction = 1;
                        }
                    }

                    damageAmount.Save();
                }
                dtoList.Add(new DamageAmountDto
                {
                    Id = damageAmount.EmpId,
                    DamageId = damageAmount.DamageId,
                    DamageAssessmentMethodId = damageAmount.DamageAssessmentMethodId,
                    ElementOfConstruction = damageAmount.ElementConstruction,
                    ElementOfConstruction_Code = damageAmount.ElementConstruction_Code,
                    MaterialDamageRange = damageAmount.DamageAssessmentMethodId.HasValue ?
                        OMDamageAssessmentMethod.Where(x => x.Id == damageAmount.DamageAssessmentMethodId.Value)
                            .Select(x => x.MaterialDamage).ExecuteFirstOrDefault()?.MaterialDamage : null,
                    MaterialDamage = damageAmount.MaterialDamage,
                    ProportionReplacementCost = damageAmount.ProportionReplacementCost,
                    ProportionDamagedArea = damageAmount.ProportionDamagedArea,
                    DamageAmount = damageAmount.DamageAmount,
                    Correction = damageAmount.Correction
                });
            }

            return dtoList;
        }

        /// <summary>
        /// Сохранение изменений запсией расчета ущерба
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DamageAmountDto UpdateDamageAmount(DamageAmountDto model)
        {
            OMDamageAmount oMDamageAmount = OMDamageAmount.Where(x => x.EmpId == model.Id).SelectAll().Execute().FirstOrDefault();
            if (oMDamageAmount != null)
            {
                if(!model.DamageAssessmentMethodId.HasValue && model.MaterialDamage.HasValue)
                {
                    // из-за ввода ручного варианта заполнения, пытаемся сами определить DamageAssessmentMethodId
                    DamageAssessmentMethodService serviceDAM = new DamageAssessmentMethodService();
                    List<DamageAssessmentTitleDto> titles = serviceDAM.GetTitlestByElemConstructionCode(new DamageAssessmentMethodGridSelectDto { Code = (long)oMDamageAmount.ElementConstruction_Code });
                    if(titles.Count == 1)
                    {
                        List<DamageAssessmentMethodDto> methods = serviceDAM.GetElemConstructionCode(titles[0].Title);
                        model.DamageAssessmentMethodId = methods.FirstOrDefault(x => x.MaterialDamageMin <= model.MaterialDamage && x.MaterialDamageMax >= model.MaterialDamage)?.Id;
                    }
                    //CIPJS-342 Нужно в материал пола добавить отдельно новую строку "Полы из рулонных материалов" , а строку "Полы из линолеума и ламината" = "Полы из ламината"
                    //На основании типа пола, сразу определять строчку для определения материального ущерба(см вложение)
                    //Аналогично, если пользователь заполняет таблицу без выбора соответствующих строк в методике, можно сразу 
                    //понять для пола какая строка используется в методике чтобы понять какое значение выводить в таблицу "Информация о повреждениях"
                    else if (oMDamageAmount.ElementConstruction_Code == ElementsOfConstructions.Floors)
                    {
                        OMDamage oMDamage = OMDamage.Where(x => x.EmpId == model.DamageId)
                            .Select(x => x.TypeFloor)
                            .Select(x => x.TypeFloor_Code)
                            .ExecuteFirstOrDefault();
                        
                        if (oMDamage != null)
                        {
                            switch(oMDamage.TypeFloor_Code)
                            {
                                case FloorMaterial.LaminateFlooring:
                                    OMDamageAssessmentMethod laminateFlooringMethod = OMDamageAssessmentMethod.Where(x => x.ElementConstruction_Code == ElementsOfConstructions.Floors
                                        && x.ElementConstructionDescription == "Оценка материального ущерба полов из ламината"
                                        && x.MaterialDamageMin <= model.MaterialDamage && x.MaterialDamageMax >= model.MaterialDamage).Select(x => x.MaterialDamage).ExecuteFirstOrDefault();
                                    model.DamageAssessmentMethodId = laminateFlooringMethod?.Id;
                                    model.MaterialDamageRange = laminateFlooringMethod?.MaterialDamage;
                                    break;
                                case FloorMaterial.ParquetFlooring:
                                    OMDamageAssessmentMethod parquetFlooringMethod = OMDamageAssessmentMethod.Where(x => x.ElementConstruction_Code == ElementsOfConstructions.Floors
                                        && x.ElementConstructionDescription == "Оценка материального ущерба полов паркетных"
                                        && x.MaterialDamageMin <= model.MaterialDamage && x.MaterialDamageMax >= model.MaterialDamage).Select(x => x.MaterialDamage).ExecuteFirstOrDefault();
                                    model.DamageAssessmentMethodId = parquetFlooringMethod?.Id;
                                    model.MaterialDamageRange = parquetFlooringMethod?.MaterialDamage;
                                    break;
                                case FloorMaterial.RollMaterialsFlooring:
                                    OMDamageAssessmentMethod rollMaterialsFlooringMethod = OMDamageAssessmentMethod.Where(x => x.ElementConstruction_Code == ElementsOfConstructions.Floors
                                        && x.ElementConstructionDescription == "Оценка материального ущерба полов из рулонных материалов"
                                        && x.MaterialDamageMin <= model.MaterialDamage && x.MaterialDamageMax >= model.MaterialDamage).Select(x => x.MaterialDamage).ExecuteFirstOrDefault();
                                    model.DamageAssessmentMethodId = rollMaterialsFlooringMethod?.Id;
                                    model.MaterialDamageRange = rollMaterialsFlooringMethod?.MaterialDamage;
                                    break;
                                case FloorMaterial.WoodFlooring:
                                    OMDamageAssessmentMethod woodFlooringMethod = OMDamageAssessmentMethod.Where(x => x.ElementConstruction_Code == ElementsOfConstructions.Floors
                                        && x.ElementConstructionDescription == "Оценка материального ущерба полов дощатых"
                                        && x.MaterialDamageMin <= model.MaterialDamage && x.MaterialDamageMax >= model.MaterialDamage).Select(x => x.MaterialDamage).ExecuteFirstOrDefault();
                                    model.DamageAssessmentMethodId = woodFlooringMethod?.Id;
                                    model.MaterialDamageRange = woodFlooringMethod?.MaterialDamage;
                                    break;
                            }
                        }
                    }
                }
                oMDamageAmount.DamageAssessmentMethodId = model.DamageAssessmentMethodId;
                oMDamageAmount.MaterialDamage = model.MaterialDamage;
                oMDamageAmount.ProportionDamagedArea = model.ProportionDamagedArea;
                oMDamageAmount.DamageAmount = model.DamageAmount;
                //CIPJS-385 «Поправочный коэффициент» , по умолчанию =1
                oMDamageAmount.Correction = model.Correction.HasValue? model.Correction.Value : 1m;
                oMDamageAmount.Save();

                return model;
            }
            throw new Exception("Объект не найден");
        }


        /// <summary>
        /// Получение списка
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<DamageAnalysisDamageAmountDto> GetDamageAmounts(long id)
        {
            List<DamageAnalysisDamageAmountDto> list = new List<DamageAnalysisDamageAmountDto>();

            List<OMDamageAmount> amounts = OMDamageAmount
                .Where(x => x.DamageId == id && x.MaterialDamage != null && x.DamageAssessmentMethodId != null)
                .Select(x => x.MaterialDamage)
                .Select(x => x.ParentDamageAssessmentMethod.ElementConstructionDescription)
                .Select(x => x.ParentDamageAssessmentMethod.DamageSymptom)
                .Select(x => x.ParentDamageAssessmentMethod.WorkComposition)
                .Select(x => x.ParentDamageAssessmentMethod.MaterialDamage)
                .Execute();
            foreach (var amount in amounts)
            {
                if (amount.ParentDamageAssessmentMethod != null)
                {
                    string elemOfConstr = amount.ParentDamageAssessmentMethod.ElementConstructionDescription.Replace("Оценка материального ущерба ", "");
                    elemOfConstr = elemOfConstr[0].ToString().ToUpper() + elemOfConstr.Substring(1);
                    DamageAnalysisDamageAmountDto elem = new DamageAnalysisDamageAmountDto
                    {
                        MaterialDamage = amount.MaterialDamage,
                        MaterialDamageRange = amount.ParentDamageAssessmentMethod.MaterialDamage,
                        ElementOfConstruction = elemOfConstr,
                        DamageSymptom = amount.ParentDamageAssessmentMethod.DamageSymptom,
                        WorkComposition = amount.ParentDamageAssessmentMethod.WorkComposition
                    };

                    list.Add(elem);
                }
            }

            return list;
        }
    }
}
