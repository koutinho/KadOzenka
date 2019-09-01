namespace CIPJS.Models.DamageAnalysis
{
    public class CalcDamageAmountDto
    {
        public decimal? MaterialDamage { get; set; }

        public decimal? Correction { get; set; }

        public decimal? ProportionReplacementCost { get; set; }

        public decimal? ProportionDamagedArea { get; set; }

        public decimal? EstimatedValue { get; set; }
    }
}
