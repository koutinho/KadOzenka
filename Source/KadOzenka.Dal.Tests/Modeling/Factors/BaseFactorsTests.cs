using KadOzenka.Dal.Modeling.Dto.Factors;
using KadOzenka.Dal.Tests.Modeling.Models;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.UnitTests.Modeling.Factors
{
	public class BaseFactorsTests : BaseModelTests
	{
		protected ManualModelFactorDto PrepareManualModelFactorForCRUD(MarkType markType, decimal? correctItem,
			decimal? k)
		{
			var factor = new ManualFactorDtoBuilder().Type(markType).CorrectItem(correctItem).K(k).Build();
			ModelFactorsRepository.Setup(x =>
					x.IsTheSameAttributeExists(factor.Id, factor.FactorId.Value, factor.GeneralModelId.Value,
						factor.Type))
				.Returns(false);

			return factor;
		}
	}
}
