using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Modeling
{
    public class DictionaryImportModel
    {
        public long DictionaryId { get; set; }

	    [Display(Name = "Значение")]
        [Required(ErrorMessage = "Поле 'Значение' обязательное")]
        public string Value { get; set; }

        [Display(Name = "Метка")]
        [Required(ErrorMessage = "Поле 'Метка' обязательное")]
        public string CalculationValue { get; set; }

        [Display(Name = "Удалить старые данные")]
        public bool IsDeleteOldValues { get; set; }
    }
}
