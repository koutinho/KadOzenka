using System;

namespace KadOzenka.Dal.Tasks.Dto
{
    public class DocumentDto
    {
        public long Id { get; set; }

        public DateTime? CreationDate { get; set; }

        public string RegNumber { get; set; }

        public string Description { get; set; }

        public DateTime? ApproveDate { get; set; }
    }
}
