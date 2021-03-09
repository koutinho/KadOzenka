﻿using ObjectModel.Core.Register;
using ObjectModel.Gbu;
using ObjectModel.KO;

namespace KadOzenka.Dal.ObjectsCharacteristics.Repositories
{
	public class ObjectsCharacteristicsRepository : IObjectCharacteristicsRepository
	{
		public void CreateOrUpdateCharacteristicSetting(long attributeId, bool useParentAttributeForLivingPlacement, bool useParentAttributeForNotLivingPlacement, bool useParentAttributeForCarPlace)
		{
			var settings = OMAttributeSettings.Where(x => x.AttributeId == attributeId).SelectAll().ExecuteFirstOrDefault();
			if (settings == null)
			{
				settings = new OMAttributeSettings
				{
					AttributeId = attributeId
				};
			}

			settings.UseParentAttributeForLivingPlacements = useParentAttributeForLivingPlacement;
			settings.UseParentAttributeForNotLivingPlacements = useParentAttributeForNotLivingPlacement;
			settings.UseParentAttributeForCarPlace = useParentAttributeForCarPlace;
			settings.Save();
		}

		public int GetNumberOfExistingRegistersWithCharacteristics()
		{
			return OMObjectsCharacteristicsRegister.Where(x => true).SelectAll().ExecuteCount();
		}

		public void CreateObjectCharacteristics(long registerId)
		{
			new OMObjectsCharacteristicsRegister
			{
				RegisterId = registerId
			}.Save();
		}

		public OMAttributeSettings GetRegisterAttributeSettings(long attributeId)
		{
			return OMAttributeSettings.Where(x => x.AttributeId == attributeId).SelectAll().ExecuteFirstOrDefault();
		}

		public void SaveRegister(OMRegister omRegister)
		{
			omRegister.Save();
		}
	}
}
