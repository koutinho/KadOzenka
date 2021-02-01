using System;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Exceptions;
using KadOzenka.Dal.Modeling.Resources;
using Moq;
using NUnit.Framework;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Modeling.Models
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
			var modelingRepositoryMock = new Mock<IModelingRepository>();
			modelingRepositoryMock.Setup(foo => foo.GetModelById(It.IsAny<long>())).Returns((OMModel) null);
			ModelingService.StubModelingRepository(modelingRepositoryMock.Object);

			var modelId = Random.Next();
			Assert.Throws<ModelNotFoundByIdException>(() => ModelingService.GetModelEntityById(modelId));
		}
	}
}