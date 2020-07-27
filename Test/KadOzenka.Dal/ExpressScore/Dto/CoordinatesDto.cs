using System;

namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class CoordinatesDto : IEquatable<CoordinatesDto>
	{
		public long? Id { get; set; }
		public decimal Lat { get; set; }
		public decimal Lng { get; set; }
		public string ObjectMiniCard { get; set; }

		public bool Equals(CoordinatesDto other)
		{
			if (ReferenceEquals(other, null)) return false;

			if (ReferenceEquals(this, other)) return true;

			return Lat.Equals(other.Lat) && Lng.Equals(other.Lng);
		}

		public override int GetHashCode()
		{
			int hashLate =  Lat.GetHashCode();

			int hashLng = Lng.GetHashCode();

			return hashLate ^ hashLng;
		}
	}
}