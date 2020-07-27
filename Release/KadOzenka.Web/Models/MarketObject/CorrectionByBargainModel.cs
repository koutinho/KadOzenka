using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.Correction;
using KadOzenka.Dal.Correction.Requests;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.MarketObject
{
    public class CorrectionByBargainModel: IValidatableObject
    {
        [Required(ErrorMessage = "Выберете Географический охват")]
        public CoefficientCoverageAreaType? CoverageAreaType { get; set; }
        public bool UseAllSegments { get; set; } = true;
        public List<string> MarketSegmentFilter { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public CorrectionByBargainRequest ToCorrectionByBargainRequest()
        {
            var request = new CorrectionByBargainRequest();
            if (UseAllSegments)
            {
                var segments = System.Enum.GetValues(typeof(MarketSegment)).Cast<MarketSegment>();
                request.MarketSegments = new HashSet<MarketSegment>(segments);
            }
            else
            {
                var segments = new List<MarketSegment>();
                if (MarketSegmentFilter.Count > 0)
                    foreach (var segmentString in MarketSegmentFilter)
                    {
                        segments.Add((MarketSegment)typeof(MarketSegment).GetEnumByCode(segmentString));
                    }
                request.MarketSegments = new HashSet<MarketSegment>(segments);
            }
            request.CoverageAreaType = CoverageAreaType.Value;
            request.DateFrom = DateFrom;
            request.DateTo = DateTo;

            return request;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!UseAllSegments)
            {
                if (MarketSegmentFilter?.Count == null || MarketSegmentFilter?.Count == 0)
                {
                    yield return
                        new ValidationResult(errorMessage: "Список сегментов не может быть пустым",
                            memberNames: new[] {nameof(MarketSegmentFilter) });
                }
            }

            if(DateFrom.HasValue && DateTo.HasValue && DateTo < DateFrom)
            {
                yield return
                    new ValidationResult(errorMessage: "Некорректный временной интервал: 'Дата с' не может быть больше 'Даты по'",
                        memberNames: new[] { nameof(DateFrom), nameof(DateTo) });
            }
        }
    }
}
