using System.ComponentModel;
using System.Xml.Serialization;

namespace DebugApplication.Report
{
    public class Parameter
    {
        [XmlAttribute]
        [DefaultValue("")]
        public string Key { get; set; }

        [XmlAttribute]
        [DefaultValue("")]
        public string Value { get; set; }
    }
}
