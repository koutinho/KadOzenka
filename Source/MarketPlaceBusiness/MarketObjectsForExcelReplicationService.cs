using System.Collections.Generic;
using System.Linq;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectsForExcelReplicationService : IMarketObjectsForExcelReplicationService
	{
		public List<OMCoreObject> GetRosreestrObjectAddresses()
		{
			return OMCoreObject.Where(x => x.Market_Code == ObjectModel.Directory.MarketTypes.Rosreestr)
				.Select(x => new {x.Address}).OrderBy(x => x.Address).Execute().ToList();
		}

		public List<OMCoreObject> GetObjectsToSetCoordinatesByYandex(int objectsCount)
		{
			return OMCoreObject.Where(x =>
					x.ProcessType_Code == ObjectModel.Directory.ProcessStep.AddressStep &&
					x.Market_Code == ObjectModel.Directory.MarketTypes.Rosreestr)
				.Select(x => new {x.ProcessType_Code, x.Address, x.Lng, x.Lat, x.ExclusionStatus_Code}).Execute()
				.Take(objectsCount).ToList();
		}

		public OMCoreObject GetObjectAddressAndCoordinates(long objectId)
		{
			return OMCoreObject.Where(x => x.Id == objectId)
				.Select(x => new
				{
					x.Address, 
					x.Lng, 
					x.Lat
				}).ExecuteFirstOrDefault();
		}
	}
}
