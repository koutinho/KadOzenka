using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.SRD;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.Tours.Dto;
using ObjectModel.Common;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tours
{
    public class TourService
    {
        private TourFactorService TourFactorService { get; set; }
        private GroupService GroupService { get; set; }
        private RecycleBinService RecycleBinService { get; }

        public TourService(TourFactorService tourFactorService, GroupService groupService, RecycleBinService recycleBinService)
        {
            TourFactorService = tourFactorService;
            GroupService = groupService;
            RecycleBinService = recycleBinService;
        }

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
            ValidateTour(tourDto);

            var id= new OMTour
            {
	            Year = tourDto.Year.Value
            }.Save();

            return id;
        }

        public int UpdateTour(TourDto tourDto)
        {
            ValidateTour(tourDto);

            var tour = GetTourByIdInternal(tourDto.Id);

            tour.Year = tourDto.Year;
            var id = tour.Save();

            return id;
        }

        public void UpdateTourAttributeSettings(TourAttributeSettingsDto tourDto)
        {
            var tour = GetTourByIdInternal(tourDto.TourId);

            if (tourDto.AttributeId.HasValue)
            {
	            var attribute = OMAttribute.Where(x => x.Id == tourDto.AttributeId).SelectAll().ExecuteFirstOrDefault();
                if (attribute == null)
                {
                    throw new Exception($"Не найден атрибут с ИД {tourDto.AttributeId}");
                }
            }

            var tourAttributeSettings = OMTourAttributeSettings
                .Where(x => x.TourId == tourDto.TourId &&
                            x.AttributeUsingType_Code == tourDto.KoAttributeUsingType)
                .SelectAll()
                .ExecuteFirstOrDefault();
            if (tourAttributeSettings == null)
            {
                tourAttributeSettings = new OMTourAttributeSettings
                {
                    TourId = tourDto.TourId,
                    AttributeUsingType_Code = tourDto.KoAttributeUsingType,
                    AttributeId = tourDto.AttributeId
                };
            }
            else
            {
                tourAttributeSettings.AttributeId = tourDto.AttributeId;
            }

            tourAttributeSettings.Save();
        }

        public bool CanTourBeDeleted(long tourId)
        {
	        return !OMTask.Where(x => x.TourId == tourId).ExecuteExists();
        }

        public void DeleteTour(long tourId)
        {
			var tour = OMTour.Where(x => x.Id == tourId).SelectAll().ExecuteFirstOrDefault();

			if (!CanTourBeDeleted(tourId))
		        throw new Exception($"Тур {tour?.Year} не может быть удален, т.к. имеются связанные задания на оценку");

	        var complianceGuides = OMComplianceGuide.Where(x => x.TourId == tourId).Execute();

	        using (var ts = new TransactionScope())
	        {
		        var recycleBinRecord = new OMRecycleBin
		        {
			        DeletedTime = DateTime.Now,
			        UserId = SRDSession.GetCurrentUserId().Value,
			        ObjectRegisterId = OMTour.GetRegisterId(),
			        Description = $"Тур '{tour.Year}'"
		        };
		        long eventId = recycleBinRecord.Save();

		        GroupService.DeleteGroups(tourId, true, eventId);
		        GroupService.DeleteGroups(tourId, false, eventId);

		        RecycleBinService.MoveObjectsToRecycleBin(complianceGuides.Select(x => x.Id).ToList(), OMComplianceGuide.GetRegisterId(), eventId);

                TourFactorService.RemoveTourFactorRegistersLogically(tourId, eventId);

				if (tour != null)
		        	RecycleBinService.MoveObjectToRecycleBin(tourId, OMTour.GetRegisterId(), eventId);

                ts.Complete();
	        }
        }

        #region Support Methods

        private  void ValidateTour(TourDto tourDto)
        {
            if (tourDto.Year == null || tourDto.Year.Value == 0)
                throw new Exception("Не указан год при создании Тура");

            if (!int.TryParse(tourDto.Year.Value.ToString(), out _))
                throw new Exception("Введенное число не может быть преобразовано в год");

            var isTourWithTheSameYearExists = OMTour.Where(x => x.Year == tourDto.Year && x.Id != tourDto.Id).ExecuteExists();
            if (isTourWithTheSameYearExists)
	            throw new Exception($"Тур с годом '{tourDto.Year}' уже существует");
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
