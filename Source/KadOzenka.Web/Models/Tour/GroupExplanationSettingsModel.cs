using KadOzenka.Dal.Groups.Dto;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Tour
{
	public class GroupExplanationSettingsModel
	{
		public long ExplanationSettingsGroupId { get; set; }

		[Display(Name = "Примененные подходы при определении кадастровой стоимости объекта недвижимости с обоснованием их выбора")]
		public string AppliedApproachesInCadastralCost { get; set; }

		[Display(Name = "Примененные методы оценки при определении кадастровой стоимости объекта недвижимости с обоснованием их выбора")]
		public string AppliedEvaluationMethodsInCadastralCost { get; set; }

		[Display(Name = "Способ определения кадастровой стоимости объекта недвижимости (массовая или индивидуальная оценка в отношении объектов недвижимости) с обоснованием его выбора")]
		public string CadastralCostDetermingMethod { get; set; }

		[Display(Name = "Обоснование модели")]
		public string ModelJustification { get; set; }

		[Display(Name = "Сегмент объектов недвижимости, к которому относится объект недвижимости, с обоснованием его выбора")]
		public string ObjectsSegment { get; set; }

		[Display(Name = "Группа (подгруппа) объектов недвижимости, к которой относится объект недвижимости, с обоснованием ее выбора)")]
		public string ObjectsSubgroup { get; set; }

		[Display(Name = "Краткое описание последовательности определения кадастровой стоимости объекта недвижимости")]
		public string CadastralCostCalculationOrderDescription { get; set; }

		[Display(Name = "Характеристика ценовой зоны, в которой находится объект недвижимости, в том числе характеристика типового объекта недвижимости")]
		public string PriceZoneCharacteristic { get; set; }

		[Display(Name = "Сегмент рынка недвижимости, к которому отнесен объект недвижимости")]
		public string MarketSegment { get; set; }

		[Display(Name = @"Краткая характеристика особенностей функционирования сегмента рынка объектов недвижимости, к которому отнесен объект недвижимости 
(с указанием на страницы отчета об итогах государственной кадастровой оценки , где содержится полная характеристика сегмента рынка объектов недвижимости, в том числе анализ рыночной информации о ценах сделок (предложений) в таком сегменте, затрат на строительство объектов недвижимости)")]
		public string MarketSegmentFunctioningFeatures { get; set; }

		public GroupExplanationSettingsDto ToDto()
		{
			return new GroupExplanationSettingsDto
			{
				GroupId = ExplanationSettingsGroupId,
				AppliedApproachesInCadastralCost = AppliedApproachesInCadastralCost,
				AppliedEvaluationMethodsInCadastralCost = AppliedEvaluationMethodsInCadastralCost,
				CadastralCostDetermingMethod = CadastralCostDetermingMethod,
				ModelJustification = ModelJustification,
				ObjectsSegment = ObjectsSegment,
				ObjectsSubgroup = ObjectsSubgroup,
				CadastralCostCalculationOrderDescription = CadastralCostCalculationOrderDescription,
				PriceZoneCharacteristic = PriceZoneCharacteristic,
				MarketSegment = MarketSegment,
				MarketSegmentFunctioningFeatures = MarketSegmentFunctioningFeatures
			};
		}

		public static GroupExplanationSettingsModel FromDto(GroupExplanationSettingsDto dto)
		{
			return new GroupExplanationSettingsModel
			{
				ExplanationSettingsGroupId = dto.GroupId,
				AppliedApproachesInCadastralCost = dto.AppliedApproachesInCadastralCost,
				AppliedEvaluationMethodsInCadastralCost = dto.AppliedEvaluationMethodsInCadastralCost,
				CadastralCostDetermingMethod = dto.CadastralCostDetermingMethod,
				ModelJustification = dto.ModelJustification,
				ObjectsSegment = dto.ObjectsSegment,
				ObjectsSubgroup = dto.ObjectsSubgroup,
				CadastralCostCalculationOrderDescription = dto.CadastralCostCalculationOrderDescription,
				PriceZoneCharacteristic = dto.PriceZoneCharacteristic,
				MarketSegment = dto.MarketSegment,
				MarketSegmentFunctioningFeatures = dto.MarketSegmentFunctioningFeatures
			};
		}
	}
}
