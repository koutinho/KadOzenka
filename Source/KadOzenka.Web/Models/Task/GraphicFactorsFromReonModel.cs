using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Task
{
    public class GraphicFactorsFromReonModel : IValidatableObject
    {
        public long TaskId { get; set; }
        public List<long> AttributeIds { get; set; }


        public GraphicFactorsFromReonModel()
        {
            AttributeIds = new List<long>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (TaskId == 0)
                errors.Add(new ValidationResult("Не выбрано задание на оценку"));

            if (AttributeIds == null || AttributeIds.Count == 0)
                errors.Add(new ValidationResult("Не выбраны атрибуты"));

            return errors;
        }
    }
}
