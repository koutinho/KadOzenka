using System;
using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Dto.ExpressScore;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IMarketObjectsForMapService : IMarketObjectBaseService
	{
		QSQuery<OMCoreObject> GetBaseQuery();
	}
}