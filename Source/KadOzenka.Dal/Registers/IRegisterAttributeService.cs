using System.Collections.Generic;
using Core.Register;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Registers
{
	public interface IRegisterAttributeService
	{
		OMAttribute GetRegisterAttribute(long attributeId);
		List<OMAttribute> GetActiveRegisterAttributes(long registerId, List<long> attributes = null);
		OMAttribute CreateRegisterAttribute(string attributeName, long registerId, RegisterAttributeType type,
			bool withValueField, long? referenceId = null);
		void RenameRegisterAttribute(long attributeId, string newAttributeName);
		void RemoveRegisterAttribute(long attributeId);
		void SetAttributeValue(int objectId, long attributeId, object attributeValue, int referenceItemId = -1);
	}
}
