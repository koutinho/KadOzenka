using System;

namespace CIPJS.DAL.LivingPremiseInsurCost
{
    public class LivingPremiseInsurCostDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Дата начала действия)
        /// </summary>
        public DateTime? DateBegin { get; set; }

        /// <summary>
        /// Условие
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// Значение, руб.
        /// </summary>
        public decimal? Value { get; set; }
    }
}
