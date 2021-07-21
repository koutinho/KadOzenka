using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KadOzenka.Common.Tests;
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

namespace KadOzenka.Dal.UnitTests.Modeling.Modeling
{
	public class MarksCreationTests : BaseModelingTests
	{
		private long _modelId;
		private long _addressAttributeId;

		[SetUp]
		public void SetUp()
		{
			_addressAttributeId = RandomGenerator.GenerateRandomId();

			_modelId = RandomGenerator.GenerateRandomId();
		}


		[Test]
		public void Can_Not_Create_Marks_Without_Model_Objects()
		{
			var factor = new ModelFactorRelationPureBuilder().MarkType(MarkType.Default).Build();
			MockModelObjects();
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(new List<ModelFactorRelationPure> {factor});

			Assert.Throws<CanNotCreateMarksBecauseNoMarketObjectsException>(() => ModelingService.CreateMarks(_modelId));
		}

		[Test]
		public void Can_Not_Create_Marks_Without_Model_Factors()
		{
			var modelObject = new ModelObjectBuilder().Build();
			MockModelObjects(modelObject);
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(new List<ModelFactorRelationPure>());

			Assert.Throws<CanNotCreateMarksBecauseNoFactorsException>(() => ModelingService.CreateMarks(_modelId));
		}


		[Test]
		public void Can_Not_Create_Marks_For_Factor_Without_DictionaryId()
		{
			var modelObject = new ModelObjectBuilder().Build();
			var factor = new ModelFactorRelationPureBuilder().MarkType(MarkType.Default).DictionaryId(null).Build();
			MockModelObjects(modelObject);
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(new List<ModelFactorRelationPure> { factor });

			Assert.Throws<CanNotCreateMarksBecauseNoDictionaryException>(() => ModelingService.CreateMarks(_modelId));
		}


		#region Support Methods

		private void MockModelObjects(params OMModelToMarketObjects[] modelObjects)
		{
			var result = modelObjects?.ToList() ?? new List<OMModelToMarketObjects>();

			ModelObjectsRepository.Setup(x => x.GetIncludedModelObjects(_modelId, IncludedObjectsMode.Training,
					It.IsAny<Expression<Func<OMModelToMarketObjects, object>>>())).Returns(result);
		}

		#endregion
	}
}
