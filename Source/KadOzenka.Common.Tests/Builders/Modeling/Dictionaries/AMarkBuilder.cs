using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders.Modeling.Dictionaries
{
	public abstract class AMarkBuilder
	{
		protected readonly OMModelingDictionariesValues _mark;


		protected AMarkBuilder()
		{
			_mark = new OMModelingDictionariesValues
			{
				DictionaryId = RandomGenerator.GenerateRandomId(),
				Value = RandomGenerator.GetRandomString(),
				CalculationValue = RandomGenerator.GenerateRandomInteger()
			};
		}


		public AMarkBuilder Dictionary(long? dictionaryId)
		{
			_mark.DictionaryId = dictionaryId.GetValueOrDefault();
			return this;
		}

		public AMarkBuilder Value(string value)
		{
			_mark.Value = value;
			return this;
		}

		public AMarkBuilder Metka(decimal metka)
		{
			_mark.CalculationValue = metka;
			return this;
		}


		public abstract OMModelingDictionariesValues Build();
	}
}