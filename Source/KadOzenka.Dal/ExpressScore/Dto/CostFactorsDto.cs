//using System.Collections.Generic;

//namespace KadOzenka.Dal.ExpressScore.Dto
//{
//	public enum ComplexCostFactorSpecialization
//	{
//		Common,
//		SquareFactor
//	}

//	public class SimpleCostFactor
//	{
//		public string Name { get; set; }
//		public decimal? Coefficient { get; set; }
//	}

//	public class ComplexCostFactor
//	{
//		public ComplexCostFactor()
//		{
//		}

//		public ComplexCostFactor(ComplexCostFactor complexCostFactor)
//		{
//			Name = complexCostFactor.Name;
//			Coefficient = complexCostFactor.Coefficient;
//			AttributeId = complexCostFactor.AttributeId;
//			DictionaryId = complexCostFactor.DictionaryId;
//			ComplexCostFactorType = complexCostFactor.ComplexCostFactorType;
//			ShowInCalculatePage = complexCostFactor.ShowInCalculatePage;
//		}

//		public ComplexCostFactor(ComplexCostFactorSpecialization factorSpecialization)
//		{
//			ComplexCostFactorType = factorSpecialization;
//			if (factorSpecialization == ComplexCostFactorSpecialization.SquareFactor)
//			{
//				Name = "Площадь";
//			}
//		}

//		public string Name { get; set; }
//		public decimal? Coefficient { get; set; }
//		public int? AttributeId { get; set; }
//		public int? DictionaryId { get; set; }
//		public ComplexCostFactorSpecialization ComplexCostFactorType { get; set; }

//		/// <summary>
//		/// Чекбокс указывающий что данный параемтр будет отображаться на странице расчетов
//		/// </summary>
//		public bool ShowInCalculatePage { get; set; }

//		/// <summary>
//		/// Значение по уболчанию на случай если выбран пункт показывать на старнице расчетов
//		/// </summary>
//		public string DefaultValue { get; set; }
//	}
//	public class CostFactorsDto
//	{
//		/// <summary>
//		/// Ид словаря индекс дата для обязательного параметра
//		/// </summary>
//		public decimal? IndexDateDicId { get; set; }

//		/// <summary>
//		/// Ид словаря доля ЗУ для обязательного параметра
//		/// </summary>
//		public decimal? LandShareDicId { get; set; }

//		/// <summary>
//        /// Флаг для учета НДС
//        /// </summary>
//        public bool? IsVatIncluded { get; set; }

//        /// <summary>
//        /// Ид словаря для НДС
//        /// </summary>
//        public decimal? VatDictionaryId { get; set; }

//        /// <summary>
//        /// Флаг использования фактора площади при расчете
//        /// </summary>
//        public bool? IsSquareFactorUsedInCalculations { get; set; }

//        /// <summary>
//        /// Флаг использования корректировки на торг при расчете
//        /// </summary>
//		public bool? IsCorrectionByBargainUsedInCalculations { get; set; }
//		/// <summary>
//		/// Коэффициент корректировки на торг для типов «Предложение-продажа» и «Предложение-аренда»
//		/// </summary>
//		public decimal? CorrectionByBargainCoef { get; set; }

//		/// <summary>
//		/// Флаг учитывания операционных расходов при расчете
//		/// </summary>
//		public bool? IsOperatingCostsUsedInCalculations { get; set; }
//		/// <summary>
//		/// Коэффициент операционных расходов при расчете
//		/// </summary>
//		public decimal? OperatingCostsCoef { get; set; }

//		public List<SimpleCostFactor> SimpleCostFactors { get; set; }
//		public List<ComplexCostFactor> ComplexCostFactors { get; set; }
//	}
//}