using System.Collections.Generic;

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

		/// <summary>
		/// Ид словаря индекс дата для обязательного параметра
		/// </summary>
		public decimal? IndexDateDicId { get; set; }

		/// <summary>
		/// Ид словаря доля ЗУ для обязательного параметра
		/// </summary>
		public decimal? LandShareDicId { get; set; }

		/// <summary>
		/// Ид словаря этажа расположения обязательный параметр
		/// </summary>
		public decimal? FloorDicId { get; set; }

        /// <summary>
        /// Флаг для учета НДС
        /// </summary>
        public bool? IsVatIncluded { get; set; }

        /// <summary>
        /// Ид словаря для НДС
        /// </summary>
        public decimal? VatDictionaryId { get; set; }

        public List<SimpleCostFactor> SimpleCostFactors { get; set; }
		public List<ComplexCostFactor> ComplexCostFactors { get; set; }
	}
}