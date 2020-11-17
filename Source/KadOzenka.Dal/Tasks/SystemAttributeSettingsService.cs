using System;
using System.Linq;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tasks
{
	public class SystemAttributeSettingsService
	{
		private GbuObjectService GbuObjectService { get; set; }

		public SystemAttributeSettingsService()
		{
			GbuObjectService = new GbuObjectService();
		}


		

		public long? GetCadastralDataCadastralQuarterAttributeId()
		{
			var attributeSettings = GetSetting(KoAttributeTypeForSettings.CadastralQuarter);

			return attributeSettings?.AttributeId;
		}

		public long? GetCadastralDataBuildingCadastralNumberAttributeId()
		{
			var attributeSettings = GetSetting(KoAttributeTypeForSettings.BuildingCadastralNumber);

			return attributeSettings?.AttributeId;
		}

		public void UpdateCadastralDataAttributeSettings(long? cadastralQuarterAttrId, long? buildingCadastralNumberAttrId)
		{
			ValidateAttributes(cadastralQuarterAttrId, buildingCadastralNumberAttrId);

			UpdateAttributeSetting(cadastralQuarterAttrId, KoAttributeTypeForSettings.CadastralQuarter);
			UpdateAttributeSetting(buildingCadastralNumberAttrId, KoAttributeTypeForSettings.BuildingCadastralNumber);
		}


		#region Support Methods

		private void UpdateAttributeSetting(long? attributeId, KoAttributeTypeForSettings attributeType)
		{
			var attributeSetting = GetSetting(attributeType);

			if (attributeSetting == null)
			{
				attributeSetting = new OMSystemAttributeSettings
				{
					AttributeUsingType_Code = attributeType
				};
			}

			attributeSetting.AttributeId = attributeId;
			attributeSetting.Save();
		}

		private OMSystemAttributeSettings GetSetting(KoAttributeTypeForSettings type)
		{
			return OMSystemAttributeSettings.Where(x => x.AttributeUsingType_Code == type)
				.SelectAll()
				.ExecuteFirstOrDefault();
		}

		private void ValidateAttributes(long? cadastralQuarterAttrId, long? buildingCadastralNumberAttrId)
		{
			if (cadastralQuarterAttrId.HasValue)
				ValidateAttribute(cadastralQuarterAttrId.Value);
			if (buildingCadastralNumberAttrId.HasValue)
				ValidateAttribute(buildingCadastralNumberAttrId.Value);

			if (!cadastralQuarterAttrId.HasValue && !buildingCadastralNumberAttrId.HasValue)
			{
				throw new Exception("Должен быть указан хотя бы один из атрибутов настройки");
			}
		}

		private void ValidateAttribute(long attributeId)
		{
			var attribute =
				GbuObjectService.GetGbuAttributes().FirstOrDefault(x => x.Id == attributeId);
			if (attribute == null)
				throw new Exception($"Не найден ГБУ атрибут с ИД '{attributeId}'");
		}

		#endregion
	}
}
