using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders.Modeling
{
	public abstract class AMarkBuilder
	{
		protected readonly OMMarkCatalog _mark;


		protected AMarkBuilder()
		{
			_mark = new OMMarkCatalog
			{
				GroupId = RandomGenerator.GenerateRandomInteger(),
				FactorId = RandomGenerator.GenerateRandomInteger(),
				ValueFactor = RandomGenerator.GetRandomString(),
				MetkaFactor = RandomGenerator.GenerateRandomDecimal()
			};
		}


		public AMarkBuilder Factor(long? factorId)
		{
			_mark.FactorId = factorId.GetValueOrDefault();
			return this;
		}

		public AMarkBuilder Group(long groupId)
		{
			_mark.GroupId = groupId;
			return this;
		}

		public AMarkBuilder Value(string value)
		{
			_mark.ValueFactor = value;
			return this;
		}

		public AMarkBuilder Metka(decimal metka)
		{
			_mark.MetkaFactor = metka;
			return this;
		}


		public abstract OMMarkCatalog Build();
	}
}