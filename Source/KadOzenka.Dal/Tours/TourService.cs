using System;
using System.Collections.Generic;
using System.Transactions;
using KadOzenka.Dal.Tours.Dto;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tours
{
    public class TourService
    {
        public TourDto GetTourById(long? tourId)
        {
            var tour = GetTourByIdInternal(tourId);
            return new TourDto
            {
                Id = tour.Id,
                Year = tour.Year
            };
        }

        public List<OMTour> GetTours()
        {
            return OMTour.Where(x => true).SelectAll().Execute();
        }

        public int AddTour(TourDto tourDto)
        {
            ValidateTourYear(tourDto);
            var existedTour = OMTour.Where(x => x.Year == tourDto.Year).Select(x => x.Id).ExecuteFirstOrDefault();
            if (existedTour != null)
                throw new Exception($"Тур с годом '{tourDto.Year}' уже существует");

            int id;
            using (var ts = new TransactionScope())
            {
                id = new OMTour
                {
                    Year = tourDto.Year.Value
                }.Save();
                ts.Complete();
            }

            return id;
        }

        public int UpdateTour(TourDto tourDto)
        {
            ValidateTourYear(tourDto);

            var tour = GetTourByIdInternal(tourDto.Id);

            int id;
            using (var ts = new TransactionScope())
            {
                tour.Year = tourDto.Year;
                id = tour.Save();
                ts.Complete();
            }

            return id;
        }


        #region Support Methods

        private  void ValidateTourYear(TourDto tourDto)
        {
            if (tourDto.Year == null || tourDto.Year.Value == 0)
                throw new Exception("Не указан год при создании Тура");

            if (!int.TryParse(tourDto.Year.Value.ToString(), out _))
                throw new Exception("Введенное число не может быть преобразовано в год");
        }

        private OMTour GetTourByIdInternal(long? tourId)
        {
            if (tourId == null)
                throw new Exception("Не передан идентификатор Тура для поиска");

            var tour = OMTour.Where(x => x.Id == tourId).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
                throw new Exception($"Не найден Тур с id='{tourId}'");

            return tour;
        }

        #endregion
    }
}
