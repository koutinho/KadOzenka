using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class DeclarationModel
	{
		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		[Display(Name = "Идентификатор")]
		public long Id { get; set; }

		/// <summary>
		/// Податель декларации (OWNER_TYPE)
		/// </summary>
		[Display(Name = "Подача декларации")]
		public OwnerType? OwnerType { get; set; }

		public static DeclarationModel FromEntity(OMDeclaration entity)
		{
			if (entity == null)
			{
				return new DeclarationModel
				{
					Id = -1
				};
			}
			return new DeclarationModel
			{
				Id = entity.Id,
				OwnerType = entity.OwnerType_Code
			};
		}

		public static void ToEntity(DeclarationModel declarationViewModel, ref OMDeclaration entity)
		{
			if (declarationViewModel.OwnerType.HasValue)
			{
				entity.OwnerType_Code = declarationViewModel.OwnerType.Value;
			}
		}
	}
}
