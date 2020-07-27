using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;

namespace KadOzenka.Web.Models.GbuObject.ObjectAttributes
{
    public class AttributeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsString { get; set; }
        public bool IsDate { get; set; }
        public bool IsNumber { get; set; }
        public string StringValue { get; set; }
        public decimal? NumValue { get; set; }
        public DateTime? DateValue { get; set; }
        public string AttributeHistoryUrl { get; set; }

        public AttributeDto(GbuObjectAttribute gbuObjectAttribute, long objectId)
        {
            Id = gbuObjectAttribute.AttributeId;
            Name = gbuObjectAttribute.GetAttributeName();
            IsString = gbuObjectAttribute.AttributeData.Type == RegisterAttributeType.STRING;
            IsDate = gbuObjectAttribute.AttributeData.Type == RegisterAttributeType.DATE;
            IsNumber = gbuObjectAttribute.AttributeData.Type == RegisterAttributeType.INTEGER ||
                       gbuObjectAttribute.AttributeData.Type == RegisterAttributeType.DECIMAL;
            StringValue = gbuObjectAttribute.StringValue;
            NumValue = gbuObjectAttribute.NumValue;
            DateValue = gbuObjectAttribute.DtValue;
            AttributeHistoryUrl =
                $"/GbuObject/GetAttributeHistory?objectId={objectId}&registerId={gbuObjectAttribute.RegisterData.Id}&attrId={Id}"
                    .ResolveClientUrl();
        }
    }
}
