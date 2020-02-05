using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
		public string Group { get; set; }
		public string OutputDoc { get; set; }
	}
}
