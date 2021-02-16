using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Modeling.Builders
{
	public class ModelBuilder
	{
		private readonly OMModel _model;

		public ModelBuilder()
		{
			_model = new OMModel
			{
				Id = RandomGenerator.GenerateRandomInteger(),
				GroupId = RandomGenerator.GenerateRandomInteger(),
				Name = RandomGenerator.GetRandomString(),
				Description = RandomGenerator.GetRandomString(),
				Formula = RandomGenerator.GetRandomString(),
				AlgoritmType = KoAlgoritmType.Line.GetEnumDescription(),
				AlgoritmType_Code = KoAlgoritmType.Line,
				A0 = RandomGenerator.GenerateRandomDecimal(),
				CalculationType = KoCalculationType.Comparative.GetEnumDescription(),
				CalculationType_Code = KoCalculationType.Comparative,
				CalculationMethod = KoCalculationMethod.IndividualCalculation.GetEnumDescription(),
				CalculationMethod_Code = KoCalculationMethod.IndividualCalculation,
				LinearTrainingResult = RandomGenerator.GetRandomString(),
				ExponentialTrainingResult = RandomGenerator.GetRandomString(),
				MultiplicativeTrainingResult = RandomGenerator.GetRandomString(),
				IsOksObjectType = false,
				Type = KoModelType.Manual.GetEnumDescription(),
				Type_Code = KoModelType.Manual,
				A0ForExponential = RandomGenerator.GenerateRandomDecimal(),
				A0ForMultiplicative = RandomGenerator.GenerateRandomDecimal(),
				A0ForLinearTypeInPreviousTour = RandomGenerator.GenerateRandomDecimal(),
				A0ForExponentialTypeInPreviousTour = RandomGenerator.GenerateRandomDecimal(),
				A0ForMultiplicativeTypeInPreviousTour = RandomGenerator.GenerateRandomDecimal(),
				ObjectsStatistic = RandomGenerator.GetRandomString(),
				IsActive = true
			};
		}

		public ModelBuilder IsActive(bool isActive)
		{
			_model.IsActive = isActive;
			return this;
		}

		public ModelBuilder Manual()
		{
			var type = KoModelType.Manual;
			_model.Type = type.GetEnumDescription();
			_model.Type_Code = type;
			return this;
		}

		public ModelBuilder Automatic()
		{
			var type = KoModelType.Automatic;
			_model.Type = type.GetEnumDescription();
			_model.Type_Code = type;
			return this;
		}

		public ModelBuilder AlgorithmType(KoAlgoritmType type)
		{
			_model.AlgoritmType = type.GetEnumDescription();
			_model.AlgoritmType_Code = type;
			return this;
		}

		public ModelBuilder A0(decimal a0)
		{
			_model.A0 = a0;
			return this;
		}

		public ModelBuilder LinearTrainingResult(string result)
		{
			_model.LinearTrainingResult = result;
			return this;
		}

		public ModelBuilder ExponentialTrainingResult(string result)
		{
			_model.ExponentialTrainingResult = result;
			return this;
		}

		public ModelBuilder MultiplicativeTrainingResult(string result)
		{
			_model.MultiplicativeTrainingResult = result;
			return this;
		}

		public OMModel Build()
		{
			return _model;
		}
	}
}