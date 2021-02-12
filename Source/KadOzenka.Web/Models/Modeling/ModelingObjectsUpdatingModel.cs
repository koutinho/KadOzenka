using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace KadOzenka.Web.Models.Modeling
{
    public class ModelingObjectsUpdatingModel
    {
        public bool IsBackgroundDownload { get; set; }
        public IFormFile File { get; set; }
        public List<DataColumnDto> Columns { get; set; }
    }


    public class DataColumnDto
    {
	    public string ColumnName { get; set; }
	    public int ColumnIndex { get; set; }
	    public long AttributeId { get; set; }
	    public bool IsKey { get; set; }
    }
}
