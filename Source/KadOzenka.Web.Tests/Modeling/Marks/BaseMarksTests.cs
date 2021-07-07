using KadOzenka.Common.Tests;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.UnitTests.Modeling.Marks
{
	public class BaseMarksTests : BaseModelingTests
	{
		protected void MockFactorForAutomaticModel(long dictionaryId)
		{
			var modelId = RandomGenerator.GenerateRandomId();
			ModelFactorsRepository.Setup(x => x.GetFactorByDictionary(dictionaryId)).Returns(new OMModelFactor { ModelId = modelId });
			ModelingService.Setup(x => x.GetModelById(modelId)).Returns(new ModelingModelDto { Type = KoModelType.Automatic });
		}
	}
}
