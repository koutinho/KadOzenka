using Core.Shared.Extensions;
using ObjectModel.Directory.KO;
using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders.Task
{
	public class InheritanceFactorSettingsBuilder
	{
		private OMFactorSettings _inheritanceFactor;

		public InheritanceFactorSettingsBuilder()
		{
			var type = FactorInheritance.ftKvartal;

			_inheritanceFactor = new OMFactorSettings
			{
				Id = RandomGenerator.GenerateRandomId(),
				FactorId = RandomGenerator.GenerateRandomId(),
				Inheritance_Code = type,
				Inheritance = type.GetEnumDescription(),
				Source = RandomGenerator.GetRandomString(),
				CorrectFactorId = RandomGenerator.GenerateRandomId()
			};
		}



		public InheritanceFactorSettingsBuilder FactorId(long factorId)
		{
			_inheritanceFactor.FactorId = factorId;
			return this;
		}

		public InheritanceFactorSettingsBuilder CorrectFactorId(long correctFactorId)
		{
			_inheritanceFactor.CorrectFactorId = correctFactorId;
			return this;
		}

		public OMFactorSettings Build()
		{
			return _inheritanceFactor;
		}
	}
}
