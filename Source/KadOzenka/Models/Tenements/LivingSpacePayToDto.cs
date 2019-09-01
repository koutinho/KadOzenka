using ObjectModel.Insur;
using System;

namespace CIPJS.Models.Tenements
{
    public class LivingSpacePayToDto
    {
        public string InsuranceCompany { get; set; }

        public string ContractNumber { get; set; }

        public DateTime? BeginDate { get; set; }

        public decimal? DamageAmount { get; set; }

        public decimal? RefundAmount { get; set; }

        public decimal? HeldPartAmount { get; set; }

        public string SkNumber { get; set; }

        public DateTime? SkDate { get; set; }

        public static LivingSpacePayToDto Map(OMPayTo omPayTo)
        {
            return new LivingSpacePayToDto
            {
                InsuranceCompany = omPayTo.ParentInsuranceOrganization?.FullName,
                ContractNumber = omPayTo.Npol,
                BeginDate = omPayTo.Npoldate,
                DamageAmount = omPayTo.Sl,
                RefundAmount = omPayTo.SpFact,
                HeldPartAmount = omPayTo.SpNo,
                SkNumber = omPayTo.Factnumber,
                SkDate = omPayTo.Factdate
            };
        }
    }
}