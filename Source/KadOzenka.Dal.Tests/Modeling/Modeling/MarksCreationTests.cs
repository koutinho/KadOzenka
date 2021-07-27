using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.LongProcess.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using ModelingBusiness.Dictionaries.Entities;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Modeling.Exceptions;
using ModelingBusiness.Objects.Entities;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory.Ko;
using ObjectModel.Modeling;
using Microsoft.Extensions.DependencyInjection;

namespace KadOzenka.Dal.UnitTests.Modeling.Modeling
{
	public class MarksCreationTests : BaseModelingTests
	{
		private long _modelId;
		private MarksCalculationLongProcess MarksCalculationLongProcess => Provider.GetService<MarksCalculationLongProcess>();


		[SetUp]
		public void SetUp()
		{
			_modelId = RandomGenerator.GenerateRandomId();

			ModelService.Setup(x => x.GetModelEntityById(_modelId)).Returns(new ModelBuilder().Automatic().Build());
		}


		[Test]
		public void Can_Not_Create_Marks_For_Non_Automatic_Model()
		{
			var factor = new ModelFactorRelationDtoBuilder().MarkType(MarkType.Default).Build();
			ModelService.Setup(x => x.GetModelEntityById(_modelId)).Returns(new ModelBuilder().Manual().Build());
			MockModelObjects();
			MockModelFactors(factor);

			Assert.Throws<CanNotCreateMarksForNonAutomaticModelException>(() => MarksCalculationLongProcess.CalculateMarks(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Not_Create_Marks_Without_Model_Objects()
		{
			var factor = new ModelFactorRelationDtoBuilder().MarkType(MarkType.Default).Build();
			MockModelObjects();
			MockModelFactors(factor);

			Assert.Throws<CanNotCreateMarksBecauseNoMarketObjectsException>(() => MarksCalculationLongProcess.CalculateMarks(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Not_Create_Marks_Without_Model_Factors()
		{
			var modelObject = new ModelObjectBuilder().Build();
			MockModelObjects(modelObject);
			MockModelFactors();

			Assert.Throws<CanNotCreateMarksBecauseNoFactorsException>(() => MarksCalculationLongProcess.CalculateMarks(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Not_Create_Marks_If_Model_Has_No_Factors_With_Default_Mark_Type()
		{
			var factor = new ModelFactorRelationDtoBuilder().MarkType(MarkType.Straight).Build();
			var modelObject = new ModelObjectBuilder().Coefficient(factor.AttributeId).Build();
			MockModelObjects(modelObject);
			MockModelFactors(factor);

			Assert.Throws<CanNotCreateMarksBecauseNoFactorsException>(() => MarksCalculationLongProcess.CalculateMarks(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Not_Create_Marks_If_Model_Has_No_Active_Factors()
		{
			var factor = new ModelFactorRelationDtoBuilder().MarkType(MarkType.Default).Active(false).Build();
			var modelObject = new ModelObjectBuilder().Coefficient(factor.AttributeId).Build();
			MockModelObjects(modelObject);
			MockModelFactors(factor);

			Assert.Throws<CanNotCreateMarksBecauseNoFactorsException>(() => MarksCalculationLongProcess.CalculateMarks(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Not_Create_Marks_For_Factor_Without_DictionaryId()
		{
			var factor = new ModelFactorRelationDtoBuilder().MarkType(MarkType.Default).DictionaryId(null).Build();
			var modelObject = new ModelObjectBuilder().Coefficient(factor.AttributeId).Build();
			MockModelObjects(modelObject);
			MockModelFactors(factor);

			Assert.Throws<CanNotCreateMarksBecauseNoDictionaryException>(() => MarksCalculationLongProcess.CalculateMarks(_modelId, new CancellationToken()));
		}

		[Test]
		public void Can_Not_Create_Marks_If_Model_Objects_Have_No_Model_Factors()
		{
			var factor = new ModelFactorRelationDtoBuilder().MarkType(MarkType.Default).Build();
			var modelObject = new ModelObjectBuilder().Build();
			MockModelObjects(modelObject);
			MockModelFactors(factor);

			Assert.Throws<CanNotCreateMarksBecauseNoMarketObjectsWithSelectedFactorsException>(() => MarksCalculationLongProcess.CalculateMarks(_modelId, new CancellationToken()));
		}


		#region Support Methods

		private void MockModelObjects(params OMModelToMarketObjects[] modelObjects)
		{
			var result = modelObjects?.ToList() ?? new List<OMModelToMarketObjects>();

			ModelObjectsRepository.Setup(x => x.GetIncludedModelObjects(_modelId, IncludedObjectsMode.Training,
				It.IsAny<CancellationToken>(),
				It.IsAny<Expression<Func<OMModelToMarketObjects, object>>>())).Returns(result);
		}

		private void MockModelFactors(params ModelFactorRelation[] modelFactors)
		{
			var result = modelFactors?.ToList() ?? new List<ModelFactorRelation>();
			ModelFactorsService.Setup(x => x.GetFactors(_modelId)).Returns(result);
		}

		#endregion
	}
}
