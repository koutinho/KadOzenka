using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.Models.DamageAnalysis
{
    public class CheckListDto
    {
        public long Id { get; set; }

        public DateTime? CreateDate { get; set; }

        public string Number { get; set; }

        public string SK { get; set; }

        public string FIO { get; set; }

        public DateTime? DateSS { get; set; }

        public decimal? Amount { get; set; }

        public decimal? CityPart { get; set; }

        public string DamageStatus { get; set; }

        public long? UNOM { get; set; }

        public string Address { get; set; }

        public string FlatNumber { get; set; }

        public string Status { get; set; }

        public string User { get; set; }

        public static CheckListDto Map(OMDamage omDamage, List<OMInvoice> omInvoices)
        {
            OMInvoice omInvoice = omInvoices.FirstOrDefault(x => x.LinkDamage == omDamage.EmpId);

            return new CheckListDto
            {
                Id = omDamage.EmpId,
                CreateDate = omDamage.DateFill1,
                Number = omDamage.NomDoc,
                SK = omDamage.ParentInsuranceOrganization?.ShortName,
                FIO = omInvoice?.SubjectName,
                DateSS = omDamage.DamageData,
                Amount = omDamage.SumDamage,
                CityPart = omInvoice?.SumOpl,
                DamageStatus = omDamage.DamageAmountStatus,
                UNOM = omDamage.ParentFlat?.Unom,
                Address = omDamage.ParentFlat?.ParentBuilding?.ParentAddress?.FullAddress,
                FlatNumber = omDamage.ParentFlat?.Kvnom,
                Status = omInvoice?.Status,
                User = omDamage.ParentUser?.FullName
            };
        }
    }
}