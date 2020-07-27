using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class ChangesUploadingDto
	{
		public string CadastralNumber { get; set; }
		public DateTime? ChangedDate { get; set; }
		public string PropertyType { get; set; }
		public string Status { get; set; }
		public string OldValue { get; set; }
		public string NewValue { get; set; }
		public string Changing { get; set; }
	}
}
