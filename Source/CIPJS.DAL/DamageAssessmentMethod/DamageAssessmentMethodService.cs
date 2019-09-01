using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.DAL.DamageAssessmentMethod
{
    public class DamageAssessmentMethodService
    {
        private DamageAssessmentMethodDto CreateEmpty()
        {
            return new DamageAssessmentMethodDto
            {
                Id = -1
            };
        }

        public DamageAssessmentMethodDto Get(long? id)
        {
            if (id.HasValue)
            {
                OMDamageAssessmentMethod damageAssessmentMethod = OMDamageAssessmentMethod.Where(x => x.Id == id).SelectAll().Execute().FirstOrDefault();
                if (damageAssessmentMethod != null)
                {
                    return new DamageAssessmentMethodDto
                    {
                        Id = damageAssessmentMethod.Id,
                        MaterialDamageRange = damageAssessmentMethod.MaterialDamage,
                        DamageSymptom = damageAssessmentMethod.DamageSymptom,
                        ElementConstruction = damageAssessmentMethod.ElementConstruction,
                        WorkComposition = damageAssessmentMethod.WorkComposition,
                        MaterialDamageMin = damageAssessmentMethod.MaterialDamageMin,
                        MaterialDamageMax = damageAssessmentMethod.MaterialDamageMax
                    };
                }
            }

            return CreateEmpty();
        }

        public DamageAssessmentMethodDto GetWithLoad(long? id, long? damageRecordId)
        {
            if (id.HasValue)
            {
                OMDamageAssessmentMethod damageAssessmentMethod = OMDamageAssessmentMethod.Where(x => x.Id == id).SelectAll().Execute().FirstOrDefault();
                OMDamageAmount amount = OMDamageAmount.Where(x => x.EmpId == damageRecordId).Select(x => x.MaterialDamage).Select(x => x.ProportionDamagedArea).Execute().FirstOrDefault();
                if (damageAssessmentMethod != null)
                {
                    return new DamageAssessmentMethodDto
                    {
                        Id = damageAssessmentMethod.Id,
                        MaterialDamage = amount?.MaterialDamage,
                        MaterialDamageRange = damageAssessmentMethod.MaterialDamage,
                        DamageSymptom = damageAssessmentMethod.DamageSymptom,
                        ElementConstruction = damageAssessmentMethod.ElementConstruction,
                        WorkComposition = damageAssessmentMethod.WorkComposition,
                        MaterialDamageMin = damageAssessmentMethod.MaterialDamageMin,
                        MaterialDamageMax = damageAssessmentMethod.MaterialDamageMax,
                        ProportionDamagedArea = amount?.ProportionDamagedArea ?? 0
                    };
                }
            }

            return CreateEmpty();
        }

        public List<DamageAssessmentTitleDto> GetTitlestByElemConstructionCode(DamageAssessmentMethodGridSelectDto model)
        {
            if (model.Code.HasValue)
            {
                if(model.RefId.HasValue && model.RefItemId.HasValue)
                {
                    return OMDamageAssessmentMethod
                        .Where(x => x.ElementConstruction_Code == (ElementsOfConstructions)model.Code && x.RefId == model.RefId && x.RefItemId == model.RefItemId)
                        .Select(x => x.ElementConstructionDescription)
                        .Execute()
                        .Select(x => x.ElementConstructionDescription)
                        .Distinct()
                        .Select(x => new DamageAssessmentTitleDto { Title = x })
                        .ToList();
                }
                return OMDamageAssessmentMethod
                    .Where(x => x.ElementConstruction_Code == (ElementsOfConstructions)model.Code)
                    .Select(x => x.ElementConstructionDescription)
                    .Execute()
                    .Select(x => x.ElementConstructionDescription)
                    .Distinct()
                    .Select(x => new DamageAssessmentTitleDto { Title = x })
                    .ToList();
            }

            return null;
        }

        public List<DamageAssessmentMethodDto> GetElemConstructionCode(string elemConstrutTitle)
        {
            if (elemConstrutTitle.IsNotEmpty())
            {
                return OMDamageAssessmentMethod
                    .Where(x => x.ElementConstructionDescription == elemConstrutTitle)
                    .Select(x => x.DamageSymptom)
                    .Select(x => x.MaterialDamage)
                    .Select(x => x.MaterialDamageMin)
                    .Select(x => x.MaterialDamageMax)
                    .Execute()
                    .Select(x => new DamageAssessmentMethodDto {
                        Id = x.Id,
                        MaterialDamageRange = x.MaterialDamage,
                        DamageSymptom = x.DamageSymptom,
                        MaterialDamageMin = x.MaterialDamageMin,
                        MaterialDamageMax = x.MaterialDamageMax
                    })
                    .OrderBy(x => x.MaterialDamageMin)
                    .ToList();
            }

            return null;
        }
    }
}
