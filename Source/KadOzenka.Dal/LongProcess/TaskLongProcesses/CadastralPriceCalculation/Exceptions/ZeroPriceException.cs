using System.Collections.Generic;
using KadOzenka.Dal.LongProcess._Common;
using KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Entities;
using org.mariuszgromada.math.mxparser;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Exceptions
{
	public class ZeroPriceException : CalculationException
	{
		public ZeroPriceException(Expression expression, List<FactorInfo> factors)
			: base(expression, factors, Messages.ZeroCadastralPrice)
		{

		}
	}
}
