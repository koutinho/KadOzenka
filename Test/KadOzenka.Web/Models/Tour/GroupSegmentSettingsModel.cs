using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;
using ObjectModel.Ko;

namespace KadOzenka.Web.Models.Tour
{
    public class GroupSegmentSettingsModel : IValidatableObject
    {
        public long GroupId { get; set; }

        [Display(Name="Сегмент")]
        public MarketSegment MarketSegment { get; set; }
        [Display(Name = "Территория")]
        public TerritoryType TerritoryType { get; set; }


        public static GroupSegmentSettingsModel FromEntity(OMGroupToMarketSegmentRelation entity)
        {
            return new GroupSegmentSettingsModel
            {
                MarketSegment = entity.MarketSegment_Code,
                TerritoryType = entity.TerritoryType_Code
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if(MarketSegment == MarketSegment.None)
                errors.Add(new ValidationResult("Не выбран сегмент"));
            if (TerritoryType == TerritoryType.None)
                errors.Add(new ValidationResult("Не выбрана территория"));

            return errors;
        }
    }
}
