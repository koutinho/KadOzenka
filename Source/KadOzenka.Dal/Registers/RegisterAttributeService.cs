using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.RefLib;
using Core.Register;
using ObjectModel.Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using ObjectModel.Core.Shared;

namespace KadOzenka.Dal.Registers
{
    public class RegisterAttributeService
    {
        public static OMAttribute GetRegisterAttribute(long attributeId)
        {
            return OMAttribute.Where(x => x.Id == attributeId).SelectAll().ExecuteFirstOrDefault();
        }

        public List<OMAttribute> GetActiveRegisterAttributes(long registerId)
        {
            return OMAttribute
                .Where(x => x.RegisterId == registerId && x.IsDeleted.Coalesce(false) == false &&
                            x.IsPrimaryKey.Coalesce(false) == false).OrderBy(x => x.Name).SelectAll().Execute();
        }

        public OMAttribute CreateRegisterAttribute(string attributeName, long registerId, RegisterAttributeType type, bool withValueField, long? referenceId = null)
        {
            OMAttribute omAttribute;
            using (var ts = new TransactionScope())
            {
                omAttribute = new OMAttribute
                {
                    Id = -1,
                    RegisterId = registerId,
                    Name = attributeName,
                    Type = type == RegisterAttributeType.REFERENCE ? (long)RegisterAttributeType.STRING : (long)type,
                    IsNullable = true
                };
                var id = omAttribute.Save();

                if(withValueField)
                    omAttribute.ValueField = $"field{id}";

                if (type == RegisterAttributeType.REFERENCE)
                {
                    omAttribute.CodeField = $"field{id}_code";
                    omAttribute.ReferenceId = referenceId;
                }
                omAttribute.Save();

                ts.Complete();
            }

            return omAttribute;
        }

        public void RenameRegisterAttribute(long attributeId, string newAttributeName)
        {
            var attribute = OMAttribute.Where(x => x.Id == attributeId).SelectAll().ExecuteFirstOrDefault();
            if (attribute == null)
            {
                throw new Exception($"Не найден атрибут с ИД {attributeId}");
            }
            attribute.Name = newAttributeName;
            attribute.Save();
        }

        public void RemoveRegisterAttribute(long attributeId)
        {
            var attribute = OMAttribute.Where(x => x.Id == attributeId).SelectAll().ExecuteFirstOrDefault();
            if (attribute == null)
            {
                throw new Exception($"Не найден атрибут с ИД {attributeId}");
            }
            attribute.IsDeleted = true;
            attribute.Save();
        }

        public void SetAttributeValue(int objectId, long attributeId, object attributeValue, int referenceItemId = -1)
        {
	        var attributeData = RegisterCache.GetAttributeData(attributeId);
	        var registerObject = new RegisterObject(attributeData.RegisterId, objectId);

	        if (attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
	        {
		        var reference =
			        OMReference.Where(x => x.ReferenceId == attributeData.ReferenceId).ExecuteFirstOrDefault();
		        var valueStr = attributeValue != null ? attributeValue.ToString() : string.Empty;
                var items = ReferencesCommon.GetItems(reference.ReferenceId, false);
		        OMReferenceItem item = items.FirstOrDefault(x => (referenceItemId != -1 ? x.ItemId == referenceItemId : x.Value == valueStr));
		        if (item != null)
		        {
			        referenceItemId = (int) item.ItemId;
		        }
	        }

	        object value = null;
	        switch (attributeData.Type)
	        {
		        case RegisterAttributeType.INTEGER:
			        value = attributeValue.ParseToLongNullable();
			        break;
		        case RegisterAttributeType.DECIMAL:
			        value = attributeValue.ParseToDecimalNullable();
			        break;
		        case RegisterAttributeType.BOOLEAN:
			        value = attributeValue.ParseToBooleanNullable();
			        break;
		        case RegisterAttributeType.STRING:
			        value = attributeValue.ParseToStringNullable();
			        break;
		        case RegisterAttributeType.DATE:
			        value = attributeValue.ParseToDateTimeNullable();
			        break;
	        }

	        registerObject.SetAttributeValue(attributeId, value, referenceItemId);
	        RegisterStorage.Save(registerObject);
        }
    }
}
