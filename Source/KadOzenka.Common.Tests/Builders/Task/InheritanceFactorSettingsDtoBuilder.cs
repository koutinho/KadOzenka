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
				CorrectFactorName = RandomGenerator.GetRandomString(),
				TourId = RandomGenerator.GenerateRandomId()
			};
		}


		public InheritanceFactorSettingsDtoBuilder Id(long id)
		{
			_inheritanceFactor.Id = id;
			return this;
		}

		public InheritanceFactorSettingsDtoBuilder FactorId(long? factorId)
		{
			_inheritanceFactor.FactorId = factorId.GetValueOrDefault();
			return this;
		}

		public InheritanceFactorSettingsDtoBuilder CorrectFactorId(long? correctFactorId)
		{
			_inheritanceFactor.CorrectFactorId = correctFactorId.GetValueOrDefault();
			return this;
		}

		public InheritanceFactorSettingDto Build()
		{
			return _inheritanceFactor;
		}
	}
}
