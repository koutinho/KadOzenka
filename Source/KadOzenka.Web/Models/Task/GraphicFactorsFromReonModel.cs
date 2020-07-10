using System.Collections.Generic;

namespace KadOzenka.Web.Models.Task
{
    public class GraphicFactorsFromReonModel
    {
        public long TaskId { get; set; }
        public List<long> AttributeIds { get; set; }


        public GraphicFactorsFromReonModel()
        {
            AttributeIds = new List<long>();
        }
    }
}
