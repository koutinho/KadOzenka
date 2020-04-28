using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Oks;

namespace KadOzenka.Web.Models.Tour
{
    public class TourAttributeSettingsModel
    {
        [Display(Name = "Тур")]
        [Required(ErrorMessage = "Выберете тур")]
        public long? TourId { get; set; }
        public long ObjType { get; set; }

        [Display(Name = "Атрибут кадастрового квартала")]
        public long? CodeQuarterAttributeId { get; set; }

        [Display(Name = "Атрибут кода группы")]
        public long? CodeGroupAttributeId { get; set; }

        [Display(Name = "Атрибут типа помещения")]
        public long? TypeRoomAttributeId { get; set; }

    }
}
