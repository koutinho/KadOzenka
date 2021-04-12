using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.GbuCod
{
	public class CodDictionaryValueModel
	{
		public long Id { get; set; }
		public long JobId { get; set; }

		[Display(Name = "Значение")]
		public string Value { get; set; }

		[Display(Name = "Код")]
		public string Code { get; set; }



        public static CodDictionaryValueModel ToModel()
        {
            return new CodDictionaryValueModel
            {
                Id = -1
            };
		}

		public static CodDictionaryValueModel FromEntity(OMCodDictionary entity)
		{
			if (entity == null)
			{
				return new CodDictionaryValueModel
				{
					Id = -1
				};
			}

			return new CodDictionaryValueModel
			{
				Id = entity.Id,
				Code = entity.Code,
				Value = entity.Value
            };
		}

		public static void ToEntity(CodDictionaryValueModel valueModel, ref OMCodDictionary codDictionary)
		{
			codDictionary.IdCodjob = valueModel.JobId;
			codDictionary.Code = valueModel.Code;
			codDictionary.Value = valueModel.Value;
        }
	}
}
