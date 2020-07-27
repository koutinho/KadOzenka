using System;
using KadOzenka.Dal.GbuObject;

namespace KadOzenka.Web.Models.GbuObject.ObjectAttributes
{
    public class AttributeHistoryRecordDto
    {
        public string Value { get; set; }
        public DateTime ActualDate { get; set; }
        public string Document { get; set; }
        public string UserFullname { get; set; }

        public AttributeHistoryRecordDto(GbuObjectAttribute attributeValue)
        {
            Value = attributeValue.GetValueInString();
            ActualDate = attributeValue.S;
            UserFullname = attributeValue.UserFullname;
            Document = attributeValue.GetDocument();
        }
    }
}
