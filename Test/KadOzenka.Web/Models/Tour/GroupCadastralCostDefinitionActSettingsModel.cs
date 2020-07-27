using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Groups.Dto;

namespace KadOzenka.Web.Models.Tour
{
	public class GroupCadastralCostDefinitionActSettingsModel
	{
		public long CadastralCostDefinitionActSettingsGroupId { get; set; }

		[Display(Name = "Ссылки на модели оценки кадастровой стоимости")]
		public string CadastralCostEstimationModelsReferences { get; set; }

		[Display(Name = "Ссылка на допущения")]
		public string AssumptionsReference { get; set; }

		[Display(Name = "Иная отражающаяся на стоимости информация")]
		public string OtherCostRelatedInfo { get; set; }

		public GroupCadastralCostDefinitionActSettingsDto ToDto()
		{
			return new GroupCadastralCostDefinitionActSettingsDto
			{
				GroupId = CadastralCostDefinitionActSettingsGroupId,
				CadastralCostEstimationModelsReferences = CadastralCostEstimationModelsReferences,
				AssumptionsReference = AssumptionsReference,
				OtherCostRelatedInfo = OtherCostRelatedInfo
			};
		}

		public static GroupCadastralCostDefinitionActSettingsModel FromDto(GroupCadastralCostDefinitionActSettingsDto dto)
		{
			return new GroupCadastralCostDefinitionActSettingsModel
			{
				CadastralCostDefinitionActSettingsGroupId = dto.GroupId,
				CadastralCostEstimationModelsReferences = dto.CadastralCostEstimationModelsReferences,
				AssumptionsReference = dto.AssumptionsReference,
				OtherCostRelatedInfo = dto.OtherCostRelatedInfo
			};
		}
	}
}
