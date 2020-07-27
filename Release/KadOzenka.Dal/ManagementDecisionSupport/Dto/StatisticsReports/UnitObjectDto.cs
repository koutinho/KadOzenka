using System;
using System.Collections.Generic;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports
{
	public class UnitObjectDto
	{
		public long ID { get; set; }
		public string CadastralNumber { get; set; }
		public string PropertyType { get; set; }
		public decimal? Square { get; set; }
		public DateTime TaskCreationDate { get; set; }

		public virtual List<object> ToRowExportObjects()
		{
			return new List<object>
			{
				CadastralNumber,
				PropertyType,
				Square,
				TaskCreationDate
			};
		}
	}
}
