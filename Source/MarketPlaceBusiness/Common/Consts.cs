using Core.Register.QuerySubsystem;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Common
{
	public static class Consts
	{
		public static readonly int RegisterId = OMCoreObject.GetRegisterId();

		//TODO KOMO-33: если будет много колонок, подумать над отдельным методом
		public static readonly QSColumn PrimaryKeyColumn = OMCoreObject.GetColumn(x => x.Id);
		public static readonly QSColumn ProcessTypeCodeColumn = OMCoreObject.GetColumn(x => x.ProcessType_Code);
		public static readonly QSColumn MarketSegmentCodeColumn = OMCoreObject.GetColumn(x => x.PropertyMarketSegment_Code);
		public static readonly QSColumn AreaColumn = OMCoreObject.GetColumn(x => x.Area);
		public static readonly QSColumn DealTypeCodeColumn = OMCoreObject.GetColumn(x => x.DealType_Code);
		public static readonly QSColumn LngColumn = OMCoreObject.GetColumn(x => x.Lng);
		public static readonly QSColumn LatColumn = OMCoreObject.GetColumn(x => x.Lat);
		public static readonly QSColumn ParserTimeColumn = OMCoreObject.GetColumn(x => x.ParserTime);
		public static readonly QSColumn LastDateUpdateColumn = OMCoreObject.GetColumn(x => x.LastDateUpdate);
		public static readonly QSColumn CadastralNumberColumn = OMCoreObject.GetColumn(x => x.CadastralNumber);

		public static readonly long PriceAttributeId = OMCoreObject.GetColumnAttributeId(x => x.Price);
		public static readonly long CadastralNumberAttributeId = OMCoreObject.GetColumnAttributeId(x => x.CadastralNumber);
		public static readonly long AddressAttributeId = OMCoreObject.GetColumnAttributeId(x => x.Address);
	}
}
