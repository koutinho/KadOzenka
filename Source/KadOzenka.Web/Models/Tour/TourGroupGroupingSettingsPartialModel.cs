using KadOzenka.Dal.Models.Filters;

namespace KadOzenka.Web.Models.Tour
{
    public class TourGroupGroupingSettingsPartialModel
    {
        public int Index { get; set; }

        public long? DictionaryId { get; set; }

        public string DictionaryValue { get; set; }

        public Filters GroupFilters { get; set; }

        public long? KoAttributes { get; set; }

        public TourGroupGroupingSettingsPartialModel()
        {
            GroupFilters = new Filters();
            KoAttributes = new long?();
        }
    }
}