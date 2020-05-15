using System;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports
{
	public class UnitObjectDto
	{
		public long ID { get; set; }
		public string CadastralNumber { get; set; }
		public string PropertyType { get; set; }
		public decimal? Square { get; set; }
		public DateTime TaskCreationDate { get; set; }
	}
}
