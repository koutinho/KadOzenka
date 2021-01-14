using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.Tours.Dto;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tours
{
    public class TourService
    {
        private TourFactorService TourFactorService { get; set; }
        private GroupService GroupService { get; set; }

        public TourService(TourFactorService tourFactorService, GroupService groupService)
        {
            TourFactorService = tourFactorService;
            GroupService = groupService;
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
	        if (tour == null)
		        throw new Exception("Тур с указанным ид не найден");

	        if (!CanTourBeDeleted(tourId))
		        throw new Exception($"Тур {tour.Year} не может быть удален, т.к. имеются связанные задания на оценку");

		    var groupIds = OMTourGroup.Where(x => x.TourId == tourId).Select(x => x.GroupId).Execute().Select(x => x.GroupId).ToList();
		    var complianceGuides = OMComplianceGuide.Where(x => x.TourId == tourId).Execute();

            using (var ts = new TransactionScope())
	        {
		        foreach (var groupId in groupIds)
		        {
			        GroupService.DeleteGroup(groupId);
                }

		        foreach (var complianceGuide in complianceGuides)
		        {
			        complianceGuide.Destroy();
		        }

		        TourFactorService.RemoveTourFactorRegisters(tour.Id);
                tour.Destroy();

		        ts.Complete();
	        }
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
