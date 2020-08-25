﻿using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class SubjectsUPKSByTypeDto
	{
		public string PropertyType { get; set; }
		public int ObjectsCount { get; set; }
		public int UpksCalcType { get; set; }
		public UpksCalcType UpksCalcTypeEnum => (UpksCalcType)UpksCalcType;
		public decimal? UpksCalcValue { get; set; }
	}
}
