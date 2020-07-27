using DocumentFormat.OpenXml.Office2010.Excel;

namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class YearDto
	{
		public int Id { get; set; }
		
		/// <summary>
		/// Год постройки 
		/// </summary>
		public int? Year { get; set; }
	}
}