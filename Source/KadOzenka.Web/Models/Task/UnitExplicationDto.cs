using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadOzenka.Web.Models.Task
{
	public class UnitExplicationDto
	{
		public long Id { get; set; }

		public long? GroupId { get; set; }
		public string Group { get; set; }
		public decimal? Square { get; set; }
		public decimal? Upks { get; set; }
		public decimal? Kc { get; set; }
		public string Analog { get; set; }
	}

	public class UnitFactorsDto
	{
		public long Id { get; set; }

		public long? FactorId { get; set; }
		public string FactorName { get; set; }
		public string FactorValue { get; set; }
	}
}
