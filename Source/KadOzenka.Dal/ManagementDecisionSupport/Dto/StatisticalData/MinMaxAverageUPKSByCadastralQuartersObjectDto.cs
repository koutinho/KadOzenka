namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class MinMaxAverageUPKSByCadastralQuartersObjectDto : UpksCalcDto
	{
		public string CadastralRegionNumber { get; set; }
		public string CadastralQuater { get; set; }
		public string PropertyType { get; set; }
	}
}
