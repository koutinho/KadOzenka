using System.Collections.Generic;
using MarketPlaceBusiness.Dto.ExpressScore;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IMarketObjectsForExpressScoreService : IMarketObjectBaseService
	{
		List<AnalogDto> GetAnalogsByIds(List<int> ids);
		long? GetAnalogId(string cadastralNumber);
	}
}