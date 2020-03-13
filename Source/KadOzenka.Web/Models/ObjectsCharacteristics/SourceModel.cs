using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;

namespace KadOzenka.Web.Models.ObjectsCharacteristics
{
    public class SourceModel
    {
        public long Id { get; set; }
        public long RegisterId { get; set; }

        [Required(ErrorMessage = "Имя источника не может быть пустым")]
        [Display(Name = "Пользовательское наименование")]
        public string Name { get; set; }

        public static SourceDto UnMap(SourceModel model)
        {
            return new SourceDto
            {
                Id = model.Id,
                RegisterDescription = model.Name
            };
        }
    }
}
