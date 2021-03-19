using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Web.Models.GbuObject.ObjectAttributes;
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
		public int RegisterId { get; set; }

		[Display(Name = "Кадастровый номер")]
		public string CadastralNumber { get; set; }

		[Display(Name = "Тип объекта недвижимости")]
		public PropertyTypes? ObjectType { get; set; }

		[Display(Name = "Дата актуализации")]
		public DateTime? DateActual { get; set; }

        public List<RegisterDto> RegisterDtoList { get; set; }

        public static GbuObjectViewModel FromEntity(OMMainObject entity, DateTime? dateActual, List<RegisterDto> registerDtoList)
		{
			if (entity == null)
			{
				return new GbuObjectViewModel
				{
					Id = -1,
					RegisterId = OMMainObject.GetRegisterId(),
					DateActual = dateActual,
				    RegisterDtoList = registerDtoList
                };
			}

			return new GbuObjectViewModel
			{
				Id = entity.Id,
				RegisterId = OMMainObject.GetRegisterId(),
				CadastralNumber = entity.CadastralNumber,
				ObjectType = entity.ObjectType_Code,
				DateActual = dateActual,
                RegisterDtoList = registerDtoList
			};
		}
	}
}
