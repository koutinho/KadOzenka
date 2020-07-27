namespace KadOzenka.Dal.Correction
{
    public static class Consts
    {
        public static readonly string DateFormatForDateCorrection = "MMMM yyyy";
        public static readonly int PrecisionForPrice = 2;
        public static readonly int PrecisionForCoefficients = 4;
        public static readonly decimal LowerLimitForDateCorrectionCoefficients = 0.7m;
        public static readonly decimal UpperLimitForDateCorrectionCoefficients = 1.3m;
        public static readonly decimal LowerLimitForBargainCorrectionCoefficients = 0.6m;
        public static readonly decimal UpperLimitForBargainCorrectionCoefficients = 1.0m;
    }
}
