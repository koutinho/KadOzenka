using System;
using KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.DataInfo;
using ObjectModel.Directory;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.ReportItem
{
	public class Vyon
	{
		public KoNoteType TaskType { get; set; }
		public string EvaluationSubgroup { get; set; }
		public decimal? Upks { get; set; }
		public decimal? CadastralCost { get; set; }
		public string CadastralQuartal { get; set; }
		public DateTime? EGRNChangeDate { get; set; }
		public KoUnitStatus Status { get; set; }
		public string Changes { get; set; }

		public Vyon(ObjectMainInfo objectMainInfo)
		{
			TaskType = objectMainInfo.TaskType;
			EvaluationSubgroup = objectMainInfo.Group;
			Upks = objectMainInfo.ObjectUpks;
			CadastralCost = objectMainInfo.ObjectCost;
			CadastralQuartal = objectMainInfo.CadastralQuartal;
			EGRNChangeDate = objectMainInfo.UnitCreationDate;
			Status = objectMainInfo.Status;
			Changes = objectMainInfo.ChangedFactors;
		}
	}
}
