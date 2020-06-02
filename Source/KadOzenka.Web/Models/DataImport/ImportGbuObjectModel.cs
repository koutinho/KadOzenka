﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using KadOzenka.Web.Models.DataUpload;

namespace KadOzenka.Web.Models.DataImport
{
    public class ImportGbuObjectModel
    {
        public bool IsBackgroundDownload { get; set; }
        public int MainRegisterId { get; set; }
        public string RegisterViewId { get; set; }
        public IFormFile File { get; set; }
        public List<DataColumnDto> Columns { get; set; }
    }
}
