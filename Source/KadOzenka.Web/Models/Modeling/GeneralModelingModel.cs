using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Oks;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Modeling
{
    public class GeneralModelingModel
    {
	    public long Id { get; set; }

	    [Display(Name = "Имя")]
	    [Required(ErrorMessage = "Не заполнено Имя")]
	    public string Name { get; set; }

	    [Display(Name = "Описание")]
	    public string Description { get; set; }

	    [Display(Name = "Тип")]
	    public string TypeStr => Type.GetEnumDescription();
	    [Display(Name = "Тип")]
	    public KoModelType Type { get; set; }

	    [Display(Name = "Алгоритм расчета")]
		public string AlgorithmTypeStr => AlgorithmType.GetEnumDescription();
		[Display(Name = "Алгоритм расчета")]
	    public KoAlgoritmType AlgorithmType { get; set; }

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
	    public ObjectType ObjectType { get; set; }

	    [Display(Name = "Группа")]
	    [Required(ErrorMessage = "Не выбрана Группа")]
	    public long? GroupId { get; set; }

	    [Display(Name = "Свободный член")]
	    public decimal? A0 { get; set; }


		public virtual ModelingModelDto ToDto()
	    {
		    return new ModelingModelDto
		    {
			    ModelId = Id,
			    Name = Name,
			    Description = string.IsNullOrWhiteSpace(Description) ? "-" : Description,
			    Type = Type,
			    AlgorithmType = AlgorithmType,
				CalculationType = CalculationType,
				TourId = TourId,
				IsOksObjectType = ObjectType == ObjectType.Oks,
				GroupId = GroupId,
				A0 = A0
		    };
	    }
	}
}
