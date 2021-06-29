using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;
using ObjectModel.Ko;

namespace KadOzenka.Web.Models.Tour
{
    public class GroupSegmentSettingsModel : IValidatableObject
    {
	    public static string NoSegmentErrorMessage => "Не выбран сегмент";
	    public static string NoTerritoryErrorMessage => "Не выбрана территория";

        public long GroupId { get; set; }

        [Display(Name="Сегмент")]
        public MarketSegment MarketSegment { get; set; }


        public static GroupSegmentSettingsModel FromEntity(OMGroupToMarketSegmentRelation entity)
        {
            return new GroupSegmentSettingsModel
            {
                MarketSegment = entity.MarketSegment_Code
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
	        if (MarketSegment == MarketSegment.None)
		        yield return new ValidationResult(NoSegmentErrorMessage);
        }
    }
}
