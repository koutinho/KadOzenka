using System;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;
using ObjectModel.Gbu;

namespace KadOzenka.Web.Models.GbuObject
{
	public class GbuObjectViewModel
	{
		/// <summary>
		/// Идентификатор ГБУ объекта
		/// </summary>
		public long Id { get; set; }

		[Display(Name = "Кадастровый номер")]
		public string CadastralNumber { get; set; }

		[Display(Name = "Тип объекта недвижимости")]
		public PropertyTypes? ObjectType { get; set; }

		[Display(Name = "Дата актуализации")]
		public DateTime? DateActual { get; set; }

		public static GbuObjectViewModel FromEntity(OMMainObject entity, DateTime? dateActual)
		{
			if (entity == null)
			{
				return new GbuObjectViewModel
				{
					Id = -1,
					DateActual = dateActual
				};
			}

			return new GbuObjectViewModel
			{
				Id = entity.Id,
				CadastralNumber = entity.CadastralNumber,
				ObjectType = entity.ObjectType_Code,
				DateActual = dateActual
			};
		}
	}
}
