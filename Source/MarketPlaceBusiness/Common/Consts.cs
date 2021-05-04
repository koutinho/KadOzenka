using Core.Register.QuerySubsystem;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Common
{
	public static class Consts
	{
		public static readonly int RegisterId = OMCoreObject.GetRegisterId();

		//TODO KOMO-33: если будет много колонок, подумать над отдельным методом
		public static readonly QSColumn PrimaryKeyColumn = OMCoreObject.GetColumn(x => x.Id);
		public static readonly long PriceAttributeId = OMCoreObject.GetColumnAttributeId(x => x.Price);
	}
}
