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
			var attributeSettings = GetSetting(KoUpdateCadastralDataAttributeType.CadastralQuarterAttribute);

			return attributeSettings?.AttributeId;
		}

		public long? GetCadastralDataBuildingCadastralNumberAttributeId()
		{
			var attributeSettings = GetSetting(KoUpdateCadastralDataAttributeType.BuildingCadastralNumberAttribute);

			return attributeSettings?.AttributeId;
		}

		public void UpdateCadastralDataAttributeSettings(long? cadastralQuarterAttrId, long? buildingCadastralNumberAttrId)
		{
			ValidateAttributes(cadastralQuarterAttrId, buildingCadastralNumberAttrId);

			UpdateAttributeSetting(cadastralQuarterAttrId, KoUpdateCadastralDataAttributeType.CadastralQuarterAttribute);
			UpdateAttributeSetting(buildingCadastralNumberAttrId, KoUpdateCadastralDataAttributeType.BuildingCadastralNumberAttribute);
		}


		#region Support Methods

		private void UpdateAttributeSetting(long? attributeId, KoUpdateCadastralDataAttributeType attributeType)
		{
			var attributeSetting = GetSetting(attributeType);

			if (attributeSetting == null)
			{
				attributeSetting = new OMUpdateCadastralDataAttributeSettings
				{
					AttributeUsingType_Code = attributeType
				};
			}

			attributeSetting.AttributeId = attributeId;
			attributeSetting.Save();
		}

		private OMUpdateCadastralDataAttributeSettings GetSetting(KoUpdateCadastralDataAttributeType type)
		{
			return OMUpdateCadastralDataAttributeSettings.Where(x => x.AttributeUsingType_Code == type)
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
