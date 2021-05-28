//using ObjectModel.Directory;
//using System;

//namespace KadOzenka.Dal.ExpressScore.Dto
//{
//	public class CoordinatesDto : IEquatable<CoordinatesDto>
//	{
//		public long? Id { get; set; }
//		public decimal Lat { get; set; }
//		public decimal Lng { get; set; }
//		public string ObjectMiniCard { get; set; }
//		public string ObjectMiniCardContent { get; set; }
//		public string Images { get; set; }
//		public decimal? Price { get; set; }
//		public decimal? PricePerMeter { get; set; }
//		public decimal? Area { get; set; }
//		public string Address { get; set; }
//		public string CadastralNumber { get; set; }
//		public string PropertyMarketSegment { get; set; }
//		public string DealType { get; set; }
//		public MarketTypes Market_Code { get; set; }
//		public PropertyTypesCIPJS PropertyTypesCIPJS_Code { get; set; }

//		public bool Equals(CoordinatesDto other)
//		{
//			if (ReferenceEquals(other, null)) return false;

//			if (ReferenceEquals(this, other)) return true;

//			return Lat.Equals(other.Lat) && Lng.Equals(other.Lng);
//		}

//		public override int GetHashCode()
//		{
//			int hashLate =  Lat.GetHashCode();

//			int hashLng = Lng.GetHashCode();

//			return hashLate ^ hashLng;
//		}
//	}
//}