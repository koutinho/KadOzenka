using System;
using System.Linq;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tasks
{
	public class UpdateCadastralDataService
	{
		private GbuObjectService GbuObjectService { get; set; }

		public UpdateCadastralDataService()
		{
			GbuObjectService = new GbuObjectService();
		}

		public long? GetCadastralDataCadastralQuarterAttributeId()
		{
			var attributeSettings = OMUpdateCadastralDataAttributeSettings.Where(x =>
					x.AttributeUsingType_Code == KoUpdateCadastralDataAttributeType.CadastralQuarterAttribute)
				.SelectAll()
				.ExecuteFirstOrDefault();

			return attributeSettings?.AttributeId;
		}

		public long? GetCadastralDataBuildingCadastralNumberAttributeId()
		{
			var attributeSettings = OMUpdateCadastralDataAttributeSettings.Where(x =>
					x.AttributeUsingType_Code == KoUpdateCadastralDataAttributeType.BuildingCadastralNumberAttribute)
				.SelectAll()
				.ExecuteFirstOrDefault();

			return attributeSettings?.AttributeId;
		}

		public void UpdateCadastralDataAttributeSettings(long cadastralQuarterAttrId, long buildingCadastralNumberAttrId)
		{
			ValidateAttribute(cadastralQuarterAttrId);
			ValidateAttribute(buildingCadastralNumberAttrId);

			UpdateAttributeSetting(cadastralQuarterAttrId,
				KoUpdateCadastralDataAttributeType.CadastralQuarterAttribute);
			UpdateAttributeSetting(buildingCadastralNumberAttrId,
				KoUpdateCadastralDataAttributeType.BuildingCadastralNumberAttribute);
		}

		private void UpdateAttributeSetting(long attributeId, KoUpdateCadastralDataAttributeType attributeType)
		{
			var attributeSetting = OMUpdateCadastralDataAttributeSettings.Where(x =>
					x.AttributeUsingType_Code == attributeType)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (attributeSetting == null)
			{
				attributeSetting = new OMUpdateCadastralDataAttributeSettings();
				attributeSetting.AttributeUsingType_Code =
					attributeType;
			}

			attributeSetting.AttributeId = attributeId;
			attributeSetting.Save();
		}

		private void ValidateAttribute(long attributeId)
		{
			var attribute =
				GbuObjectService.GetGbuAttributes().FirstOrDefault(x => x.Id == attributeId);
			if (attribute == null)
				throw new Exception($"Не найден ГБУ атрибут с ИД '{attributeId}'");
		}
	}
}
