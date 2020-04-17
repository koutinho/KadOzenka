using System.ComponentModel.DataAnnotations;
using ObjectModel.ES;

namespace KadOzenka.Web.Models.ExpressScoreReference
{
    public class ReferenceViewModel
    {
        public long Id { get; set; }

        /// <summary>
        /// Наименование справочника
        /// </summary>
        [Display(Name = "Наименование справочника")]
        [Required(ErrorMessage = "Поле Наименование справочника обязательное")]
        public string Name { get; set; }

        public bool ShowItems { get; set; }

        public static ReferenceViewModel FromEntity(OMEsReference entity, bool showItems = false)
        {
            if (entity == null)
            {
                return new ReferenceViewModel
                {
                    Id = -1,
                    ShowItems = showItems
                };
            }

            return new ReferenceViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                ShowItems = showItems
            };
        }
    }
}
