using KadOzenka.Common.Tests;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Model.Entities;
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
			ModelService.Setup(x => x.GetModelById(modelId)).Returns(new ModelDto { Type = KoModelType.Automatic });
		}
	}
}
