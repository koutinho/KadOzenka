using System.Collections.Generic;
using KadOzenka.Dal.Tours.Dto;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tours
{
	public interface ITourService
	{
		TourDto GetTourById(long? tourId);
		List<OMTour> GetTours();
		int AddTour(TourDto tourDto);
		int UpdateTour(TourDto tourDto);
		void UpdateTourAttributeSettings(TourAttributeSettingsDto tourDto);
		bool CanTourBeDeleted(long tourId);
		void DeleteTour(long tourId);
	}
}