using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Tour
{
    public class TourAttributeSettingsModel
    {
        [Display(Name = "Тур")]
        [Required(ErrorMessage = "Выберете тур")]
        public long? TourId { get; set; }

        [Display(Name = "Атрибут кадастрового квартала")]
        public long? CodeQuarterAttributeId { get; set; }

        [Display(Name = "Атрибут кода группы")]
        public long? CodeGroupAttributeId { get; set; }

        [Display(Name = "Атрибут типа территории")]
        public long? TerritoryTypeAttributeId { get; set; }

    }
}
