using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Exceptions;
using KadOzenka.Dal.Modeling.Resources;
using NUnit.Framework;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Tests.Modeling.Models
{
	[TestFixture]
	public class GettingTests : BaseModelTests
	{
		[TestCase(null)]
		[TestCase(0)]
		public void If_Model_Id_Is_Empty_Throw_Exception(long modelId)
		{
			var exception = Assert.Throws<EmptyModelIdException>(() => ModelingService.GetModelEntityById(modelId));

			StringAssert.Contains(Messages.EmptyModelId, exception.Message);
		}

		
		#region Support Methods

		private ModelingModelDto GetModelInfoDto()
		{
			return new ModelingModelDto
			{
				Name = GenerateRandomString(),
				Description = GenerateRandomString(),
				GroupId = Random.Next(),
				IsOksObjectType = true,
				AlgorithmTypeForCadastralPriceCalculation = KoAlgoritmType.Line,
				Type = KoModelType.Automatic
			};
		}

		#endregion
	}
}