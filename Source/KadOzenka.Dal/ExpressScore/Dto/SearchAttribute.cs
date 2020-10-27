namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class SearchAttribute
	{
		/// <summary>
		/// Ид атрибута в котором ищем
		/// </summary>
		public long IdAttribute { get; set; }
		/// <summary>
		/// Значение полученной со страницы расчетов
		/// </summary>
		public string Value { get; set; }
		/// <summary>
		/// Ид справочника
		/// </summary>
		public long ReferenceId { get; set; }
	}
}