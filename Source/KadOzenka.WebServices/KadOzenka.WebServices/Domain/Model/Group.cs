using System.ComponentModel.DataAnnotations.Schema;

namespace KadOzenka.WebServices.Domain.Model
{
	[Table("ko_group")]
	public class Group
	{
		[Column("id")]
		public long Id { get; set; }

		[Column("parent_id")]
		public long ParentId { get; set; }

		[Column("number")]
		public string Number { get; set; }
	}
}