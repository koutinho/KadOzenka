using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory.ES;
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

        /// <summary>
        /// Тип данных значения справочника
        /// </summary>
        [Display(Name = "Тип данных")]
        [Required(ErrorMessage = "Поле Тип данных значений справочника обязательное")]
        public ReferenceItemCodeType ValueType { get; set; } = ReferenceItemCodeType.String;

        public bool ValueTypeCanBeChanged { get; set; }

        public bool ShowItems { get; set; }

        public static ReferenceViewModel FromEntity(OMEsReference entity, bool valueTypeCanBeChanged = false, bool showItems = false)
        {
            if (entity == null)
            {
                return new ReferenceViewModel
                {
                    Id = -1,
                    ShowItems = showItems,
                    ValueTypeCanBeChanged = valueTypeCanBeChanged
                };
            }

            return new ReferenceViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                ValueType = entity.ValueType_Code,
                ValueTypeCanBeChanged = valueTypeCanBeChanged,
                ShowItems = showItems
            };
        }
    }
}
