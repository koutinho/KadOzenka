using System.Collections.Generic;

namespace KadOzenka.Web.Models.Modeling
{
    public class CorrelationModel
    {
        public string QsQueryXmlStr { get; set; }
        public List<long> AttributeIds { get; set; }


        public CorrelationModel()
        {
            AttributeIds = new List<long>();
        }
    }
}
