using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Tours.Dto;

namespace KadOzenka.Web.Models.Tour
{
    public class TourModel
    {
        public long Id { get; set; }

        [Display(Name = "Наименование")]
        public long? Year { get; set; }

        public static TourModel ToModel(TourDto tour)
        {
            return new TourModel
            {
                Id = tour.Id,
                Year = tour.Year
            };
        }

        public static TourDto FromModel(TourModel tour)
        {
            return new TourDto
            {
                Id = tour.Id,
                Year = tour.Year
            };
        }
    }
}
