﻿using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.ObjectsCharacteristics
{
	public interface IObjectsCharacteristicsService
	{
		OMAttributeSettings GetRegisterAttributeSettings(long attributeId);
		long AddCharacteristic(CharacteristicDto characteristicDto);
		void EditCharacteristic(CharacteristicDto characteristicDto);
		void DeleteCharacteristic(long characteristicId);
	}
}
