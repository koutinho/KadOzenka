using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.ExpressScore.Dto;
using ObjectModel.Core.Register;

namespace KadOzenka.Web.Models.ExpressScore
{
    public class AttributePureModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class TargetObjectModel
    {
        public long UnitId { get; set; }

        public List<AttributePureModel> Attributes { get; set; }


        public static TargetObjectModel ToModel(TargetObjectDto targetObjectDto)
        {
            var emptyAttributes = targetObjectDto.Attributes.Where(x => string.IsNullOrEmpty(x.Value)).ToList();

            var attributeIds = emptyAttributes.Select(x => (long)x.Id).Distinct().ToList();
            var omAttributes = attributeIds.Count > 0 
                ? OMAttribute.Where(x => attributeIds.Contains(x.Id)).Select(x => x.Name).Execute().ToList() 
                : null;

            return new TargetObjectModel
            {
                UnitId = targetObjectDto.UnitId,
                Attributes = emptyAttributes.Select(x =>
                {
                    var attributeName = omAttributes?.FirstOrDefault(a => a.Id == x.Id)?.Name;
                    return new AttributePureModel
                    {
                        Id = x.Id,
                        Name = attributeName,
                        Value = x.Value
                    };
                }).ToList()
            };
        }
    }
}
