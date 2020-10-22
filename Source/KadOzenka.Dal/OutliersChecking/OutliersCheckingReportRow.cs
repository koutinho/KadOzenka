using ObjectModel.Directory;

namespace KadOzenka.Dal.OutliersChecking
{
	public class OutliersCheckingReportRow
	{
		public OutliersCheckingReportRow(string location)
		{
			LocationName = location;
		}

		public long MarketObjectId { get; private set;}
		public string Kn { get; private set; }
		public string LocationName { get; private set; }
		public decimal PricePerMeter { get; private set; }
		public ProcessStep PreviousStatus { get; private set; }
		public decimal LowerMedian { get; private set; }
		public decimal UpperMedian { get; private set; }
		public decimal? MinDeltaCoef { get; private set; }
		public decimal? MaxDeltaCoef { get; private set; }
		public decimal LowerLimit { get; private set; }
		public decimal UpperLimit { get; private set; }
		public bool IsExcluded { get; private set; }

		public void SetMedians(decimal lowerMedian, decimal upperMedian)
		{
			LowerMedian = lowerMedian;
			UpperMedian = upperMedian;
		}

		public void SetDeltaCoefs(decimal? minDeltaCoef, decimal? maxDeltaCoef)
		{
			MinDeltaCoef = minDeltaCoef;
			MaxDeltaCoef = maxDeltaCoef;
		}

		public void SetLimits(decimal lowerLimit, decimal upperLimit)
		{
			LowerLimit = lowerLimit;
			UpperLimit = upperLimit;
		}

		public void SetMarketInfo(long marketObjectId, string kn, decimal pricePerMeter, ProcessStep objectStatus)
		{
			MarketObjectId = marketObjectId;
			Kn = kn;
			PricePerMeter = pricePerMeter;
			PreviousStatus = objectStatus;
		}

		public void SetExcludedStatus(bool isExcluded)
		{
			IsExcluded = isExcluded;
		}
	}
}