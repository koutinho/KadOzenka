using System.ComponentModel.DataAnnotations.Schema;

namespace KadOzenka.WebServices.Domain.Model
{
	[Table("core_td_instance")]
	public class TdInstance
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("regnumber")]
		public string RegNumber { get; set; }

	}
}