using System.Collections.Generic;
using KadOzenka.Web.Models.DataUpload;
using Microsoft.AspNetCore.Http;

namespace KadOzenka.Web.Models.DataImportByTemplate
{
    public class ImportGbuCodDictionaryModel
    {
        public bool IsBackgroundDownload { get; set; }
        public int MainRegisterId { get; set; }
        public IFormFile File { get; set; }
        public List<DataColumnDto> Columns { get; set; }
        public int DictionaryId { get; set; }
    }
}   
