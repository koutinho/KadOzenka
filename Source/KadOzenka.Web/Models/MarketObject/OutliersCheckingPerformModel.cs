using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.MarketObjects.Settings;
using KadOzenka.Dal.OutliersChecking;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.MarketObject
{
	public class OutliersCheckingPerformModel : IValidatableObject
	{
		public int? Segment { get; set; }
		public bool AllPropertyTypes { get; set; }
		public List<int> PropertyTypes { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (!AllPropertyTypes && PropertyTypes.IsEmpty())
			{
				yield return
					new ValidationResult("Не заданы виды объектов недвижимости");
			}
		}

		public OutliersCheckingProcessSettings ToSettings()
		{
			MarketSegment? segment = null;
			if (Segment.HasValue)
			{
				segment = (MarketSegment) Segment;
			}

			var propertyTypes = new List<ObjectPropertyTypeDivision>();
			if (AllPropertyTypes)
			{
				propertyTypes = System.Enum.GetValues(typeof(ObjectPropertyTypeDivision)).Cast<ObjectPropertyTypeDivision>().ToList();
			}
			else
			{
				foreach (var propertyType in PropertyTypes)
				{
					propertyTypes.Add((ObjectPropertyTypeDivision) propertyType);
				}
			}

			return new OutliersCheckingProcessSettings
			{
				Segment = segment,
				PropertyTypes = propertyTypes
			};
		}
	}
}
