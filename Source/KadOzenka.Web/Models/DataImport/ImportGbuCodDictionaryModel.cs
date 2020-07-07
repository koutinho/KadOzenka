using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using KadOzenka.Web.Models.DataUpload;
using KadOzenka.Web.Models.GbuObject;

namespace KadOzenka.Web.Models.DataImport
{
    public class ImportGbuCodDictionaryModel
    {
        public bool IsBackgroundDownload { get; set; }
        public int MainRegisterId { get; set; }
        public string RegisterViewId { get; set; }
        public IFormFile File { get; set; }
        public List<DataColumnDto> Columns { get; set; }
        public int DictionaryId { get; set; }
    }
}   
