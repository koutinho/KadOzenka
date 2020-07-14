using System.Collections.Generic;
using KadOzenka.Web.Models.DataUpload;
using KadOzenka.Web.Models.GbuObject;
using Microsoft.AspNetCore.Http;

namespace KadOzenka.Web.Models.DataImportByTemplate
{
    public class ImportGbuObjectModel
    {
        public bool IsBackgroundDownload { get; set; }
        public int MainRegisterId { get; set; }
        public string RegisterViewId { get; set; }
        public IFormFile File { get; set; }
        public List<DataColumnDto> Columns { get; set; }
        public PartialDocumentViewModel Document { get; set; }
    }
}
