using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Tours.Dto;

namespace KadOzenka.Web.Models.Tour
{
    public class TourModel
    {
        [Display(Name = "Наименование")]
        public long? Year { get; set; }

        public static TourModel ToModel(TourDto tour)
        {
            return new TourModel
            {
                Year = tour.Year
            };
        }
    }
}
