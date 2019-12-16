using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ObjectModel.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class DeclarationModel
	{
		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		[Display(Name = "Идентификатор")]
		public long Id { get; set; }

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
				Id = entity.Id
			};
		}
	}
}
