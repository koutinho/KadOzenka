using KadOzenka.Common.Tests;
using ModelingBusiness.Model.Entities;
using ModelingBusiness.Model.Exceptions;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Models
{
	[TestFixture]
	public class CreationTests : BaseModelTests
	{
		[Test]
		public void CanNot_Create_Model_With_Empty_Name()
		{
			var modelDto = GetModelInfoDto();
			modelDto.Name = null;
			
			var exception = Assert.Throws<ModelCrudException>(() => ModelService.AddAutomaticModel(modelDto));

			StringAssert.Contains(ModelingBusiness.Messages.EmptyName, exception.Message);
			ModelingRepository.Verify(foo => foo.Save(It.IsAny<OMModel>()), Times.Never);
		}


		#region Support Methods

		private ModelDto GetModelInfoDto()
		{
			return new ModelDto
			{
				Name = RandomGenerator.GetRandomString(),
				Description = RandomGenerator.GetRandomString(),
				GroupId = RandomGenerator.GenerateRandomInteger(),
				IsOksObjectType = true,
				AlgorithmTypeForCadastralPriceCalculation = KoAlgoritmType.Line,
				Type = KoModelType.Automatic
			};
		}

		#endregion
	}
}