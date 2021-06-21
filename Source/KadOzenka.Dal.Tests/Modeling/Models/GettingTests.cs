using System;
using System.Linq.Expressions;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.Modeling.Exceptions;
using KadOzenka.Dal.Modeling.Resources;
using Moq;
using NUnit.Framework;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Models
{
	[TestFixture]
	public class GettingTests : BaseModelTests
	{
		[TestCase(null)]
		[TestCase(0)]
		public void If_Model_Id_Is_Empty_Throw_Exception(long modelId)
		{
			var exception = Assert.Throws<Exception>(() => ModelingService.GetModelEntityById(modelId));

			StringAssert.Contains(Messages.EmptyModelId, exception.Message);
		}


		[Test]
		public void If_Model_Not_Found_By_Id_Throw_Exception()
		{
			var modelId = RandomGenerator.GenerateRandomInteger();

			ModelingRepository
				.Setup(foo => foo.GetById(modelId, It.IsAny<Expression<Func<OMModel, object>>>()))
				.Returns((OMModel) null);

			Assert.Throws<ModelNotFoundByIdException>(() => ModelingService.GetModelEntityById(modelId));
		}
	}
}