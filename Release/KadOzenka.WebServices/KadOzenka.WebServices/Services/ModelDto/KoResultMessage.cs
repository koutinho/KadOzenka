using System;

namespace KadOzenka.WebServices.Services.ModelDto
{
	public class KoResultMessage
	{
		/// <summary>
		/// unique identification 
		/// </summary>
		public string Guid { get; set; }

		/// <summary>
		/// Date created record into journal
		/// </summary>
		public DateTime CreateDate { get; set; }

		/// <summary>
		/// Year of tour
		/// </summary>
		public int YearTour { get; set; }

		/// <summary>
		/// Registration number 
		/// </summary>
		public string RegNumber { get; set; }

		/// <summary>
		/// Date created task
		/// </summary>
		public DateTime? DateCreatedTask { get; set; }
	}
}