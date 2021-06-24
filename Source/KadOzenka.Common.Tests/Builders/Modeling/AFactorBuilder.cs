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
			var algorithm = KoAlgoritmType.Line;
			var markType = ObjectModel.Directory.Ko.MarkType.None;

			_factor = new OMModelFactor
			{
				ModelId = RandomGenerator.GenerateRandomInteger(),
				FactorId = RandomGenerator.GenerateRandomInteger(),
				MarkerId = RandomGenerator.GenerateRandomInteger(),
				Weight = RandomGenerator.GenerateRandomDecimal(),
				B0 = RandomGenerator.GenerateRandomDecimal(),
				SignDiv = true,
				SignAdd = true,
				SignMarket = true,
				DictionaryId = RandomGenerator.GenerateRandomInteger(),
				AlgorithmType = algorithm.GetEnumDescription(),
				AlgorithmType_Code = algorithm,
				PreviousWeight = RandomGenerator.GenerateRandomDecimal(),
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
			_factor.Weight = (decimal) weight;
			return this;
		}

		public AFactorBuilder Correction(decimal weight)
		{
			_factor.Weight = weight;
			return this;
		}

		public AFactorBuilder Coefficient(double b0)
		{
			_factor.B0 = (decimal) b0;
			return this;
		}

		public AFactorBuilder Coefficient(decimal b0)
		{
			_factor.B0 = b0;
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

		public AFactorBuilder Model(OMModel model)
		{
			_factor.ModelId = model.Id;
			_factor.AlgorithmType = model.AlgoritmType_Code.GetEnumDescription();
			_factor.AlgorithmType_Code = model.AlgoritmType_Code;

			return this;
		}


		public abstract OMModelFactor Build();
	}
}