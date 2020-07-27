using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KadOzenka.WebServices.Domain.Model
{
	[Table("ko_result_send_journal")]
	public class ReonJournal
	{
		[Column("id")]
		public long Id { get; set; }

		[Column("guid")]
		public string Guid { get; set; }

		[Column("task_id")]
		public long TaskId { get; set; }

		[Column("create_date")]
		public DateTime CreateDate { get; set; }

		[Column("send_date")]
		public DateTime? SendDate { get; set; }

		[Column("confirm_date")]
		public DateTime? ConfirmDate { get; set; }

		[Column("result_export_id")]
		public long ResultReportId { get; set; }
	}
}