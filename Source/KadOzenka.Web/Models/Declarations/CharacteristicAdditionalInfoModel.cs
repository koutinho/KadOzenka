using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class CharacteristicAdditionalInfoModel
	{
		/// <summary>
		/// Статус характеристики (принято/не принято)
		/// </summary>
		public HarStatus? HarStatus { get; set; }

		/// <summary>
		/// Использование в разделе 'Формальная проверка / Уведомление'
		/// </summary>
		public bool? IsShownInDeclaration { get; set; }

		public static CharacteristicAdditionalInfoModel FromEntity(OMHarOKSAdditionalInfo entity)
		{
			return new CharacteristicAdditionalInfoModel
			{
				HarStatus = entity?.HarStatus_Code,
				IsShownInDeclaration = entity?.IsUsedInDeclaration
			};
		}

		public static void ToEntity(CharacteristicAdditionalInfoModel model, ref OMHarOKSAdditionalInfo entity)
		{
			entity.HarStatus_Code = model.HarStatus.GetValueOrDefault();
			entity.IsUsedInDeclaration = model.IsShownInDeclaration;
		}
	}
	
}
