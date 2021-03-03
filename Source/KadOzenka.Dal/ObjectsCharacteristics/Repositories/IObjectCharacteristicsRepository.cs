using ObjectModel.Core.Register;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.ObjectsCharacteristics.Repositories
{
	public interface IObjectCharacteristicsRepository
	{
		void CreateOrUpdateCharacteristicSetting(long attributeId, bool useParentAttributeForLivingPlacement,
			bool useParentAttributeForNotLivingPlacement, bool useParentAttributeForCarPlace);
		int GetNumberOfExistingRegistersWithCharacteristics();
		void CreateObjectCharacteristics(long registerId);
		OMAttributeSettings GetRegisterAttributeSettings(long attributeId);
		void SaveRegister(OMRegister omRegister);
	}
}
