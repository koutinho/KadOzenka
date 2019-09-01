using System.Collections.Generic;
using System.Xml.Serialization;

namespace CIPJS.DAL.Mfc.Upload
{
    [XmlType("MapConfig")]
    public class MapConfig
    {
        [XmlElement("Map")]
        public List<Map> MapList { get; set; }
    }

    [XmlType("Map")]
    public class Map
    {
        [XmlAttribute("Src")]
        public string Src { get; set; }

        [XmlAttribute("Dest")]
        public string Dest { get; set; }
        
        [XmlAttribute("ReferenceTable")]
        public string ReferenceTable { get; set; }

        [XmlAttribute("ReferenceId")]
        public string ReferenceId { get; set; }

        [XmlAttribute("ReferenceColumn")]
        public string ReferenceColumn { get; set; }
    }
}
