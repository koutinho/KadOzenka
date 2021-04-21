using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;

namespace KadOzenka.Dal.DataImport.Validation
{
	public static class DataImporterGknValidator
	{
		public static void ValidateExcelColumnsForNotPetition(IEnumerable<long> attributeIds)
		{
			var notSelectedRequiredAttributeIds = DataImporterGknNew.Consts.RequiredAttributeIds.Except(attributeIds).ToList();
			if (notSelectedRequiredAttributeIds.Count != 0)
			{
				var attributeNames = RegisterCache.RegisterAttributes
					.Where(x => notSelectedRequiredAttributeIds.Contains(x.Key)).Select(x => x.Value.Name).ToList();
				var message = string.Join(',', attributeNames);

				throw new Exception($"Не указаны обязательные параметры: {message}");
			}
		}
	}
}
