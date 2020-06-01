using System;
using System.Collections.Generic;
using System.Text;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class MinMaxAverageUPKSByCadastralQuartersDto
	{
		public string CadastralRegionNumber { get; set; }
		public string CadastralQuater { get; set; }
		public int ObjectsCount { get; set; }
		public string PropertyType { get; set; }
		public UpksCalcType UpksCalcType { get; set; }
		public decimal? UpksCalcValue { get; set; }
	}
}
