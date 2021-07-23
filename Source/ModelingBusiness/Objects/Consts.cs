namespace ModelingBusiness.Objects
{
	public static class Consts
	{
		public static string PrefixForFactor => "_";
		public static string PrefixForValueInNormalizedColumn => $"{PrefixForFactor}1";
		public static string PrefixForCoefficientInNormalizedColumn => $"{PrefixForFactor}2";
	}
}
