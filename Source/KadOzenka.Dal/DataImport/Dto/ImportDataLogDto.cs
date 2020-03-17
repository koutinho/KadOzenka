using System;

namespace KadOzenka.Dal.DataImport.Dto
{
    public class ImportDataLogDto
    {
        public long Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Author { get; set; }
        public string FileName { get; set; }
        public long? NumberOfImportedObjects { get; set; }
        public long? TotalNumberOfObjects { get; set; }
    }
}
