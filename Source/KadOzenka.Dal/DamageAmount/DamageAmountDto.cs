using ObjectModel.Directory;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.DAL.DamageAmount
{
    public class DamageAmountDto
    {
        public long Id { get; set; }

        public long DamageId { get; set; }

        public long? DamageAssessmentMethodId { get; set; }

        [Display(Name = "Элемент конструкции")]
        public string ElementOfConstruction { get; set; }

        [Display(Name = "Элемент конструкции")]
        public ElementsOfConstructions ElementOfConstruction_Code { get; set; }

        [Display(Name = "Разброс")]
        public string MaterialDamageRange { get; set; }

        [Display(Name = "Материальный ущерб %")]
        public decimal? MaterialDamage { get; set; }

        [Display(Name = "Удельный вес восстановительной стоимости")]
        public decimal? ProportionReplacementCost { get; set; }

        [Display(Name = "Удельный вес поврежденного участка")]
        public decimal? ProportionDamagedArea { get; set; }

        [Display(Name = "Сумма ущерба")]
        public decimal? DamageAmount { get; set; }

        [Display(Name = "Поправочный коэффициент")]
        public decimal? Correction { get; set; }
    }
}
