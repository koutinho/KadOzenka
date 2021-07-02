using KadOzenka.Dal.Tasks.Dto;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Dto;
using ObjectModel.Directory.KO;

namespace KadOzenka.Common.Tests.Builders.Task
{
	public class InheritanceFactorSettingsDtoBuilder
	{
		private InheritanceFactorSettingDto _inheritanceFactor;

		public InheritanceFactorSettingsDtoBuilder()
		{
			_inheritanceFactor = new InheritanceFactorSettingDto
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
			_inheritanceFactor.FactorId = factorId;
			return this;
		}

		public InheritanceFactorSettingsDtoBuilder CorrectFactorId(long correctFactorId)
		{
			_inheritanceFactor.CorrectFactorId = correctFactorId;
			return this;
		}

		public InheritanceFactorSettingDto Build()
		{
			return _inheritanceFactor;
		}
	}
}
