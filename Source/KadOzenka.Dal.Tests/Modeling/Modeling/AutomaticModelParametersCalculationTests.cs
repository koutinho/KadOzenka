using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using CommonSdks;
using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Dal.LongProcess.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using ModelingBusiness.Objects.Entities;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory.Ko;
using ObjectModel.Modeling;
using Microsoft.Extensions.DependencyInjection;
using ModelingBusiness.Factors.Exceptions.AutomaticModelParametersCalculation;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Modeling
{
	public class AutomaticModelParametersCalculationTests : BaseModelingTests
	{
		private long _modelId;
		private AutomaticModelParametersCalculationLongProcess LongProcess => Provider.GetService<AutomaticModelParametersCalculationLongProcess>();


		[SetUp]
		public void SetUp()
		{
			_modelId = RandomGenerator.GenerateRandomId();

			ModelService.Setup(x => x.GetModelEntityById(_modelId)).Returns(new ModelBuilder().Automatic().Build());
		}


		[Test]
		public void Can_Not_Calculate_Parameters_For_Non_Automatic_Model()
		{
			var manualModel = new ModelBuilder().Manual().Build();
			var factor = CreateFactor();
			ModelService.Setup(x => x.GetModelEntityById(manualModel.Id)).Returns(manualModel);
			MockModelObjects();
			MockModelFactors(factor);

			Assert.Throws<CanNotCalculateParametersForNonAutomaticModelException>(() =>
				LongProcess.StartProcess(new OMProcessType(), new OMQueue {ObjectId = manualModel.Id},
					new CancellationToken()));
		}

		[Test]
		public void Can_Not_Calculate_Parameters_Without_Model_Factors()
		{
			var modelObject = new ModelObjectBuilder().Build();
			MockModelObjects(modelObject);
			MockModelFactors();

			Assert.Throws<CanNotCalculateParametersBecauseNoFactorsException>(() => LongProcess.CalculateParameters(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Not_Calculate_Parameters_Without_Model_Objects()
		{
			var factor = CreateFactor();
			MockModelObjects();
			MockModelFactors(factor);

			Assert.Throws<CanNotCalculateParametersBecauseNoMarketObjectsException>(() => LongProcess.CalculateParameters(_modelId, new CancellationToken()));
		}

		[TestCase(MarkType.Default)]
		[TestCase(MarkType.None)]
		public void Can_Not_Calculate_Parameters_If_Model_Has_No_Factors_With_Straight_Or_Reverse_Mark_Type(MarkType mark)
		{
			var factor = new ModelFactorBuilder().MarkType(mark).Build();
			var modelObject = new ModelObjectBuilder().Coefficient(factor.FactorId).Build();
			MockModelObjects(modelObject);
			MockModelFactors(factor);

			Assert.Throws<CanNotCalculateParametersBecauseNoFactorsException>(() => LongProcess.CalculateParameters(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Not_Calculate_Parameters_If_Model_Has_No_Active_Factors()
		{
			var factor = new ModelFactorBuilder().MarkType(MarkType.Straight).Active(false).Build();
			var modelObject = new ModelObjectBuilder().Coefficient(factor.FactorId).Build();
			MockModelObjects(modelObject);
			MockModelFactors(factor);

			Assert.Throws<CanNotCalculateParametersBecauseNoFactorsException>(() => LongProcess.CalculateParameters(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Calculate_K()
		{
			var firstModelObjectCoefficient = 2;
			var secondModelObjectCoefficient = 5;
			var thirdModelObjectCoefficient = 3;
			var allCoefficients = new List<decimal> { firstModelObjectCoefficient, secondModelObjectCoefficient, thirdModelObjectCoefficient };

			var k = LongProcess.CalculateK(allCoefficients);

			var expectedK = (allCoefficients.Average() + MathExtended.CalculateMedian(allCoefficients)) / 2.0m;
			Assert.That(k, Is.EqualTo(expectedK));
		}

		[Test]
		public void Can_Calculate_CorrectionTerm()
		{
			var firstModelObjectCoefficient = 2;
			var secondModelObjectCoefficient = 5;
			var thirdModelObjectCoefficient = 3;
			var allCoefficients = new List<decimal> { firstModelObjectCoefficient, secondModelObjectCoefficient, thirdModelObjectCoefficient };

			var correctionTerm = LongProcess.CalculateCorrectingTerm(allCoefficients);

			var expectedCorrectingTerm = 0.2m * (allCoefficients.Max() - allCoefficients.Min());
			Assert.That(correctionTerm, Is.EqualTo(expectedCorrectingTerm));
		}


		#region Support Methods

		private void MockModelObjects(params OMModelToMarketObjects[] modelObjects)
		{
			var result = modelObjects?.ToList() ?? new List<OMModelToMarketObjects>();

			ModelObjectsRepository.Setup(x => x.GetIncludedModelObjects(_modelId, IncludedObjectsMode.Training,
				It.IsAny<CancellationToken>(),
				It.IsAny<Expression<Func<OMModelToMarketObjects, object>>>())).Returns(result);
		}

		private void MockModelFactors(params OMModelFactor[] modelFactors)
		{
			var result = modelFactors?.ToList() ?? new List<OMModelFactor>();
			ModelFactorsService.Setup(x => x.GetFactorsEntities(_modelId)).Returns(result);
		}

		private OMModelFactor CreateFactor()
		{
			return new ModelFactorBuilder().MarkType(MarkType.Straight).Build();
		}

		#endregion
	}
}
