using System;

namespace KadOzenka.Dal.Tasks.InheritanceFactorSettings.Exceptions
{
	public class InheritanceFactorNotFoundException : Exception
	{
		public InheritanceFactorNotFoundException(long id) 
			: base($"Не найдена настройка с ИД '{id}'")
		{
			
		}
	}
}
