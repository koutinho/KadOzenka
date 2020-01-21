using ObjectModel.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadOzenka.Web.Models.DataUpload
{
	public class UploadGknDto
	{
		public long? Tour { get; set; }
		public long? TourId { get; set; }
		public long DocNumber { get; set; }
		public DateTime? DocDate { get; set; }
		public KoNoteType? NoteType { get; set; }
	}
}
