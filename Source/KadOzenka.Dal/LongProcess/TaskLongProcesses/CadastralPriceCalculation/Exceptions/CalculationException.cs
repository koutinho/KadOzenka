using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.LongProcess._Common;
using KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Entities;
using org.mariuszgromada.math.mxparser;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Exceptions
{
	public class CalculationException : Exception
	{
		public CalculationException(Expression expression, List<FactorInfo> factors)
			: base(CreateMessage(expression, factors, Messages.CadastralPriceCalculationError))
		{

		}

		protected CalculationException(Expression expression, List<FactorInfo> factors, string baseMessagePart)
			: base(CreateMessage(expression, factors, baseMessagePart))
		{

		}

		//TODO тесты на ковертер
		private static string CreateMessage(Expression expression, List<FactorInfo> factors, string baseMessagePart)
		{
			var expressionStr = expression.getExpressionString();
			for (var i = 0; i < expression.getArgumentsNumber(); i++)
			{
				var argument = expression.getArgument(i);
				var argumentName = argument.getArgumentName();
				
				var factorIdStr = argumentName.Split(CalculateCadastralPriceLongProcess.AttributePrefixInFormula)
					.ElementAtOrDefault(1);

				var valueToReplaceInFormula = argument.getArgumentValue().ToString();
				if (!string.IsNullOrWhiteSpace(factorIdStr))
				{
					if(long.TryParse(factorIdStr, out var factorId))
					{
						var factor = factors.FirstOrDefault(x => x.FactorId == factorId);
						valueToReplaceInFormula = $"{valueToReplaceInFormula} ({factor?.AttributeName})";
					}
				}
				expressionStr = expressionStr.Replace($"{argumentName}", $"[{valueToReplaceInFormula}]");
			}

			return $"{baseMessagePart}. Формула: '{expressionStr}'";
		}
	}
}
