using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using ObjectModel.Directory;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms
{
    //TODO отчет не сделан, т.к. ждем ответа от аналитика
    public class CalculationsAnalysisReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "AdditionalFormsCalculationsAnalysisReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            return new DataSet();
        }


        #region Entites

        public class MarketObjectCharacteristics
        {
            public string CadastralNumber { get; set; }
            public ObjectType Type { get; set; }
            public decimal? Square { get; set; }
            public string Purpose { get; set; }
            public string Address { get; set; }
            public string Location { get; set; }
        }

        public class ReportGko2018
        {
            public string EvaluationSubgroup2018 { get; set; }
            public decimal? Upks2018 { get; set; }
            public decimal? CadastralCost2018 { get; set; }
            public string CadastralQuartal2018 { get; set; }
        }

        public class Comparison
        {
            public KoNoteType TaskType { get; set; }
            public DateTime? EGRNChangeDate { get; set; }
            public string EvaluationSubgroup { get; set; }
            public decimal? Upks { get; set; }
            public decimal? CadastralCost { get; set; }
            public string CadastralQuartal { get; set; }
        }

        public class Vyon
        {
            public KoNoteType TaskType { get; set; }
            public string EvaluationSubgroup { get; set; }
            public decimal? Upks { get; set; }
            public decimal? CadastralCost { get; set; }
            public string CadastralQuartal { get; set; }
            public DateTime? EGRNChangeDate { get; set; }
            public string Status { get; set; }
            public string Changes { get; set; }
        }

        public class AdditionalInfo
        {
            public decimal? MinUpksByCadastralQuartal { get; set; }
            public decimal? AverageUpksByCadastralQuartal { get; set; }
            public decimal? MaxUpksByCadastralQuartal { get; set; }
            public decimal? MinUpksByZone { get; set; }
            public decimal? AverageUpksByZone { get; set; }
            public decimal? MaxUpksByZone { get; set; }
        }

        public class Participating
        {
            public decimal? ParticipatingCount { get; set; }
            public decimal? CountInYear { get; set; }
            public decimal? CountInDays { get; set; }
        }

        public class ReportItem
        {
            public MarketObjectCharacteristics MarketObjectCharacteristics { get; set; }
            public ReportGko2018 ReportGko2018 { get; set; }
            public Comparison Comparison { get; set; }
            public Vyon Vyon { get; set; }
            public AdditionalInfo AdditionalInfo { get; set; }
            public Participating Participating { get; set; }

            public ReportItem()
            {
                MarketObjectCharacteristics = new MarketObjectCharacteristics();
                ReportGko2018 = new ReportGko2018();
                Comparison = new Comparison();
                Vyon = new Vyon();
                AdditionalInfo = new AdditionalInfo();
                Participating = new Participating();
            }
        }

        #endregion
    }
}
