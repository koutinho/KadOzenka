namespace KadOzenka.Dal.DataComparing.Files.CadastralCostFDFile.UnitInfo
{
	public class FactorValue
	{
		public string FactorName { get; protected set; }
		public bool IsSignMarket { get; protected set; }
		public string Value { get; protected set; }
		public string UsedValue { get; protected set; }

		public FactorValue(string name, bool isSignMarket, string factorValue, string usedValue)
		{
			FactorName = name;
			IsSignMarket = isSignMarket;
			Value = factorValue;
			UsedValue = usedValue;
		}
	}
}
