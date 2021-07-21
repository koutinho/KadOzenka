using System.Collections.Generic;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Modeling.Exceptions;
using NUnit.Framework;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.UnitTests.Modeling.Modeling
{
	public class MarksCreationTests : BaseModelingTests
	{
		[Test]
		public void Can_Not_Create_Marks_Without_Model_Objects()
		{
			var modelId = RandomGenerator.GenerateRandomId();
			var factor = new ModelFactorRelationPureBuilder().Build();
			ModelObjectsService.Setup(x => x.GetModelObjects(modelId)).Returns(new List<OMModelToMarketObjects>());
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(modelId)).Returns(new List<ModelFactorRelationPure> {factor});

			Assert.Throws<CanNotCreateMarksBecauseNoMarketObjectsException>(() => ModelingService.CreateMarks(modelId));
		}

		[Test]
		public void Can_Not_Create_Marks_Without_Model_Factors()
		{
			var modelId = RandomGenerator.GenerateRandomId();
			var modelObject = new ModelObjectBuilder().Build();
			ModelObjectsService.Setup(x => x.GetModelObjects(modelId)).Returns(new List<OMModelToMarketObjects>{ modelObject });
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(modelId)).Returns(new List<ModelFactorRelationPure>());

			Assert.Throws<CanNotCreateMarksBecauseNoFactorsException>(() => ModelingService.CreateMarks(modelId));
		}
	}
}
