using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Web.Models.Modeling
{
	public abstract class GeneralFactorModel
	{
		public long Id { get; set; }
		public bool IsNewFactor => Id == -1;
		public long? ModelId { get; set; }
		public abstract bool IsAutomatic { get; }

		[Display(Name = "Фактор")]
		public long FactorId { get; set; }

		[Display(Name = "Тип метки")]
		public MarkType MarkType { get; set; }

		[Display(Name = "Корректирующее слагаемое")]
		public decimal? CorrectItem { get; set; }

		[Display(Name = "K=[A+M]/2")]
		public decimal? K { get; set; }

		[Display(Name = "Словарь")]
		public string DictionaryName { get; set; }
		public long? DictionaryId { get; set; }

		[Display(Name = "Поправка")]
		public decimal Correction { get; set; }
	}
}