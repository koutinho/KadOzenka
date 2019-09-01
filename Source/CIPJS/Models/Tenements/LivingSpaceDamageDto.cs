using ObjectModel.Insur;
using System;

namespace CIPJS.Models.Tenements
{
    public class LivingSpaceDamageDto
    {
        public string CaseNumber { get; set; }
        
        public DateTime? CaseDate { get; set; }

        public string InsuranceCompany { get; set; }

        public DateTime? DamageDate { get; set; }

        public decimal? DamageAmount { get; set; }

        public string DamageReason { get; set; }

        public long? DamageId { get; set; }

        public static LivingSpaceDamageDto Map(OMDamage omDamage)
        {
            return new LivingSpaceDamageDto
            {
                CaseNumber = omDamage.NomDoc,
                CaseDate = omDamage.NomDate,
                InsuranceCompany = omDamage.ParentInsuranceOrganization?.FullName,
                DamageDate = omDamage.DamageData,
                DamageAmount = omDamage.SumDamage,
                DamageReason = omDamage.DamageReasonGP,
                DamageId = omDamage.EmpId
            };
        }
    }
}