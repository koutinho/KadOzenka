using System;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes
{
	public class ImportedAttribute
	{
		public long AttributeId { get; }

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
					attributeValue.StringValue = value.ParseToStringNullable();
					break;
				case RegisterAttributeType.INTEGER:
				case RegisterAttributeType.DECIMAL:
					attributeValue.NumValue = value.ParseToDecimalNullable();
					break;
				case RegisterAttributeType.DATE:
					attributeValue.DtValue = value.ParseToDateTimeNullable();
					break;
				case RegisterAttributeType.BOOLEAN:
					attributeValue.NumValue = (value.ParseToBoolean() ? 1 : 0);
					break;
			}

			GbuObjectService.SaveAttributeValueWithCheck(attributeValue);
		}
	}
}
