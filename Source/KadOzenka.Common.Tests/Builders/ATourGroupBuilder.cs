using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders
{
	public abstract class ATourGroupBuilder
	{
		protected readonly OMTourGroup _tourGroup;

		protected ATourGroupBuilder()
		{
			_tourGroup = new OMTourGroup
			{
				TourId = RandomGenerator.GenerateRandomInteger(),
				GroupId = RandomGenerator.GenerateRandomInteger()
			};
		}

		public ATourGroupBuilder Tour(long tourId)
		{
			_tourGroup.TourId = tourId;
			return this;
		}

		public ATourGroupBuilder Group(long groupId)
		{
			_tourGroup.GroupId = groupId;
			return this;
		}


		public abstract OMTourGroup Build();
	}
}
