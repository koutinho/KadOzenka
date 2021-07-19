using CommonSdks;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;
using ObjectModel.KO;

namespace KadOzenka.Dal.ObjectsCharacteristics.Repositories
{
	public interface IObjectCharacteristicsRepository : IGenericRepository<OMObjectsCharacteristicsRegister>
	{
		void CreateOrUpdateCharacteristicSetting(long attributeId, bool useParentAttributeForLivingPlacement,
			bool useParentAttributeForNotLivingPlacement, bool useParentAttributeForCarPlace, bool disableAttributeEditing);
		int GetNumberOfExistingRegistersWithCharacteristics();
		void CreateObjectCharacteristics(long registerId, bool disableAttributeEditing);
		OMAttributeSettings GetRegisterAttributeSettings(long attributeId);
		void SaveRegister(OMRegister omRegister, bool? disableAttributeEditing);

		bool GetObjectRegisterEditState(long registerId);
	}
}
