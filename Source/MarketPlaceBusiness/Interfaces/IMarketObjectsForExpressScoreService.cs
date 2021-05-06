using System;
using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Dto.ExpressScore;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IMarketObjectsForExpressScoreService : IAMarketObjectBaseService
	{
		List<AnalogDto> GetAnalogsByIds(List<int> ids);
		long? GetAnalogId(string cadastralNumber);
		List<OMCoreObject> GetObjectsInfoForCard(List<long?> resultObjectIds);
		List<OMCoreObject> GetNearestObjects(DateTime actualDate, QSCondition conditionAnalog);
	}
}