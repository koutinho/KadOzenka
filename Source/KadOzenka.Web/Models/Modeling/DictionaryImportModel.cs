using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Modeling
{
    public class DictionaryImportModel
    {
        public long DictionaryId { get; set; }

	    [Display(Name = "Метка")]
        [Required(ErrorMessage = "Поле 'Метка' обязательное")]
        public string Value { get; set; }

        [Display(Name = "Значение для расчета")]
        [Required(ErrorMessage = "Поле 'Значение для расчета' обязательное")]
        public string CalcValue { get; set; }

        [Display(Name = "Удалить старые данные")]
        public bool IsDeleteOldValues { get; set; }
    }
}
