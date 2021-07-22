using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders.Modeling
{
	public abstract class AModelBuilder
	{
		protected readonly OMModel _model;

		protected AModelBuilder()
		{
			_model = new OMModel
			{
				GroupId = RandomGenerator.GenerateRandomInteger(),
				Name = RandomGenerator.GetRandomString(maxNumberOfCharacters: 10),
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
				ObjectsStatistic = RandomGenerator.GetRandomString(),
				IsActive = true
			};
		}

		public abstract OMModel Build();


		public AModelBuilder Group(long groupId)
		{
			_model.GroupId = groupId;
			return this;
		}

		public AModelBuilder Name(string name)
		{
			_model.Name = name;
			return this;
		}

		public AModelBuilder IsActive(bool isActive)
		{
			_model.IsActive = isActive;
			return this;
		}

		public AModelBuilder Manual()
		{
			var type = KoModelType.Manual;
			_model.Type = type.GetEnumDescription();
			_model.Type_Code = type;
			return this;
		}

		public AModelBuilder Automatic()
		{
			var type = KoModelType.Automatic;
			_model.Type = type.GetEnumDescription();
			_model.Type_Code = type;
			return this;
		}

		public AModelBuilder AlgorithmType(KoAlgoritmType type)
		{
			_model.AlgoritmType = type.GetEnumDescription();
			_model.AlgoritmType_Code = type;
			return this;
		}

		public AModelBuilder A0(decimal a0)
		{
			_model.SetA0(a0);
			return this;
		}

		public AModelBuilder LinearTrainingResult(string result)
		{
			_model.LinearTrainingResult = result;
			return this;
		}

		public AModelBuilder ExponentialTrainingResult(string result)
		{
			_model.ExponentialTrainingResult = result;
			return this;
		}

		public AModelBuilder MultiplicativeTrainingResult(string result)
		{
			_model.MultiplicativeTrainingResult = result;
			return this;
		}
	}
}