using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KadOzenka.WebServices.Domain.Model
{
	[Table("ko_task")]
	public class Task
	{
		/// <summary>
		/// Identification
		/// </summary>
		[Column("id")]
		public int Id { get; set; }

		/// <summary>
		/// Tour id
		/// </summary>
		[Column("tour_id")]
		public int TourId { get; set; }

		/// <summary>
		/// Date created
		/// </summary>
		[Column("creation_date")]
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Document id
		/// </summary>
		[Column("document_id")]
		public int DocumentId { get; set; }
	}
}