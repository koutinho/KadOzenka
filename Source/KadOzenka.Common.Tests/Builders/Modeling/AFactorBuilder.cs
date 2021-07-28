using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders.Modeling
{
	public abstract class AFactorBuilder
	{
		public long Id => _factor.Id;

		protected readonly OMModelFactor _factor;


		protected AFactorBuilder()
		{
			var markType = ObjectModel.Directory.Ko.MarkType.None;

			_factor = new OMModelFactor
			{
				ModelId = RandomGenerator.GenerateRandomId(),
				FactorId = RandomGenerator.GenerateRandomId(),
				Correction = RandomGenerator.GenerateRandomDecimal(),
				CoefficientForLinear = RandomGenerator.GenerateRandomDecimal(),
				CoefficientForExponential = RandomGenerator.GenerateRandomDecimal(),
				CoefficientForMultiplicative = RandomGenerator.GenerateRandomDecimal(),
				SignMarket = true,
				DictionaryId = RandomGenerator.GenerateRandomId(),
				IsActive = true,
				MarkType = markType.GetEnumDescription(),
				MarkType_Code = markType,
				CorrectingTerm = RandomGenerator.GenerateRandomDecimal(),
				K = RandomGenerator.GenerateRandomDecimal()
			};
		}


		public AFactorBuilder FactorId(long factorId)
		{
			_factor.FactorId = factorId;
			return this;
		}

		public AFactorBuilder Dictionary(long dictionaryId)
		{
			_factor.DictionaryId = dictionaryId;
			return this;
		}

		public AFactorBuilder Dictionary(OMModelingDictionary dictionary)
		{
			_factor.DictionaryId = dictionary.Id;
			return this;
		}

		public AFactorBuilder MarkType(MarkType markType)
		{
			_factor.MarkType = markType.GetEnumDescription();
			_factor.MarkType_Code = markType;
			return this;
		}

		public AFactorBuilder Model(long modelId)
		{
			_factor.ModelId = modelId;
			return this;
		}

		public AFactorBuilder Correction(double weight)
		{
			_factor.Correction = (decimal) weight;
			return this;
		}

		public AFactorBuilder Correction(decimal correction)
		{
			_factor.Correction = correction;
			return this;
		}

		public AFactorBuilder Coefficient(double coefficient, KoAlgoritmType type)
		{
			_factor.SetCoefficient((decimal) coefficient, type);
			return this;
		}

		public AFactorBuilder Coefficient(decimal coefficient, KoAlgoritmType type)
		{
			_factor.SetCoefficient(coefficient, type);
			return this;
		}

		public AFactorBuilder CorrectingTerm(double correctingTerm)
		{
			_factor.CorrectingTerm = (decimal)correctingTerm;
			return this;
		}

		public AFactorBuilder K(double k)
		{
			_factor.K = (decimal)k;
			return this;
		}

		public AFactorBuilder Active(bool isActive)
		{
			_factor.IsActive = isActive;
			return this;
		}

		public AFactorBuilder Model(OMModel model)
		{
			_factor.ModelId = model.Id;
			return this;
		}


		public abstract OMModelFactor Build();
	}
}