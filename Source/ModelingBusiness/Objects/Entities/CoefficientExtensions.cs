using System.Collections.Generic;
using Newtonsoft.Json;

namespace ModelingBusiness.Objects.Entities
{
	public static class CoefficientExtensions
	{
		public static string SerializeCoefficient(this List<CoefficientForObject> coefficients)
		{
			return JsonConvert.SerializeObject(coefficients);
		}
	}
}