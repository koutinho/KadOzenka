using System;
using System.Transactions;
using Core.Register;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Registers
{
    public class RegisterAttributeService
    {
        public OMAttribute GetRegisterAttribute(long attributeId)
        {
            return OMAttribute.Where(x => x.Id == attributeId).SelectAll().ExecuteFirstOrDefault();
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
    }
}
