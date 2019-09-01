using System;

namespace CIPJS.DAL.DamageAnalysis
{
    /// <summary>
    /// Класс для вывода информации по договорам дела
    /// </summary>
    public class DamageAnalysisContractDto
    {
        public long? AllPropertyId { get; set; }

        public long? FspId { get; set; }

        public string ContractType { get; set; }

        public string ContractNumber { get; set; }

        public string Paysign { get; set; }

        public string DateBegin { get; set; }

        public string DateEnd { get; set; }

        public decimal? PartInRoom { get; set; }

        public decimal? AreaByContract { get; set; }

        public decimal? InsurCost { get; set; }

        public decimal? CalcCost { get; set; }

        public decimal? InsurSum { get; set; }

        public bool IsInsured { get; set; }
        public bool IsPaid { get; set; }
        public bool InsurCostChanged { get; set; }

        public long? DamageId { get; set; }

        public string Url { get; set; }
    }
}
