using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.ExpressScore.Dto;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.ExpressScore
{
	public class SettingsExpressScoreViewModel : IValidatableObject
	{
		[Required(ErrorMessage = "Выберите тур")]
		public long? TourId { get; set; }

		/// <summary>
		/// ид реестра в которм хранятся данные для оценки
		/// </summary>
		[Required(ErrorMessage = "Выберите реестр")]
		public long? FactorRegisterId { get; set; }

		/// <summary>
		/// Список факторов для экспресс оценки
		/// </summary>
		public CostFactorsDto CostFactors { get; set; }

		/// <summary>
		/// Тип сегмента
		/// </summary>
		[Required(ErrorMessage = "Не передан тип сегмента")]
		public MarketSegment? SegmentType { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (CostFactors == null || (CostFactors.SimpleCostFactors == null && CostFactors.ComplexCostFactors == null))
			{
				yield return
					new ValidationResult(errorMessage: "Нет параметров");
			}

			if (CostFactors.SimpleCostFactors != null && CostFactors.SimpleCostFactors.Count == 0 && CostFactors.ComplexCostFactors !=null && CostFactors.ComplexCostFactors.Count == 0)
			{
				yield return
					new ValidationResult(errorMessage: "Нет параметров");
			}

			if (CostFactors.SimpleCostFactors != null && CostFactors.SimpleCostFactors.Count != 0)
			{
				if (CostFactors.SimpleCostFactors.Any(x => x.Name == null || x.Coefficient == null || x.Coefficient == 0))
				{
					yield return
						new ValidationResult(errorMessage: "Заполните обязательные параметры");
				}

			}

			if (CostFactors.ComplexCostFactors != null && CostFactors.ComplexCostFactors.Count != 0)
			{
				if (CostFactors.ComplexCostFactors.Any(x => x.Name == null || x.Coefficient == null || x.Coefficient == 0))
				{
					yield return
						new ValidationResult(errorMessage: "Заполните обязательные параметры");
				}

			}

			if (CostFactors != null && CostFactors.YearBuildId == 0 || CostFactors.YearBuildId == null)
			{
				yield return
					new ValidationResult(errorMessage: "Заполните атрибут года постройки");
			}

		}
	}
}