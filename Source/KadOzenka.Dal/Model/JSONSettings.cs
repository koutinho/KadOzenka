using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OuterMarketParser.Model
{
    class JSONSettings
    {
        public class CIAN
        {
            public string Template { get; set; }
            public int DealId { get; set; }
            public List<int> RegionIds { get; set; }
            public string DateTimeTemplate { get; set; }
        }

        public string Login { get; set; }
        public string Token { get; set; }
        public int MinutesDelta { get; set; }
        public CIAN Cian { get; set; }

        public string ToString(string startDate, string endDate) =>
            string.Format(
                Cian.Template,
                Login,
                Token,
                Cian.DealId,
                string.Join("&", Cian.RegionIds.Select(x => $"region_id={x}")),
                startDate,
                endDate
            );
    }
}
