using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Directory.KO;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Task
{
	public class FactorSettingsModel
	{
		public long Id { get; set; }
		public bool IsNew => Id == -1;
		
		[Display(Name = "Фактор")]
		public long FactorId { get; set; }
		public string FactorName { get; set; }

		[Display(Name = "Тип наследования")]
		public FactorInheritance FactorInheritanceTypeCode { get; set; }
		public string FactorInheritanceType => FactorInheritanceTypeCode.GetEnumDescription();

		[Display(Name = "Источник для факторов отсутствующих в данных ГБУ")]
		public string Source { get; set; }

		[Display(Name = "Корректируемый фактор")]
		public long CorrectFactorId { get; set; }
		public string CorrectFactorName { get; set; }

		public List<SelectListItem> TourFactors { get; set; }
		public long TourId { get; set; }



		public FactorSettingsModel()
		{
			TourFactors = new List<SelectListItem>();
		}



		public static FactorSettingsModel FromDto(InheritanceFactorSettingDto dto)
		{
			return new FactorSettingsModel
			{
				Id = dto.Id,
				FactorId = dto.FactorId,
				FactorName = dto.FactorName,
				FactorInheritanceTypeCode = dto.FactorInheritance,
				Source = dto.Source,
				CorrectFactorName = dto.CorrectFactorName,
				CorrectFactorId = dto.CorrectFactorId
			};
		}

		public void FromEntity(OMFactorSettings entity)
		{
			Id = entity.Id;
			FactorId = entity.FactorId.GetValueOrDefault();
			FactorInheritanceTypeCode = entity.Inheritance_Code;
			Source = entity.Source;
			CorrectFactorId = entity.CorrectFactorId.GetValueOrDefault();
		}

		public InheritanceFactorSettingDto ToDto()
		{
			return new InheritanceFactorSettingDto
			{
				Id = Id,
				FactorId = FactorId,
				FactorInheritance = FactorInheritanceTypeCode,
				Source = Source,
				CorrectFactorId = CorrectFactorId,
				TourId = TourId
			};
		}
	}
}
