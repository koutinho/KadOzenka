using System;
using ObjectModel.Directory;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.DataInfo
{
	public class ObjectMainInfo
	{
		public string CadastralNumber { get; set; }
		public PropertyTypes PropertyType { get; set; }
		public string RosreestrSquareValue { get; set; }
		public string ObjectName { get; set; }
		public string TypeOfUse { get; set; }
		public string BuildingPurpose { get; set; }
		public string PlacementPurpose { get; set; }
		public string ConstructionPurpose { get; set; }
		public string Address { get; set; }
		public string Location { get; set; }

		public string NoteType { get; set; }
		public long UnitId { get; set; }
		public KoNoteType TaskType { get; set; }
		public string Group { get; set; }
		public DateTime? UnitCreationDate { get; set; }
		public decimal? ObjectUpks { get; set; }
		public decimal? ObjectCost { get; set; }
		public decimal? UnitSquare { get; set; }
		public string CadastralQuartal { get; set; }
		public KoUnitStatus Status { get; set; }
		public string ChangedFactors { get; set; }
		public long? Zone { get; internal set; }
	}
}
