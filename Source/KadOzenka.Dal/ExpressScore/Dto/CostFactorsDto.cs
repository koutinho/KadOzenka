﻿using System.Collections.Generic;

namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class SimpleCostFactor
	{
		public string Name { get; set; }
		public decimal? Coefficient { get; set; }
	}

	public class ComplexCostFactor
	{
		public string Name { get; set; }
		public decimal? Coefficient { get; set; }
		public int? AttributeId { get; set; }
		public int? DictionaryId { get; set; }
	}
	public class CostFactorsDto
	{
		/// <summary>
		/// ид аттрибута года постройки для поиска аналогов
		/// </summary>
		public decimal? YearBuildId { get; set; }
		public List<SimpleCostFactor> SimpleCostFactors { get; set; }
		public List<ComplexCostFactor> ComplexCostFactors { get; set; }
	}
}