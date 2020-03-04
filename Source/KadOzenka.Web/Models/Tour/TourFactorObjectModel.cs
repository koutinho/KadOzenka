using System.ComponentModel.DataAnnotations;
using Core.Register;

namespace KadOzenka.Web.Models.Tour
{
	public class TourFactorObjectModel
	{
		public long Id { get; set; }

        [Required(ErrorMessage = "Имя фактора не может быть пустым")]
        [Display(Name = "Имя фактора")]
	    public string Name { get; set; }

	    [Display(Name = "Тип")]
        public RegisterAttributeType Type { get; set; }

        public long TourId { get; set; }

		public bool IsSteadObjectType { get; set; }

		public long? RegisterFactorId { get; set; }
    }
}
