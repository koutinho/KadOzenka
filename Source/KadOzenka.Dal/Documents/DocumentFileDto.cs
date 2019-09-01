using System.IO;

namespace CIPJS.DAL.Documents
{
    public class DocumentFileDto
    {
        public string Name { get; set; }

        public Stream StreamFile { get; set; }
    }
}
