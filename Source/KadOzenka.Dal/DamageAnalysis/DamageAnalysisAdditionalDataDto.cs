using System.Collections.Generic;

namespace CIPJS.DAL.DamageAnalysis
{
    public class DamageAnalysisAdditionalDataDto
    {
        public List<DamageAnalysisAdditionalContractInfoDto> Contracts { get; set; }
    }

    public class DamageAnalysisAdditionalContractInfoDto
    {
        public long FspId { get; set; }

        public bool IsPaid { get; set; }

        public decimal? InsurCost { get; set; }
    }
}
