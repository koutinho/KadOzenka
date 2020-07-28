using System;

namespace KadOzenka.Web.Models.Unit
{
	public class UnitHistoryDto
	{
		public long Id { get; set; }
		public string CadastralNumber { get; set; }
		public DateTime? CreationDate { get; set; }
		public string NoteType { get; set; }
		public string InputDoc { get; set; }
		public decimal? CadastralCost { get; set; }
		public decimal? Upks { get; set; }
		public long? GroupId { get; set; }
        public string ParentGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string OutputDoc { get; set; }

		public bool IsActual { get; set; }
		public bool IsBad { get; set; }
	}
}
