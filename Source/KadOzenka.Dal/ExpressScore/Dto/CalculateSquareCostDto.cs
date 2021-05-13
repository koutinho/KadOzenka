//using System.Collections.Generic;
//using KadOzenka.Dal.Enum;
//using MarketPlaceBusiness.Dto.ExpressScore;
//using ObjectModel.Directory;
//using ObjectModel.Directory.ES;

//namespace KadOzenka.Dal.ExpressScore.Dto
//{
//	public class CalculateSquareCostDto
//	{
//		public List<AnalogDto> Analogs { get; set; }
//		/// <summary>
//		/// ИД объекта оценки полученное из Ко части
//		/// </summary>
//		public int TargetObjectId { get; set; }
//		public MarketSegment MarketSegment { get; set; }
//		public DealTypeShort DealTypeShort { get; set; }

//		/// <summary>
//		/// Ид объекта оценки из аналогов при условии что объект оценки там был найден
//		/// </summary>
//		public long? TargetMarketObjectId { get; set; }
//		public ScenarioType? ScenarioType { get; set; }
//		public string Kn { get; set; }
//		public List<SearchAttribute> ComplexCalculateParameters { get; set; }
//	}
//}