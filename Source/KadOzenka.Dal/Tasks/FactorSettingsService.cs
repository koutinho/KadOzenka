using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.Tasks.Dto;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.Tasks
{
	public class FactorSettingsService
	{
		private static readonly ILogger Logger = Log.ForContext<FactorSettingsService>();


		public List<FactorSettingsDto> GetFactorSettings(List<long> tourAttributes)
		{
			if (tourAttributes.IsEmpty())
				return new List<FactorSettingsDto>();

			var factors = OMFactorSettings.Where(x => tourAttributes.Contains((long) x.FactorId) || tourAttributes.Contains((long) x.CorrectFactorId)).SelectAll().Execute();
			
			return factors.Select(x => new FactorSettingsDto
			{
				Id = x.Id,
				FactorName = GetAttributeNameSafety(x.FactorId),
				FactorInheritance = x.Inheritance_Code,
				Source = x.Source,
				CorrectFactorName = GetAttributeNameSafety(x.CorrectFactorId)
			}).ToList();
		}

		
		#region Support Methods

		private string GetAttributeNameSafety(long? attributeId)
		{
			try
			{
				return RegisterCache.GetAttributeData(attributeId.GetValueOrDefault()).Name;
			}
			catch (Exception e)
			{
				Logger.Warning(e, "В кеше не найден атрибут с ИД {AttributeId}", attributeId);
			}

			return string.Empty;
		}

		#endregion
	}
}
