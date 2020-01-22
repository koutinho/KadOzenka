using System;
using KadOzenka.Dal.Tours.Dto;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tours
{
    public class TourService
    {
        public TourDto GetTourById(long? tourId)
        {
            if(tourId == null)
                throw new Exception("Не передан идентификатор Тура для поиска");

            var tour = OMTour.Where(x => x.Id == tourId).SelectAll().ExecuteFirstOrDefault();
            if(tour == null)
                throw new Exception($"Не найден Тур с id='{tourId}'");

            return new TourDto
            {
                Id = tour.Id,
                Year = tour.Year
            };
        }
    }
}
