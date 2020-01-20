using System;

namespace KadOzenka.Dal.Tasks.Dto
{
    public class TaskDto
    {
        public long Id { get; set; }

        public DateTime? CreationDate { get; set; }

        public TourDto Tour { get; set; }

        public string NoteType { get; set; }

        public string Status { get; set; }

        public DocumentDto ResponseDocument { get; set; }

        public DocumentDto IncomingDocument { get; set; }

        public TaskDto()
        {
            Tour = new TourDto();
            ResponseDocument = new DocumentDto();
            IncomingDocument = new DocumentDto();
        }
    }
}
