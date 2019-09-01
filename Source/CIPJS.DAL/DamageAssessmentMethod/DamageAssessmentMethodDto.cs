using System.ComponentModel.DataAnnotations;

namespace CIPJS.DAL.DamageAssessmentMethod
{
    public class DamageAssessmentMethodDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Признаки  ущерба
        /// </summary>
        public string DamageSymptom { get; set; }

        /// <summary>
        /// Материальный ущерб, %
        /// </summary>
        [Display(Name = "Материальный ущерб, %")]
        public decimal? MaterialDamage { get; set; }

        /// <summary>
        /// Материальный ущерб, %
        /// </summary>
        [Display(Name = "Материальный ущерб, %")]
        public string MaterialDamageRange { get; set; }

        /// <summary>
        /// Примерный состав работ для устранения ущерба
        /// </summary>
        public string WorkComposition { get; set; }

        /// <summary>
        /// Элемент конструкции
        /// </summary>
        public string ElementConstruction { get; set; }

        /// <summary>
        /// Минимальный процент урона
        /// </summary>
        [Display(Name = "Минимальный процент урона")]
        public decimal? MaterialDamageMin { get; set; }

        /// <summary>
        /// Максимальный процент урона
        /// </summary>
        [Display(Name = "Максимальный процент урона")]
        public decimal? MaterialDamageMax { get; set; }

        /// <summary>
        /// Удельный вес поврежденного участка
        /// </summary>
        [Display(Name = "Удельный вес поврежденного участка")]
        public decimal? ProportionDamagedArea { get; set; }
    }
}
