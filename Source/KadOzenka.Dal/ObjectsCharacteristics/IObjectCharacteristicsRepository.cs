namespace KadOzenka.Dal.ObjectsCharacteristics
{
	public interface IObjectCharacteristicsRepository
	{
		void CreateOrUpdateCharacteristicSetting(long attributeId, bool useParentAttributeForLivingPlacement,
			bool useParentAttributeForNotLivingPlacement, bool useParentAttributeForCarPlace);
		int GetNumberOfExistingRegistersWithCharacteristics();
		void CreateObjectCharacteristics(long registerId);
	}
}
