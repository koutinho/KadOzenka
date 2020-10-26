using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.OutliersChecking.Dto;
using ObjectModel.Market;

namespace KadOzenka.Dal.OutliersChecking
{
	public class OutliersCheckingSettingsService
	{
		public List<OutliersCheckingSettingDto> GetOutliersCheckingSettings()
		{
			var settings = OMCoefficientsOutliersChecking.Where(x => true)
				.SelectAll()
				.Execute();

			return settings.Select(x => new OutliersCheckingSettingDto
			{
				Id = x.Id,
				Zone = x.Zone,
				District = x.District_Code,
				Region = x.Region_Code,
				MinDeltaCoef = x.MinDeltaCoef,
				MaxDeltaCoef = x.MaxDeltaCoef
			}).OrderBy(x => x.Zone).ThenBy(x =>x.District).ThenBy(x => x.Region).ToList();
		}

		public void UpdateOutliersCheckingSettings(OutliersCheckingSettingDto dto)
		{
			var setting = OMCoefficientsOutliersChecking
				.Where(x => x.Zone == dto.Zone && x.District_Code == dto.District && x.Region_Code == dto.Region)
				.SelectAll().ExecuteFirstOrDefault();
			if (setting == null)
			{
				throw new Exception($"Не найдена запись для 'Зона {dto.Zone}_{dto.District.GetShortTitle()}_{dto.Region.GetEnumDescription()}'");
			}

			setting.MinDeltaCoef = dto.MinDeltaCoef;
			setting.MaxDeltaCoef = dto.MaxDeltaCoef;
			setting.Save();
		}
	}
}
