using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadOzenka.Web.Models.Tour
{
    public class DictionarySelectListItem : SelectListItem
    {
        public int Type { get; set; }

        public DictionarySelectListItem()
        {
        }

        public DictionarySelectListItem(string text, string value): base(text, value){}
    }
}