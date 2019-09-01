using System;

namespace CIPJS.DAL.DamageAnalysis
{
    public class DamageAnalysisPayToDto
    {
        public string FactNumber { get; set; }

        public DateTime? FactDate { get; set; }

        public decimal? SpFact { get; set; }

        public decimal? SpNo { get; set; }
    }
}
