using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.OutliersChecking.Dto;
using Microsoft.AspNetCore.Http;

namespace KadOzenka.Web.Models.MarketObject
{
	public class OutliersSettingsImportModel
	{
		/// <summary>
		/// Зона
		/// </summary>
		[Display(Name = "Зона")]
		[Required(ErrorMessage = "Поле Зона обязательное")]
		public string ZoneColumnName { get; set; }

		/// <summary>
		/// Административный округ
		/// </summary>
		[Display(Name = "Административный округ")]
		[Required(ErrorMessage = "Поле Административный округ обязательное")]
		public string DistrictColumnName { get; set; }

		/// <summary>
		/// Район
		/// </summary>
		[Display(Name = "Район")]
		[Required(ErrorMessage = "Поле Район обязательное")]
		public string RegionColumnName { get; set; }

		/// <summary>
		/// Коэффициент минимальной разности
		/// </summary>
		[Display(Name = "Коэффициент минимальной разности")]
		[Required(ErrorMessage = "Поле Коэффициент минимальной разности обязательное")]
		public string MinCoefColumnName { get; set; }

		/// <summary>
		/// Коэффициент максимальной разности
		/// </summary>
		[Display(Name = "Коэффициент максимальной разности")]
		[Required(ErrorMessage = "Поле Коэффициент максимальной разности обязательное")]
		public string MaxCoefColumnName { get; set; }

		[Display(Name = "Удалить старые данные")]
		public bool DeleteOldValues { get; set; } = false;

		public OutliersCheckingSettingImportFromExcelDto ToDto(IFormFile file)
		{
			return new OutliersCheckingSettingImportFromExcelDto
			{
				ZoneColumnName = ZoneColumnName,
				DistrictColumnName = DistrictColumnName,
				RegionColumnName = RegionColumnName,
				MinCoefColumnName = MinCoefColumnName,
				MaxCoefColumnName = MaxCoefColumnName,
				DeleteOldValues = DeleteOldValues,
				FileName = file.FileName
			};
		}
	}
}
