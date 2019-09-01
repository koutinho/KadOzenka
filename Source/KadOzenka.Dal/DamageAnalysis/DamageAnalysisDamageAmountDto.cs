namespace CIPJS.DAL.DamageAnalysis
{
    public class DamageAnalysisDamageAmountDto
    {
        /// <summary>
        /// Элемент конструкции
        /// </summary>
        public string ElementOfConstruction { get; set; }
        /// <summary>
        /// Признаки  ущерба
        /// </summary>
        public string DamageSymptom { get; set; }

        /// <summary>
        /// Материальный ущерб, %
        /// </summary>
        public decimal? MaterialDamage { get; set; }

        /// <summary>
        /// Примерный состав работ для устранения ущерба
        /// </summary>
        public string WorkComposition { get; set; }
    }
}
