using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommonSdks;
using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Consts;
using KadOzenka.Dal.Integration._Builders.Model;
using KadOzenka.Dal.IntegrationTests.Modeling;
using KadOzenka.Dal.LongProcess.Modeling;
using ModelingBusiness.Factors.Exceptions.AutomaticModelParametersCalculation;
using ModelingBusiness.Objects.Entities;
using NUnit.Framework;
using ObjectModel.Directory.Ko;
using ObjectModel.Directory.KO;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.IntegrationTests.Factors.Modeling
{
	public class AutomaticModelParametersCalculationTests : BaseModelingTests
	{
		private OMModel _model;
		private OMModelFactor _squareFactor;
		private AutomaticModelParametersCalculationLongProcess CalculationLongProcess { get; set; }


		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			CalculationLongProcess = new AutomaticModelParametersCalculationLongProcess();
		}


		[SetUp]
		public void SetUp()
		{
			_model = new ModelBuilder().Automatic().IsActive(false).Build();
			
			var dictionary = new DictionaryBuilder().Type(ModelDictionaryType.String).Build();

			_squareFactor = new ModelFactorBuilder().Model(_model).FactorId(Tour2018OksFactorsAttributeIds.SquareAttributeId)
				.Dictionary(dictionary).MarkType(MarkType.Straight).Build();
		}



		[Test]
		public void CanNot_Calculate_Parameters_If_Model_Object_Is_Excluded()
		{
			new ModelObjectBuilder().Model(_model).ForControl(true).Excluded(true).Build();
			
			Assert.Throws<CanNotCalculateParametersBecauseNoMarketObjectsException>(() => CalculationLongProcess.CalculateParameters(_model.Id, new CancellationToken()));
		}


		[Test]
		public void CanNot_Process_Model_Object_With_Empty_Required_Factors()
		{
			var coefficients = new List<CoefficientForObject>
			{
				CreateCoefficientForDistanceToMkad(),
				CreateCoefficientForSquare(null)
			};
			var modelObject = CreateModelObject(coefficients);
			CreateDistanceToMkadFactor();
			
			CalculationLongProcess.CalculateParameters(_model.Id, new CancellationToken());

			var updatedModelObject = OMModelToMarketObjects.Where(x => x.Id == modelObject.Id).Select(x => new{x.IsExcluded}).ExecuteFirstOrDefault();
			Assert.That(updatedModelObject.IsExcluded, Is.EqualTo(true));
		}

		[Test]
		public void Can_Calculate_Parameters()
		{
			var firstDistance = 1;
			var firstSquare = 2;
			var secondDistance = 3;
			var secondSquare = 4;
			var firstModelObjectCoefficients = new List<CoefficientForObject>
			{
				CreateCoefficientForDistanceToMkad(firstDistance),
				CreateCoefficientForSquare(firstSquare)
			};
			var secondModelObjectCoefficients = new List<CoefficientForObject>
			{
				CreateCoefficientForDistanceToMkad(secondDistance),
				CreateCoefficientForSquare(secondSquare)
			};
			CreateModelObject(firstModelObjectCoefficients);
			CreateModelObject(secondModelObjectCoefficients);
			CreateDistanceToMkadFactor();


			CalculationLongProcess.CalculateParameters(_model.Id, new CancellationToken());


			var updatedSquareFactor = OMModelFactor.Where(x => x.Id == _squareFactor.Id).Select(x => new {x.K, x.CorrectingTerm}).ExecuteFirstOrDefault();
			var allCoefficientsForSquare = new List<decimal> {firstSquare, secondSquare};
			var expectedK = (allCoefficientsForSquare.Average() + MathExtended.CalculateMedian(allCoefficientsForSquare)) / 2.0m;
			var expectedCorrectingTerm = 0.2m * (allCoefficientsForSquare.Max() - allCoefficientsForSquare.Min());
			Assert.That(updatedSquareFactor.K, Is.EqualTo(expectedK));
			Assert.That(updatedSquareFactor.CorrectingTerm, Is.EqualTo(expectedCorrectingTerm));
		}


		#region Support Methods

		private OMModelToMarketObjects CreateModelObject(List<CoefficientForObject> coefficients)
		{
			return new ModelObjectBuilder().Model(_model).ForControl(true).Excluded(false).Coefficients(coefficients).Build();
		}

		private CoefficientForObject CreateCoefficientForSquare(decimal? square = null)
		{
			return new CoefficientForObject(Tour2018OksFactorsAttributeIds.SquareAttributeId)
			{
				Coefficient = square,
				Value = square?.ToString()
			};
		}

		private CoefficientForObject CreateCoefficientForDistanceToMkad(decimal? distance = null)
		{
			var value = distance ?? RandomGenerator.GenerateRandomDecimal();
			return new CoefficientForObject(Tour2018OksFactorsAttributeIds.DistanceToMkadAttributeId)
			{
				Coefficient = value,
				Value = value.ToString()
			};
		}

		private OMModelFactor CreateDistanceToMkadFactor()
		{
			return new ModelFactorBuilder().Model(_model)
				.FactorId(Tour2018OksFactorsAttributeIds.DistanceToMkadAttributeId)
				.MarkType(MarkType.Straight).Build();
		}

		#endregion
	}
}
