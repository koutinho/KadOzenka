using System;
using CommonSdks.ConfigurationManagers;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes
{
	public class ImportedAttribute
	{
		public long AttributeId { get; }
		protected virtual bool SkipNullValues { get; } = false;
		private const int MaxStringSizeForSpecialAttributes = 5000;


		public ImportedAttribute(long attributeId)
		{
			AttributeId = attributeId;
		}

		public void SetAttributeValue(object value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser)
		{
			var attributeValue = new GbuObjectAttribute
			{
				Id = -1,
				AttributeId = AttributeId,
				ObjectId = idObject,
				ChangeDocId = idDocument,
				S = sDate,
				ChangeUserId = idUser,
				ChangeDate = DateTime.Now,
				Ot = otDate
			};

			switch (RegisterCache.GetAttributeData((int)AttributeId).Type)
			{
				case RegisterAttributeType.STRING:
				{
					var strValue = value.ParseToStringNullable();
					if (ShouldCheckValueLength(AttributeId))
					{
						if (!string.IsNullOrWhiteSpace(strValue) && strValue.Length >= MaxStringSizeForSpecialAttributes)
						{
							strValue = strValue.Substring(0, MaxStringSizeForSpecialAttributes);
						}
					}

					attributeValue.StringValue = strValue;
					if(SkipNullValues && string.IsNullOrEmpty(attributeValue.StringValue))
						return;

					break;
				}
				case RegisterAttributeType.INTEGER:
				case RegisterAttributeType.DECIMAL:
				{
					attributeValue.NumValue = value.ParseToDecimalNullable();
					if (SkipNullValues && !attributeValue.NumValue.HasValue)
						return;

					break;
				}
				case RegisterAttributeType.DATE:
				{
					attributeValue.DtValue = value.ParseToDateTimeNullable();
					if (SkipNullValues && !attributeValue.DtValue.HasValue)
						return;

					break;
				}
				case RegisterAttributeType.BOOLEAN:
				{
					var booleanValue = value.ParseToBooleanNullable();
					if (SkipNullValues && !booleanValue.HasValue)
						return;

					attributeValue.NumValue = booleanValue.GetValueOrDefault() ? 1 : 0;

					break;
				}
			}

			GbuObjectService.SaveAttributeValueWithCheck(attributeValue);
		}


		#region Support Methods

		private bool ShouldCheckValueLength(long attributeId)
		{
			return ConfigurationManager.KoConfig.ImportDocumentForTaskConfig
				.AttributesToTrimLength.Contains(attributeId);
		}

		#endregion
	}
}
