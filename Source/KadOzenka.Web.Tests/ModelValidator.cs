using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KadOzenka.Web.Tests
{
	public static class ModelValidator
	{
		public static List<ValidationResult> Validate(object model)
		{
			if (!(model is IValidatableObject))
			{
				throw new Exception($"Модель типа {model.GetType()} не реализует интерфейс '{nameof(IValidatableObject)}'");
			}

			var results = new List<ValidationResult>();
			var validationContext = new ValidationContext(model, null, null);

			Validator.TryValidateObject(model, validationContext, results, true);

			return results;
		}

		public static string GetAllErrorMessagesAsOneString(this List<ValidationResult> errors)
		{
			return string.Join('\n', errors.Select(x => x.ErrorMessage).ToList());
		}
	}
}
