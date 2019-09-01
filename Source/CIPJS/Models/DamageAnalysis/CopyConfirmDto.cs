using CIPJS.DAL.DamageAnalysis;
using ObjectModel.Directory;
using System;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.DamageAnalysis
{
    public class CopyConfirmDto
    {
        public long Id { get; set; }

        public long? ObjId { get; set; }

        [Display(Name = "UNOM")]
        public string Unom { get; set; }

        [Display(Name = "Адрес объекта")]
        public string Address { get; set; }

        [Display(Name = "Номер квартиры")]
        public string FlatNumber { get; set; }

        [Display(Name = "Площадь квартиры")]
        public decimal? Area { get; set; }

        [Display(Name = "Дата страхового события")]
        public DateTime? DamageDate { get; set; }

        [Display(Name = "Тип дела")]
        public ContractType TypeCode { get; set; }

        [Display(Name = "Причина ущерба")]
        public CausesOfDamageGP CausesOfDamageGP { get; set; }

        [Display(Name = "Причина ущерба")]
        public CausesOfDamageOI CausesOfDamageOI { get; set; }

        public static CopyConfirmDto OMMap(DamageAnalysisCardDto damage)
        {
            if (!damage.Id.HasValue)
            {
                throw new ArgumentException("Не удалось определить идентификатор переданного дела по ущербу", "damage.Id");
            }

            return new CopyConfirmDto
            {
                Id = damage.Id.Value,
                ObjId = damage.ObjId,
                Unom = damage.UNOM,
                Address = damage.Address,
                FlatNumber = damage.FlatNumber,
                Area = damage.Area,
                DamageDate = damage.DamageData,
                TypeCode = damage.TypeCode,
                CausesOfDamageGP = damage.CausesOfDamageGP,
                CausesOfDamageOI = damage.CausesOfDamageOI
            };
        }
    }
}
