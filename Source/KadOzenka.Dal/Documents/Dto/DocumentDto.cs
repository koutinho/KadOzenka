using System;

namespace KadOzenka.Dal.Documents.Dto
{
    public class DocumentDto
    {
        public long? Id { get; set; }
        public string Description { get; set; }
        public string RegNumber { get; set; }
        public DateTime? ApproveDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
