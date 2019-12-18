using System;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations.DeclarationTabModel
{
	public class DeclarationCharacteristicModel
	{
		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		public long? Id { get; set; }

		/// <summary>
		/// Идентификатор декларации (Declaration_Id)
		/// </summary>
		public long? DeclarationId { get; set; }

		/// <summary>
		/// Тип объекта
		/// </summary>
		public ObjectType? ObjectType { get; set; }

		public static DeclarationCharacteristicModel FromEntity(long? id, OMDeclaration declaration)
		{
			return new DeclarationCharacteristicModel
			{
				Id = id.HasValue ? id.Value : -1,
				DeclarationId = declaration?.Id,
				ObjectType = !string.IsNullOrEmpty(declaration?.TypeObj) ? declaration?.TypeObj_Code : (ObjectType?)null
			};
		}
	}
}
