using KadOzenka.Dal.Tasks.Dto;
using ObjectModel.Directory.KO;

namespace KadOzenka.Common.Tests.Builders.Task
{
	public class InheritanceFactorSettingsDtoBuilder
	{
		private FactorSettingsDto _factor;

		public InheritanceFactorSettingsDtoBuilder()
		{
			_factor = new FactorSettingsDto
			{
				Id = RandomGenerator.GenerateRandomId(),
				FactorId = RandomGenerator.GenerateRandomId(),
				FactorName = RandomGenerator.GetRandomString(),
				FactorInheritance = FactorInheritance.ftKvartal,
				Source = RandomGenerator.GetRandomString(),
				CorrectFactorId = RandomGenerator.GenerateRandomId(),
				CorrectFactorName = RandomGenerator.GetRandomString()
			};
		}



		public InheritanceFactorSettingsDtoBuilder FactorId(long factorId)
		{
			_factor.FactorId = factorId;
			return this;
		}

		public InheritanceFactorSettingsDtoBuilder CorrectFactorId(long correctFactorId)
		{
			_factor.CorrectFactorId = correctFactorId;
			return this;
		}

		public FactorSettingsDto Build()
		{
			return _factor;
		}
	}
}
