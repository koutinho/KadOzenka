using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;

namespace KadOzenka.Web.Models.ObjectsCharacteristics
{
    public class ObjectsCharacteristicModel
    {
        public long Id { get; set; }

        [Display(Name = "Пользовательское наименование")]
        public string Name { get; set; }

        public static ObjectsCharacteristicDto UnMap(ObjectsCharacteristicModel model)
        {
            return new ObjectsCharacteristicDto
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
