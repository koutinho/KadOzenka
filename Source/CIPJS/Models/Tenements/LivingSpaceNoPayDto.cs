using ObjectModel.Insur;
using System;

namespace CIPJS.Models.Tenements
{
    public class LivingSpaceNoPayDto
    {
        public string InsuranceCompany { get; set; }

        public string ContractNumber { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EventDate { get; set; }

        public string Reason { get; set; }

        public static LivingSpaceNoPayDto Map(OMNoPay omNoPay)
        {
            return new LivingSpaceNoPayDto
            {
                InsuranceCompany = omNoPay.ParentInsuranceOrganization?.FullName,
                ContractNumber = omNoPay.Npol,
                BeginDate = omNoPay.Npoldate,
                EventDate = omNoPay.Eventdat,
                Reason = omNoPay.Reject
            };
        }
    }
}