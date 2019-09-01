using System;

namespace CIPJS.Models.Calculation
{
    public class AgreementProjectCategory
    {
        public int Type { get; set; }

        public string Name { get; set; }

        public decimal? TotalCost { get; set; }

        public decimal? DesignCost { get; set; }

        public bool Excluded { get; set; }

        public decimal? Baserate { get; set; }

        public decimal? Annualprem { get; set; }

        public decimal? Monthlyprem { get { return Math.Round((Annualprem ?? 0m) / 12, 2, MidpointRounding.AwayFromZero); } }
    }
}