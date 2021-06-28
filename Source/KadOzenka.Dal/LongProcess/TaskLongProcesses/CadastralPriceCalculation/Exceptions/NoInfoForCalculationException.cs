using System;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Exceptions
{
	public class NoInfoForCalculationException : Exception
	{
		public NoInfoForCalculationException(string message) : base(message)
		{
			
		}
	}
}
