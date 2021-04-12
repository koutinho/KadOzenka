﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.Registers.Entities;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.GbuCod
{
	public class CodDictionaryValueModel: IValidatableObject
	{
		public long Id { get; set; }
        public long DictionaryId { get; set; }

		[Display(Name = "Значение")]
		public string Value { get; set; }

		[Display(Name = "Код")]
		public string Code { get; set; }

        public List<CodDictionaryValuePure> Values { get; set; }
        public List<AttributePure> RegisterAttributes { get; private set; }



		public static CodDictionaryValueModel ToModel(OMCodJob dictionary, CodDictionaryValue dictionaryValue = null)
        {
            var registerAttributes = CodDictionaryService.GetDictionaryRegisterAttributes(dictionary.RegisterId)
                .Where(x => x.Name != CodDictionaryConsts.CodeColumnName)
                .Select(x => new AttributePure
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

			return new CodDictionaryValueModel
            {
                Id = dictionaryValue?.Id ?? -1,
				DictionaryId = dictionary.Id,
                Code = dictionaryValue?.Code,
				RegisterAttributes = registerAttributes,
                Values = dictionaryValue?.Values ?? new List<CodDictionaryValuePure>()
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

        public CodDictionaryValue ToDto()
        {
            return new CodDictionaryValue(Id, Code, Values);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return CodDictionaryService.ValidateDictionaryValue(ToDto());
        }


        #region Support Methods


        #endregion
    }
}
