using System.Collections.Generic;

namespace CIPJS.DAL.InsuranceObjectLoader
{
	public class FlagInsurCondition
	{
		public string Name { get; set; }

		public int? AttributeId { get; set; }

		public string Expression { get; set; }

		public bool AllowedValuesByCode { get; set; }

		public List<string> AllowedValues { get; set; }
	}

    public class ConfigCalculateFlagInsur
    {
		public List<FlagInsurCondition> Conditions { get; set; }
	}
}
