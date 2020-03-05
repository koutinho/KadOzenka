using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Register;

namespace KadOzenka.Web.Models.Tour
{
	public class TourFactorObjectModel : IValidatableObject
	{
		public long Id { get; set; }

        [Required(ErrorMessage = "Имя фактора не может быть пустым")]
        [Display(Name = "Имя фактора")]
	    public string Name { get; set; }

	    [Display(Name = "Тип")]
        public RegisterAttributeType Type { get; set; }

	    [Display(Name = "Связь со справочником")]
        public long? ReferenceId { get; set; }

        public long TourId { get; set; }

		public bool IsSteadObjectType { get; set; }

		public long? RegisterFactorId { get; set; }

	    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	    {
	        if (Type == RegisterAttributeType.REFERENCE && !ReferenceId.HasValue)
	        {
                yield return new ValidationResult(errorMessage: "Поле Cправочник обязательное для данного типа фактора",
                                    memberNames: new[] { nameof(ReferenceId) });
            }
	    }
    }
}
