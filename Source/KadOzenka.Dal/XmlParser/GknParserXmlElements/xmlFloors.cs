namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Количество этажей (в том числе подземных)
	/// </summary>
	public class xmlFloors
	{
		/// <summary>
		/// Количество этажей
		/// </summary>
		public string Floors { get; set; }
		/// <summary>
		/// В том числе подземных этажей
		/// </summary>
		public string Underground_Floors { get; set; }
	}
}