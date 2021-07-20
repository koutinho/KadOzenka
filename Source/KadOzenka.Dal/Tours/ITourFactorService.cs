using System.Collections.Generic;
using CommonSdks.RecycleBin;
using Core.Register;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Tours.Dto;
using ObjectModel.Core.Register;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Tours
{
	public interface ITourFactorService
	{
		RegisterService RegisterService { get; set; }
		RegisterAttributeService RegisterAttributeService { get; set; }
		RecycleBinService RecycleBinService { get; }
		List<OMAttribute> GetTourAttributes(long tourId, ObjectTypeExtended objectType);
		OMRegister GetTourRegister(long tourId, ObjectType objectType);
		OMRegister CreateTourFactorRegister(long tourId, bool isStead);
		void RemoveTourFactorRegistersLogically(long tourId, long eventId);

		long CreateTourFactorRegisterAttribute(string attributeName, long registerId, RegisterAttributeType type,
			long? referenceId = null);

		void RenameTourFactorRegisterAttribute(long attributeId, string newAttributeName);
		void RemoveTourFactorRegisterAttribute(long attributeId);
		List<AttributeSettingsDto> GetTourAttributesFromSettings(long tourId);
		RegisterAttribute GetTourAttributeFromSettings(long tourId, KoAttributeUsingType type);
		TourEstimatedGroupAttributeParamsDto GetEstimatedGroupModelParamsForTask(long taskId);
		TourAttributesDto GetAllTourAttributes(long tourId);
	}
}