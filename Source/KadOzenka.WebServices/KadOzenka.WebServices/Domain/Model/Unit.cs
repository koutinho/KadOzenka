using System.ComponentModel.DataAnnotations.Schema;

namespace KadOzenka.WebServices.Domain.Model
{
	[Table("ko_unit")]
	public class Unit
	{
		[Column("id")]
		public long Id { get; set; }

		[Column("group_id")]
		public long? GroupId { get; set; }

		[Column("tour_id")]
		public long? TourId { get; set; }

		[Column("task_id")]
		public long? TaskId { get; set; }

		[Column("cadastral_number")]
		public string CadastralNumber { get; set; }

		[Column("upks")]
		public decimal? Upks { get; set; }

		[Column("cadastral_cost")]
		public decimal? CadastralCost { get; set; }
	}
}