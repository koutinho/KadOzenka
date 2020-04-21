using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.KoObject;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KadOzenka.Web.Models.Task
{
	public class EstimatedGroupViewModel
	{
        [Display(Name = "Задание на оценку")]
	    [Required(ErrorMessage = "Параметр Задание на оценку обязательный")]
        public long? IdTask { get; set; }

		[Required(ErrorMessage = "Атрибут для кода группы обязательный")]
		public long? IdCodeGroup { get; set; }

		[Required(ErrorMessage = "Атрибут для кадастрового квартала обязательный")]
		public long? IdCodeQuarter { get; set; }

		[Required(ErrorMessage = "Атрибут для типа помещения обязательный")]
		public long? IdTypeRoom { get; set; }

		/// <summary>
		/// Result parameter.
		/// </summary>
		[Required(ErrorMessage = "Атрибут для проставления оценочной группы обязательный")]
		public long? IdEstimatedSubGroup { get; set; }


		public EstimatedGroupModel ToGroupModel()
		{
			return new EstimatedGroupModel
			{
				IdTask = IdTask.Value,
				IdCodeQuarter = IdCodeQuarter.GetValueOrDefault(),
				IdCodeGroup = IdCodeGroup.GetValueOrDefault(),
				IdTypeRoom = IdTypeRoom.GetValueOrDefault(),
				IdEstimatedSubGroup = IdEstimatedSubGroup.GetValueOrDefault()
			};
		}
	}
}