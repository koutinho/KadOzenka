using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders
{
	public abstract class ATourBuilder
	{
		protected readonly OMTour _tour;

		protected ATourBuilder()
		{
			_tour = new OMTour
			{
				Year = RandomGenerator.GenerateRandomInteger(2030, 3000)
			};
		}

		public abstract OMTour Build();
	}
}
