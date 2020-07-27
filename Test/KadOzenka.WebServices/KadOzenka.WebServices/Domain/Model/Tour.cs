using System.ComponentModel.DataAnnotations.Schema;

namespace KadOzenka.WebServices.Domain.Model
{
	[Table("ko_tour")]
	public class Tour
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("year")]
		public int Year { get; set; }
	}
}