using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KadOzenka.WebServices.Domain.Model
{
	[Table("common_export_by_templates")]
	public class ExportTemplate
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("user_id")]
		public int UserId { get; set; }

		[Column("status")]
		public int Status { get; set; }

		[Column("date_created")]
		public DateTime DateCreate { get; set; }

		[Column("template_file_name")]
		public string TemplateFileName { get; set; }

		[Column("columns_mapping")]
		public string ColumnsMapping { get; set; }

		[Column("error_id")]
		public int? ErrorId { get; set; }

		[Column("date_started")]
		public DateTime? DateStarted { get; set; }

		[Column("date_finished")]
		public DateTime? DateFinished { get; set; }

		[Column("result_message")]
		public string ResultMessage { get; set; }

		[Column("main_register_id")]
		public int MainRegisterId { get; set; }

		[Column("register_view_id")]
		public string RegisterViewId { get; set; }

	}
}