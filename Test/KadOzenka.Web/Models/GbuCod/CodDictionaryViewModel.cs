using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.GbuCod
{
	public class CodDictionaryViewModel
	{
		public long Id { get; set; }
		public long JobId { get; set; }

		[Display(Name = "Значение")]
		public string Value { get; set; }

		[Display(Name = "Код")]
		public string Code { get; set; }

		[Display(Name = "Источник")]
		public string Source { get; set; }

		[Display(Name = "Эксперт")]
		public string Expert { get; set; }

		public static CodDictionaryViewModel FromEntity(OMCodDictionary entity)
		{
			if (entity == null)
			{
				return new CodDictionaryViewModel
				{
					Id = -1
				};
			}

			return new CodDictionaryViewModel
			{
				Id = entity.Id,
				Code = entity.Code,
				Value = entity.Value,
				Source = entity.Source,
				Expert = entity.Expert
			};
		}

		public static void ToEntity(CodDictionaryViewModel viewModel, ref OMCodDictionary codDictionary)
		{
			codDictionary.IdCodjob = viewModel.JobId;
			codDictionary.Code = viewModel.Code;
			codDictionary.Value = viewModel.Value;
			codDictionary.Source = viewModel.Source;
			codDictionary.Expert = viewModel.Expert;
		}
	}
}
