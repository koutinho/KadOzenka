using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.GbuObject;

namespace KadOzenka.Web.Models.GbuObject.ObjectAttributes
{
    public class EditAttributeDto
    {
        public long AttributeId { get; set; }
        public long ObjectId { get; set; }
        public string Name { get; set; }
        public bool IsString { get; set; }
        public bool IsDate { get; set; }
        public bool IsBool { get; set; }
        public bool IsNumber { get; set; }
        public string StringValue { get; set; }
        public decimal? NumValue { get; set; }
        public DateTime? DateValue { get; set; }
        public string BoolValue { get; set; }
        public bool NotEditable { get; set; }

        public DateTime? AttributeSetDate { get; set; }

        public EditAttributeDto()
        {

        }
        public EditAttributeDto(RegisterAttribute registerAttribute, long objectId)
        {
            AttributeSetDate = DateTime.Today;
            AttributeId = registerAttribute.Id;
            ObjectId = objectId;
            Name = registerAttribute.Name;

            IsString = registerAttribute.Type == RegisterAttributeType.STRING;
            IsDate = registerAttribute.Type == RegisterAttributeType.DATE;
            IsNumber = registerAttribute.Type == RegisterAttributeType.INTEGER ||
                       registerAttribute.Type == RegisterAttributeType.DECIMAL;
            IsBool = registerAttribute.Type == RegisterAttributeType.BOOLEAN;
        }

        public GbuObjectAttribute GetGbuObjectAttribute()
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = AttributeId,
                ObjectId = ObjectId,
                S = AttributeSetDate ?? DateTime.Today,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = AttributeSetDate ?? DateTime.Today
            };

            switch (RegisterCache.GetAttributeData((int)AttributeId).Type)
            {
                case RegisterAttributeType.STRING:
                    attributeValue.StringValue = StringValue;
                    break;
                case RegisterAttributeType.INTEGER:
                case RegisterAttributeType.DECIMAL:
                    attributeValue.NumValue = NumValue;
                    break;
                case RegisterAttributeType.DATE:
                    attributeValue.DtValue = DateValue;
                    break;
                case RegisterAttributeType.BOOLEAN:
                    attributeValue.NumValue = (BoolValue.ParseToBoolean() ? 1 : 0);
                    break;
            }

            return attributeValue;
        }
    }
}
