using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectsForMapService : AMarketObjectBaseService, IMarketObjectsForMapService
	{
		public QSQuery<OMCoreObject> GetBaseQuery()
		{
			return OMCoreObject.Where(x =>
				(x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed) &&
				x.Lng != null && x.Lat != null && (x.LastDateUpdate != null || x.Market_Code == MarketTypes.Rosreestr));
		}
	}
}
