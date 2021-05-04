using Core.Register.QuerySubsystem;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class BaseService
	{
		public static readonly long RegisterId = OMCoreObject.GetRegisterId();
		public static readonly QSColumn PrimaryKeyColumn = OMCoreObject.GetColumn(x => x.Id);
	}
}
