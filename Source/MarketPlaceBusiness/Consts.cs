using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public static class Consts
	{
		public static readonly int RegisterId = OMCoreObject.GetRegisterId();

		//TODO KOMO-33: если будет много колонок, подумать над отдельным методом
		public static readonly QSColumn PrimaryKeyColumn = OMCoreObject.GetColumn(x => x.Id);
		public static readonly QSColumn MarketSegmentCodeColumn = OMCoreObject.GetColumn(x => x.PropertyMarketSegment_Code);
		public static readonly QSColumn AreaColumn = OMCoreObject.GetColumn(x => x.Area);
		public static readonly QSColumn CadastralNumberColumn = OMCoreObject.GetColumn(x => x.CadastralNumber);


		public static readonly long PriceAttributeId = OMCoreObject.GetColumnAttributeId(x => x.Price);
		public static readonly long CadastralNumberAttributeId = OMCoreObject.GetColumnAttributeId(x => x.CadastralNumber);
		public static readonly long AddressAttributeId = OMCoreObject.GetColumnAttributeId(x => x.Address);
		
		
		public static readonly RegisterAttribute PropertyTypesCIPJSAttribute = OMCoreObject.GetAttributeData(x => x.PropertyTypesCIPJS);
		public static readonly RegisterAttribute PropertyMarketSegmentAttribute = OMCoreObject.GetAttributeData(x => x.PropertyMarketSegment);
		public static readonly RegisterAttribute MarketAttribute = OMCoreObject.GetAttributeData(x => x.Market);
		public static readonly RegisterAttribute QualityClassCodeAttribute = OMCoreObject.GetAttributeData(x => x.QualityClass_Code);
		public static readonly RegisterAttribute PriceAttribute = OMCoreObject.GetAttributeData(x => x.Price);
	}
}
