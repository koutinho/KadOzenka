using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Tour
{
    public class TourAttributeSettingsModel
    {
        [Display(Name = "Тур")]
        [Required(ErrorMessage = "Выберете тур")]
        public long? TourId { get; set; }

        [Display(Name = "Атрибут кода группы")]
        public long? CodeGroupAttributeId { get; set; }
    }
}
