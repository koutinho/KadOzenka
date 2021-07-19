using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Modeling.Model.Entities;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Modeling
{
    public class GeneralModelingModel
    {
	    public long Id { get; set; }
		public bool IsCreationMode { get; set; }
		public bool IsReadOnly { get; set; }

	    [Display(Name = "Имя")]
	    [Required(ErrorMessage = "Не заполнено Имя")]
	    public string Name { get; set; }

	    [Display(Name = "Описание")]
	    public string Description { get; set; }

	    [Display(Name = "Активная модель")]
	    public bool IsActive { get; set; }

		[Display(Name = "Тип")]
	    public string TypeStr => Type.GetEnumDescription();
	    [Display(Name = "Тип")]
	    public KoModelType Type { get; set; }

	    [Display(Name = "Алгоритм для рассчета Кадастровой стоимости")]
	    [Required(ErrorMessage = "Не выбран Алгоритм для рассчета Кадастровой стоимости")]
	    public KoAlgoritmType AlgorithmTypeForCadastralPriceCalculation { get; set; }

	    [Display(Name = "Тип расчета")]
	    public string CalculationTypeStr => CalculationType.GetEnumDescription();
	    [Display(Name = "Тип расчета")]
	    public KoCalculationType CalculationType { get; set; }

		[Display(Name = "Тур")]
	    [Required(ErrorMessage = "Не выбран Тур")]
	    public long TourId { get; set; }
	    [Display(Name = "Тур")]
	    public long TourYear { get; set; }

	    [Display(Name = "Тип")]
	    public ObjectTypeExtended ObjectType { get; set; }

	    [Display(Name = "Группа")]
	    [Required(ErrorMessage = "Не выбрана Группа")]
	    public long? GroupId { get; set; }

	    [Display(Name = "Свободный член")]
	    public decimal? A0 { get; set; }


		public virtual ModelDto ToDto()
	    {
		    return new ModelDto
		    {
			    ModelId = Id,
			    Name = Name,
			    Description = string.IsNullOrWhiteSpace(Description) ? "-" : Description,
			    Type = Type,
			    AlgorithmTypeForCadastralPriceCalculation = AlgorithmTypeForCadastralPriceCalculation,
				CalculationType = CalculationType,
				TourId = TourId,
				IsOksObjectType = ObjectType == ObjectTypeExtended.Oks,
				GroupId = GroupId,
				A0 = A0
		    };
	    }
	}
}
