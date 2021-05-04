using Core.Register.QuerySubsystem;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class BaseService
	{
		public static readonly long RegisterId = OMCoreObject.GetRegisterId();
		//TODO KOMO-33: если будет много колонок, подумать над отдельным методом
		public static readonly QSColumn PrimaryKeyColumn = OMCoreObject.GetColumn(x => x.Id);
		public static readonly long PriceAttributeId = OMCoreObject.GetColumnAttributeId(x => x.Price);
	}
}
