﻿namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData.Dto.MinMaxAverage
{
	public class MinMaxAverageByGroupsUprsZuDto : MinMaxAverageCalculationInfoDto
    {
		public string ParentGroup { get; set; }
        public int ObjectsCount { get; set; }
    }
}
