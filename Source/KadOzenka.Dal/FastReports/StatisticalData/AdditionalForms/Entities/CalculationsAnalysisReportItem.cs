using System;
using ObjectModel.Directory;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities
{
	public class CalculationsAnalysisReportItem
	{
		public string CadastralNumber { get; set; }
		public int Type { get; set; }
		public PropertyTypes TypeEnum => (PropertyTypes)Type;
		public string RosreestrSquareValue { get; set; }
		public string ObjectNameTypeOfUse { get; set; }
		public string Purpose { get; set; }
		public string Address { get; set; }
		public string Location { get; set; }
		public string EvaluationSubgroup2018 { get; set; }
		public decimal? Upks2018 { get; set; }
		public decimal? CadastralCost2018 { get; set; }
		public string CadastralQuartal2018 { get; set; }
		public int TaskType { get; set; }
		public KoNoteType TaskTypeEnum => (KoNoteType) TaskType;
		public string EvaluationSubgroup { get; set; }
		public decimal? Upks { get; set; }
		public decimal? CadastralCost { get; set; }
		public string CadastralQuartal { get; set; }
		public DateTime? EGRNChangeDate { get; set; }
		public int Status { get; set; }
		public KoUnitStatus StatusEnum => (KoUnitStatus) Status;
		public string Changes { get; set; }
		public decimal? MinUpksByCadastralQuartal { get; set; }
		public decimal? AverageUpksByCadastralQuartal { get; set; }
		public decimal? MaxUpksByCadastralQuartal { get; set; }
		public decimal? MinUpksByZone { get; set; }
		public decimal? AverageUpksByZone { get; set; }
		public decimal? MaxUpksByZone { get; set; }
		public int ParticipatingCount { get; set; }
		public int CountInYear { get; set; }
		public int CountInDays { get; set; }
	}
}
