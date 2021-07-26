using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.LongProcess.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using KadOzenka.Dal.UnitTests.Modeling.Modeling;
using Microsoft.Extensions.DependencyInjection;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Factors.Exceptions.AutomaticModelParametersCalculation;
using ModelingBusiness.Modeling.Exceptions;
using ModelingBusiness.Objects.Entities;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory.Ko;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.UnitTests.Modeling.Factors
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
		public void Can_Not_Create_Marks_For_Non_Automatic_Model()
		{
			var factor = new ModelFactorRelationPureBuilder().MarkType(MarkType.Straight).Build();
			ModelService.Setup(x => x.GetModelEntityById(_modelId)).Returns(new ModelBuilder().Manual().Build());
			MockModelObjects();
			MockModelFactors(factor);

			Assert.Throws<CanNotCalculateParametersForNonAutomaticModelException>(() => LongProcess.CalculateParameters(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Not_Create_Marks_Without_Model_Factors()
		{
			var modelObject = new ModelObjectBuilder().Build();
			MockModelObjects(modelObject);
			MockModelFactors();

			Assert.Throws<CanNotCalculateParametersBecauseNoFactorsException>(() => LongProcess.CalculateParameters(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Not_Create_Marks_Without_Model_Objects()
		{
			var factor = new ModelFactorRelationPureBuilder().MarkType(MarkType.Straight).Build();
			MockModelObjects();
			MockModelFactors(factor);

			Assert.Throws<CanNotCalculateParametersBecauseNoMarketObjectsException>(() => LongProcess.CalculateParameters(_modelId, new CancellationToken()));
		}

		[TestCase(MarkType.Default)]
		[TestCase(MarkType.None)]
		public void Can_Not_Create_Marks_If_Model_Has_No_Factors_With_Straight_Or_Reverse_Mark_Type(MarkType mark)
		{
			var factor = new ModelFactorRelationPureBuilder().MarkType(mark).Build();
			var modelObject = new ModelObjectBuilder().Coefficient(factor.AttributeId).Build();
			MockModelObjects(modelObject);
			MockModelFactors(factor);

			Assert.Throws<CanNotCalculateParametersBecauseNoFactorsException>(() => LongProcess.CalculateParameters(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Not_Create_Marks_If_Model_Has_No_Active_Factors()
		{
			var factor = new ModelFactorRelationPureBuilder().MarkType(MarkType.Straight).Active(false).Build();
			var modelObject = new ModelObjectBuilder().Coefficient(factor.AttributeId).Build();
			MockModelObjects(modelObject);
			MockModelFactors(factor);

			Assert.Throws<CanNotCalculateParametersBecauseNoFactorsException>(() => LongProcess.CalculateParameters(_modelId, new CancellationToken()));
		}


		#region Support Methods

		private void MockModelObjects(params OMModelToMarketObjects[] modelObjects)
		{
			var result = modelObjects?.ToList() ?? new List<OMModelToMarketObjects>();

			ModelObjectsRepository.Setup(x => x.GetIncludedModelObjects(_modelId, IncludedObjectsMode.Training,
				It.IsAny<CancellationToken>(),
				It.IsAny<Expression<Func<OMModelToMarketObjects, object>>>())).Returns(result);
		}

		private void MockModelFactors(params ModelFactorRelationPure[] modelFactors)
		{
			var result = modelFactors?.ToList() ?? new List<ModelFactorRelationPure>();
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(result);
		}

		#endregion
	}
}
